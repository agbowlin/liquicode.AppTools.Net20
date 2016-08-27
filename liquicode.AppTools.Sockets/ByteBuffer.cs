using System;
using System.Collections.Generic;
using System.Text;

namespace liquicode.AppTools
{
public class ByteBuffer
{


//---------------------------------------------------------------------

	public byte[] Bytes = null;

//---------------------------------------------------------------------
	public ByteBuffer()
	{
		Length = 0;
	}


//---------------------------------------------------------------------
	public ByteBuffer(int ByteCount_in)
	{
		Length = ByteCount_in;
	}


//---------------------------------------------------------------------
	public ByteBuffer(byte[] Bytes_in, int ByteCount_in )
	{
		if (Bytes_in != null) 
		{
			if (ByteCount_in < 0)
			{
				ByteCount_in = Bytes_in.Length;
				}
			Append(Bytes_in, ByteCount_in);
		}
	}


//---------------------------------------------------------------------
	public ByteBuffer(byte[] Bytes_in)
	{
		if (Bytes_in != null) 
		{
			Append(Bytes_in, Bytes_in.Length);
		}
	}


//---------------------------------------------------------------------
	public ByteBuffer(System.IO.Stream Stream_in, int ByteCount_in )
	{
		if (Stream_in != null)
		 {
			if (ByteCount_in < 0)
			{
				ByteCount_in = (int)Stream_in.Length;
				}
			Append(Stream_in, ByteCount_in);
		}
	}


//---------------------------------------------------------------------
	public ByteBuffer(System.IO.Stream Stream_in)
	{
		if (Stream_in != null)
		 {
			Append(Stream_in,(int) Stream_in.Length);
		}
	}


//---------------------------------------------------------------------
	public void Clear()
	{
		Length = 0;
	}


//---------------------------------------------------------------------
	public int Length {
		get {
			lock (this) {
				if ((Bytes == null)) {
					return 0;
				} else {
					return Bytes.Length;
				}
			}
		}
		set {
			lock (this) {
				if ((value <= 0)) {
					Bytes = null;
					Bytes = null;
				} else {
					if ((Bytes == null)) {
						Bytes = new byte[value];
					} else {
						Array.Resize(ref Bytes, value);
					}
				}
			}
		}
	}


//---------------------------------------------------------------------
	public bool Append(byte[] Bytes_in, int ByteCount_in)
	{
		lock (this) 
		{
			if (ByteCount_in < 0)
			{
				return false;
				}
			if (ByteCount_in == 0)
			{
				return true;
				}
			int cb = Length;
			Length = (cb + ByteCount_in);
			Array.Copy(Bytes_in, 0, Bytes, cb, ByteCount_in);
			return true;
		}
	}


//---------------------------------------------------------------------
	public bool Append(System.IO.Stream Stream_in, int ByteCount_in)
	{
		byte[] rgByte = new byte[512];
		int cbBlock = rgByte.Length;
		int cbPosition = (int)Stream_in.Position;
		int cbRemaining = (int)(Stream_in.Length - cbPosition);
		int cbRead = 0;
		while (cbRemaining > 0)
		 {
			if ((cbBlock > cbRemaining))
			{
				cbBlock = cbRemaining;
				}
			cbRead = Stream_in.Read(rgByte, 0, cbBlock);
			if (cbRead > 0)
			{
				Append(rgByte, cbRead);
				cbPosition += cbRead;
				cbRemaining -= cbRead;
			}
		}
		return true;
	}


//---------------------------------------------------------------------
	public bool Append(object Object_in)
	{
		lock (this) {
			try {
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				//if (Object_in.GetType().IsPrimitive) 
				//{
				//    System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms);
				//    bw.Write(Object_in);
				//}
				// else 
				//{
					System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = null;
					bf=new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
					bf.Serialize(ms, Object_in);
				//}
				return Append(ms.GetBuffer(), (int)ms.Position);
			} catch (Exception ex) {
				return false;
			}
		}
	}


//---------------------------------------------------------------------
	public bool Insert(int StartIndex_in, int ByteCount_in)
	{
		lock (this) {
			if ((StartIndex_in < 0))
				return false;
			if ((ByteCount_in < 0))
				return false;
			if ((ByteCount_in == 0))
				return true;
			if ((Bytes == null)) {
				Bytes = new byte[(StartIndex_in + ByteCount_in)];
				Length = (StartIndex_in + ByteCount_in);
			} else {
				int cb = (Length - StartIndex_in);
				byte[] ba = new byte[cb];
				Array.Copy(Bytes, StartIndex_in, ba, 0, cb);
				Length += ByteCount_in;
				Array.Copy(ba, 0, Bytes, (StartIndex_in + ByteCount_in), cb);
			}
			return true;
		}
	}


//---------------------------------------------------------------------
	public System.IO.MemoryStream MemoryStream()
	{
		if (Bytes == null){			return null;}
		System.IO.MemoryStream ms = new System.IO.MemoryStream(Bytes, true);
		ms.Seek(0, System.IO.SeekOrigin.Begin);
		return ms;
	}
	public System.IO.MemoryStream MemoryStream(int StartIndex_in)
	{
		if (Bytes == null){			return null;}
		System.IO.MemoryStream ms = new System.IO.MemoryStream(Bytes, true);
		ms.Seek(StartIndex_in, System.IO.SeekOrigin.Begin);
		return ms;
	}
	public System.IO.BinaryReader BinaryReader()
	{
		return new System.IO.BinaryReader(MemoryStream());
	}
	public System.IO.BinaryReader BinaryReader(int StartIndex_in)
	{
		return new System.IO.BinaryReader(MemoryStream(StartIndex_in));
	}
	public System.IO.BinaryWriter BinaryWriter()
	{
		return new System.IO.BinaryWriter(MemoryStream(0));
	}
	public System.IO.BinaryWriter BinaryWriter(int StartIndex_in )
	{
		return new System.IO.BinaryWriter(MemoryStream(StartIndex_in));
	}


//---------------------------------------------------------------------
	public byte[] Clone(int StartIndex_in , int ByteCount_in )
	{
		lock (this) {
			if ((StartIndex_in < 0))
				return null;
			if ((ByteCount_in < 0))
				ByteCount_in = Length;
			if ((ByteCount_in == 0))
				return null;
			if (((StartIndex_in + ByteCount_in) > Length))
				return null;
			byte[] ba = null;
			ba = new byte[ByteCount_in];
			Array.Copy(Bytes, StartIndex_in, ba, 0, ByteCount_in);
			return ba;
		}
	}
	public byte[] Clone()
	{
		return this.Clone(0, -1);
	}


//---------------------------------------------------------------------
	public bool Remove(int StartIndex_in, int ByteCount_in)
	{
		lock (this) {
			if ((StartIndex_in < 0))
				return false;
			if ((ByteCount_in < 0))
				return false;
			if ((ByteCount_in == 0))
				return true;
			if (((StartIndex_in + ByteCount_in) > Length))
				return false;
			if (((StartIndex_in + ByteCount_in) < Length)) {
				int cb = (StartIndex_in + ByteCount_in);
				Array.Copy(Bytes, cb, Bytes, StartIndex_in, (Length - cb));
			}
			Length = (Length - ByteCount_in);
			return true;
		}
	}


//---------------------------------------------------------------------
	public int Find(byte[] Bytes_in, int StartIndex_in)
	{
		if ((StartIndex_in < 0))
			return -1;
		if (((StartIndex_in + Bytes_in.Length) > Length))
			return -1;
		int ndxNext = StartIndex_in;
		int ndxFind = Array.IndexOf(Bytes, Bytes_in[0], ndxNext);
		while ((ndxFind >= 0) & ((ndxFind + Bytes_in.Length) <= Bytes.Length)) {
			int ndx = 0;
			for (ndx = 1; ndx <= (Bytes_in.Length - 1); ndx++) {
				if ((Bytes[ndxFind + ndx] != Bytes_in[ndx]))
					goto FindNext;
			}
			return ndxFind;
			FindNext:
			ndxNext = ndxFind + 1;
			ndxFind = Array.IndexOf(Bytes, Bytes_in[0], ndxNext);
		}
		return -1;
	}


}


}
