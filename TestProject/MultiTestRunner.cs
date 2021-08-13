using ExternalProgramExecution;
using MutantCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utility.PathProviders;

namespace MutantTestRunner
{
    public class MultiTestRunner : TestRunner
    {
        private const string FAILED = "failed";
        private const string PASSED = "passed";
        private const string NUMBER_OF_TESTS = "total";
        private const string TEST_RUN = "TestRun";
        private const string RESULTS_SUMMARY = "{http://microsoft.com/schemas/VisualStudio/TeamTest/2010}ResultSummary";
        private const string COUNTERS = "{http://microsoft.com/schemas/VisualStudio/TeamTest/2010}Counters";
        private readonly IPathProvider _paths;
        private readonly bool _isVerbose;
        public MultiTestRunner(IPathProvider paths, bool isVerbose=false)
        {
            _paths = paths;
            _isVerbose = isVerbose;
        }

        public override TestResult ProcessResultFile(string fileName)
        {
            var directoryName = Path.GetDirectoryName(fileName);
            var filesInDirectory = Directory.GetFiles(directoryName);
            XElement xmlTree;
            IEnumerable<XElement> resultSummaries;
            IEnumerable<XElement> resultCounters;

            int totalTests = 0;
            int totalPasses = 0;
            int totalFails = 0;
            int tests;
            int passes;
            int fails;

            


            foreach(string file in filesInDirectory)
            {
                if(String.Compare(Path.GetExtension(file), ".trx") != 0)
                {
                    continue;//Skip non trx files
                }
                try
                {
                    xmlTree = XElement.Load(file);
                }
                catch (Exception)
                {
                    continue;//Ignore missing or badly formatted files
                }
                var elems = xmlTree.Elements();
                resultSummaries = xmlTree.Elements(RESULTS_SUMMARY);
                resultCounters = resultSummaries.Elements(COUNTERS);
                foreach(var counter in resultCounters)
                {
                    if(int.TryParse(counter.Attribute(NUMBER_OF_TESTS).Value, out tests))
                    {
                        totalTests += tests;
                    }
                    if (int.TryParse(counter.Attribute(PASSED).Value, out passes))
                    {
                        totalPasses += passes;
                    }
                    if (int.TryParse(counter.Attribute(FAILED).Value, out fails))
                    {
                        totalFails += fails;
                    }
                }
            }
            return new TestResult {
                Survived =(totalFails == 0 && totalPasses > 0),
                TestsRan = totalTests,
                Passes = totalPasses,
                Fails = totalFails
            };
        }

        public override bool RunExternalTestToolForSolution(string inputFile, string outputFile)
        {
            var outputDirectory = Path.GetDirectoryName(outputFile);
            DotnetTester tester = new DotnetTester(_paths, outputDirectory);
            try
            {
                tester.Execute(_isVerbose);
            }
            catch(ProcessException)
            {
                return false;
            }
            return true;
        }

        public override async Task<bool> RunExternalTestToolForSolutionAsync(string inputFile, string outputFile)
        {
            var outputDirectory = Path.GetDirectoryName(outputFile);
            DotnetTester tester = new DotnetTester(_paths, outputDirectory);
            try
            {
                await tester.ExecuteAsync();
            }
            catch (ProcessException)
            {
                return false;
            }
            return true;
        }
    }
}
