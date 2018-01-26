
using System;

namespace LZ4
{
    public class LZ4FileFormatWriter
    {

        public byte[] GetFooter() => null;
        public byte[] GetHeader() => null;

        public void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy) { }
    }
}
