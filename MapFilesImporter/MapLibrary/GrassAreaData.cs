using System.Collections.Generic;
using System.IO;

namespace MapFilesImporter.Struct
{
    public class GrassAreaData
    {
        public List<Block> Usings = new List<Block>();
        public OffsetBlock OffsetBlock;

        public int GrassId;
        public float X;
        public float Y;
        public float Z;
        public float Unk1;
        public int XSize;
        public int YSize;
        public int ZoneId;
        public int Unk2;

        public GrassAreaData(BinaryReader ecbsd, OffsetBlock block, int version)
        {
            OffsetBlock = block;

            ecbsd.BaseStream.Position = block.Offset;
            GrassId = ecbsd.ReadInt32();
            X = ecbsd.ReadSingle();
            Y = ecbsd.ReadSingle();
            Z = ecbsd.ReadSingle();
            Unk1 = ecbsd.ReadSingle();
            XSize = ecbsd.ReadInt32();
            YSize = ecbsd.ReadInt32();
            ZoneId = ecbsd.ReadInt32();
            Unk2 = ecbsd.ReadInt32();
        }

        public void Write(BinaryWriter ecbsd, int version)
        {
            OffsetBlock.Offset = (int) ecbsd.BaseStream.Position;
            ecbsd.Write(GrassId);
            ecbsd.Write(X);
            ecbsd.Write(Y);
            ecbsd.Write(Z);
            ecbsd.Write(Unk1);
            ecbsd.Write(XSize);
            ecbsd.Write(YSize);
            ecbsd.Write(ZoneId);
            ecbsd.Write(Unk2);
        }
    }
}
