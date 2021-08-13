using Mono.Cecil.Cil;
using MutantGeneration.CodeContexts;

namespace MutantGeneration.MutationSteps
{
    public class ReplaceOperationOpCode:IMutationStep<InstructionContext>
    {
        private readonly OpCode _oldCode;
        private readonly OpCode _newCode;

        public ReplaceOperationOpCode(OpCode newOpCode, OpCode oldOpCode)
        {

            _oldCode = oldOpCode;
            _newCode = newOpCode;
        }

        public void Mutate(InstructionContext code)
        {
            code.Instruction.OpCode = _newCode;
        }

        public void UnMutate(InstructionContext code)
        {
            code.Instruction.OpCode = _oldCode;
        }
    }
}
