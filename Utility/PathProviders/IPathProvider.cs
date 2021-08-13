namespace Utility.PathProviders
{
    public interface IPathProvider
    {
        string TemporaryOriginalCodeDirectory { get; }
        string MutantCodeDirectory { get; }
        string MainDirectory { get; }
        string CopyDirectory { get; }
        string ProjectDirectoy { get; }
        string TestFilepath { get; }
        string CopiedSourceFilepath { get; }
        string TemporarySourceFilepath { get; }
        string TestResultsFilepath { get; }
        string SolutionPath { get; }
        string TestFilename { get; }
        string SourceFilename { get; }
        string ReportDirectory { get; }
        string ReportJsonFilepath { get; }
        string BuildDirectory { get; }
        string DotnetPath { get; }

        string GetMutantFilepath(string mutantName);
        string GetMutantTestResultFilepath(string mutantName);
    }
}
