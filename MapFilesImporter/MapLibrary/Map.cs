using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MapFilesImporter.Struct;

namespace MapFilesImporter
{
    public class Map
    {
        public string EcwldPath;
        public string EcbsdPath;

        public Ecwld Ecwld;
        public Ecbsd Ecbsd;
        public List<string> BrokenLitmodels = new List<string>();

        public Map(string ecwldPath, string ecbsdPath)
        {
            EcwldPath = ecwldPath;
            EcbsdPath = ecbsdPath;
            try
            {
                BinaryReader brEcbsd = new BinaryReader(new StreamReader(ecbsdPath).BaseStream);
                BinaryReader brEcwld = new BinaryReader(new StreamReader(ecwldPath).BaseStream);
                Ecbsd = new Ecbsd(brEcbsd);
                Ecwld = new Ecwld(brEcwld, brEcbsd, Ecbsd, 0);
                brEcbsd.Close();
                brEcwld.Close();
            }
            catch
            {
                BinaryReader brEcbsd = new BinaryReader(new StreamReader(ecbsdPath).BaseStream);
                BinaryReader brEcwld = new BinaryReader(new StreamReader(ecwldPath).BaseStream);
                Ecbsd = new Ecbsd(brEcbsd);
                if (Ecbsd.Version == 17)
                {
                    Ecwld = new Ecwld(brEcwld, brEcbsd, Ecbsd, 1);
                }
                brEcbsd.Close();
                brEcwld.Close();
            }

        }

        public void Write(string wldpath, string bsdpath, int ecwldVersion, int ecbswVersion)
        {
            var sp = wldpath.Split('\\');
            if (!Directory.Exists(string.Join("\\", sp.Take(sp.Count() - 1))))
            {
                Directory.CreateDirectory(string.Join("\\", sp.Take(sp.Count() - 1)));
            }
            var bwEcwld = new BinaryWriter(File.Create(wldpath));
            var bwEcbsd = new BinaryWriter(File.Create(bsdpath));
            var all = Ecbsd.OrnamentData.Where(e => BrokenLitmodels.Contains(e.Value.ModelPath));
            Ecbsd.Write(bwEcbsd, ecbswVersion);
            Ecwld.Write(bwEcwld, bwEcbsd, ecwldVersion);
            bwEcbsd.Close();
            bwEcwld.Close();
        }
    }

    public class Ecwld
    {
        public int Header;
        public int Version;
        public float[] Size;//2
        public float BlockSize;
        public int Unk0;
        public int Count;
        public int Unk1;
        public int Unk2;
        public int[] Split;//16
        public Dictionary<int, Block> Blocks;//count
        public List<Tree> Trees;//Count_Trees
        public List<Grass> Grasses;//Count_Grasses

        public Ecwld(BinaryReader ecwld, BinaryReader ecbsd, Ecbsd ecbsdClass, int IsDifferent)
        {
            Header = ecwld.ReadInt32();
            Version = ecwld.ReadInt32();
            Size = new float[2];
            Size[0] = ecwld.ReadSingle();
            Size[1] = ecwld.ReadSingle();
            BlockSize = ecwld.ReadSingle();
            Unk0 = ecwld.ReadInt32();
            Count = ecwld.ReadInt32();
            Unk1 = ecwld.ReadInt32();
            Unk2 = ecwld.ReadInt32();
            var treesOffset = ecwld.ReadInt32();
            var grassesOffset = ecwld.ReadInt32();
            var countTrees = ecwld.ReadInt32();
            var countGrasses = ecwld.ReadInt32();
            Split = new int[16];
            for (var i = 0; i < 16; i++)
                Split[i] = ecwld.ReadInt32();
            Blocks = new Dictionary<int, Block>();
            var offsets = new int[Count];
            for (var i = 0; i < Count; i++)
                offsets[i] = ecwld.ReadInt32();

            Trees = new List<Tree>();
            ecwld.BaseStream.Position = treesOffset;
            for (var i = 0; i < countTrees; i++)
                Trees.Add(new Tree(ecwld, Version));
            Grasses = new List<Grass>();
            ecwld.BaseStream.Position = grassesOffset;
            for (var i = 0; i < countGrasses; i++)
                Grasses.Add(new Grass(ecwld, Version));
            for (var i = 0; i < Count; i++)
                Blocks.Add(offsets[i], new Block(offsets[i], ecwld, ecbsd, Version, ecbsdClass, this, IsDifferent));
        }

        public void Write(BinaryWriter ecwld, BinaryWriter ecbsd, int version)
        {
            ecwld.Write(Header);
            ecwld.Write(version);
            ecwld.Write(Size[0]);
            ecwld.Write(Size[1]);
            ecwld.Write(BlockSize);
            ecwld.Write(Unk0);
            ecwld.Write(Blocks.Count);
            ecwld.Write(Unk1);
            ecwld.Write(Unk2);
            var offsetTree = ecwld.BaseStream.Position;
            ecwld.Write(new byte[4]); // TreeOffset
            ecwld.Write(new byte[4]); // GrassOffset
            ecwld.Write(Trees.Count);
            ecwld.Write(Grasses.Count);
            for (var i = 0; i < 16; i++)
            {
                ecwld.Write(Split[i]);
            }
            var offsetPoint = ecwld.BaseStream.Position;
            var blockList = Blocks.Values.ToList();
            ecwld.Write(new byte[4 * blockList.Count]);
            var offsets = new int[blockList.Count];
            for (var i = 0; i < blockList.Count; i++)
            {
                offsets[i] = (int)ecwld.BaseStream.Position;
                blockList[i].Write(ecwld, version);
            }
            var newTreeOffset = (int)ecwld.BaseStream.Position;
            for (var i = 0; i < Trees.Count; i++)
            {
                Trees[i].Write(ecwld, version);
            }
            var newGrassOffset = (int)ecwld.BaseStream.Position;
            for (var i = 0; i < Grasses.Count; i++)
            {
                Grasses[i].Write(ecwld, version);
            }
            ecwld.BaseStream.Position = offsetPoint;
            for (var i = 0; i < offsets.Length; i++)
            {
                ecwld.Write(offsets[i]);
            }
            ecwld.BaseStream.Position = offsetTree;
            ecwld.Write(newTreeOffset);
            ecwld.Write(newGrassOffset);
        }
    }

    public class Block
    {
        public short X;
        public short Y;
        public List<TreeData> TreeData;
        public Dictionary<int, WaterData> WaterData;
        public Dictionary<int, GrassAreaData> GrassAreaData;
        public Dictionary<int, BmdModel> Litmodels;
        public Dictionary<int, TerrainInfo> Textures;
        public Dictionary<int, SmdModel> SmdModels;
        public Dictionary<int, Effect> EffectData;
        public Dictionary<int, Ecm> EcmModels;
        public Dictionary<int, BezierRoute> BezierRoutes;
        public Dictionary<int, SoundObject> SoundObjects;

        public Block(int offset, BinaryReader ecwld, BinaryReader ecbsd, int version, Ecbsd ecbsdClass, Ecwld ecwldClass, int IsDifferent)
        {
            ecwld.BaseStream.Position = offset;

            TreeData = new List<TreeData>();
            WaterData = new Dictionary<int, WaterData>();
            GrassAreaData = new Dictionary<int, GrassAreaData>();
            Litmodels = new Dictionary<int, BmdModel>();
            Textures = new Dictionary<int, TerrainInfo>();
            SmdModels = new Dictionary<int, SmdModel>();
            EffectData = new Dictionary<int, Effect>();
            EcmModels = new Dictionary<int, Ecm>();
            BezierRoutes = new Dictionary<int, BezierRoute>();
            SoundObjects = new Dictionary<int, SoundObject>();

            X = ecwld.ReadInt16();
            Y = ecwld.ReadInt16();
            var treeDataCount = ecwld.ReadInt32();
            var waterDataCount = ecwld.ReadInt32();
            var grassAreaDataCount = ecwld.ReadInt32();
            var ornamentDataCount = ecwld.ReadInt32();
            var boxAreaDataCount = ecwld.ReadInt32();
            var effectDataCount = ecwld.ReadInt32();
            var ecModelCount = ecwld.ReadInt32();
            var critterGroupDataCount = ecwld.ReadInt32();
            var bezierRoutesCount = ecwld.ReadInt32();
            var soundObjectsCount = ecwld.ReadInt32();
            int unknown;
            if (version == 16 && ecbsdClass.Version == 17 && IsDifferent == 1)
            {
                unknown = ecwld.ReadInt32();
            }
            for (var i = 0; i < treeDataCount; i++)
            {
                var temp = new TreeData(ecwld, version);
                TreeData.Add(temp);
                if (!ecwldClass.Trees[temp.TreeIndex].Usings.Contains(this))
                    ecwldClass.Trees[temp.TreeIndex].Usings.Add(this);
            }
            for (var i = 0; i < waterDataCount; i++)
            {
                var temp = new OffsetBlock(ecwld, version);
                if (!ecbsdClass.WaterData.ContainsKey(temp.Id))
                    ecbsdClass.WaterData.Add(temp.Id, new WaterData(ecbsd, temp, ecbsdClass.Version));
                else
                {
                    if (ecbsdClass.WaterData[temp.Id].OffsetBlock.Offset != temp.Offset)
                        throw new Exception();
                    if (!ecbsdClass.WaterData[temp.Id].Usings.Contains(this))
                        ecbsdClass.WaterData[temp.Id].Usings.Add(this);
                }

                WaterData.Add(temp.Id, ecbsdClass.WaterData[temp.Id]);
            }
            for (var i = 0; i < ornamentDataCount; i++)
            {
                var temp = new OffsetBlock(ecwld, version);
                if (!ecbsdClass.OrnamentData.ContainsKey(temp.Id))
                    ecbsdClass.OrnamentData.Add(temp.Id, new BmdModel(ecbsd, temp, ecbsdClass.Version));
                else
                {
                    if (ecbsdClass.OrnamentData[temp.Id].OffsetBlock.Offset != temp.Offset)
                        throw new Exception();
                    if (!ecbsdClass.OrnamentData[temp.Id].Usings.Contains(this))
                        ecbsdClass.OrnamentData[temp.Id].Usings.Add(this);
                }

                Litmodels.Add(temp.Id, ecbsdClass.OrnamentData[temp.Id]);
            }
            for (var i = 0; i < boxAreaDataCount; i++)
            {
                var temp = new OffsetBlock(ecwld, version);
                if (!ecbsdClass.BoxAreaData.ContainsKey(temp.Id))
                    ecbsdClass.BoxAreaData.Add(temp.Id, new TerrainInfo(ecbsd, temp, ecbsdClass.Version, version, IsDifferent));
                else
                {
                    if (ecbsdClass.BoxAreaData[temp.Id].OffsetBlock.Offset != temp.Offset)
                        throw new Exception();
                    if (!ecbsdClass.BoxAreaData[temp.Id].Usings.Contains(this))
                        ecbsdClass.BoxAreaData[temp.Id].Usings.Add(this);
                }

                Textures.Add(temp.Id, ecbsdClass.BoxAreaData[temp.Id]);
            }
            for (var i = 0; i < grassAreaDataCount; i++)
            {
                var temp = new OffsetBlock(ecwld, version);
                if (!ecbsdClass.GrassAreaData.ContainsKey(temp.Id))
                    ecbsdClass.GrassAreaData.Add(temp.Id, new GrassAreaData(ecbsd, temp, ecbsdClass.Version));
                else
                {
                    if (ecbsdClass.GrassAreaData[temp.Id].OffsetBlock.Offset != temp.Offset)
                        throw new Exception();
                    if (!ecbsdClass.GrassAreaData[temp.Id].Usings.Contains(this))
                        ecbsdClass.GrassAreaData[temp.Id].Usings.Add(this);
                }

                GrassAreaData.Add(temp.Id, ecbsdClass.GrassAreaData[temp.Id]);
            }
            for (var i = 0; i < effectDataCount; i++)
            {
                var temp = new Effect(ecwld, version);
                EffectData.Add(temp.Id, temp);
            }
            for (var i = 0; i < ecModelCount; i++)
            {
                var temp = new Ecm(ecwld, version);
                EcmModels.Add(temp.Id, temp);
            }
            for (var i = 0; i < critterGroupDataCount; i++)
            {
                var temp = new OffsetBlock(ecwld, version);
                if (!ecbsdClass.CritterGroupData.ContainsKey(temp.Id))
                    ecbsdClass.CritterGroupData.Add(temp.Id, new SmdModel(ecbsd, temp, ecbsdClass.Version));
                else
                {
                    if (ecbsdClass.CritterGroupData[temp.Id].OffsetBlock.Offset != temp.Offset)
                        throw new Exception();
                    if (!ecbsdClass.CritterGroupData[temp.Id].Usings.Contains(this))
                        ecbsdClass.CritterGroupData[temp.Id].Usings.Add(this);
                }

                SmdModels.Add(temp.Id, ecbsdClass.CritterGroupData[temp.Id]);
            }
            for (var i = 0; i < bezierRoutesCount; i++)
            {
                var temp = new OffsetBlock(ecwld, version);
                if (!ecbsdClass.BezierRoutes.ContainsKey(temp.Id))
                    ecbsdClass.BezierRoutes.Add(temp.Id, new BezierRoute(ecbsd, temp, ecbsdClass.Version, version));
                else
                {
                    if (ecbsdClass.BezierRoutes[temp.Id].OffsetBlock.Offset != temp.Offset)
                        throw new Exception();
                    if (!ecbsdClass.BezierRoutes[temp.Id].Usings.Contains(this))
                        ecbsdClass.BezierRoutes[temp.Id].Usings.Add(this);
                }

                BezierRoutes.Add(temp.Id, ecbsdClass.BezierRoutes[temp.Id]);
            }
            for (var i = 0; i < soundObjectsCount; i++)
            {
                var temp = new SoundObject(ecwld, version);
                SoundObjects.Add(temp.Id, temp);
            }
        }

        public void Write(BinaryWriter bw, int version)
        {
            BezierRoutes.Clear();
            bw.Write(X);
            bw.Write(Y);
            bw.Write(TreeData.Count);
            bw.Write(WaterData.Count);
            bw.Write(GrassAreaData.Count);
            bw.Write(Litmodels.Count);
            bw.Write(Textures.Count);
            bw.Write(EffectData.Count);
            bw.Write(EcmModels.Count);
            bw.Write(SmdModels.Count);
            bw.Write(BezierRoutes.Count);
            bw.Write(SoundObjects.Count);

            for (var i = 0; i < TreeData.Count; i++)
                TreeData[i].Save(bw, version);

            var waterData = WaterData.Values.ToList();
            for (var i = 0; i < waterData.Count; i++)
                waterData[i].OffsetBlock.Save(bw, version);

            var ornamentData = Litmodels.Values.ToList();
            for (var i = 0; i < ornamentData.Count; i++)
                ornamentData[i].OffsetBlock.Save(bw, version);

            var boxAreaData = Textures.Values.ToList();
            for (var i = 0; i < boxAreaData.Count; i++)
                boxAreaData[i].OffsetBlock.Save(bw, version);

            var grassAreaData = GrassAreaData.Values.ToList();
            for (var i = 0; i < grassAreaData.Count; i++)
                grassAreaData[i].OffsetBlock.Save(bw, version);

            var effectData = EffectData.Values.ToList();
            for (var i = 0; i < effectData.Count; i++)
                effectData[i].Save(bw, version);

            var ecModel = EcmModels.Values.ToList();
            for (var i = 0; i < ecModel.Count; i++)
                ecModel[i].Save(bw, version);

            var critterGroupData = SmdModels.Values.ToList();
            for (var i = 0; i < critterGroupData.Count; i++)
                critterGroupData[i].OffsetBlock.Save(bw, version);

            var bezierRoutes = BezierRoutes.Values.ToList();
            for (var i = 0; i < bezierRoutes.Count; i++)
                bezierRoutes[i].OffsetBlock.Save(bw, version);

            var soundObjects = SoundObjects.Values.ToList();
            for (var i = 0; i < soundObjects.Count; i++)
                soundObjects[i].Save(bw, version);
        }
    }

    public class TreeData
    {
        public int TreeIndex;
        public float X;
        public float Y;
        public float Z;

        public TreeData(int treeIndex, float x, float y, float z)
        {
            TreeIndex = treeIndex;
            X = x;
            Y = y;
            Z = z;
        }

        public TreeData(BinaryReader br, int version)
        {
            TreeIndex = br.ReadInt32();
            X = br.ReadSingle();
            Y = br.ReadSingle();
            Z = br.ReadSingle();
        }

        public void Save(BinaryWriter bw, int version)
        {
            bw.Write(TreeIndex);
            bw.Write(X);
            bw.Write(Y);
            bw.Write(Z);
        }
    }

    public class Tree
    {
        public List<Block> Usings = new List<Block>();

        public string Spt;
        public string Dds;

        public Tree(string spt, string dds)
        {
            Spt = spt;
            Dds = dds;
        }

        public Tree(BinaryReader br, int version)
        {
            Spt = Encoding.GetEncoding("GBK").GetString(br.ReadBytes(br.ReadInt32()));
            Dds = Encoding.GetEncoding("GBK").GetString(br.ReadBytes(br.ReadInt32()));
        }

        public void Write(BinaryWriter bw, int version)
        {
            var sptBytes = Encoding.GetEncoding("GBK").GetBytes(Spt);
            var ddsBytes = Encoding.GetEncoding("GBK").GetBytes(Dds);
            bw.Write(sptBytes.Length);
            bw.Write(sptBytes);
            bw.Write(ddsBytes.Length);
            bw.Write(ddsBytes);
        }
    }

    public class Grass
    {
        public string Mox;
        public int Unk0;
        public int Unk1;
        public float Unk2;
        public float Unk3;
        public float Unk4;
        public float Unk5;

        public Grass(int unk0, int unk1, float unk2, float unk3, float unk4, float unk5, string mox)
        {
            Unk0 = unk0;
            Unk1 = unk1;
            Unk2 = unk2;
            Unk3 = unk3;
            Unk4 = unk4;
            Unk5 = unk5;
            Mox = mox;
        }

        public Grass(BinaryReader br, int version)
        {
            Unk0 = br.ReadInt32();
            Unk1 = br.ReadInt32();
            Unk2 = br.ReadSingle();
            Unk3 = br.ReadSingle();
            Unk4 = br.ReadSingle();
            Unk5 = br.ReadSingle();
            Mox = Encoding.GetEncoding("GBK").GetString(br.ReadBytes(br.ReadInt32()));
        }

        public void Write(BinaryWriter bw, int version)
        {
            var moxBytes = Encoding.GetEncoding("GBK").GetBytes(Mox);

            bw.Write(Unk0);
            bw.Write(Unk1);
            bw.Write(Unk2);
            bw.Write(Unk3);
            bw.Write(Unk4);
            bw.Write(Unk5);
            bw.Write(moxBytes.Length);
            bw.Write(moxBytes);
        }
    }

    public class Ecbsd
    {
        private long Header;
        public int Version;
        public byte[] Splits; // 60
        public List<string> BsData;

        public Dictionary<int, WaterData> WaterData;
        public Dictionary<int, GrassAreaData> GrassAreaData;
        public Dictionary<int, BmdModel> OrnamentData;
        public Dictionary<int, TerrainInfo> BoxAreaData;
        public Dictionary<int, SmdModel> CritterGroupData;
        public Dictionary<int, BezierRoute> BezierRoutes;

        public Ecbsd(BinaryReader br)
        {
            WaterData = new Dictionary<int, WaterData>();
            GrassAreaData = new Dictionary<int, GrassAreaData>();
            OrnamentData = new Dictionary<int, BmdModel>();
            BoxAreaData = new Dictionary<int, TerrainInfo>();
            CritterGroupData = new Dictionary<int, SmdModel>();
            BezierRoutes = new Dictionary<int, BezierRoute>();

            Header = br.ReadInt64();
            if (Header != 4779238698121711437)
                return;
            Version = br.ReadInt32();
            var count = br.ReadInt32();
            Splits = br.ReadBytes(60);
            BsData = new List<string>();
            for (var i = 0; i < count; i++)
                BsData.Add(Encoding.ASCII.GetString(br.ReadBytes(br.ReadInt32())));
        }

        public void Write(BinaryWriter bw, int version)
        {
            bw.Write(Header);
            bw.Write(version);
            bw.Write(BsData.Count);
            bw.Write(Splits);
            for (var i = 0; i < BsData.Count; i++)
            {
                var bsData = Encoding.ASCII.GetBytes(BsData[i]);
                bw.Write(bsData.Length);
                bw.Write(bsData);
            }
            foreach (var temp in WaterData.Values)
                temp.Write(bw, version);
            foreach (var temp in GrassAreaData.Values)
                temp.Write(bw, version);
            foreach (var temp in OrnamentData.Values)
                temp.Write(bw, version);
            foreach (var temp in BoxAreaData.Values)
                temp.Write(bw, version);
            foreach (var temp in CritterGroupData.Values)
                temp.Write(bw, version);
            foreach (var temp in BezierRoutes.Values)
                temp.Write(bw, version);
        }
    }
}