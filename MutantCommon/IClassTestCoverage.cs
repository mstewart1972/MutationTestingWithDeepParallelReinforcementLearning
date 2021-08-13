using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public interface IClassTestCoverage
    {
        ISet<Unittest> Coverage(Class originalClass);
        ISet<Unittest> Coverage(IEnumerable<Class> classes);
        ISet<Class> Covers(Unittest test);
        ISet<Class> AllTestedClasses();
        ISet<Unittest> Unittests();
        void Add(Class coveredClass, Unittest test);
    }
}
