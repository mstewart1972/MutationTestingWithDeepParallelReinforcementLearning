using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using MutantGeneration.CodeContexts;

namespace MutantGeneration.CodeProviders
{

    public class InstructionProvider : Provider<InstructionContext>
    {
        private readonly Func<TypeDefinition, bool> _typeFilterPredicate;
        private readonly Func<MethodDefinition, bool> _methodFilterPredicate;
        public InstructionProvider(Func<TypeDefinition,bool> typeFilterPredicate, Func<MethodDefinition, bool> methodFilterPredicate)
        {
            _typeFilterPredicate = typeFilterPredicate;
            _methodFilterPredicate = methodFilterPredicate;
        }

        public override IEnumerable<InstructionContext> GetProvided(ModuleDefinition module)
        {

            var instructions = module.Types
                .Where(_typeFilterPredicate)
                .SelectMany(type => type.Methods)
                .Where(_methodFilterPredicate)
                .Select(method => method.Body)
                .SelectMany(body => body.Instructions.Select(
                instruction => new InstructionContext
                {
                    Instruction = instruction,
                    ILProcessor = body.GetILProcessor(),
                    Body = body,
                    Method = body.Method,
                    Type = body.Method.DeclaringType,
                    Module = body.Method.Module,
                }
                ));



            return instructions;
        }
    }
}
