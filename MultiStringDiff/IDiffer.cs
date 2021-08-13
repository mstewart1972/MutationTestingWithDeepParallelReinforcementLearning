using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStringDiff
{
    public interface IDiffer
    {
        List<StringReplacement> Diff(string text1, string text2);
    }
}
