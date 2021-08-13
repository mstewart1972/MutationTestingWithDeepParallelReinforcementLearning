using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestComponents;
using Xunit;

namespace Test.TestComponents
{
    public class TestChunker
    {
        [Fact]
        public void TestChunkerChunksWithEmptyList()
        {
            var emptyList = new List<int>();
            var chunker = new Chunker(1);
            Assert.Empty(chunker.Chunk<int>(emptyList));
        }

        [Fact]
        public void TestChunkerChunksWith0ChunksizeThrows()
        {
            Assert.ThrowsAny<Exception>(() => new Chunker(0));
        }

        [Fact]
        public void TestChunkerWithExactlyEnoughFor1ChunkProduces1Chunk()
        {
            var list = new List<int> { 1,2,3};
            var chunker = new Chunker(3);
            Assert.Collection(chunker.Chunk<int>(list),
                one => Assert.Collection(one,
                    oneOne => Assert.Equal(1, oneOne),
                    oneTwo => Assert.Equal(2, oneTwo),
                    oneThree => Assert.Equal(3,oneThree)
                ));
        }

        [Fact]
        public void TestChunkerWithLessThanEnoughFor1ChunkProduces1Chunk()
        {
            var list = new List<int> { 1, 2, 3 };
            var chunker = new Chunker(4);
            Assert.Collection(chunker.Chunk<int>(list),
                one => Assert.Collection(one,
                    oneOne => Assert.Equal(1, oneOne),
                    oneTwo => Assert.Equal(2, oneTwo),
                    oneThree => Assert.Equal(3, oneThree)
                ));
        }

        [Fact]
        public void TestChunkerWithMoreThanEnoughFor1ChunkProducesMultipleChunks()
        {
            var list = new List<int> { 1, 2, 3 };
            var chunker = new Chunker(2);
            Assert.Collection(chunker.Chunk<int>(list),
                one => Assert.Collection(one,
                    oneOne => Assert.Equal(1, oneOne),
                    oneTwo => Assert.Equal(2, oneTwo)),
                two => Assert.Collection(two,
                    twoOne => Assert.Equal(3, twoOne)
                ));
        }

    }
}
