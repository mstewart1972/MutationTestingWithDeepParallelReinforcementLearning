using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public class MutantModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }
        public TestResult TestResult { get; set; }
        public Class Class { get; set; }
        public MutationFamily Family { get; set; }
        public IMutationPurpose Purpose { get; set; }
    }
}
