using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantGeneration.ID
{
    public class IdGenerator : IIdGenerator
    {
        private int nextID = 0;

        public int GenerateID()
        {
            return nextID++;
        }

        private static IdGenerator staticGenerator = new IdGenerator();

        public static IdGenerator Get()
        {
            return staticGenerator;
        }
    }
}
