using System.Collections.Generic;
using System.Threading.Tasks;
using MutantCommon;

namespace TestComponents
{
    public interface ITestRunner
    {
        TestResult ProcessResultFile(string fileName);
        bool RunExternalTestToolForSolution(string inputFile, string outputFile, ISet<Unittest> tests);
        TestResult TestSolution(string inputFile, string outputFile, ISet<Unittest> tests);
        Task<TestResult> TestSolutionAsync(string inputFile, string outputFile, ISet<Unittest> tests);
    }
}