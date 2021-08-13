using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;
using MutantCommon;

namespace MutantTestRunner
{
    public class TestRunnerMSTest// : TestRunner
    {
        #region Constant Declaration
        public const string UNIT_TEST_RESULT_ELEMENT_NAME= "UnitTestResult";
        public const string OUT_COME_ELEMENT_NAME = "outcome";
        public const string PASSED_ELEMENT_NAME = "Passed";
        public const string COUNTER_ELEMENT_NAME = "Counters";
        public const string TOTAL_ELEMENT_NAME = "total";
        public const string MS_TEST_PATH = "MSTestPath";
        public const string MESSAGE_ELEMENT_NAME = "<message>";
        #endregion
        public override TestResult ProcessResultFile(string fileName)
        {
            XmlReader reader = XmlReader.Create(fileName, new XmlReaderSettings()
            {
                CheckCharacters = false
            });
            bool flag = false;
            //TODO: need to implement MSTEST.
            //message = MESSAGE_ELEMENT_NAME;
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == UNIT_TEST_RESULT_ELEMENT_NAME)
                        {
                            XElement xelement = XNode.ReadFrom(reader) as XElement;
                            if (xelement != null)
                            {
                                string key = xelement.Attribute((XName)OUT_COME_ELEMENT_NAME).Value;
                                flag = key.Equals(PASSED_ELEMENT_NAME);
                            }
                        }
                        else if (reader.Name == COUNTER_ELEMENT_NAME)
                        {
                            XElement xelement = XNode.ReadFrom(reader) as XElement;
                            if (xelement != null)
                            {
                                this.testCaseCount = Convert.ToInt32(xelement.Attribute((XName)TOTAL_ELEMENT_NAME).Value);
                            }
                              
                        }
                            break;
                
                }

            }
            reader.Close();
            return new TestResult { Survived = flag };
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="inputFile"></param>
       /// <param name="outputFile"></param>
       /// <returns></returns>
        public override bool RunExternalTestToolForSolution(string inputFile, string outputFile)
        {
            string str = "/testcontainer:" + inputFile + " /resultsfile:" + outputFile;
            string msTestPath = ConfigurationManager.AppSettings[MS_TEST_PATH];
            Process process = Process.Start(new ProcessStartInfo()
            {
                Arguments = str,
                CreateNoWindow = true,
                ErrorDialog = true,
                RedirectStandardOutput = false,
                FileName = msTestPath,
                UseShellExecute = false,

            });
            return process.WaitForExit(60000) && process.ExitCode >= 0;
        }
      
    }
}
