using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public class MutationTestingStateModel
    {
        public IEnumerable<Class> UnittestedClasses { get; set; }
        public IEnumerable<MutantModel> Mutants { get; set; }
        public Dictionary<Class, IList<StringSectionModel>> Diffs { get; set; }
        public int MutantsTested { get; set; }
        public int TotalMutants { get; set; }
        public int KilledMutants { get; set; }
        public int EquivalentMutants { get; set; }
        public int TestCaseCount { get; set; }
        public double MutationScore { get; set; }
        public MutationTestingOperation CurrentOperation { get; set; }
        public IEnumerable<Class> MutationTestedClasses { get; set; }
        public bool OriginalCodePassesAllTests { get; set; }
        public double PercentComplete { get; set; }
        public bool BuildComplete { get; set; }
        public bool MutantGenerationComplete { get; set; }
        public bool OriginalCodeTestingComplete { get; set; }
        public bool MutationTestingCompleted { get; set; }
        public bool DiffingCompleted { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
