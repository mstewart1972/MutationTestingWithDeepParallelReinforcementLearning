using MultiStringDiff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.MultiStringDiff
{
    public class StringSectionTest
    {
        [Theory]
        [InlineData(0, 0, 4, true, true, false, "Example", "")]
        [InlineData(0, 1, 1, false, false, true, "", "Example")]
        [InlineData(0, 3, 3, false, false, true, "", "Example")]
        [InlineData(0, 4, 4, true, false, true, "", "Example")]
        [InlineData(1, 0, 1, false, false, false, "", "Example")]
        public void TestAddAlterantive(int position, int length, int endLength, bool nextIsNull, bool addPrefix, bool addAlternative, string newPrefix, string newAlternative)
        {
            var baseSection = StringSection<int>.BaseStringSection("Test");
            var key = 1;
            baseSection.AddAlternative(key, "Example", position, length);

            Assert.Equal(0, baseSection.Position);
            Assert.Equal(endLength, baseSection.Length);

            Assert.True((baseSection.Next == null) == nextIsNull);

            if (addPrefix)
            {
                Assert.True(baseSection.Prefixes.ContainsKey(key));
                Assert.Equal(0, String.Compare(newPrefix, baseSection.Prefixes[key]));
            }
            else
            {
                Assert.False(baseSection.Prefixes.ContainsKey(key));
            }


            if (addAlternative)
            {
                Assert.True(baseSection.Alternatives.ContainsKey(key));
                Assert.Equal(0, String.Compare(newAlternative, baseSection.Alternatives[key]));
            }
            else
            {
                Assert.False(baseSection.Alternatives.ContainsKey(key));
            }
        }

        [Fact]
        public void TestAddAlternativeAtTotalEnd()
        {
            var baseSection = StringSection<int>.BaseStringSection("Cake");
            baseSection.AddAlternative(1, "s", 4, 0);

            Assert.Empty(baseSection.Prefixes);
            Assert.Empty(baseSection.Alternatives);
            Assert.Equal("Cake", baseSection.BaseString);
            Assert.Equal(4, baseSection.Length);

            var nextSection = baseSection.Next;

            Assert.Empty(nextSection.Alternatives);
            Assert.Collection(nextSection.Prefixes,
                one =>
                {
                    Assert.Equal(1, one.Key);
                    Assert.Equal("s", one.Value);
                }
                );
            Assert.Equal("", nextSection.BaseString);
            Assert.Equal(0, nextSection.Length);
        }

        [Fact]
        public void TestAddAlternativeAtTotalEndTwice()
        {
            var baseSection = StringSection<int>.BaseStringSection("Cake");
            baseSection.AddAlternative(1, "s", 4, 0);
            baseSection.AddAlternative(2, "y", 4, 0);

            Assert.Empty(baseSection.Prefixes);
            Assert.Empty(baseSection.Alternatives);
            Assert.Equal("Cake", baseSection.BaseString);
            Assert.Equal(4, baseSection.Length);

            var nextSection = baseSection.Next;

            Assert.Empty(nextSection.Alternatives);
            Assert.Collection(nextSection.Prefixes,
                one =>
                {
                    Assert.Equal(1, one.Key);
                    Assert.Equal("s", one.Value);
                },
                two =>
                {
                    Assert.Equal(2, two.Key);
                    Assert.Equal("y", two.Value);
                }
                );
            Assert.Equal("", nextSection.BaseString);
            Assert.Equal(0, nextSection.Length);
        }
    }
}
