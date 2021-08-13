using Moq;
using MultiStringDiff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.MultiStringDiff
{
    public class TestMultiStringDiff
    {
        [Fact]
        public void TestAddStringCallsDiffer()
        { 
            var differ = new Mock<IDiffer>();
            var baseString = "I Like Cake";
            var addString = "You Like Cake";
            var multiStringDiff = new MultiStringDiff<int>(baseString, differ.Object);

            var replacements = new List<StringReplacement> { new StringReplacement { OldString = "I", NewString = "You", Position = 0} };

            differ.Setup(r => r.Diff(baseString, addString)).Returns(replacements);

            multiStringDiff.AddString(1, addString);

            differ.VerifyAll();
            
        }

        [Fact]
        public void TestToModelWithReplacementAtBeginning()
        {
            var differ = new Mock<IDiffer>();
            var baseString = "I Like Cake";
            var addString = "You Like Cake";
            var multiStringDiff = new MultiStringDiff<int>(baseString, differ.Object);

            var replacements = new List<StringReplacement> { new StringReplacement { OldString = "I", NewString = "You", Position = 0 } };

            differ.Setup(r => r.Diff(baseString, addString)).Returns(replacements);

            multiStringDiff.AddString(1, addString);
            var result = multiStringDiff.GetModel(num => num.ToString());

            var emptyDictionary = new Dictionary<string, string>();

            Assert.Collection(result,
                one=>
                {
                    Assert.Equal("I", one.BaseString);
                    Assert.Equal(new Dictionary<string, string> { { "1", "You" } }, one.Alternatives);
                    Assert.Equal(emptyDictionary, one.Prefixes);
                },
                two=>
                {
                    Assert.Equal(" Like Cake", two.BaseString);
                    Assert.Equal(emptyDictionary, two.Alternatives);
                    Assert.Equal(emptyDictionary, two.Prefixes);
                }
                
                );
        }

        [Fact]
        public void TestToModelWithInsertionAtBeginning()
        {
            var differ = new Mock<IDiffer>();
            var baseString = "Cake";
            var addString = "I Like Cake";
            var multiStringDiff = new MultiStringDiff<int>(baseString, differ.Object);

            var replacements = new List<StringReplacement> { new StringReplacement { OldString = "", NewString = "I Like ", Position = 0 } };

            differ.Setup(r => r.Diff(baseString, addString)).Returns(replacements);

            multiStringDiff.AddString(1, addString);
            var result = multiStringDiff.GetModel(num => num.ToString());

            var emptyDictionary = new Dictionary<string, string>();

            Assert.Collection(result,
                one =>
                {
                    Assert.Equal("Cake", one.BaseString);
                    Assert.Equal(new Dictionary<string, string> { { "1", "I Like " } }, one.Prefixes);
                    Assert.Equal(emptyDictionary, one.Alternatives);
                }
                );
        }

        [Fact]
        public void TestToModelWithDeletionAtBeginning()
        {
            var differ = new Mock<IDiffer>();
            var baseString = "I Like Cake";
            var addString = "Cake";
            var multiStringDiff = new MultiStringDiff<int>(baseString, differ.Object);

            var replacements = new List<StringReplacement> { new StringReplacement { OldString = "I Like ", NewString = "", Position = 0 } };

            differ.Setup(r => r.Diff(baseString, addString)).Returns(replacements);

            multiStringDiff.AddString(1, addString);
            var result = multiStringDiff.GetModel(num => num.ToString());

            var emptyDictionary = new Dictionary<string, string>();

            Assert.Collection(result,
                one =>
                {
                    Assert.Equal("I Like ", one.BaseString);
                    Assert.Equal(new Dictionary<string, string> { { "1", "" } }, one.Alternatives);
                    Assert.Equal(emptyDictionary, one.Prefixes);
                },
                two =>
                {
                    Assert.Equal("Cake", two.BaseString);
                    Assert.Equal(emptyDictionary, two.Alternatives);
                    Assert.Equal(emptyDictionary, two.Prefixes);
                }

                );
        }

        [Fact]
        public void TestToModelWithInsertionAtEnd()
        {
            var differ = new Mock<IDiffer>();
            var baseString = "Cake";
            var addString = "Cakes";
            var multiStringDiff = new MultiStringDiff<int>(baseString, differ.Object);

            var replacements = new List<StringReplacement> { new StringReplacement { OldString = "", NewString = "s", Position = 4 } };

            differ.Setup(r => r.Diff(baseString, addString)).Returns(replacements);

            multiStringDiff.AddString(1, addString);
            var result = multiStringDiff.GetModel(num => num.ToString());

            var emptyDictionary = new Dictionary<string, string>();

            Assert.Collection(result,
                one =>
                {
                    Assert.Equal("Cake", one.BaseString);
                    Assert.Equal(emptyDictionary, one.Prefixes);
                    Assert.Equal(emptyDictionary, one.Alternatives);
                },
                two =>
                {
                    Assert.Equal("", two.BaseString);
                    Assert.Equal(new Dictionary<string, string> { { "1", "s" } }, two.Prefixes);
                    Assert.Equal(emptyDictionary, two.Alternatives);
                }
                );
        }
    }
}
