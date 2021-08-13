using System.IO;

namespace Utility.PathProviders
{
    public class PathProvider : IPathProvider
    {
        private const string TEST_RESULT_FILENAME = "testResults.xml";
        private const string ORIGINAL_CODE_FOLDER = "OriginalCode";
        private const string MUTANT_CODE_FOLDER = "Mutants";
        private const string COPY_FOLDER = "CopyFiles";
        private const string JSON_REPORT_FILENAME = "report.json";
        private const string DOTNET_PATH = "dotnet";
        private const string REPORT_KEY_WORD = "report";

        private readonly string _mainDirectory;
        private readonly string _sourceFilename;
        private readonly string _solutionPath;
        private readonly string _testFilename;
        private readonly string _reportDirectory;

        public string TestFilename => _testFilename;
        public string TemporaryOriginalCodeDirectory => Path.Combine(MainDirectory, ORIGINAL_CODE_FOLDER);

        public string MutantCodeDirectory => Path.Combine(MainDirectory, MUTANT_CODE_FOLDER);

        public string MainDirectory => _mainDirectory;

        public string CopyDirectory => Path.Combine(MainDirectory, COPY_FOLDER);

        //public string ReportDirectory => _reportDirectory;
        public string ReportDirectory => Path.Combine(MainDirectory, REPORT_KEY_WORD);

        public string SourceFilename => _sourceFilename;

        public string SolutionPath => _solutionPath;

        public string ProjectDirectoy => _solutionPath.Substring(0, _solutionPath.LastIndexOf('\\'));

        public string TestFilepath => Path.Combine(CopyDirectory, _testFilename);

        public string CopiedSourceFilepath => Path.Combine(CopyDirectory, _sourceFilename);

        public string TemporarySourceFilepath => Path.Combine(TemporaryOriginalCodeDirectory, _sourceFilename);

        public string TestResultsFilepath => Path.Combine(CopyDirectory, TEST_RESULT_FILENAME);

        public string ReportJsonFilepath => Path.Combine(ReportDirectory, JSON_REPORT_FILENAME);

        public string DotnetPath => DOTNET_PATH;

        public string BuildDirectory => CopyDirectory;

        public PathProvider(string mainDirectory, string sourceFilename, string solutionPath, string testFilename, string reportDirectory)
        {
            _mainDirectory = mainDirectory;
            _solutionPath = solutionPath;
            _sourceFilename = sourceFilename;
            _testFilename = testFilename;
            _reportDirectory = reportDirectory;

        }

        public string GetMutantFilepath(string mutantName)
        {
            return Path.Combine(MutantCodeDirectory, mutantName + ".dll");
        }

        public string GetMutantTestResultFilepath(string mutantName)
        {
            return Path.Combine(Path.Combine(CopyDirectory, mutantName), mutantName + ".xml");
        }
    }
}
