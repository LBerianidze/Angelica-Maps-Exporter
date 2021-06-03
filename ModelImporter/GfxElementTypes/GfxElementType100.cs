using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType100
    {
        public float Width;
        public float Height;
        public int RotFromView;
        public int GrndNormOnly;
        public int NoScale1;
        public int NoScale2;
        public float OrgPt1;
        public float OrgPt2;
        public float ZOffset;
        public int MatchSurface;
        public int SurfaceUseParentDir;
        public float MaxExtent;
        public int YawEffect;
        public GfxElementType100(StreamReaderL sr, int vers)
        {
            Width = sr.ReadLine().GetEcmLineValue().ToSingle();
            Height = sr.ReadLine().GetEcmLineValue().ToSingle();
            RotFromView = sr.ReadLine().GetEcmLineValue().ToInt32();
            GrndNormOnly = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (vers >= 37)
            {
                NoScale1 = sr.ReadLine().GetEcmLineValue().ToInt32();
                NoScale2 = sr.ReadLine().GetEcmLineValue().ToInt32();
                OrgPt1 = sr.ReadLine().GetEcmLineValue().ToSingle();
                OrgPt2 = sr.ReadLine().GetEcmLineValue().ToSingle();
            }
            if (vers >= 42)
            {
                ZOffset = sr.ReadLine().GetEcmLineValue().ToSingle();
            }
            if (vers >= 54)
            {
                MatchSurface = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (vers >= 86)
            {
                SurfaceUseParentDir = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (vers >= 54)
            {
                MaxExtent = sr.ReadLine().GetEcmLineValue().ToSingle();
            }
            if (vers >= 61)
            {
                YawEffect = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("Width", Width);
            sw.WriteParameter("Height", Height);
            sw.WriteParameter("RotFromView", RotFromView);
            sw.WriteParameter("GrndNormOnly", GrndNormOnly);
            if (vers >= 37)
            {
                sw.WriteParameter("NoScale", NoScale1);
                sw.WriteParameter("NoScale", NoScale2);
                sw.WriteParameter("OrgPt", OrgPt1);
                sw.WriteParameter("OrgPt", OrgPt2);
            }
            if (vers >= 42)
            {
                sw.WriteParameter("ZOffset", ZOffset);
            }
            if (vers >= 54)
            {
                sw.WriteParameter("MatchSurface", MatchSurface);
            }
            if (vers >= 86)
            {
                sw.WriteParameter("SurfaceUseParentDir", SurfaceUseParentDir);
            }
            if (vers >= 54)
            {
                sw.WriteParameter("MaxExtent", MaxExtent);
            }
            if (vers >= 61)
            {
                sw.WriteParameter("YawEffect", YawEffect);
            }
        }
    }
}
