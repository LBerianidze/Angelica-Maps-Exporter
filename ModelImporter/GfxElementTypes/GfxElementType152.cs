using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType152
    {
        public int BufLen;
        public float Amplitude;
        public int WaveLen;
        public float Persistence;
        public int OctaveNum;
        public Vector3D StartPos;
        public Vector3D EndPos;
        public int Segs;
        public int LightNum;
        public float WaveLen_2;
        public int Interval;
        public float Width;
        public float Width_2;
        public float Alpha;
        public float Alpha_2;
        public float Width_3;
        public float Alpha_3;
        public float Amplitude_1;
        public int Pos1Enable;
        public int Pos2Enable;
        public int UseNormal;
        public Vector3D Normal;
        public int Filter;
        public int isappend;
        public int renderside;
        public int istaildisappear;
        public int vertslife;
        public int tailfadeout;
        public int DestNum;
        public float[] Dest_S;
        public int[] TransTime_s;
        public int TransTime;
        public int StartTime;
        public float DestVal;
        public int WaveMoving;
        public float WaveMovingSpeed;
        public int FixWaveLength;
        public float NumWaves;
        public GfxElementType152(StreamReaderL sr, int vers)
        {
            BufLen = sr.ReadLine().GetEcmLineValue().ToInt32();
            Amplitude = sr.ReadLine().GetEcmLineValue().ToSingle();
            WaveLen = sr.ReadLine().GetEcmLineValue().ToInt32();
            Persistence = sr.ReadLine().GetEcmLineValue().ToSingle();
            OctaveNum = sr.ReadLine().GetEcmLineValue().ToInt32();
            StartPos = new Vector3D(sr, false);
            EndPos = new Vector3D(sr, false);
            Segs = sr.ReadLine().GetEcmLineValue().ToInt32();
            LightNum = sr.ReadLine().GetEcmLineValue().ToInt32();
            WaveLen_2 = sr.ReadLine().GetEcmLineValue().ToSingle();
            Interval = sr.ReadLine().GetEcmLineValue().ToInt32();
            Width = sr.ReadLine().GetEcmLineValue().ToSingle();
            Width_2 = sr.ReadLine().GetEcmLineValue().ToSingle();
            Alpha = sr.ReadLine().GetEcmLineValue().ToSingle();
            Alpha_2 = sr.ReadLine().GetEcmLineValue().ToSingle();
            if (vers >= 35)
            {
                Width_3 = sr.ReadLine().GetEcmLineValue().ToSingle();
                Alpha_3 = sr.ReadLine().GetEcmLineValue().ToSingle();
            }
            if (vers >= 103)
            {
                DestNum = sr.ReadLine().GetEcmLineValue().ToInt32();
                StartTime = sr.ReadLine().GetEcmLineValue().ToInt32();
                DestVal = sr.ReadLine().GetEcmLineValue().ToSingle();
                Dest_S = new float[DestNum];
                TransTime_s = new int[DestNum];
                for (int i = 0; i < DestNum; i++)
                {
                    Dest_S[i] = sr.ReadLine().GetEcmLineValue().ToSingle();
                }
                for (int i = 0; i < DestNum; i++)
                {
                    TransTime_s[i] = sr.ReadLine().GetEcmLineValue().ToInt32();
                }
            }
            else
            {
                Amplitude_1 = sr.ReadLine().GetEcmLineValue().ToSingle();
            }
            Pos1Enable = sr.ReadLine().GetEcmLineValue().ToInt32();
            Pos2Enable = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (vers >= 38)
            {
                UseNormal = sr.ReadLine().GetEcmLineValue().ToInt32();
                Normal = new Vector3D(sr, false);
            }
            if (vers >= 72)
            {
                Filter = sr.ReadLine().GetEcmLineValue().ToInt32();
                if (vers >= 103)
                {
                    WaveMoving = sr.ReadLine().GetEcmLineValue().ToInt32();
                    WaveMovingSpeed = sr.ReadLine().GetEcmLineValue().ToSingle();
                    FixWaveLength = sr.ReadLine().GetEcmLineValue().ToInt32();
                    NumWaves = sr.ReadLine().GetEcmLineValue().ToSingle();
                }
            }
            if (Pos1Enable == 0 && vers >= 59)
            {
                isappend = sr.ReadLine().GetEcmLineValue().ToInt32();
                renderside = sr.ReadLine().GetEcmLineValue().ToInt32();
                istaildisappear = sr.ReadLine().GetEcmLineValue().ToInt32();
                vertslife = sr.ReadLine().GetEcmLineValue().ToInt32();
                tailfadeout = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("BufLen", BufLen);
            sw.WriteParameter("Amplitude", Amplitude);
            sw.WriteParameter("WaveLen", WaveLen);
            sw.WriteParameter("Persistence", Persistence);
            sw.WriteParameter("OctaveNum", OctaveNum);
            StartPos.Save(sw, "StartPos");
            EndPos.Save(sw, "EndPos");
            sw.WriteParameter("Segs", Segs);
            sw.WriteParameter("LightNum", LightNum);
            sw.WriteParameter("WaveLen", WaveLen_2);
            sw.WriteParameter("Interval", Interval);
            sw.WriteParameter("Width", Width);
            sw.WriteParameter("Width", Width_2);
            sw.WriteParameter("Alpha", Alpha);
            sw.WriteParameter("Alpha", Alpha_2);
            if (vers >= 35)
            {
                sw.WriteParameter("Width", Width_3);
                sw.WriteParameter("Alpha", Alpha_3);
            }
            if (vers >= 103)
            {
                sw.WriteParameter("DestNum", DestNum);
                sw.WriteParameter("StartTime", StartTime);
                sw.WriteParameter("DestVal", DestVal);
                for (int i = 0; i < DestNum; i++)
                {
                    sw.WriteParameter("DestVal", Dest_S[i]);
                }
                for (int i = 0; i < DestNum; i++)
                {
                    sw.WriteParameter("TransTime", TransTime_s[i]);
                }
            }
            else
            {
                sw.WriteParameter("Amplitude", Amplitude_1);
            }
            sw.WriteParameter("Pos1Enable", Pos1Enable);
            sw.WriteParameter("Pos2Enable", Pos2Enable);
            if (vers >= 38)
            {
                sw.WriteParameter("UseNormal", UseNormal);
                Normal.Save(sw, "Normal");
            }
            if (vers >= 72)
            {
                sw.WriteParameter("Filter", Filter);
                if (vers >= 103)
                {
                    sw.WriteParameter("WaveMoving", WaveMoving);
                    sw.WriteParameter("WaveMovingSpeed", WaveMovingSpeed);
                    sw.WriteParameter("FixWaveLength", FixWaveLength);
                    sw.WriteParameter("NumWaves", NumWaves);
                }
            }
            if (Pos1Enable == 0 && vers>=59)
            {
                sw.WriteParameter("isappend", isappend);
                sw.WriteParameter("renderside", renderside);
                sw.WriteParameter("istaildisappear", istaildisappear);
                sw.WriteParameter("vertslife", vertslife);
                sw.WriteParameter("tailfadeout", tailfadeout);
            }
        }

    }

}
