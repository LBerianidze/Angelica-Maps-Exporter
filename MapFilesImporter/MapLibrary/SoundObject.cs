using System.IO;
using System.Text;

namespace MapFilesImporter.Struct
{
    public class SoundObject
    {
        public int Id;
        public float X;
        public float Y;
        public float Z;
        public float Unk0;
        public float Unk1;
        public float Unk2;
        public int Unk3;
        public int unk4;
        public int unk5;
        public int unk6;
        public float unk7;
        public int unk8;
        public string WavPath;

        public SoundObject(int id, float x, float y, float z, float unk0, float unk1, float unk2, int unk3, string wavFile)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
            Unk0 = unk0;
            Unk1 = unk1;
            Unk2 = unk2;
            Unk3 = unk3;
            WavPath = wavFile;
        }

        public SoundObject(BinaryReader br, int version)
        {
            Id = br.ReadInt32();
            X = br.ReadSingle();
            Y = br.ReadSingle();
            Z = br.ReadSingle();
            Unk0 = br.ReadSingle();
            Unk1 = br.ReadSingle();
            Unk2 = br.ReadSingle();
            Unk3 = br.ReadInt32();
            if (version == 14)
            {
                unk4 = br.ReadInt32();
                unk5 = br.ReadInt32();
                unk6 = br.ReadInt32();
                unk7 = br.ReadSingle();
                unk8 = br.ReadInt32();
            }
            WavPath = Encoding.GetEncoding("GBK").GetString(br.ReadBytes(br.ReadInt32()));
        }

        public void Save(BinaryWriter bw, int version)
        {
            var wavBytes = Encoding.GetEncoding("GBK").GetBytes(WavPath);

            bw.Write(Id);
            bw.Write(X);
            bw.Write(Y);
            bw.Write(Z);
            bw.Write(Unk0);
            bw.Write(Unk1);
            bw.Write(Unk2);
            bw.Write(Unk3);
            bw.Write(wavBytes.Length);
            bw.Write(wavBytes);
        }
    }
}
