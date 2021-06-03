using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapFilesImporter
{
    public class MoxFile
    {
        public string Texture;
        public MoxFile(byte[] bt)
        {
            BinaryReader br = new BinaryReader(new MemoryStream(bt));
            if (br.ReadInt32() == 1113083725)
            {
                byte[] TextureBytes = Encoding.GetEncoding("GBK").GetBytes("TEXTURE: "), MaterialBytes = Encoding.GetEncoding("GBK").GetBytes("\0MATERIAL:");
                bool TR = true;
                while (TR)
                {
                    TR = false;
                    for (int z = 0; z < TextureBytes.Length; z++)
                    {
                        if (br.ReadByte() != TextureBytes[z])
                        {
                            TR = true;
                            break;
                        }
                    }
                }
                int TextureEndPosition = (int)br.BaseStream.Position, MaterialStartPosition = 0;
                bool MR = true;
                while (MR)
                {
                    MR = false;
                    for (int z = 0; z < MaterialBytes.Length; z++)
                    {
                        if (br.ReadByte() != MaterialBytes[z])
                        {
                            MR = true;
                            break;
                        }
                    }
                }
                MaterialStartPosition = (int)br.BaseStream.Position - MaterialBytes.Length;
                br.BaseStream.Position = TextureEndPosition;
                int TLength = MaterialStartPosition - TextureEndPosition;
                Texture = Encoding.GetEncoding(936).GetString(br.ReadBytes(TLength)).ToLower();
                br.Close();
            }
            else
            {
                br.Close();
                StreamReaderL l = new StreamReaderL(bt, Encoding.GetEncoding(936));
              Texture = l.ReadWhile("TEXTURE:", null).GetEcmLineValue().ToLower();
                l.Close();
            }
        }
    }
}
