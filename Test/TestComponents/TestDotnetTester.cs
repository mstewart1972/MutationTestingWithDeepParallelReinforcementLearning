using Moq;
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
    public class TestDotnetTester
    {
        [Fact]
        public void TestProgramPathReturnsDotnetPath()
        {
            var paths = new Mock<IPathProvider>();
            var resultDirectory = "RESULT";
            var filterArgument = "FILTERARGUMENT";
            var dotnetPath = "DOTNETPATH";

            paths.Setup(p => p.DotnetPath).Returns(dotnetPath);

            var dotnetTester = new DotnetTester(paths.Object, resultDirectory, filterArgument);
            Assert.Equal(dotnetPath, dotnetTester.ProgramPath);
        }

        [Fact]
        public void TestArgumentsReturnsCorrectly()
        {
            var paths = new Mock<IPathProvider>();
            var resultDirectory = "RESULT";
            var buildDirectory = "BUILD";
            var solutionPath = "SOLUTION";
            var filterArgument = "FILTERARGUMENT";
            var dotnetPath = "DOTNETPATH";

            var expectedResult = "test -o BUILD -v q --no-build -r RESULT -l trx; SOLUTION FILTERARGUMENT";

            paths.Setup(p => p.DotnetPath).Returns(dotnetPath);
            paths.Setup(p => p.SolutionPath).Returns(solutionPath);
            paths.Setup(p => p.BuildDirectory).Returns(buildDirectory);

            var dotnetTester = new DotnetTester(paths.Object, resultDirectory, filterArgument);
            Assert.Equal(expectedResult, dotnetTester.Arguments);
        }

    }
}
