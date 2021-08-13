using System;

namespace MutantCommon
{
    public class TestResult
    {
        public Boolean Survived { get; set; }
        public int TestsRan { get; set; }
        public int Passes { get; set; }
        public int Fails { get; set; }
    }
}
