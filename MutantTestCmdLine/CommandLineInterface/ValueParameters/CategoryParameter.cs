using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

namespace MutantTesting.CommandLineInterface.ValueParameters
{
    public class CategoryParameter : ValueParameter
    {
        public override string Key => "-category";

        public override string FormatInstructions => "[string]";

        public override bool IsMandatory => false;

        public override string Name => "Category";

        public override string Description => "Mutation operator category to utilize: A=all, B=basic";

        public override bool SetField(InputParameters parameters, string value)
        {
            try
            {
                parameters.Category = value;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
