using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ModelImporter
{
    public class MaterialSki
    {
        public byte TrailZero;
        public float[] MaterialParamsA;
        public float[] MaterialParamsB;
        public float[] MaterialParamsC;
        public float[] MaterialParamsD;
        public float Scale;
        public byte Clothing;
        public static MaterialSki Read(BinaryReader r)
        {
            var f = r.ReadBytes(10);
            MaterialSki skiMaterial = new MaterialSki();
            skiMaterial.TrailZero = r.ReadByte();
            skiMaterial.MaterialParamsA = new float[4];
            skiMaterial.MaterialParamsB = new float[4];
            skiMaterial.MaterialParamsC = new float[4];
            skiMaterial.MaterialParamsD = new float[4];
            for (int i = 0; i < 4; i++)
            {
                skiMaterial.MaterialParamsA[i] = r.ReadSingle();
            }
            for (int j = 0; j < 4; j++)
            {
                skiMaterial.MaterialParamsB[j] = r.ReadSingle();
            }
            for (int k = 0; k < 4; k++)
            {
                skiMaterial.MaterialParamsC[k] = r.ReadSingle();
            }
            for (int l = 0; l < 4; l++)
            {
                skiMaterial.MaterialParamsD[l] = r.ReadSingle();
            }
            skiMaterial.Scale = r.ReadSingle();
            skiMaterial.Clothing = r.ReadByte();
            return skiMaterial;
        }

    }
}
