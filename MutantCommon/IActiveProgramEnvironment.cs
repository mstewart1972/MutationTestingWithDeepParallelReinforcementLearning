using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantCommon
{
    public interface IActiveProgramEnvironment
    {
        void Load(IProgramVersion version);
    }
}
