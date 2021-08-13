using Mono.Cecil;
using MutantGeneration.AbstractMutationCreation;
using MutantGeneration.ReinforcementMutationCreation;
using MutantGeneration.MutationGenerators;
using System.Collections.Generic;
using System.Linq;
using MutantCommon;
using Utility.PathProviders;

namespace MutantGeneration.MutantGenerators
{
    public class MutantGenerator:IMutantGenerator
    {
        private readonly IPathProvider paths;

        public MutantGenerator(IPathProvider paths)
        {
            this.paths = paths;
        }

        public List<IMutant> Generate(string category, int[] operators, ISet<Class> testedClasses)
        {
            // Static list of all mutation operators
            //var abstractInstructionMutations = AbstractMutationFinder.GetAllAbstractInstructionMutations();
            //var abstractFieldMutations = AbstractMutationFinder.GetAllAbstractFieldMutations();

            // MAS 20210118
            // Dynamically filter Mutation Operators based on Reinforcement Learning
            // This does limit mutant generation and testing, so may be ideal?
            var abstractInstructionMutations = ReinforcementMutationFinder.GetAllReinforcementInstructionMutations(category, operators);
            //var abstractFieldMutations = ReinforcementMutationFinder.GetAllReinforcementFieldMutations();
            //

            List<IMutation> mutations;
            List<IMutant> mutants;
            using (var _module = ModuleDefinition.ReadModule(paths.CopiedSourceFilepath))
            {
                var instructionGenerators = new InstructionMutationGenerators(testedClasses, abstractInstructionMutations);
                //var fieldGenerators = new FieldMutationGenerators(abstractFieldMutations);
                //var instructionMutations = instructionGenerators.GenerateMutations(_module);
                //var fieldMutations = fieldGenerators.GenerateMutations(_module);
                //mutations = instructionMutations.Concat(fieldMutations).ToList();

                // MAS 20200215 - limit testing to instruction mutations
                mutations = instructionGenerators.GenerateMutations(_module).ToList();

                mutants = SaveMutants(mutations);

            }
            return mutants;
        }

        private List<IMutant> SaveMutants(IEnumerable<IMutation> mutations)
        {
            var mutants = new List<IMutant>();
            foreach (var mutation in mutations)
            {
                mutants.Add(SaveMutant(mutation));
            }
            return mutants;
        }

        private IMutant SaveMutant(IMutation mutation)
        {
            var filename = paths.GetMutantFilepath(mutation.Name);
            IMutant newMutant = new Mutant (mutation, filename);            
            mutation.Mutate();
            mutation.Context.Module.Write(newMutant.Filename);
            mutation.Unmutate();
            return newMutant;
        }
    }
}
