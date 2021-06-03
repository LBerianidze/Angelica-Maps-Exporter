using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType230
    {
        public string ECMPath;
        public string ECMActName;
        public int LDType;
        public string UsrCmd;
        public GfxElementType230(StreamReaderL sr, int vers)
        {
            ECMPath = sr.ReadLine().GetEcmLineValue();
            ECMActName = sr.ReadLine().GetEcmLineValue();
            LDType = sr.ReadLine().GetEcmLineValue().ToInt32();
            UsrCmd = sr.ReadLine().GetEcmLineValue();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("ECMPath", ECMPath);
            sw.WriteParameter("ECMActName", ECMActName);
            sw.WriteParameter("LDType", LDType);
            sw.WriteParameter("UsrCmd", UsrCmd);

        }
    }

}
