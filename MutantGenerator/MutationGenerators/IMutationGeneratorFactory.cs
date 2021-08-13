using MutantGeneration.Mutations;

namespace MutantGeneration.MutationGenerators
{
    interface IMutationGeneratorFactory<CodeType>
    {
        IMutationGenerator Construct(IAbstractMutation<CodeType> abstractMutation);
    }
}
