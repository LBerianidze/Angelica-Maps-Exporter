using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType140
    {
        public float Radius;
        public float Height;
        public float Pitch;
        public int Sects;
        public int NoRadScale;
        public int NoHeiScale;
        public int OrgAtCenter;
        public GfxElementType140(StreamReaderL sr, int vers)
        {
            Radius = sr.ReadLine().GetEcmLineValue().ToSingle();
            Height = sr.ReadLine().GetEcmLineValue().ToSingle();
            Pitch = sr.ReadLine().GetEcmLineValue().ToSingle();
            Sects = sr.ReadLine().GetEcmLineValue().ToInt32();
            NoRadScale = sr.ReadLine().GetEcmLineValue().ToInt32();
            NoHeiScale = sr.ReadLine().GetEcmLineValue().ToInt32();
            OrgAtCenter = sr.ReadLine().GetEcmLineValue().ToInt32();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("Radius", Radius);
            sw.WriteParameter("Height", Height);
            sw.WriteParameter("Pitch", Pitch);
            sw.WriteParameter("Sects", Sects);
            sw.WriteParameter("NoRadScale", NoRadScale);
            sw.WriteParameter("NoHeiScale", NoHeiScale);
            sw.WriteParameter("OrgAtCenter", OrgAtCenter);
        }
    }

}
