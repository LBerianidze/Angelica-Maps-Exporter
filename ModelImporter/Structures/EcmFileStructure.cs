using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelImporter
{
    public class EcmFileStructure
    {
        public string[] sp = { ": " };
        public int Version;                                      //All
        public string Smd;                                   //All
        public string OrgColor = "ffffffff";                               //18
        public int SrcBlend = 5;                                   //21
        public int DestBlend = 6;                                    //21
        public int OuterNum;                                     //18
        public float[] Outers;                                   //18
        public int NewScale;                                     //28
        public List<Bone> Bones = new List<Bone>();              //18
        public float DefSpeed = 1;                                   //27
        public int ChannelCount;                                 //32
        public List<int> ChannelMask = new List<int>();          //32
        public int CoGfxNum;                                     //14
        public int ComActCount;                                  //All
        public List<Gfx> Effects = new List<Gfx>();              //14
        public List<Action> Actions = new List<Action>();        //All
        public List<EndScript> Scripts = new List<EndScript>();  //25
        public List<string> AddiSkins = new List<string>();      //All
        public List<Child> Childs = new List<Child>();           //All
        public int AutoUpdata = 1;                            //33
        public string physfilename;                              //Under Question
        public int CanCastShadow = 1;                                //43
        public int RenderModel = 1;                                  //45
        public int RenderEdge = 1;                              //48
        public string EmissiveCol = "";                          //52
        public string ShaderFile = "";                           //57
        public string ShaderTex = "";                            //57
        public int PSConstCount = 1;                                 //57
        public int PSFileVersion = 1;                               //60
        public int[] AudioEventGroupEnable = new int[11];        //71
        public int ParticleBonesCount;                           //71
        public List<EcmHook> Hooks = new List<EcmHook>();
        public List<string> GfxFiles = new List<string>();
        public List<string> AttFiles = new List<string>();
        public List<string> WavFiles = new List<string>();
        public EcmFileStructure(byte[] file)
        {
            StreamReaderL sr = new StreamReaderL(file, Encoding.GetEncoding(936));
            Version = sr.ReadLine().GetEcmLineValue().ToInt32();
            Smd = sr.ReadLine().GetEcmLineValue().ToLower();
            if (Version >= 33) AutoUpdata = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 18) OrgColor = sr.ReadLine().GetEcmLineValue();
            if (Version >= 52) EmissiveCol = sr.ReadLine().GetEcmLineValue();
            if (Version >= 21)
            {
                SrcBlend = sr.ReadLine().GetEcmLineValue().ToInt32();
                DestBlend = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 18)
            {
                OuterNum = sr.ReadLine().GetEcmLineValue().ToInt32();
                Outers = new float[OuterNum];
                for (int i = 0; i < OuterNum; i++)
                    Outers[i] = sr.ReadLine().GetEcmLineValue().ToSingle();
            }
            if (Version >= 28) NewScale = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 18) Bones = new List<Bone>(sr.ReadLine().GetEcmLineValue().ToInt32());
            for (int i = 0; i < Bones.Capacity; i++)
                Bones.Add(new Bone(sr, Version, NewScale));
            if (Version >= 27) DefSpeed = sr.ReadNonEmptyLine().GetEcmLineValue().ToSingle();
            if (Version >= 43) CanCastShadow = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 45) RenderModel = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 48) RenderEdge = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 60) PSFileVersion = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 57)
            {
                ShaderFile = sr.ReadLine().GetEcmLineValue();
                ShaderTex = sr.ReadLine().GetEcmLineValue();
                PSConstCount = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 32)
            {
                ChannelCount = sr.ReadLine().GetEcmLineValue().ToInt32();
                ChannelMask = new List<int>(sr.ReadWhile("ChannelCount", null).GetEcmLineValue().ToInt32());
                for (int i = 0; i < ChannelMask.Capacity; i++)
                {
                    ChannelMask.Add(sr.ReadLine().GetEcmLineValue().ToInt32());
                }
            }
            if (Version >= 14)
            {
                CoGfxNum = sr.ReadNonEmptyLine().GetEcmLineValue().ToInt32();
            }
            ComActCount = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 71)
            {
                for (int i = 0; i < 11; i++)
                {
                    AudioEventGroupEnable[i] = sr.ReadLine().GetEcmLineValue().ToInt32();
                }
                ParticleBonesCount = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            for (int i = 0; i < CoGfxNum; i++)
            {
                Effects.Add(new Gfx(sr, Version, true));
            }
            if (Version == 24 && ComActCount == 4)
            {
                ComActCount /= 2;
            }
            for (int i = 0; i < ComActCount; i++)
            {
                Actions.Add(new Action(sr, Version));
            }
            if (Version >= 25)
            {
                Scripts = new List<EndScript>(sr.ReadLine().GetEcmLineValue().ToInt32());
                for (int i = 0; i < Scripts.Capacity; i++)
                {
                    Scripts.Add(new EndScript(sr, Version));
                }
            }
            List<string> l = new List<string>();
            AddiSkins = new List<string>(sr.ReadWhile("AddiSkinCount", l).GetEcmLineValue().ToInt32());
            for (int i = 0; i < AddiSkins.Capacity; i++)
            {
                AddiSkins.Add("models\\" + sr.ReadLine().GetEcmLineValue().ToLower());
            }
            Childs = new List<Child>(sr.ReadLine().GetEcmLineValue().ToInt32());
            for (int i = 0; i < Childs.Capacity; i++)
            {
                Childs.Add(new Child(sr, Version));
            }
            string t = sr.TryReadLine();
            if (t != null)
            {
                string[] f = t.Replace(" ", "").Split(':');
                if (f[0].ToLower() == "physfilename")
                {
                    physfilename = f[1];
                    string lt = sr.TryReadLine();
                    if (lt != null)
                    {
                        string[] e = lt.Replace(" ", "").Split(':');
                        if (e[0].ToLower() == "ecmhookcount")
                        {
                            Hooks = new List<EcmHook>(e[1].ToInt32());
                        }
                    }

                }
                else if (f[0].ToLower() == "ecmhookcount")
                {
                    Hooks = new List<EcmHook>(f[1].ToInt32());
                }
                for (int i = 0; i < Hooks.Capacity; i++)
                {
                    Hooks.Add(new EcmHook(sr, Version));
                }
            }
            sr.Close();

            GfxFiles = Actions.SelectMany(e => e.Gfxs.Select(n => n.FxFilePath)).Select(r => r?.ToLower()).ToList();
            GfxFiles.AddRange(Effects.Select(e => e.FxFilePath).ToList());
            GfxFiles.AddRange(Effects.SelectMany(e => e.FxFiles.Select(n => n)).Select(r => r?.ToLower()).ToList());
            GfxFiles.AddRange(Actions.SelectMany(e => e.Gfxs.SelectMany(n => n.FxFiles)).Select(r => r?.ToLower()).ToList());
            GfxFiles.RemoveAll(e => e == null);
            GfxFiles = GfxFiles.GroupBy(j => j).Select(i => "gfx\\" + i.FirstOrDefault()).ToList();

            AttFiles = Actions.SelectMany(e => e.Attacks.Select(n => n.AtkPath)).Select(r => r?.ToLower()).ToList();
            AttFiles.RemoveAll(e => e == null);
            AttFiles = WavFiles.GroupBy(j => j).Select(i => "gfx\\skillattack\\" + i.FirstOrDefault()).ToList();

            WavFiles = Actions.SelectMany(e => e.Sfx.Select(n => n.FxFilePath)).Select(r => r?.ToLower()).ToList();
            WavFiles.AddRange(Actions.SelectMany(e => e.Sfx.SelectMany(n => n.FxFiles)).Select(r => r?.ToLower()).ToList());
            WavFiles.RemoveAll(e => e == null);
            WavFiles = WavFiles.GroupBy(j => j).Select(i => "sfx\\" + i.FirstOrDefault()).ToList();

        }
        public void Save(StreamWriter sw, int Version)
        {
            sw.WriteParameter("MOXTVersion", Version);
            sw.WriteParameter("SkinModelPath", Smd);
            if (Version >= 33) sw.WriteParameter("AutoUpdata", AutoUpdata);
            if (Version >= 18) sw.WriteParameter("OrgColor", OrgColor);
            if (Version >= 52) sw.WriteParameter("EmissiveCol", EmissiveCol);
            if (Version >= 21)
            {
                sw.WriteParameter("SrcBlend", SrcBlend);
                sw.WriteParameter("DestBlend", DestBlend);
            }
            if (Version >= 18)
            {
                sw.WriteParameter("OuterNum", OuterNum);
                for (int i = 0; i < Outers.Length; i++)
                {
                    sw.WriteParameter("Float", Outers[i]);
                }
            }
            if (Version >= 28)
            {
                sw.WriteParameter("NewScale", NewScale);
            }
            if (Version >= 18)
            {
                sw.WriteParameter("BoneNum", Bones.Count);
                for (int i = 0; i < Bones.Count; i++)
                {
                    Bones[i].Save(sw, Version, NewScale);
                }
            }
            if (Version > 28)
            {
                sw.WriteLine();
            }
            if (Version >= 27)
            {
                sw.WriteParameter("DefSpeed", DefSpeed);
            }
            if (Version >= 43)
            {
                sw.WriteParameter("CanCastShadow", CanCastShadow);
            }
            if (Version >= 45)
            {
                sw.WriteParameter("RenderModel", RenderModel);
            }
            if (Version >= 48)
            {
                sw.WriteParameter("RenderEdge", RenderEdge);
            }
            if (Version >= 60)
            {
                sw.WriteParameter("PSFileVersion", PSFileVersion);
            }
            if (Version >= 57)
            {
                sw.WriteParameter("ShaderFile", ShaderFile);
                sw.WriteParameter("ShaderTex", ShaderTex);
                sw.WriteParameter("PSConstCount", PSConstCount);
            }
            if (Version >= 32)
            {
                sw.WriteParameter("ChannelCount", ChannelCount);
                sw.WriteParameter("ChannelCount", ChannelMask.Count);
                for (int i = 0; i < ChannelMask.Count; i++)
                {
                    sw.WriteParameter("ChannelMask", ChannelMask[i]);
                }
            }
            if (Version >= 14)
            {
                sw.WriteParameter("CoGfxNum", CoGfxNum);
            }
            sw.WriteParameter("ComActCount", ComActCount);
            if (Version >= 71)
            {
                for (int i = 0; i < 11; i++)
                {
                    sw.WriteParameter("AudioEventGroupEnable", AudioEventGroupEnable[i]);
                }
                sw.WriteParameter("ParticleBonesCount", ParticleBonesCount);
            }
            if (Version >= 14)
            {
                for (int i = 0; i < Effects.Count; i++)
                {
                    Effects[i].Save(sw, Version);
                }
            }
            for (int i = 0; i < ComActCount; i++)
            {
                Actions[i].Save(sw, Version);
            }
            if (Version >= 25)
            {
                sw.WriteParameter("ScriptCount", Scripts.Count);
                for (int i = 0; i < Scripts.Count; i++)
                {
                    Scripts[i].Write(sw, Version);
                }
            }
            sw.WriteParameter("AddiSkinCount", AddiSkins.Count);
            for (int i = 0; i < AddiSkins.Count; i++)
            {
                sw.WriteParameter("AddiSkinPath", AddiSkins[i]);
            }
            sw.WriteParameter("ChildCount", Childs.Count);
            for (int i = 0; i < Childs.Count; i++)
            {
                Childs[i].Save(sw, Version);
            }
            if (Version >= 33)
            {
                sw.WriteParameter("PhysFileName", physfilename);
            }
            if (Version >= 41)
            {
                sw.WriteParameter("ECMHookCount", Hooks.Count);
                for (int i = 0; i < Hooks.Count; i++)
                {
                    Hooks[i].Save(sw, Version);
                }

            }
        }
        public void FixToFW()
        {
            for (int i = 0; i < Actions.Count; i++)
            {
                if (Actions[i].CombineActName.ToLower() == "快速移动")
                {
                    Actions[i].CombineActName = "奔跑";
                }
                else if (Actions[i].CombineActName.ToLower() == "技能施放起")
                {
                    Actions[i].CombineActName = "技能攻击1";
                }
                else if (Actions[i].CombineActName.ToLower() == "普攻1起")//攻击
                {
                    Actions[i].CombineActName = "普通攻击1";
                }
                else if (Actions[i].CombineActName.ToLower() == "普攻2起")
                {
                    Actions[i].CombineActName = "普通攻击2";
                }
                else if (Actions[i].CombineActName.ToLower() == "慢速移动")
                {
                    Actions[i].CombineActName = "行走";
                }
                else if (Actions[i].CombineActName.ToLower() == "休闲")
                {
                    Actions[i].CombineActName = "休闲";
                }
                else if (Actions[i].CombineActName.ToLower() == "戒备")
                {
                    Actions[i].CombineActName = "战斗站立";
                }
                else if (Actions[i].CombineActName.ToLower() == "站立")
                {
                    Actions[i].CombineActName = "站立";
                }
            }
        }


        public void FixToPW()
        {
            for (int i = 0; i < Actions.Count; i++)
            {
                if (Actions[i].CombineActName.ToLower() == "奔跑")
                {
                    Actions[i].CombineActName = "快速移动";
                }
                else if (Actions[i].CombineActName.ToLower() == "技能攻击1")
                {
                    Actions[i].CombineActName = "技能施放起";
                }
                else if (Actions[i].CombineActName.ToLower() == "普通攻击1" || Actions[i].CombineActName.ToLower() == "攻击")
                {
                    Actions[i].CombineActName = "普攻1起";
                }
                else if (Actions[i].CombineActName.ToLower() == "普通攻击2")
                {
                    Actions[i].CombineActName = "普攻2起";
                }
                else if (Actions[i].CombineActName.ToLower() == "行走")
                {
                    Actions[i].CombineActName = "慢速移动";
                }
                else if (Actions[i].CombineActName.ToLower() == "休闲")
                {
                    Actions[i].CombineActName = "休闲";
                }
                else if (Actions[i].CombineActName.ToLower() == "战斗站立")
                {
                    Actions[i].CombineActName = "戒备";
                }
                else if (Actions[i].CombineActName.ToLower() == "站立")
                {
                    Actions[i].CombineActName = "站立";
                }
            }
        }
    }
    public class EcmHook
    {
        public string Name;
        public int Id;
        public decimal Scale;
        public decimal[] Hook1 = new decimal[3];
        public decimal[] Hook2 = new decimal[3];
        public decimal[] Hook3 = new decimal[3];
        public decimal[] Hook4 = new decimal[3];
        public EcmHook(StreamReaderL sr, int Version)
        {
            Name = sr.ReadLine().GetEcmLineValue();
            Id = sr.ReadLine().GetEcmLineValue().ToInt32();
            Scale = sr.ReadLine().GetEcmLineValue().ToDecimal();
            string[] Vals1 = sr.ReadLine().Split(new string[] { " " }, StringSplitOptions.None);
            for (int i = 0; i < 3; i++)
            {
                Hook1[i] = Vals1[i].ToDecimal();
            }
            string[] Vals2 = sr.ReadLine().Split(new string[] { " " }, StringSplitOptions.None);
            for (int i = 0; i < 3; i++)
            {
                Hook2[i] = Vals2[i].ToDecimal();
            }
            string[] Vals3 = sr.ReadLine().Split(new string[] { " " }, StringSplitOptions.None);
            for (int i = 0; i < 3; i++)
            {
                Hook3[i] = Vals3[i].ToDecimal();
            }
            string[] Vals4 = sr.ReadLine().Split(new string[] { " " }, StringSplitOptions.None);
            for (int i = 0; i < 3; i++)
            {
                Hook4[i] = Vals4[i].ToDecimal();
            }

        }
        public void Save(StreamWriter sw, int Version)
        {
            sw.WriteParameter("ECMHook", Name);
            sw.WriteParameter("ID", Id);
            sw.WriteParameter("Scale", Scale);
            sw.WriteParameter("", Hook1, true);
            sw.WriteParameter("", Hook2, true);
            sw.WriteParameter("", Hook3, true);
            sw.WriteParameter("", Hook4, true);
        }
    }
    public class Action
    {
        public string CombineActName;
        public int LoopCount;
        public List<Rank> Ranks = new List<Rank>();
        public int EventChannel;
        public int EventCount;
        public List<BaseAct> BaseActions = new List<BaseAct>();
        public List<Gfx> Gfxs = new List<Gfx>();
        public List<Sfx> Sfx = new List<Sfx>();
        public List<Color> Colors = new List<Color>();
        public List<ChildAct> ChildActs = new List<ChildAct>();
        public List<Attack> Attacks = new List<Attack>();
        //>=40
        public decimal PlaySpeed = 1;
        //>=49
        public int StopChildAct;
        public int ResetMtl = 1;
        //>=54
        public List<Type107> Type107s = new List<Type107>();
        public List<Type108> Type108s = new List<Type108>();
        //>=56
        public List<Script> Scripts = new List<Script>();
        public Action(StreamReader sr, int Version)
        {
            CombineActName = sr.ReadLine().GetEcmLineValue();
            LoopCount = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 32)
            {
                Ranks = new List<Rank>(sr.ReadLine().GetEcmLineValue().ToInt32());
                for (int i = 0; i < Ranks.Capacity; i++)
                {
                    Ranks.Add(new Rank(sr));
                }
                EventChannel = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 40)
            {
                PlaySpeed = sr.ReadLine().GetEcmLineValue().ToDecimal();
            }
            if (Version >= 49)
            {
                StopChildAct = sr.ReadLine().GetEcmLineValue().ToInt32();
                ResetMtl = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            BaseActions = new List<BaseAct>(sr.ReadLine().GetEcmLineValue().ToInt32());
            for (int i = 0; i < BaseActions.Capacity; i++)
            {
                BaseActions.Add(new BaseAct(sr, Version));
            }
            EventCount = sr.ReadLine().GetEcmLineValue().ToInt32();
            for (int i = 0; i < EventCount; i++)
            {
                int t = sr.ReadLine().GetEcmLineValue().ToInt32();
                if (t == 100)
                {
                    Gfxs.Add(new Gfx(sr, Version, false));
                }
                else if (t == 101)
                {
                    Sfx.Add(new Sfx(sr, Version, false));
                }
                else if (t == 102)
                {
                    ChildActs.Add(new ChildAct(sr, Version, false));
                }
                else if (t == 103)
                {
                    Colors.Add(new Color(sr, Version, false));
                }
                else if (t == 104)
                {
                    Attacks.Add(new Attack(sr, Version, false));
                }
                else if (t == 105)
                {
                    Scripts.Add(new Script(sr, Version, false));
                }
                else if (t == 107)
                {
                    Type107s.Add(new Type107(sr, Version, false));
                }
                else if (t == 108)
                {
                    Type108s.Add(new Type108(sr, Version, false));
                }
            }
        }
        public void Save(StreamWriter sw, int Version)
        {
            sw.WriteParameter("CombineActName", CombineActName);
            sw.WriteParameter("LoopCount", LoopCount);
            if (Version >= 32)
            {
                sw.WriteParameter("RankCount", Ranks.Count);
                for (int i = 0; i < Ranks.Count; i++)
                {
                    Ranks[i].Write(sw);
                }
                sw.WriteParameter("EventChannel", EventChannel);
            }
            if (Version >= 42)
            {
                sw.WriteParameter("PlaySpeed", PlaySpeed);
            }
            if (Version >= 49)
            {
                sw.WriteParameter("StopChildAct", StopChildAct);
                sw.WriteParameter("ResetMtl", ResetMtl);
            }
            sw.WriteParameter("BaseActCount", BaseActions.Count);
            for (int i = 0; i < BaseActions.Count; i++)
            {
                BaseActions[i].Write(sw, Version);
            }
            sw.WriteParameter("EventCount", EventCount);
            for (int i = 0; i < Gfxs.Count; i++)
            {
                Gfxs[i].Save(sw, Version);
            }
            for (int i = 0; i < Sfx.Count; i++)
            {
                Sfx[i].Save(sw, Version);
            }
            for (int i = 0; i < ChildActs.Count; i++)
            {
                ChildActs[i].Save(sw, Version);
            }
            for (int i = 0; i < Colors.Count; i++)
            {
                Colors[i].Save(sw, Version);
            }
            for (int i = 0; i < Attacks.Count; i++)
            {
                Attacks[i].Save(sw, Version);
            }
            for (int i = 0; i < Scripts.Count; i++)
            {
                Scripts[i].Save(sw, Version);
            }
            for (int i = 0; i < Type107s.Count; i++)
            {
                Type107s[i].Save(sw, Version);
            }
            for (int i = 0; i < Type108s.Count; i++)
            {
                Type108s[i].Save(sw, Version);
            }
        }
    }
    public class Gfx
    {
        public int EventType = 100;
        public int StartTime;
        public int TimeSpan = -1;
        public int Once;
        public string FxFilePath;
        public string HookName;
        public decimal[] HookOffset = new decimal[3];
        public decimal HookYaw;
        public decimal HookPitch;
        public decimal HookRot;
        public int BindParent = 1;
        public int FadeOut = 1000;
        public int UseModelAlpha = 1;
        public decimal GfxScale = 1;
        public decimal GfxAlpha = 1;
        public decimal GfxSpeed = 1;
        public int GfxOuterPath;
        public List<GfxParam> GfxParams = new List<GfxParam>();
        //>=35
        public int GfxRelToECM;
        //>=54
        public List<string> FxFiles = new List<string>();
        public int GfxDelayTime;
        //>=59
        public int CustomPath;
        //>=62
        public int CustomData;
        //>=65
        public int GfxRotWithModel = 1;
        //>=71
        public int GfxUseFixedPoint;
        public int FxStartTime;
        public Gfx(StreamReader sr, int Version, bool ReadType)
        {
            if (ReadType)
            {
                EventType = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 18)
            {
                StartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
                if (Version >= 20)
                {
                    TimeSpan = sr.ReadLine().GetEcmLineValue().ToInt32();
                }
                Once = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            else
            {
                FxStartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 54)
            {
                FxFiles = new List<string>(sr.ReadLine().GetEcmLineValue().ToInt32());
                for (int i = 0; i < FxFiles.Capacity; i++)
                {
                    FxFiles.Add(sr.ReadLine().GetEcmLineValue());
                }
                FxFilePath = FxFiles.FirstOrDefault();
            }
            else
            {
                FxFilePath = sr.ReadLine().GetEcmLineValue();
            }
            HookName = sr.ReadLine().GetEcmLineValue();
            string[] Vals = sr.ReadLine().GetEcmLineValue().Split(new string[] { "," }, StringSplitOptions.None);
            for (int i = 0; i < 3; i++)
            {
                HookOffset[i] = Vals[i].ToDecimal();
            }
            HookYaw = sr.ReadLine().GetEcmLineValue().ToDecimal();
            if (HookYaw != 0)
            {

            }
            HookPitch = sr.ReadLine().GetEcmLineValue().ToDecimal();
            if (Version >= 19)
            {
                HookRot = sr.ReadLine().GetEcmLineValue().ToDecimal();
            }
            BindParent = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 15)
            {
                FadeOut = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 18)
            {
                UseModelAlpha = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 59)
            {
                CustomPath = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 62)
            {
                CustomData = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            GfxScale = sr.ReadLine().GetEcmLineValue().ToDecimal();
            if (Version >= 22)
            {
                GfxAlpha = sr.ReadLine().GetEcmLineValue().ToDecimal();
            }
            GfxSpeed = sr.ReadLine().GetEcmLineValue().ToDecimal();
            if (Version <= 14)
            {
                FadeOut = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 23)
            {
                GfxOuterPath = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 35)
            {
                GfxRelToECM = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 54)
            {
                GfxDelayTime = sr.ReadLine().GetEcmLineValue().ToInt32();

            }
            if (Version >= 66)
            {
                GfxRotWithModel = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 71)
            {
                GfxUseFixedPoint = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            GfxParams = new List<GfxParam>(sr.ReadLine().GetEcmLineValue().ToInt32());
            for (int i = 0; i < GfxParams.Capacity; i++)
            {
                GfxParams.Add(new GfxParam(sr, Version));
            }
        }
        public void Save(StreamWriter sw, int Version)
        {
            sw.WriteParameter("EventType", EventType);
            if (Version >= 18)
            {
                sw.WriteParameter("StartTime", StartTime);
                if (Version >= 20)
                {
                    sw.WriteParameter("TimeSpan", TimeSpan);
                }
                sw.WriteParameter("Once", Once);
            }
            else
            {
                sw.WriteParameter("FxStartTime", FxStartTime);
            }
            if (Version >= 54)
            {

                sw.WriteParameter("FxFileNum", FxFiles.Count);
                for (int i = 0; i < FxFiles.Count; i++)
                {
                    sw.WriteParameter("FxFilePath", FxFiles[i]);
                }
            }
            else
            {
                sw.WriteParameter("FxFilePath", FxFilePath);
            }
            sw.WriteParameter("HookName", HookName);
            sw.WriteParameter("HookOffset", HookOffset, true);
            sw.WriteParameter("HookYaw", HookYaw);
            sw.WriteParameter("HookPitch", HookPitch);
            if (Version >= 19)
            {
                sw.WriteParameter("HookRot", HookRot);
            }
            sw.WriteParameter("BindParent", BindParent);
            if (Version >= 15)
            {
                sw.WriteParameter("FadeOut", FadeOut);
            }
            if (Version >= 18)
            {
                sw.WriteParameter("UseModelAlpha", UseModelAlpha);
            }
            if (Version >= 59)
            {
                sw.WriteParameter("CustomPath", CustomPath);
            }

            if (Version >= 62)
            {
                sw.WriteParameter("CustomData", CustomData);
            }
            sw.WriteParameter("GfxScale", GfxScale);
            if (Version >= 22)
            {
                sw.WriteParameter("GfxAlpha", GfxAlpha);
            }
            sw.WriteParameter("GfxSpeed", GfxSpeed);
            if (Version <= 14)
            {
                sw.WriteParameter("FadeOut", FadeOut);
            }
            if (Version >= 23)
            {
                sw.WriteParameter("GfxOuterPath", GfxOuterPath);
            }
            if (Version >= 35)
            {
                sw.WriteParameter("GfxRelToECM", GfxRelToECM);
            }
            if (Version >= 54)
            {
                sw.WriteParameter("GfxDelayTime", GfxDelayTime);
            }
            if (Version >= 66)
            {
                sw.WriteParameter("GfxRotWithModel", GfxRotWithModel);
            }
            if (Version >= 71)
            {
                sw.WriteParameter("GfxUseFixedPoint", GfxUseFixedPoint);
            }
            sw.WriteParameter("GfxParamCount", GfxParams.Count);
            for (int i = 0; i < GfxParams.Count; i++)
            {
                GfxParams[i].Write(sw, Version);
            }

        }
    }
    public class Sfx
    {
        public int EventType = 101;
        public int StartTime;
        public int TimeSpan;
        public int Once;
        public string FxFilePath;
        public string HookName;
        public decimal[] HookOffset = new decimal[3];
        public decimal HookYaw;
        public decimal HookPitch;
        public decimal HookRot;
        public int BindParent;
        public int FadeOut;
        public int UseModelAlpha;
        public int SoundVer;
        public int Force2D;
        public int IsLoop;
        public int Volume;
        public decimal MinDist;
        public decimal MaxDist;
        //>=54
        public List<string> FxFiles = new List<string>();
        public int VolMin;
        public int VolMax;
        public decimal PitchMin;
        public decimal PitchMax;
        //>=59
        public int CustomPath;
        //>=62
        public int CustomData;
        //>=65
        public int AbsoluteVolume;
        public int FixSpeed;
        public int SilentHeader;
        //>=71
        public decimal PercentStart;
        public int Group;
        public int FxStartTime;
        public Sfx(StreamReader sr, int Version, bool ReadType)
        {
            if (ReadType)
            {
                EventType = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 18)
            {
                StartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
                if (Version >= 20)
                {
                    TimeSpan = sr.ReadLine().GetEcmLineValue().ToInt32();
                }
                Once = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            else
            {
                FxStartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 54)
            {
                FxFiles = new List<string>(sr.ReadLine().GetEcmLineValue().ToInt32());
                for (int i = 0; i < FxFiles.Capacity; i++)
                {
                    FxFiles.Add(sr.ReadLine().GetEcmLineValue());
                }
                FxFilePath = FxFiles.FirstOrDefault();
            }
            else
            {
                FxFilePath = sr.ReadLine().GetEcmLineValue();
            }
            HookName = sr.ReadLine().GetEcmLineValue();
            string[] Vals = sr.ReadLine().GetEcmLineValue().Split(new string[] { "," }, StringSplitOptions.None);
            for (int i = 0; i < 3; i++)
            {
                HookOffset[i] = Vals[i].ToDecimal();
            }
            HookYaw = sr.ReadLine().GetEcmLineValue().ToDecimal();
            HookPitch = sr.ReadLine().GetEcmLineValue().ToDecimal();
            if (Version >= 19)
            {
                HookRot = sr.ReadLine().GetEcmLineValue().ToDecimal();
            }
            if (Version >= 15)
            {
                BindParent = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            FadeOut = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 18)
            {
                UseModelAlpha = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 59)
            {
                CustomPath = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 62)
            {
                CustomData = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            SoundVer = sr.ReadLine().GetEcmLineValue().ToInt32();
            Force2D = sr.ReadLine().GetEcmLineValue().ToInt32();
            IsLoop = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version < 54)
            {
                Volume = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            else
            {
                VolMin = sr.ReadLine().GetEcmLineValue().ToInt32();
                VolMax = sr.ReadLine().GetEcmLineValue().ToInt32();
                if (Version >= 65)
                {
                    AbsoluteVolume = sr.ReadLine().GetEcmLineValue().ToInt32();
                }
                PitchMin = sr.ReadLine().GetEcmLineValue().ToDecimal();
                PitchMax = sr.ReadLine().GetEcmLineValue().ToDecimal();
                Volume = VolMax;
            }
            MinDist = sr.ReadLine().GetEcmLineValue().ToDecimal();
            MaxDist = sr.ReadLine().GetEcmLineValue().ToDecimal();
            if (Version >= 65)
            {
                FixSpeed = sr.ReadLine().GetEcmLineValue().ToInt32();
                SilentHeader = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 71)
            {
                PercentStart = sr.ReadLine().GetEcmLineValue().ToDecimal();
                Group = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
        }
        public void Save(StreamWriter sw, int Version)
        {
            sw.WriteParameter("EventType", EventType);
            if (Version >= 18)
            {
                sw.WriteParameter("StartTime", StartTime);
                if (Version >= 20)
                {
                    sw.WriteParameter("TimeSpan", TimeSpan);
                }
                sw.WriteParameter("Once", Once);
            }
            else
            {
                sw.WriteParameter("FxStartTime", FxStartTime);
            }
            if (Version >= 54)
            {
                sw.WriteParameter("FxFileNum", FxFiles.Count);
                for (int i = 0; i < FxFiles.Count; i++)
                {
                    sw.WriteParameter("FxFilePath", FxFiles[i]);
                }
            }
            else
            {
                sw.WriteParameter("FxFilePath", FxFilePath);
            }
            sw.WriteParameter("HookName", HookName);
            sw.WriteParameter("HookOffset", HookOffset, true);
            sw.WriteParameter("HookYaw", HookYaw);
            sw.WriteParameter("HookPitch", HookPitch);
            if (Version >= 19)
            {
                sw.WriteParameter("HookRot", HookRot);
            }
            if (Version >= 15)
            {
                sw.WriteParameter("BindParent", BindParent);
            }
            sw.WriteParameter("FadeOut", FadeOut);
            if (Version >= 18)
            {
                sw.WriteParameter("UseModelAlpha", UseModelAlpha);
            }
            if (Version >= 59)
            {
                sw.WriteParameter("CustomPath", CustomPath);
            }
            if (Version >= 62)
            {
                sw.WriteParameter("CustomData", CustomData);
            }
            sw.WriteParameter("SoundVer", SoundVer);
            sw.WriteParameter("Force2D", Force2D);
            sw.WriteParameter("IsLoop", IsLoop);
            if (Version < 54)
            {
                sw.WriteParameter("Volume", Volume);
            }
            else
            {
                sw.WriteParameter("VolMin", VolMin);
                sw.WriteParameter("VolMax", VolMax);
                if (Version >= 65)
                {
                    sw.WriteParameter("AbsoluteVolume", AbsoluteVolume);
                }
                sw.WriteParameter("PitchMin", PitchMin);
                sw.WriteParameter("PitchMax", PitchMax);
            }
            sw.WriteParameter("MinDist", MinDist);
            sw.WriteParameter("MaxDist", MaxDist);
            if (Version >= 65)
            {
                sw.WriteParameter("FixSpeed", FixSpeed);
                sw.WriteParameter("SilentHeader", SilentHeader);
            }
            if (Version >= 71)
            {
                sw.WriteParameter("PercentStart", PercentStart);
                sw.WriteParameter("Group", Group);
            }
        }
    }
    public class BaseAct
    {
        public string BaseActName;
        public int ActStartTime;
        public int LoopCount = -1;
        //>=36
        public int LoopMinNum = -1;
        public int LoopMaxNum = -1;
        public BaseAct(StreamReader sr, int Version)
        {
            BaseActName = sr.ReadLine().GetEcmLineValue();
            ActStartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version < 36)
            {
                LoopCount = sr.ReadLine().GetEcmLineValue().ToInt32();
                LoopMinNum = LoopCount;
                LoopMaxNum = LoopCount;
            }
            else
            {
                LoopMinNum = sr.ReadLine().GetEcmLineValue().ToInt32();
                LoopMaxNum = sr.ReadLine().GetEcmLineValue().ToInt32();
                LoopCount = LoopMinNum;
            }
        }
        public void Write(StreamWriter sw, int Version)
        {
            sw.WriteParameter("BaseActName", BaseActName);
            sw.WriteParameter("ActStartTime", ActStartTime);
            if (Version < 36)
            {
                sw.WriteParameter("LoopCount", LoopCount);
            }
            else
            {
                sw.WriteParameter("LoopMinNum", LoopMinNum);
                sw.WriteParameter("LoopMaxNum", LoopMaxNum);
            }

        }
    }
    public class Bone
    {
        public int BoneIndex;
        public int BoneSclType;
        public decimal[] BoneScale = new decimal[3];
        public Bone(StreamReader sr, int Version, int sc)
        {
            BoneIndex = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (sc == 0)
            {
                BoneSclType = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            string[] Vals = sr.ReadLine().GetEcmLineValue().Split(new string[] { "," }, StringSplitOptions.None);
            for (int i = 0; i < 3; i++)
            {
                BoneScale[i] = Vals[i].ToDecimal();
            }

        }
        public void Save(StreamWriter sw, int Version, int sc)
        {
            sw.WriteParameter("BoneIndex", BoneIndex);
            if (sc == 0)
            {
                sw.WriteParameter("BoneSclType", BoneSclType);
            }
            sw.WriteParameter("BoneScale", BoneScale, true);
        }
    }
    public class GfxParam
    {
        public string ParamEleName;
        public int ParamId;
        public int ParamDataType;
        public int ParamDataIsCmd;
        public string ParamDataHook;
        public GfxParam(StreamReader sr, int Version)
        {
            ParamEleName = sr.ReadLine().GetEcmLineValue();
            ParamId = sr.ReadLine().GetEcmLineValue().ToInt32();
            ParamDataType = sr.ReadLine().GetEcmLineValue().ToInt32();
            ParamDataIsCmd = sr.ReadLine().GetEcmLineValue().ToInt32();
            ParamDataHook = sr.ReadLine().GetEcmLineValue();
        }
        public void Write(StreamWriter sw, int Version)
        {
            sw.WriteParameter("ParamEleName", ParamEleName);
            sw.WriteParameter("ParamId", ParamId);
            sw.WriteParameter("ParamDataType", ParamDataType);
            sw.WriteParameter("ParamDataIsCmd", ParamDataIsCmd);
            sw.WriteParameter("ParamDataHook", ParamDataHook);
        }
    }
    public class EndScript
    {
        public int Id;
        public int LinesCount;
        public List<string> ScriptLines = new List<string>();
        public EndScript(StreamReader sr, int Version)
        {
            Id = sr.ReadLine().GetEcmLineValue().ToInt32();
            LinesCount = sr.ReadLine().GetEcmLineValue().ToInt32();
            for (int i = 0; i < LinesCount; i++)
            {
                ScriptLines.Add(sr.ReadLine());
            }
        }
        public void Write(StreamWriter sw, int Version)
        {
            sw.WriteParameter("id", Id);
            sw.WriteParameter("ScriptLines", LinesCount);
            for (int i = 0; i < LinesCount; i++)
            {
                sw.WriteLine(ScriptLines[i]);
            }

        }
    }
    public class Child
    {
        public string ChildName;
        public string ChildPath;
        public string HHName;
        public string CCName;
        public Child(StreamReader sr, int Version)
        {
            ChildName = sr.ReadLine().GetEcmLineValue();
            ChildPath = sr.ReadLine().GetEcmLineValue();
            HHName = sr.ReadLine().GetEcmLineValue();
            CCName = sr.ReadLine().GetEcmLineValue();
        }
        public void Save(StreamWriter sw, int Version)
        {
            sw.WriteParameter("ChildName", ChildName);
            sw.WriteParameter("ChildPath", ChildPath);
            sw.WriteParameter("HHName", HHName);
            sw.WriteParameter("CCName", CCName);
        }
    }
    public class EcmPoint3D
    {
        public decimal X;
        public decimal Y;
        public decimal Z;
        public decimal R;
        public EcmPoint3D(StreamReader sr, bool Direction)
        {
            string[] Vals = sr.ReadLine().GetEcmLineValue().Split(new string[] { "," }, StringSplitOptions.None);
            X = Vals[0].ToDecimal();
            Y = Vals[1].ToDecimal();
            Z = Vals[2].ToDecimal();
            if (Direction)
            {
                R = Vals[3].ToDecimal();
            }
        }
    }
    public class Rank
    {
        public int Channel;
        public int RankT;
        public Rank(StreamReader sr)
        {
            string[] st = sr.ReadLine().Replace(" ", "").Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            string[] ft = st[1].Split(',');
            Channel = Convert.ToInt32(ft[0]);
            RankT = Convert.ToInt32(st[2]);
        }
        public void Write(StreamWriter sw)
        {
            sw.WriteLine(string.Format("Channel: {0}, Rank: {1}", Channel, RankT));
        }
    }
    public class Dest
    {
        public int Col;
        public float Time;
        public Dest(StreamReader sr)
        {
            Col = sr.ReadLine().GetEcmLineValue().ToInt32();
            Time = sr.ReadLine().GetEcmLineValue().ToSingle();
        }
        public void Write(StreamWriter sw, int Version)
        {
            sw.WriteParameter("Col", Col);
            sw.WriteParameter("Time", Time);
        }
    }
    public class Scale
    {
        public string Name;
        public decimal[] SclParam = new decimal[3];
        public List<decimal[]> SclDest = new List<decimal[]>();
        public Scale(StreamReader sr)
        {
            Name = sr.ReadLine().GetEcmLineValue();
            string[] Vals = sr.ReadLine().Split(':')[1].Split(new string[] { " " }, StringSplitOptions.None);
            for (int i = 0; i < 3; i++)
            {
                SclParam[i] = Vals[i + 1].Replace(" ", "").ToDecimal();
            }
            for (int z = 0; z < SclParam[2]; z++)
            {
                SclDest.Add(new decimal[3]);
                string[] Vals1 = sr.ReadLine().Split(':')[1].Split(new string[] { " " }, StringSplitOptions.None);
                for (int i = 0; i < 3; i++)
                {
                    SclDest.Last()[i] = Vals1[i + 1].ToDecimal();
                }
            }
        }
        public void Write(StreamWriter sw, int Version)
        {
            sw.WriteParameter("Name", Name);
            sw.WriteParameter("SclParam", string.Join(" ", SclParam));
            for (int i = 0; i < SclParam[2]; i++)
            {
                sw.WriteParameter("SclDest", SclDest[i], true);
            }
        }
    }
    public class ChildAct
    {
        public int EventType = 102;
        public int StartTime;
        public int TimeSpan;
        public int Once;
        public string ChildActName;
        public string HHName;
        public int IsTrail;
        public int TrailSpan;
        public int Segs;
        public List<EcmPoint3D> Positions = new List<EcmPoint3D>();
        public List<EcmPoint3D> Directions = new List<EcmPoint3D>();
        //>=42
        public int TransTime;
        public ChildAct(StreamReader sr, int Version, bool ReadType)
        {
            if (ReadType)
            {
                EventType = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            StartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 20)
            {
                TimeSpan = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            Once = sr.ReadLine().GetEcmLineValue().ToInt32();
            ChildActName = sr.ReadLine().GetEcmLineValue();
            HHName = sr.ReadLine().GetEcmLineValue();
            if (Version >= 42)
            {
                TransTime = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            IsTrail = sr.ReadLine().GetEcmLineValue().ToInt32();
            TrailSpan = sr.ReadLine().GetEcmLineValue().ToInt32();
            Segs = sr.ReadLine().GetEcmLineValue().ToInt32();
            for (int i = 0; i < Segs; i++)
            {
                Positions.Add(new EcmPoint3D(sr, false));
                Directions.Add(new EcmPoint3D(sr, true));
            }
        }
        public void Save(StreamWriter sw, int Version)
        {
            sw.WriteParameter("EventType", EventType);
            sw.WriteParameter("StartTime", StartTime);
            if (Version >= 20)
            {
                sw.WriteParameter("TimeSpan", TimeSpan);
            }
            sw.WriteParameter("Once", Once);
            sw.WriteParameter("ChildActName", ChildActName);
            sw.WriteParameter("HHName", HHName);
            if (Version >= 42)
            {
                sw.WriteParameter("TransTime", TransTime);
            }
            sw.WriteParameter("IsTrail", IsTrail);
            sw.WriteParameter("TrailSpan", TrailSpan);
            sw.WriteParameter("Segs", Segs);
            for (int i = 0; i < Segs; i++)
            {
                sw.WriteParameter("Pos", string.Format("{0}, {1}, {2}", Positions[i].X.RoundToSix(6), Positions[i].Y.RoundToSix(6), Positions[i].Z.RoundToSix(6)));
                sw.WriteParameter("Dir", string.Format("{0}, {1}, {2}, {3}", Directions[i].X.RoundToSix(6), Directions[i].Y.RoundToSix(6), Directions[i].Z.RoundToSix(6), Directions[i].R.RoundToSix(6)));
            }

        }
    }
    public class Color
    {
        public int EventType = 103;
        public int StartTime;
        public int TimeSpan;
        public int Once;
        public decimal[] ColorValue1 = new decimal[4];
        public decimal[] ColorValue2 = new decimal[4];
        public decimal[] ColorValue3 = new decimal[4];
        public decimal[] ColorValue4 = new decimal[4];
        public int ApplyChild;
        public Color(StreamReader sr, int Version, bool ReadType)
        {
            if (ReadType)
            {
                EventType = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            StartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 20)
            {
                TimeSpan = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            Once = sr.ReadLine().GetEcmLineValue().ToInt32();
            string[] Vals1 = sr.ReadLine().GetEcmLineValue().Split(new string[] { "," }, StringSplitOptions.None);
            for (int i = 0; i < 4; i++)
            {
                ColorValue1[i] = Vals1[i].ToDecimal();
            }
            string[] Vals2 = sr.ReadLine().GetEcmLineValue().Split(new string[] { "," }, StringSplitOptions.None);
            for (int i = 0; i < 4; i++)
            {
                ColorValue2[i] = Vals2[i].ToDecimal();
            }
            string[] Vals3 = sr.ReadLine().GetEcmLineValue().Split(new string[] { "," }, StringSplitOptions.None);
            for (int i = 0; i < 4; i++)
            {
                ColorValue3[i] = Vals3[i].ToDecimal();
            }
            string[] Vals4 = sr.ReadLine().GetEcmLineValue().Split(new string[] { "," }, StringSplitOptions.None);
            for (int i = 0; i < 4; i++)
            {
                ColorValue4[i] = Vals4[i].ToDecimal();
            }
            ApplyChild = sr.ReadLine().GetEcmLineValue().ToInt32();
        }
        public void Save(StreamWriter sw, int Version)
        {
            sw.WriteParameter("EventType", EventType);
            sw.WriteParameter("StartTime", StartTime);
            if (Version >= 20)
            {
                sw.WriteParameter("TimeSpan", TimeSpan);
            }
            sw.WriteParameter("Once", Once);
            sw.WriteParameter("ColorValue", ColorValue1, true);
            sw.WriteParameter("ColorValue", ColorValue2, true);
            sw.WriteParameter("ColorValue", ColorValue3, true);
            sw.WriteParameter("ColorValue", ColorValue4, true);
            sw.WriteParameter("ApplyChild", ApplyChild);
        }
    }
    public class Attack
    {
        public int EventType = 104;
        public int StartTime;
        public int TimeSpan;
        public int Once;
        public string AtkPath;
        public int Divisions;
        //>=39
        public int AtkUseDelay;
        public int AtkDelayNum;
        public int AtkDelayTime1;
        public int AtkDelayTime2;
        //>=65
        public int AtkOrient;
        public Attack(StreamReader sr, int Version, bool ReadType)
        {
            if (ReadType)
            {
                EventType = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            StartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
            TimeSpan = sr.ReadLine().GetEcmLineValue().ToInt32();
            Once = sr.ReadLine().GetEcmLineValue().ToInt32();
            AtkPath = sr.ReadLine().GetEcmLineValue();
            Divisions = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 39)
            {
                AtkUseDelay = sr.ReadLine().GetEcmLineValue().ToInt32();
                AtkDelayNum = sr.ReadLine().GetEcmLineValue().ToInt32();
                AtkDelayTime1 = sr.ReadLine().GetEcmLineValue().ToInt32();
                AtkDelayTime2 = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 65)
            {
                AtkOrient = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
        }
        public void Save(StreamWriter sw, int Version)
        {
            sw.WriteParameter("EventType", EventType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("TimeSpan", TimeSpan);
            sw.WriteParameter("Once", Once);
            sw.WriteParameter("AtkPath", AtkPath);
            sw.WriteParameter("Divisions", Divisions);
            if (Version >= 39)
            {
                sw.WriteParameter("AtkUseDelay", AtkUseDelay);
                sw.WriteParameter("AtkDelayNum", AtkDelayNum);
                sw.WriteParameter("AtkDelayTime", AtkDelayTime1);
                sw.WriteParameter("AtkDelayTime", AtkDelayTime2);
            }
            if (Version >= 65)
            {
                sw.WriteParameter("AtkOrient", AtkOrient);
            }
        }
    }
    public class Type107
    {
        public int EventType = 107;
        public int StartTime;
        public int TimeSpan;
        public int Once;
        public List<Scale> Scales = new List<Scale>();
        //>=67
        public int Use_File_Scale;
        public Type107(StreamReader sr, int Version, bool ReadType)
        {
            if (ReadType)
            {
                EventType = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            StartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
            TimeSpan = sr.ReadLine().GetEcmLineValue().ToInt32();
            Once = sr.ReadLine().GetEcmLineValue().ToInt32();
            Scales = new List<Scale>(sr.ReadLine().GetEcmLineValue().ToInt32());
            for (int i = 0; i < Scales.Capacity; i++)
            {
                Scales.Add(new Scale(sr));
            }
            if (Version >= 67)
            {
                Use_File_Scale = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
        }
        public void Save(StreamWriter sw, int Version)
        {
            sw.WriteParameter("EventType", EventType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("TimeSpan", TimeSpan);
            sw.WriteParameter("Once", Once);
            sw.WriteParameter("Num", Scales.Count);
            for (int i = 0; i < Scales.Count; i++)
            {
                Scales[i].Write(sw, Version);
            }
            if (Version >= 67)
            {
                sw.WriteParameter("Use File Scale", Use_File_Scale);
            }
        }
    }
    public class Type108
    {
        public int EventType = 108;
        public int StartTime;
        public int TimeSpan;
        public int Once;
        public int Col;
        public int ApplyChild;
        public List<Dest> Dests = new List<Dest>();
        //>=56
        public Type108(StreamReader sr, int Version, bool ReadType)
        {
            if (ReadType)
            {
                EventType = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            StartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
            TimeSpan = sr.ReadLine().GetEcmLineValue().ToInt32();
            Once = sr.ReadLine().GetEcmLineValue().ToInt32();
            Col = sr.ReadLine().GetEcmLineValue().ToInt32();
            Dests = new List<Dest>(sr.ReadLine().GetEcmLineValue().ToInt32());
            if (Version >= 56)
            {
                ApplyChild = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            for (int i = 0; i < Dests.Capacity; i++)
            {
                Dests.Add(new Dest(sr));
            }
        }
        public void Save(StreamWriter sw, int Version)
        {
            sw.WriteParameter("EventType", EventType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("TimeSpan", TimeSpan);
            sw.WriteParameter("Once", Once);
            sw.WriteParameter("Col", Col);
            sw.WriteParameter("DestNum", Dests.Count);
            if (Version >= 56)
            {
                sw.WriteParameter("ApplyChild", ApplyChild);
            }
            for (int i = 0; i < Dests.Count; i++)
            {
                Dests[i].Write(sw, Version);

            }
        }
    }
    public class Script
    {
        public int EventType = 105;
        public int StartTime;
        public int TimeSpan;
        public int Once;
        public int ScriptCfgState;
        public int ScriptLinesCount;
        public List<string> ScriptLines = new List<string>();
        //>=59
        public int ScriptUsage;
        public Script(StreamReader sr, int Version, bool ReadType)
        {
            if (ReadType)
            {
                EventType = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            StartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
            TimeSpan = sr.ReadLine().GetEcmLineValue().ToInt32();
            Once = sr.ReadLine().GetEcmLineValue().ToInt32();
            ScriptCfgState = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 59)
            {
                string[] st = sr.ReadLine().SplitEcmLine();
                if (st[0] == "ScriptLines")
                {
                    ScriptLinesCount = st[1].ToInt32();
                }
                else
                {
                    ScriptUsage = st[1].ToInt32();
                    ScriptLinesCount = sr.ReadLine().GetEcmLineValue().ToInt32();
                }
            }
            else
            {
                ScriptLinesCount = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            for (int i = 0; i < ScriptLinesCount; i++)
            {
                ScriptLines.Add(sr.ReadLine());
            }
        }
        public void Save(StreamWriter sw, int Version)
        {
            sw.WriteParameter("EventType", EventType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("TimeSpan", TimeSpan);
            sw.WriteParameter("Once", Once);
            sw.WriteParameter("ScriptCfgState", ScriptCfgState);
            if (Version >= 59 && ScriptUsage == 1)
            {
                sw.WriteParameter("ScriptUsage", ScriptUsage);
            }
            sw.WriteParameter("ScriptLinesCount", ScriptLinesCount);
            for (int i = 0; i < ScriptLinesCount; i++)
            {
                sw.WriteParameter("", ScriptLines[i]);
            }
        }
    }
}
