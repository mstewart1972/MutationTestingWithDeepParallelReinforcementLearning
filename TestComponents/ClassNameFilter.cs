using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestComponents
{
    public class ClassNameFilter:IClassNameFilter
    {
        private readonly IEnumerable<string> _wordsToAvoid;

        public ClassNameFilter(IEnumerable<string> wordsToAvoid)
        {
            _wordsToAvoid = wordsToAvoid;
        }

        public bool Accept(string className)
        {
            foreach(var wordToAvoid in _wordsToAvoid)
            {
                if (className.Contains(wordToAvoid))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
