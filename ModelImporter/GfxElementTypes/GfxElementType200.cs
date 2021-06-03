using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GfxReader
{
    public class GfxElementType200
    {
        public string GfxPath;
        public int OutColor;
        public int LoopFlag;
        public float PlaySpeed;
        public int DummyUseGScale;
        public GfxElementType200(StreamReaderL sr, int vers)
        {
            GfxPath = sr.ReadLine().GetEcmLineValue();
            if (vers >= 47)
            {
                OutColor = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (vers >= 56)
            {
                LoopFlag = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (vers >= 78)
            {
                PlaySpeed = sr.ReadLine().GetEcmLineValue().ToSingle();
            }
            if (vers >= 95)
            {
                DummyUseGScale = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("GfxPath", GfxPath);
            if (vers >= 47)
            {
                sw.WriteParameter("OutColor", OutColor);
            }
            if (vers >= 56)
            {
                sw.WriteParameter("LoopFlag", LoopFlag);
            }
            if (vers >= 78)
            {
                sw.WriteParameter("PlaySpeed", PlaySpeed);
            }
            if (vers >= 95)
            {
                sw.WriteParameter("DummyUseGScale", DummyUseGScale);
            }
        }
    }
}