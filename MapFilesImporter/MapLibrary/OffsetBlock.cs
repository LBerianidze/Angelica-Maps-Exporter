using System.IO;

namespace MapFilesImporter.Struct
{
    public class OffsetBlock
    {
        public int Id;
        public int Offset;

        public OffsetBlock(int id, int offset)
        {
            Id = id;
            Offset = offset;
        }

        public OffsetBlock(BinaryReader br, int version)
        {
            Id = br.ReadInt32();
            Offset = br.ReadInt32();
        }

        public void Save(BinaryWriter bw, int version)
        {
            bw.Write(Id);
            bw.Write(Offset);
        }
    }
}
