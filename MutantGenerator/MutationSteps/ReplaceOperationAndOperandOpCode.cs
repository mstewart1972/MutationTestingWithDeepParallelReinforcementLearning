using Mono.Cecil.Cil;
using MutantGeneration.CodeContexts;

namespace MutantGeneration.MutationSteps
{
    class ReplaceOperationAndOperandOpCode : IMutationStep<InstructionContext>
    {
        private readonly ReplaceOperandOpCode replaceOperand;
        private readonly ReplaceOperationOpCode replaceOperation;

        public ReplaceOperationAndOperandOpCode(OpCode newOperationOpCode, OpCode oldOperationOpCode, OpCode newOperandOpCode, OpCode oldOperandOpCode)
        {
            replaceOperation = new ReplaceOperationOpCode(newOperationOpCode, oldOperationOpCode);
            replaceOperand = new ReplaceOperandOpCode(newOperandOpCode, oldOperandOpCode);
        }

        public void Mutate(InstructionContext code)
        {
            replaceOperation.Mutate(code);
            replaceOperand.Mutate(code);
        }

        public void UnMutate(InstructionContext code)
        {
            replaceOperation.UnMutate(code);
            replaceOperand.UnMutate(code);
        }
    }
}
