using Mono.Cecil;
using MutantCommon;
using MutantGeneration.CodeContexts;
using MutantGeneration.Mutations;
using System.Collections.Generic;

namespace MutantGeneration.MutationGenerators
{
    public class InstructionMutationGenerators:IMutationGenerator
    {
        private readonly IList<IMutationGenerator> generators = new List<IMutationGenerator>();
        public InstructionMutationGenerators(ISet<Class> testedClasses, IEnumerable<IAbstractMutation<InstructionContext>> abstractMutations)
        {
            var generatorFactory = new InstructionMutationGeneratorFactory(testedClasses);

            foreach(var abstractMutation in abstractMutations)
            {
                generators.Add(generatorFactory.Construct(abstractMutation));
            }

        }

        public IEnumerable<IMutation> GenerateMutations(ModuleDefinition module)
        {
            var mutants = new List<IMutation>();
            foreach(var generator in generators)
            {
                mutants.AddRange(generator.GenerateMutations(module));
            }
            return mutants;
        }
    }
}
