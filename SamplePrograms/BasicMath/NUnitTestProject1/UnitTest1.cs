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
            // 1000-addToMul=live, 0100-addToSub=kill, 0010-addToDiv=kill, 0001-addToRem=kill        
            BasicMathFunctions system = new BasicMathFunctions();
            int expected = 4;
            int actual = system.Add(2, 2);
            Assert.AreEqual(expected, actual, "AddTest: The expected value did not match the actual value.");
        }
    }
}