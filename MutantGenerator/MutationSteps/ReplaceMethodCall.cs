using Mono.Cecil;
using MutantGeneration.CodeContexts;

namespace MutantGeneration.MutationSteps
{
    public class ReplaceMethodCall:IMutationStep<InstructionContext>
    {
        private readonly string _oldMethodName;
        private readonly string _newMethodName;

        public ReplaceMethodCall(string newMethodName, string oldMethodName)
        {
            _oldMethodName = oldMethodName;
            _newMethodName = newMethodName;
        }

        public void Mutate(InstructionContext code)
        {
            MethodReference methodCall = code.Instruction.Operand as MethodReference;
            methodCall.Name = _newMethodName;
        }

        public void UnMutate(InstructionContext code)
        {
            MethodReference methodCall = code.Instruction.Operand as MethodReference;
            methodCall.Name = _oldMethodName;
        }
    }
}
