using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapFilesImporter
{
    public class SkiFile
    {
        public List<string> Textures = new List<string>();
        public SkiFile(string Path)
        {
            BinaryReader br = new BinaryReader(File.Open(Path, FileMode.Open));
            br.BaseStream.Seek(8, SeekOrigin.Begin);
            int Version = br.ReadInt32();
            br.BaseStream.Seek(16, SeekOrigin.Current);
            int textCount = br.ReadInt32();
            int matCount = br.ReadInt32();
            int bipsCount = br.ReadInt32();
            br.BaseStream.Seek(68, SeekOrigin.Current);
            if (Version >= 9)
            {
                for (int i = 0; i < bipsCount; i++)
                {
                    br.BaseStream.Seek(br.ReadInt32(), SeekOrigin.Current);
                }
            }
            for (int i = 0; i < textCount; i++)
            {
                Textures.Add(Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32())));
            }
            br.Close();
        }
    }
}
