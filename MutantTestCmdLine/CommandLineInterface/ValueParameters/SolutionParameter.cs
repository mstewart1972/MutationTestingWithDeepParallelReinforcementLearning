using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

namespace MutantTesting.CommandLineInterface.ValueParameters
{
    public class SolutionParameter : ValueParameter
    {
        public override string Key => "-solution";
        public override string Name => "Solution";
        public override string Description => "The path to the solution to mutation test";

        public override string FormatInstructions => "[path]/[solution_name].sln";

        public override bool IsMandatory => true;

        public override bool SetField(InputParameters parameters, string value)
        {
            if (!value.Contains(ConstantsDeclaration.SOLUTION_EXTENSION))
            {
                return false;
            }
            parameters.SolutionPath = value;
            return true;
        }
    }
}
