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

        public LZ4FileHeaderInfo()
        {

        }

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
        }

        public byte FrameDescriptor_FLG_Version {
            get {
                return (byte)(frameDescriptor_FLG & 0b1100_0000);
            }
            set => frameDescriptor_FLG |= (byte)(value & 0b1100_0000);
        }

        byte[] MagicNumber => new byte[] { 0x04, 0x22, 0x4d, 0x18 };

        public static void WriteHeader(Stream stream, LZ4FileHeaderInfo headerInfo)
        {
            for (var i = 0; i < 4; i++)
                stream.WriteByte(headerInfo.MagicNumber[i]);
            stream.WriteByte(headerInfo.frameDescriptor_FLG);
            stream.WriteByte(headerInfo.frameDescriptor_BD);
            if (headerInfo.FrameDescriptor_FLG_ContentSize)
            {
                throw new NotImplementedException();
            }
            stream.WriteByte(headerInfo.frameDescriptor_HC);
        }

        public BlockMaximumSize FrameDescriptor_BD_BlockMaxSize {
            get => (BlockMaximumSize)((frameDescriptor_BD & 0b0111_0000) >> 4);
            set => frameDescriptor_BD = (byte)((int)value << 4);
        }

        public bool FrameDescriptor_FLG_BChecksum {
            get => (frameDescriptor_FLG & 0b0001_0000) > 0;
            set => frameDescriptor_FLG = value ? frameDescriptor_FLG |= 0b0001_0000 : frameDescriptor_FLG &= 0b1110_1111;
        }

        public bool FrameDescriptor_FLG_BIndependence {
            get => (frameDescriptor_FLG & 0b0010_0000) > 0;
            set => frameDescriptor_FLG = value ? frameDescriptor_FLG |= 0b0010_0000 : frameDescriptor_FLG &= 0b1101_1111;
        }

        public bool FrameDescriptor_FLG_ContentChecksum {
            get => (frameDescriptor_FLG & 0b0000_0100) > 0;
            set => frameDescriptor_FLG = value ? frameDescriptor_FLG |= 0b0000_0100 : frameDescriptor_FLG &= 0b1111_1011;
        }

        public bool FrameDescriptor_FLG_ContentSize {
            get => (frameDescriptor_FLG & 0b0000_1000) > 0;
            set => frameDescriptor_FLG = value ? frameDescriptor_FLG |= 0b0000_1000 : frameDescriptor_FLG &= 0b1111_0111;
        }
    }
}