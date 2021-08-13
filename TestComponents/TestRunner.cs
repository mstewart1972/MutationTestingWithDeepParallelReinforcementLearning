using MutantCommon;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestComponents
{
    public abstract class TestRunner : ITestRunner
    {
        public string ToolName = "";
        public int testCaseCount = 0;        

        public abstract bool RunExternalTestToolForSolution(string inputFile, string outputFile, ISet<Unittest> tests);
        public abstract Task<bool> RunExternalTestToolForSolutionAsync(string inputFile, string outputFile, ISet<Unittest> tests);
        public abstract TestResult ProcessResultFile(string fileName);
        public TestResult TestSolution(string inputFile, string outputFile, ISet<Unittest> tests)
        {
            RunExternalTestToolForSolution(inputFile, outputFile, tests);
            return ProcessResultFile(outputFile);
        }

        public async Task<TestResult> TestSolutionAsync(string inputFile, string outputFile, ISet<Unittest> tests)
        {
            await RunExternalTestToolForSolutionAsync(inputFile, outputFile, tests);
            return ProcessResultFile(outputFile);
        }
    }
}
