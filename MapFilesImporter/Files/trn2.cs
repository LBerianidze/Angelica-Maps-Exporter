using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace MapFilesImporter
{
    public class Trn2
    {
        int Header;
        int Version;
        SizeF MapSize;
        float unk1;
        int unk2;
        int unk3;
        int unk4;
        int unk5;
        int TexturesAmount;
        int unk7;
        int unk8;
        int unk9;
        int FilesCount;
        int unk11;
        int unk12;
        int unk13;
        float unk14;
        int[] unk15;
        int ImageSize;
        public List<MapT2bkInfo> MapT2bkInfoL = new List<MapT2bkInfo>();
        public List<string> Textures = new List<string>();
        public List<string> FileNames = new List<string>();
        public Trn2(string Path)
        {
            BinaryReader br = new BinaryReader(File.Open(Path, FileMode.Open));
            Header = br.ReadInt32();
            Version = br.ReadInt32();
            MapSize.Width = br.ReadSingle();
            MapSize.Height = br.ReadSingle();
            unk1 = br.ReadSingle();
            unk2 = br.ReadInt32();
            unk3 = br.ReadInt32();
            unk4 = br.ReadInt32();
            unk5 = br.ReadInt32();
            TexturesAmount = br.ReadInt32();
            unk7 = br.ReadInt32();
            unk8 = br.ReadInt32();
            unk9 = br.ReadInt32();
            FilesCount = br.ReadInt32();
            unk11 = br.ReadInt32();
            unk12 = br.ReadInt32();
            unk13 = br.ReadInt32();
            unk14 = br.ReadSingle();
            unk15 = new int[17];
            for (int i = 0; i < 17; i++)
            {
                unk15[i] = br.ReadInt32();
            }
            for (int i = 0; i < FilesCount; i++)
            {
                MapT2bkInfoL.Add(new MapT2bkInfo(br));
            }
            for (int i = 0; i < TexturesAmount; i++)
            {
                Textures.Add(Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32())));
            }
            ImageSize = br.ReadInt32();
            for (int i = 0; i < FilesCount; i++)
            {
                FileNames.Add(Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32())));
            }
            Textures = Textures.GroupBy(v => v).Select(t => t.FirstOrDefault()).ToList();
            br.Close();
        }
    }
    public class MapT2bkInfo
    {
        public int unk1;
        public float Division;
        public MapT2bkInfo(BinaryReader br)
        {
            unk1 = br.ReadInt32();
            Division = br.ReadInt32();
        }
    }
}
