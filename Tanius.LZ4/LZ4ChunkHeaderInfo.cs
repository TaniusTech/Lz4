using System;

namespace LZ4
{
    public class LZ4ChunkHeaderInfo
    {
        public int ChunkSize { get; set; }
        public bool IsCompressed { get; set; }
        public int ChunkChecksum { get; set; }
    }
}