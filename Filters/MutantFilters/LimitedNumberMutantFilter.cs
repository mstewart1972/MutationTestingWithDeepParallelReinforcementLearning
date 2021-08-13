using System.Collections.Generic;
using System.Linq;
using MutantCommon;

// MAS 20210118
namespace Filters.MutantFilters
{
    public class LimitedNumberMutantFilter : IMutantFilter
    {
        public int Limit { get; private set; }

        public LimitedNumberMutantFilter(int limit)
        {
            Limit = limit;
        }

        public IEnumerable<IMutant> Filter(IEnumerable<IMutant> mutants)
        {
            if (mutants.Count() <= Limit)
            {
                return mutants;
            }
            else
            {
                return mutants.Take(Limit);
            }
        }

    }

}
