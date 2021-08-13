using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiffMatchPatch;

namespace MultiStringDiff
{
    public class DefaultDiffToReplacementStrategy : IDiffsToReplacementStrategy
    {
        public bool AddReplacementTo(IList<StringReplacement> replacements, int position, Diff diff1, Diff diff2)
        {
            return false;
        }

        public int newPosition(int oldPosition, Diff diff1, Diff diff2)
        {
            return oldPosition + diff1.text.Length;
        }
    }
}
