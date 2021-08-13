using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public interface ITestFramework
    {
        TestResult Test(IProgramVersion program);
        Task<TestResult> TestAsync(IProgramVersion program);
        ISet<Class> GetTestedClasses();
        ISet<Unittest> GetUnittests();
        IClassTestCoverage GetCoverage();
    }
}
