using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType110
    {
        public Vector3D OrgPos1;
        public Vector3D OrgPos2;
        public int EnableMat;
        public int EnableOrgPos1;
        public int EnableOrgPos2;
        public int SegLife;
        public int Bind;
        public int Spline;
        public GfxElementType110(StreamReaderL sr, int vers)
        {
            OrgPos1 = new Vector3D(sr, false);
            OrgPos2 = new Vector3D(sr, false);
            EnableMat = sr.ReadLine().GetEcmLineValue().ToInt32();
            EnableOrgPos1 = sr.ReadLine().GetEcmLineValue().ToInt32();
            EnableOrgPos2 = sr.ReadLine().GetEcmLineValue().ToInt32();
            SegLife = sr.ReadLine().GetEcmLineValue().ToInt32();
            Bind = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (vers >= 87)
            {
                Spline = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            OrgPos1.Save(sw, "OrgPos1");
            OrgPos2.Save(sw, "OrgPos2");
            sw.WriteParameter("EnableMat", EnableMat);
            sw.WriteParameter("EnableOrgPos1", EnableOrgPos1);
            sw.WriteParameter("EnableOrgPos2", EnableOrgPos2);
            sw.WriteParameter("SegLife", SegLife);
            sw.WriteParameter("Bind", Bind);
            if (vers >= 87)
            {
                sw.WriteParameter("Spline", Spline);
            }
        }
    }

}
