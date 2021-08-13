using NUnit.Framework;
using FindMax;

namespace MaxNUnitTestProject
{
    public class MaxTestSet1
    {
        [Test]
        public void test1()
        {
            int[] tmp = new int[] {
                9,
                3,
                8,
                5,
                3,
                5,
                1,
                4,
                6,
                2};
            Max m = new Max(10, tmp);
            int r = m.findmax();
            Assert.AreEqual(9,r);
        }

        [Test]
        public void test2()
        {
            int[] tmp = new int[] {
                8,
                3,
                8,
                5,
                9,
                5,
                1,
                4,
                6,
                2};
            Max m = new Max(10, tmp);
            int r = m.findmax();
            Assert.AreEqual(9, r);
        }

        [Test]
        public void test3()
        {
            int[] tmp = new int[] {
                8,
                3,
                8,
                5,
                7,
                5,
                1,
                4,
                6,
                9};
            Max m = new Max(10, tmp);
            int r = m.findmax();
            Assert.AreEqual(9, r);
        }

        [Test]
        public void test4()
        {
            int[] tmp = new int[] {
                8,
                3,
                8,
                5,
                7,
                9,
                1,
                4,
                6,
                2};
            Max m = new Max(10, tmp);
            int r = m.findmax();
            Assert.AreEqual(9, r);
        }
    }
}