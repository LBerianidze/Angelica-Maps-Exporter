using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace MapFilesImporter.Struct
{
    public class TerrainInfo
    {
        public List<Block> Usings = new List<Block>();
        public OffsetBlock OffsetBlock;

        public int Unk0;
        public int Unk1;
        public float X;
        public float Y;
        public float Z;
        public float XSize;
        public float YSize;
        public float ZSize;
        public Unk1[] U1; //3
        public Unk2[] U2; //3
        public int[] Arbas; //3
        public Unk2 U3;
        public int Unk2;
        public int Unk3;
        public float Unk4;
        public int ColorGbra0;
        public int Unk5;
        public float Unk6;
        public Unk3[] U4; //2
        public Unk2 U5;
        public int ColorGbra1;
        public int[] unk7;
        float unk8;
        float unk9;
        public string Sky1;
        public string Sky2;
        public string Sky3;
        public string Sky4;
        public string Sky5;
        public string Sky6;
        public string Music;

        public TerrainInfo(BinaryReader ecbsd, OffsetBlock block, int BsdVersion, int WldVersion, int IsDifferent)
        {
            OffsetBlock = block;

            ecbsd.BaseStream.Position = block.Offset;
            Unk0 = ecbsd.ReadInt32();
            Unk1 = ecbsd.ReadInt32();
            X = ecbsd.ReadSingle();
            Y = ecbsd.ReadSingle();
            Z = ecbsd.ReadSingle();
            XSize = ecbsd.ReadSingle();
            YSize = ecbsd.ReadSingle();
            ZSize = ecbsd.ReadSingle();
            U1 = new Unk1[3];
            for (var i = 0; i < 3; i++)
            {
                U1[i] = new Unk1(ecbsd, BsdVersion);
            }
            U2 = new Unk2[3];
            for (var i = 0; i < 3; i++)
            {
                U2[i] = new Unk2(ecbsd, BsdVersion);
            }
            Arbas = new int[3];
            for (var i = 0; i < 3; i++)
            {
                Arbas[i] = ecbsd.ReadInt32();
            }
            U3 = new Unk2(ecbsd, BsdVersion);
            Unk2 = ecbsd.ReadInt32();
            Unk3 = ecbsd.ReadInt32();
            Unk4 = ecbsd.ReadSingle();
            ColorGbra0 = ecbsd.ReadInt32();
            Unk5 = ecbsd.ReadInt32();
            Unk6 = ecbsd.ReadSingle();
            U4 = new Unk3[2];
            for (var i = 0; i < 2; i++)
                U4[i] = new Unk3(ecbsd, BsdVersion);
            U5 = new Unk2(ecbsd, BsdVersion);
            ColorGbra1 = ecbsd.ReadInt32();
            if (BsdVersion == 14 || BsdVersion == 15 || BsdVersion == 17)
            {
                unk7 = new int[12];
                for (int i = 0; i < 12; i++)
                {
                    unk7[i] = ecbsd.ReadInt32();
                }
                if ((WldVersion == 14 || WldVersion == 15 || WldVersion == 13) && (BsdVersion == 15))
                {
                    unk8 = ecbsd.ReadSingle();
                    unk9 = ecbsd.ReadSingle();
                }
                if (IsDifferent == 1)
                {
                    ecbsd.ReadBytes(92);
                }
            }
            Sky1 = Encoding.GetEncoding("GBK").GetString(ecbsd.ReadBytes(ecbsd.ReadInt32()));
            Sky2 = Encoding.GetEncoding("GBK").GetString(ecbsd.ReadBytes(ecbsd.ReadInt32()));
            Sky3 = Encoding.GetEncoding("GBK").GetString(ecbsd.ReadBytes(ecbsd.ReadInt32()));
            Sky4 = Encoding.GetEncoding("GBK").GetString(ecbsd.ReadBytes(ecbsd.ReadInt32()));
            Sky5 = Encoding.GetEncoding("GBK").GetString(ecbsd.ReadBytes(ecbsd.ReadInt32()));
            Sky6 = Encoding.GetEncoding("GBK").GetString(ecbsd.ReadBytes(ecbsd.ReadInt32()));
            Music = Encoding.GetEncoding("GBK").GetString(ecbsd.ReadBytes(ecbsd.ReadInt32()));
        }

        public void Write(BinaryWriter ecbsd, int version)
        {
            var sky1 = Encoding.GetEncoding("GBK").GetBytes(Sky1);
            var sky2 = Encoding.GetEncoding("GBK").GetBytes(Sky2);
            var sky3 = Encoding.GetEncoding("GBK").GetBytes(Sky3);
            var sky4 = Encoding.GetEncoding("GBK").GetBytes(Sky4);
            var sky5 = Encoding.GetEncoding("GBK").GetBytes(Sky5);
            var sky6 = Encoding.GetEncoding("GBK").GetBytes(Sky6);
            var music = Encoding.GetEncoding("GBK").GetBytes(Music);

            OffsetBlock.Offset = (int)ecbsd.BaseStream.Position;
            ecbsd.Write(Unk0);
            ecbsd.Write(Unk1);
            ecbsd.Write(X);
            ecbsd.Write(Y);
            ecbsd.Write(Z);
            ecbsd.Write(XSize);
            ecbsd.Write(YSize);
            ecbsd.Write(ZSize); //32
            for (var i = 0; i < 3; i++)
                U1[i].Write(ecbsd, version);
            for (var i = 0; i < 3; i++)
                U2[i].Write(ecbsd, version);
            for (var i = 0; i < 3; i++)
                ecbsd.Write(Arbas[i]);
            U3.Write(ecbsd, version);
            ecbsd.Write(Unk2);
            ecbsd.Write(Unk3);
            ecbsd.Write(Unk4);
            ecbsd.Write(ColorGbra0);
            ecbsd.Write(Unk5);
            ecbsd.Write(Unk6);
            for (var i = 0; i < 2; i++)
                U4[i].Write(ecbsd, version);
            U5.Write(ecbsd, version);
            ecbsd.Write(ColorGbra1);
            ecbsd.Write(sky1.Length);
            ecbsd.Write(sky1);
            ecbsd.Write(sky2.Length);
            ecbsd.Write(sky2);
            ecbsd.Write(sky3.Length);
            ecbsd.Write(sky3);
            ecbsd.Write(sky4.Length);
            ecbsd.Write(sky4);
            ecbsd.Write(sky5.Length);
            ecbsd.Write(sky5);
            ecbsd.Write(sky6.Length);
            ecbsd.Write(sky6);
            ecbsd.Write(music.Length);
            ecbsd.Write(music);
        }
    }

    public class Unk1
    {
        public float Unk0;
        public float unk_1;
        public float Unk2;
        public float Unk3;

        public Unk1(BinaryReader br, int version)
        {
            Unk0 = br.ReadSingle();
            unk_1 = br.ReadSingle();
            Unk2 = br.ReadSingle();
            Unk3 = br.ReadSingle();
        }

        public void Write(BinaryWriter bw, int version)
        {
            bw.Write(Unk0);
            bw.Write(unk_1);
            bw.Write(Unk2);
            bw.Write(Unk3);
        }
    }

    public class Unk2
    {
        public int Rgba;
        public float Unk0;
        public float Unk1;
        public float unk_2;

        public Unk2(BinaryReader br, int version)
        {
            Rgba = br.ReadInt32();
            Unk0 = br.ReadSingle();
            Unk1 = br.ReadSingle();
            unk_2 = br.ReadSingle();
        }

        public void Write(BinaryWriter bw, int version)
        {
            bw.Write(Rgba);
            bw.Write(Unk0);
            bw.Write(Unk1);
            bw.Write(unk_2);
        }
    }

    public class Unk3
    {
        public int Rgba;
        public float Unk0;
        public float Unk1;

        public Unk3(BinaryReader br, int version)
        {
            Rgba = br.ReadInt32();
            Unk0 = br.ReadSingle();
            Unk1 = br.ReadSingle();
        }

        public void Write(BinaryWriter bw, int version)
        {
            bw.Write(Rgba);
            bw.Write(Unk0);
            bw.Write(Unk1);
        }
    }
}
