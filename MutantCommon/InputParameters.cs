using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public class InputParameters
    {
        public String SolutionPath { get; set; }
        public String SourceFileName { get; set; }
        public String TestFileName { get; set; }
        //public String Testadaptor { get; set; }
        public bool BuildSolution { get; set; }
        public String ReportFolder { get; set; }
        public String WorkingDirectory { get; set; }
        //public List<String> MutantOperators { get; set; }
        public bool Verbose { get; set; }
        public bool ShouldRun { get; set; }
        public uint TestBatchSize { get; set; }
        public int[] Operators { get; set; }
        public String Category { get; set; }
        public uint AgentNumber { get; set; }
    }
}
