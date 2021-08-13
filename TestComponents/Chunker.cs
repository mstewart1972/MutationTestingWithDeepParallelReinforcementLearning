using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestComponents
{
    public class Chunker : IChunker
    {
        private readonly uint _chunkSize;

        public Chunker(uint chunkSize)
        {
            if (chunkSize == 0)
            {
                throw new ArgumentException("Cannot have 0 chunksize");
            }
            _chunkSize = chunkSize;
        }

        public IList<IList<T>> Chunk<T>(IList<T> listToChunk)
        {
            var chunks = new List<IList<T>>();
            var chunk = new List<T>();
            foreach (var item in listToChunk)
            {
                chunk.Add(item);
                if (chunk.Count >= _chunkSize)
                {
                    chunks.Add(chunk);
                    chunk = new List<T>();
                }
            }
            if(chunk.Count > 0)
            {
                chunks.Add(chunk);
            }
            return chunks;
        }
    }
}
