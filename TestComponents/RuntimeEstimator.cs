using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestComponents
{
    public class RuntimeEstimator
    {
        public RuntimeEstimation Estimate(IClassTestCoverage coverage, IEnumerable<IMutant> mutants, TimeSpan unittestTime)
        {
            var averageUnittestTime = unittestTime.TotalSeconds / coverage.Unittests().Count;
            var numberOfTestsToRun = 0;
            foreach(var mutant in mutants)
            {
                numberOfTestsToRun += coverage.Coverage(mutant.MutatedClass).Count;
            }
            var estimatedNumberOfSeconds = numberOfTestsToRun * averageUnittestTime;
            return new RuntimeEstimation { TimeSpan= TimeSpan.FromSeconds(estimatedNumberOfSeconds) };
        }
    }
}
