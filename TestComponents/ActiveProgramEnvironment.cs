using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.FileSystemManagers;
using Utility.PathProviders;

namespace TestComponents
{
    public class ActiveProgramEnvironment : IActiveProgramEnvironment
    {
        private readonly IDirectoryManager _directoryManager;
        private readonly IPathProvider _paths;
        public ActiveProgramEnvironment(IPathProvider paths, IDirectoryManager directoryManager)
        {
            _paths = paths;
            _directoryManager = directoryManager;
        }

        public void Load(IProgramVersion version)
        {
            _directoryManager.CopyFile(version.SourceFileCopyPath, _paths.CopiedSourceFilepath, force: true);
        }
    }
}
