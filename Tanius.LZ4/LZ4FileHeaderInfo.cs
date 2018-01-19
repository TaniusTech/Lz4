using System;

namespace LZ4
{
    public class LZ4FileHeaderInfo
    {
        public byte[] MagicNumber { get { return new byte[] { 0x18, 0x4d, 0x22, 0x04 }; } }
        public byte FrameDescriptor_FLG { get { return 0; } }
        public byte FrameDescriptor_BD { get { return 0; } }
        public int FrameDescriptor_ContentSize { get { return 0; } }
        public byte FrameDescriptor_HC { get { return 0; } }

        public byte FrameDescriptor_FLG_Version { get { return 0; } }
        public byte FrameDescriptor_FLG_BIndep { get { return 0; } }

        public byte FrameDescriptor_FLG_BChecksum { get { return 0; } }

        public byte FrameDescriptor_FLG_ChunkSize { get { return 0; } }
        public byte FrameDescriptor_FLG_ChunkChecksum { get { return 0; } }

        public byte FrameDescriptor_BD_BlockMaxSize { get { return 0; } }

    }
}