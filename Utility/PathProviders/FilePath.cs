using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.PathProviders
{
    public class FilePath:Paths, IEquatable<FilePath>
    {
        public FilePath(string path):base(path)
        {
            
        }

        public FilePath(Paths path) : base(path.Path)
        {

        }

        public DirectoryPath GetParent()
        {
            return new DirectoryPath(new FileInfo(Path).DirectoryName);
        }

        public void Copy(FilePath destination, bool force = false)
        {
            File.Copy(Path, destination.Path, force);
        }

        public void Delete()
        {
            if (Exists)
            {
                File.Delete(Path);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FilePath);
        }

        public bool Equals(FilePath other)
        {
            return other != null &&
                   base.Equals(other) &&
                   Filename == other.Filename;
        }

        public override int GetHashCode()
        {
            var hashCode = 89913150;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Filename);
            return hashCode;
        }

        public bool Exists => File.Exists(Path);
        public string Filename => System.IO.Path.GetFileName(Path);
    }
}
