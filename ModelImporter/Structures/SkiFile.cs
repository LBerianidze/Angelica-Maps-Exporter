using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelImporter
{
    public class SkiFile
    {
        public List<string> Textures = new List<string>();
        public bool isnull;
        public int Version;
        public SkiFile(byte[] b)
        {
            if (b.Length == 0)
            {
                isnull = true;
                return;
            }
            BinaryReader br = new BinaryReader(new MemoryStream(b));
            br.BaseStream.Seek(8, SeekOrigin.Begin);
            Version = br.ReadInt32();
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
                Textures.Add(Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32())).ToLower());
            }
            br.Close();
        }
    }
}
