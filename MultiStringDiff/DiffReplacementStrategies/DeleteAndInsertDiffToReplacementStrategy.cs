using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiffMatchPatch;

namespace MultiStringDiff
{
    public class DeleteAndInsertDiffsToReplacementStrategy : IDiffsToReplacementStrategy
    {
        public bool AddReplacementTo(IList<StringReplacement> replacements, int position, Diff diff1, Diff diff2)
        {
            replacements.Add(new StringReplacement { Position = position, NewString = diff2.text, OldString = diff1.text });
            return true;
        }

        public int newPosition(int oldPosition, Diff diff1, Diff diff2)
        {
            return oldPosition + diff1.text.Length;
        }
    }
}
