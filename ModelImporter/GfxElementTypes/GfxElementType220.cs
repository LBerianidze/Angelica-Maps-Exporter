using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType220
    {
        public string PhysDesc;
        public float ParHeight;
        public float ParWidth;
        public float ScaleMax;
        public float ScaleMin;
        public float RotMax;
        public float RotMin;
        public int ColorMax;
        public int ColorMin;
        public int IsFaceToView;
        public GfxElementType220(StreamReaderL sr, int vers)
        {
            PhysDesc = sr.ReadLine().GetEcmLineValue();
            ParHeight = sr.ReadLine().GetEcmLineValue().ToSingle();
            ParWidth = sr.ReadLine().GetEcmLineValue().ToSingle();
            ScaleMax = sr.ReadLine().GetEcmLineValue().ToSingle();
            ScaleMin = sr.ReadLine().GetEcmLineValue().ToSingle();
            RotMax = sr.ReadLine().GetEcmLineValue().ToSingle();
            RotMin = sr.ReadLine().GetEcmLineValue().ToSingle();
            ColorMax = sr.ReadLine().GetEcmLineValue().ToInt32();
            ColorMin = sr.ReadLine().GetEcmLineValue().ToInt32();
            IsFaceToView = sr.ReadLine().GetEcmLineValue().ToInt32();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("PhysDesc", PhysDesc);
            sw.WriteParameter("ParHeight", ParHeight);
            sw.WriteParameter("ParWidth", ParWidth);
            sw.WriteParameter("ScaleMax", ScaleMax);
            sw.WriteParameter("ScaleMin", ScaleMin);
            sw.WriteParameter("RotMax", RotMax);
            sw.WriteParameter("RotMin", RotMin);
            sw.WriteParameter("ColorMax", ColorMax);
            sw.WriteParameter("ColorMin", ColorMin);
            sw.WriteParameter("IsFaceToView", IsFaceToView);
        }
    }

}
