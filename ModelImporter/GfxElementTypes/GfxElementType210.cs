using LBLIBRARY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxReader
{
    public class GfxElementType210
    {
        public int wNumber;
        public int hNumber;
        public List<WHNumber> WHNumbers = new List<WHNumber>();
        public float fGridSize;
        public float fZOffset;
        public int keyNumber;
        public List<GridAnimationLines> GridAnimations = new List<GridAnimationLines>();
        public int AffByScl;
        public int RotFromView;
        public float fOffsetHeight;
        public GfxElementType210(StreamReaderL sr, int vers)
        {
            wNumber = sr.ReadLine().GetEcmLineValue().ToInt32();
            hNumber = sr.ReadLine().GetEcmLineValue().ToInt32();
            for (int i = 0; i < wNumber * hNumber; i++)
            {
                WHNumbers.Add(new WHNumber(sr, vers));
            }
            fGridSize = sr.ReadLine().GetEcmLineValue().ToSingle();
            fZOffset = sr.ReadLine().GetEcmLineValue().ToSingle();
            if (vers >= 99)
            {
                keyNumber = sr.ReadLine().GetEcmLineValue().ToInt32();
            }
            for (int i = 0; i < keyNumber; i++)
            {
                GridAnimations.Add(new GridAnimationLines(sr));
            }
            if (vers >= 100)
            {
                AffByScl = sr.ReadLine().GetEcmLineValue().ToInt32();
                RotFromView = sr.ReadLine().GetEcmLineValue().ToInt32();
                if (vers >= 101)
                {
                    fOffsetHeight = sr.ReadLine().GetEcmLineValue().ToSingle();
                }
            }
        }
        public void Save(StreamWriter sw, int vers)
        {
            sw.WriteParameter("wNumber", wNumber);
            sw.WriteParameter("hNumber", hNumber);
            for (int i = 0; i < WHNumbers.Count; i++)
            {
                WHNumbers[i].Save(sw, vers);
            }
            sw.WriteParameter("fGridSize", fGridSize);
            sw.WriteParameter("fZOffset", fZOffset);
            if (vers >= 99)
            {
                sw.WriteParameter("keyNumber", keyNumber);
            }
            for (int i = 0; i < keyNumber; i++)
            {
                GridAnimations[i].Save(sw, vers);
            }
            if (vers >= 100)
            {
                sw.WriteParameter("AffByScl", AffByScl);
                sw.WriteParameter("RotFromView", RotFromView);
                if (vers >= 101)
                {
                    sw.WriteParameter("fOffsetHeight", fOffsetHeight);
                }
            }
        }
    }

}
