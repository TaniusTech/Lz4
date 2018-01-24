using System;
using System.IO;

namespace LZ4
{
    public class LZ4HeaderChunkInfo
    {
        byte[] sizeBytes;

        public LZ4HeaderChunkInfo() => sizeBytes = new byte[4];

        public LZ4HeaderChunkInfo(Stream stream): this()
        {
            for (var i = 0; i < 4; i++)
                sizeBytes[i] = (byte)stream.ReadByte();
        }

        public int ChunkChecksum { get; set; }
        public int ChunkSize {
            get => sizeBytes[0] + 256 * sizeBytes[1] + 256 * 256 * sizeBytes[2] + 256 * 256 * 256 * (sizeBytes[3] & 0b0111111);
            set {
                var cByte = BitConverter.GetBytes(value);
                if (!BitConverter.IsLittleEndian) Array.Reverse(cByte);
                for (var i = 0; i < 4; i++) sizeBytes[i] = cByte[i];
            }
        }

        public bool IsCompressed {
            get => (sizeBytes[3] & 0b1000_0000) == 0;
            set {
                if (value)
                {
                    sizeBytes[3] = sizeBytes[3] &= 0b0111_1111;
                }
                else
                {
                    sizeBytes[3] = sizeBytes[3] |= 0b1000_0000;
                }
            }
        }
        public static void WriteHeader(Stream stream, LZ4HeaderChunkInfo chunkInfo)
        {
            for (var i = 0; i < 4; i++)
                stream.WriteByte(chunkInfo.sizeBytes[i]);
        }
    }
}