using MutantCommon;
using System;


namespace MutantGeneration.Mutations
{
    public interface IMutationMethod
    {
        String Name { get; set; }
        MutationFamily Family { get; set; }
    }
}
