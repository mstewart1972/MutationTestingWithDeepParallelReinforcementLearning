using MutantCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MutantTestRunner
{
    public class TestRunnerXUnit : TestRunner
    {
        #region Constant Declaration
        public const string UNIT_TEST_NAME = "XUnit";
        
        public const string PASSED_ELEMENT_NAME = "passed";
        public const string FAILED_ELEMENT_NAME = "Failed";
        public const string TEST_CASE_COUNT = "total";
        public const string XUNIT_TEST_PATH = "XUnitTestPath";
        public const string NUNIT_TEST_ERR_MESSAGE = "Error when executing NUnit Test: ";
        public const string COLLECTION_ELEMENT_NAME = "collection";
        public const string MESSAGE_ELEMENT_NAME = "message";
        public const string TEST_ELEMENT_NAME = "test";
        public const string REASON_ELEMENT_NAME = "reason";
        public const string FAILURE_ELEMENT_NAME = "failure";

        #endregion
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
                            if (reader.Name == COLLECTION_ELEMENT_NAME)
                            {
                                XElement xelement = XNode.ReadFrom(reader) as XElement;
                                if (xelement != null)
                                {
                                    testCaseCount = Convert.ToInt32(xelement.Attribute((XName)TEST_CASE_COUNT).Value);
                                    int passed = Convert.ToInt32(xelement.Attribute((XName)PASSED_ELEMENT_NAME).Value);
                                    flag = testCaseCount == passed;



                                }
                                if (!flag && xelement.Element(TEST_ELEMENT_NAME).Element(FAILURE_ELEMENT_NAME).Element(MESSAGE_ELEMENT_NAME) != null)
                                {
                                    //message = xelement.Element(TEST_ELEMENT_NAME).Element(FAILURE_ELEMENT_NAME).Element(MESSAGE_ELEMENT_NAME).Value;
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
            string str = "\"" + inputFile + "\" -xml \"" + outputFile + "\"";
            string xunitTestPath = ConfigurationManager.AppSettings[XUNIT_TEST_PATH];
            Process process = Process.Start(new ProcessStartInfo()
            {
                Arguments = str,
                CreateNoWindow = true,
                ErrorDialog = true,
                RedirectStandardOutput = false,
                FileName = xunitTestPath,
                UseShellExecute = false,

            });
            return process.WaitForExit(60000) && process.ExitCode >= 0;
        }
    }
}
