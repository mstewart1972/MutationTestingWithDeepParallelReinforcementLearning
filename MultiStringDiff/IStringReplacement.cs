using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStringDiff
{
    public interface IStringReplacement
    {
        int Length { get; }
        int Position { get; }
        string NewString { get; }
        string OldString { get; }
    }
}
