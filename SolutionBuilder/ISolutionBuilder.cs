using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionBuilder
{
    public interface ISolutionBuilder
    {
        void Build(bool showOutput);
        Task BuildAsync(bool showOutput);
    }
}
