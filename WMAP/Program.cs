using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMAP
{
    class Program
    {
        static List<CWaterArea> CWaterAreas = new List<CWaterArea>();
        static float m_fWidth;
        static float m_fLength;
        static int iNum;
        static void Main(string[] args)
        {
            BinaryReader br = new BinaryReader(File.Open(@"C:\Users\Luka\Desktop\Other\MapsExploring\ServerMaps\world\watermap\¶јіЗКµСй.wmap", FileMode.Open));
            int Header = br.ReadInt32();
            if (Header == -872415231)
            {
                m_fWidth = br.ReadSingle();
                m_fLength = br.ReadSingle();
                iNum = br.ReadInt32();
                for (int i = 0; i < iNum; i++)
                {
                    CWaterAreas.Add(new CWaterArea(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle()));
                }
            }
            br.Close();
        }
        class CWaterArea
        {
            public CWaterArea(float f1, float f2, float f3, float f4, float f5)
            {
                CenterX = f1;
                CenterZ = f2;
                HalfWidth = f3;
                HalfLength = f4;
                Height = f5;
            }
            public float CenterX;
            public float CenterZ;
            public float HalfWidth;
            public float HalfLength;
            public float Height;
        }
    }
}
