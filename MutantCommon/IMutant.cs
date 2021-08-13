using System.Threading.Tasks;

namespace MutantCommon
{
    public interface IMutant : IProgramVersion
    {
        IContext CodeContext { get; }
        string Filename { get; }
        int ID { get; }
        MutantModel Model { get; }
        MutationFamily MutantFamily { get; }
        Class MutatedClass { get; }
        IMutation Mutation { get; }
        new string Name { get; }
        TestResult TestResult { get; }
        TestResult Test(ITestFramework testFramework);
        Task<TestResult> TestAsync(ITestFramework testFramework);
    }
}