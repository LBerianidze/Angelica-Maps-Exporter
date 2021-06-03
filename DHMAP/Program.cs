using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DHMAP
{
    class Program
    {
        static int Version;
        static int FileSize;
        static int m_iWidth;
        static int m_iLength;
        static int m_iBlockSizeExp;
        static int m_iBlockSize;
        static int m_iImageWidth;
        static int m_iImageLength;
        static float PixelSize;
        static int BlkIDSize;
        static int BlkSize;
        static List<int> m_BlockIDs;
        static List<List<ushort>> m_arrBlocks;
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                FileName = "DHMap Server File",
                Filter = "DHMap|*.dhmap"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                BinaryReader br = new BinaryReader(File.Open(ofd.FileName, FileMode.Open));
                if (br.BaseStream.Length != 0)
                {
                    Version = br.ReadInt32();
                    FileSize = br.ReadInt32();
                    m_iWidth = br.ReadInt32();
                    m_iLength = br.ReadInt32();
                    m_iBlockSizeExp = br.ReadInt32();
                    m_iBlockSize = 1 << m_iBlockSizeExp;
                    m_iImageWidth = br.ReadInt32();
                    m_iImageLength = br.ReadInt32();
                    PixelSize = br.ReadSingle();
                    BlkIDSize = m_iWidth * m_iLength;
                    BlkSize = m_iBlockSize * m_iBlockSize;
                    m_BlockIDs = new List<int>();
                    for (int i = 0; i < BlkIDSize; i++)
                    {
                        m_BlockIDs.Add(br.ReadInt32());
                    }
                    int arrBlkSize = br.ReadInt32();
                    m_arrBlocks = new List<List<ushort>>(arrBlkSize);
                    for (int i = 0; i < arrBlkSize; i++)
                    {
                        List<ushort> shorts = new List<ushort>();
                        for (int b = 0; b < BlkSize; b++)
                        {
                            shorts.Add(br.ReadUInt16());
                        }
                        m_arrBlocks.Add(shorts);
                    }
                    Bitmap bm = new Bitmap(1024, 1024, PixelFormat.Format24bppRgb);
                    for (int y = 0; y < m_iImageLength; y++)
                    {
                        for (int x = 0; x < m_iImageWidth; x++)
                        {
                            byte[] values = BitConverter.GetBytes(GetPixel(x, y));
                            bm.SetPixel(x, 1023 - y, Color.FromArgb(255, values[0], values[1], values[2]));
                        }
                    }
                    FolderBrowserDialog sfd = new FolderBrowserDialog();
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var k = ofd.FileName.Split('\\');
                        var d = k.Last().Split('.');
                        string fl = sfd.SelectedPath + "\\" + k[k.Length - 3] + "_" + d[0] + "-" + "dhmap" + ".png";
                        bm.Save(fl, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                br.Close();
            }
        }
        public static Byte LOBYTE(int val)
        {
            return (Byte)(val & 0xFFFFFF);
        }
        static int GetPixel(int u, int v)
        {
            int uBlkOffset;
            int vBlkOffset;
            int pixel = 0;
            int BlkID;
            int vBlkID;
            int uBlkID;
            uBlkID = u >> LOBYTE(m_iBlockSizeExp);
            vBlkID = v >> LOBYTE(m_iBlockSizeExp);
            BlkID = m_BlockIDs[uBlkID + m_iWidth * vBlkID];
            if (BlkID != -1)
            {
                uBlkOffset = u & (m_iBlockSize - 1);
                vBlkOffset = v & (m_iBlockSize - 1);
                int pixelPos = uBlkOffset + (vBlkOffset << LOBYTE(m_iBlockSizeExp));
                pixel = m_arrBlocks[BlkID][pixelPos];
            }
            return pixel;
        }
    }
}
