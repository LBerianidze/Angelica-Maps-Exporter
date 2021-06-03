using DevIL;
using DevIL.Unmanaged;
using LBLIBRARY;
using ModelImporter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
namespace MapFilesImporter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Map Files Importer By Luka v1.0 - Licensed to Freeshare";
            if (File.Exists(Application.StartupPath + "\\settings.xml"))
            {
                XmlTextReader xml = new XmlTextReader(Application.StartupPath + "\\settings.xml");
                try
                {
                    Options = new OptionsForm(this);
                    if (File.Exists(string.Format(Application.StartupPath + "\\Settings.xml")))
                    {
                        using (System.Xml.XmlTextReader rg = new XmlTextReader(string.Format(Application.StartupPath + "\\Settings.xml")))
                        {
                            rg.ReadToFollowing("Elements_path");
                            ElementPath = rg.ReadElementContentAsString();
                            LoadPcks(ElementPath);
                            rg.ReadToFollowing("Game");
                            string tt = rg.ReadElementContentAsString();
                            switch (tt)
                            {
                                case "Pw": ChosenGame = Game.Pw; break;
                                case "Fw": ChosenGame = Game.Fw; break;
                                case "Jd": ChosenGame = Game.Jd; break;
                                case "Loma": ChosenGame = Game.Loma; break;
                                case "HoTK": ChosenGame = Game.HoTK; break;
                                case "Eso": ChosenGame = Game.Eso; break;
                                default:
                                    ChosenGame = Game.Pw;
                                    break;
                            }
                            rg.ReadToFollowing("Destination_Path");
                            NonModified_DestinationPath = rg.ReadElementContentAsString();
                            DestinationTextBox.Text = NonModified_DestinationPath;
                            rg.ReadToFollowing("CreateNewDirectory");
                            CreateNewDirectory.Checked = Convert.ToBoolean(rg.ReadElementContentAsString());
                            rg.ReadToFollowing("Selected_Map");
                            int ind = Convert.ToInt32(rg.ReadElementContentAsString());
                            Options.RefreshInfo(ElementPath, ChosenGame);
                            ModifePaths();
                            if (Options.CheckPath() == true)
                            {
                                RefreshCombobox();
                                if (MapsCombobox.Items.Count > ind)
                                {
                                    MapsCombobox.SelectedIndex = ind;
                                }
                            }
                        }
                    }
                }
                catch (Exception) { }
                xml.Close();
            }
            buttonZ1.Focus();
        }
        string SelectedMap;
        OptionsForm Options;
        public string NonModified_DestinationPath;
        public string ElementPath;
        public string MapsPath;
        public string DestinationPath;
        public Game ChosenGame = Game.Pw;
        List<string> Loddata;
        List<string> TextureFiles;
        List<string> LitModelsFiles;
        List<string> BuildingTextFiles;
        List<string> SmdFiles;
        List<string> EcmFiles;
        List<string> ExtraFilesInModels; // Contains: stck,ski,bon
        List<string> EffectFiles;
        List<string> SfxWavFiles;
        List<string> EffectTextFiles;
        List<string> ShadersFiles;
        List<string> GfxSmds;
        List<string> GrassFiles;
        List<string> TreeFiles;
        List<string> ExtraModelFilesInGfx;// Contains: stck,ski,bon
        List<Unknown2> BhtInfo = new List<Unknown2>();
        SizeF MapSize;
        bool InProcess;
        static string LogPath;
        public Dictionary<string, ModelImporter.ArchiveEngine> Pcks = new Dictionary<string, ModelImporter.ArchiveEngine>();
        public bool FromPck = true;
        Map GameMap;
        string TexturesNameTo = "building\textures";
        public void SearchBmds(Map gm)
        {
            float x = 2361;
            float z = 2211;
            List<Bbbb> keys = new List<Bbbb>();
            foreach (var item in gm.Ecwld.Blocks)
            {
                var ssb = item.Value.Textures.Where(t => Math.Abs(Math.Abs(t.Value.X) - x) < 250 && Math.Abs(Math.Abs(t.Value.Z) - z) < 250);
                foreach (var it in ssb)
                {
                    keys.Add(new Bbbb(item.Key, it.Key));
                }
            }
            foreach (var item in keys)
            {
                gm.Ecwld.Blocks[item.BlockKey].Litmodels.Remove(item.LitModelKey);
            }
            gm.Write(@"C:\Users\Luka\Desktop\world.ecwld", @"C:\Users\Luka\Desktop\world.ecbsd", 12, 12);
        }
        private void StartImporting_Click(object sender, EventArgs e)
        {
            if (MapsCombobox.SelectedIndex == -1)
            {
                MessageBox.Show("Карта не выбрана!!...", "MapImporter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (InProcess == false)
                {
                    if (Directory.Exists(DestinationTextBox.Text))
                    {
                        TexturesNameTo = "building\\" + NewBmdTextureName.Text;
                        SelectedMap = MapsCombobox.SelectedItem.ToString();
                        if (!Directory.Exists(Application.StartupPath + "\\logs"))
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\logs");
                        }
                        ChangeProgress(0, "Чтение Ecwld\\Ecbsd:");
                        LogPath = Application.StartupPath + "\\logs\\Map(" + SelectedMap + ").Time(" + DateTime.Now.ToString().Replace(":", "-") + ").log";
                        var MapWld = Directory.GetFiles(MapsPath + $"\\{SelectedMap}", "*.ecwld", SearchOption.TopDirectoryOnly).ToList();
                        var MapBsd = Directory.GetFiles(MapsPath + $"\\{SelectedMap}", "*.ecbsd", SearchOption.TopDirectoryOnly).ToList();
                        GameMap = new Map(MapWld[0], MapBsd[0]);
                        new Thread(() =>
                        {
                            InProcess = true;
                            GenerateFilePaths(GameMap, SelectedMap);
                            CheckDestinationDirectory(SelectedMap);
                            CopyFiles();
                            GameMap.Write(DestinationPath + "\\maps\\" + SelectedMap + "\\" + SelectedMap + ".ecwld", DestinationPath + "\\maps\\" + SelectedMap + "\\" + SelectedMap + ".ecbsd", 12, 12);
                            InProcess = false;
                        }).Start();
                    }
                }
            }
        }
        public void GenerateFilePaths(Map GameMap, string SelectedMap)
        {
            Loddata = new List<string>();
            TextureFiles = new List<string>();
            LitModelsFiles = new List<string>();
            BuildingTextFiles = new List<string>();
            SmdFiles = new List<string>();
            EcmFiles = new List<string>();
            EffectFiles = new List<string>();
            SfxWavFiles = new List<string>();
            EffectTextFiles = new List<string>();
            ShadersFiles = new List<string>();
            ExtraFilesInModels = new List<string>();
            GfxSmds = new List<string>();
            ExtraModelFilesInGfx = new List<string>();
            GrassFiles = new List<string>();
            TreeFiles = new List<string>();
            BhtInfo = new List<Unknown2>();
            if (GameMap.Ecwld == null)
                return;
            MapSize = new SizeF(GameMap.Ecwld.Size[0], GameMap.Ecwld.Size[1]);
            ChangeProgress(GameMap.Ecwld.Blocks.Count, "Сбор информации о необходимых файлах:");
            foreach (var Block in GameMap.Ecwld.Blocks)
            {
                IncrementValue("");
                foreach (var item in Block.Value.Textures)
                {
                    TextureFiles.Add(string.Format("textures\\sky\\{0}", item.Value.Sky1.ToLower()));
                    TextureFiles.Add(string.Format("textures\\sky\\{0}", item.Value.Sky2.ToLower()));
                    TextureFiles.Add(string.Format("textures\\sky\\{0}", item.Value.Sky3.ToLower()));
                    TextureFiles.Add(string.Format("textures\\sky\\{0}", item.Value.Sky4.ToLower()));
                    TextureFiles.Add(string.Format("textures\\sky\\{0}", item.Value.Sky5.ToLower()));
                    TextureFiles.Add(string.Format("textures\\sky\\{0}", item.Value.Sky6.ToLower()));
                    SfxWavFiles.Add("sfx\\" + item.Value.Music.ToLower());
                }
                foreach (var item in Block.Value.Litmodels)
                {
                    LitModelsFiles.Add(string.Format("litmodels\\{0}\\{1}", SelectedMap, item.Value.ModelPath.ToLower()));
                }
                foreach (var item in Block.Value.SmdModels)
                {
                    SmdFiles.Add(item.Value.ModelPath.ToLower());
                }
                foreach (var item in Block.Value.EcmModels)
                {
                    EcmFiles.Add(item.Value.EcmPath.ToLower());
                }
                foreach (var item in Block.Value.EffectData)
                {
                    EffectFiles.Add("gfx\\" + item.Value.GfxPath.ToLower());
                }
                foreach (var item in Block.Value.SoundObjects)
                {
                    SfxWavFiles.Add("sfx\\" + item.Value.WavPath.ToLower());
                }
            }
            Trn2 trn2file = new Trn2(MapsPath + "\\" + SelectedMap + "\\" + SelectedMap + ".trn2");
            //Трава
            GrassFiles = GameMap.Ecwld.Grasses.Select(t => t.Mox).GroupBy(v => v).Select(i => i.FirstOrDefault().ToLower()).ToList();
            //Деревья,текстуры и сами файлы
            TreeFiles = GameMap.Ecwld.Trees.Select(t => t.Dds).GroupBy(v => v).Select(i => i.FirstOrDefault().ToLower()).ToList();
            TreeFiles.AddRange(GameMap.Ecwld.Trees.Select(t => t.Spt).GroupBy(v => v).Select(i => i.FirstOrDefault().ToLower()));
            //Текстуры неба и земли карты
            TextureFiles = TextureFiles.GroupBy(t => t).Select(u => u.FirstOrDefault()).ToList();
            TextureFiles.AddRange(trn2file.Textures.Select(t => t.ToLower()));
            //файлы из loddata
            Loddata = Pcks["loddata"].Files.Where(e => e.Path.StartsWith("loddata\\" + SelectedMap)).Select(f => f.Path).ToList();
            //
            ReadLitmodels();
            ReadEcms();
            ReadGfxs();
            ReadGfxSmds();
            ReadSmds();
            ReadSkies();
            ReadGfxSkies();
            ReadMoxFiles();
            SfxWavFiles = SfxWavFiles.GroupBy(t => t).Select(u => u.FirstOrDefault()).ToList();
            SfxWavFiles.RemoveAll(b => b.Count(f => f == '\\') == 1);
        }
        public void CopyFiles()
        {
            ChangeProgress(TextureFiles.Count, "Копирование текстур карты");
            foreach (var item in TextureFiles)
            {
                IncrementValue(item);
                byte[] b = ReadFile("textures", item);
                if (b.Length != 0)
                {
                    WriteFile(b, DestinationPath + item);
                }
                else
                {
                    WriteLog(item);
                }
            }
            ChangeProgress(GrassFiles.Count, "Копирование файлов с архива grasses");
            foreach (var item in GrassFiles)
            {
                IncrementValue(item);
                byte[] b = ReadFile("grasses", item);
                if (b.Length != 0)
                {
                    WriteFile(b, DestinationPath + item);
                }
                else
                {
                    WriteLog(item);
                }
            }
            ChangeProgress(Loddata.Count, "Копирование файлов с архива loddata");
            foreach (var item in Loddata)
            {
                IncrementValue(item);
                byte[] b = ReadFile("loddata", item);
                if (b.Length != 0)
                {
                    WriteFile(b, DestinationPath + item);
                }
                else
                {
                    WriteLog(item);
                }
            }
            ChangeProgress(TreeFiles.Count, "Копирование файлов с архива trees");
            foreach (var item in TreeFiles)
            {
                IncrementValue(item);
                byte[] b = ReadFile("trees", item);
                if (b.Length != 0)
                {
                    WriteFile(b, DestinationPath + item);
                }
                else
                {
                    WriteLog(item);
                }
            }
            BmdStructure.TexturesNameTo = checkBox1.Checked ? TexturesNameTo : "building\\textures";
            ChangeProgress(LitModelsFiles.Count, "Копирование файлов .bmd с архива litmodels");
            foreach (var item in LitModelsFiles)
            {
                IncrementValue(item);
                try
                {
                    byte[] b = ReadFile("litmodels", item);
                    MemoryStream mem = new MemoryStream();
                    mem.Position = 0;
                    BinaryWriter bw = new BinaryWriter(mem);
                    BmdStructure bmd = new BmdStructure(new BinaryReader(new MemoryStream(b)), true);
                    bmd.Save(bw);
                    bw.Flush();
                    mem.Seek(0, SeekOrigin.Begin);
                    if (mem.Length != 0)
                    {
                        WriteFile(mem.ToArray(), DestinationPath + item);
                    }
                    else
                    {
                        WriteLog(item);
                    }
                }
                catch (Exception)
                {
                }
            }
            ChangeProgress(BuildingTextFiles.Count, "Копирование текстур построек с архива building");
            foreach (var item in BuildingTextFiles)
            {
                IncrementValue(item);
                byte[] b = ReadFile("building", item);
                if (b.Length != 0)
                {
                    string newitem = TexturesNameTo + item.Substring(17, item.Length - 17);
                    WriteFile(b, DestinationPath + newitem);
                }
                else
                {
                    WriteLog(item);
                }
            }
            ChangeProgress(SmdFiles.Count, "Копирование файлов smd с архива models");
            foreach (var item in SmdFiles)
            {
                IncrementValue(item);
                byte[] b = ReadFile(item.Split('\\')[0], item);
                if (b.Length != 0)
                {
                    WriteFile(b, DestinationPath + item);
                }
                else
                {
                    WriteLog(item);
                }
            }
            ChangeProgress(EcmFiles.Count, "Копирование файлов ecm с архива models");
            foreach (var item in EcmFiles)
            {
                IncrementValue(item);
                string pck = item.Split('\\')[0];
                byte[] s = ReadFile("models", item);
                if (s.Length != 0)
                {
                    if (item.EndsWith(".ecm") && FixEcm.Checked)
                    {
                        try
                        {
                            EcmFileStructure ecmfile = new EcmFileStructure(s);
                            ecmfile.FixToPW();
                            MemoryStream mem = new MemoryStream();
                            StreamWriter sw = new StreamWriter(mem, Encoding.GetEncoding(936));
                            int v = ecmfile.Version;
                            if (ForLowVersion.Checked && ecmfile.Version > 32)
                            {
                                v = 32;
                            }
                            ecmfile.Save(sw, v);
                            sw.Flush();
                            mem.Seek(0, SeekOrigin.Begin);
                            WriteFile(mem.ToArray(), DestinationPath + item);
                        }
                        catch
                        {
                            WriteFile(s, DestinationPath + item);
                        }
                        continue;
                    }
                    WriteFile(s, DestinationPath + item);
                }
                else
                {
                    WriteLog(item);
                }
            }
            ChangeProgress(ExtraFilesInModels.Count, "Копирование дополнительных файлов из models");
            foreach (var item in ExtraFilesInModels)
            {
                IncrementValue(item);
                byte[] b = ReadFile("models", item);
                if (b.Length != 0)
                {
                    WriteFile(b, DestinationPath + item);
                }
                else
                {
                    WriteLog(item);
                }
            }
            ChangeProgress(EffectFiles.Count, "Копирование эффектов с архива gfx");
            foreach (var item in EffectFiles)
            {
                IncrementValue(item);
                byte[] s = ReadFile("gfx", item);
                if (s.Length != 0)
                {
                    if (item.EndsWith(".gfx") && FixGfx.Checked)
                    {
                        try
                        {
                            var enc = new StreamReaderL(s, Encoding.GetEncoding(936));
                            GfxReader.GfxStructure gfx = new GfxReader.GfxStructure(enc);
                            if (gfx?.Version > 58 && gfx != null)
                            {
                                MemoryStream mem = new MemoryStream
                                {
                                    Position = 0
                                };
                                StreamWriter sw = new StreamWriter(mem, Encoding.GetEncoding(936));
                                gfx.Save(sw, (int)GfxFixToVersion.Value, false);
                                sw.Flush();
                                mem.Seek(0, SeekOrigin.Begin);
                                WriteFile(mem.ToArray(), DestinationPath + item);
                            }
                            else
                            {
                                WriteFile(s, DestinationPath + item);
                            }
                        }
                        catch
                        {
                            WriteLog("Error occured while fix file: " + item);
                            WriteFile(s, DestinationPath + item);
                        }
                    }
                    else
                    {
                        WriteFile(s.ToArray(), DestinationPath + item);
                    }
                }
                else
                {
                    WriteLog(item);
                }
            }
            ChangeProgress(SfxWavFiles.Count, "Копирование эффектов звука с архива sfx");
            foreach (var item in SfxWavFiles)
            {
                IncrementValue(item);
                byte[] b = ReadFile("sfx", item);
                if (b.Length != 0)
                {
                    WriteFile(b, DestinationPath + item);
                }
                else
                {
                    WriteLog(item);
                }
            }
            ChangeProgress(EffectTextFiles.Count, "Копирование текстур эффектов  с архива gfx");
            foreach (var item in EffectTextFiles)
            {
                IncrementValue(item);
                byte[] b = ReadFile("gfx", item);
                if (b.Length != 0)
                {
                    WriteFile(b, DestinationPath + item);
                }
                else
                {
                    WriteLog(item);
                }
            }
            ChangeProgress(ShadersFiles.Count, "Копирование файлов с архива shaders");
            foreach (var item in ShadersFiles)
            {
                IncrementValue(item);
                byte[] b = ReadFile("shaders", item);
                if (b.Length != 0)
                {
                    WriteFile(b, DestinationPath + item);
                }
                else
                {
                    WriteLog(item);
                }
            }
            ChangeProgress(GfxSmds.Count, "Копирование файлов smd с архива gfx");
            foreach (var item in GfxSmds)
            {
                IncrementValue(item);
                byte[] b = ReadFile("gfx", item);
                if (b.Length != 0)
                {
                    WriteFile(b, DestinationPath + item);
                }
                else
                {
                    WriteLog(item);
                }
            }
            ChangeProgress(ExtraModelFilesInGfx.Count, "Копирование дополнительных файлов моделей с архива gfx");
            foreach (var item in ExtraModelFilesInGfx)
            {
                IncrementValue(item);
                byte[] b = ReadFile("gfx", item);
                if (b.Length != 0)
                {
                    WriteFile(b, DestinationPath + item);
                }
                else
                {
                    WriteLog(item);
                }
            }
            {

                ChangeProgress(1, "Генерирование и копирование файлов surfaces");
                Bitmap bm = LoadDDSImage(ReadFile("surfaces", "surfaces\\midmaps\\" + SelectedMap + ".dds"), true);
                if (bm != null)
                {
                    bm = ResizeImage(bm, (int)GameMap.Ecwld.Size[0] / 2, (int)GameMap.Ecwld.Size[1] / 2);
                    int WidthCount = bm.Width / 256;
                    int HeightCount = bm.Height / 256;
                    int X = 0;
                    int Y = 0;
                    for (int z = 0; z < HeightCount; z++)
                    {
                        for (int i = 0; i < WidthCount; i++)
                        {
                            MemoryStream mem = SplitAndSave(bm, X, Y, 256, 256);
                            string v = z.ToString();
                            string t = i.ToString();
                            if (v.Length == 1)
                            {
                                v = "0" + v;
                            }
                            if (t.Length == 1)
                            {
                                t = "0" + t;
                            }
                            string item = $"surfaces\\minimaps\\{SelectedMap}\\{v}{t}.dds";
                            WriteFile(mem.ToArray(), DestinationPath + item);
                            X += 256;
                        }
                        Y += 256;
                        X = 0;
                    }
                }
            }
            byte[] bs = ReadFile("surfaces", "surfaces\\minimaps\\" + SelectedMap + ".dds");
            if (bs.Length != 0)
            {
                WriteFile(bs, DestinationPath + "surfaces\\ingame\\" + SelectedMap + ".dds");
            }
            IncrementValue("");
            ChangeProgress(0, "Копирование основных файлов карты", false);
            DirectoryCopy(MapsPath + "\\" + SelectedMap, DestinationPath + "\\maps\\" + SelectedMap, true);
            if (!Directory.Exists(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap))
            {
                Directory.CreateDirectory(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap);
            }
            if (CreateBht.Checked)
            {
                ChangeProgress(0, "Создаем .bht файл", false);
                BhtFile.WriteBht(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + "\\map.bht", BhtInfo, MapSize);
            }
            if (CreateHMap.Checked)
            {
                var t2bkFilePaths = Directory.GetFiles(MapsPath + "\\" + SelectedMap, "*.t2bk");
                ChangeProgress(t2bkFilePaths.Count() * 2, "Создание файлов .hmap");
                List<float[]> HmapFloats = new List<float[]>();
                for (int i = 0; i < t2bkFilePaths.Count(); i++)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        ImportingProgress.Value++;
                    }));
                    HmapFloats.Add(new T2bkFile(t2bkFilePaths[i]).t1map);
                }
                if (HmapFloats.Count != 0 && !Directory.Exists(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + "\\map"))
                {
                    Directory.CreateDirectory(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + "\\map");
                }
                for (int i = 0; i < HmapFloats.Count; i++)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        ImportingProgress.Value++;
                        CopyingFile.Text = DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + "\\map\\" + (i + 1) + ".hmap";
                    }));
                    BinaryWriter bw0 = new BinaryWriter(File.Create(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + "\\map\\" + (i + 1) + ".hmap"));
                    foreach (var item in HmapFloats[i])
                    {
                        bw0.Write(item);
                    }
                    bw0.Close();
                }
                BinaryWriter bw = new BinaryWriter(File.Create(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + "\\" + "path.sev"));
                bw.Write(1);
                bw.Write(0);
                bw.Close();
                BinaryWriter bw1 = new BinaryWriter(File.Create(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + "\\" + "precinct.sev"));
                bw1.Write(7);
                bw1.Write(0);
                bw1.Write(1382012810);
                bw1.Close();
                BinaryWriter bw2 = new BinaryWriter(File.Create(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + "\\" + "region.sev"));
                bw2.Write(5);
                bw2.Write(0);
                bw2.Write(0);
                bw2.Write(1382012810);
                bw2.Close();
                BinaryWriter bw3 = new BinaryWriter(File.Create(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + "\\" + "npcgen.data"));
                bw3.Write(10);
                bw3.Write(0);
                bw3.Write(0);
                bw3.Write(0);
                bw3.Write(0);
                bw3.Close();
                if (!Directory.Exists(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + "\\airmap"))
                {
                    Directory.CreateDirectory(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + "\\airmap");
                }
                StreamWriter sw = new StreamWriter(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + "\\airmap\\spmap.conf", false, Encoding.GetEncoding(936));
                sw.WriteLine("Map Width = " + (int)(GameMap.Ecwld.Size[0] / 1024));
                sw.WriteLine("Map Length = " + (int)(GameMap.Ecwld.Size[1] / 1024));
                sw.WriteLine("Submap Size = 1024");
                sw.WriteLine("Voxel Size = 2");
                sw.WriteLine(";格式固定，切勿修改除了数字之外的其他部分，包括顺序");
                sw.Close();
                for (int i = 0; i < GameMap.Ecwld.Size[0] / 1024; i++)
                {
                    for (int t = 0; t < GameMap.Ecwld.Size[1] / 1024; t++)
                    {
                        BinaryWriter bw4 = new BinaryWriter(File.Create(DestinationPath + "\\home\\gamed\\config\\" + SelectedMap + $"\\airmap\\{(i * t) + 1}.octr"));
                        bw4.Write(-872415231);
                        bw4.Write((byte)1);
                        bw4.Write(1243532);
                        bw4.Write(1243532);
                        bw4.Write(1943855267);
                        bw4.Write(13);
                        bw4.Write(1943831216);
                        bw4.Write(0);
                        bw4.Close();
                    }
                }
            }

            this.BeginInvoke(new MethodInvoker(delegate
            {
                ImportingProgress.Value = 0;
                CopyingFile.Text = "";
                ProgressText.Text = "Экспортирование карты успешно завершено";

            }));
        }
        public MemoryStream SplitAndSave(Bitmap source, int X, int Y, int Width, int Height)
        {
            Bitmap b = new Bitmap(Width, Height);
            Graphics gr = Graphics.FromImage(b);
            gr.DrawImage(source, 0, 0, new RectangleF(X, Y, Width, Height), GraphicsUnit.Pixel);
            gr.Save();
            MemoryStream mem = new MemoryStream();
            b.Save(mem, ImageFormat.Png);
            mem.Close();
            LoadDDSImage(mem.ToArray(), false);
            IL.SaveImageToStream(ImageType.Dds, mem);
            return mem;
        }
        Bitmap ResizeImage(Bitmap source, int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(bm);
            gr.DrawImage(source, 0, 0, width, height);
            gr.Save();
            return bm;
        }
        public static Bitmap LoadDDSImage(byte[] ByteArray, bool shutdown)
        {
            Bitmap bitmap = null;
            IL.Initialize();
            IL.Enable(ILEnable.AbsoluteFormat);
            IL.SetDataFormat(DataFormat.BGRA);
            IL.Enable(ILEnable.AbsoluteType);
            IL.SetDataType(DataType.UnsignedByte);
            MemoryStream stream = new MemoryStream(ByteArray);
            if (IL.LoadImageFromStream(stream))
            {
                ImageInfo imageInfo = IL.GetImageInfo();
                bitmap = new Bitmap(imageInfo.Width, imageInfo.Height, PixelFormat.Format32bppArgb);
                Rectangle rect = new Rectangle(0, 0, imageInfo.Width, imageInfo.Height);
                BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                IntPtr data = bitmapdata.Scan0;
                IL.CopyPixels(0, 0, 0, imageInfo.Width, imageInfo.Height, 1, DataFormat.BGRA, DataType.UnsignedByte, data);
                bitmap.UnlockBits(bitmapdata);
            }
            if (shutdown)
                IL.Shutdown();
            stream.Close();
            return bitmap;
        }

        private void Options_Click(object sender, EventArgs e)
        {
            Options.ShowDialog();
        }
        private void SearchDestinationDirectory_Click(object sender, EventArgs e)
        {
            if (DestinationDialog.ShowDialog() == DialogResult.OK)
            {
                DestinationTextBox.Text = DestinationDialog.SelectedPath;
                NonModified_DestinationPath = DestinationTextBox.Text;
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            XmlTextWriter vt = new XmlTextWriter(Application.StartupPath + "\\Settings.xml", Encoding.UTF8)
            {
                Formatting = Formatting.Indented,
                IndentChar = '\t',
                Indentation = 1,
                QuoteChar = '\''

            };
            vt.WriteStartDocument();
            vt.WriteStartElement("root");
            vt.WriteStartAttribute("ProductName");
            vt.WriteString("MapFilesImporter");
            vt.WriteEndAttribute();
            vt.WriteStartElement("Settings");
            vt.WriteElementString("Version", "1.0");
            vt.WriteElementString("Elements_path", ElementPath);
            vt.WriteElementString("Game", ChosenGame.ToString());
            vt.WriteElementString("Destination_Path", DestinationTextBox.Text);
            vt.WriteElementString("CreateNewDirectory", CreateNewDirectory.Checked.ToString());
            vt.WriteElementString("Selected_Map", MapsCombobox.SelectedIndex.ToString());
            vt.WriteEndElement();
            vt.WriteEndDocument();
            vt.Close();
            Environment.Exit(0);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
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
        private void CheckDestinationDirectory(string CMap)
        {
            if (CreateNewDirectory.Checked == true)
            {
                DestinationPath = string.Format("{0}\\{1}\\", DestinationTextBox.Text, CMap);
                if (!Directory.Exists(DestinationPath))
                {
                    Directory.CreateDirectory(DestinationPath);
                }
            }
            else
            {
                DestinationPath = DestinationTextBox.Text;
            }

        }
        public void RefreshCombobox()
        {
            MapsCombobox.Items.Clear();
            var Maps = Directory.GetDirectories(MapsPath).ToList().Select(z => z.Split(new char[] { '\\' }).Last()).ToList();
            List<string> SortedMaps = new List<string>();
            for (int i = 0; i < Maps.Count; i++)
            {
                var MapWld = Directory.GetFiles(MapsPath + $"\\{Maps[i]}", "*.ecwld", SearchOption.TopDirectoryOnly).ToList();
                var MapBsd = Directory.GetFiles(MapsPath + $"\\{Maps[i]}", "*.ecbsd", SearchOption.TopDirectoryOnly).ToList();
                if (MapWld.Count > 0 && MapBsd.Count > 0)
                {
                    var MapSplittedWld = MapWld[0].Split('.');
                    var MapSplittedBsd = MapBsd[0].Split('.');
                    if (MapSplittedWld[0] == MapSplittedBsd[0])
                    {
                        SortedMaps.Add(Maps[i]);
                    }
                }

            }
            MapsCombobox.Items.AddRange(SortedMaps.ToArray());
        }
        public void ModifePaths()
        {
            MapsPath = ElementPath + "\\maps";
        }
        public void WriteFile(byte[] Source, string Dest)
        {
            List<string> SplittedDirectory = Dest.Split(new char[] { '\\' }).ToList();
            string DestinationDirectory = string.Join("\\", SplittedDirectory.Take(SplittedDirectory.Count - 1));
            if (!Directory.Exists(DestinationDirectory))
            {
                Directory.CreateDirectory(DestinationDirectory);
            }
            File.WriteAllBytes(Dest, Source);
        }
        public byte[] ReadFile(string pck, string file)
        {
            if (FromPck)
            {
                if (Pcks.ContainsKey(pck))
                    return Pcks[pck].ReadFile(Pcks[pck].PckFile, Pcks[pck].Files.Find(b => b.Path == file));

                WriteLog(file);
                return new byte[0];
            }
            else
            {
                string p = ElementPath + '\\' + pck + ".pck.files\\" + file;
                if (File.Exists(p))
                    return File.ReadAllBytes(p);

                WriteLog(p);
                return new byte[0];
            }
        }
        public static void WriteLog(string text)
        {
            if (text.EndsWith(".mphy"))
                return;
            StreamWriter sw = new StreamWriter(LogPath, true, Encoding.UTF8);
            sw.WriteLine(text);
            sw.Close();

        }
        public bool FileExists(string pck, string file)
        {
            if (FromPck)
            {
                if (Pcks.ContainsKey(pck))
                    return Pcks[pck].Files.FindIndex(b => b.Path == file) != -1;

                WriteLog(file);
                return false;
            }
            else
            {
                string p = ElementPath + '\\' + pck + ".pck.files\\" + file;
                if (File.Exists(p))
                    return true;

                WriteLog(p);
                return false;
            }
        }
        public void ReadLitmodels()
        {
            LitModelsFiles = LitModelsFiles.GroupBy(s => s).Select(grp => grp.FirstOrDefault()).OrderBy(s => s).ToList();
            ChangeProgress(LitModelsFiles.Count, "Чтение файлов .bmd:");
            foreach (var item in LitModelsFiles)
            {
                try
                {
                    IncrementValue(item);
                    byte[] b = ReadFile("litmodels", item);
                    if (b.Length < 10)
                        continue;
                    BmdStructure bmd = new BmdStructure(new BinaryReader(new MemoryStream(b)));
                    BuildingTextFiles.AddRange(bmd.Textures.Select(t => t.ToLower()));
                    BhtInfo.AddRange(bmd.BhtInfo);
                }
                catch { GameMap.BrokenLitmodels.Add(item); }
            }
            LitModelsFiles.RemoveAll(v => GameMap.BrokenLitmodels.Contains(v));
            BuildingTextFiles = BuildingTextFiles.GroupBy(s => s).Select(grp => grp.FirstOrDefault()).OrderBy(s => s).ToList();
        }
        public void ReadEcms()
        {
            EcmFiles.AddRange(SmdFiles.Where(z => z.EndsWith(".ecm") || z.EndsWith(".ECM")));
            EcmFiles = EcmFiles.GroupBy(s => s).Select(grp => grp.FirstOrDefault()).OrderBy(s => s).ToList();
            ChangeProgress(EcmFiles.Count, "Чтение файлов .ecm:");
            List<string> ExtraEcms = new List<string>();
            foreach (var item in EcmFiles)
            {
                IncrementValue(item);
                ReadEcm(item, ExtraEcms);
            }
            EcmFiles.AddRange(ExtraEcms);
        }
        public void ReadEcm(string path, List<string> ExtraEcm)
        {
            byte[] b = ReadFile("models", path);
            if (b.Length < 10)
                return;
            ModelImporter.EcmFile ec = new ModelImporter.EcmFile(new MemoryStream(b).ToLines(Encoding.GetEncoding(936)));
            EffectFiles.AddRange(ec.GfxFiles.Select(z => z));
            SmdFiles.Add(ec.SmdFileName);
            SfxWavFiles.AddRange(ec.WavFiles.Select(z => z));
            ExtraEcm.AddRange(ec.ExtraEcms);
            foreach (var item in ec.ExtraEcms)
            {
                ReadEcm(item, ExtraEcm);
            }
        }
        public void ReadGfxs()
        {
            EffectFiles = EffectFiles.GroupBy(s => s).Select(grp => grp.FirstOrDefault()).OrderBy(s => s).ToList();
            List<string> LinkedGfxs = new List<string>();
            ChangeProgress(EffectFiles.Count, "Чтение файлов .gfx:");
            foreach (var item in EffectFiles)
            {
                IncrementValue(item);
                ReadGfxFile(item, ref LinkedGfxs);
            }
            EffectFiles.AddRange(LinkedGfxs);
            ShadersFiles = ShadersFiles.GroupBy(s => s).Select(grp => grp.FirstOrDefault()).OrderBy(s => s).ToList();
            EffectTextFiles = EffectTextFiles.GroupBy(s => s).Select(grp => grp.FirstOrDefault()).OrderBy(s => s).ToList();
            EffectFiles = EffectFiles.GroupBy(s => s).Select(grp => grp.FirstOrDefault()).OrderBy(s => s).ToList();
        }
        private void ReadGfxFile(string path, ref List<string> linked)
        {
            string gfxpck = path.Split('\\')[0];
            MemoryStream s = new MemoryStream(ReadFile(gfxpck, path));
            if (s.Length < 10)
            {
                return;
            }
            List<string> GfxLines = s.ToLines(Encoding.GetEncoding(936));
            GfxFile gfx = new GfxFile(GfxLines);
            EffectTextFiles.AddRange(gfx.Textures);
            linked.AddRange(gfx.LinkedGfxs);
            ShadersFiles.AddRange(gfx.ShaderFiles);
            SfxWavFiles.AddRange(gfx.SoundFiles);
            GfxSmds.AddRange(gfx.SmdModels);
            foreach (var item in gfx.LinkedGfxs)
            {
                ReadGfxFile(item, ref linked);
            }
        }
        public void ReadGfxSmds()
        {
            GfxSmds = GfxSmds.GroupBy(f => f).Select(t => t.FirstOrDefault()).ToList();
            ChangeProgress(GfxSmds.Count, "Чтение файлов .smd из gfx.pck:");
            foreach (var item in GfxSmds)
            {
                IncrementValue(item);
                byte[] b = ReadFile("gfx", item);
                if (b.Length == 0)
                    continue;
                ModelImporter.SmdFile sm = new ModelImporter.SmdFile(b);
                string[] SplittedSmdPath = item.Split('\\');
                if (sm.StckDirectory != null)
                {
                    ExtraModelFilesInGfx.Add(string.Join("\\", SplittedSmdPath.Take(SplittedSmdPath.Length - 1)) + "\\" + SplittedSmdPath.Last().Split('.')[0] + ".stck");
                    foreach (var it in sm.Actions)
                    {
                        ExtraModelFilesInGfx.Add(string.Join("\\", SplittedSmdPath.Take(SplittedSmdPath.Length - 1)) + "\\" + sm.StckDirectory + "\\" + it.ActionFileName);
                    }
                }
                if (sm.BonFile != "")
                {
                    ExtraModelFilesInGfx.Add(string.Join("\\", SplittedSmdPath.Take(SplittedSmdPath.Length - 1)) + "\\" + sm.BonFile);
                }
                if (sm.SkiFile != null && sm.SkiFile != "")
                    ExtraModelFilesInGfx.Add(string.Join("\\", SplittedSmdPath.Take(SplittedSmdPath.Length - 1)) + "\\" + sm.SkiFile);
                if (sm.PhyFile != null && sm.PhyFile != "")
                {
                    var sp = sm.PhyFile.Split('.');
                    ExtraModelFilesInGfx.Add(string.Join("\\", SplittedSmdPath.Take(SplittedSmdPath.Length - 1)) + "\\" + sp[0] + ".m" + sp[1]);
                }
            }
        }
        public void ReadSmds()
        {
            SmdFiles.RemoveAll(z => z.EndsWith(".ecm") || z.EndsWith(".ECM") || (!z.EndsWith(".smd") && !z.EndsWith(".SMD")));
            SmdFiles = SmdFiles.GroupBy(s => s).Select(grp => grp.FirstOrDefault()).OrderBy(s => s).ToList();
            ChangeProgress(SmdFiles.Count, "Чтение файлов .smd из models.pck:");
            foreach (var item in SmdFiles)
            {
                IncrementValue(item);
                byte[] b = ReadFile("models", item);
                if (b.Length == 0)
                    continue;
                ModelImporter.SmdFile sm = new ModelImporter.SmdFile(b);
                string[] SplittedSmdPath = item.Split('\\');
                if (sm.StckDirectory != null)
                {
                    ExtraFilesInModels.Add(string.Join("\\", SplittedSmdPath.Take(SplittedSmdPath.Length - 1)) + "\\" + SplittedSmdPath.Last().Split('.')[0] + ".stck");
                    foreach (var it in sm.Actions)
                    {
                        ExtraFilesInModels.Add(string.Join("\\", SplittedSmdPath.Take(SplittedSmdPath.Length - 1)) + "\\" + sm.StckDirectory + "\\" + it.ActionFileName);
                    }
                }
                if (sm.BonFile != "")
                {
                    ExtraFilesInModels.Add(string.Join("\\", SplittedSmdPath.Take(SplittedSmdPath.Length - 1)) + "\\" + sm.BonFile);
                }
                if (sm.SkiFile != null && sm.SkiFile != "")
                    ExtraFilesInModels.Add(string.Join("\\", SplittedSmdPath.Take(SplittedSmdPath.Length - 1)) + "\\" + sm.SkiFile);
                if (sm.PhyFile != null && sm.PhyFile != "")
                {
                    var sp = sm.PhyFile.Split('.');
                    ExtraFilesInModels.Add(string.Join("\\", SplittedSmdPath.Take(SplittedSmdPath.Length - 1)) + "\\" + sp[0] + ".m" + sp[1]);
                }
            }
        }
        public void ReadSkies()
        {
            List<string> ModelTextures = new List<string>();
            var files = ExtraFilesInModels.Where(z => z.EndsWith(".ski") || z.EndsWith(".SKI"));
            ChangeProgress(files.Count(), "Чтение файлов .ski из models.pck:");
            foreach (var item in files)
            {
                IncrementValue(item);
                byte[] b = ReadFile("models", item);
                ModelImporter.SkiFile ski = new ModelImporter.SkiFile(b);
                string[] SplittedSmdPath = item.Split('\\');
                string folder = string.Join("\\", SplittedSmdPath.Take(SplittedSmdPath.Length - 1));
                string SkiTextDirPath = folder + "\\tex_" + SplittedSmdPath.Last().Replace(".ski", "") + "\\";
                if (ski.Textures.Count != 0)
                {
                    if (FileExists("models", SkiTextDirPath + ski.Textures[0]))
                    {
                        ModelTextures.AddRange(ski.Textures.Select(t => SkiTextDirPath + t));
                    }
                    else
                    {
                        ModelTextures.AddRange(ski.Textures.Select(t => folder + "\\textures\\" + t));
                    }
                }
            }
            ExtraFilesInModels.AddRange(ModelTextures);
        }
        public void ReadGfxSkies()
        {
            List<string> ModelTextures = new List<string>();
            var files = ExtraModelFilesInGfx.Where(z => z.EndsWith(".ski") || z.EndsWith(".SKI"));
            ChangeProgress(files.Count(), "Чтение файлов .ski из gfx.pck:");
            foreach (var item in files)
            {
                IncrementValue(item);
                byte[] b = ReadFile("gfx", item);
                if (b.Length == 0)
                    continue;
                ModelImporter.SkiFile ski = new ModelImporter.SkiFile(b);
                string[] SplittedSmdPath = item.Split('\\');
                string folder = string.Join("\\", SplittedSmdPath.Take(SplittedSmdPath.Length - 1));
                string SkiTextDirPath = folder + "\\tex_" + SplittedSmdPath.Last().Replace(".ski", "") + "\\";
                if (ski.Textures.Count != 0)
                {
                    if (FileExists("gfx", SkiTextDirPath + ski.Textures[0]))
                    {
                        ModelTextures.AddRange(ski.Textures.Select(t => SkiTextDirPath + t));
                    }
                    else
                    {
                        ModelTextures.AddRange(ski.Textures.Select(t => folder + "\\textures\\" + t));
                    }
                }
            }
            ExtraModelFilesInGfx.AddRange(ModelTextures);
        }
        public void ReadMoxFiles()
        {
            List<string> GrassTextures = new List<string>();
            ChangeProgress(GrassFiles.Count, "Чтение файлов .mox:");
            foreach (var item in GrassFiles)
            {
                IncrementValue(item);
                byte[] b = ReadFile("grasses", item);
                if (b.Length == 0)
                    continue;
                MoxFile sm = new MoxFile(b);
                string[] SplittedMoxPath = item.Split('\\');
                GrassTextures.Add(string.Join("\\", SplittedMoxPath.Take(SplittedMoxPath.Length - 1)) + "\\textures\\" + sm.Texture);
            }
            GrassFiles.AddRange(GrassTextures);
        }
        private IAsyncResult ChangeProgress(int Am, string tx, bool k = true)
        {
            return this.BeginInvoke(new MethodInvoker(delegate
            {
                if (k == true)
                {
                    ImportingProgress.Maximum = Am;
                    ImportingProgress.Value = 0;
                }
                ProgressText.Text = tx;
            }));
        }
        private IAsyncResult IncrementValue(string it)
        {
            return this.BeginInvoke(new MethodInvoker(delegate
            {
                ImportingProgress.Value++;
                CopyingFile.Text = it;
            }));
        }
        private void ProgressText_TextChanged(object sender, EventArgs e)
        {
            ProgressText.Location = new Point((this.Width / 2) - (TextRenderer.MeasureText(ProgressText.Text, ProgressText.Font).Width / 2), ProgressText.Location.Y);
        }
        public void LoadPcks(string path)
        {
            Pcks.Clear();
            string[] pcks = Directory.GetFiles(path, "*.pck");
            Task[] tasks = new Task[pcks.Length];
            int i = 0;
            foreach (var c in pcks)
            {
                if (!c.ToLower().EndsWith("elements.pck") && !c.ToLower().EndsWith("gshop.pck") && !c.ToLower().EndsWith("tasks.pck") && !c.ToLower().EndsWith("path.pck"))
                {
                    tasks[i] = new Task(new System.Action(() =>
                    {
                        ModelImporter.ArchiveEngine ae = new ModelImporter.ArchiveEngine();
                        ae.Load(c);
                        Pcks.Add(c.ToLower().Split('\\').Last().Split('.')[0], ae);
                    }));
                    i++;
                }

            }
            tasks = tasks.Where(e => e != null).ToArray();
            foreach (var item in tasks)
            {
                item.Start();
            }
            Task.WaitAll(tasks);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dg = MessageBox.Show("Вы уверены,что хотите закрыть программу?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Information_Click(object sender, EventArgs e)
        {
           MessageBox.Show("Maps Importer By Luka\rSkype:Luka007789\r17.05.2018 (c)Luka", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
    public enum Game
    {
        Pw = 0,
        Fw = 1,
        Jd = 2,
        Loma = 3,
        HoTK = 4,
        Eso = 5
    }
}