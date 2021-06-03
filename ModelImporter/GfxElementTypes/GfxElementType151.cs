using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType151
    {
        public float Deviation;
        public float StepMin;
        public float StepMax;
        public float WidthStart;
        public float WidthEnd;
        public float AlphaStart;
        public float AlphaEnd;
        public float StrokeAmp;
        public int MaxSteps;
        public int MaxBranches;
        public int Interval;
        public int PerBolts;
        public int Circles;
        public GfxElementType151(StreamReaderL sr, int vers)
        {
            Deviation = sr.ReadLine().GetEcmLineValue().ToSingle();
            StepMin = sr.ReadLine().GetEcmLineValue().ToSingle();
            StepMax = sr.ReadLine().GetEcmLineValue().ToSingle();
            WidthStart = sr.ReadLine().GetEcmLineValue().ToSingle();
            WidthEnd = sr.ReadLine().GetEcmLineValue().ToSingle();
            AlphaStart = sr.ReadLine().GetEcmLineValue().ToSingle();
            AlphaEnd = sr.ReadLine().GetEcmLineValue().ToSingle();
            StrokeAmp = sr.ReadLine().GetEcmLineValue().ToSingle();
            MaxSteps = sr.ReadLine().GetEcmLineValue().ToInt32();
            MaxBranches = sr.ReadLine().GetEcmLineValue().ToInt32();
            Interval = sr.ReadLine().GetEcmLineValue().ToInt32();
            PerBolts = sr.ReadLine().GetEcmLineValue().ToInt32();
            Circles = sr.ReadLine().GetEcmLineValue().ToInt32();
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("Deviation", Deviation);
            sw.WriteParameter("StepMin", StepMin);
            sw.WriteParameter("StepMax", StepMax);
            sw.WriteParameter("WidthStart", WidthStart);
            sw.WriteParameter("WidthEnd", WidthEnd);
            sw.WriteParameter("AlphaStart", AlphaStart);
            sw.WriteParameter("AlphaEnd", AlphaEnd);
            sw.WriteParameter("StrokeAmp", StrokeAmp);
            sw.WriteParameter("MaxSteps", MaxSteps);
            sw.WriteParameter("MaxBranches", MaxBranches);
            sw.WriteParameter("Interval", Interval);
            sw.WriteParameter("PerBolts", PerBolts);
            sw.WriteParameter("Circles", Circles);
        }
    }

}
