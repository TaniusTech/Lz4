using NUnit.Framework;
using System;
using System.IO;

namespace Tanius.LZ4.Tests
{
    [TestFixture]
    public class LZ4ToolsTests : LZ4Tools
    {

        [Test]
        public void TestDecompressStreamWithLZ4Tools()
        {
            var fileName = "TestDecompressStreamWithLZ4Tools.lz4";
            var content = "test test test testtesttesttesttesttesttesttesttest test";
            CompressString(fileName, content);
            var resultFileName = "TestDecompressStreamWithLZ4Tools.txt";
            Assert.IsFalse(DecompressWithCommandLineTools(fileName, resultFileName));
            var decompressedResult = File.ReadAllText(resultFileName);
            Assert.AreEqual(content, decompressedResult);
        }
        [Test]
        public void TestCompressStreamWithLZ4ToolsAndLoadWithLZ4Stream()
        {
            var fileName = "TestCompressStreamWithLZ4ToolsAndLoadWithLZ4Stream.txt";
            var fileNameLZ4 = "TestCompressStreamWithLZ4ToolsAndLoadWithLZ4Stream.lz4";

            var content = "test test test testtesttesttesttesttesttesttesttest test";
            File.WriteAllText(fileName, content);
            CompressWithCommandLineTools(fileNameLZ4, fileName);

            string result =DecompressString(fileNameLZ4);
            Assert.AreEqual(content, result);
        }
        [Test]
        public void TestCompressStreamWithLZ4ToolsAndLoadWithLZ4Stream2()
        {
            var fileName = "TestCompressStreamWithLZ4ToolsAndLoadWithLZ4Stream2.txt";
            var fileNameLZ4 = "TestCompressStreamWithLZ4ToolsAndLoadWithLZ4Stream2.lz4";

            var content = "test test test testtesttesttesttesttesttesttesttest test ";
            File.WriteAllText(fileName, content);
            CompressWithCommandLineTools(fileNameLZ4, fileName);

            string result = DecompressString(fileNameLZ4);
            Assert.AreEqual(content, result);
        }
        [Test]
        public void TestCompressStream2()
        {
            var fileNameLZ4 = "TestCompressStream2.lz4";

            var content = "test test test testtesttesttesttesttesttesttesttest test ";
            CompressString(fileNameLZ4, content);
        }

    }
}
