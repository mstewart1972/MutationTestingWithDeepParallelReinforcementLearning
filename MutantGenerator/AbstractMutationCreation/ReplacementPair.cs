using Mono.Cecil.Cil;
using MutantCommon;

namespace MutantGeneration.AbstractMutationCreation
{
    public class ReplacementPair
    {
        public OpCode OldCode { get; set; }
        public OpCode NewCode { get; set; }
        public IMutationPurpose Purpose { get; set; }
    }
}
