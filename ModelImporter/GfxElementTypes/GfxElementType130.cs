using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType130
    {
        public int LightType;
        public int Diffuse;
        public int Specular;
        public int Ambient;
        public Vector3D Position;
        public Vector3D Direction;
        public float Range;
        public float FallOff;
        public float Attentuation0_1;
        public float Attentuation0_2;
        public float Attentuation0_3;
        public float Theta;
        public float Phi;
        public GfxElementType130(StreamReaderL sr, int vers)
        {
            LightType = sr.ReadLine().GetEcmLineValue().ToInt32();
            Diffuse = sr.ReadLine().GetEcmLineValue().ToInt32();
            Specular = sr.ReadLine().GetEcmLineValue().ToInt32();
            Ambient = sr.ReadLine().GetEcmLineValue().ToInt32();
            Position = new Vector3D(sr, false);
            Direction = new Vector3D(sr, false);
            Range = sr.ReadLine().GetEcmLineValue().ToSingle();
            FallOff = sr.ReadLine().GetEcmLineValue().ToSingle();
            Attentuation0_1 = sr.ReadLine().GetEcmLineValue().ToSingle();
            Attentuation0_2 = sr.ReadLine().GetEcmLineValue().ToSingle();
            Attentuation0_3 = sr.ReadLine().GetEcmLineValue().ToSingle();
            Theta = sr.ReadLine().GetEcmLineValue().ToSingle();
            Phi = sr.ReadLine().GetEcmLineValue().ToSingle();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("LightType", LightType);
            sw.WriteParameter("Diffuse", Diffuse);
            sw.WriteParameter("Specular", Specular);
            sw.WriteParameter("Ambient", Ambient);
            Position.Save(sw, "Pos");
            Direction.Save(sw, "Dir");
            sw.WriteParameter("Range", Range);
            sw.WriteParameter("FallOff", FallOff);
            sw.WriteParameter("Attentuation0", Attentuation0_1);
            sw.WriteParameter("Attentuation0", Attentuation0_2);
            sw.WriteParameter("Attentuation0", Attentuation0_3);
            sw.WriteParameter("Attentuation0", Theta);
            sw.WriteParameter("Attentuation0", Phi);
        }
    }

}
