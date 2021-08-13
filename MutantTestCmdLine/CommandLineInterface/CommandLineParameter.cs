using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantTesting.CommandLineInterface
{
    public abstract class CommandLineParameter
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
    }
}
