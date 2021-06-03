using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LBLIBRARY;
using System.Drawing;

namespace t2lm
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryReader br = new BinaryReader(File.Open(@"D:\Pw Servers\Myserverpw\element\maps\world\world_1.t2lm1", FileMode.Open));
            T2lmFile T2lm = new T2lmFile(br);
            br.Close();
            List<Bitmap> Map = new List<Bitmap>();
            for (int i = 0; i < T2lm.Images.Count; i++)
            {
                Map.Add(LBLIBRARY.PWHelper.LoadDDSImage(T2lm.Images[i]));
            }
            int sz = T2lm.unk1 * 4;
            Bitmap bm = new Bitmap(sz, sz);
            Graphics gr = Graphics.FromImage(bm);
            int x = 0;
            int y = 0;
            for (int i = 0; i < Map.Count; i++)
            {
                gr.DrawImage(Map[i],new Point( x, y));
                x += T2lm.unk1;
                if(x== sz)
                {
                    x = 0;
                    y += T2lm.unk1;
                }
            }
            bm.Save(@"C:\Users\Luka\Desktop\a02_t2lm1.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
    public class T2lmFile
    {
        public int Header;
        public int Version;
        public int Amount;
        public int unk1;
        public int unk2;
        public int unk3;
        public int[] Offsets;
        public List<byte[]> Images = new List<byte[]>();
        public T2lmFile(BinaryReader br)
        {
            Header = br.ReadInt32();
            Version = br.ReadInt32();
            Amount = br.ReadInt32();
            unk1 = br.ReadInt32();
            unk2 = br.ReadInt32();
            unk3 = br.ReadInt32();
            Offsets = new int[Amount];
            for (int i = 0; i < Amount; i++)
            {
                Offsets[i] = br.ReadInt32();
            }
            for (int i = 0; i < Amount; i++)
            {
                List<byte> bts = new List<byte>();
                bts.AddRange(br.ReadBytes(20));
                int BytesAmount = br.ReadInt32();
                bts.AddRange(BitConverter.GetBytes(BytesAmount));
                bts.AddRange(br.ReadBytes(56));
                int TypeLength = br.ReadInt32();
                bts.AddRange(BitConverter.GetBytes(TypeLength));
                bts.AddRange(br.ReadBytes(TypeLength));
                bts.AddRange(br.ReadBytes(40));
                bts.AddRange(br.ReadBytes(BytesAmount));
                Images.Add(bts.ToArray());
            }
        }
    }
}
