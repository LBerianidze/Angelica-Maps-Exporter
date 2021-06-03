using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace ModelImporter
{
    public class MeshObject
    {
        public string Name;
        public int TexIndex;
        public int MatIndex;
        public int VertexCount;
        public int IndexCount;
        public int faceVertsCount;
        public Vertex[] Vertexes;
        public Face[] Faces;
        public static MeshObject Read(BinaryReader r, int MeshType, int type = 0)
        {
            MeshObject meshObject = new MeshObject();
            int count = r.ReadInt32();
            byte[] bytes = r.ReadBytes(count);
            meshObject.Name = Encoding.GetEncoding("GB2312").GetString(bytes);
            meshObject.TexIndex = r.ReadInt32();
            meshObject.MatIndex = r.ReadInt32();
            if (type == 1)
            {
                r.ReadInt32();
            }
            meshObject.VertexCount = r.ReadInt32();
            meshObject.IndexCount = r.ReadInt32();
            if (MeshType == 1) meshObject.faceVertsCount = r.ReadInt32() / 3;
            int VertexesCount = MeshType == 1 ? meshObject.IndexCount : meshObject.VertexCount;
            int vertextype = MeshType == 1 ? 1 : type;
            meshObject.Vertexes = new Vertex[VertexesCount];
            for (int i = 0; i < VertexesCount; i++)
            {
                meshObject.Vertexes[i] = Vertex.Read(r, vertextype);
            }
            int FacesCount = MeshType == 1 ? meshObject.faceVertsCount : meshObject.IndexCount / 3;
            meshObject.Faces = new Face[FacesCount];
            for (int i = 0; i < FacesCount; i++)
            {
                meshObject.Faces[i] = Face.Read(r);
            }
            return meshObject;
        }
    }
}
