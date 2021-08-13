using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantTesting.CommandLineInterface
{
    public abstract class FlagParameter:CommandLineParameter
    {
        public abstract string Key { get; }
        public abstract void SetField(InputParameters parameters, bool value); 
        public void UpdateParametersIfContainsFlag(InputParameters parameters, HashSet<string> flags)
        {
            if (flags.Contains(Key))
            {
                SetField(parameters, true);
            }
        }
    }
}
