using Mono.Cecil;
using MutantGeneration.CodeContexts;

namespace MutantGeneration.MutationSteps
{
    public class ReplaceFieldAttribute : IMutationStep<FieldContext>
    {
        private readonly FieldAttributes _newAttributes;
        private readonly FieldAttributes _oldAttributes;

        public ReplaceFieldAttribute(FieldAttributes newAttributes, FieldAttributes oldAttributes)
        {
            _newAttributes = newAttributes;
            _oldAttributes = oldAttributes;
        }

        public void Mutate(FieldContext code)
        {
            code.Field.Attributes &= ~_oldAttributes;
            code.Field.Attributes |= _newAttributes;
        }

        public void UnMutate(FieldContext code)
        {
            code.Field.Attributes &= ~_newAttributes;
            code.Field.Attributes |= _oldAttributes;
        }
    }
}
