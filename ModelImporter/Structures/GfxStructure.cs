using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxStructure
    {
        public static string ID;
        public string[] sp = { ": " };
        public int Version;
        public int IsAngelica3;//80
        public float DedaultScale;
        public float PlaySpeed;
        public float DefaultAlpha;
        public int RayTrace;
        public int FaceToViewer;
        public int FaceByDist;
        public float FadeStart;
        public float FadeEnd;
        public Vector3D Vec1;
        public Vector3D Vec2;
        public int UseAABB;
        public int AccurateAABB;
        public int ShakeCam;
        public CtrlType112 _112Header;
        public int ShakePeriod;
        public int ShakeByDistance;
        public float[] ShakeCamBlur = new float[2];
        public float ShakeCamPixOff;
        public int ShakeDamp;
        public int NoChangeDir;
        public int _2DRender;
        public int _2DBackLayer;
        public int PhysExist;
        public int GFXELEMENTCOUNT;
        public List<GfxElement> GfxElements = new List<GfxElement>();
        public bool isanjelica;
        public GfxStructure(StreamReaderL sr)
        {
            ID = null;
            Version = sr.ReadLine().GetEcmLineValue().ToInt32();
            if(Version<34)
            {
                return;
            }
            string l = sr.ReadLine();
            if (l.ToLower().Contains("3:"))
            {
                isanjelica = true;
                IsAngelica3 = l.GetEcmLineValue().ToInt32();
                DedaultScale = sr.ReadLine().GetEcmLineValue().ToSingle();
            }
            else
            {
                DedaultScale = l.GetEcmLineValue().ToSingle();
            }
            PlaySpeed = sr.ReadLine().GetEcmLineValue().ToSingle();
            DefaultAlpha = sr.ReadLine().GetEcmLineValue().ToSingle();
            RayTrace = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (Version >= 38)
            {
                FaceToViewer = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            FaceByDist = sr.ReadLine().GetEcmLineValue().ToInt32();
            FadeStart = sr.ReadLine().GetEcmLineValue().ToSingle();
            FadeEnd = sr.ReadLine().GetEcmLineValue().ToSingle();
            Vec1 = new Vector3D(sr, false);
            Vec2 = new Vector3D(sr, false);
            if (Version >= 53)
            {
                UseAABB = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 84)
            {
                AccurateAABB = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            ShakeCam = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (ShakeCam >= 1)
            {
                _112Header = new CtrlType112(sr, Version, true);
                if (Version >= 98)
                {
                    ShakePeriod = sr.ReadLine().GetEcmLineValue().ToInt32();
                    ShakeByDistance = sr.ReadLine().GetEcmLineValue().ToInt32();
                    string[] et = sr.ReadLine().GetEcmLineValue().Split(',');
                    ShakeCamBlur[0] = et[0].ToSingle();
                    ShakeCamBlur[1] = et[1].ToInt32();
                    ShakeCamPixOff = sr.ReadLine().GetEcmLineValue().ToSingle();
                }
            }
            if (Version >= 84)
            {
                ShakeDamp = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 46)
            {
                NoChangeDir = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 49)
            {
                _2DRender = sr.ReadLine().GetEcmLineValue().ToInt32();
                _2DBackLayer = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (Version >= 63)
            {
                PhysExist = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            GFXELEMENTCOUNT = sr.ReadLine().GetEcmLineValue().ToInt32();
            for (int i = 0; i < GFXELEMENTCOUNT; i++)
            {
                GfxElements.Add(new GfxElement(sr, Version));
            }
            sr.Close();
        }
        public void Save(StreamWriter sw, int vers,bool closewritter)
        {
            sw.WriteParameter("MOXTVersion", vers);
            if (isanjelica && vers > 70)
            {
                sw.WriteParameter("IsAngelica3", IsAngelica3);
            }
            sw.WriteParameter("DedaultScale", DedaultScale);
            sw.WriteParameter("PlaySpeed", PlaySpeed);
            sw.WriteParameter("DefaultAlpha", DefaultAlpha);
            sw.WriteParameter("Raytrace", RayTrace);
            if (vers >= 38)
            {
                sw.WriteParameter("FaceToViewer", FaceToViewer);
            }
            sw.WriteParameter("FadeByDist", FaceByDist);
            sw.WriteParameter("FadeStart", FadeStart);
            sw.WriteParameter("FadeEnd", FadeEnd);
            Vec1.Save(sw, "Vec");
            Vec2.Save(sw, "Vec");
            if (vers >= 53)
            {
                sw.WriteParameter("UseAABB", UseAABB);
            }
            if (vers >= 84)
            {
                sw.WriteParameter("AccurateAABB", AccurateAABB);
            }
            sw.WriteParameter("ShakeCam", ShakeCam);
            if (ShakeCam >= 1)
            {
                _112Header.Save(sw, vers);
                if (vers >= 98)
                {
                    sw.WriteParameter("ShakePeriod", ShakePeriod);
                    sw.WriteParameter("ShakeByDistance", ShakeByDistance);
                    sw.WriteParameter("ShakeCamBlur", string.Join(", ", ShakeCamBlur[0].RoundToSix(6) + ", " + ShakeCamBlur[1]));
                    sw.WriteParameter("ShakeCamPixOff", ShakeCamPixOff);
                }
            }
            if (vers >= 84)
            {
                sw.WriteParameter("ShakeDamp", ShakeDamp);
            }
            if (vers >= 46)
            {
                sw.WriteParameter("NoChangeDir", NoChangeDir);
            }
            if (vers >= 49)
            {
                sw.WriteParameter("2DRender", _2DRender);
                sw.WriteParameter("2DBackLayer", _2DBackLayer);
            }
            if (vers >= 63)
            {
                sw.WriteParameter("PhysExist", PhysExist);
            }
         //   GfxElements.RemoveAll(e => e.ID == 240 || e.ID==210 || e.ID==200||e.ID==101);
            //  GfxElements.RemoveAll(e => e.ID != 100 && e.ID != 101 && e.ID != 110 && e.ID != 120 && e.ID != 121 && e.ID != 123 && e.ID!= 124&&e.ID!=140&&e.ID!=150&&e.ID!=160&&e.ID!=170);
            sw.WriteParameter("GFXELEMENTCOUNT", GfxElements.Count);
            for (int i = 0; i < GfxElements.Count; i++)
            {
                GfxElements[i].Save(sw, vers);
            }
            if(closewritter)
            sw.Close();
        }
    }
    public class GfxElement
    {
        public int ID;
        public string Name;
        public int SrcBlend;
        public int DestBlend;
        public int RepeatCount;
        public int RepeatDelay;
        public string TexFile;
        public string BindEle;
        public int ZEnable;
        public int MatchGrnd;
        public int GroundHeight;
        public int TexRow;
        public int TexCol;
        public int TexInterval;
        public int Priority;
        public int IsDummy;
        public string DummyEle;
        public int Warp;
        public int TileMode;
        public float TexSpeed1;
        public float TexSpeed2;
        public int UReverse;
        public int VReverse;
        public int UVExchg;
        public int RenderLayer;
        public int NoDownSample;
        public int ResetLoopEnd;
        public int TexAnimMaxTime;
        public int PSFileVersion;
        public string ShaderFile;
        public string ShaderTex;
        public int PSConstCount;
        public List<PSConst> PSConsts = new List<PSConst>();
        public int CanDoFadeOut;
        public GfxElementType100 GfxElementType100E;
        public GfxElementType101 GfxElementType101E;
        public GfxElementType102 GfxElementType102E;
        public GfxElementType110 GfxElementType110E;
        public GfxElementType120 GfxElementType120E;
        public GfxElementType121 GfxElementType121E;
        public GfxElementType122 GfxElementType122E;
        public GfxElementType123 GfxElementType123E;
        public GfxElementType124 GfxElementType124E;
        public GfxElementType125 GfxElementType125E;
        public GfxElementType130 GfxElementType130E;
        public GfxElementType140 GfxElementType140E;
        public GfxElementType150 GfxElementType150E;
        public GfxElementType151 GfxElementType151E;
        public GfxElementType152 GfxElementType152E;
        public GfxElementType160 GfxElementType160E;
        public GfxElementType170 GfxElementType170E;
        public GfxElementType180 GfxElementType180E;
        public GfxElementType190 GfxElementType190E;
        public GfxElementType200 GfxElementType200E;
        public GfxElementType210 GfxElementType210E;
        public GfxElementType211 GfxElementType211E;
        public GfxElementType220 GfxElementType220E;
        public GfxElementType230 GfxElementType230E;
        public GfxElementType240 GfxElementType240E;
        public int StartTime;
        public int KEYPOINTCOUNT;
        public List<KeyPoint> KeyPoints = new List<KeyPoint>();
        public string GfxPath;
        public GfxElement(StreamReaderL sr, int vers)
        {
            ID = sr.ReadLine().GetEcmLineValue().ToInt32();
            Name = sr.ReadLine().GetEcmLineValue();
            SrcBlend = sr.ReadLine().GetEcmLineValue().ToInt32();
            DestBlend = sr.ReadLine().GetEcmLineValue().ToInt32();
            RepeatCount = sr.ReadLine().GetEcmLineValue().ToInt32();
            RepeatDelay = sr.ReadLine().GetEcmLineValue().ToInt32();
            TexFile = sr.ReadLine().GetEcmLineValue();
            BindEle = sr.ReadLine().GetEcmLineValue();
            ZEnable = sr.ReadLine().GetEcmLineValue().ToInt32();
            MatchGrnd = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (vers >= 57)
            {
                GroundHeight = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            TexRow = sr.ReadLine().GetEcmLineValue().ToInt32();
            TexCol = sr.ReadLine().GetEcmLineValue().ToInt32();
            TexInterval = sr.ReadLine().GetEcmLineValue().ToInt32();
            Priority = sr.ReadLine().GetEcmLineValue().ToInt32();
            IsDummy = sr.ReadLine().GetEcmLineValue().ToInt32();
            DummyEle = sr.ReadLine().GetEcmLineValue();
            Warp = sr.ReadLine().GetEcmLineValue().ToInt32();
            TileMode = sr.ReadLine().GetEcmLineValue().ToInt32();
            TexSpeed1 = sr.ReadLine().GetEcmLineValue().ToSingle();
            TexSpeed2 = sr.ReadLine().GetEcmLineValue().ToSingle();
            if (vers >= 41)
            {
                UReverse = sr.ReadLine().GetEcmLineValue().ToInt32();
                VReverse = sr.ReadLine().GetEcmLineValue().ToInt32();
                UVExchg = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (vers >= 45)
            {
                RenderLayer = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (vers >= 58)
            {
                NoDownSample = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (vers >= 74)
            {
                ResetLoopEnd = sr.ReadLine().GetEcmLineValue().ToInt32();
                if (vers >= 75)
                {
                    TexAnimMaxTime = sr.ReadLine().GetEcmLineValue().ToInt32();
                    if (vers >= 92)
                    {
                        if (vers >= 95)
                        {
                            PSFileVersion = sr.ReadLine().GetEcmLineValue().ToInt32();
                        }
                        ShaderFile = sr.ReadLine().GetEcmLineValue();
                        ShaderTex = sr.ReadLine().GetEcmLineValue();
                        PSConstCount = sr.ReadLine().GetEcmLineValue().ToInt32();
                        for (int i = 0; i < PSConstCount; i++)
                        {
                            PSConsts.Add(new PSConst(sr, vers));
                        }
                        CanDoFadeOut = sr.ReadLine().GetEcmLineValue().ToInt32();
                    }
                }
            }
            switch (ID)
            {
                case 100: GfxElementType100E = new GfxElementType100(sr, vers); break;
                case 101: GfxElementType101E = new GfxElementType101(sr, vers); break;
                case 102: GfxElementType102E = new GfxElementType102(sr, vers); break;
                case 110: GfxElementType110E = new GfxElementType110(sr, vers); break;
                case 120: GfxElementType120E = new GfxElementType120(sr, vers); break;
                case 121: GfxElementType121E = new GfxElementType121(sr, vers); break;
                case 122: GfxElementType122E = new GfxElementType122(sr, vers); break;
                case 123: GfxElementType123E = new GfxElementType123(sr, vers); break;
                case 124: GfxElementType124E = new GfxElementType124(sr, vers); break;
                case 125: GfxElementType125E = new GfxElementType125(sr, vers); break;
                case 130: GfxElementType130E = new GfxElementType130(sr, vers); break;
                case 140: GfxElementType140E = new GfxElementType140(sr, vers); break;
                case 150: GfxElementType150E = new GfxElementType150(sr, vers); break;
                case 151: GfxElementType151E = new GfxElementType151(sr, vers); break;
                case 152: GfxElementType152E = new GfxElementType152(sr, vers); break;
                case 160: GfxElementType160E = new GfxElementType160(sr, vers); break;
                case 170: GfxElementType170E = new GfxElementType170(sr, vers); break;
                case 180: GfxElementType180E = new GfxElementType180(sr, vers); break;
                case 190: GfxElementType190E = new GfxElementType190(sr, vers); break;
                case 200: GfxElementType200E = new GfxElementType200(sr, vers); break;
                case 210: GfxElementType210E = new GfxElementType210(sr, vers); break;
                case 211: GfxElementType211E = new GfxElementType211(sr, vers); break;
                case 220: GfxElementType220E = new GfxElementType220(sr, vers); break;
                case 230: GfxElementType230E = new GfxElementType230(sr, vers); break;
                case 240: GfxElementType240E = new GfxElementType240(sr, vers); break;
                default:
                    {
                        break;
                    }
            }
            StartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
            KEYPOINTCOUNT = sr.ReadLine().GetEcmLineValue().ToInt32();
            for (int i = 0; i < KEYPOINTCOUNT; i++)
            {
                KeyPoints.Add(new KeyPoint(sr, vers));
            }
            GfxStructure.ID = Name;
        }
        public void Save(StreamWriter sw, int vers)
        {
            if (ID == 210)
            {
                ID = 160;
                GfxElementType160E = new GfxElementType160();
                GfxElementType160E.ModelPath = "";
                GfxElementType160E.ModelActName = "";
                GfxElementType160E.Loops = -1;
                GfxElementType160E.AlphaCmp = 10;
                GfxElementType160E.WriteZ = 10;
            }
            if (ID == 999)
            {
                ID = 150;
                GfxElementType150E = new GfxElementType150();
                GfxElementType150E.Alpha = GfxElementType152E.Alpha;
                GfxElementType150E.Alpha_2 = GfxElementType152E.Alpha_2;
                GfxElementType150E.Alpha_3 = GfxElementType152E.Alpha_3;
                GfxElementType150E.Amplitude = GfxElementType152E.Amplitude;
                GfxElementType150E.Amplitude_1 = GfxElementType152E.Amplitude_1;
                GfxElementType150E.BufLen = GfxElementType152E.BufLen;
                GfxElementType150E.DestNum = GfxElementType152E.DestNum;
                GfxElementType150E.DestVal = GfxElementType152E.DestVal;
                GfxElementType150E.Dest_S = GfxElementType152E.Dest_S;
                GfxElementType150E.EndPos = GfxElementType152E.EndPos;
                GfxElementType150E.Filter = GfxElementType152E.Filter;
                GfxElementType150E.FixWaveLength = GfxElementType152E.FixWaveLength;
                GfxElementType150E.Interval = GfxElementType152E.Interval;
                GfxElementType150E.isappend = GfxElementType152E.isappend;
                GfxElementType150E.istaildisappear = GfxElementType152E.istaildisappear;
                GfxElementType150E.LightNum = GfxElementType152E.LightNum;
                GfxElementType150E.Normal = GfxElementType152E.Normal;
                GfxElementType150E.NumWaves = GfxElementType152E.NumWaves;
                GfxElementType150E.OctaveNum = GfxElementType152E.OctaveNum;
                GfxElementType150E.Persistence = GfxElementType152E.Persistence;
                GfxElementType150E.Pos1Enable = GfxElementType152E.Pos1Enable;
                GfxElementType150E.Pos2Enable = GfxElementType152E.Pos2Enable;
                GfxElementType150E.renderside = GfxElementType152E.renderside;
                GfxElementType150E.Segs = GfxElementType152E.Segs;
                GfxElementType150E.StartPos = GfxElementType152E.StartPos;
                GfxElementType150E.StartTime = GfxElementType152E.StartTime;
                GfxElementType150E.tailfadeout = GfxElementType152E.tailfadeout;
                GfxElementType150E.TransTime_s = GfxElementType152E.TransTime_s;
                GfxElementType150E.UseNormal = GfxElementType152E.UseNormal;
                GfxElementType150E.vertslife = GfxElementType152E.vertslife;
                GfxElementType150E.WaveLen = GfxElementType152E.WaveLen;
                GfxElementType150E.WaveLen_2 = GfxElementType152E.WaveLen_2;
                GfxElementType150E.WaveMoving = GfxElementType152E.WaveMoving;
                GfxElementType150E.WaveMovingSpeed = GfxElementType152E.WaveMovingSpeed;
                GfxElementType150E.Width = GfxElementType152E.Width;
                GfxElementType150E.Width_2 = GfxElementType152E.Width_2;
                GfxElementType150E.Width_3 = GfxElementType152E.Width_3;
            }
            sw.WriteParameter("GFXELEMENTID", ID);
            sw.WriteParameter("Name", Name);
            sw.WriteParameter("SrcBlend", SrcBlend);
            sw.WriteParameter("DestBlend", DestBlend);
            sw.WriteParameter("RepeatCount", RepeatCount);
            sw.WriteParameter("RepeatDelay", RepeatDelay);
            sw.WriteParameter("TexFile", TexFile);
            sw.WriteParameter("BindEle", BindEle);
            sw.WriteParameter("ZEnable", ZEnable);
            sw.WriteParameter("MatchGrnd", MatchGrnd);
            if (vers >= 57)
                sw.WriteParameter("GroundHeight", GroundHeight);
            sw.WriteParameter("TexRow", TexRow);
            sw.WriteParameter("TexCol", TexCol);
            sw.WriteParameter("TexInterval", TexInterval);
            sw.WriteParameter("Priority", Priority);
            sw.WriteParameter("IsDummy", IsDummy);
            sw.WriteParameter("DummyEle", DummyEle);
            sw.WriteParameter("Warp", Warp);
            sw.WriteParameter("TileMode", TileMode);
            sw.WriteParameter("TexSpeed", TexSpeed1);
            sw.WriteParameter("TexSpeed", TexSpeed2);
            if (vers >= 41)
            {
                sw.WriteParameter("UReverse", UReverse);
                sw.WriteParameter("VReverse", VReverse);
                sw.WriteParameter("UVExchg", UVExchg);
            }
            if (vers >= 45)
            {
                sw.WriteParameter("RenderLayer", RenderLayer);
            }
            if (vers >= 58)
            {
                sw.WriteParameter("NoDownSample", NoDownSample);
            }
            if (vers >= 74)
            {
                sw.WriteParameter("ResetLoopEnd", ResetLoopEnd);
                if (vers >= 75)
                {
                    sw.WriteParameter("TexAnimMaxTime", TexAnimMaxTime);
                    if (vers >= 92)
                    {
                        if (vers >= 95)
                        {
                            sw.WriteParameter("PSFileVersion", PSFileVersion);

                        }
                        sw.WriteParameter("ShaderFile", ShaderFile);
                        sw.WriteParameter("ShaderTex", ShaderTex);
                        sw.WriteParameter("PSConstCount", PSConstCount);
                        for (int i = 0; i < PSConstCount; i++)
                        {
                            PSConsts[i].Save(sw, vers);
                        }
                        sw.WriteParameter("CanDoFadeOut", CanDoFadeOut);

                    }
                }
            }
            switch (ID)
            {
                case 100: GfxElementType100E.Save(sw, vers); break;
                case 101: GfxElementType101E.Save(sw, vers); break;
                case 102: GfxElementType102E.Save(sw, vers); break;
                case 110: GfxElementType110E.Save(sw, vers); break;
                case 120: GfxElementType120E.Save(sw, vers); break;
                case 121: GfxElementType121E.Save(sw, vers); break;
                case 122: GfxElementType122E.Save(sw, vers); break;
                case 123: GfxElementType123E.Save(sw, vers); break;
                case 124: GfxElementType124E.Save(sw, vers); break;
                case 125: GfxElementType125E.Save(sw, vers); break;
                case 130: GfxElementType130E.Save(sw, vers); break;
                case 140: GfxElementType140E.Save(sw, vers); break;
                case 150: GfxElementType150E.Save(sw, vers); break;
                case 152: GfxElementType152E.Save(sw, vers); break;
                case 160: GfxElementType160E.Save(sw, vers); break;
                case 170: GfxElementType170E.Save(sw, vers); break;
                case 180: GfxElementType180E.Save(sw, vers); break;
                case 190: GfxElementType190E.Save(sw, vers); break;
                case 200: GfxElementType200E.Save(sw, vers); break;
                case 210: GfxElementType210E.Save(sw, vers); break;
                case 211: GfxElementType211E.Save(sw, vers); break;
                case 220: GfxElementType220E.Save(sw, vers); break;
                case 230: GfxElementType230E.Save(sw, vers); break;
                case 240: GfxElementType240E.Save(sw, vers); break;
            }
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("KEYPOINTCOUNT", KEYPOINTCOUNT);
            for (int i = 0; i < KEYPOINTCOUNT; i++)
            {
                KeyPoints[i].Save(sw, vers);
            }
        }
    }
}
