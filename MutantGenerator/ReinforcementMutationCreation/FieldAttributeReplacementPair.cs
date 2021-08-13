using Mono.Cecil;
using MutantCommon;

// MAS 20210118
namespace MutantGeneration.ReinforcementMutationCreation
{
    public class FieldAttributeReplacementPair
    {
        public FieldAttributes OldAttribute { get; set; }
        public FieldAttributes NewAttribute { get; set; }
        public IMutationPurpose Purpose { get; set; }
    }
}
