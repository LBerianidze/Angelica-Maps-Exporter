using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelImporter
{
    public class SkiPreview
    {
        public EcmFile Ecm;
        public SmdFile smd;
        public byte[] SkiBytes;
        public SkiFile Ski;
        public Dictionary<string, byte[]> textures = new Dictionary<string, byte[]>();
    }
}
