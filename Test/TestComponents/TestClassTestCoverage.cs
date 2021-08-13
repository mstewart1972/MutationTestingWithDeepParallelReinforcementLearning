using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestComponents;
using Xunit;

namespace Test.TestComponents
{
    public class TestClassTestCoverage
    {
        [Fact]
        public void TestEmptyCoverageHasNoClasses()
        {
            var coverage = new ClassTestCoverage();
            Assert.Empty(coverage.AllTestedClasses());
        }

        [Fact]
        public void TestEmptyCoverageHasNoTests()
        {
            var coverage = new ClassTestCoverage();
            Assert.Empty(coverage.Unittests());
        }

        [Fact]
        public void TestEmptyCoverageReturnsNoClassesOnCovers()
        {
            var coverage = new ClassTestCoverage();
            var unittest = new Unittest();
            Assert.Empty(coverage.Covers(unittest));
        }

        [Fact]
        public void TestEmptyCoverageReturnsNoTestsOnCoverage()
        {
            var coverage = new ClassTestCoverage();
            var testClass = new Class();
            Assert.Empty(coverage.Coverage(testClass));
        }

        [Fact]
        public void TestPopulatedCoverageReturnsNoTestsOnCoverageWhenClassDoesNotMatch()
        {
            var coverage = new ClassTestCoverage();
            var unusedClass = new Class { Name = "Unused"};
            var usedClass = new Class { Name = "Used" };
            var test = new Unittest { Name = "Test" };
            coverage.Add(usedClass, test);
            Assert.Empty(coverage.Coverage(unusedClass));
        }

        [Fact]
        public void TestPopulatedCoverageReturnsNoClassesOnCoversWhenTestDoesNotMatch()
        {
            var coverage = new ClassTestCoverage();
            var usedClass = new Class { Name = "Used" };
            var test = new Unittest { Name = "Test" };
            var unusedTest = new Unittest { Name = "Unused" };
            coverage.Add(usedClass, test);
            Assert.Empty(coverage.Covers(unusedTest));
        }

        [Fact]
        public void TestPopulatedCoverageReturnsCorrectTestsOnCoverageWhenClassDoesMatch()
        {
            var coverage = new ClassTestCoverage();
            var usedClass2 = new Class { Name = "Used2" };
            var usedClass1 = new Class { Name = "Used1" };
            var test1 = new Unittest { Name = "Test1" };
            var test2 = new Unittest { Name = "Test2" };
            coverage.Add(usedClass1, test1);
            coverage.Add(usedClass2, test2);
            Assert.Collection(coverage.Coverage(usedClass1),
                testOne => Assert.Equal(testOne, test1)                
                );
        }

        [Fact]
        public void TestPopulatedCoverageReturnsCorrectClassesOnCoversWhenTestDoesMatch()
        {
            var coverage = new ClassTestCoverage();
            var usedClass2 = new Class { Name = "Used2" };
            var usedClass1 = new Class { Name = "Used1" };
            var test1 = new Unittest { Name = "Test1" };
            var test2 = new Unittest { Name = "Test2" };
            coverage.Add(usedClass1, test1);
            coverage.Add(usedClass2, test2);
            Assert.Collection(coverage.Covers(test1),
                classOne => Assert.Equal(classOne, usedClass1)
                );
        }

        [Fact]
        public void TestPopulatedCoverageReturnsAllClassesOnAllTestedClasses()
        {
            var coverage = new ClassTestCoverage();
            var usedClass2 = new Class { Name = "Used2" };
            var usedClass1 = new Class { Name = "Used1" };
            var test1 = new Unittest { Name = "Test1" };
            var test2 = new Unittest { Name = "Test2" };
            coverage.Add(usedClass1, test1);
            coverage.Add(usedClass2, test2);
            Assert.Collection(coverage.AllTestedClasses(),
                classOne => Assert.Equal(classOne, usedClass1),
                classTwo => Assert.Equal(classTwo, usedClass2)
                );
        }

        [Fact]
        public void TestPopulatedCoverageReturnsAllTestsOnUnittests()
        {
            var coverage = new ClassTestCoverage();
            var usedClass2 = new Class { Name = "Used2" };
            var usedClass1 = new Class { Name = "Used1" };
            var test1 = new Unittest { Name = "Test1" };
            var test2 = new Unittest { Name = "Test2" };
            coverage.Add(usedClass1, test1);
            coverage.Add(usedClass2, test2);
            Assert.Collection(coverage.Unittests(),
                testOne => Assert.Equal(testOne, test1),
                testTwo => Assert.Equal(testTwo, test2)
                );
        }
    }
}
