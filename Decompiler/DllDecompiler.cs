using System;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using Mono.Cecil;

namespace Decompiler
{
    public class DllDecompiler: IDllDecompiler
    {
        private readonly IDecompiledClassFactory decompiledClassFactory;

        public DllDecompiler(IDecompiledClassFactory classFactory)
        {
            decompiledClassFactory = classFactory;
        }

        public DecompiledClass DecompileClass(String filepath, String fullTypeName)
        {
            CSharpDecompiler decompiler = new CSharpDecompiler(filepath, new DecompilerSettings());

            string decompiledCode = null;
            using (var _module = ModuleDefinition.ReadModule(filepath))
            {
                foreach (TypeDefinition type in _module.Types)
                {
                    if(String.Compare(type.FullName, fullTypeName)==0)
                    {
                        decompiledCode = decompiler.DecompileAsString(type);
                    }
                }
            }
            if (String.IsNullOrEmpty(decompiledCode))
            {
                throw new Exception($"No type matching {fullTypeName} to decompile" );
            }

            return decompiledClassFactory.Create(fullTypeName, decompiledCode);

        }
    }
}
