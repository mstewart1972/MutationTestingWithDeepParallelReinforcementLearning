using MutantCommon;
using MutantGeneration.CodeContexts;
using MutantGeneration.CodeProviders;
using MutantGeneration.Mutations;
using System;
using System.Collections.Generic;

namespace MutantGeneration.MutationGenerators
{
    class InstructionMutationGeneratorFactory:IMutationGeneratorFactory<InstructionContext>
    {
        private readonly ISet<Class> _testedClasses;
        private readonly IProvider<InstructionContext> _instructionProvider;
        private readonly Func<InstructionContext, IContext> _getContext = r => r;

        public InstructionMutationGeneratorFactory(ISet<Class> testedClasses)
        {
            _testedClasses = testedClasses;

            _instructionProvider = new InstructionProvider(type => _testedClasses.Contains(new Class { Name = type.FullName }), method => !method.IsConstructor && method.HasBody);
        }

        public IMutationGenerator Construct(IAbstractMutation<InstructionContext> abstractMutation)
        {
            return new MutationGenerator<InstructionContext>(_instructionProvider, abstractMutation, _getContext);
        }
    }
}
