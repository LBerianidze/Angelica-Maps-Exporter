using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapFilesImporter
{
    public class EcmFile
    {
        public string Smd;
        public List<string> GfxFiles = new List<string>();
        public List<string> WavFiles = new List<string>();
        public EcmFile(string Path) : this(File.ReadAllLines(Path, Encoding.GetEncoding(936)).ToList())
        {
    
        }
        public EcmFile(List<string> Lines)
        {
            Smd = Lines.First(t => t.StartsWith("SkinModelPath:")).Split(':').ElementAt(1).Replace(" ", "");
            var Files = Lines.Where(z => z.StartsWith("FxFilePath"));
            GfxFiles.AddRange(Files.Where(z => z.EndsWith(".gfx")).Select(z => z.Split(':').ElementAt(1).Replace(" ", "")).ToList());
            WavFiles.AddRange(Files.Where(z => z.EndsWith(".wav")).Select(z => z.Split(':').ElementAt(1).Replace(" ", "")).ToList());
            GfxFiles = GfxFiles.GroupBy(z => z).Select(f => f.FirstOrDefault()).ToList();
            WavFiles = WavFiles.GroupBy(z => z).Select(f => f.FirstOrDefault()).ToList();
        }
    }
}
