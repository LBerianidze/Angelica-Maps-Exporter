using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class CtrlType100
    {
        public int CtrlType = 100;
        public float StartTime;
        public float EndTime;
        public Vector3D Pos;
        public float Vel;
        public float Acc;
        public CtrlType100(StreamReader sr, int vers)
        {
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            Pos = new Vector3D(sr, false);
            Vel = sr.ReadLine().GetEcmLineValue().ToSingle();
            Acc = sr.ReadLine().GetEcmLineValue().ToSingle();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            Pos.Save(sw, "Dir");
            sw.WriteParameter("Vel", Vel);
            sw.WriteParameter("Acc", Acc);
        }
    }
    public class CtrlType101
    {
        public int CtrlType = 101;
        public float StartTime;
        public float EndTime;
        public float Vel;
        public float Acc;
        public CtrlType101(StreamReader sr, int vers)
        {
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            Vel = sr.ReadLine().GetEcmLineValue().ToSingle();
            Acc = sr.ReadLine().GetEcmLineValue().ToSingle();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            sw.WriteParameter("Vel", Vel);
            sw.WriteParameter("Acc", Acc);
        }
    }
    public class CtrlType102
    {
        public int CtrlType = 102;
        public float StartTime;
        public float EndTime;
        public Vector3D Pos;
        public Vector3D Axis;
        public float Vel;
        public float Acc;
        public CtrlType102(StreamReader sr, int vers)
        {
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            Pos = new Vector3D(sr, false);
            Axis = new Vector3D(sr, false);
            Vel = sr.ReadLine().GetEcmLineValue().ToSingle();
            Acc = sr.ReadLine().GetEcmLineValue().ToSingle();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            Pos.Save(sw, "Dir");
            Axis.Save(sw, "Axis");
            sw.WriteParameter("Vel", Vel);
            sw.WriteParameter("Acc", Acc);
        }
    }
    public class CtrlType103
    {
        public int CtrlType = 103;
        public float StartTime;
        public float EndTime;
        public Vector3D Pos;
        public Vector3D Axis;
        public float Vel;
        public float Acc;
        public CtrlType103(StreamReader sr, int vers)
        {
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            Pos = new Vector3D(sr, false);
            Axis = new Vector3D(sr, false);
            Vel = sr.ReadLine().GetEcmLineValue().ToSingle();
            Acc = sr.ReadLine().GetEcmLineValue().ToSingle();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            Pos.Save(sw, "Dir");
            Axis.Save(sw, "Axis");
            sw.WriteParameter("Vel", Vel);
            sw.WriteParameter("Acc", Acc);
        }
    }
    public class CtrlType104
    {
        public int CtrlType = 104;
        public float StartTime;
        public float EndTime;
        public Vector3D Pos;
        public float Vel;
        public float Acc;
        public CtrlType104(StreamReader sr, int vers)
        {
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            Pos = new Vector3D(sr, false);
            Vel = sr.ReadLine().GetEcmLineValue().ToSingle();
            Acc = sr.ReadLine().GetEcmLineValue().ToSingle();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            Pos.Save(sw, "Pos");
            sw.WriteParameter("Vel", Vel);
            sw.WriteParameter("Acc", Acc);
        }
    }
    public class CtrlType105
    {
        public int CtrlType = 105;
        public float StartTime;
        public float EndTime;
        public int[] ColorDelta = new int[4];
        public CtrlType105(StreamReader sr, int vers)
        {
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            string[] Vals3 = sr.ReadLine().GetEcmLineValue().Split(new string[] { "," }, StringSplitOptions.None);
            for (int i = 0; i < 4; i++)
            {
                ColorDelta[i] = Vals3[i].ToInt32();
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            sw.WriteParameter("ColorDelta", ColorDelta[0] + ", " + ColorDelta[1] + ", " + ColorDelta[2] + ", " + ColorDelta[3]);
        }
    }
    public class CtrlType106
    {
        public int CtrlType = 106;
        public float StartTime;
        public float EndTime;
        public Vector3D ScaleChage;
        public CtrlType106(StreamReader sr, int vers)
        {
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            ScaleChage = new Vector3D(sr, false);
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            ScaleChage.Save(sw, "ScaleChage");
        }
    }
    public class CtrlType107
    {
        public int CtrlType = 107;
        public float StartTime;
        public float EndTime;
        public int BufLen;
        public float Amplitude;
        public int WaveLen;
        public float Persistence;
        public int OctaveNum;
        public int BaseColor;
        public CtrlType107(StreamReader sr, int vers)
        {
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            BufLen = sr.ReadLine().GetEcmLineValue().ToInt32();
            Amplitude = sr.ReadLine().GetEcmLineValue().ToSingle();
            WaveLen = sr.ReadLine().GetEcmLineValue().ToInt32();
            Persistence = sr.ReadLine().GetEcmLineValue().ToSingle();
            OctaveNum = sr.ReadLine().GetEcmLineValue().ToInt32();
            BaseColor = sr.ReadLine().GetEcmLineValue().ToInt32();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            sw.WriteParameter("BufLen", BufLen);
            sw.WriteParameter("Amplitude", Amplitude);
            sw.WriteParameter("WaveLen", WaveLen);
            sw.WriteParameter("Persistence", Persistence);
            sw.WriteParameter("OctaveNum", OctaveNum);
            sw.WriteParameter("BaseColor", BaseColor);
        }
    }
    public class CtrlType108
    {
        public int CtrlType = 108;
        public float StartTime;
        public float EndTime;
        public int Color;
        public int Count;
        public List<Color> Colors = new List<Color>();
        public int AlphaOnly;
        public CtrlType108(StreamReader sr, int vers = 79)
        {
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            Color = sr.ReadLine().GetEcmLineValue().ToInt32();
            Count = sr.ReadLine().GetEcmLineValue().ToInt32();
            for (int i = 0; i < Count; i++)
            {
                Colors.Add(new Color(sr, vers));
            }
            if (vers > 79)
            {
                AlphaOnly = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            sw.WriteParameter("Color", Color);
            sw.WriteParameter("Count", Count);
            for (int i = 0; i < Count; i++)
            {
                Colors[i].Save(sw, vers);
            }
            if (vers > 79)
            {
                sw.WriteParameter("AlphaOnly", AlphaOnly);
            }
        }
    }
    public class CtrlType109
    {
        public int CtrlType = 109;
        public float StartTime;
        public float EndTime;
        public int BufLen;
        public float Amplitude;
        public int WaveLen;
        public float Persistence;
        public int OctaveNum;
        public CtrlType109(StreamReader sr, int vers)
        {
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            BufLen = sr.ReadLine().GetEcmLineValue().ToInt32();
            Amplitude = sr.ReadLine().GetEcmLineValue().ToSingle();
            WaveLen = sr.ReadLine().GetEcmLineValue().ToInt32();
            Persistence = sr.ReadLine().GetEcmLineValue().ToSingle();
            OctaveNum = sr.ReadLine().GetEcmLineValue().ToInt32();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            sw.WriteParameter("BufLen", BufLen);
            sw.WriteParameter("Amplitude", Amplitude);
            sw.WriteParameter("WaveLen", WaveLen);
            sw.WriteParameter("Persistence", Persistence);
            sw.WriteParameter("OctaveNum", OctaveNum);
        }
    }
    public class CtrlType110
    {
        public int CtrlType = 110;
        public float StartTime;
        public float EndTime;
        public int CalcDir;
        public List<Vector3D> Vectors = new List<Vector3D>();
        public CtrlType110(StreamReader sr, int vers)
        {
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            if(vers>=44)
            {
                CalcDir = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            Vectors = new List<Vector3D>(sr.ReadLine().GetEcmLineValue().ToInt32());
            for (int i = 0; i < Vectors.Capacity; i++)
            {
                Vectors.Add(new Vector3D(sr, false));
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            if(vers>=44)
            {
                sw.WriteParameter("CalcDir", CalcDir);
            }
            sw.WriteParameter("Count", Vectors.Count);
            for (int i = 0; i < Vectors.Count; i++)
            {
                Vectors[i].Save(sw, "Pos");
            }
        }
    }
    public class CtrlType111
    {
        public int CtrlType = 111;
        public float StartTime;
        public float EndTime;
        public float Scale;
        public int Count;
        public List<Scale> Scales = new List<Scale>();
        public CtrlType111(StreamReader sr, int vers)
        {
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            Scale = sr.ReadLine().GetEcmLineValue().ToSingle();
            Count = sr.ReadLine().GetEcmLineValue().ToInt32();
            for (int i = 0; i < Count; i++)
            {
                Scales.Add(new Scale(sr, vers));
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            sw.WriteParameter("Scale", Scale);
            sw.WriteParameter("Count", Count);
            for (int i = 0; i < Count; i++)
            {
                Scales[i].Save(sw, vers);
            }
        }
    }
    public class CtrlType112
    {
        public int CtrlType = 112;
        public float StartTime;
        public float EndTime;
        public int BufLen;
        public float Amplitude;
        public int WaveLen;
        public float Persistence;
        public int OctaveNum;
        public CtrlType112(StreamReader sr, int vers, bool e = false)
        {
            if (e) CtrlType = sr.ReadLine().GetEcmLineValue().ToInt32();
            StartTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            EndTime = sr.ReadLine().GetEcmLineValue().ToSingle();
            BufLen = sr.ReadLine().GetEcmLineValue().ToInt32();
            Amplitude = sr.ReadLine().GetEcmLineValue().ToSingle();
            WaveLen = sr.ReadLine().GetEcmLineValue().ToInt32();
            Persistence = sr.ReadLine().GetEcmLineValue().ToSingle();
            OctaveNum = sr.ReadLine().GetEcmLineValue().ToInt32();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("CtrlType", CtrlType);
            sw.WriteParameter("StartTime", StartTime);
            sw.WriteParameter("EndTime", EndTime);
            sw.WriteParameter("BufLen", BufLen);
            sw.WriteParameter("Amplitude", Amplitude);
            sw.WriteParameter("WaveLen", WaveLen);
            sw.WriteParameter("Persistence", Persistence);
            sw.WriteParameter("OctaveNum", OctaveNum);

        }
    }
    public class KeyPoint
    {
        public int InterpolateMode;
        public int TimeSpan;
        public Vector3D Position;
        public int Color;
        public float Scale;
        public Vector3D Direction;
        public float Rad_2D;
        public int CtrlMethodCount;
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
        public KeyPoint(StreamReader sr, int vers)
        {
            InterpolateMode = sr.ReadLine().GetEcmLineValue().ToInt32();
            TimeSpan = sr.ReadLine().GetEcmLineValue().ToInt32();
            Position = new Vector3D(sr, false);
            Color = sr.ReadLine().GetEcmLineValue().ToInt32();
            Scale = sr.ReadLine().GetEcmLineValue().ToSingle();
            Direction = new Vector3D(sr, true);
            Rad_2D = sr.ReadLine().GetEcmLineValue().ToSingle();
            CtrlMethodCount = sr.ReadLine().GetEcmLineValue().ToInt32();
            for (int i = 0; i < CtrlMethodCount; i++)
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
            sw.WriteParameter("InterpolateMode", InterpolateMode);
            sw.WriteParameter("TimeSpan", TimeSpan);
            Position.Save(sw, "Position");
            sw.WriteParameter("Color", Color);
            sw.WriteParameter("Scale", Scale);
            Direction.Save(sw, "Direction");
            sw.WriteParameter("Rad_2D", Rad_2D);
            sw.WriteParameter("CtrlMethodCount", CtrlMethodCount);
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
    public class Color
    {
        public int Color_P;
        public float TimeSpan;
        public Color(StreamReader sr, int vers)
        {
            Color_P = sr.ReadLine().GetEcmLineValue().ToInt32();
            TimeSpan = sr.ReadLine().GetEcmLineValue().ToSingle();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("Color", Color_P);
            sw.WriteParameter("TimeSpan", TimeSpan);
        }
    }
    public class Scale
    {
        public float Scale_P;
        public float TimeSpan;
        public Scale(StreamReader sr, int vers)
        {
            Scale_P = sr.ReadLine().GetEcmLineValue().ToSingle();
            TimeSpan = sr.ReadLine().GetEcmLineValue().ToSingle();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("Scale", Scale_P);
            sw.WriteParameter("TimeSpan", TimeSpan);
        }
    }
    public class Vector3D
    {
        public decimal X;
        public decimal Y;
        public decimal Z;
        public decimal R;
        public bool IsDirection;
        public Vector3D(StreamReader sr, bool Direction)
        {
            try
            {
                string[] Vals = sr.ReadLine().GetEcmLineValue().Split(new string[] { "," }, StringSplitOptions.None);
                X = Vals[0].ToDecimal();
                Y = Vals[1].ToDecimal();
                Z = Vals[2].ToDecimal();
                if (Direction)
                {
                    R = Vals[3].ToDecimal();
                    IsDirection = true;
                }
            }
            catch { }
        }
        public void Save(StreamWriter sw, string parameter)
        {
            string XS = X.RoundToSix(6);
            string YS = Y.RoundToSix(6);
            string ZS = Z.RoundToSix(6);
            string fullline = XS + ", " + YS + ", " + ZS;
            if (IsDirection)
            {
                string RS = R.RoundToSix(6);
                fullline += ", " + RS;
            }
            sw.WriteParameter(parameter, fullline);
        }
    }
    public class WHNumber
    {
        public decimal[] Vector3D = new decimal[3];
        public string Color;
        public WHNumber(StreamReader sr, int vers)
        {
            string[] Vals = sr.ReadLine().Split(new string[] { " " }, StringSplitOptions.None);
            Vector3D[0] = Vals[0].ToDecimal();
            Vector3D[1] = Vals[1].ToDecimal();
            Vector3D[2] = Vals[2].ToDecimal();
            Color = sr.ReadLine().GetEcmLineValue();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteLine(string.Join(" ", Vector3D.Select(e => e.RoundToSix(6))));
            sw.WriteParameter("dwColor", Color);
        }
    }
    public class PSConst
    {
        public int PSConstIndex;
        public Vector3D PSConstValue;
        public int PSLoopCount;
        public int PSTargetCount;
        public List<PSConstTarget> PSConstTargets = new List<PSConstTarget>();
        public PSConst(StreamReader sr, int vers)
        {
            PSConstIndex = sr.ReadLine().GetEcmLineValue().ToInt32();
            PSConstValue = new Vector3D(sr, true);
            PSLoopCount = sr.ReadLine().GetEcmLineValue().ToInt32();
            PSTargetCount = sr.ReadLine().GetEcmLineValue().ToInt32();
            for (int i = 0; i < PSTargetCount; i++)
            {
                PSConstTargets.Add(new PSConstTarget(sr, vers));
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("PSConstIndex", PSConstIndex);
            PSConstValue.Save(sw, "PSConstValue");
            sw.WriteParameter("PSLoopCount", PSLoopCount);
            sw.WriteParameter("PSTargetCount", PSTargetCount);
            for (int i = 0; i < PSTargetCount; i++)
            {
                PSConstTargets[i].Save(sw, vers);
            }

        }
    }
    public class PSConstTarget
    {
        public int PSInterval;
        public Vector3D PSConstValue;
        public PSConstTarget(StreamReader sr, int vers)
        {
            PSInterval = sr.ReadLine().GetEcmLineValue().ToInt32();
            PSConstValue = new Vector3D(sr, false);
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("PSInterval", PSInterval);
            PSConstValue.Save(sw, "PSConstValue");
        }
    }
    public class GridAnimationLines
    {
        public int GridAnimationLines_Count;
        public List<string> Lines = new List<string>();
        public GridAnimationLines(StreamReader sr)
        {
            GridAnimationLines_Count = sr.ReadLine().GetEcmLineValue().ToInt32();
            for (int i = 0; i < GridAnimationLines_Count; i++)
            {
                Lines.Add(sr.ReadLine());
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("GridAnimationLines", GridAnimationLines_Count);
            for (int i = 0; i < GridAnimationLines_Count; i++)
            {
                sw.WriteParameter("", Lines[i]);
            }

        }
    }
}
