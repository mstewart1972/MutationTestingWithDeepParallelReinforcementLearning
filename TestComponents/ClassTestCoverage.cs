using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

namespace TestComponents
{
    public class ClassTestCoverage : IClassTestCoverage
    {
        private readonly IDictionary<Class, ISet<Unittest>> coverageOfClasses;
        private readonly IDictionary<Unittest, ISet<Class>> coverageOfTests;
        
        public ClassTestCoverage()
        {
            coverageOfClasses = new Dictionary<Class, ISet<Unittest>>();
            coverageOfTests = new Dictionary<Unittest, ISet<Class>>();
        }
        public void Add(Class coveredClass, Unittest test)
        {
            ISet<Unittest> tests;
            ISet<Class> classes;
            if(coverageOfClasses.TryGetValue(coveredClass, out tests))
            {
                tests.Add(test);
            }
            else
            {
                coverageOfClasses[coveredClass] = new HashSet<Unittest> { test };
            }

            if(coverageOfTests.TryGetValue(test, out classes))
            {
                classes.Add(coveredClass);
            }
            else
            {
                coverageOfTests[test] = new HashSet<Class> { coveredClass };
            }
        }

        public ISet<Class> AllTestedClasses()
        {
            return new HashSet<Class>(coverageOfClasses.Keys);
        }

        public ISet<Unittest> Coverage(Class originalClass)
        {
            ISet<Unittest> tests;
            if (coverageOfClasses.TryGetValue(originalClass, out tests))
            {
                return new HashSet<Unittest>(tests);
            }
            else
            {
                return new HashSet<Unittest>();
            }
        }

        public ISet<Unittest> Coverage(IEnumerable<Class> classes)
        {
            var tests = new HashSet<Unittest>();
            foreach(var coveredClass in classes)
            {
                tests.UnionWith(Coverage(coveredClass));
            }
            return tests;
        }

        public ISet<Class> Covers(Unittest test)
        {
            ISet<Class> classes;
            if (coverageOfTests.TryGetValue(test, out classes))
            {
                return new HashSet<Class>(classes);
            }
            else
            {
                return new HashSet<Class>();
            }
        }

        public ISet<Unittest> Unittests()
        {
            return new HashSet<Unittest>(coverageOfTests.Keys);
        }
    }
}
