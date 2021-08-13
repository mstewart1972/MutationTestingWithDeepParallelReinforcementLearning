using System.Threading.Tasks;

namespace ExternalProgramExecution
{
    public interface IExternalProgramExecutor
    {
        void Execute(bool showOutput);
        Task<int> ExecuteAsync(bool showOutput);
    }
}
