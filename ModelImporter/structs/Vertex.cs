using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Media3D;

namespace ModelImporter
{
    public class Vertex
    {
        public Point3D Position;
        public float[] VertexWeight;
        public byte[] BoneIndex;
        public Vector3D Normal;
        public Point UVCoords;
        public static Vertex Read(BinaryReader r, int vertex_type = 0)
        {
            Vertex vertex = new Vertex();
            vertex.Position = new Point3D
            {
                X = r.ReadSingle(),
                Y = r.ReadSingle(),
                Z = r.ReadSingle()
            };
            if (vertex_type == 0)
            {
                vertex.VertexWeight = new float[3];
                for (int i = 0; i < 3; i++)
                {
                    vertex.VertexWeight[i] = r.ReadSingle();
                }
                vertex.BoneIndex = new byte[4];
                for (int j = 0; j < 4; j++)
                {
                    vertex.BoneIndex[j] = r.ReadByte();
                }
            }
            vertex.Normal = new Vector3D
            {
                X = r.ReadSingle(),
                Z = r.ReadSingle(),
                Y = r.ReadSingle(),
            };
            vertex.UVCoords = new Point
            {
                X = r.ReadSingle(),
                Y = r.ReadSingle()
            };

            return vertex;
        }
    }
}
