using System.Collections.Generic;
using System.IO;

namespace MapFilesImporter.Struct
{
    public class WaterData
    {
        public List<Block> Usings = new List<Block>();
        public OffsetBlock OffsetBlock;

        public int Unk0;
        public int Unk1;

        public WaterData(BinaryReader ecbsd, OffsetBlock Block, int version)
        {
            OffsetBlock = Block;

            ecbsd.BaseStream.Position = Block.Offset;
            Unk0 = ecbsd.ReadInt32();
            Unk1 = ecbsd.ReadInt32();
        }

        public void Write(BinaryWriter ecbsd, int version)
        {
            OffsetBlock.Offset = (int) ecbsd.BaseStream.Position;
            ecbsd.Write(Unk0);
            ecbsd.Write(Unk1);
        }
    }
}
