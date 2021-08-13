using MutantCommon;

namespace MutantGeneration.AbstractMutationCreation
{
    public class MethodReplacementPair
    {
        public string newMethodName { get; set; }
        public string oldMethodName { get; set; }
        public IMutationPurpose Purpose { get; set; }
    }
}
