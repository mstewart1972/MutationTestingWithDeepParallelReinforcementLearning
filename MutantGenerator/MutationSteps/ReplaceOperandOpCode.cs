using Mono.Cecil.Cil;
using MutantGeneration.CodeContexts;

namespace MutantGeneration.MutationSteps
{
    public class ReplaceOperandOpCode : IMutationStep<InstructionContext>
    {
        private readonly OpCode newOperandOpCode;
        private readonly OpCode oldOperandOpCode;

        public ReplaceOperandOpCode(OpCode newCode, OpCode oldCode)
        {
            newOperandOpCode = newCode;
            oldOperandOpCode = oldCode;
        }

        public void Mutate(InstructionContext code)
        {
            var operand = ((Instruction)code.Instruction.Operand);
            operand.OpCode = newOperandOpCode;
        }

        public void UnMutate(InstructionContext code)
        {
            var operand = ((Instruction)code.Instruction.Operand);
            operand.OpCode = oldOperandOpCode;
        }
    }
}
