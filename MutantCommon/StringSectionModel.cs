using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public class StringSectionModel
    {
        public string BaseString { get; set; }
        public IDictionary<string, string> Prefixes { get; set; }
        public IDictionary<string, string> Alternatives { get; set; }
        public bool HasDiffs { get => (Prefixes.Count != 0 || Alternatives.Count != 0); }
    }
}
