using Mono.Cecil;
using System.Collections.Generic;
using MutantCommon;

namespace MutantGeneration.MutationGenerators
{
    public interface IMutationGenerator
    {
        IEnumerable<IMutation> GenerateMutations(ModuleDefinition module);
    }
}
