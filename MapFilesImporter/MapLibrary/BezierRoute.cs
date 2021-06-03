using System.Collections.Generic;
using System.IO;

namespace MapFilesImporter.Struct
{
    public class BezierRoute
    {
        public List<Block> Usings = new List<Block>();
        public OffsetBlock OffsetBlock;

        public int Unk0;
        public int Unk1;
        public int Unk2;
        public int Unk3;
        public int Unk4;
        public int unk5;
        public int count1;
        public int count2;
        public List<BezierRouteBlock1> SubBlocks1 = new List<BezierRouteBlock1>();//[Count1];
        public List<BezierRouteBlock2> SubBlocks2 = new List<BezierRouteBlock2>();//[Count2];

        public BezierRoute(BinaryReader ecbsd, OffsetBlock Block, int version, int WldVersion)
        {
            OffsetBlock = Block;

            ecbsd.BaseStream.Position = Block.Offset;
            Unk0 = ecbsd.ReadInt32();
            Unk1 = ecbsd.ReadInt32();
            Unk2 = ecbsd.ReadInt32();
            Unk3 = ecbsd.ReadInt32();
            Unk4 = ecbsd.ReadInt32();
            if (WldVersion == 13 || WldVersion ==14)
            {
                unk5 = ecbsd.ReadInt32();
            }
            count1 = ecbsd.ReadInt32();
            for (var i = 0; i < count1; i++)
                SubBlocks1.Add(new BezierRouteBlock1(ecbsd, version));
            count2 = ecbsd.ReadInt32();
            for (var i = 0; i < count2; i++)
                SubBlocks2.Add(new BezierRouteBlock2(ecbsd, version));
        }

        public void Write(BinaryWriter ecbsd, int version)
        {
            OffsetBlock.Offset = (int)ecbsd.BaseStream.Position;
            ecbsd.Write(Unk0);
            ecbsd.Write(Unk1);
            ecbsd.Write(Unk2);
            ecbsd.Write(Unk3);
            ecbsd.Write(Unk4);
            ecbsd.Write(SubBlocks1.Count);
            for (var i = 0; i < SubBlocks1.Count; i++)
                SubBlocks1[i].Save(ecbsd, version);
            ecbsd.Write(SubBlocks2.Count);
            for (var i = 0; i < SubBlocks2.Count; i++)
                SubBlocks2[i].Write(ecbsd, version);
        }
    }

    public class BezierRouteBlock1
    {
        public float X;
        public float Y;
        public float Z;
        public float Ox;
        public float Oy;
        public float Oz;

        public BezierRouteBlock1(BinaryReader br, int version)
        {
            X = br.ReadSingle();
            Y = br.ReadSingle();
            Z = br.ReadSingle();
            Ox = br.ReadSingle();
            Oy = br.ReadSingle();
            Oz = br.ReadSingle();
        }

        public void Save(BinaryWriter bw, int version)
        {
            bw.Write(X);
            bw.Write(Y);
            bw.Write(Z);
            bw.Write(Ox);
            bw.Write(Oy);
            bw.Write(Oz);
        }
    }
    public class BezierRouteBlock2
    {
        public float XFrom;
        public float YFrom;
        public float ZFrom;
        public float XTo;
        public float YTo;
        public float ZTo;
        public int IndexFrom;
        public int IndexTo;
        public float Size;

        public BezierRouteBlock2(BinaryReader br, int version)
        {
            XFrom = br.ReadSingle();
            YFrom = br.ReadSingle();
            ZFrom = br.ReadSingle();
            XTo = br.ReadSingle();
            YTo = br.ReadSingle();
            ZTo = br.ReadSingle();
            IndexFrom = br.ReadInt32();
            IndexTo = br.ReadInt32();
            Size = br.ReadSingle();
        }

        public void Write(BinaryWriter bw, int version)
        {
            bw.Write(XFrom);
            bw.Write(YFrom);
            bw.Write(ZFrom);
            bw.Write(XTo);
            bw.Write(YTo);
            bw.Write(ZTo);
            bw.Write(IndexFrom);
            bw.Write(IndexTo);
            bw.Write(Size);
        }
    }
}
