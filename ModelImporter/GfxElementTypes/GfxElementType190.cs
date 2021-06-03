using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType190
    {
        public float Coeff;
        public float height;
        public GfxElementType190(StreamReaderL sr, int vers)
        {
            Coeff = sr.ReadLine().GetEcmLineValue().ToSingle();
            height = sr.ReadLine().GetEcmLineValue().ToSingle();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("Coeff", Coeff);
            sw.WriteParameter("height", height);
        }
    }

}
