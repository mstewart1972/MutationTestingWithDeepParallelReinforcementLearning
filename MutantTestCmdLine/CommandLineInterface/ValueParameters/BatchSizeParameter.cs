using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

namespace MutantTesting.CommandLineInterface.ValueParameters
{
    public class BatchSizeParameter : ValueParameter
    {
        public override string Key => "-batchSize";

        public override string FormatInstructions => "[positive integer]";

        public override bool IsMandatory => false;

        public override string Name => "Batch Size";

        public override string Description => "The maximum number of tests to run at once";

        public override bool SetField(InputParameters parameters, string value)
        {
            uint batchSize;
            try
            {
                batchSize = Convert.ToUInt32(value);
            }
            catch
            {
                return false;
            }
            parameters.TestBatchSize = batchSize;
            return true;
        }
    }
}
