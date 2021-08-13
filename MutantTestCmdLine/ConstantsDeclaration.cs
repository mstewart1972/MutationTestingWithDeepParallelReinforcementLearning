using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantTesting
{
    public static class ConstantsDeclaration
    {
        public const string AOR_MUTANT = "AOR";
        public const string AOR_MUTANT_DESCRIPTION = "Arithmetic operator replacement (+, -, *, /, %, +, -, ++, --)";
        public const string LOR_MUTANT = "LOR";
        public const string LOR_MUTANT_DESCRIPTION = "Logical operator replacement (&&, ||)";
        public const string ROR_MUTANT = "ROR";
        public const string ROR_MUTANT_DESCRIPTION = "Relational operator replacement (>, >=, <, <=, ==, !=)";
        public const string AMC_MUTANT = "AMC";
        public const string AMC_MUTANT_DESCRIPTION = "Access modifier change (public,private,protected)";

        public const string NUNIT = "nunit";
        public const string NUNIT_FRAMEWORK = "nunit.framework";
        public const string CECIL = "cecil";
        public const string MS_TEST_FRAMEWORK = "UnitTestFramework";
        public const string TEST_KEY_WORD = "Test";
      
        public const string NUNIT_3 = "NUnit3";
        public const string TEST_PROJECT_RUNNER = "MutantTestRunner";

        public const string MODULE_KEY_WORD = "<Module>";
        public const string SOURCE_KEY_WORD = "Source";
        public const string MSTEST_CLASS_ATTRIBUTE = "Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute";
        public const string MSTEST_UNIT_TYPE = "MSTEST";
        public const string XUNIT_UNIT_TYPE = "XUNIT";
        public const string NUNIT_FIXTURE_ATTRIBUTE = "NUnit.Framework.TestFixtureAttribute";
        public const string NUNITEST_UNIT_TYPE = "NUnit";
        public const string UNKNOWN_KEYWORD = "Unknown";
        public const string CONGRATULATION_KEY_WORD = "Congratulation";
        public const string DIFFERENCE_KEYWORD = "Difference";

        public const string SURVIVED_KEY_WORD = "Survived";
        public const string KILLED_KEY_WORD = "Killed";
        public const string SURVIVED_MUTANTS_KEY_WORD = "Survived Mutants:";
        public const string KILLED_MUTANTS_KEY_WORD = "Killed Mutants:";

        #region Button Name
        public const string SELECT_ALL_BUTTON = "Select All";
        public const string DESELECT_ALL_BUTTON = "Deselect All";
        #endregion
        #region ToolTip
        public const string OFFUTT_TOOLTIP = "Offutt operators can generate 99.5 mutation score";
        #endregion


        #region File extension
        public const string DLL_KEY_WORD = "dll";
        public const string PDB_EXTENSION_KEY_WORD = ".pdb";
        public const string DLL_EXTENSION_KEY_WORD = ".dll";
        public const string XML_KEY_WORD = "xml";


        #endregion

        #region Search pattern
        public const string SEARCH_ALL_XML_PATTERN= "*.xml";
        #endregion
        #region Message Dialog
        public const string BLANK_SOLUTION_MESSAGE = "Please choose a solution to compile";
        public const string INFORMATION_KEY_WORD = "Information";
        public const string BUILD_SOLUTION_SUCCESSFULLY_MSG = "Buid solution completely ! Please wait to run test case";
        public const string BUILD_SOLUTION_ERR_MSG = "There are errors in solution. Please check again !";
        public const string COPY_FILES_ERROR_MESSAGE = "Error when copying files: ";
        public const string ERROR_KEY_WORD = "Error";
        public const string RUNNING_UNIT_TEST = "Running Unit Test...";
        public const string TEST_CASE_PASS_MESSAGE = "All test cases pass !";
        public const string TEST_CASE_FAILED_MESSAGE = "Test case failed.Please check the test case !";
        public const string EXECUTING_UNIT_TEST_ERROR_MESSAGE = "Error when executing test !";
        public const string NO_OPERATOR_SELECTION_MESSAGE = "Please choose operators to generate mutant files !";
        public const string GENERATING_MUTANT_SUCCESSFULLY_MESSAGE_WITH_CONFIRMATION = " mutants are generated successfullly ! Would you like to view their details ?";
        public const string GENERATING_MUTANT_SUCCESSFULLY_MESSAGE = " mutants are generated successfully !";
        #endregion

        #region Folder Name
        public const string PACKAGE_FOLDER = "packages";
        public const string VS_FOLDER = ".vs";
        public const string BIN_DEBUG_FOLDER = "\\bin\\Debug";
        public const string ORIGINAL_CODE_FOLDER = "\\OriginalCode";
        public const string MUTANT_CODE_FOLDER = "\\Mutants";
        public const string COPY_FOLDER = "CopyFiles";
        #endregion


        #region Command line parameter
        public const string SOLUTION_DIR_PARAMETER_NAME = "-solutiondir";
        public const string UNIT_TEST_TYPE_PARAMETER_NAME = "-unittesttype";
        public const string BUILD_SOLUTION_PARAMETER_NAME = "-buildsolution";
        public const string REPORT_DIR_PARAMETER_NAME = "-reportdir";
        public const string EQUAL_OPERATOR = "=";
        public const string TEST_FILE_PARAMETER_NAME = "-testfile";
        public const string SOURCE_FILE_PARAMETER_NAME = "-sourcefile";
        public const string MUTANT_OPERATOR_PARAMETER_NAME = "-operators";
        public const string TEST_ADAPTOR_PARAMETER_NAME = "-testadaptor";
        public const string VERBOSE_PARAMETER_NAME = "-verbose";
        public const string BATCH_SIZE_PARAMETER_NAME = "-batchsize";
        public const string AGENT_NUMBER_PARAMETER_NAME = "-agentnumber";

        public const string PARAMETER_INVALID_ERR_MESSAGE = "Parameter name is not correct.Please --help to get more information !";
        public const string REQUIRED_PARAMETER_ERR_MESSAGE = "Solution Directory, Source File and Test File name parameters are required !";
        public const string MISSING_PARAMETER_MESSAGE = "Mandatory parameter {0} missing";
        public const string PARAMETERS_HELP_START = "Parameters:";
        public const string PARAMETERS_HELP_FORMAT = "{Name}: {Description}\nUse: {Key}={FormatInstructions}";
        #endregion

        #region Logging message
        public const string CREATE_AOR_MUTANT_LOGGING = "Creating AOR Mutant....";
        public const string CREATE_LOR_MUTANT_LOGGING = "Creating LOR Mutant....";
        public const string CREATE_ROR_MUTANT_LOGGING = "Creating ROR Mutant....";
        public const string CREATE_AMC_MUTANT_LOGGING = "Creating AMC Mutant....";
        public const string HELP_INSTRUCTION_MESSAGE = "Invalid command. Please use --help for more information";
        public const string LOG_CONSOLE_NAME = "log console";
        public const string STARTING_BUILD_SOLUTION = "Starting Build solution..............";
        public const string STARTING_COPY_FILES = "Starting copy files...";
        public const string SOURCE_PATH_ERR_MESSAGE = "The path is not valid. It should contain \"Path\\Solutionname.sln\"";
        public const string BUILD_SOLUTION_ERR_MESSAGE = "Error when executing build solution: ";
        public const string MUTANT_OPERATOR_ERR_MESSAGE = "Mutant operators parameter is not correct. It should contain at least 1 mutant operator or list of mutant operators which are separated by ,";
        public const string SOURCE_FILE_NAME_ERR_MESSAGE = "Source file name must be an dll file. Example: ClassLibrary.dll";
        public const string TEST_FILE_NAME_ERR_MESSAGE = "Test file name must be an dll file. Example: ClassLibrary.Testing.dll";
        public const string SPECIFY_TARGET_FODLER_TO_COPY = "Error when specifying folders to copy ";
        public const string PROCESS_XCOPY_ERR_MSG = "Error when calling XCOPY: ";
        public const string DELETE_OLD_FILES_ERR_MSG = "Error when deleting old files: ";
        public const string GENERATING_MUTANTS_ERR_MSG = "Error when generating mutants: ";
        public const string RUNNING_MUTANT_TESTING_ERR_MSG = "Error when running mutant testing: ";
        public const string RUNNING_TEST_SUITE_AGAINST_MUTANT ="Running test suite against ";
        public const string TESTING_MUTANT_SUCC_MESSAGE = "Test mutant successfully !";
        public const string TOTAL_TEST_CASES = "Total test case:";
        public const string LINE_OF_STAR = "***********************************************";
        public const string TOTAL_MUTANTS = "Total Mutants:";
        public const string KILLED_MUTANTS = "Killed Mutants:";
        public const string MUTANT_SCORE = "Mutant Score:";
        public const string GENENERATING_REPORT_MSG = "Generating report...";
        public const string GENENERATING_REPORT_ERR_MSG = "Error when generating report...:";
        public const string GENENERATING_MUTANT_CODE_ERR_MSG = "Exception in Utils.GenerateMutantCodeDataTable method:";
        public const string GET_SOURCE_CODE_ERR_MSG = "Exception in Utils.GetSourceCode method:";
        public const string GET_LINE_OF_CODE_ERR_MSG = "Exception in Utils.GetLineOfCode method:";
        #endregion

        #region Help instruction
        public const string VERSION_KEY_WORD = "Version:";
        public const string USE_KEY_WORD = "Use:";
        public const string COMMAND_INSTRUCTION = "mutanttesting -solutiondir=<Path to the solution> -sourcefile=<DLL name of source code which is tested> -testfile=<DLL name of testing library> [-reportdir] [-unitesttype] [-buildsolution]";
        public const string SOURCE_DIR_INSTRUCTION = "-solutiondir" + "\t\t" + "The path to the solution. Example: -solutiondir=C:\\TestProject\\TestProject.sln";
        public const string REPORT_DIR_INSTRUCTION = "-reportdir" + "\t\t" + "Optional parameter - The path to the folder in which generated reports are saved. Example: -reportdir=C:\\TestProject\\ReportDir";
        public const string UNIT_TEST_TYPE_INSTRUCTION = "-unitesttype" + "\t\t" + "Optional parameter - The type of unit test.The current version of the tool supports NUnit,MSTest, XUnit. Example: -unittesttype=NUnit (by default) |XUnit|MSTest";
        public const string BUILD_SOLUTION_INSTRUCTION = "-buildsolution" + "\t\t"+ "Optional parameter - Build again the test solution or not = True|False. Example: -buildsolution=True";
        public const string VERBOSE_INSTRUCTION = "-verbose" + "\t\t" + "Optional parameter - Display output of external tools = True|False. Defualt is False. Example: -verbose=True";
        public const string MUTANT_OPERATOR_INSTRUCTION = "-operators" + "\t\t" + "Optional parameter - List of mutant operators which are applied. Mutant operators are separated by ,. Example: -operator=AMC,ABS,LOR";
        public const string SOLUTION_EXTENSION = ".sln";
        public const string SOURCE_FILE_NAME_INSTRUCTION = "-sourcefile" + "\t\t" + "DLL name of target source code. Example:-sourcefile=TestProject.dll";
        public const string TEST_FILE_NAME_INSTRUCTION = "-testfile" + "\t\t" + "DLL name of target tested code. Example:-testfile=TestProject.Testing.dll";
        public const string TEST_ADAPTOR_INSTRUCTION = "-testadaptor" + "\t\t" + "Optional parameter - Path to a test adaptor to use. Example:-testadaptor=C:\\SomeDirectory\\xunit.runner.visualstudio.testadapter.dll";
        public const string BATCH_SIZE_INSTRUCTION = "-batchsize" + "\t\t" + "Optional parameter - Maximum number of tests to run each time. Example:-batchsize=100";
        #endregion

        #region Report Template
        public const string REPORT_TEMPLATE_NAME = "report_template.html";
        public const string REPORT_INDIVIDUAL_TEMPLATE_NAME = "source_code_template.html";
        public const string INDEX_REPORT_NAME = "index.html";
        public const string REPORT_TEMPLATE_FOLDER = "template";
        public const string REPORT_KEY_WORD = "report";
        #endregion

        #region Key configuration
        public const string DEV_ENV_PATH = "DevEnvPath";
        #endregion
        #region Bracket symbol
        public const string OPEN_BRACKET_SYMBOL = "{";
        public const string CLOSE_BRACKET_SYMBOL = "}";

        #endregion
        #region Create Report module
        public const string CREATED_DATE_TAG ="{CREATED_DATE}";
        public const string NUMBER_OF_ASSEMBLIES_TAG = "{NUMBER_OF_ASSEMBLIES}";
        public const string NUMBER_OF_CLASSES_TAG ="{NUMBER_OF_CLASSES}";
        public const string MUTANT_OPERATORS_TAG = "{MUTANT_OPERATORS}";
        public const string TOTAL_MUTANT_TAG = "{TOTAL_MUTANT}";
        public const string KILLED_MUTANT_TAG = "{KILLED_MUTANT}";
        public const string LINE_OF_CODE_TAG = "{LINE OF CODE}";
        public const string NUMBER_OF_TEST_CASE_TAG ="{NUMBER_TEST_CASE}";
        public const string UNIT_TEST_TYPE_TAG ="{UNIT_TEST_TYPE}";
        public const string REPORT_FOLDER_TAG ="{REPORT_FOLDER}";
        public const string MUTATION_SCORE_TAG = "{MUTATION_SCORE}";
        public const string TESTING_MUTANT_DETAILS_TAG = "{TESTING_MUTANT_DETAILS}";
        public const string SOURCE_CODE_TAG ="{SOURCE_CODE}";
        public const string LIST_CLASS_NAME_TAG ="{LIST_CLASS_NAME}";
        #endregion
        #region Column DataTable
        public const string DIFFERENCE_COL_NAME ="Difference";
        public const string MUTANT_FILE_COL_NAME = "MutantFile";
        public const string CODE_COL_NAME = "Code";
        public const string LOC_COL_NAME ="LOC";
        public const string MUTANT_OPERATOR_COL_NAME = "MutantOperator";
        #endregion

        #region Others
        public const string DIFFERENCE_CONSTANT_VALUE = "1";
        public const string COMMA_CONSTANT_VALUE = ",";
        public const string SPACE_CONSTANT_VALUE = " ";
        #endregion region
    }
}
