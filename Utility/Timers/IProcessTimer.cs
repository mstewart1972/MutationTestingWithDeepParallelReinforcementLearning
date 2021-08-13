using MutantCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Timers
{
    public interface IProcessTimer<T>
    {
        void CheckTimer(object obj, T x);
        Stopwatch GetStopwatch();
    }
}
