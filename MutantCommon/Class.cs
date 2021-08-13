using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public class Class : IEquatable<Class>
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Class);
        }

        public bool Equals(Class other)
        {
            return other != null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
