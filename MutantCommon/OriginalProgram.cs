using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public class OriginalProgram: TestableProgramVersion
    {
        public OriginalProgram(string sourceFilePath)
        {
            SourceFileCopyPath = sourceFilePath;
        }
        public override string Name => "OriginalProgram";

        public override string SourceFileCopyPath { get; }

        public override ISet<Unittest> TestsToRun(IClassTestCoverage coverage)
        {
            return coverage.Coverage(coverage.AllTestedClasses());
        }
    }
}
