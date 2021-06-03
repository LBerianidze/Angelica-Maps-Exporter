using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MapFilesImporter.Struct
{
    public class SmdModel
    {
        public List<Block> Usings = new List<Block>();
        public OffsetBlock OffsetBlock;

        public int Id;
        public float X;
        public float Y;
        public float Z;
        public float Unk0;
        public int Unk1;
        public int Unk2;
        public float Unk3;
        public float Unk4;
        public string ModelPath;

        public SmdModel(int id, float x, float y, float z, float unk0, int unk1, int unk2, float unk3, float unk4, string modelPath)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
            Unk0 = unk0;
            Unk1 = unk1;
            Unk2 = unk2;
            Unk3 = unk3;
            Unk4 = unk4;
            ModelPath = modelPath;
        }

        public SmdModel(BinaryReader ecbsd, OffsetBlock Block, int version)
        {
            OffsetBlock = Block;

            ecbsd.BaseStream.Position = OffsetBlock.Offset;
            Id = ecbsd.ReadInt32();
            X = ecbsd.ReadSingle();
            Y = ecbsd.ReadSingle();
            Z = ecbsd.ReadSingle();
            Unk0 = ecbsd.ReadSingle();
            Unk1 = ecbsd.ReadInt32();
            Unk2 = ecbsd.ReadInt32();
            Unk3 = ecbsd.ReadSingle();
            Unk4 = ecbsd.ReadSingle();
            ModelPath = Encoding.GetEncoding("GBK").GetString(ecbsd.ReadBytes(ecbsd.ReadInt32()));
        }

        public void Write(BinaryWriter ecbsd, int version)
        {
            var modelPathData = Encoding.GetEncoding("GBK").GetBytes(ModelPath);

            OffsetBlock.Offset = (int) ecbsd.BaseStream.Position;
            ecbsd.Write(Id);
            ecbsd.Write(X);
            ecbsd.Write(Y);
            ecbsd.Write(Z);
            ecbsd.Write(Unk0);
            ecbsd.Write(Unk1);
            ecbsd.Write(Unk2);
            ecbsd.Write(Unk3);
            ecbsd.Write(Unk4);
            ecbsd.Write(modelPathData.Length);
            ecbsd.Write(modelPathData);
        }
    }
}
