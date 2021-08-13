using Mono.Cecil;
using MutantCommon;

namespace MutantGeneration.AbstractMutationCreation
{
    public class FieldAttributeReplacementPair
    {
        public FieldAttributes OldAttribute { get; set; }
        public FieldAttributes NewAttribute { get; set; }
        public IMutationPurpose Purpose { get; set; }
    }
}
