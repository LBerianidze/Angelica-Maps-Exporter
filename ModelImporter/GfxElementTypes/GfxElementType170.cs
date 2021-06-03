using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType170
    {
        public int PathNum;
        public List<string> Path_s = new List<string>();
        public string Path;
        public int SoundVer;
        public int Force2D;
        public int IsLoop;
        public int Volume;
        public float MinDist;
        public float MaxDist;
        public int VolMin;
        public int VolMax;
        public int AbsoluteVolume;
        public float PitchMin;
        public float PitchMax;
        public int FixSpeed;
        public string Path_1;
        public int UseCustom;
        public float MinDist_1;
        public float MaxDist_1;
        public int SilentHeader;
        public GfxElementType170(StreamReaderL sr, int vers)
        {
            if (vers >= 89)
            {
                PathNum = sr.ReadLine().GetEcmLineValue().ToInt32();
                for (int i = 0; i < PathNum; i++)
                {
                    Path_s.Add(sr.ReadLine().GetEcmLineValue());
                }
                Path = Path_s.FirstOrDefault();
            }
            else
            {
                Path = sr.ReadLine().GetEcmLineValue();
            }
            SoundVer = sr.ReadLine().GetEcmLineValue().ToInt32();
            Force2D = sr.ReadLine().GetEcmLineValue().ToInt32();
            IsLoop = sr.ReadLine().GetEcmLineValue().ToInt32();
            if (vers >= 89)
            {
                VolMin = sr.ReadLine().GetEcmLineValue().ToInt32();
                VolMax = sr.ReadLine().GetEcmLineValue().ToInt32();
                if (vers >= 97)
                {
                    AbsoluteVolume = sr.ReadLine().GetEcmLineValue().ToInt32();
                }
                PitchMin = sr.ReadLine().GetEcmLineValue().ToSingle();
                PitchMax = sr.ReadLine().GetEcmLineValue().ToSingle();
                Volume = VolMin;
            }
            else
            {
                Volume = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            MinDist = sr.ReadLine().GetEcmLineValue().ToSingle();
            MaxDist = sr.ReadLine().GetEcmLineValue().ToSingle();
            if (vers >= 96)
            {
                if (vers >= 100)
                {
                    FixSpeed = sr.ReadLine().GetEcmLineValue().ToInt32();
                    if (vers >= 101)
                    {
                        SilentHeader = sr.ReadLine().GetEcmLineValue().ToInt32();
                    }
                }
                Path = sr.ReadLine().GetEcmLineValue();
                UseCustom = sr.ReadLine().GetEcmLineValue().ToInt32();
                MinDist_1 = sr.ReadLine().GetEcmLineValue().ToSingle();
                MaxDist_1 = sr.ReadLine().GetEcmLineValue().ToSingle();
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            if (vers >= 89)
            {
                sw.WriteParameter("PathNum", PathNum);
                for (int i = 0; i < PathNum; i++)
                {
                    sw.WriteParameter("Path", Path_s[i]);
                }
            }
            else
            {
                sw.WriteParameter("Path", Path);
            }
            sw.WriteParameter("SoundVer", SoundVer);
            sw.WriteParameter("Force2D", Force2D);
            sw.WriteParameter("IsLoop", IsLoop);
            if (vers >= 89)
            {
                sw.WriteParameter("VolMin", VolMin);
                sw.WriteParameter("VolMax", VolMax);
                if (vers >= 97)
                {
                    sw.WriteParameter("AbsoluteVolume", AbsoluteVolume);
                }
                sw.WriteParameter("PitchMin", PitchMin);
                sw.WriteParameter("PitchMax", PitchMax);

            }
            else
            {
                sw.WriteParameter("Volume", Volume);
            }
            sw.WriteParameter("MinDist", MinDist);
            sw.WriteParameter("MaxDist", MaxDist);
            if (vers >= 96)
            {
                if (vers >= 100)
                {
                    sw.WriteParameter("FixSpeed", FixSpeed);
                    if (vers >= 101)
                    {
                        sw.WriteParameter("SilentHeader", SilentHeader);
                    }
                }
                sw.WriteParameter("Path", Path);
                sw.WriteParameter("UseCustom", UseCustom);
                sw.WriteParameter("MinDist", MinDist_1);
                sw.WriteParameter("MaxDist", MaxDist_1);
            }
        }
    }

}
