using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public interface IProgramVersion
    {
        string SourceFileCopyPath { get; }
        string Name { get; }
        ISet<Unittest> TestsToRun(IClassTestCoverage coverage);
    }
}
