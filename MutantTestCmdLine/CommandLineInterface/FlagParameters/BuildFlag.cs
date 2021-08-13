using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

namespace MutantTesting.CommandLineInterface.FlagParameters
{
    public class BuildFlag : FlagParameter
    {
        public override string Key => "-noBuild";
        public override string Name => "Don't Build Solution";
        public override string Description => "Whether to build the solution before testing";
        public override void SetField(InputParameters parameters, bool value)
        {
            parameters.BuildSolution = !value;
        }
    }
}
