using DiffMatchPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStringDiff
{
    public interface IDiffsToReplacementStrategy
    {
        bool AddReplacementTo(IList<StringReplacement> replacements, int position, Diff diff1, Diff diff2);
        int newPosition(int oldPosition, Diff diff1, Diff diff2);
    }
}
