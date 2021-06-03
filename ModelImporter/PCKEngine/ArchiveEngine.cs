using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace ModelImporter
{
    public delegate void CloseAfterFinish();

    public class ArchiveEngine
    {
        public PCKStream PckFile;
        public List<PCKFileEntry> Files;
        public int GetFilesCount(PCKStream stream)
        {
            stream.Seek(-8, SeekOrigin.End);
            return stream.ReadInt32();
        }
        public PCKFileEntry[] ReadFileTable(PCKStream stream)
        {
            stream.Seek(-8, SeekOrigin.End);
            int FilesCount = stream.ReadInt32();
            int endSeek = -272;
            int itemLength = 276;
            if ((int)game == 3)
            {
                endSeek = -280;
                itemLength = 288;
            }
            stream.Seek(endSeek, SeekOrigin.End);
            var FileTableOffset = (long)(ulong)(uint)(stream.ReadUInt32() ^ (ulong)stream.key.Key_1);
            PCKFileEntry[] entrys = new PCKFileEntry[FilesCount];
            stream.Seek(FileTableOffset, SeekOrigin.Begin);
            int currentGame = (int)game;
            for (int i = 0; i < entrys.Length; ++i)
            {
                int EntrySize = stream.ReadInt32() ^ stream.key.Key_1;
                int d = stream.ReadInt32()^stream.key.Key_1;
                entrys[i] = new PCKFileEntry(stream.ReadBytes(EntrySize), currentGame, pck, itemLength);
            }
            return entrys;
        }
        public Game game;
        string pck;
        public void Load(string path)
        {
            for (int i = 0; i < 7; i++)
            {
                try
                {
                    game = (Game)i;
                    PckFile = new PCKStream(path, game);
                    pck = path.Split('\\').Last().Split('.').First() + '\\';
                    Files = ReadFileTable(PckFile).ToList();
                    return;
                }
                catch(Exception)
                {

                }
                finally
                {
                    if (Files == null)
                    {
                        PckFile.Dispose();
                        PckFile = null;
                        Files = null;
                    }
                    GC.Collect();
                }
            }
        }
        public byte[] ReadFile(PCKStream stream, PCKFileEntry file)
        {
            if (file != null)
            {
                stream.Seek(file.Offset, SeekOrigin.Begin);
                byte[] bytes = stream.ReadBytes(file.CompressedSize);
                return file.CompressedSize < file.Size ? PCKZlib.Decompress(bytes, file.Size) : bytes;
            }
            return new byte[0];
        }
    }
    public enum Game
    {
        PW,
        FW,
        Eso,
        PW_China,
        JD,
        Loma,
        HoTK
    }
}
