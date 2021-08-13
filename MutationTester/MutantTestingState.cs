using MutantCommon;
using System;
using System.Collections.Generic;

namespace MutantTester
{
    public class MutantTestingState : IMutationTestingState
    {
        private MutationTestingOperation operation;
        private IClassTestCoverage coverage = null;
        private IDictionary<Class, IList<StringSectionModel>> diffs = new Dictionary<Class, IList<StringSectionModel>>();
        private readonly ISet<IMutant> mutants = new HashSet<IMutant>();
        private readonly IList<string> errors = new List<string>();

        public void BeginOperation(MutationTestingOperation operation)
        {
            this.operation = operation | this.operation;
        }

        public void BeginOperation(MutationTestingOperation operation, IProgress<MutationTestingStateModel> progress)
        {
            BeginOperation(operation);
            UpdateProgress(progress);
        }

        public MutationTestingStateModel CreateModel()
        {
            var mutantModels = new List<MutantModel>();
            int killed = 0;
            int tested = 0;
            int total = 0;
            TestResult testResult;
            foreach (var mutant in mutants)
            {
                mutantModels.Add(mutant.Model);
                total++;
                testResult = mutant.TestResult;
                if(testResult != null)
                {
                    tested++;
                    if (!testResult.Survived)
                    {
                        killed++;
                    }
                }
            }
            double mutationScore = 0;
            double percentComplete = 0;
            if (total > 0) {
                percentComplete = (double)tested / total;
                mutationScore = (double)killed / total;
            }

            int testCaseCount;
            List<Class> unittestedClasses;
            if(coverage != null)
            {
                unittestedClasses = new List<Class>(coverage.AllTestedClasses());
                testCaseCount = coverage.Unittests().Count;
            }
            else
            {
                testCaseCount = 0;
                unittestedClasses = new List<Class>();
            }


            var modelCopy = new MutationTestingStateModel
            {
                CurrentOperation = operation,
                Diffs = new Dictionary<Class, IList<StringSectionModel>>(diffs),
                UnittestedClasses = unittestedClasses,
                TestCaseCount = testCaseCount,
                TotalMutants = total,
                MutantsTested = tested,
                KilledMutants = killed,
                Mutants = mutantModels,
                PercentComplete = percentComplete,
                MutationScore = mutationScore,
                EquivalentMutants = 0,
                Errors = new List<string>(errors)
            };
            return modelCopy;
        }

        public void EndOperation(MutationTestingOperation operation)
        {
            this.operation = (~operation) & this.operation;
        }

        public void EndOperation(MutationTestingOperation operation, IProgress<MutationTestingStateModel> progress)
        {
            EndOperation(operation);
            UpdateProgress(progress);
        }

        public void UpdateTestCoverage(IClassTestCoverage coverage)
        {
            this.coverage = coverage;
        }

        public void UpdateTestCoverage(IClassTestCoverage coverage, IProgress<MutationTestingStateModel> progress)
        {
            UpdateTestCoverage(coverage);
            UpdateProgress(progress);
        }

        public void UpdateDiffs(IDictionary<Class, IList<StringSectionModel>> diffs)
        {
            this.diffs = diffs;
        }

        public void UpdateDiffs(IDictionary<Class, IList<StringSectionModel>> diffs, IProgress<MutationTestingStateModel> progress)
        {
            UpdateDiffs(diffs);
            UpdateProgress(progress);
        }

        public void UpdateMutant(IMutant mutant)
        {
            mutants.Add(mutant);
        }

        public void UpdateMutant(IMutant mutant, IProgress<MutationTestingStateModel> progress)
        {
            UpdateMutant(mutant);
            UpdateProgress(progress);
        }

        private void UpdateProgress(IProgress<MutationTestingStateModel> progress)
        {
            if(progress != null)
            {
                progress.Report(CreateModel());
            }
        }

        public void AddError(string error)
        {
            errors.Add(error);
        }

        public void AddError(string error, IProgress<MutationTestingStateModel> progress)
        {
            AddError(error);
            UpdateProgress(progress);
        }
    }
}
