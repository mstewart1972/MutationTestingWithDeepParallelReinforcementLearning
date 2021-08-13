using MutantCommon;
using MutantTesting.CommandLineInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantTesting
{
    public class MutationTesterFactory
    {
        private readonly InputParameters input;

        public MutationTesterFactory(InputParameters input)
        {
            this.input = input;
        }

        public async Task<MutationTester> InitiliseMutationTester()
        {
            var tester = new MutationTester(input);
            await tester.InitialiseAsync().ConfigureAwait(false);
            return tester;
        }
    }
}
