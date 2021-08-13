using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class BasicMathFunctionsUnitTest
    {

        //[TestMethod]
        //public void AddNegativeTest()
        //{
        //    try
        //    {
        //        BasicMathFunctions system = new BasicMathFunctions();
        //        int expected = 42;
        //        int actual = system.Add("x", 2);
        //        Assert.Fail("AddNegativeTest: was unsuccessful");
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsTrue(ex.Message = "Conversion from string ""x"" to type 'Integer' is not valid.", "AddNegativeTest: was successful");
        //    }
        //}

        [TestMethod]
        public void AddTest()
        {
            BasicMathFunctions system = new BasicMathFunctions();
            int expected = 4;
            int actual = system.Add(2, 2);
            Assert.AreEqual(expected, actual, "AddTest: The expected value did not match the actual value.");
        }

    }

    //<TestMethod()> Public Sub AddTest()

    //    Dim system As New BasicMathFunctions()
    //    Dim expected As Integer = 42
    //    Dim actual As Integer = system.Add(40, 2)
    //    Assert.AreEqual(expected, actual, "AddTest: The expected vaule did not match the actual value.")
    //End Sub



    //<TestMethod()> Public Sub SubtractTest()
    //    Dim system As New BasicMathFunctions()
    //    Dim expected As Integer = 38
    //    Dim actual As Integer = system.Subtract(40, 2)
    //    Assert.AreEqual(expected, actual, "SubtractTest: The expected vaule did not match the actual value.")
    //End Sub
}
