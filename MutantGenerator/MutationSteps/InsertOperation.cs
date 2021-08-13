using Mono.Cecil.Cil;
using MutantGeneration.CodeContexts;

namespace MutantGeneration.MutationSteps
{
    public class InsertOperation : IMutationStep<InstructionContext>
    {
        private readonly OpCode _newCode;

        public InsertOperation(OpCode newCode)
        {
            _newCode = newCode;
        }

        public void Mutate(InstructionContext code)
        {
            var newInstruction = code.ILProcessor.Create(_newCode);
            code.ILProcessor.InsertAfter(code.Instruction, newInstruction);
        }

        public void UnMutate(InstructionContext code)
        {
            code.ILProcessor.Remove(code.Instruction.Next);
        }
    }
}
