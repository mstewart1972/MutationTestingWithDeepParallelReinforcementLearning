using System;

namespace Decompiler
{
    public interface IDllDecompiler
    {
        DecompiledClass DecompileClass(String filename, String fullTypeName);
    }
}
