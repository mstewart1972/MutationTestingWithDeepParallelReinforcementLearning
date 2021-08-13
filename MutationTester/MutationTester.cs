using Decompiler;
// MAS 20210118
using Filters.MutantFilters;
//
using MultiStringDiff;
using MutantCommon;
using MutantGeneration.MutantGenerators;
using SolutionBuilder;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestComponents;
using Utility.FileSystemManagers;
using Utility.PathProviders;

namespace MutantTester
{
    public class MutationTester : IMutationTester
    {
        private bool disposed = false;

        public IEnumerable<Class> TestableClasses => throw new NotImplementedException();


        private readonly IMutationTestingState state;
        private readonly InputParameters inputParameters;
        private readonly IDirectoryManager directoryManager;
        private readonly IPathProvider paths;
        private readonly ISolutionBuilder solutionBuilder;
        private readonly ITestFramework testFramework;
        private readonly IMutantGenerator mutantGenerator;
        private readonly IDiffCalculator diffCalculator;
        private readonly OriginalProgram originalProgram;
        // MAS 20210118
        private readonly IMutantFilter mutantAllFilter;
        private readonly IMutantFilter mutantLimitedFilter;
        private readonly IMutantFilter mutantMachineLearningFilter;
        //
        private CancellationTokenSource cancellationTokenSource;

        public static MutationTester CreateMutationTester(InputParameters input)
        {
            var _state = new MutantTestingState();
            var _inputParameters = input;
            var _directoryManager = new DirectoryManager();
            // MAS 20210224
            var _paths = new PathProvider(input.WorkingDirectory + "\\" + input.AgentNumber.ToString(), input.SourceFileName, input.SolutionPath, input.TestFileName, input.ReportFolder);
            //var _paths = new PathProvider(input.WorkingDirectory, input.SourceFileName, input.SolutionPath, input.TestFileName, input.ReportFolder);
            var buildPath = new DirectoryPath(_paths.BuildDirectory);
            var solutionPath = new FilePath(_paths.SolutionPath);
            var dotnetPath = new FilePath(_paths.DotnetPath);
            var _solutionBuilder = new DotnetSolutionBuilder(dotnetPath, solutionPath, buildPath);
            var _originalProgram = new OriginalProgram(_paths.TemporarySourceFilepath);
            var environment = new ActiveProgramEnvironment(_paths, new DirectoryManager());
            var testRunner = new DotnetTestRunnerBatched(_paths, input.TestBatchSize, input.Verbose);
            var classNamesToAvoid = new List<string> { "System", "NUnit", "!!0", "Moq.Mock" };
            var classNameFilter = new ClassNameFilter(classNamesToAvoid);
            var coverageCalculator = new TestCoverageCalculator(buildPath, classNameFilter);
            var _testFramework = new DotnetTestFramework(_paths, testRunner, coverageCalculator, environment);
            var _mutantGenerator = new MutantGenerator(_paths);
            var decompiler = new DllDecompiler(new DecompiledClassFactory());
            var _diffCalculator = new DiffCalculator(decompiler);
            return new MutationTester(_state, _inputParameters, _directoryManager, _paths, _solutionBuilder, _testFramework, _mutantGenerator, _diffCalculator, _originalProgram);
        }

        public MutationTester(InputParameters input)
        {
            state = new MutantTestingState();
            inputParameters = input;
            directoryManager = new DirectoryManager();
            // MAS 20210224
            paths = new PathProvider(input.WorkingDirectory + "\\" + input.AgentNumber.ToString(), input.SourceFileName, input.SolutionPath, input.TestFileName, input.ReportFolder);
            var buildPath = new DirectoryPath(paths.BuildDirectory);
            var solutionPath = new FilePath(paths.SolutionPath);
            var dotnetPath = new FilePath(paths.DotnetPath);
            solutionBuilder = new DotnetSolutionBuilder(dotnetPath, solutionPath, buildPath);
            originalProgram = new OriginalProgram(paths.TemporarySourceFilepath);
            var environment = new ActiveProgramEnvironment(paths, new DirectoryManager());
            var testRunner = new DotnetTestRunnerBatched(paths, input.TestBatchSize, input.Verbose);
            var classNamesToAvoid = new List<string> { "System", "NUnit", "!!0", "Moq.Mock" };
            var classNameFilter = new ClassNameFilter(classNamesToAvoid);
            var coverageCalculator = new TestCoverageCalculator(buildPath, classNameFilter);
            testFramework = new DotnetTestFramework(paths, testRunner, coverageCalculator, environment);
            mutantGenerator = new MutantGenerator(paths);
            var decompiler = new DllDecompiler(new DecompiledClassFactory());
            diffCalculator = new DiffCalculator(decompiler);

            // MAS 20210118
            mutantAllFilter = new AllMutantsFilter();
            mutantLimitedFilter = new LimitedNumberMutantFilter(2);
            mutantMachineLearningFilter = new MachineLearningMutantFilter(2);
            //
        }

        private MutationTester(
            IMutationTestingState state,
            InputParameters input,
            IDirectoryManager directoryManager,
            IPathProvider paths,
            ISolutionBuilder solutionBuilder,
            ITestFramework testFramework,
            IMutantGenerator mutantGenerator,
            IDiffCalculator diffCalculator,
            OriginalProgram originalProgram
            )
        {
            this.state = state;
            this.inputParameters = input;
            this.directoryManager = directoryManager;
            this.paths = paths;
            this.solutionBuilder = solutionBuilder;
            this.testFramework = testFramework;
            this.mutantGenerator = mutantGenerator;
            this.diffCalculator = diffCalculator;
            this.originalProgram = originalProgram;

            // MAS 20210118
            mutantAllFilter = new AllMutantsFilter();
            mutantLimitedFilter = new LimitedNumberMutantFilter(2);
            mutantMachineLearningFilter = new MachineLearningMutantFilter(2);
            //
        }

        public void Cancel()
        {
            cancellationTokenSource.Cancel();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                //Dispose resources
                Dispose();
            }
            disposed = true;
        }

        public async Task<MutationTestingStateModel> MutationTest(IProgress<MutationTestingStateModel> progress)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            try
            {
                return await MutationTest(cancellationToken, progress).ConfigureAwait(false);
            }
            catch(Exception e)
            {
                state.AddError(e.Message, progress);
                ResetState(progress);
                state.BeginOperation(MutationTestingOperation.Error, progress);
                return state.CreateModel();
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }

        private async Task<MutationTestingStateModel> MutationTest(CancellationToken cancellationToken, IProgress<MutationTestingStateModel> progress)
        {
            ResetState(progress);
            cancellationToken.ThrowIfCancellationRequested();

            CleanDirectories(progress);
            cancellationToken.ThrowIfCancellationRequested();

            await BuildOriginalCode(progress).ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();

            //await TestOriginal(progress).ConfigureAwait(false);
            //cancellationToken.ThrowIfCancellationRequested();

            var mutants = GenerateMutants(progress);
            cancellationToken.ThrowIfCancellationRequested();

            //// MAS 20210126
            // back propagate (learn)
            //var fmutants = FilterMutants(mutants, cancellationToken, progress);
            //cancellationToken.ThrowIfCancellationRequested();

            await TestMutants(mutants, cancellationToken, progress).ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();

            //CreateDiffs(mutants, progress);
            //cancellationToken.ThrowIfCancellationRequested();

            state.BeginOperation(MutationTestingOperation.Complete, progress);
            return state.CreateModel();
        }

        private void ResetState(IProgress<MutationTestingStateModel> progress)
        {
            // MAS 20210126
            state.EndOperation(
                MutationTestingOperation.FilteringMutants |
                MutationTestingOperation.Error |
                MutationTestingOperation.Complete |
                MutationTestingOperation.BuildingSolution |
                MutationTestingOperation.Diffing |
                MutationTestingOperation.GeneratingMutants |
                MutationTestingOperation.TestingMutants |
                MutationTestingOperation.TestingOriginalCode,
                progress);
        }

        private void CleanDirectories(IProgress<MutationTestingStateModel> progress)
        {
            directoryManager.DeleteAndRecreateDirectory(paths.CopyDirectory);

        }

        private async Task BuildOriginalCode(IProgress<MutationTestingStateModel> progress)
        {
            if (inputParameters.BuildSolution)
            {
                state.BeginOperation(MutationTestingOperation.BuildingSolution, progress);
                await solutionBuilder.BuildAsync(inputParameters.Verbose).ConfigureAwait(false);
            }
            // MAS 20210216 prevent sporadic file errors
            //directoryManager.DeleteAndRecreateDirectory(paths.TemporaryOriginalCodeDirectory);
            directoryManager.DeleteDirectory(paths.TemporaryOriginalCodeDirectory);
            directoryManager.CreateDirectory(paths.TemporaryOriginalCodeDirectory);
            directoryManager.CopyFile(paths.CopiedSourceFilepath, paths.TemporarySourceFilepath, true);
            
            state.EndOperation(MutationTestingOperation.BuildingSolution, progress);
        }

        private async Task TestOriginal(IProgress<MutationTestingStateModel> progress)
        {
            state.BeginOperation(MutationTestingOperation.TestingOriginalCode, progress);
            var result = await originalProgram.TestAsync(testFramework).ConfigureAwait(false);
            state.EndOperation(MutationTestingOperation.TestingOriginalCode, progress);
        }

        private IClassTestCoverage CalculateTestCoverage(IProgress<MutationTestingStateModel> progress)
        {
            var coverage = testFramework.GetCoverage();
            state.UpdateTestCoverage(coverage, progress);
            return coverage;
        }

        private IEnumerable<IMutant> GenerateMutants(IProgress<MutationTestingStateModel> progress)
        {
            state.BeginOperation(MutationTestingOperation.GeneratingMutants, progress);
            var coverage = CalculateTestCoverage(progress);
            var testedClasses = coverage.AllTestedClasses();
            // MAS 20210216
            //directoryManager.DeleteAndRecreateDirectory(paths.TemporaryOriginalCodeDirectory);
            //directoryManager.DeleteAndRecreateDirectory(paths.MutantCodeDirectory);
            directoryManager.DeleteDirectory(paths.TemporaryOriginalCodeDirectory);
            directoryManager.DeleteDirectory(paths.MutantCodeDirectory);
            directoryManager.CreateDirectory(paths.TemporaryOriginalCodeDirectory);
            directoryManager.CreateDirectory(paths.MutantCodeDirectory);


            directoryManager.CopyDirectory(paths.CopyDirectory, paths.TemporaryOriginalCodeDirectory);

            // MAS 20210130
            IEnumerable<IMutant> mutants = mutantGenerator.Generate(inputParameters.Category, inputParameters.Operators, testedClasses);

            // MAS 20210118
            // Dynamically filter Mutants based on Reinforcement Learning
            // This does not limit mutant generation, just mutant testing so may not be ideal?
            //mutants = mutantAllFilter.Filter(mutants);
            //mutants = mutantLimitedFilter.Filter(mutants);
            //

            //foreach mutant add mutant model to state
            foreach (var mutant in mutants)
            {
                state.UpdateMutant(mutant, progress);
            }

            state.EndOperation(MutationTestingOperation.GeneratingMutants, progress);
            return mutants;
        }

        private IEnumerable<IMutant> FilterMutants(IEnumerable<IMutant> mutants, CancellationToken cancellationToken, IProgress<MutationTestingStateModel> progress)
        {
            cancellationToken.ThrowIfCancellationRequested();
            state.BeginOperation(MutationTestingOperation.FilteringMutants, progress);

            // back propagate (learn)
            IEnumerable<IMutant> fmutants = mutantMachineLearningFilter.Filter(mutants);

            state.EndOperation(MutationTestingOperation.FilteringMutants, progress);
            return fmutants;
        }

        private async Task TestMutants(IEnumerable<IMutant> mutants, CancellationToken cancellationToken, IProgress<MutationTestingStateModel> progress)
        //private void TestMutants(IEnumerable<IMutant> mutants, CancellationToken cancellationToken, IProgress<MutationTestingStateModel> progress)
        {
            cancellationToken.ThrowIfCancellationRequested();
            state.BeginOperation(MutationTestingOperation.TestingMutants, progress);

            // serial loop
            foreach (var mutant in mutants)
            {
                await mutant.TestAsync(testFramework);
                //mutant.Test(testFramework);
                //update state with mutant result
                state.UpdateMutant(mutant, progress);
                if (cancellationToken.IsCancellationRequested)
                {
                    state.EndOperation(MutationTestingOperation.TestingMutants, progress);
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }

            // parallel loop
            //Parallel.ForEach(mutants, async(mutant) =>
            // {
            //     await mutant.TestAsync(testFramework);
            //    //update state with mutant result
            //    state.UpdateMutant(mutant, progress);
            //     if (cancellationToken.IsCancellationRequested)
            //     {
            //         state.EndOperation(MutationTestingOperation.TestingMutants, progress);
            //         cancellationToken.ThrowIfCancellationRequested();
            //     }
            // });

            state.EndOperation(MutationTestingOperation.TestingMutants, progress);
        }

        private IDictionary<Class, IList<StringSectionModel>> CreateDiffs(IEnumerable<IMutant>mutants, IProgress<MutationTestingStateModel> progress)
        {
            IDictionary<Class, IList<StringSectionModel>> diffs;
            state.BeginOperation(MutationTestingOperation.Diffing, progress);
            try
            {
                diffs = diffCalculator.Calculate(originalProgram, mutants);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("Decompile error: {0}", ex.Message);
                state.AddError(errorMessage, progress);
                diffs = new Dictionary<Class, IList<StringSectionModel>>();
            }

            state.UpdateDiffs(diffs, progress);
            state.EndOperation(MutationTestingOperation.Diffing, progress);
            return diffs;
        }

        public ISet<Class> AvailableClasses()
        {
            throw new NotImplementedException();
        }
    }
}
