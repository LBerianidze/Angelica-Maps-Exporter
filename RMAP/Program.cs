using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
namespace RMAP
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                FileName = "RMap Server File",
                Filter = "RMap|*.rmap"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                BinaryReader br = new BinaryReader(File.Open(ofd.FileName, FileMode.Open));
                int Version = br.ReadInt32();
                int FileSize = br.ReadInt32();
                int LineSize = br.ReadInt32();
                int MapWidth = br.ReadInt32();
                int MapHeight = br.ReadInt32();
                int LinesCount = br.ReadInt32();
                float PixelSize = br.ReadSingle();
                BitArray[] Lines = new BitArray[LinesCount];
                for (int i = 0; i < LinesCount; i++)
                {
                    Lines[i] = new BitArray(new byte[1]);
                }
                for (int i = LinesCount - 1; i > -1; i--)
                {
                    Lines[i] = new BitArray(br.ReadBytes(LineSize));
                }
                br.Close();
                byte[] FrameBytes = new byte[MapWidth * MapHeight];
                IntPtr pointer = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(FrameBytes, 0);
                Bitmap btml = new Bitmap(MapWidth, MapHeight, MapWidth, System.Drawing.Imaging.PixelFormat.Format8bppIndexed, pointer);
                int p = 0;

                for (int y = 0; y < Lines.Length; y++)
                {
                    for (int x = 0; x < Lines[y].Count; x++)
                    {
                        if (Lines[y][x])
                        {
                            //RMAP->FrameBytes[Block_to_Image_Position(i,n,k)] = 255;
                            FrameBytes[p] = 255;
                        }
                        p++;
                    }
                }
                FolderBrowserDialog sfd = new FolderBrowserDialog();
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var k = ofd.FileName.Split('\\');
                    var d = k.Last().Split('.');
                    string fl = sfd.SelectedPath + "\\" + k[k.Length - 3] +"_"+ d[0]+"-"+"rmap"+ ".png";
                    btml.Save(fl, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
    }
}
