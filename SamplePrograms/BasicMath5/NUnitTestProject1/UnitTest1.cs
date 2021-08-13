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

        [Test]
        public void SubTest()
        {
            // 1000-subToMul=kill, 0100-subToAdd=kill, 0010-subToDiv=kill, 0001-subToRem=live
            BasicMathFunctions system = new BasicMathFunctions();
            int expected = 0;
            int actual = system.Sub(1, 1);
            Assert.AreEqual(expected, actual, "SubTest: The expected value did not match the actual value.");
        }

        [Test]
        public void MulTest()
        {
            // 1000-mulToAdd=kill, 0100-mulToSub=kill, 0010-mulToDiv=live, 0001-mulToRem=kill
            BasicMathFunctions system = new BasicMathFunctions();
            int expected = 1;
            int actual = system.Mul(1, 1);
            Assert.AreEqual(expected, actual, "MulTest: The expected value did not match the actual value.");
        }

        [Test]
        public void DivTest()
        {
            // 1000-divToAdd=kill, 0100-divToSub=kill, 0010-divToMul=live, 0001-divToRem=kill
            BasicMathFunctions system = new BasicMathFunctions();
            int expected = 1;
            int actual = system.Div(1, 1);
            Assert.AreEqual(expected, actual, "DivTest: The expected value did not match the actual value.");
        }

        [Test]
        public void ModTest()
        {
            // 1000-remToAdd=kill, 0100-remToSub=kill, 0010-remToDiv=live, 0001-remToMul=live
            BasicMathFunctions system = new BasicMathFunctions();
            int expected = 0;
            int actual = system.Mod(0, 1);
            Assert.AreEqual(expected, actual, "ModTest: The expected value did not match the actual value.");
        }

    }
}