using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Timers
{
    public interface IChangeDetector<T>
    {
        bool IsOn(T x);
        bool IsOff(T x);
    }
}
