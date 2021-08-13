using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.FileSystemManagers
{
    public interface IDirectoryManager
    {
        void CopyFile(string sourceFile, string destFile, bool force = false);
        void CleanDirectory(String directoryPath);
        void DeleteAndRecreateDirectory(String directoryPath);
        void DeleteDirectory(String directoryPath);
        void CreateDirectory(String directoryPath);
        void CopyDirectory(String directoryToBackup, String destinationDirectory);
    }
}
