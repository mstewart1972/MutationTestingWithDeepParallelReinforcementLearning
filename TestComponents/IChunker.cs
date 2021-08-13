using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestComponents
{
    public interface IChunker
    {
        IList<IList<T>> Chunk<T>(IList<T> listToChunk);
    }
}
