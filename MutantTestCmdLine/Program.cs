using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MutantTesting.CommandLineInterface;
using Decompiler;
using MutantGeneration.MutantGenerators;
using MutantCommon;
using Utility.PathProviders;
using MultiStringDiff;
using Newtonsoft.Json;
using System.Diagnostics;
using Utility.FileSystemManagers;
using System.Threading.Tasks;
using Utility.Timers;
using MutantTester;

namespace MutantTesting
{
    static class Program
    {
        /// <summary>
        /// mutanttesting 
        /// --sourceDirs <path to source></path>
        /// --unitTestType <NUNIT|MSTest>
        /// --buildSolution True|False
        /// </summary>
        /// <param name="args"></param>

        static void Main(string[] args)
        {
            var wholeExecutionTimer = Stopwatch.StartNew();
            var console = new CLIConsole();
            var defaults = new DefaultInputParameters();
            var input = console.ParseInput(args, defaults);

            if (!input.ShouldRun)
            {
                return;
            }

            var solutionBuildTimer = new ProcessTimer<MutationTestingStateModel>(new LambdaChangeDetector<MutationTestingStateModel>(x => x.CurrentOperation.HasFlag(MutationTestingOperation.BuildingSolution)));
            var projectTestTimer = new ProcessTimer<MutationTestingStateModel>(new LambdaChangeDetector<MutationTestingStateModel>(x => x.CurrentOperation.HasFlag(MutationTestingOperation.TestingOriginalCode)));
            var generatingMutantsTimer = new ProcessTimer<MutationTestingStateModel>(new LambdaChangeDetector<MutationTestingStateModel>(x => x.CurrentOperation.HasFlag(MutationTestingOperation.GeneratingMutants)));
            var mutantTestingTimer = new ProcessTimer<MutationTestingStateModel>(new LambdaChangeDetector<MutationTestingStateModel>(x => x.CurrentOperation.HasFlag(MutationTestingOperation.TestingMutants)));
            var diffingTimer = new ProcessTimer<MutationTestingStateModel>(new LambdaChangeDetector<MutationTestingStateModel>(x => x.CurrentOperation.HasFlag(MutationTestingOperation.Diffing)));


            var mutationTester = MutationTester.CreateMutationTester(input);
            var progress = new Progress<MutationTestingStateModel>();

            progress.ProgressChanged += (obj, model) => { PrintState(console, model); };
            progress.ProgressChanged += solutionBuildTimer.CheckTimer;
            progress.ProgressChanged += projectTestTimer.CheckTimer;
            progress.ProgressChanged += generatingMutantsTimer.CheckTimer;
            progress.ProgressChanged += mutantTestingTimer.CheckTimer;
            progress.ProgressChanged += diffingTimer.CheckTimer;

            var task = mutationTester.MutationTest(progress);
            task.Wait();
            var result = task.Result;

            IDirectoryManager directoryManager = new Utility.FileSystemManagers.DirectoryManager();
            // MAS 20210224
            var paths = new PathProvider(input.WorkingDirectory + "\\" + input.AgentNumber.ToString(), input.SourceFileName, input.SolutionPath, input.TestFileName, input.ReportFolder);
            //var paths = new PathProvider(input.WorkingDirectory, input.SourceFileName, input.SolutionPath, input.TestFileName, input.ReportFolder);

            var totaltimeElapsed = wholeExecutionTimer.Elapsed;
            var solutionBuildTime = solutionBuildTimer.GetStopwatch().Elapsed;
            var projectTestTime = projectTestTimer.GetStopwatch().Elapsed;
            var generationTime = generatingMutantsTimer.GetStopwatch().Elapsed;
            var mutantTestTime = mutantTestingTimer.GetStopwatch().Elapsed;
            var diffTime = diffingTimer.GetStopwatch().Elapsed;

            var mutantModels = result.Mutants;
            var testedClasses = result.UnittestedClasses;
            var diffs = result.Diffs;
            var errors = result.Errors;
            var times = new
            {
                total = totaltimeElapsed,
                solutionBuild = solutionBuildTime,
                projectTest = projectTestTime,
                mutantGeneration = generationTime,
                mutantTest = mutantTestTime,
                diffing = diffTime
            };

            var outputData = new
            {
                Summary = new
                {
                    TotalMutants = mutantModels.Count(),
                    Survived = mutantModels.Count(r => r.TestResult!=null && r.TestResult.Survived),
                    Killed = mutantModels.Count(r => r.TestResult!=null && !r.TestResult.Survived),
                    Score = mutantModels.Count() == 0 ? 0 : (double)mutantModels.Count(r => r.TestResult != null && !r.TestResult.Survived) / mutantModels.Count(),
                    TotalTime = times.total
                },
                //GeneratedOn = finishedAt,
                Classes = testedClasses,
                TotalTestCases = result.TestCaseCount,
                Mutants = mutantModels,
                MutantCodeDiff = diffs,
                ExecutionTimes = times,
                Errors = errors
            };

            console.Write($"Total time: {totaltimeElapsed}");
            console.Write($"Killed Mutants: {outputData.Summary.Killed}");
            console.Write($"Total Mutants: {outputData.Summary.TotalMutants}");
            console.Write($"Mutation Score: {outputData.Summary.Score}");
            if (errors.Count() != 0)
            {
                console.Write($"Errors:");
                foreach(string error in errors)
                {
                    console.Write(error);
                }
                console.Write();
            }
            console.Write(paths.ReportJsonFilepath);
            // MAS 20210216 - prevent sporadic file errors
            //directoryManager.DeleteAndRecreateDirectory(paths.ReportDirectory);
            directoryManager.DeleteDirectory(paths.ReportDirectory);
            directoryManager.CreateDirectory(paths.ReportDirectory);
            using (StreamWriter file = File.CreateText(paths.ReportJsonFilepath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, outputData);
            }
        }

        private static void PrintState(CLIConsole console, MutationTestingStateModel stateModel)
        {
            console.Write("Progress: " + stateModel.PercentComplete);
            console.Write("Current Operation: " + stateModel.CurrentOperation.ToString());
            
        }
    }
}
