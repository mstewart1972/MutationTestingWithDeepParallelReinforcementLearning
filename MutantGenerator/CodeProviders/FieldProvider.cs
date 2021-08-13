using Mono.Cecil;
using MutantGeneration.CodeContexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MutantGeneration.CodeProviders
{
    public class FieldProvider : Provider<FieldContext>
    {
        private readonly Func<TypeDefinition, bool> _typeFilterPredicate;
        public FieldProvider(Func<TypeDefinition, bool> typeFilterPredicate)
        {
            _typeFilterPredicate = typeFilterPredicate;
        }
        public override IEnumerable<FieldContext> GetProvided(ModuleDefinition module)
        {
            var fields = module.Types
                .Where(_typeFilterPredicate)
                .SelectMany(type => type.Fields)
                .Select(field => new FieldContext
                {
                    Field = field,
                    Type = field.DeclaringType,
                    Module = field.Module
                });

            return fields;
        }
    }
}
