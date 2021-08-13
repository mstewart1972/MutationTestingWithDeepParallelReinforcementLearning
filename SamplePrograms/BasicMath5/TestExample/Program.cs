using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace TestExample
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicMathFunctions x = new BasicMathFunctions();
            x.FirstNumber = 2;
            x.SecondNumber = 2;
            Console.WriteLine(x.Add(2, 2).ToString());
        }
    }
}
