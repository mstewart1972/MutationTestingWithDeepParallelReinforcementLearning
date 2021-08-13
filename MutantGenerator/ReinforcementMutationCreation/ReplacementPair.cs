using Mono.Cecil.Cil;
using MutantCommon;

// MAS 20210118
namespace MutantGeneration.ReinforcementMutationCreation
{
    public class ReplacementPair
    {
        public OpCode OldCode { get; set; }
        public OpCode NewCode { get; set; }
        public IMutationPurpose Purpose { get; set; }
    }
}
