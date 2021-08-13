using MutantCommon;
using MutantGeneration.ID;
using MutantGeneration.MutationSteps;

namespace MutantGeneration.Mutations
{
    public class Mutation<MutatedObjectType> : IMutation
    {
        private readonly IIdGenerator ids = IdGenerator.Get();
        public IMutationStep<MutatedObjectType> Steps { get; }
        public MutatedObjectType MutatedObject { get; }
        public IAbstractMutation<MutatedObjectType> AbstractMutation { get; }
        public IContext Context { get; }
        public MutationFamily Family { get { return AbstractMutation.Family; } }
        public IMutationPurpose Purpose { get { return AbstractMutation.Purpose; } }
        public int ID { get; }
        public string Name { get { return Family.Name + "_" + Context.Type.FullName + "_" + ID.ToString(); } }

        public Mutation(IAbstractMutation<MutatedObjectType> abstractMutation,
            MutatedObjectType mutatedObject,
            IContext context
            )
        {
            ID = ids.GenerateID();
            Steps = abstractMutation.MutationSteps;
            MutatedObject = mutatedObject;
            AbstractMutation = abstractMutation;
            Context = context;
        }

        public void Mutate()
        {
            Steps.Mutate(MutatedObject);
        }

        public void Unmutate()
        {
            Steps.UnMutate(MutatedObject);
        }
    }
}
