using MultiStringDiff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.MultiStringDiff
{
    public class DiffTest
    {
        [Fact]
        public void TestSameStringsGivesNoDifferences()
        {
            var differ = new Differ();
            var result = differ.Diff("Cake", "Cake");
            Assert.Empty(result);
        }

        [Fact]
        public void TestPrefixedStringGivesInsertion()
        {
            var differ = new Differ();
            var result = differ.Diff("Cake", "A Cake");
            Assert.Collection(result, one =>
             {
                 Assert.Equal("A ", one.NewString);
                 Assert.Equal("", one.OldString);
                 Assert.Equal(0, one.Position);
                 Assert.Equal(0, one.Length);
             }
             );
        }

        [Fact]
        public void TestReplacedStringGivesReplacement()
        {
            var differ = new Differ();
            var result = differ.Diff("Cake", "Fake");
            Assert.Collection(result, one =>
            {
                Assert.Equal("F", one.NewString);
                Assert.Equal("C", one.OldString);
                Assert.Equal(0, one.Position);
                Assert.Equal(1, one.Length);
            }
             );
        }

        [Fact]
        public void TestInsertionInCenterGivesCorrectPosition()
        {
            var differ = new Differ();
            var result = differ.Diff("I Like Cake", "I Eat Cake");
            Assert.Collection(result, one =>
            {
                Assert.Equal("Eat", one.NewString);
                Assert.Equal("Like", one.OldString);
                Assert.Equal(2, one.Position);
                Assert.Equal(4, one.Length);
            }
             );
        }

        [Fact]
        public void TestInsertionAtEnd()
        {
            var differ = new Differ();
            var result = differ.Diff("Cake", "Cake Is Great");
            Assert.Collection(result, one =>
            {
                Assert.Equal(" Is Great", one.NewString);
                Assert.Equal("", one.OldString);
                Assert.Equal(4, one.Position);
                Assert.Equal(0, one.Length);
            }
             );
        }

        [Fact]
        public void TestDeletionAtEnd()
        {
            var differ = new Differ();
            var result = differ.Diff("Cake Is Great", "Cake");
            Assert.Collection(result, one =>
            {
                Assert.Equal("", one.NewString);
                Assert.Equal(" Is Great", one.OldString);
                Assert.Equal(4, one.Position);
                Assert.Equal(9, one.Length);
            }
             );
        }

        [Fact]
        public void TestReplacementAtEnd()
        {
            var differ = new Differ();
            var result = differ.Diff("Cake Is Great", "Cake Is Tasty");
            Assert.Collection(result, one =>
            {
                Assert.Equal("Tasty", one.NewString);
                Assert.Equal("Great", one.OldString);
                Assert.Equal(8, one.Position);
                Assert.Equal(5, one.Length);
            }
             );
        }

        [Fact]
        public void TestMultipleChangesInStringsGivesMultipleChanges()
        {
            var differ = new Differ();
            var result = differ.Diff("I Like Eating Cake", "I Hate Eating Peanut Butter");
            Assert.Collection(result
                , one =>
                {
                    Assert.Equal("Hat", one.NewString);
                    Assert.Equal("Lik", one.OldString);
                    Assert.Equal(2, one.Position);
                    Assert.Equal(3, one.Length);
                }
                , two =>
                {
                    Assert.Equal("Peanut Butter", two.NewString);
                    Assert.Equal("Cake", two.OldString);
                    Assert.Equal(14, two.Position);
                    Assert.Equal(4, two.Length);
                }
                 );
        }
    }
}
