using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

// MAS 20210118
namespace Filters.MutantFilters
{
    public interface IMutantFilter
    {
        IEnumerable<IMutant> Filter(IEnumerable<IMutant> mutants);
    }
}
