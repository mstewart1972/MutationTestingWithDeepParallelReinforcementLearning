using ClassLibrary1;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddTest()
        {
            // 1000-addToMul=live, 0100-addToSub=live, 0010-addToDiv=undefined, 0001-addToRem=live
            BasicMathFunctions system = new BasicMathFunctions();
            int expected = 0;
            int actual = system.Add(0, 0);
            Assert.AreEqual(expected, actual, "AddTest: The expected value did not match the actual value.");
        }

        [Test]
        public void SubTest()
        {
            // 1000-subToMul=kill, 0100-subToAdd=kill, 0010-subToDiv=kill, 0001-subToRem=kill
            BasicMathFunctions system = new BasicMathFunctions();
            int expected = 2;
            int actual = system.Sub(3, 1);
            Assert.AreEqual(expected, actual, "SubTest: The expected value did not match the actual value.");
        }

    }
}