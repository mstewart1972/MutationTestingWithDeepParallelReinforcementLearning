using Moq;
using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestComponents;
using Utility.FileSystemManagers;
using Utility.PathProviders;
using Xunit;

namespace Test.TestComponents
{
    public class TestActiveProgramEnvironment
    {
        [Fact]
        public void TestLoadCopiesFiles()
        {
            var directoryManager = new Mock<IDirectoryManager>();
            var paths = new Mock<IPathProvider>();
            var programVersion = new Mock<IProgramVersion>();
            var copyFilePath = "COPYFILEPATH";
            var versionFilePath = "VERSIONFILEPATH";

            var environment = new ActiveProgramEnvironment(paths.Object, directoryManager.Object);

            paths.Setup(p => p.CopiedSourceFilepath).Returns(copyFilePath);
            programVersion.Setup(p => p.SourceFileCopyPath).Returns(versionFilePath);

            environment.Load(programVersion.Object);

            directoryManager.Verify(r => r.CopyFile(versionFilePath, copyFilePath, true));
            directoryManager.VerifyNoOtherCalls();
        }
    }
}
