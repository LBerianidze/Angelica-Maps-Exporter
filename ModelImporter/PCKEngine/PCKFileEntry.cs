using System;
using System.IO;
using System.Text;

namespace ModelImporter
{
    public class PCKFileEntry
    {
        public string Path;
        public long Offset { get; set; }
        public int Size { get; set; }
        public int CompressedSize { get; set; }

        public PCKFileEntry(byte[] bytes, int game, string pck, int length)
        {
            if (bytes.Length < length)
            {
                bytes = PCKZlib.Decompress(bytes, length);
            }
            BinaryReader br = new BinaryReader(new MemoryStream(bytes));
            Path = Encoding.GetEncoding(936).GetString(br.ReadBytes(260)).TrimEnd('\0').Replace("/", "\\").ToLower();
            if (game == 1)
                Path = pck + Path;
            if (game == 2)
            {
                if (Path.StartsWith("\\"))
                {
                    Path = Path.Remove(0, 1);
                }
            }
            if (game == 3)
            {
                br.ReadInt32();
                Offset = br.ReadInt64();
            }
            else
                Offset = br.ReadUInt32();
            Size = br.ReadInt32();
            CompressedSize = br.ReadInt32();
            br.Close();
        }
    }
}
