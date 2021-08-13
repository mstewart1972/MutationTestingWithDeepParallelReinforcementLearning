namespace MutantGeneration.MutationSteps
{
    public interface IMutationStep<CodeType>
    {
        void Mutate(CodeType code);
        void UnMutate(CodeType code);
    }
}
