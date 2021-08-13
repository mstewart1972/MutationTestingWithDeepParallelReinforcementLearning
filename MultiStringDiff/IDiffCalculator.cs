using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStringDiff
{
    public interface IDiffCalculator
    {
        Dictionary<Class, IList<StringSectionModel>> Calculate(OriginalProgram originalProgram, IEnumerable<IMutant> mutants);
    }
}
