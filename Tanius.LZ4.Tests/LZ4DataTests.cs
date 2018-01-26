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
    }
}
