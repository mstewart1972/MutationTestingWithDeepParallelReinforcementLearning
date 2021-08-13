using Moq;
using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestComponents;
using Utility.PathProviders;
using Xunit;

namespace Test.TestComponents
{
    public class TestDotnetTestFramework
    {
        private const string testFilepath = "TESTFILEPATH";
        private const string mutantTestResultFilepath = "MUTANTRESULTS";

        [Fact]
        public void TestGetTestedClassesReturnsClasssesGeneratedByCoverageCalculator()
        {
            var paths = new Mock<IPathProvider>();
            var runner = new Mock<TestRunner>();
            var coverageCalculator = new Mock<ITestCoverageCalculator>();
            var environment = new Mock<IActiveProgramEnvironment>();
            var coverage = new Mock<IClassTestCoverage>();
            var fakeClasses = new HashSet<Class>{ new Class(), new Class()};
            var fakeTests = new HashSet<Unittest> { new Unittest(), new Unittest()};



            coverageCalculator.Setup(r => r.ClassTestCoverage()).Returns(coverage.Object);
            coverage.Setup(r => r.AllTestedClasses()).Returns(fakeClasses);
            coverage.Setup(r => r.Unittests()).Returns(fakeTests);

            var framework = new DotnetTestFramework(paths.Object, runner.Object, coverageCalculator.Object, environment.Object);

            var result = framework.GetTestedClasses();

            Assert.Equal(fakeClasses, result);

        }

        [Fact]
        public void TestGetUnittestsReturnsUnittestsGeneratedByCoverageCalculator()
        {
            var paths = new Mock<IPathProvider>();
            var runner = new Mock<TestRunner>();
            var coverageCalculator = new Mock<ITestCoverageCalculator>();
            var environment = new Mock<IActiveProgramEnvironment>();
            var coverage = new Mock<IClassTestCoverage>();
            var fakeClasses = new HashSet<Class> { new Class(), new Class() };
            var fakeTests = new HashSet<Unittest> { new Unittest(), new Unittest() };



            coverageCalculator.Setup(r => r.ClassTestCoverage()).Returns(coverage.Object);
            coverage.Setup(r => r.AllTestedClasses()).Returns(fakeClasses);
            coverage.Setup(r => r.Unittests()).Returns(fakeTests);

            var framework = new DotnetTestFramework(paths.Object, runner.Object, coverageCalculator.Object, environment.Object);

            Assert.Equal(fakeTests, framework.GetUnittests());
        }


        [Fact]
        public void TestTestLoadsNewEnvironment()
        {
            var paths = new Mock<IPathProvider>();
            var runner = new Mock<ITestRunner>();
            var coverageCalculator = new Mock<ITestCoverageCalculator>();
            var environment = new Mock<IActiveProgramEnvironment>();
            var coverage = new Mock<IClassTestCoverage>();
            var mockTests = new Mock<ISet<Unittest>>();
            var program = new Mock<IProgramVersion>();
            var programName = "PROGRAM";
            var testResult = new Mock<TestResult>();

            program.Setup(r => r.Name).Returns(programName);
            program.Setup(r => r.TestsToRun(coverage.Object)).Returns(mockTests.Object);
            coverageCalculator.Setup(r => r.ClassTestCoverage()).Returns(coverage.Object);
            paths.Setup(r => r.TestFilepath).Returns(testFilepath);
            paths.Setup(r => r.GetMutantTestResultFilepath(programName)).Returns(mutantTestResultFilepath);
            runner.Setup(r => r.TestSolution(testFilepath, mutantTestResultFilepath, mockTests.Object)).Returns(testResult.Object);

            var framework = new DotnetTestFramework(paths.Object, runner.Object, coverageCalculator.Object, environment.Object);

            framework.Test(program.Object);

            environment.Verify(r => r.Load(program.Object));
            environment.VerifyNoOtherCalls();
        }

        [Fact]
        public void TestTestReturnsTestResultsFromRunner()
        {
            var paths = new Mock<IPathProvider>();
            var runner = new Mock<ITestRunner>();
            var coverageCalculator = new Mock<ITestCoverageCalculator>();
            var environment = new Mock<IActiveProgramEnvironment>();
            var coverage = new Mock<IClassTestCoverage>();
            var mockTests = new Mock<ISet<Unittest>>();
            var program = new Mock<IProgramVersion>();
            var programName = "PROGRAM";
            var testResult = new Mock<TestResult>();

            program.Setup(r => r.Name).Returns(programName);
            program.Setup(r => r.TestsToRun(coverage.Object)).Returns(mockTests.Object);
            coverageCalculator.Setup(r => r.ClassTestCoverage()).Returns(coverage.Object);
            paths.Setup(r => r.TestFilepath).Returns(testFilepath);
            paths.Setup(r => r.GetMutantTestResultFilepath(programName)).Returns(mutantTestResultFilepath);
            runner.Setup(r => r.TestSolution(testFilepath, mutantTestResultFilepath, mockTests.Object)).Returns(testResult.Object);

            var framework = new DotnetTestFramework(paths.Object, runner.Object, coverageCalculator.Object, environment.Object);

            Assert.Equal(testResult.Object,framework.Test(program.Object));


        }

    }
}
