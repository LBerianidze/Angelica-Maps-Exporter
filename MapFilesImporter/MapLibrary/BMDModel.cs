using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MapFilesImporter.Struct
{
    public class BmdModel
    {
        public List<Block> Usings = new List<Block>();
        public OffsetBlock OffsetBlock;

        public float X;
        public float Y;
        public float Z;
        public float Unk0;
        public float Unk1;
        public float Unk2;
        public float Unk3;
        public float Unk4;
        public float Unk5;
        public int Unk6;
        public string ModelPath;

        public BmdModel(BinaryReader ecbsd, OffsetBlock block, int version)
        {
            OffsetBlock = block;
            ecbsd.BaseStream.Position = block.Offset;

            X = ecbsd.ReadSingle();
            Y = ecbsd.ReadSingle();
            Z = ecbsd.ReadSingle();
            Unk0 = ecbsd.ReadSingle();
            Unk1 = ecbsd.ReadSingle();
            Unk2 = ecbsd.ReadSingle();
            Unk3 = ecbsd.ReadSingle();
            Unk4 = ecbsd.ReadSingle();
            Unk5 = ecbsd.ReadSingle();
            Unk6 = ecbsd.ReadInt32();
            ModelPath = Encoding.GetEncoding("GBK").GetString(ecbsd.ReadBytes(ecbsd.ReadInt32()));
        }

        public BmdModel(float x, float y, float z, float unk0, float unk1, float unk2, float unk3, float unk4, float unk5, int unk6, string model)
        {
            X = x;
            Y = y;
            Z = z;
            Unk0 = unk0;
            Unk1 = unk1;
            Unk2 = unk2;
            Unk3 = unk3;
            Unk4 = unk4;
            Unk5 = unk5;
            Unk6 = unk6;
            ModelPath = model;
        }

        public void Write(BinaryWriter ecbsd, int version)
        {
            var model = Encoding.GetEncoding("GBK").GetBytes(ModelPath);
            OffsetBlock.Offset = (int) ecbsd.BaseStream.Position;
            ecbsd.Write(X);
            ecbsd.Write(Y);
            ecbsd.Write(Z);
            ecbsd.Write(Unk0);
            ecbsd.Write(Unk1);
            ecbsd.Write(Unk2);
            ecbsd.Write(Unk3);
            ecbsd.Write(Unk4);
            ecbsd.Write(Unk5);
            ecbsd.Write(Unk6);
            ecbsd.Write(model.Length);
            ecbsd.Write(model);
        }
    }
}
