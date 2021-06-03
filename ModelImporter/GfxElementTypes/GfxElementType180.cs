using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType180
    {
        public Vector3D Pos;
        public Vector3D Pos_1;
        public int SegLife;
        public float Amp;
        public float Amp_1;
        public int Bind;
        public int EnableMat;
        public int EnablePos;
        public int EnablePos_1;
        public GfxElementType180(StreamReaderL sr, int vers)
        {
            Pos = new Vector3D(sr, false);
            Pos_1 = new Vector3D(sr, false);
            SegLife = sr.ReadLine().GetEcmLineValue().ToInt32();
            Amp = sr.ReadLine().GetEcmLineValue().ToSingle();
            Amp_1 = sr.ReadLine().GetEcmLineValue().ToSingle();
            Bind = sr.ReadLine().GetEcmLineValue().ToInt32();
            EnableMat = sr.ReadLine().GetEcmLineValue().ToInt32();
            EnablePos = sr.ReadLine().GetEcmLineValue().ToInt32();
            EnablePos_1 = sr.ReadLine().GetEcmLineValue().ToInt32();
        }
        public void Save(StreamWriter sw, int vers)
        {
            Pos.Save(sw, "Pos");
            Pos_1.Save(sw, "Pos");
            sw.WriteParameter("SegLife", SegLife);
            sw.WriteParameter("Amp", Amp);
            sw.WriteParameter("Amp", Amp_1);
            sw.WriteParameter("Bind", Bind);
            sw.WriteParameter("EnableMat", EnableMat);
            sw.WriteParameter("EnablePos", EnablePos);
            sw.WriteParameter("EnablePos", EnablePos_1);
        }
    }

}
