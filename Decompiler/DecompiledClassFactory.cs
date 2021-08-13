namespace Decompiler
{
    public class DecompiledClassFactory : IDecompiledClassFactory
    {
        public DecompiledClass Create(string name, string code)
        {
            return new DecompiledClass { name = name, code = code };
        }
    }
}
