using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType240
    {
        public Vector3D Pos;
        public Vector3D Pos_1;
        public int SegLife;
        public float Spline;
        public float TimeToGravity;
        public int Bind;
        public int EnableMat;
        public int EnablePos;
        public int EnablePos_1;
        public float VelocityToGravity;
        public float Gravity;
        public float VerticalNoise;
        public float VecticalSpeed;
        public float HorzAmplitude;
        public float HorzSpeed;
        public int XNoise;
        public int ZNoise;
        public GfxElementType240(StreamReaderL sr, int vers)
        {
            Pos = new Vector3D(sr, false);
            Pos_1 = new Vector3D(sr, false);
            EnableMat = sr.ReadLine().GetEcmLineValue().ToInt32();
            EnablePos = sr.ReadLine().GetEcmLineValue().ToInt32();
            EnablePos_1 = sr.ReadLine().GetEcmLineValue().ToInt32();
            SegLife = sr.ReadLine().GetEcmLineValue().ToInt32();
            Bind = sr.ReadLine().GetEcmLineValue().ToInt32();
            Spline = sr.ReadLine().GetEcmLineValue().ToSingle();
            TimeToGravity = sr.ReadLine().GetEcmLineValue().ToSingle();
            VelocityToGravity = sr.ReadLine().GetEcmLineValue().ToSingle();
            Gravity = sr.ReadLine().GetEcmLineValue().ToSingle();
            VerticalNoise = sr.ReadLine().GetEcmLineValue().ToSingle();
            VecticalSpeed = sr.ReadLine().GetEcmLineValue().ToSingle();
            HorzAmplitude = sr.ReadLine().GetEcmLineValue().ToSingle();
            HorzSpeed = sr.ReadLine().GetEcmLineValue().ToSingle();
            XNoise = sr.ReadLine().GetEcmLineValue().ToInt32();
            ZNoise = sr.ReadLine().GetEcmLineValue().ToInt32();
        }
        public void Save(StreamWriter sw, int vers)
        {
            Pos.Save(sw, "Pos");
            Pos_1.Save(sw, "Pos");
            sw.WriteParameter("EnableMat", EnableMat);
            sw.WriteParameter("EnablePos", EnablePos);
            sw.WriteParameter("EnablePos", EnablePos_1);
            sw.WriteParameter("SegLife", SegLife);
            sw.WriteParameter("Bind", Bind);
            sw.WriteParameter("Spline", Spline);
            sw.WriteParameter("TimeToGravity", TimeToGravity);
            sw.WriteParameter("VelocityToGravity", VelocityToGravity);
            sw.WriteParameter("Gravity", Gravity);
            sw.WriteParameter("VerticalNoise", VerticalNoise);
            sw.WriteParameter("VecticalSpeed", VecticalSpeed);
            sw.WriteParameter("HorzAmplitude", HorzAmplitude);
            sw.WriteParameter("HorzSpeed", HorzSpeed);
            sw.WriteParameter("XNoise", XNoise);
            sw.WriteParameter("ZNoise", ZNoise);
        }
    }

}
