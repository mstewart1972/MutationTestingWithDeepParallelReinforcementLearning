using MutantCommon;
using MutantGeneration.MutationSteps;
using System;

namespace MutantGeneration.Mutations
{
    public interface IAbstractMutation<MutatedCode>
    {
        String Name { get; set; }
        MutationFamily Family { get; set; }
        IMutationPurpose Purpose { get; set; }
        Func<MutatedCode, bool> CodeFilter { get; set; }
        IMutationStep<MutatedCode> MutationSteps { get; set; }

        IMutation CreateMutation(MutatedCode codeSection, IContext context);
    }
}
