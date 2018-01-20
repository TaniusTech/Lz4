using LZ4;
using NUnit.Framework;
using System;
using System.IO;

namespace Tanius.LZ4.Tests
{
    [TestFixture]
    public class LZ4ReadHeaderTests : LZ4Tools
    {
        const string STR_Lz4 = "TestCompressStreamWithLZ4ToolsAndLoadWithLZ4Stream.lz4";
        LZ4FileHeaderInfo info;
        LZ4HeaderChunkInfo chunkInfo;

        [SetUp]
        public void SetUp()
        {
            var fileName = "TestCompressStreamWithLZ4ToolsAndLoadWithLZ4Stream.txt";
            var fileNameLZ4 = STR_Lz4;
            var content = "test test test testtesttesttesttesttesttesttesttest test";
            File.WriteAllText(fileName, content);
            CompressWithCommandLineTools(fileNameLZ4, fileName);
            using (var fs = new FileStream(STR_Lz4, FileMode.Open))
            {
                LZ4FileFormatReader reader = new LZ4FileFormatReader();
                info = reader.ReadHeader(fs);
                chunkInfo = reader.ReadChunkHeader(fs);
            }
        }

        [Test]
        public void TestSimple()
        {
            Assert.IsTrue(info!= null);            
        }

        [Test]
        public void TestBlockIndependance()
        {
            Assert.IsTrue(info.FrameDescriptor_FLG_BIndependence);
        }

        [Test]
        public void TestBlockChunkChecksum()
        {
            Assert.True(info.FrameDescriptor_FLG_ContentChecksum);
        }
        [Test]
        public void TestBlockChunkSize()
        {
            Assert.IsFalse(info.FrameDescriptor_FLG_ContentSize);
        }
        [Test]
        public void TestBlockChecksum()
        {
            Assert.IsFalse(info.FrameDescriptor_FLG_BChecksum);
        }
        [Test]
        public void TestBlockMaxSize()
        {
            Assert.AreEqual(BlockMaximumSize.Block64K, info.FrameDescriptor_BD_BlockMaxSize);
        }
        [Test]
        public void TestChunkSize()
        {
            Assert.AreEqual(18, chunkInfo.ChunkSize);
        }
        [Test]
        public void TestChunkcompressed()
        {
            Assert.IsTrue(chunkInfo.IsCompressed);
        }

    }
}
