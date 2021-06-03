using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckBmdReader
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelImporter.ArchiveEngine litmodels = new ModelImporter.ArchiveEngine();
            litmodels.Load(@"D:\GamesMailRu\Jade Dynasty\element\litmodels.pck");
            litmodels.ReadFileTable(litmodels.PckFile);
            var Files = litmodels.Files.Where(e => e.Path.EndsWith(".bmd"));
            int amount = 0;
            foreach (var item in Files)
            {
                try
                {
                    new BmdStructure(new System.IO.BinaryReader(new MemoryStream(litmodels.ReadFile(litmodels.PckFile, item))));
                }
                catch
                {
                    Console.WriteLine(item.Path);
                    amount++;
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("End "+amount+"\\"+Files.Count());
            Console.Beep(300, 5000);
            Console.ReadLine();
        }
    }
}
