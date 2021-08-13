using MutantCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.PathProviders;

namespace TestComponents
{
    public class DotnetTestFramework : ITestFramework
    {
        private readonly ITestRunner runner;
        private readonly IPathProvider _paths;
        private readonly ITestCoverageCalculator coverageCalculator;
        private readonly IActiveProgramEnvironment environment;

        public DotnetTestFramework(IPathProvider paths,ITestRunner testRunner, ITestCoverageCalculator coverageCalculator, IActiveProgramEnvironment activeEnvironment)
        {
            runner = testRunner;
            _paths = paths;
            this.coverageCalculator = coverageCalculator;
            environment = activeEnvironment;

        }

        public IClassTestCoverage GetCoverage()
        {
            return coverageCalculator.ClassTestCoverage();
        }

        public ISet<Class> GetTestedClasses()
        {
            return coverageCalculator.ClassTestCoverage().AllTestedClasses();
        }

        public ISet<Unittest> GetUnittests()
        {
            return coverageCalculator.ClassTestCoverage().Unittests();
        }

        public TestResult Test(IProgramVersion program)
        {
            environment.Load(program);
            string resultFilePath = _paths.GetMutantTestResultFilepath(program.Name);
            string testFilePath =_paths.TestFilepath;
            return runner.TestSolution(testFilePath, resultFilePath, program.TestsToRun(coverageCalculator.ClassTestCoverage()));
        }

        public async Task<TestResult> TestAsync(IProgramVersion program)
        {
            try {
                environment.Load(program);
            }
            catch(Exception ex)
            {
                foreach (var process in Process.GetProcessesByName("dotnet"))
                {
                    try
                    {
                        process.Kill();
                        process.WaitForExit();
                    }
                    catch(InvalidOperationException e)
                    {
                        //Process is already dead
                    }
                    catch (NotSupportedException e)
                    {
                        //Is remote, killing is not necessary
                    }
                    catch (Exception e)
                    {
                        //process already closing
                        process.WaitForExit();
                    }
                }
                environment.Load(program);
            }
            string resultFilePath = _paths.GetMutantTestResultFilepath(program.Name);
            string testFilePath = _paths.TestFilepath;
            return await runner.TestSolutionAsync(testFilePath, resultFilePath, program.TestsToRun(coverageCalculator.ClassTestCoverage())).ConfigureAwait(false);
            }
    }
}
