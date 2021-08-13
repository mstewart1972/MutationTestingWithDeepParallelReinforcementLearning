namespace MutantCommon
{
    public interface IMutation
    {
        IContext Context { get; }
        void Mutate();
        void Unmutate();
        MutationFamily Family { get; }
        int ID { get; }
        string Name { get; }
        IMutationPurpose Purpose { get;  }
    }
}
