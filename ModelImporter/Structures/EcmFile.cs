using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelImporter
{
    public class EcmFile
    {
        public string SmdFileName = "";
        public List<string> GfxFiles = new List<string>();
        public List<string> AttFiles = new List<string>();
        public List<string> WavFiles = new List<string>();
        public List<string> AddSkinFiles = new List<string>();
        public List<string> ExtraEcms = new List<string>();
        public EcmFile(List<string> Lines)
        {
            var ts1 = Lines.FirstOrDefault(t => t.StartsWith("SkinModelPath:"));
            if (ts1 != null)
            {
                SmdFileName = ts1.Split(':').ElementAt(1).Replace(" ", "").ToLower();
            }
            var Files = Lines.Where(z => z.StartsWith("FxFilePath"));
            GfxFiles.AddRange(Files.Where(z => z.EndsWith(".gfx")).Select(z => "gfx\\" + z.Split(':').ElementAt(1).Replace(" ", "").ToLower()).ToList());
            AttFiles.AddRange(Lines.Where(t => t.StartsWith("AtkPath:")).Select(y => "gfx\\skillattack\\" + y.Split(':').ElementAt(1).Replace(" ", "").ToLower()).ToList());
            WavFiles.AddRange(Files.Where(z => z.EndsWith(".wav")).Select(z => "sfx\\" + z.Split(':').ElementAt(1).Replace(" ", "").ToLower()).ToList());
            GfxFiles = GfxFiles.GroupBy(z => z).Select(f => f.FirstOrDefault().ToLower()).ToList();
            WavFiles = WavFiles.GroupBy(z => z).Select(f => f.FirstOrDefault().ToLower()).ToList();
            AddSkinFiles = Lines.Where(t => t.StartsWith("AddiSkinPath:")).Select(y => y.Split(':').ElementAt(1).Replace(" ", "").ToLower()).ToList();
            ExtraEcms = Lines.Where(t => t.StartsWith("ChildPath:")).Select(y => y.Split(':').ElementAt(1).Replace(" ", "").ToLower()).ToList();
        }
    }
}
