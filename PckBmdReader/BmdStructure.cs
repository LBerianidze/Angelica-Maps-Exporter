using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckBmdReader
{
    public class Bbbb
    {
        public int BlockKey;
        public int LitModelKey;
        public Bbbb(int a, int b)
        {
            BlockKey = a;
            LitModelKey = b;
        }
    }
    public class BmdStructure
    {
        public byte[] Header = new byte[4];
        public int version;
        public byte collideOnly;
        public short u_3;
        public short u_33;
        public Location BmdLocation;
        public int ModelsNum;
        public int AllAmount;
        public List<Mesh_Struct> Meshes = new List<Mesh_Struct>();
        public List<byte[]> UnkBytes = new List<byte[]>();
        public List<Unknown2> BhtInfo = new List<Unknown2>();
        public List<string> Textures = new List<string>();

        public BmdStructure(string path) : this(new BinaryReader(File.Open(path, FileMode.Open)))
        {

        }
        public BmdStructure(BinaryReader br)
        {
            Header = br.ReadBytes(4);
            version = br.ReadInt32();
            if (version == -2147483647)
            {
                collideOnly = br.ReadByte();
            }
            else
            {
                br.BaseStream.Position -= 4;
            }
            u_3 = br.ReadInt16();
            u_33 = br.ReadInt16();
            BmdLocation = new Location(br);
            ModelsNum = br.ReadInt32();
            for (int i = 0; i < ModelsNum; i++)
            {
                Meshes.Add(new Mesh_Struct(br, version, u_3, Textures));
            }
            try
            {
                AllAmount = br.ReadInt32();
                for (int i = 0; i < AllAmount; i++)
                {
                    int val = br.ReadInt32();
                    List<byte> arr = new List<byte>();
                    arr.AddRange(BitConverter.GetBytes(val));
                    arr.AddRange(br.ReadBytes(4 * val));
                    UnkBytes.Add(arr.ToArray());
                }
                for (int i = 0; i < AllAmount; i++)
                {
                    BhtInfo.Add(new Unknown2(br));
                }
            }
            catch
            { }
            Textures.RemoveAll(z => string.IsNullOrWhiteSpace(z));
            br.Close();
            Textures.RemoveAll(e => e == null || e == "");
            Textures = Textures.GroupBy(e => e).Select(f => f.FirstOrDefault()).ToList();
        }
        public void Save(BinaryWriter bw)
        {
            bw.Write(Header);
            bw.Write(version);
            if (version == -2147483647)
            {
                bw.Write(collideOnly);
            }
            else
            {
                bw.BaseStream.Position -= 4;
            }
            bw.Write(u_3);
            bw.Write(u_33);
            BmdLocation.Save(bw);
            bw.Write(ModelsNum);
            for (int i = 0; i < ModelsNum; i++)
            {
                //Meshes[i].Save(bw, version, u_3);
            }
            bw.Write(AllAmount);
            for (int i = 0; i < AllAmount; i++)
            {
                bw.Write(UnkBytes[i]);
            }
            for (int i = 0; i < AllAmount; i++)
            {
                BhtInfo[i].Save(bw);
            }
        }
    }
    public class Mesh_Struct
    {
        public short version2;
        public short u_6;
        public byte a;
        public string name;
        public string[] Textures = new string[4];
        public int vertexcount;
        public int facescount;
        public byte b;
        public List<Vertex> Vertexes = new List<Vertex>();
        public List<Face> Faces = new List<Face>();
        public List<Vector3D> Vectors = new List<Vector3D>();
        public List<Unknown_struct> Unknown_structS = new List<Unknown_struct>();
        public Location MeshLocation;
        public MaterialStruct Material;
        public byte[] bytes;
        public UnknownLoc UnknownLocE;
        int namelength;
        public Mesh_Struct(BinaryReader br, int version, short u_3, List<string> textures)
        {
            version2 = br.ReadInt16();
            u_6 = br.ReadInt16();
            if (u_6 == 1 && version2 > 1)
            {
                a = br.ReadByte();
            }
            if (version2 > 1)
            {
                name = Encoding.GetEncoding(936).GetString(br.ReadBytes(64)).TrimEnd('\0');

                for (int i = 0; i < 4; i++)
                {
                    textures.Add(Encoding.GetEncoding(936).GetString(br.ReadBytes(64)).TrimEnd('\0'));
                }
                Textures = textures.ToArray();
            }
            else
            {
                namelength = br.ReadInt32();
                name = Encoding.GetEncoding(936).GetString(br.ReadBytes(namelength)).TrimEnd('\0');
                textures.Add(Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32())).TrimEnd('\0'));
                Textures[0] = textures.FirstOrDefault();
            }
            vertexcount = br.ReadInt32();
            facescount = br.ReadInt32();
            if (version2 == 6)
                b = br.ReadByte();
            for (int i = 0; i < vertexcount; i++)
            {
                Vertexes.Add(new Vertex(br));
            }
            for (int i = 0; i < facescount; i++)
            {
                Faces.Add(new Face(br));
            }
            for (int i = 0; i < vertexcount; i++)
            {
                Vectors.Add(new Vector3D(br));
            }
            if (version2 > 1)
            {
                for (int i = 0; i < vertexcount; i++)
                {
                    Unknown_structS.Add(new Unknown_struct(br));
                }
                MeshLocation = new Location(br);

            }
            if (version == -2147483647)
            {
                if (version2 != 4)
                    Material = new MaterialStruct(br, u_3, vertexcount);
                if (version2 == 1 || (u_6 != 1 && version2 == 0))
                {
                    UnknownLocE = new UnknownLoc(br);
                }
            }
            if (u_6 == 1 && version2 == 1)
            {
                bytes = br.ReadBytes(848);
            }
            if (u_6 == 1 && version2 == 0)
            {
                bytes = br.ReadBytes(1502);
            }
        }
    }
    public class MaterialStruct
    {
        byte[] Header = new byte[11];
        public float[] values = new float[16];
        public float scale;
        public byte isClothing;
        public byte unk;
        public Su Su;
        public MaterialStruct(BinaryReader br, short u_3, int vertexcount)
        {
            Header = br.ReadBytes(11);
            for (int i = 0; i < 16; i++)
            {
                values[i] = br.ReadSingle();
            }
            scale = br.ReadSingle();
            isClothing = br.ReadByte();
            if (scale > 0)
            {
                unk = br.ReadByte();
            }
            if (u_3 > 2)
            {
                Su = new Su(br, vertexcount);
            }
        }
        public void Save(BinaryWriter bw, short u_3)
        {
            bw.Write(Header);
            for (int i = 0; i < 16; i++)
            {
                bw.Write(values[i]);
            }
            bw.Write(scale);
            bw.Write(isClothing);
            if (scale > 0)
            {
                bw.Write(unk);
            }
            if (u_3 > 2)
            {
                Su.Save(bw);
            }
        }
    }
    public class Su
    {
        public List<float> u_21 = new List<float>();
        public Su(BinaryReader br, int count)
        {
            for (int i = 0; i < count; i++)
            {
                u_21.Add(br.ReadSingle());
                u_21.Add(br.ReadSingle());
            }
        }
        public void Save(BinaryWriter bw)
        {
            for (int i = 0; i < u_21.Count; i++)
            {
                bw.Write(u_21[i]);
            }
        }
    }
    public class UnknownLoc
    {
        public float scaleX;
        public float scaleY;
        public float scaleZ;
        public float directionX;
        public float directionY;
        public float directionZ;
        public float upX;
        public float upY;
        public float upZ;
        public float positionX;
        public float positionY;
        public float positionZ;

        public int Unk1;
        public float Unk2;
        public float Unk3;
        public float Unk4;
        public UnknownLoc(BinaryReader br)
        {
            scaleX = br.ReadSingle();
            scaleY = br.ReadSingle();
            scaleZ = br.ReadSingle();
            directionX = br.ReadSingle();
            directionY = br.ReadSingle();
            directionZ = br.ReadSingle();
            upX = br.ReadSingle();
            upY = br.ReadSingle();
            upZ = br.ReadSingle();
            positionX = br.ReadSingle();
            positionY = br.ReadSingle();
            positionZ = br.ReadSingle();
            Unk1 = br.ReadInt32();
            Unk2 = br.ReadSingle();
            Unk3 = br.ReadSingle();
            Unk4 = br.ReadInt16();
        }
        public void Save(BinaryWriter bw)
        {
            bw.Write(scaleX);
            bw.Write(scaleY);
            bw.Write(scaleZ);
            bw.Write(directionX);
            bw.Write(directionY);
            bw.Write(directionZ);
            bw.Write(upX);
            bw.Write(upY);
            bw.Write(upZ);
            bw.Write(positionX);
            bw.Write(positionY);
            bw.Write(positionZ);
            bw.Write(Unk1);
            bw.Write(Unk2);
            bw.Write(Unk3);
            bw.Write(Unk4);
        }
    }
    public class Unknown_struct
    {
        public byte[] unk1To8;
        public Unknown_struct(BinaryReader br)
        {
            unk1To8 = br.ReadBytes(8);
        }
        public void Save(BinaryWriter bw)
        {
            bw.Write(unk1To8);
        }
    }
    public class Face
    {
        public short one;
        public short two;
        public short three;
        public Face(BinaryReader br)
        {
            one = br.ReadInt16();
            two = br.ReadInt16();
            three = br.ReadInt16();
        }
        public void Save(BinaryWriter bw)
        {
            bw.Write(one);
            bw.Write(two);
            bw.Write(three);
        }
    }
    public class Vertex
    {
        public Vector3D Vector;
        public int unk_1;
        public float u;
        public float v;
        public Vertex(BinaryReader br)
        {
            Vector = new Vector3D(br);
            unk_1 = br.ReadInt32();
            u = br.ReadSingle();
            v = br.ReadSingle();
        }
        public void Save(BinaryWriter bw)
        {
            Vector.Save(bw);
            bw.Write(unk_1);
            bw.Write(u);
            bw.Write(v);
        }
    }
    public class Vector3D
    {
        public float X;
        public float Y;
        public float Z;
        public Vector3D(BinaryReader br)
        {
            X = br.ReadSingle();
            Y = br.ReadSingle();
            Z = br.ReadSingle();
        }
        public void Save(BinaryWriter bw)
        {
            bw.Write(X);
            bw.Write(Y);
            bw.Write(Z);
        }
    }
    public class Location
    {
        public float scaleX;
        public float scaleY;
        public float scaleZ;
        public float directionX;
        public float directionY;
        public float directionZ;
        public float upX;
        public float upY;
        public float upZ;
        public float positionX;
        public float positionY;
        public float positionZ;
        public Location(BinaryReader br)
        {
            scaleX = br.ReadSingle();
            scaleY = br.ReadSingle();
            scaleZ = br.ReadSingle();
            directionX = br.ReadSingle();
            directionY = br.ReadSingle();
            directionZ = br.ReadSingle();
            upX = br.ReadSingle();
            upY = br.ReadSingle();
            upZ = br.ReadSingle();
            positionX = br.ReadSingle();
            positionY = br.ReadSingle();
            positionZ = br.ReadSingle();
        }
        public void Save(BinaryWriter bw)
        {
            bw.Write(scaleX);
            bw.Write(scaleY);
            bw.Write(scaleZ);
            bw.Write(directionX);
            bw.Write(directionY);
            bw.Write(directionZ);
            bw.Write(upX);
            bw.Write(upY);
            bw.Write(upZ);
            bw.Write(positionX);
            bw.Write(positionY);
            bw.Write(positionZ);
        }
    }
    public class CDBrushe
    {
        public float normalX;
        public float normalY;
        public float normalZ;
        public float distance;
        public byte bevel;
        public CDBrushe(BinaryReader br)
        {
            normalX = br.ReadSingle();
            normalY = br.ReadSingle();
            normalZ = br.ReadSingle();
            distance = br.ReadSingle();
            bevel = br.ReadByte();
        }
        public void Save(BinaryWriter bw)
        {
            bw.Write(normalX);
            bw.Write(normalY);
            bw.Write(normalZ);
            bw.Write(distance);
            bw.Write(bevel);
        }
    }
    public class Unknown2
    {
        public float[] Positions = new float[3];
        public float[] Extents = new float[3];
        public float[] Mins = new float[3];
        public float[] Maxs = new float[3];
        public int flags;
        public List<CDBrushe> Brushes;
        public Unknown2(BinaryReader br)
        {
            Positions[0] = br.ReadSingle();
            Positions[1] = br.ReadSingle();
            Positions[2] = br.ReadSingle();
            Extents[0] = br.ReadSingle();
            Extents[1] = br.ReadSingle();
            Extents[2] = br.ReadSingle();
            Mins[0] = br.ReadSingle();
            Mins[1] = br.ReadSingle();
            Mins[2] = br.ReadSingle();
            Maxs[0] = br.ReadSingle();
            Maxs[1] = br.ReadSingle();
            Maxs[2] = br.ReadSingle();
            flags = br.ReadInt32();
            int i = br.ReadInt32();
            Brushes = new List<CDBrushe>(i);
            for (int y = 0; y < i; y++)
            {
                Brushes.Add(new CDBrushe(br));
            }
        }
        public void Save(BinaryWriter bw)
        {
            bw.Write(Positions[0]);
            bw.Write(Positions[1]);
            bw.Write(Positions[2]);
            bw.Write(Extents[0]);
            bw.Write(Extents[1]);
            bw.Write(Extents[2]);
            bw.Write(Mins[0]);
            bw.Write(Mins[1]);
            bw.Write(Mins[2]);
            bw.Write(Maxs[0]);
            bw.Write(Maxs[1]);
            bw.Write(Maxs[2]);
            bw.Write(flags);
            bw.Write(Brushes.Count);
            for (int i = 0; i < Brushes.Count; i++)
            {
                Brushes[i].Save(bw);
            }
        }
    };
}
