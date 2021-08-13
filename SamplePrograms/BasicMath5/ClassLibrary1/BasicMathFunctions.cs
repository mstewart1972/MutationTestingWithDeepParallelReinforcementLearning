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

        public int Sub(int FirstNumber, int SecondNumber)
        {
            return FirstNumber - SecondNumber;
        }

        public int Mul(int FirstNumber, int SecondNumber)
        {
            return FirstNumber * SecondNumber;
        }

        public int Div(int FirstNumber, int SecondNumber)
        {
            return FirstNumber / SecondNumber;
        }

        public int Mod(int FirstNumber, int SecondNumber)
        {
            return FirstNumber % SecondNumber;
        }

    }
}
