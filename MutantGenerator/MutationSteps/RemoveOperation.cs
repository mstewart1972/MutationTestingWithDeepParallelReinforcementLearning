using Mono.Cecil.Cil;
using MutantGeneration.CodeContexts;

namespace MutantGeneration.MutationSteps
{
    public class RemoveOperation:IMutationStep<InstructionContext>
    {
        private readonly InsertOperation insertOperation;
        public RemoveOperation(OpCode removedCode)
        {
            insertOperation = new InsertOperation(removedCode);
        }

        public void Mutate(InstructionContext code)
        {
            insertOperation.UnMutate(code);
        }

        public void UnMutate(InstructionContext code)
        {
            insertOperation.Mutate(code);
        }
    }
}
