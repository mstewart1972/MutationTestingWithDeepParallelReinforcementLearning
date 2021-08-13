using Mono.Cecil;
using MutantCommon;
using MutantGeneration.CodeContexts;
using MutantGeneration.Mutations;
using System.Collections.Generic;

namespace MutantGeneration.MutationGenerators
{
    public class FieldMutationGenerators : IMutationGenerator
    {
        private readonly IList<IMutationGenerator> generators = new List<IMutationGenerator>();
        public FieldMutationGenerators(IEnumerable<IAbstractMutation<FieldContext>> abstractMutations)
        {
            var generatorFactory = new FieldMutationGeneratorFactory();
            foreach (var abstractMutation in abstractMutations)
            {
                generators.Add(generatorFactory.Construct(abstractMutation));
            }
        }
        public IEnumerable<IMutation> GenerateMutations(ModuleDefinition module)
        {
            var mutants = new List<IMutation>();
            foreach (var generator in generators)
            {
                mutants.AddRange(generator.GenerateMutations(module));
            }
            return mutants;
        }
    }
}
