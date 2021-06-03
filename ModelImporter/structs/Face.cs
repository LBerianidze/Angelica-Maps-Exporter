using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ModelImporter
{
	public class Face
	{
		public short[] VertIndexs
		{
			get;
			set;
		}

		public static Face Read(BinaryReader r)
		{
			Face face = new Face();
			face.VertIndexs = new short[3];
			for (int i = 0; i < 3; i++)
			{
				face.VertIndexs[i] = r.ReadInt16();
			}
			return face;
		}
	}
}
