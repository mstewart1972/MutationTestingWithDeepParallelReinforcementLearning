
using System;
using System.Collections.Generic;

namespace MutantCommon
{
    public class Mutant : TestableProgramVersion, IMutant
    {
        public Mutant(IMutation mutation, String filename)
        {
            Mutation = mutation;
            Filename = filename;
            MutatedClass = new Class { Name = Mutation.Context.Type.FullName };
        }

        public int ID => Mutation.ID;
        public IMutation Mutation { get; }
        public String Filename { get; }
        public IContext CodeContext => Mutation.Context;
        public Class MutatedClass { get; private set; }
        public MutationFamily MutantFamily => Mutation.Family;
        public override string Name => Mutation.Name;

        public MutantModel Model { get {
                return new MutantModel
                {
                    ID = ID,
                    Class = MutatedClass,
                    Family = MutantFamily,
                    Filename = Filename,
                    Name = Name,
                    Purpose = Mutation.Purpose,
                    TestResult = TestResult
                };
            }
        }

        public override string SourceFileCopyPath => Filename;

        public override ISet<Unittest> TestsToRun(IClassTestCoverage coverage)
        {
            return coverage.Coverage(MutatedClass);
        }
    }
}
