using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

namespace MutantTesting.CommandLineInterface.FlagParameters
{
    public class VerboseFlag : FlagParameter
    {
        public override string Key => "-verbose";
        public override string Name => "Verbose";
        public override string Description => "Whether to pass on the builder and tester output";
        public override void SetField(InputParameters parameters, bool value)
        {
            parameters.Verbose = value;
        }
    }
}
