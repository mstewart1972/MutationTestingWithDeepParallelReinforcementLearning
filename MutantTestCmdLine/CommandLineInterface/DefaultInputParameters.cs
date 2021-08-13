using MutantCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantTesting.CommandLineInterface
{
    public class DefaultInputParameters:InputParameters
    {
        public DefaultInputParameters()
        {
            BuildSolution = true;
            //MutantOperators = new List<string> { ConstantsDeclaration.AOR_MUTANT, ConstantsDeclaration.LOR_MUTANT, ConstantsDeclaration.ROR_MUTANT, ConstantsDeclaration.AMC_MUTANT };
            Verbose = false;
            WorkingDirectory = Directory.GetCurrentDirectory();
            //ReportFolder = Path.Combine(WorkingDirectory, ConstantsDeclaration.REPORT_KEY_WORD);
            ReportFolder = "";
            ShouldRun = true;
            TestFileName = "";
            TestBatchSize = 100;


            //Unset Defaults
            //
            //Testadaptor = ?;
            //TestFileName =?;
            //SolutionPath = ?;
            //SourceFileName = ?;

        }
    }
}
