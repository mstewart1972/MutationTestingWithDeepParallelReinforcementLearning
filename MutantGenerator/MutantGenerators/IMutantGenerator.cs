using MutantCommon;
using System.Collections.Generic;

namespace MutantGeneration.MutantGenerators
{
    public interface IMutantGenerator
    {
        // MAS 20210130
        //List<IMutant> Generate(ISet<Class> testedClasses);
        List<IMutant> Generate(string category, int[] operators, ISet<Class> testedClasses);
    }
}
