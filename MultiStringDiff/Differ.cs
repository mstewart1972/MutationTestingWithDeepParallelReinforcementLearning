using DiffMatchPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStringDiff
{
    public class Differ : IDiffer
    {
        private readonly diff_match_patch diffMatchPatch = new diff_match_patch();

        private readonly IDiffsToReplacementStrategy deleteEqualStrategy = new DeleteAndEqualDiffToReplacementStrategy();
        private readonly IDiffsToReplacementStrategy deleteInsertStrategy = new DeleteAndInsertDiffsToReplacementStrategy();
        private readonly IDiffsToReplacementStrategy insertEqualStrategy = new InsertAndEqualDiffToReplacementStrategy();
        private readonly IDiffsToReplacementStrategy defaultStrategy = new DefaultDiffToReplacementStrategy();

        public List<StringReplacement> Diff(string text1, string text2)
        {
            var diffs = diffMatchPatch.diff_main(text1, text2);
            diffMatchPatch.diff_cleanupSemantic(diffs);
            List<StringReplacement> replacements = new List<StringReplacement>();
            Diff emptyDiff = new Diff(Operation.EQUAL, String.Empty);
            Diff previousDiff = emptyDiff;
            diffs.Add(emptyDiff);
            int position = 0;
            IDiffsToReplacementStrategy strategy;
            bool justAdded = false;


            foreach(var diff in diffs)
            {
                if (!justAdded)
                {
                    if (previousDiff.operation == Operation.DELETE && diff.operation == Operation.EQUAL)
                    {
                        strategy = deleteEqualStrategy;
                    }
                    else if (previousDiff.operation == Operation.DELETE && diff.operation == Operation.INSERT)
                    {
                        strategy = deleteInsertStrategy;
                    }
                    else if (previousDiff.operation == Operation.INSERT && diff.operation == Operation.EQUAL)
                    {
                        strategy = insertEqualStrategy;
                    }
                    else
                    {
                        strategy = defaultStrategy;
                    }
                    justAdded = strategy.AddReplacementTo(replacements, position, previousDiff, diff);
                    position = strategy.newPosition(position, previousDiff, diff);
                }
                else
                {
                    justAdded = false;
                }
                previousDiff = diff;

            }
            return replacements;
        }
    }

    
}
