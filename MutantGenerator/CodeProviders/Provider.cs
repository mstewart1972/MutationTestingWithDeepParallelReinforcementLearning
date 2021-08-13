using System.Collections.Generic;
using Mono.Cecil;

namespace MutantGeneration.CodeProviders
{
    public abstract class Provider<ProvidedType> : IProvider<ProvidedType>
    {
        private readonly Dictionary<ModuleDefinition, IEnumerable<ProvidedType>> _results = new Dictionary<ModuleDefinition, IEnumerable<ProvidedType>>();

        public abstract IEnumerable<ProvidedType> GetProvided(ModuleDefinition module);

        public IEnumerable<ProvidedType> Provide(ModuleDefinition module)
        {
            if (!_results.ContainsKey(module))
            {
                _results[module] = GetProvided(module);
            }
            return _results[module];
        }
    }
}
