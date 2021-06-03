using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapFilesImporter
{
    public class T2bkFile
    {
        public float[] t1map = new float[513 * 513];
        public T2bkFile(string FilePath)
        {
            BinaryReader br = new BinaryReader(File.Open(FilePath, FileMode.Open));
            int Header = br.ReadInt32();
            int Version = br.ReadInt32();
            int Amount = br.ReadInt32();
            int unk1 = br.ReadInt32();
            int[] Offsets = new int[Amount];
            for (int i = 0; i < Amount; i++)
            {
                Offsets[i] = br.ReadInt32();
            }
            List<MapT2bk> MapTiles = new List<MapT2bk>();
            for (int i = 0; i < Amount; i++)
            {
                MapTiles.Add(new MapT2bk(br, Version));
            }
            br.Close();

            float height_scale = 800;
            t1map = new float[513 * 513];
            int x_offset;
            int y_offset;
            int pos;
            for (int i = 0; i < MapTiles.Count; i++)
            {
                y_offset = (i / 16 * 513 * (33 - 1));
                x_offset = i % 16 * (33 - 1);

                for (int j = 0; j < MapTiles[i].hmap.Length / 4; j++)
                {
                    pos = y_offset + x_offset + j / 33 * 513 + j % 33;
                    t1map[pos] = BitConverter.ToSingle(MapTiles[i].hmap, 4 * j) / height_scale;
                }
            }
        }
    }
    public class MapT2bk
    {
        public int Zone;
        public int Global_Pixel_position;
        public float unknown_01;
        public int pixel_size;
        public int Amount1;
        public int Amount2;
        public int Amount3;
        public int Amount4;
        public int unk1;
        public byte[] hmap;
        public byte[] tile_2;
        public byte[] tile_3;
        public byte[] tile_4;
        public MapT2bk(BinaryReader br, int Version)
        {
            Zone = br.ReadInt32();
            Global_Pixel_position = br.ReadInt32();
            unknown_01 = br.ReadInt32();
            pixel_size = br.ReadInt32();
            Amount1 = br.ReadInt32();
            Amount2 = br.ReadInt32();
            Amount3 = br.ReadInt32();
            Amount4 = br.ReadInt32();
            if (Version > 5)
            {
                unk1 = br.ReadInt32();
            }
            hmap = Decompress(br.ReadBytes(Amount1));
            tile_2 = Decompress(br.ReadBytes(Amount2));
            tile_3 = Decompress(br.ReadBytes(Amount3));
            tile_4 = Decompress(br.ReadBytes(Amount4));
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
}
