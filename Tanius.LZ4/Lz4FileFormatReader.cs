using System;
using System.IO;

namespace LZ4
{
    public class LZ4FileFormatReader
    {

        ///// <summary>Reads the variable length int. Work with assumption that value is in the stream
        ///// and throws exception if it isn't. If you want to check if value is in the stream
        ///// use <see cref="TryReadVarInt"/> instead.</summary>
        ///// <returns></returns>
        //ulong ReadVarInt(Stream stream)
        //{
        //    ulong result;
        //    if (!TryReadVarInt(stream, out result)) throw new EndOfStreamException();
        //    return result;
        //}
        ///// <summary>Tries to read variable length int.</summary>
        ///// <param name="result">The result.</param>
        ///// <returns><c>true</c> if integer has been read, <c>false</c> if end of stream has been
        ///// encountered. If end of stream has been encountered in the middle of value 
        ///// <see cref="EndOfStreamException"/> is thrown.</returns>
        //bool TryReadVarInt(Stream stream, out ulong result)
        //{
        //    var buffer = new byte[1];
        //    var count = 0;
        //    result = 0;

        //    while (true)
        //    {
        //        if (stream.Read(buffer, 0, 1) == 0)
        //        {
        //            if (count == 0) return false;
        //            throw new EndOfStreamException();
        //        }
        //        var b = buffer[0];
        //        result = result + ((ulong)(b & 0x7F) << count);
        //        count += 7;
        //        if ((b & 0x80) == 0 || count >= 64) break;
        //    }

        //    return true;
        //}

        public LZ4HeaderChunkInfo ReadChunkHeader(Stream stream)
        {
            var info = new LZ4HeaderChunkInfo(stream);
            return info;
        }

        public bool ReadFooter(object input) => false;
        public LZ4FileHeaderInfo ReadHeader(Stream stream)
        {
            var info = new LZ4FileHeaderInfo(stream);
            return info;
        }

        public void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy) { }

        public void Validate() { }


    }
}