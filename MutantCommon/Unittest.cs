using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public class Unittest : IEquatable<Unittest>
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Unittest);
        }

        public bool Equals(Unittest other)
        {
            return other != null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
