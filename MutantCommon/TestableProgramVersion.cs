using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public abstract class TestableProgramVersion : IProgramVersion
    {
        public abstract string SourceFileCopyPath { get; }
        public abstract string Name { get; }
        public abstract ISet<Unittest> TestsToRun(IClassTestCoverage coverage);
        public TestResult TestResult { get; private set; }
        public virtual TestResult Test(ITestFramework framework)
        {
            TestResult = framework.Test(this);
            return TestResult;
        }

        public async Task<TestResult> TestAsync(ITestFramework framework)
        {
            TestResult = await framework.TestAsync(this).ConfigureAwait(false);
            return TestResult;
        }
    }
}
