using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace MapFilesImporter
{
    public class SmdFile
    {
        public int HasSki;
        public int Version;
        public int ActionsAmount;
        public string BonFile;
        public string SkiFile;
        public string PhyFile;
        public string StckDirectory;
        public List<SmdAction> Actions = new List<SmdAction>();
        public SmdFile(string FilePath)
        {
            BinaryReader br = new BinaryReader(File.Open(FilePath, FileMode.Open));
            br.BaseStream.Position += 8;
            Version = br.ReadInt32();
            HasSki = br.ReadInt32();
            ActionsAmount = br.ReadInt32();
            br.BaseStream.Position += 68;
            BonFile = Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32()));
            if (HasSki==1)
            {
                SkiFile = Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32()));
            }
            if (Version > 4)
            {
                PhyFile = Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32()));
            }
            if (Version > 6)
            {
                StckDirectory = Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32()));
            }
            for (int i = 0; i < ActionsAmount; i++)
            {
                SmdAction sm = new SmdAction();
                sm.ActionName = Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32()));
                sm.unk1 = br.ReadSingle();
                if (Version > 5)
                {
                    sm.unk2 = br.ReadSingle();
                }
                if (Version == 5)
                {
                    sm.Amount = br.ReadInt32();
                    sm.unk3 = br.ReadInt32();
                    sm.unk4 = br.ReadBytes(sm.Amount * 20);
                }
                if(Version>8)
                {
                    sm.unk5 = br.ReadInt32();
                }
                if (Version > 6)
                {
                    sm.ActionFileName = Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32()));
                }
                Actions.Add(sm);
            }
            br.Close();
        }
    }
    public class SmdAction
    {
        public string ActionName;
        public float unk1;
        public float unk2;
        public int Amount;
        public int unk3;
        public byte[] unk4;
        public int unk5;
        public string ActionFileName;
    }
}

