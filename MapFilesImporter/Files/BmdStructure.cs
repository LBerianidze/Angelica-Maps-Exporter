using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MapFilesImporter
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
        public int m_dwBuildVersion;
        public byte collideOnly;
        public Location BmdLocation;
        public int ModelsNum;
        public int m_strCHFFileLength;
        public List<Mesh_Struct> Meshes = new List<Mesh_Struct>();
        public List<byte[]> UnkBytes = new List<byte[]>();
        public List<Unknown2> BhtInfo = new List<Unknown2>();
        public List<string> Textures = new List<string>();
        public static string TexturesNameTo = "building\\textures";
        public BmdStructure(string path) : this(new BinaryReader(File.Open(path, FileMode.Open)))
        {

        }

        private readonly bool mLightMap;
        private readonly bool bShareData;
        public BmdStructure(BinaryReader br, bool change = false)
        {
            Header = br.ReadBytes(4);
            m_dwBuildVersion = br.ReadInt32();
            if (m_dwBuildVersion == -2147483647)
            {
                collideOnly = br.ReadByte();
            }
            else
            {
                br.BaseStream.Position -= 4;
            }
            var m_dwVersion = br.ReadInt32();
            BmdLocation = new Location(br);
            ModelsNum = br.ReadInt32();
            mLightMap = br.ReadBoolean();
            bShareData = br.ReadBoolean();
            if (bShareData)
            {
                //cShareData with dynamic Length
                br.ReadBytes(br.ReadInt32());
            }
            for (var i = 0; i < ModelsNum; i++)
            {
                Meshes.Add(new Mesh_Struct(br, m_dwVersion, mLightMap, bShareData, Textures));
            }
            if(mLightMap)
            {
                br.ReadBytes(br.ReadInt32());
                br.ReadBytes(br.ReadInt32());
            }
            int dwExtraSize = br.ReadInt32();
            if(dwExtraSize>0)
            {
                br.BaseStream.Position = br.BaseStream.Position - dwExtraSize;
                br.ReadBytes(dwExtraSize);
            }
            if(m_dwBuildVersion == -2147483647)
            {
                this.m_strCHFFileLength = br.ReadInt32();
                for (var i = 0; i < this.m_strCHFFileLength; i++)
                {
                    var val = br.ReadInt32();
                    var arr = new List<byte>();
                    arr.AddRange(BitConverter.GetBytes(val));
                    arr.AddRange(br.ReadBytes(4 * val));
                    UnkBytes.Add(arr.ToArray());
                }
                for (var i = 0; i < this.m_strCHFFileLength; i++)
                {
                    BhtInfo.Add(new Unknown2(br));
                }
            }
            Textures.RemoveAll(z => string.IsNullOrWhiteSpace(z));
            br.Close();
            Textures.RemoveAll(e => e == null || e == "");
            Textures = Textures.GroupBy(e => e).Select(f => f.FirstOrDefault()).ToList();
            if (change)
            {
                for (var e = 0; e < Meshes.Count; e++)
                {
                    for (var i = 0; i < Meshes[e].Textures.Length; i++)
                    {
                        if (Meshes[e].Textures[i] != "")
                        {
                            Meshes[e].Textures[i] = TexturesNameTo + Meshes[e].Textures[i].Substring(17, Meshes[e].Textures[i].Length - 17);
                        }
                    }
                }
            }
        }
        public void Save(BinaryWriter bw)
        {
            //bw.Write(Header);
            //bw.Write(version);
            //if (version == -2147483647)
            //{
            //    bw.Write(collideOnly);
            //}
            //else
            //{
            //    bw.BaseStream.Position -= 4;
            //}
            //bw.Write(u_3);
            //bw.Write(u_33);
            //BmdLocation.Save(bw);
            //bw.Write(ModelsNum);
            //for (var i = 0; i < ModelsNum; i++)
            //{
            //    Meshes[i].Save(bw, version, u_3);
            //}
            //bw.Write(AllAmount);
            //for (var i = 0; i < AllAmount; i++)
            //{
            //    bw.Write(UnkBytes[i]);
            //}
            //for (var i = 0; i < AllAmount; i++)
            //{
            //    BhtInfo[i].Save(bw);
            //}
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
        private readonly int namelength;
        public Mesh_Struct(BinaryReader br, int m_dwVersion, bool mLightMap, bool bShareData, List<string> AllTextures)
        {
            var bSupportLightMap = br.ReadBoolean();
            var bUseLightMap = br.ReadBoolean();
            if (!bShareData)
            {
                namelength = br.ReadInt32();
                name = Encoding.GetEncoding(936).GetString(br.ReadBytes(namelength)).TrimEnd('\0');
                Textures[0] = Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32())).TrimEnd('\0');
                AllTextures.Add(Textures[0]);
                Textures[1] = "";
                Textures[2] = "";
                Textures[3] = "";
                vertexcount = br.ReadInt32();
                facescount = br.ReadInt32();

                for (var i = 0; i < vertexcount; i++)
                {
                    Vertexes.Add(new Vertex(br));
                }
                for (var i = 0; i < facescount; i++)
                {
                    Faces.Add(new Face(br));
                }
                for (var i = 0; i < vertexcount; i++)
                {
                    Vectors.Add(new Vector3D(br));
                }
                Material = new MaterialStruct(br);
                if (bSupportLightMap)
                {
                    br.ReadBytes(8 * vertexcount);
                }
            }
            bool bHasVertexLight = false;
            if (m_dwVersion == 0x10000300)
            {
                if (!bSupportLightMap || !bUseLightMap) bHasVertexLight = true;
            }
            else
            {
                 bHasVertexLight = br.ReadBoolean();
            }
            if(bHasVertexLight)
            {
                br.ReadBytes(4 * vertexcount);
                br.ReadBytes(4 * vertexcount);
            }
            if (m_dwVersion == 0x10000302)
            {
                bool bUseAlphaBlend = br.ReadBoolean();
            }
            //AeDAABB
            br.ReadBytes(60);
            int dwExtraSize = br.ReadInt32();
            if(dwExtraSize>0)
            {
                br.BaseStream.Position = br.BaseStream.Position - dwExtraSize;
                br.ReadBytes(dwExtraSize);
            }
        }
        public void Save(BinaryWriter bw, int version, short u_3)
        {
            bw.Write(version2);
            bw.Write(u_6);
            if (u_6 == 1 && version2 > 1)
            {
                bw.Write(a);
            }
            if (version2 > 1)
            {
                bw.Write(LBLIBRARY.ExtensionMethods.GetBytesFromString(name, 64, Encoding.GetEncoding(936)));
                for (var i = 0; i < 4; i++)
                {
                    bw.Write(LBLIBRARY.ExtensionMethods.GetBytesFromString(Textures[i], 64, Encoding.GetEncoding(936)));

                }
            }
            else
            {
                bw.Write(LBLIBRARY.ExtensionMethods.GetBytesFromString(name, namelength, Encoding.GetEncoding(936)));
                bw.Write(LBLIBRARY.ExtensionMethods.GetBytesFromString(Textures[0], Textures[0].Length, Encoding.GetEncoding(936)));
            }
            bw.Write(vertexcount);
            bw.Write(facescount);
            if (version2 == 6)
            {
                bw.Write(b);
            }
            for (var i = 0; i < vertexcount; i++)
            {
                Vertexes[i].Save(bw);
            }
            for (var i = 0; i < facescount; i++)
            {
                Faces[i].Save(bw);
            }
            for (var i = 0; i < vertexcount; i++)
            {
                Vectors[i].Save(bw);
            }
            if (version2 > 1)
            {
                for (var i = 0; i < vertexcount; i++)
                {
                    Unknown_structS[i].Save(bw);
                }
                MeshLocation.Save(bw);
            }
            if (version == -2147483647)
            {
                if (version2 != 4)
                {
                    Material.Save(bw, u_3);
                }

                if (version2 == 1 || (u_6 != 1 && version2 == 0))
                {
                    UnknownLocE.Save(bw);
                }
            }
            if (u_6 == 1)
            {
                bw.Write(bytes);
            }
        }
    }
    public class MaterialStruct
    {
        private readonly byte[] Header = new byte[11];
        public float[] values = new float[16];
        public float scale;
        public byte isClothing;
        public byte unk;
        public Su Su;
        public MaterialStruct(BinaryReader br)
        {
            Header = br.ReadBytes(11);
            br.ReadBytes(69);
        }
        public void Save(BinaryWriter bw, short u_3)
        {
            bw.Write(Header);
            for (var i = 0; i < 16; i++)
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
            for (var i = 0; i < count; i++)
            {
                u_21.Add(br.ReadSingle());
                u_21.Add(br.ReadSingle());
            }
        }
        public void Save(BinaryWriter bw)
        {
            for (var i = 0; i < u_21.Count; i++)
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
        public Unknown_struct()
        {
            unk1To8 = new byte[8];
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
        public int textureindex;
        public float u;
        public float v;
        public Vertex(BinaryReader br)
        {
            Vector = new Vector3D(br);
            textureindex = br.ReadInt32();
            u = br.ReadSingle();
            v = br.ReadSingle();
        }
        public void Save(BinaryWriter bw)
        {
            Vector.Save(bw);
            bw.Write(textureindex);
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
        public Location()
        {
        }
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
            var i = br.ReadInt32();
            Brushes = new List<CDBrushe>(i);
            for (var y = 0; y < i; y++)
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
            for (var i = 0; i < Brushes.Count; i++)
            {
                Brushes[i].Save(bw);
            }
        }
    };
}
