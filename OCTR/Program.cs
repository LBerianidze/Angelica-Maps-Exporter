using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace OCTR
{
    class Program
    {
        static Dictionary<string, int> ConfigFile = new Dictionary<string, int>();
        static List<CGlobalSPMap> AirMaps = new List<CGlobalSPMap>();
        static float[] posGlobalOrigin = new float[3];
        static double m_fRecipSubMapSize;
        static double m_fRecipVoxelSize;
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            Console.WriteLine("Нажмите Enter что бы выбрать карту!!...");
            var hh = Console.ReadKey();
            if (hh.Key == ConsoleKey.Enter)
            {
                System.Windows.Forms.FolderBrowserDialog folderbr = new System.Windows.Forms.FolderBrowserDialog();
                if (folderbr.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(folderbr.SelectedPath + "\\airmap\\spmap.conf");
                    string str = " ";
                    while (true)
                    {
                        str = sr.ReadLine();
                        if (!str.Contains('=')) break;
                        string[] splitted = str.Split('=');
                        ConfigFile.Add(splitted[0], Convert.ToInt32(splitted[1]));
                    }
                    m_fRecipSubMapSize = 1.0 / (double)ConfigFile["Submap Size "];
                    m_fRecipVoxelSize = 1.0 / (double)ConfigFile["Voxel Size "];
                    posGlobalOrigin[0] = ConfigFile["Map Width "] * ConfigFile["Submap Size "] >> 1;
                    posGlobalOrigin[1] = 0;
                    posGlobalOrigin[2] = ConfigFile["Map Length "] * ConfigFile["Submap Size "] >> 1;
                    for (int i = 0; i < ConfigFile["Map Length "]; i++)
                    {
                        for (int g = 0; g < ConfigFile["Map Width "]; g++)
                        {
                            int iFileID = g + ConfigFile["Map Width "] * (ConfigFile["Map Length "] - i - 1) + 1;
                            string FilePath = folderbr.SelectedPath + $"\\airmap\\{iFileID}.octr";
                            if (File.Exists(FilePath))
                            {
                                CGlobalSPMap sp = new CGlobalSPMap();
                                BinaryReader br = new BinaryReader(File.Open(FilePath, FileMode.Open));
                                sp.uiVer = br.ReadInt32();
                                sp.unk1 = br.ReadByte();
                                for (int h = 0; h < 4; h++)
                                {
                                    sp.Cube[h] = br.ReadInt32();
                                }
                                sp.m_iLeafNodeSize = br.ReadInt32();
                                sp.m_iNodesNum = br.ReadUInt32();
                                sp.m_Nodes = new uint[sp.m_iNodesNum];
                                for (int y = 0; y < sp.m_iNodesNum; y++)
                                {
                                    sp.m_Nodes[y] = br.ReadUInt32();
                                }
                                br.Close();
                                AirMaps.Add(sp);
                            }
                        }
                    }
                }
            }
        }
    }
    class CGlobalSPMap
    {
        public int uiVer;
        public byte unk1;
        public int[] Cube = new int[4];
        public int m_iLeafNodeSize;
        public uint m_iNodesNum;
        public uint[] m_Nodes;
    }
}
