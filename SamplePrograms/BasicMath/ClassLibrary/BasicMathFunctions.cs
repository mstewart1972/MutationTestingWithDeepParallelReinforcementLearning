using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
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
