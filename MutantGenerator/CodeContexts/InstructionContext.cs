using Mono.Cecil;
using Mono.Cecil.Cil;
using MutantCommon;

namespace MutantGeneration.CodeContexts
{
    public class InstructionContext:IContext
    {
        public Instruction Instruction { get; set; }
        public ILProcessor ILProcessor { get; set; }
        public MethodBody Body { get; set; }
        public MethodDefinition Method { get; set; }
        public TypeDefinition Type { get; set; }
        public ModuleDefinition Module { get; set; }
    }
}
