using Mono.Cecil;
using MutantCommon;
using MutantGeneration.CodeContexts;
using MutantGeneration.MutationSteps;
using System;

namespace MutantGeneration.Mutations
{
    public class AbstractMutation<CodeType> : IAbstractMutation<CodeType>
    {
        public string Name { get; set; }
        public MutationFamily Family { get; set; }
        public Func<CodeType, bool> CodeFilter { get; set ; }
        public IMutationStep<CodeType> MutationSteps { get; set; }
        public IMutationPurpose Purpose { get; set; }

        public IMutation CreateMutation(CodeType codeSection, IContext context)
        {
            return new Mutation<CodeType>(this, codeSection, context);
        }
    }
}
