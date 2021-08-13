using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

namespace MutantTesting.CommandLineInterface.ValueParameters
{
    public class OperatorParameter : ValueParameter
    {
        public override string Key => "-operators";

        public override string FormatInstructions => "[list of integers] (operators 1=selected, 0=deselected)";

        public override bool IsMandatory => false;

        public override string Name => "Operators";

        public override string Description => "Mutation operators to utilize: 1=addToMul,2=addToSub,3=addToDiv,4=addToRem";

        public override bool SetField(InputParameters parameters, string value)
        {
            try
            {
                char[] chars = value.ToCharArray();
                parameters.Operators = Array.ConvertAll(chars, s => int.Parse(s.ToString()));
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
