using System;

namespace MutantCommon
{ 
    [Flags]

// MAS 20210126
    public enum MutationTestingOperation
    {
        None,
        BuildingSolution = 1,
        TestingOriginalCode = 2,
        GeneratingMutants = 4,
        TestingMutants = 8,
        Diffing = 16,
        Complete = 32,
        Error = 64,
        FilteringMutants = 128
    }
}