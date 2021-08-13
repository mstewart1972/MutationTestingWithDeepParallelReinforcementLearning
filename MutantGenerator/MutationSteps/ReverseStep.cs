namespace MutantGeneration.MutationSteps
{
    public class ReverseStep<CodeType>:IMutationStep<CodeType>
    {
        private readonly IMutationStep<CodeType> _step;
        public ReverseStep(IMutationStep<CodeType> mutationStep)
        {
            _step = mutationStep;
        }

        public void Mutate(CodeType code)
        {
            _step.UnMutate(code);
        }

        public void UnMutate(CodeType code)
        {
            _step.Mutate(code);
        }
    }
}
