using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapFilesImporter
{
    public class BhtFile
    {
        static Dictionary<int, List<Node>> f = new Dictionary<int, List<Node>>();
        public static void WriteBht(string path, List<Unknown2> bhtinfo, SizeF sz)
        {
            var sp = path.Split('\\');
            if (!Directory.Exists(string.Join("\\", sp.Take(sp.Count() - 1))))
            {
                Directory.CreateDirectory(string.Join("\\", sp.Take(sp.Count() - 1)));
            }
            BinaryWriter bw = new BinaryWriter(File.Create(path));
            bw.Write(1651798630);
            bw.Write(1);
            bw.Write(bhtinfo.Count);
            foreach (var item in bhtinfo)
            {
                bw.Write(item.Positions[0]);
                bw.Write(item.Positions[1]);
                bw.Write(item.Positions[2]);
                bw.Write(item.Extents[0]);
                bw.Write(item.Extents[1]);
                bw.Write(item.Extents[2]);
                bw.Write(item.Mins[0]);
                bw.Write(item.Mins[1]);
                bw.Write(item.Mins[2]);
                bw.Write(item.Maxs[0]);
                bw.Write(item.Maxs[1]);
                bw.Write(item.Maxs[2]);
                bw.Write(item.flags);
                bw.Write(item.Brushes.Count);
                foreach (var it in item.Brushes)
                {
                    bw.Write(it.normalX);
                    bw.Write(it.normalY);
                    bw.Write(it.normalZ);
                    bw.Write(it.distance);
                    bw.Write(it.bevel);

                }
            }
            CreateNode(0, 0, sz.Width / 2, sz.Height / 2, false, 0);
            foreach (var item in f)
            {
                foreach (Node t in item.Value)
                {
                    bw.Write(t.p1);
                    bw.Write(t.p2);
                    bw.Write(t.p3);
                    bw.Write(t.p4);
                    bw.Write(t.b1);
                    bw.Write(t.b2);
                }
            }
            bw.Close();
        }
        static private void CreateNode(float f1, float f2, float f3, float f4, bool last, int depth)
        {
            while (true)
            {
                Node node = new Node()
                {
                    p1 = f1,
                    p2 = f2,
                    p3 = f3,
                    p4 = f4,
                    b1 = 1,
                    b2 = Convert.ToByte(last)
                };
                List<Node> nodes = null;
                try
                {
                    nodes = f[depth];
                }
                catch { }
                if (nodes == null)
                {
                    nodes = new  List<Node>();
                    f.Add(depth, nodes);
                }
                nodes.Add(node);
                if (last)
                {
                    break;
                }
                depth++;
                last = (f3 / 2) <= 8;
                CreateNode(f1 - f3 / 2.0F, f2 - f4 / 2.0F, f3 / 2.0F, f4 / 2.0F, last, depth);
                CreateNode(f1 - f3 / 2.0F, f2 + f4 / 2.0F, f3 / 2.0F, f4 / 2.0F, last, depth);
                CreateNode(f1 + f3 / 2.0F, f2 + f4 / 2.0F, f3 / 2.0F, f4 / 2.0F, last, depth);
                f4 /= 2.0F;
                f3 /= 2.0F;
                f2 = f2 - f4;
                f1 = f1 + f3;
            }
        }
    }
    class Node
    {
        public float p1;
        public float p2;
        public float p3;
        public float p4;
        public byte b1;
        public byte b2;
    }
}
