using System;
using System.IO;

namespace LZ4
{
    public enum BlockMaximumSize : byte
    {
        Block64K = 4,
        Block256K = 5,
        Block1MB = 6,
        Block4MB = 7
    }

    public class LZ4FileHeaderInfo
    {
        byte frameDescriptor_BD;
        long frameDescriptor_ContentSize;

        byte frameDescriptor_FLG;
        byte frameDescriptor_HC;

        public LZ4FileHeaderInfo(Stream stream)
        {
            for (var i = 0; i < 4; i++)
            {
                var cByte = (byte)stream.ReadByte();
                if (cByte != MagicNumber[i]) throw new Exception("incorrect stream format");
            }
            frameDescriptor_FLG = (byte)stream.ReadByte();
            if (FrameDescriptor_FLG_Version != 64) throw new Exception("incompatible format version");
            frameDescriptor_BD = (byte)stream.ReadByte();
            if (FrameDescriptor_FLG_ContentSize)
            {
                throw new NotImplementedException();
            }
            frameDescriptor_HC = (byte)stream.ReadByte();
            //TODO add checksum test
        }

        byte FrameDescriptor_FLG_Version => (byte)(frameDescriptor_FLG & 0b11000000);

        byte[] MagicNumber => new byte[] { 0x04, 0x22, 0x4d, 0x18 };

        public BlockMaximumSize FrameDescriptor_BD_BlockMaxSize => (BlockMaximumSize)((frameDescriptor_BD & 0b01110000) >> 4);

        public bool FrameDescriptor_FLG_BChecksum => (frameDescriptor_FLG & 0b00010000) > 0;
        public bool FrameDescriptor_FLG_BIndependence => (frameDescriptor_FLG & 0b00100000) > 0;
        public bool FrameDescriptor_FLG_ContentChecksum => (frameDescriptor_FLG & 0b00000100) > 0;
        public bool FrameDescriptor_FLG_ContentSize => (frameDescriptor_FLG & 0b00001000) > 0;

    }
}