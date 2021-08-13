using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

namespace MutantTesting.CommandLineInterface.ValueParameters
{
    public class SourceFileParameter : ValueParameter
    {
        public override string Key => "-source";

        public override string FormatInstructions => "[path]/[solution_name].dll";

        public override bool IsMandatory => true;

        public override string Name => "Source File";

        public override string Description => "Path to the dll to mutate";

        public override bool SetField(InputParameters parameters, string value)
        {
            if (!value.Contains(ConstantsDeclaration.DLL_EXTENSION_KEY_WORD))
            {
                return false;
            }
            parameters.SourceFileName = value;
            return true;
        }
    }
}
