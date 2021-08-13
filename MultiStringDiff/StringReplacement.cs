using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStringDiff
{
    public class StringReplacement : IStringReplacement
    {
        public int Length { get { return OldString.Length; } }

        public int Position { get; set; }

        public string NewString { get; set; }

        public string OldString { get; set; }
    }
}
