using Mono.Cecil.Cil;
using MutantGeneration.CodeContexts;

namespace MutantGeneration.MutationSteps
{
    public class InsertFollowingNot: IMutationStep<InstructionContext>
    {
        private static IMutationStep<InstructionContext> insertFollowingLoad0 = new InsertOperation(OpCodes.Ldc_I4_0);
        private static IMutationStep<InstructionContext> insertFollowingCompareEqual = new InsertOperation(OpCodes.Ceq);

        public InsertFollowingNot()
        {

        }

        public void Mutate(InstructionContext code)
        {
            insertFollowingCompareEqual.Mutate(code);
            insertFollowingLoad0.Mutate(code);
        }

        public void UnMutate(InstructionContext code)
        {
            insertFollowingLoad0.UnMutate(code);
            insertFollowingCompareEqual.UnMutate(code);
        }
    }
}
