using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.PathProviders
{
    public class Paths
    {
        public string Path { get; private set; }

        public Paths(string path)
        {
            Path = path;
        }

    }
}
