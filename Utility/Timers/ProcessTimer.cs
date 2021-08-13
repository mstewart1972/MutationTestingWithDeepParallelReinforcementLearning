using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MutantCommon;

namespace Utility.Timers
{
    public class ProcessTimer<T> : IProcessTimer<T>
    {
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly IChangeDetector<T> ChangeDectector;
        private bool started = false;
        private readonly object lockObject = new object();

        public ProcessTimer(IChangeDetector<T> changeDetector)
        {
            ChangeDectector = changeDetector;
        }

        public void CheckTimer(object obj, T x)
        {
            lock (lockObject)
            {
                if (!started &&  ChangeDectector.IsOn(x))
                {
                    started = true;
                    stopwatch.Start();
                }
                else if (started && ChangeDectector.IsOff(x))
                {
                    started = false;
                    stopwatch.Stop();
                }
            }
        }

        public Stopwatch GetStopwatch()
        {
            return stopwatch;
        }
    }
}
