using MutantCommon;
using System.Threading.Tasks;

namespace MutantTestRunner
{
    public abstract class TestRunner
    {
        public string ToolName = "";
        private string Name;
        public int testCaseCount = 0;        

        public abstract bool RunExternalTestToolForSolution(string inputFile, string outputFile);
        public abstract Task<bool> RunExternalTestToolForSolutionAsync(string inputFile, string outputFile);
        public abstract TestResult ProcessResultFile(string fileName);
    }
}
