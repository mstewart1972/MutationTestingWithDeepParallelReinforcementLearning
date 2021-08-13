using Mono.Cecil;
using Mono.Cecil.Cil;
using MutantCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.PathProviders;

namespace TestComponents
{
    public class TestCoverageCalculator : ITestCoverageCalculator
    {
        private readonly DirectoryPath _buildDirectory;
        private readonly IClassNameFilter _filter;
        public const string MODULE_KEY_WORD = "< Module >";
        public const string CONSTRUCTOR_KEY_WORD = ".ctor";
        public const string F_ANONYMOUS_TYPE = "f__AnonymousType";
        public const string TEST_ATTRIBUTE = "TestAttribute";
        public const string TEST_METHOD_ATTRIBUTE = "TestMethodAttribute";
        public const string FACT_METHOD_ATTRIBUTE = "FactAttribute";
        public const string DLL_EXTENSION = ".dll";
        public const string EXE_EXTENSION = ".exe";

        public TestCoverageCalculator(DirectoryPath buildirectory, IClassNameFilter filter)
        {
            _buildDirectory = buildirectory;
            _filter = filter;
        }

        private ClassTestCoverage TestClassCoverage(ClassTestCoverage classCoverage ,string filename)
        {
            using (var _module = ModuleDefinition.ReadModule(filename))
            {

                foreach (TypeDefinition type in _module.Types)
                {
                    if (!isModule(type) && !isAnonymous(type))
                    {
                        foreach (MethodDefinition method in type.Methods)
                        {
                            if (isTest(method))
                            {
                                addAllReferencedTypesToCoverage(classCoverage, method);
                            }

                        }
                    }
                }
            }
            return classCoverage;
        }

        public IClassTestCoverage ClassTestCoverage()
        {
            var coverage = new ClassTestCoverage();
            var files = _buildDirectory.GetFiles();
            foreach (var file in files)
            {
                if (isCompiledCodeFile(file.Path))
                {
                    TestClassCoverage(coverage, file.Path);
                }
            }
            return coverage;
        }

        private bool isCompiledCodeFile(string filepath)
        {
            var extension = Path.GetExtension(filepath);
            return (String.Compare(extension, EXE_EXTENSION) == 0 || String.Compare(extension, DLL_EXTENSION) == 0);
        }

        private bool isModule(TypeDefinition type)
        {
            return type.Name == MODULE_KEY_WORD;
        }

        private bool isAnonymous(TypeDefinition type)
        {
            return type.FullName.Contains(F_ANONYMOUS_TYPE);
        }

        private void addAllReferencedTypesToCoverage(ClassTestCoverage classCoverage, MethodDefinition method)
        {
            Unittest newTest = new Unittest { Name = String.Concat(method.DeclaringType.Name, ".", method.Name) };
            foreach (var instruction in method.Body.Instructions)
            {

                if (isNewKeyword(instruction))
                {
                    addNewCreatedClassToCoverage(classCoverage, newTest, instruction);

                }
                else if (isMethodCall(instruction))
                {
                    addReturnedClassToCoverage(classCoverage, newTest, instruction);
                    addCallingClassToCoverage(classCoverage, newTest, instruction);

                }
            }
        }

        private void addReturnedClassToCoverage(ClassTestCoverage classCoverage, Unittest newTest, Instruction instruction)
        {
            Class newClass;
            string returnedType = getReturnTypeName(instruction);
            if (_filter.Accept(returnedType))
            {
                newClass = new Class { Name = returnedType };
                classCoverage.Add(newClass, newTest);
            }
        }

        private void addCallingClassToCoverage(ClassTestCoverage classCoverage, Unittest newTest, Instruction instruction)
        {
            Class newClass;
            string callingType = getCallingTypeName(instruction);
            if (_filter.Accept(callingType))
            {
                newClass = new Class { Name = callingType };
                classCoverage.Add(newClass, newTest);
            }
        }

        private void addNewCreatedClassToCoverage(ClassTestCoverage classCoverage, Unittest newTest, Instruction instruction)
        {
            string declaringType = getDeclaringTypeName(instruction);
            if (_filter.Accept(declaringType))
            {
                var newClass = new Class { Name = declaringType };
                classCoverage.Add(newClass, newTest);
            }
        }

        private bool isTest(MethodDefinition method)
        {
            return method.Name != CONSTRUCTOR_KEY_WORD && method.CustomAttributes.Capacity > 0
                                 //NUNIT TEST
                                 && (method.CustomAttributes[0].AttributeType.FullName.Contains(TEST_ATTRIBUTE) ||
                                 //MSTEST
                                 method.CustomAttributes[0].AttributeType.FullName.Contains(TEST_METHOD_ATTRIBUTE)
                                 ||
                                 //XUNIT
                                 method.CustomAttributes[0].AttributeType.FullName.Contains(FACT_METHOD_ATTRIBUTE));
        }

        private bool isNewKeyword(Instruction instruction)
        {
            return instruction.OpCode == OpCodes.Newobj;
        }

        private bool isMethodCall(Instruction instruction)
        {
            return instruction.OpCode == OpCodes.Call;
        }

        private string getReturnTypeName(Instruction instruction)
        {
            return ((Mono.Cecil.MethodReference)instruction.Operand).ReturnType.FullName;
        }

        private string getCallingTypeName(Instruction instruction)
        {
            return ((Mono.Cecil.MethodReference)instruction.Operand).DeclaringType.FullName;
        }

        private string getDeclaringTypeName(Instruction instruction)
        {
            return ((Mono.Cecil.MemberReference)instruction.Operand).DeclaringType.FullName;
        }
    }
}
