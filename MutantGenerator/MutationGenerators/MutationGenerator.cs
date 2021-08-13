using Mono.Cecil;
using MutantCommon;
using MutantGeneration.CodeContexts;
using MutantGeneration.CodeProviders;
using MutantGeneration.Mutations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MutantGeneration.MutationGenerators
{
    public class MutationGenerator<CodeType> : IMutationGenerator
    {
        private readonly IProvider<CodeType> _codeProvider;
        private readonly IAbstractMutation<CodeType> _abstractMutation;
        private readonly Func<CodeType, IContext> _getCodeContext;
        public MutationGenerator(IProvider<CodeType> provider, IAbstractMutation<CodeType> abstractMutation, Func<CodeType, IContext> getCodeContext)
        {
            _codeProvider = provider;
            _abstractMutation = abstractMutation;
            _getCodeContext = getCodeContext;
        }

        public IEnumerable<IMutation> GenerateMutations(ModuleDefinition module)
        {
            var code = _codeProvider.Provide(module);
            var filteredCode = code.Where(_abstractMutation.CodeFilter);

            var mutations = new List<IMutation>();

            foreach (var codePiece in filteredCode)
            {
                var context = _getCodeContext(codePiece);
                mutations.Add(_abstractMutation.CreateMutation(codePiece, context));
            }

            return mutations;
        }
    }
}
