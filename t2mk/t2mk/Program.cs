using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace t2mk
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryReader br = new BinaryReader(File.Open(@"D:\Pw Servers\LafnianPW\element\maps\world\world_1.t2mk", FileMode.Open));
            T2mk file = new T2mk(br);
            List<byte> bb = new List<byte>();
            foreach (var item in file.T2mkBlock_2S)
            {
                foreach (var it in item.T2mkBlock_2_SubBlockS)
                {
                    bb.AddRange(it.bytedata);
                }
            }
        }
    }
    public class T2mk
    {
        public int Header;
        public int Version;
        public int AmountOne;
        public int AmountTwo;
        public int unk2;
        public int[] Offsets;
        public List<T2mkBlock_1> T2mkBlock_1S;
        public List<T2mkBlock_2> T2mkBlock_2S;
        public T2mk(BinaryReader br)
        {
            Header = br.ReadInt32();
            Version = br.ReadInt32();
            AmountOne = br.ReadInt32();
            AmountTwo = br.ReadInt32();
            unk2 = br.ReadInt32();
            Offsets = new int[AmountOne];
            for (int i = 0; i < AmountOne; i++)
            {
                Offsets[i] = br.ReadInt32();
            }
            T2mkBlock_1S = new List<T2mkBlock_1>();
            for (int i = 0; i < AmountTwo; i++)
            {
                T2mkBlock_1S.Add(new T2mkBlock_1(br));
            }
            T2mkBlock_2S = new List<T2mkBlock_2>();
            for (int i = 0; i < AmountOne; i++)
            {
                T2mkBlock_2S.Add(new T2mkBlock_2(br));
            }
        }
    }
    public class T2mkBlock_2
    {
        public int Amount;
        public List<T2mkBlock_2_SubBlock> T2mkBlock_2_SubBlockS;
        public T2mkBlock_2(BinaryReader br)
        {
            Amount = br.ReadInt32();
            T2mkBlock_2_SubBlockS = new List<T2mkBlock_2_SubBlock>();
            for (int i = 0; i < Amount; i++)
            {
                T2mkBlock_2_SubBlockS.Add(new T2mkBlock_2_SubBlock(br, Amount));
            }
            for (int i = 0; i < Amount; i++)
            {
                var k = new Random().Next(0, 9999999);
                BinaryWriter bwr = new BinaryWriter(File.Create($@"C:\Users\Luka\Desktop\t2smtBlocks\{i}_{k}.unk"));
                T2mkBlock_2_SubBlockS[i].bytedata = new MppcUnpacker().Unpack((br.ReadBytes(T2mkBlock_2_SubBlockS[i].Length)));
                foreach (var item in T2mkBlock_2_SubBlockS[i].bytedata)
                {
                    bwr.Write(item);
                }
                bwr.Close();
            }
        }
        public byte[] Decompress(byte[] ba)
        {

            MemoryStream ms = new MemoryStream(ba);
            ms.ReadByte();
            ms.ReadByte();
            DeflateStream ds = new DeflateStream(ms, CompressionMode.Decompress, true);
            List<int> AL = new List<int>();
            int Value;
            while ((Value = ds.ReadByte()) != -1)
            {
                AL.Add(Value);
            }
            ds.Close();
            ms.Close();
            byte[] result = new byte[AL.Count];
            result = AL.Select(t => (byte)t).ToArray();
            return result;
        }
    }
    public class T2mkBlock_2_SubBlock
    {
        public int Id;
        public int byte_position;
        public int Length;
        public byte[] bytedata;
        public T2mkBlock_2_SubBlock(BinaryReader br, int Amount)
        {
            Id = br.ReadInt32();
            byte_position = br.ReadInt32();
            Length = br.ReadInt32();
        }
    }
    public class T2mkBlock_1
    {
        public int unknown_1;
        public int seperator;
        public int unknown_2;
        public int unknown_3;
        public float unknown_4;
        public float unknown_5;
        public int unknown_6;
        public T2mkBlock_1(BinaryReader br)
        {
            unknown_1 = br.ReadInt32();
            seperator = br.ReadInt32();
            unknown_2 = br.ReadInt32();
            unknown_3 = br.ReadInt32();
            unknown_4 = br.ReadSingle();
            unknown_5 = br.ReadSingle();
            unknown_6 = br.ReadInt32();
        }
    }
}
