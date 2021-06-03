using System;
using System.IO;

namespace ModelImporter
{
    public class PCKStream : IDisposable
    {
        protected FileStream pck = null;
        protected FileStream pkx = null;
        private readonly string path = "";
        public long Position = 0;
        public PCKKey key = new PCKKey();
        private const uint PCK_MAX_SIZE = 2147483392;
        private const int BUFFER_SIZE = 33554432;

        public PCKStream(string path, Game game)
        {
            this.path = path;
            if (game == Game.Eso)
            {
                this.key.Key_1 = this.key.ESOKEY_1;
                this.key.Key_2 = this.key.ESOKEY_2;
            }
            else if (game == Game.FW)
            {
                this.key.Key_1 = this.key.FWKEY_1;
                this.key.Key_2 = this.key.FWKEY_2;
            }
            else
            {
                this.key.Key_1 = this.key.PWKEY_1;
                this.key.Key_2 = this.key.PWKEY_2;
            }

            this.pck =  (new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite));
            if (File.Exists(path.Replace(".pck", ".pkx")) && Path.GetExtension(path) != ".cup")
            {
                this.pkx =  (new FileStream(path.Replace(".pck", ".pkx"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite));
            }
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    this.Position = offset;
                    break;
                case SeekOrigin.Current:
                    this.Position += offset;
                    break;
                case SeekOrigin.End:
                    this.Position = this.GetLenght() + offset;
                    break;
            }
            if (this.Position < this.pck.Length)
            {
                this.pck.Seek(this.Position, SeekOrigin.Begin);
            }
            else
            {
                this.pkx.Seek(this.Position - this.pck.Length, SeekOrigin.Begin);
            }
        }

        public long GetLenght()
        {
            return this.pkx != null ? this.pck.Length + this.pkx.Length : this.pck.Length;
        }

        public byte[] ReadBytes(int count)
        {
            var array = new byte[count];
            var BytesRead = 0;
            if (this.Position < this.pck.Length)
            {
                BytesRead = this.pck.Read(array, 0, count);
                if (BytesRead < count && this.pkx != null)
                {
                    this.pkx.Seek(0, SeekOrigin.Begin);
                    BytesRead += this.pkx.Read(array, BytesRead, count - BytesRead);
                }
            }
            else if (this.Position > this.pck.Length && this.pkx != null)
            {
                BytesRead = this.pkx.Read(array, 0, count);
            }
            this.Position += count;
            return array;
        }

        public void WriteBytes(byte[] array)
        {
            if (this.Position + array.Length < PCK_MAX_SIZE)
            {
                this.pck.Write(array, 0, array.Length);
            }
            else if (this.Position + array.Length > PCK_MAX_SIZE)
            {
                if (this.pkx == null)
                {
                    this.pkx = new FileStream(this.path.Replace(".pck", ".pkx"), FileMode.Create, FileAccess.ReadWrite);
                }
                if (this.Position > PCK_MAX_SIZE)
                {
                    this.pkx.Write(array, 0, array.Length);
                }
                else
                {
                    if (this.pkx == null)
                    {
                        this.pkx = new FileStream(this.path.Replace(".pck", ".pkx"), FileMode.Create, FileAccess.ReadWrite);
                    }
                    this.pck.Write(array, 0, (int)(PCK_MAX_SIZE - this.Position));
                    this.pkx.Write(array, (int)(PCK_MAX_SIZE - this.Position), array.Length - (int)(PCK_MAX_SIZE - this.Position));
                }
            }
            this.Position += array.Length;
        }

        public uint ReadUInt32()
        {
            return BitConverter.ToUInt32(this.ReadBytes(4), 0);
        }

        public int ReadInt32()
        {
            return BitConverter.ToInt32(this.ReadBytes(4), 0);
        }

        public void WriteUInt32(uint value)
        {
            this.WriteBytes(BitConverter.GetBytes(value));
        }

        public void WriteInt32(int value)
        {
            this.WriteBytes(BitConverter.GetBytes(value));
        }

        public void WriteInt16(short value)
        {
            this.WriteBytes(BitConverter.GetBytes(value));
        }

        public void Dispose()
        {
            this.pck?.Close();
            this.pkx?.Close();
        }
    }
}
