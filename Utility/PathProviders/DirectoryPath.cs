using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.PathProviders
{
    public class DirectoryPath : Paths, IEquatable<DirectoryPath>
    {
        private const string REPORT_TEMPLATE_FOLDER = "template";

        public DirectoryPath(string path) : base(path)
        {

        }

        public DirectoryPath(Paths path) : base(path.Path)
        {

        }

        public bool Exists => Directory.Exists(Path);

        public IEnumerable<FilePath> GetFiles()
        {
            var files = new List<FilePath>();
            foreach (var path in Directory.GetFiles(Path))
            {
                files.Add(new FilePath(path));
            }
            return files;
        }

        public IEnumerable<DirectoryPath> GetSubdirectories()
        {
            var directories = new List<DirectoryPath>();
            foreach (var directory in Directory.GetDirectories(Path))
            {
                directories.Add(new DirectoryPath(directory));
            }
            return directories;
        }

        public void DeleteAndRecreate()
        {
            if (Exists)
            {
                Delete(true);
            }
            Create();
        }

        public IEnumerable<FilePath> SearchForFile(string filename, bool recursive = false)
        {
            var found = new List<FilePath>();
            var childFile = ChildFilePath(filename);
            if (childFile.Exists)
            {
                found.Add(childFile);
            }
            if (recursive)
            {
                foreach (DirectoryPath subdirectory in GetSubdirectories())
                {
                    found.AddRange(subdirectory.SearchForFile(filename, recursive));
                }
            }
            return found;
        }

        public IEnumerable<DirectoryPath> SearchForDirectory(string dirname, bool recursive = false)
        {
            var found = new List<DirectoryPath>();
            var childDir = ChildDirectoryPath(dirname);
            if (childDir.Exists)
            {
                found.Add(childDir);
            }
            if (recursive)
            {
                foreach (DirectoryPath subdirectory in GetSubdirectories())
                {
                    found.AddRange(subdirectory.SearchForDirectory(dirname, recursive));
                }
            }
            return found;
        }

        public void Delete(bool force = false)
        {
            Directory.Delete(Path, force);
        }

        public void Create()
        {
            Directory.CreateDirectory(Path);
        }

        public FilePath ChildFilePath(string filename)
        {
            return new FilePath(System.IO.Path.Combine(Path, filename));
        }

        public DirectoryPath ChildDirectoryPath(string directoryname)
        {
            return new DirectoryPath(System.IO.Path.Combine(Path, directoryname));
        }

        public void CopyFiles(DirectoryPath destination)
        {
            foreach (var file in GetFiles())
            {
                file.Copy(destination.ChildFilePath(file.Filename), true);
            }
        }

        public void Clean()
        {
            //delete all generated folder
            var subdirectories = GetSubdirectories();
            IEnumerable<FilePath> files;
            foreach (var directory in subdirectories)
            {
                //not delete template folder
                if (!directory.Path.Contains(REPORT_TEMPLATE_FOLDER))
                {
                    files = directory.GetFiles();
                    foreach (var file in files)
                    {
                        file.Delete();
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DirectoryPath);
        }

        public bool Equals(DirectoryPath other)
        {
            return other != null &&
                   base.Equals(other) &&
                   Exists == other.Exists;
        }

        public override int GetHashCode()
        {
            var hashCode = 889106547;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Exists.GetHashCode();
            return hashCode;
        }
    }
}

