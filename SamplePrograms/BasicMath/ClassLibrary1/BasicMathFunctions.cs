using System;

namespace ClassLibrary1
{
    public class BasicMathFunctions
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }

        public int Add(int FirstNumber, int SecondNumber)
        {
            return FirstNumber + SecondNumber;
        }

    }
}
