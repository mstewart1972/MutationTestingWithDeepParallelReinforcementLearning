using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

namespace MutantTesting.CommandLineInterface.ValueParameters
{
    public class ReportFolderParameter : ValueParameter
    {
        public override string Key => "-reportFolder";

        public override string FormatInstructions => "[path]/[folderName]";

        public override bool IsMandatory => false;

        public override string Name => "Report Folder";

        public override string Description => "The folder to write reports to";

        public override bool SetField(InputParameters parameters, string value)
        {
            parameters.ReportFolder = value;
            return true;
        }
    }
}
