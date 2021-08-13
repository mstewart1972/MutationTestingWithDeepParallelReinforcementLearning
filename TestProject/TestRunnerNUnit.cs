using MutantCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
namespace MutantTestRunner
{
    public class TestRunnerNUnit// : TestRunner
    {
        #region Constant Declaration
        public const string UNIT_TEST_NAME = "NUnit";
        public const string TEST_RUN_ELEMENT_NAME = "test-run";
        public const string RESULT_ELEMENT_NAME = "result";
        public const string FAILED_ELEMENT_NAME = "Failed";
        public const string TEST_CASE_COUNT = "testcasecount";
        public const string NUNIT_TEST_PATH = "NUnitTestPath";
        public const string NUNIT_TEST_ERR_MESSAGE = "Error when executing NUnit Test: ";
        public const string MESSAGE_ELEMENT_NAME = "message";
        public const string TEST_SUITE_ELEMENT_NAME = "test-suite";
        public const string REASON_ELEMENT_NAME = "reason";
        public const string FAILURE_ELEMENT_NAME = "failure";

        #endregion

        public TestRunnerNUnit()
        {
            this.ToolName = UNIT_TEST_NAME;
        }

   
        /// <summary>
        ///  Process result file to specify all test cases pass or not.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public override TestResult ProcessResultFile(string fileName)
        {

            XmlReader reader = XmlReader.Create(fileName, new XmlReaderSettings()
            {
                CheckCharacters = false
            });
            bool flag = false;
            //message = MESSAGE_ELEMENT_NAME;
            try
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == TEST_RUN_ELEMENT_NAME)
                            {
                                XElement xelement = XNode.ReadFrom(reader) as XElement;
                                if (xelement != null)
                                {
                                    string key = xelement.Attribute((XName)RESULT_ELEMENT_NAME).Value;
                                    flag = key.Equals(FAILED_ELEMENT_NAME);


                                    testCaseCount = Convert.ToInt32(xelement.Attribute((XName)TEST_CASE_COUNT).Value);
                                }
                                if (!flag)
                                {
                                    //if (xelement.Element(TEST_SUITE_ELEMENT_NAME).Element(REASON_ELEMENT_NAME) != null)
                                        //message = xelement.Element(TEST_SUITE_ELEMENT_NAME).Element(REASON_ELEMENT_NAME).Value;
                                    //else
                                        //message = xelement.Element(TEST_SUITE_ELEMENT_NAME).Element(FAILURE_ELEMENT_NAME).Element(MESSAGE_ELEMENT_NAME).Value;
                                }
                            }
                            break;
                    }

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //message = ex.Message;
               
            }
            return new TestResult { Survived = flag };
        }

        public override bool RunExternalTestToolForSolution(string inputFile, string outputFile)
        {
            string str = "\"" + inputFile + "\" --result \"" + outputFile + "\" --workers=32 --stoponerror";
            string nunitTestPath = ConfigurationManager.AppSettings[NUNIT_TEST_PATH];
            var logger = NLog.LogManager.GetCurrentClassLogger();
            bool result = false;

            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.Arguments = str;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.ErrorDialog = true;
                    process.StartInfo.RedirectStandardOutput = false;
                    process.StartInfo.FileName = nunitTestPath;
                    process.StartInfo.UseShellExecute = false;
                 
                    process.Start();
                    result = process.WaitForExit(600000) && process.ExitCode >= 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error(NUNIT_TEST_ERR_MESSAGE + ex.Message);
                result = false;

            }


             return result;
        }

    
       

    }
}
