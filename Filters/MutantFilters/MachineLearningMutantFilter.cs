using System.Collections.Generic;
using System.Linq;
using MutantCommon;

// MAS 20210125
namespace Filters.MutantFilters
{
    public class MachineLearningMutantFilter : IMutantFilter
    {
        public int Limit { get; private set; }

        public MachineLearningMutantFilter(int limit)
        {
            Limit = limit;
        }

        public IEnumerable<IMutant> Filter(IEnumerable<IMutant> mutants)
        {
            return mutants;
        }

    }

}
