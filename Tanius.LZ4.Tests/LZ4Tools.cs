using LZ4;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Tanius.LZ4.Tests
{
    [TestFixture]
    public class LZ4Tools
    {
        protected static string GetLZ4ToolPath() => @"C:\msys64\mingw32\bin\lz4.exe";//TODO RK: this path is specific to my laptop

        protected static bool InvokeCommandLineTools(string lz4fileName, string contentFileName, string arguments)
        {
            var command = GetLZ4ToolPath();
            var args = $"{arguments} {lz4fileName} {contentFileName}";
            var process = new Process();
            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = args;
            process.Start();
            process.WaitForExit();
            return process.ExitCode > 0;
        }

        public static void CompressString(string fileName, string content)
        {
            var fs = new FileStream(fileName, FileMode.OpenOrCreate);
            var lzStream = new LZ4Stream(fs, LZ4StreamMode.Compress);
            var array = Encoding.ASCII.GetBytes(content);
            lzStream.Write(array, 0, array.Length);
            lzStream.Close();
            fs.Close();
        }
        public static bool CompressWithCommandLineTools(string lz4fileName, string contentFileName) => InvokeCommandLineTools(contentFileName, lz4fileName, "-f -9");

        public static string DecompressString(string filename)
        {
            var fs = new FileStream(filename, FileMode.Open);
            var lzStream = new LZ4Stream(fs, LZ4StreamMode.Decompress);
            var reader = new StreamReader(lzStream);
            var result = reader.ReadToEnd();

            lzStream.Close();
            fs.Close();
            return result;
        }
        public static bool DecompressWithCommandLineTools(string lz4fileName, string contentFileName) => InvokeCommandLineTools(lz4fileName, contentFileName, "-f -d");
    }
    }
