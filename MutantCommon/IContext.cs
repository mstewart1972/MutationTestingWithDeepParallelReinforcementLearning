using Mono.Cecil;

namespace MutantCommon
{
    public interface IContext
    {
        TypeDefinition Type { get; }
        ModuleDefinition Module { get; }
    }
}
