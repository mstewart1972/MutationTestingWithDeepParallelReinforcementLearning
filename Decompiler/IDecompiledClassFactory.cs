using System;

namespace Decompiler
{
    public interface IDecompiledClassFactory
    {
        DecompiledClass Create(String name, String code);
    }
}
