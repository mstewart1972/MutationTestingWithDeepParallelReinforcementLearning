using Mono.Cecil;
using MutantCommon;

namespace MutantGeneration.CodeContexts
{
    public class FieldContext:IContext
    {
        public FieldDefinition Field { get; set; }
        public TypeDefinition Type { get; set; }
        public ModuleDefinition Module { get; set; }
    }
}
