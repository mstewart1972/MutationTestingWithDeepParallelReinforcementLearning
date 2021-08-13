using Mono.Cecil;
using System.Collections.Generic;

namespace MutantGeneration.CodeProviders
{
    public interface IProvider<ProvidedType>
    {
        IEnumerable<ProvidedType> Provide(ModuleDefinition module);
    }
}
