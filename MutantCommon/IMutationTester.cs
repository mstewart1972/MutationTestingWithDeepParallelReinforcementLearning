using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutantCommon
{
    public interface IMutationTester: IDisposable
    {

        Task<MutationTestingStateModel> MutationTest(IProgress<MutationTestingStateModel> progress);
        ISet<Class> AvailableClasses();
        void Cancel();

        IEnumerable<Class> TestableClasses { get; }


    }
}
