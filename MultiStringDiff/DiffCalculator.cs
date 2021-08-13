using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decompiler;
using MutantCommon;

namespace MultiStringDiff
{
    public class DiffCalculator : IDiffCalculator
    {
        private readonly IDllDecompiler _decompiler;

        public DiffCalculator(IDllDecompiler decompiler)
        {
            _decompiler = decompiler;
        }


        public Dictionary<Class, IList<StringSectionModel>> Calculate(OriginalProgram originalProgram, IEnumerable<IMutant> mutants)
        {
            var originalCodeFile = originalProgram.SourceFileCopyPath;
            var differ = new Differ();

            var decompiledOriginalClasses = new Dictionary<Class, string>();
            var multiStringDiffs = new Dictionary<Class, IMultiStringDiff<IMutant>>();


            IMultiStringDiff<IMutant> multiStringDiff;

            Class mutatedClass;
            string originalClassCode;
            DecompiledClass mutantClass;

            foreach (var mutant in mutants)
            {
                mutatedClass = mutant.MutatedClass;
                if (!decompiledOriginalClasses.ContainsKey(mutatedClass))
                {
                    decompiledOriginalClasses[mutatedClass] = _decompiler.DecompileClass(originalCodeFile, mutatedClass.Name).code;
                }

                originalClassCode = decompiledOriginalClasses[mutatedClass];

                if (!multiStringDiffs.ContainsKey(mutatedClass))
                {
                    multiStringDiffs[mutatedClass] = new MultiStringDiff<IMutant>(originalClassCode, differ);
                }

                multiStringDiff = multiStringDiffs[mutatedClass];
                mutantClass = _decompiler.DecompileClass(mutant.Filename, mutatedClass.Name);
                multiStringDiff.AddString(mutant, mutantClass.code);
            }

            var result = new Dictionary<Class, IList<StringSectionModel>>();
            foreach (var keyValuePair in multiStringDiffs)
            {
                result[keyValuePair.Key] = keyValuePair.Value.GetModel(mutant => mutant.Name);
            }

            return result;
        }
    }
}
