using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public interface IMutationTestingState
    {
        MutationTestingStateModel CreateModel();
        void BeginOperation(MutationTestingOperation operation, IProgress<MutationTestingStateModel> progress);
        void EndOperation(MutationTestingOperation operation, IProgress<MutationTestingStateModel> progress);
        void UpdateMutant(IMutant mutant, IProgress<MutationTestingStateModel> progress);
        void UpdateTestCoverage(IClassTestCoverage coverage, IProgress<MutationTestingStateModel> progress);
        void UpdateDiffs(IDictionary<Class, IList<StringSectionModel>> diffs, IProgress<MutationTestingStateModel> progress);
        void AddError(string error, IProgress<MutationTestingStateModel> progress);
    }
}
