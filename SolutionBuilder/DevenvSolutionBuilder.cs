using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.FileSystemManagers;
using Utility.PathProviders;

namespace SolutionBuilder
{
    public class DevenvSolutionBuilder : ExternalSolutionBuilder
    {
        private const string ARGUMENTS = "{0} /rebuild \"Debug|AnyCpu\"";
        //0 is the path to the solution

        private const int TEN_MINUTES = 600000;
        private readonly DirectoryManager _directoryManager;
        private readonly FilePath _devenvPath;
        private readonly FilePath _solutionPath;
        private readonly FilePath _testProjectPath;
        private readonly DirectoryPath _buildPath;

        public DevenvSolutionBuilder(DirectoryManager directoryManager, FilePath devenvPath, FilePath solutionPath, FilePath testProjectPath, DirectoryPath buildPath)
        {
            _directoryManager = directoryManager;
            _devenvPath = devenvPath;
            _solutionPath = solutionPath;
            _testProjectPath = testProjectPath;
            _buildPath = buildPath;
        }

        public override string ProgramPath => _devenvPath.Path;

        public override int MaxWaitTime => TEN_MINUTES;

        public override string Arguments => String.Format(ARGUMENTS, _solutionPath.Path);

        public override void CopyBuiltSolution()
        {
            DirectoryPath testDir = _testProjectPath.GetParent();
            DirectoryPath binDir = testDir.SearchForDirectory("bin", true).Single();
            DirectoryPath debugDir = binDir.SearchForDirectory("Debug").Single();
            _directoryManager.DeleteAndRecreateDirectory(_buildPath.Path);
            _directoryManager.CopyDirectory(debugDir.Path, _buildPath.Path);
        }
    }
}
