using System.IO;
using System.Text;

namespace MapFilesImporter.Struct
{
    public class Effect
    {
        public int Id;
        public float Unk1;
        public float X;
        public float Y;
        public float Z;
        public float[] Unk2;//6
        public float Unk4;
        public int Unk5;
        public float Unk6;
        public int Unk7;
        public string GfxPath;

        public Effect(int id, float unk1, float x, float y, float z, float[] unk2, float unk4, int unk5, float unk6, int unk7, string gfxPath)
        {
            Id = id;
            Unk1 = unk1;
            X = x;
            Y = y;
            Z = z;
            Unk2 = unk2;
            Unk4 = unk4;
            Unk5 = unk5;
            Unk6 = unk6;
            Unk7 = unk7;
            GfxPath = gfxPath;
        }

        public Effect(BinaryReader br, int version)
        {
            Id = br.ReadInt32();
            Unk1 = br.ReadSingle();
            X = br.ReadSingle();
            Y = br.ReadSingle();
            Z = br.ReadSingle();
            Unk2 = new float[6];
            for (var i = 0; i < 6; i++)
                Unk2[i] = br.ReadSingle();
            Unk4 = br.ReadSingle();
            Unk5 = br.ReadInt32();
            Unk6 = br.ReadSingle();
            Unk7 = br.ReadInt32();
            GfxPath = Encoding.GetEncoding("GBK").GetString(br.ReadBytes(br.ReadInt32()));
        }

        public void Save(BinaryWriter bw, int version)
        {
            var gfxBytes = Encoding.GetEncoding("GBK").GetBytes(GfxPath);

            bw.Write(Id);
            bw.Write(Unk1);
            bw.Write(X);
            bw.Write(Y);
            bw.Write(Z);
            for (var i = 0; i < 6; i++)
                bw.Write(Unk2[i]);
            bw.Write(Unk4);
            bw.Write(Unk5);
            bw.Write(Unk6);
            bw.Write(Unk7);
            bw.Write(gfxBytes.Length);
            bw.Write(gfxBytes);
        }
    }
}
