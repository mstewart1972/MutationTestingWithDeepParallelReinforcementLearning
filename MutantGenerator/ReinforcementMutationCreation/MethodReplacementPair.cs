using MutantCommon;

// MAS 20210118
namespace MutantGeneration.ReinforcementMutationCreation
{
    public class MethodReplacementPair
    {
        public string newMethodName { get; set; }
        public string oldMethodName { get; set; }
        public IMutationPurpose Purpose { get; set; }
    }
}
