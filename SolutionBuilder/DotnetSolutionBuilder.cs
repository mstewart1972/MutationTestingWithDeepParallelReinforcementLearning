using ExternalProgramExecution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.PathProviders;

namespace SolutionBuilder
{
    public class DotnetSolutionBuilder : ExternalSolutionBuilder
    {
        private const string ARGUMENTS = "build -o {0} {1}";
        //0 is the build directory
        //1 is the path to solution

        private const int TEN_MINUTES = 600000;
        private readonly FilePath _dotnetPath;
        private readonly FilePath _solutionPath;
        private readonly DirectoryPath _buildPath;
        public DotnetSolutionBuilder(FilePath dotnetPath, FilePath solutionPath, DirectoryPath buildPath)
        {
            _dotnetPath = dotnetPath;
            _solutionPath = solutionPath;
            _buildPath = buildPath;
        }

        public override string ProgramPath => _dotnetPath.Path;

        public override int MaxWaitTime => TEN_MINUTES;

        public override string Arguments => String.Format(ARGUMENTS, _buildPath.Path, _solutionPath.Path);

        public override void CopyBuiltSolution()
        {
            //Solution Already built in correct place
        }
    }
}
