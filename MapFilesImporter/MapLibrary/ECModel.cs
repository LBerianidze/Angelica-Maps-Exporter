using System;
using System.IO;
using System.Text;

namespace MapFilesImporter.Struct
{
    public class Ecm
    {
        public int Id;
        public float X;
        public float Y;
        public float Z;
        public float Rotation1;
        public float Rotation2;
        public float Rotation3;
        public float Unk7;
        public float Unk8;
        public float Unk9;
        public int unk10;
        public string EcmPath;
        public string EcmName;

        public Ecm(int id, float x, float y, float z, float rotation1, float rotation2, float rotation3, float unk7, float unk8, float unk9, string path, string name)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
            Rotation1 = rotation1;
            Rotation2 = rotation2;
            Rotation3 = rotation3;
            Unk7 = unk7;
            Unk8 = unk8;
            Unk9 = unk9;
            EcmPath = path;
            EcmName = name;
        }

        public Ecm(BinaryReader br, int version)
        {
            Id = br.ReadInt32();
            X = br.ReadSingle();
            Y = br.ReadSingle();
            Z = br.ReadSingle();
            Rotation1 = br.ReadSingle();
            Rotation2 = br.ReadSingle();
            Rotation3 = br.ReadSingle();
            Unk7 = br.ReadSingle();
            Unk8 = br.ReadSingle();
            Unk9 = br.ReadSingle();
            if (version == 13 || version == 14)
            {
                unk10 = br.ReadInt32();
            }
            EcmPath = Encoding.GetEncoding("GBK").GetString(br.ReadBytes(br.ReadInt32()));
            EcmName = Encoding.GetEncoding("GBK").GetString(br.ReadBytes(br.ReadInt32()));
        }

        public void Save(BinaryWriter bw, int version)
        {
            var ecmPathBytes = Encoding.GetEncoding("GBK").GetBytes(EcmPath);
            var ecmNameBytes = Encoding.GetEncoding("GBK").GetBytes(EcmName);

            bw.Write(Id);
            bw.Write(X);
            bw.Write(Y);
            bw.Write(Z);
            bw.Write(Rotation1);
            bw.Write(Rotation2);
            bw.Write(Rotation3);
            bw.Write(Unk7);
            bw.Write(Unk8);
            bw.Write(Unk9);
            bw.Write(ecmPathBytes.Length);
            bw.Write(ecmPathBytes);
            bw.Write(ecmNameBytes.Length);
            bw.Write(ecmNameBytes);
        }
    }
}
