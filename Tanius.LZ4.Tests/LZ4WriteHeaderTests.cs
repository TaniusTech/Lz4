using LZ4;
using NUnit.Framework;
using System;

namespace Tanius.LZ4.Tests
{
    [TestFixture]
    public class LZ4WriteHeaderTests : LZ4Tools
    {
        LZ4FileHeaderInfo info;
        LZ4HeaderChunkInfo chunkInfo;

        [SetUp]
        public void SetUp()
        {
            info = new LZ4FileHeaderInfo();
            chunkInfo = new LZ4HeaderChunkInfo();
        }
        [Test]
        public void TestSimple()
        {
            Assert.IsTrue(info != null);
        }

        [Test]
        public void TestBlockIndependence()
        {
            info.FrameDescriptor_FLG_BIndependence = true;
            Assert.IsTrue(info.FrameDescriptor_FLG_BIndependence);
        }

        [Test]
        public void TestBlockChunkChecksum()
        {
            info.FrameDescriptor_FLG_ContentChecksum = true;
            Assert.True(info.FrameDescriptor_FLG_ContentChecksum);
        }
        [Test]
        public void TestBlockChunkSize()
        {
            info.FrameDescriptor_FLG_ContentSize = true;
            Assert.IsTrue(info.FrameDescriptor_FLG_ContentSize);

            info.FrameDescriptor_FLG_ContentSize = false;
            Assert.IsFalse(info.FrameDescriptor_FLG_ContentSize);
        }

        [Test]
        public void TestBlockChecksum()
        {
            info.FrameDescriptor_FLG_BChecksum = true;
            Assert.IsTrue(info.FrameDescriptor_FLG_BChecksum);

            info.FrameDescriptor_FLG_BChecksum = false;
            Assert.IsFalse(info.FrameDescriptor_FLG_BChecksum);
        }
        [Test]
        public void TestBlockMaxSize()
        {
            info.FrameDescriptor_BD_BlockMaxSize = BlockMaximumSize.Block64K;
            Assert.AreEqual(BlockMaximumSize.Block64K, info.FrameDescriptor_BD_BlockMaxSize);
        }
        [Test]
        public void TestChunkSize()
        {
            chunkInfo.ChunkSize = 18;
            Assert.AreEqual(18, chunkInfo.ChunkSize);
        }
        [Test]
        public void TestChunkCompressed()
        {
            chunkInfo.IsCompressed = true;
            Assert.IsTrue(chunkInfo.IsCompressed);
            chunkInfo.IsCompressed = false;
            Assert.IsFalse(chunkInfo.IsCompressed);

        }
    }
}
