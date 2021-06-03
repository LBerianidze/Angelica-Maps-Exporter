using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GfxReader
{
    public class GfxElementType160
    {
        public string ModelPath;
        public string ModelActName;
        public int Loops;
        public int AlphaCmp;
        public int WriteZ;
        public int Use3DCam;
        public float MinDist;
        public float MaxDist;
        public int FacingDir;
        public GfxElementType160()
        {

        }
        public GfxElementType160(StreamReaderL sr, int vers)
        {
            ModelPath = sr.ReadLine().GetEcmLineValue();
            ModelActName = sr.ReadLine().GetEcmLineValue();
            Loops = sr.ReadLine().GetEcmLineValue().ToInt32();
            AlphaCmp = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (vers >= 47)
            {
                WriteZ = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (vers >= 77)
            {
                Use3DCam = sr.ReadLine().GetEcmLineValue().ToInt32();
                if (vers >= 100)
                {
                    FacingDir = sr.ReadLine().GetEcmLineValue().ToInt32();
                }
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("ModelPath", ModelPath);
            sw.WriteParameter("ModelActName", ModelActName);
            sw.WriteParameter("Loops", Loops);
            sw.WriteParameter("AlphaCmp", AlphaCmp);
            if (vers >= 47)
            {
                sw.WriteParameter("WriteZ", WriteZ);
            }
            if (vers >= 77)
            {
                sw.WriteParameter("Use3DCam", Use3DCam);
                if (vers >= 100)
                {
                    sw.WriteParameter("ModelPath", ModelPath);
                }
            }
        }
    }
}