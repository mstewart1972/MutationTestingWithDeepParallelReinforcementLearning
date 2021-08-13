using MutantCommon;
using MutantGeneration.CodeContexts;
using MutantGeneration.CodeProviders;
using MutantGeneration.Mutations;
using System;

namespace MutantGeneration.MutationGenerators
{
    public class FieldMutationGeneratorFactory:IMutationGeneratorFactory<FieldContext>
    {
        private readonly IProvider<FieldContext> _fieldProvider;
        private readonly Func<FieldContext, IContext> _getContext = r => r;
        public const string F_ANONYMOUS_TYPE = "f__AnonymousType";
        public const string MODULE_KEY_WORD = "< Module >";

        public FieldMutationGeneratorFactory()
        {
            _fieldProvider = new FieldProvider(type => type.Name != MODULE_KEY_WORD && !type.FullName.Contains(F_ANONYMOUS_TYPE));
        }

        public IMutationGenerator Construct(IAbstractMutation<FieldContext> abstractMutation)
        {
            return new MutationGenerator<FieldContext>(_fieldProvider, abstractMutation, _getContext);
        }
    }
}
