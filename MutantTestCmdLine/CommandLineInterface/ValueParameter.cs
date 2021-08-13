using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantTesting.CommandLineInterface
{
    public abstract class ValueParameter:CommandLineParameter
    {
        public abstract string Key { get; }
        public abstract bool SetField(InputParameters parameters, string value);
        public bool UpdateWithValue(InputParameters parameters, Dictionary<string,string> values)
        {
            if (values.ContainsKey(Key))
            {
                return SetField(parameters, values[Key]);
            }
            else
            {
                //Not Included
                return false;
            }
        }
        public abstract string FormatInstructions { get; }
        public abstract bool IsMandatory { get; }
    }
}
