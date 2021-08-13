using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

namespace MutantTesting.CommandLineInterface.ValueParameters
{
    public class AgentNumberParameter : ValueParameter
    {
        public override string Key => "-agentNumber";

        public override string FormatInstructions => "[positive integer]";

        public override bool IsMandatory => false;

        public override string Name => "Agent Number";

        public override string Description => "The agent number for running concurrently";

        public override bool SetField(InputParameters parameters, string value)
        {
            uint agentNumber;
            try
            {
                agentNumber = Convert.ToUInt32(value);
            }
            catch
            {
                return false;
            }
            parameters.AgentNumber = agentNumber;
            return true;
        }
    }
}
