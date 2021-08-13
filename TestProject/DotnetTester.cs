using ExternalProgramExecution;
using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.PathProviders;

namespace MutantTestRunner
{
    public class DotnetTester : ExternalProgramExecutor
    {
        private readonly IPathProvider _paths;
        private readonly string _resultDirectory;
        private const string arguments = "test -o {0} -v q --no-build -r {1} -l trx; {2} {3}";
        //0 is the build directory
        //1 is the directory to store the test results in
        //2 is the path to solution
        //3 is the optional filter command


        public DotnetTester(IPathProvider paths, string resultDirectory)
        {
            _paths = paths;
            _resultDirectory = resultDirectory;
        }




        public override string ProgramPath => _paths.DotnetPath;

        public override int MaxWaitTime => int.MaxValue;//Forever

        public override string Arguments => String.Format(arguments, _paths.BuildDirectory, _resultDirectory, _paths.SolutionPath, "");

        public override void OnUnsuccessful(string program, string arguments)
        {
            //Ignore unsuccessful exit code
        }
    }
}
