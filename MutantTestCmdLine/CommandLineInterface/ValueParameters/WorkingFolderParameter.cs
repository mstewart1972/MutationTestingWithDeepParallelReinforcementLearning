using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

namespace MutantTesting.CommandLineInterface.ValueParameters
{
    public class WorkingFolderParameter : ValueParameter
    {
        public override string Key => "-workingFolder";

        public override string FormatInstructions => "[full path]/[folderName]";

        public override bool IsMandatory => false;

        public override string Name => "Working Folder";

        public override string Description => "The folder to use for building and storing mutants. Must be a complete path";

        public override bool SetField(InputParameters parameters, string value)
        {
            parameters.WorkingDirectory = value;
            return true;
        }
    }
}
