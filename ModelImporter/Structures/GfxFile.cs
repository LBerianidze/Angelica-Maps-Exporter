using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelImporter
{
    public class GfxFile
    {
        public List<string> Textures = new List<string>();
        public List<string> LinkedGfxs = new List<string>();
        public List<string> SmdModels = new List<string>();
        List<string> SamplerNameTextures = new List<string>(); //Default texture,but with prefix gfx
        public List<string> ShaderFiles = new List<string>(); //From shaders.pck
        public List<string> SoundFiles = new List<string>();
        public GfxFile(List<string> Lines)
        {
            /*Textures*/
            SamplerNameTextures.AddRange(Lines.Where(z => z.StartsWith("Sampler name:")).Select(f => f.Split(':').ElementAt(2).ToLower()).ToList());
            Textures.AddRange(Lines.Where(z => z.StartsWith("TexFile:")).Select(f => f.Split(':').ElementAt(1).Replace(" ", "").ToLower()).ToList());
            Textures.AddRange(Lines.Where(z => z.StartsWith("ShaderTex:") && z != "ShaderTex: ").Select(f =>f.Split(':').ElementAt(1).Replace(" ", "").ToLower()).ToList());
            /*End*/
            LinkedGfxs.AddRange(Lines.Where(z => z.StartsWith("GfxPath:")).Select(f => "gfx\\"+f.Split(':').ElementAt(1).Replace(" ", "").ToLower()).ToList());
            SmdModels.AddRange(Lines.Where(z => z.StartsWith("ModelPath:")).Select(f =>"gfx\\models\\"+ f.Split(':').ElementAt(1).Replace(" ", "").ToLower()).ToList());
            ShaderFiles.AddRange(Lines.Where(z =>z.ToLower().StartsWith("shaderfile:")).Select(f => "shaders\\" + f.Split(':').ElementAt(1).Replace(" ", "").ToLower()).ToList());
            SoundFiles.AddRange(Lines.Where(z => z.StartsWith("Path:")).Select(f => "sfx\\"+f.Split(':').ElementAt(1).Replace(" ","").ToLower()).ToList());
            foreach (var item in SamplerNameTextures)
            {
                Textures.Add(GetGfxPckPath(item));
            }
            for (int i = 0; i < Textures.Count; i++)
            {
                Textures[i] = "gfx\\textures\\" + Textures[i];
            }
            Textures.RemoveAll(t => t == " ");
            ShaderFiles.RemoveAll(t => t == "shaders\\");
        }
        string GetGfxPckPath(string un)
        {
            var sp = un.Split('\\');
            return string.Join("\\", sp.Skip(1));
        }
    }
}
