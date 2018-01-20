using System;
using System.IO;

namespace LZ4
{
    public class LZ4HeaderChunkInfo
    {
        byte[] sizeBytes = new byte[4];

        public LZ4HeaderChunkInfo(Stream stream)
        {
            for (var i = 0; i < 4; i++)
            {
                var cByte = (byte)stream.ReadByte();
                sizeBytes[i] = cByte;
            }
        }

        public int ChunkChecksum { get; set; }
        public int ChunkSize => sizeBytes[0] + 256 * sizeBytes[1] + 256 * 256 * sizeBytes[2] + 256 * 256 * 256 * (sizeBytes[3] & 0b0111111);
        public bool IsCompressed => (sizeBytes[3] & 0b1000000) == 0;
    }
}