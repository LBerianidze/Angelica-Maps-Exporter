using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MapsResearcher
{
    class Program
    {
        static void Main(string[] args)
        {
            string SourceDir = @"D:\GamesMailRu\Chi Bi\element\maps";
            var Dirs = Directory.GetDirectories(SourceDir);
            StreamWriter sw = new StreamWriter(@"C:\Users\Luka\Desktop\ChiBi Maps\ChiBi.txt");
            for (int i = 0; i < Dirs.Count(); i++)
            {
                string Ecwld = Directory.GetFiles(string.Format("{0}", Dirs[i]), "*.ecwld").ElementAtOrDefault(0);
                string Ecbsd = Directory.GetFiles(string.Format("{0}", Dirs[i]), "*.ecbsd").ElementAtOrDefault(0);
                if (File.Exists(Ecwld) && File.Exists(Ecbsd))
                {
                    BinaryReader WLD = new BinaryReader(File.Open(Ecwld, FileMode.Open));
                    BinaryReader BSD = new BinaryReader(File.Open(Ecbsd, FileMode.Open));
                    WLD.ReadBytes(4);
                    BSD.ReadBytes(8);
                    List<string> Splitter = Ecwld.Split(new char[] { '\\', '.' }).ToList();
                    int EcwldVersion = WLD.ReadInt32();
                    int EcbsdVersion = BSD.ReadInt32();
                    sw.WriteLine(string.Format("{0}                 {1}                 {2}", Splitter.ElementAt(Splitter.Count - 3), EcwldVersion, EcbsdVersion));
                    WLD.Close();
                    BSD.Close();
                    DirectoryCopy(SourceDir + "\\" + Splitter.ElementAt(Splitter.Count - 3), @"C:\Users\Luka\Desktop\ChiBi Maps\ChiBi\"+EcwldVersion+"-"+EcbsdVersion+"\\" + Splitter.ElementAt(Splitter.Count - 3), true);
                }
            }
            sw.Close();
        }
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }
            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
