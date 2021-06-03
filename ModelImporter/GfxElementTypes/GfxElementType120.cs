using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GfxReader
{
    public class GfxElementType120
    {
        public int Quota;
        public float ParticleWidth;
        public float ParticleHeight;
        public int _3DParticle;
        public int Facing;
        public int ScaleNoOff;
        public int NoScale1;
        public int NoScale2;
        public float OrgPt1;
        public float OrgPt2;
        public int IsUseParUV;
        public int IsStartOnGrnd;
        public int StopEmitWhenFade;
        public int InitRandomTexture;
        public float EmissionRate;
        public float Angle;
        public float Speed;
        public float ParAcc;
        public Vector3D AccDir;
        public float Acc;
        public float TTL;
        public int ColorMin;
        public int ColorMax;
        public float ScaleMin;
        public float ScaleMax;
        public float RotMin;
        public float RotMax;
        public int IsSurface;
        public int IsBind;
        public int IsDrag;
        public float DragPow;
        public Vector3D ParIniDir;
        public int IsUseHSVInterp;
        public int AffectorCount;
        public List<CtrlType100> CtrlType100S = new List<CtrlType100>();
        public List<CtrlType101> CtrlType101S = new List<CtrlType101>();
        public List<CtrlType102> CtrlType102S = new List<CtrlType102>();
        public List<CtrlType103> CtrlType103S = new List<CtrlType103>();
        public List<CtrlType104> CtrlType104S = new List<CtrlType104>();
        public List<CtrlType105> CtrlType105S = new List<CtrlType105>();
        public List<CtrlType106> CtrlType106S = new List<CtrlType106>();
        public List<CtrlType107> CtrlType107S = new List<CtrlType107>();
        public List<CtrlType108> CtrlType108S = new List<CtrlType108>();
        public List<CtrlType109> CtrlType109S = new List<CtrlType109>();
        public List<CtrlType110> CtrlType110S = new List<CtrlType110>();
        public List<CtrlType111> CtrlType111S = new List<CtrlType111>();
        public List<CtrlType112> CtrlType112S = new List<CtrlType112>();
        public GfxElementType120(StreamReaderL sr, int vers)
        {
            Quota = sr.ReadLine().GetEcmLineValue().ToInt32();
            ParticleWidth = sr.ReadLine().GetEcmLineValue().ToSingle();
            ParticleHeight = sr.ReadLine().GetEcmLineValue().ToSingle();
            _3DParticle = sr.ReadLine().GetEcmLineValue().ToInt32();
            Facing = sr.ReadLine().GetEcmLineValue().ToInt32();
            ScaleNoOff = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (vers >= 37)
            {
                NoScale1 = sr.ReadLine().GetEcmLineValue().ToInt32();
                NoScale2 = sr.ReadLine().GetEcmLineValue().ToInt32();
                OrgPt1 = sr.ReadLine().GetEcmLineValue().ToSingle();
                OrgPt2 = sr.ReadLine().GetEcmLineValue().ToSingle();
            }
            if (vers >= 72)
            {
                IsUseParUV = sr.ReadLine().GetEcmLineValue().ToInt32();
                if (vers >= 79)
                {
                    IsStartOnGrnd = sr.ReadLine().GetEcmLineValue().ToInt32();
                    if (vers >= 92)
                    {
                        StopEmitWhenFade = sr.ReadLine().GetEcmLineValue().ToInt32();
                        if (vers >= 99)
                        {
                            InitRandomTexture = sr.ReadLine().GetEcmLineValue().ToInt32();
                        }
                    }
                }
            }
            EmissionRate = sr.ReadLine().GetEcmLineValue().ToSingle();
            Angle = sr.ReadLine().GetEcmLineValue().ToSingle();
            Speed = sr.ReadLine().GetEcmLineValue().ToSingle();
            ParAcc = sr.ReadLine().GetEcmLineValue().ToSingle();
            AccDir = new Vector3D(sr, false);
            Acc = sr.ReadLine().GetEcmLineValue().ToSingle();
            TTL = sr.ReadLine().GetEcmLineValue().ToSingle();
            ColorMin = sr.ReadLine().GetEcmLineValue().ToInt32();
            ColorMax = sr.ReadLine().GetEcmLineValue().ToInt32();
            ScaleMin = sr.ReadLine().GetEcmLineValue().ToSingle();
            ScaleMax = sr.ReadLine().GetEcmLineValue().ToSingle();
            RotMin = sr.ReadLine().GetEcmLineValue().ToSingle();
            RotMax = sr.ReadLine().GetEcmLineValue().ToSingle();
            IsSurface = sr.ReadLine().GetEcmLineValue().ToInt32();
            IsBind = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (vers >= 43)
            {
                IsDrag = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            if (vers >= 48)
            {
                DragPow = sr.ReadLine().GetEcmLineValue().ToSingle();
            }
            if (vers >= 61)
            {
                ParIniDir = new Vector3D(sr, false);
            }
            if (vers >= 70)
            {
                IsUseHSVInterp = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            AffectorCount = sr.ReadLine().GetEcmLineValue().ToInt32();
            for (int i = 0; i < AffectorCount; i++)
            {
                int j = sr.ReadLine().GetEcmLineValue().ToInt32();
                switch (j)
                {
                    case 100: CtrlType100S.Add(new CtrlType100(sr, vers)); break;
                    case 101: CtrlType101S.Add(new CtrlType101(sr, vers)); break;
                    case 102: CtrlType102S.Add(new CtrlType102(sr, vers)); break;
                    case 103: CtrlType103S.Add(new CtrlType103(sr, vers)); break;
                    case 104: CtrlType104S.Add(new CtrlType104(sr, vers)); break;
                    case 105: CtrlType105S.Add(new CtrlType105(sr, vers)); break;
                    case 106: CtrlType106S.Add(new CtrlType106(sr, vers)); break;
                    case 107: CtrlType107S.Add(new CtrlType107(sr, vers)); break;
                    case 108: CtrlType108S.Add(new CtrlType108(sr, vers)); break;
                    case 109: CtrlType109S.Add(new CtrlType109(sr, vers)); break;
                    case 110: CtrlType110S.Add(new CtrlType110(sr, vers)); break;
                    case 111: CtrlType111S.Add(new CtrlType111(sr, vers)); break;
                    case 112: CtrlType112S.Add(new CtrlType112(sr, vers)); break;
                }
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("Quota", Quota);
            sw.WriteParameter("ParticleWidth", ParticleWidth);
            sw.WriteParameter("ParticleHeight", ParticleHeight);
            sw.WriteParameter("3DParticle", _3DParticle);
            sw.WriteParameter("Facing", Facing);
            sw.WriteParameter("ScaleNoOff", ScaleNoOff);
            if (vers >= 37)
            {
                sw.WriteParameter("NoScale", NoScale1);
                sw.WriteParameter("NoScale", NoScale2);
                sw.WriteParameter("OrgPt", OrgPt1);
                sw.WriteParameter("OrgPt", OrgPt2);
            }
            if (vers >= 72)
            {
                sw.WriteParameter("IsUseParUV", IsUseParUV);
                if (vers >= 79)
                {
                    sw.WriteParameter("IsStartOnGrnd", IsStartOnGrnd);
                    if (vers >= 92)
                    {
                        sw.WriteParameter("StopEmitWhenFade", StopEmitWhenFade);
                        if (vers >= 99)
                        {
                            sw.WriteParameter("InitRandomTexture", InitRandomTexture);
                        }
                    }
                }
            }
            sw.WriteParameter("EmissionRate", EmissionRate);
            sw.WriteParameter("Angle", Angle);
            sw.WriteParameter("Speed", Speed);
            sw.WriteParameter("ParAcc", ParAcc);
            AccDir.Save(sw, "AccDir");
            sw.WriteParameter("Acc", Acc);
            sw.WriteParameter("TTL", TTL);
            sw.WriteParameter("ColorMin", ColorMin);
            sw.WriteParameter("ColorMax", ColorMax);
            sw.WriteParameter("ScaleMin", ScaleMin);
            sw.WriteParameter("ScaleMax", ScaleMax);
            sw.WriteParameter("RotMin", RotMin);
            sw.WriteParameter("RotMax", RotMax);
            sw.WriteParameter("IsSurface", IsSurface);
            sw.WriteParameter("IsBind", IsBind);
            if(vers>=43)
            {
                sw.WriteParameter("IsDrag", IsDrag);
            }
            if (vers >= 48)
            {
                sw.WriteParameter("DragPow", DragPow);
            }
            if (vers >= 61)
            {
                ParIniDir.Save(sw, "ParIniDir");
            }
            if (vers >= 70)
            {
                sw.WriteParameter("IsUseHSVInterp", IsUseHSVInterp);
            }
            sw.WriteParameter("AffectorCount", AffectorCount);
            for (int i = 0; i < CtrlType100S.Count; i++)
                CtrlType100S[i].Save(sw, vers);
            for (int i = 0; i < CtrlType101S.Count; i++)
                CtrlType101S[i].Save(sw, vers);
            for (int i = 0; i < CtrlType102S.Count; i++)
                CtrlType102S[i].Save(sw, vers);
            for (int i = 0; i < CtrlType103S.Count; i++)
                CtrlType103S[i].Save(sw, vers);
            for (int i = 0; i < CtrlType104S.Count; i++)
                CtrlType104S[i].Save(sw, vers);
            for (int i = 0; i < CtrlType105S.Count; i++)
                CtrlType105S[i].Save(sw, vers);
            for (int i = 0; i < CtrlType106S.Count; i++)
                CtrlType106S[i].Save(sw, vers);
            for (int i = 0; i < CtrlType107S.Count; i++)
                CtrlType107S[i].Save(sw, vers);
            for (int i = 0; i < CtrlType108S.Count; i++)
                CtrlType108S[i].Save(sw, vers);
            for (int i = 0; i < CtrlType109S.Count; i++)
                CtrlType109S[i].Save(sw, vers);
            for (int i = 0; i < CtrlType110S.Count; i++)
                CtrlType110S[i].Save(sw, vers);
            for (int i = 0; i < CtrlType111S.Count; i++)
                CtrlType111S[i].Save(sw, vers);
            for (int i = 0; i < CtrlType112S.Count; i++)
                CtrlType112S[i].Save(sw, vers);
        }
    }
}