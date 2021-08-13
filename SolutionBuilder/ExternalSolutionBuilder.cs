using ExternalProgramExecution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionBuilder
{
    public abstract class ExternalSolutionBuilder : ExternalProgramExecutor, ISolutionBuilder
    {
        public override void OnTimeOut(string program, string arguments, int timeout)
        {
            throw new BuildException($"Building solution timed out after {timeout} miliseconds");
        }
        public override void OnUnsuccessful(string program, string arguments)
        {
            throw new BuildException($"Unsuccessful build using: {program} {arguments}");
        }

        public void Build(bool showOutput)
        {
            Execute(showOutput);
            CopyBuiltSolution();
        }

        public abstract void CopyBuiltSolution();


        public async Task BuildAsync(bool showOutput)
        {
            await ExecuteAsync(showOutput).ConfigureAwait(false);
        }
    }
}
