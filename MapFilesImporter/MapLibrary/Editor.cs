using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapFilesImporter
{
    class Editor
    {
        public Map Map;

        public void Load(string ecwldPath, string ecbsdPath)
        {

            Map = new Map(ecwldPath, ecbsdPath);
        }

        public void Save(int ecwldVersion = 0, int ecbswVersion = 0)
        {
            ecwldVersion = Map.Ecwld.Version;
            ecbswVersion = Map.Ecbsd.Version;
            Map.Write("","",ecwldVersion, ecbswVersion);
        }
    }
}
