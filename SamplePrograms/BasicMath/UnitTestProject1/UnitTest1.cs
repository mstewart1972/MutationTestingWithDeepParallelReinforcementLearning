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
        public void Test1()
        {
            var cl = new ClassLibrary1.Class1();
            Assert.AreEqual(cl.Method1(2, 2), 4);
        }
    }
}