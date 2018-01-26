using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LZ4;
using NUnit.Framework;

namespace Tanius.LZ4.Tests
{
    public class LZ4DataTests
    {
        [Test]
        public void ReadSample1()
        {
            var fileName = @"C:\Users\karl\Visual Studio\External Tanius\LZ4\Tanius.LZ4.Tests\Data\test1.pcap.lz4"; // Change based on file structure of local machine

            using (var fr = File.OpenRead(fileName))
            using (var zr = new LZ4Stream(fr, LZ4StreamMode.Decompress))
            using (var ms = new MemoryStream())
            {
                zr.CopyTo(ms);
            }
        }
        [Test]
        public void ReadSample2()
        {
            var fileName = @"C:\Users\karl\Visual Studio\External Tanius\LZ4\Tanius.LZ4.Tests\Data\test2.pcap.lz4"; // Change based on file structure of local machine

            using (var fr = File.OpenRead(fileName))
            using (var zr = new LZ4Stream(fr, LZ4StreamMode.Decompress))
            using (var ms = new MemoryStream())
            {
                zr.CopyTo(ms);
            }
        }

        [Test]
        public void ReadWriteSample()
        {
            byte[] buffer = new byte[10 * 1024 * 1024];
            var nRand = new Random(DateTime.Now.Millisecond);
            nRand.NextBytes(buffer);
            var mr = new MemoryStream(buffer);
            var ms = new MemoryStream();
            var zw = new LZ4Stream(ms, LZ4StreamMode.Compress);

            mr.CopyTo(zw);
            zw.Flush();

            ms.Position = 0;
            var zr = new LZ4Stream(ms, LZ4StreamMode.Decompress);
            var mz = new MemoryStream();
            zr.CopyTo(mz);

            var clonedBuffer = mz.ToArray();

            Assert.AreEqual(buffer.Length, clonedBuffer.Length);

            for (int ix = 0; ix < buffer.Length; ix++)
            {
                Assert.AreEqual(buffer[ix], clonedBuffer[ix]);
            }
        }
    }
}
