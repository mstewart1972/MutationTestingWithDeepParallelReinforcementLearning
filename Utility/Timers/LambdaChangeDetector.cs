using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Timers
{
    public class LambdaChangeDetector<T> : IChangeDetector<T>
    {

        private readonly Func<T, bool> _isOn;
        public LambdaChangeDetector(Func<T, bool> isOn)
        {
            _isOn = isOn;
        }

        public bool IsOff(T x)
        {
            return !_isOn(x);
        }

        public bool IsOn(T x)
        {
            return _isOn(x);
        }
    }
}
