

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace liquicode.AppTools
{


	[Serializable]
	public class BlockStream : IDisposable
	{


		//-------------------------------------------------
		[Serializable]
		private class StreamHeader
		{
			public const string HeaderTag = "aops";
			public long DataOffset = 0L;
			public long DataSize = 0L;
		}


		//-------------------------------------------------
		[Serializable]
		private class StreamControlBlock
		{

			public Guid ID = Guid.Empty;
			public long Offset = 0L;
			public int Length = 0;
			public Hashtable Tags = new Hashtable();

			//-------------------------------------------------
			public StreamControlBlock( Guid ID_in, long Offset_in, int Length_in, Hashtable Tags_in )
			{
				this.ID = ID_in;
				this.Offset = Offset_in;
				this.Length = Length_in;
				this.Tags = BlockStream.CloneObject<Hashtable>( Tags_in );
				return;
			}
			public StreamControlBlock( Guid ID_in )
			{
				this.ID = ID_in;
				return;
			}

		}


		//--------------------------------------------------------------------
		public static T CloneObject<T>( T obj )
		{
			using( MemoryStream buffer = new MemoryStream() )
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize( buffer, obj );
				buffer.Position = 0;
				T temp = (T)formatter.Deserialize( buffer );
				return temp;
			}
		}


		//-------------------------------------------------
		[Serializable]
		private class StreamControlBlockList : List<StreamControlBlock> { }


		//==========================================================================
		//===   Private Variables
		//==========================================================================

		//-------------------------------------------------
		private Stream _Stream = null;
		private StreamHeader _Header = null;
		private ArrayList _Blocks = null;
		private bool _BlocksDirty = false;
		private EncodeBytes _DelegateEncodeBytes = null;
		private DecodeBytes _DelegateDecodeBytes = null;


		//==========================================================================
		//===   Public Delegates
		//==========================================================================

		//-------------------------------------------------
		public delegate bool EncodeBytes( Hashtable Tags_in, byte[] Bytes_in, ref byte[] Bytes_out );
		public delegate bool DecodeBytes( Hashtable Tags_in, byte[] Bytes_in, ref byte[] Bytes_out );


		//==========================================================================
		//===   Constructors and Destructors
		//==========================================================================

		//-------------------------------------------------
		public BlockStream()
		{
			return;
		}
		public BlockStream( EncodeBytes EncodeBytes_in, DecodeBytes DecodeBytes_in )
		{
			this._DelegateEncodeBytes = EncodeBytes_in;
			this._DelegateDecodeBytes = DecodeBytes_in;
			return;
		}


		//-------------------------------------------------
		void IDisposable.Dispose()
		{
			this.Close();
			this._DelegateEncodeBytes = null;
			this._DelegateDecodeBytes = null;
			return;
		}


		//==========================================================================
		//===   Stream Control
		//==========================================================================

		//-------------------------------------------------
		private bool _EncodeBytes( Hashtable Tags_in, byte[] Bytes_in, ref byte[] Bytes_out )
		{
			if( (this._DelegateEncodeBytes != null) )
				return this._DelegateEncodeBytes( Tags_in, Bytes_in, ref Bytes_out );
			Array.Resize<byte>( ref Bytes_out, Bytes_in.Length );
			Bytes_in.CopyTo( Bytes_out, 0 );
			return true;
		}
		private bool _DecodeBytes( Hashtable Tags_in, byte[] Bytes_in, ref byte[] Bytes_out )
		{
			if( (this._DelegateDecodeBytes != null) )
				return this._DelegateDecodeBytes( Tags_in, Bytes_in, ref Bytes_out );
			Array.Resize<byte>( ref Bytes_out, Bytes_in.Length );
			Bytes_in.CopyTo( Bytes_out, 0 );
			return true;
		}


		//-------------------------------------------------
		private bool WriteFormatControl( Stream Stream_in )
		{
			if( (Stream_in == null) )
				return false;
			if( !Stream_in.CanWrite )
				return false;
			if( (this._Blocks == null) )
				return false;
			MemoryStream ms = new MemoryStream();
			BinaryFormatter bf = new BinaryFormatter();
			try
			{
				Stream_in.Seek( 0, SeekOrigin.Begin );
				// Write Header
				ms.SetLength( 0 );
				this._Header.DataOffset = 0;
				this._Header.DataSize = 0;
				bf.Serialize( ms, _Header );
				// Update Header
				this._Header.DataOffset = ms.Position;
				this._Header.DataSize = this.DataSize();
				ms.SetLength( 0 );
				bf.Serialize( ms, this._Header );
				Stream_in.Write( ms.ToArray(), 0, Convert.ToInt32( ms.Length ) );
				// Write Trailer
				Stream_in.Seek( (this._Header.DataOffset + this._Header.DataSize), SeekOrigin.Begin );
				ms.SetLength( 0 );
				bf.Serialize( ms, this._Blocks );
				byte[] inbytes = ms.ToArray();
				byte[] outbytes = { };
				if( !_EncodeBytes( null, inbytes, ref outbytes ) )
				{
					return false;
				}
				Stream_in.Write( outbytes, 0, outbytes.Length );
				Stream_in.SetLength( Stream_in.Position );
			}
			catch( Exception ex )
			{
				throw ex;
			}
			return true;
		}


		//-------------------------------------------------
		private bool ReadFormatControl( Stream Stream_in )
		{
			if( (Stream_in == null) )
				return false;
			if( (this._Blocks == null) )
				return false;
			BinaryFormatter bf = new BinaryFormatter();
			try
			{
				Stream_in.Seek( 0, SeekOrigin.Begin );
				this._Header = (StreamHeader)bf.Deserialize( Stream_in );
				Stream_in.Seek( (this._Header.DataOffset + this._Header.DataSize), SeekOrigin.Begin );
				byte[] inbytes = new byte[ Convert.ToInt32( (Stream_in.Length - Stream_in.Position) - 1 ) + 1 ];
				Stream_in.Read( inbytes, 0, inbytes.Length );
				byte[] outbytes = { };
				if( !this._DecodeBytes( null, inbytes, ref outbytes ) )
				{
					return false;
				}
				MemoryStream ms = new MemoryStream( outbytes );
				this._Blocks = (ArrayList)bf.Deserialize( ms );
			}
			catch( Exception ex )
			{
				throw ex;
			}
			return true;
		}


		//-------------------------------------------------
		public bool CreateMemoryStream()
		{
			this.Close();
			this._Header = new StreamHeader();
			this._Blocks = new ArrayList();
			MemoryStream STM = new MemoryStream();
			if( !this.WriteFormatControl( STM ) )
				return false;
			if( !this.OpenStream( STM ) )
				return false;
			return true;
		}


		//-------------------------------------------------
		public bool CreateInStream( Stream Stream_in )
		{
			this.Close();
			this._Header = new StreamHeader();
			this._Blocks = new ArrayList();
			if( !Stream_in.CanSeek )
				return false;
			if( !Stream_in.CanRead )
				return false;
			if( !this.WriteFormatControl( Stream_in ) )
				return false;
			if( !this.OpenStream( Stream_in ) )
				return false;
			return true;
		}


		//-------------------------------------------------
		public bool OpenStream( Stream Stream_in )
		{
			this.Close();
			if( !Stream_in.CanSeek )
				return false;
			if( !Stream_in.CanRead )
				return false;
			this._Header = new StreamHeader();
			this._Blocks = new ArrayList();
			if( !this.ReadFormatControl( Stream_in ) )
			{
				this.Close();
				return false;
			}
			this._Stream = Stream_in;
			return true;
		}


		//-------------------------------------------------
		public bool CreateFile( string Filename_in )
		{
			return this.CreateInStream( new FileStream( Filename_in, FileMode.Create ) );
		}


		//-------------------------------------------------
		public bool OpenFile( string Filename_in )
		{
			return this.OpenStream( new FileStream( Filename_in, FileMode.Open ) );
		}


		//'-------------------------------------------------
		//Public Function CopyTo(ByVal BlockStream_inout As BlockStream) As Boolean
		//    BlockStream_inout()
		//    If Not WriteFormatControl(Stream_in) Then Return False
		//    Dim BLK As StreamControlBlock
		//    For Each BLK In Blocks_
		//        If (BLK.Length > 0) Then
		//            Dim bytes(BLK.Length - 1) As Byte
		//            Stream_.Seek(Header_.DataOffset + BLK.Offset, SeekOrigin.Begin)
		//            Stream_.Read(bytes, 0, BLK.Length)
		//            Stream_in.Seek(Header_.DataOffset + BLK.Offset, SeekOrigin.Begin)
		//            Stream_in.Write(bytes, 0, BLK.Length)
		//        End If
		//    Next
		//    Return True
		//End Function


		//-------------------------------------------------
		public bool Compact()
		{
			if( (this._Blocks == null) )
				return false;
			int ndx = 0;
			StreamControlBlock BLK;
			long off = this._Header.DataOffset;
			byte[] ba = { };
			for( ndx = 1; ndx <= this._Blocks.Count; ndx++ )
			{
				BLK = (StreamControlBlock)this._Blocks[ ndx - 1 ];
				if( (BLK.ID.CompareTo( Guid.Empty ) == 0) )
				{
					BLK.Length = 0;
				}
				else if( (BLK.Offset != off) )
				{
					try
					{
						this._Stream.Seek( this._Header.DataOffset + BLK.Offset, SeekOrigin.Begin );
						Array.Resize<byte>( ref ba, BLK.Length );
						this._Stream.Read( ba, 0, BLK.Length );
						this._Stream.Seek( this._Header.DataOffset + off, SeekOrigin.Begin );
						this._Stream.Write( ba, 0, BLK.Length );
						BLK.Offset = off;
						this._BlocksDirty = true;
					}
					catch( Exception ex )
					{
						throw ex;
					}
				}
				off += BLK.Length;
			}
			for( ndx = this._Blocks.Count; ndx >= 1; ndx += -1 )
			{
				BLK = (StreamControlBlock)_Blocks[ ndx - 1 ];
				if( (BLK.ID.CompareTo( Guid.Empty ) == 0) )
				{
					this._Blocks.RemoveAt( ndx - 1 );
					this._BlocksDirty = true;
				}
			}
			return true;
		}


		//-------------------------------------------------
		public Stream Close( bool Compact_in, bool CloseStream_in )
		{
			if( (this._Stream != null) )
			{
				if( Compact_in )
				{
					if( !this.Compact() )
						return null;
				}
				if( this._BlocksDirty )
				{
					if( !this.WriteFormatControl( this._Stream ) )
						return null;
				}
				if( CloseStream_in )
				{
					if( (this._Stream != null) )
						this._Stream.Close();
				}
			}
			this._Header = null;
			this._Blocks = null;
			this._BlocksDirty = false;
			Stream stream = this._Stream;
			this._Stream = null;
			return stream;
		}
		public Stream Close()
		{
			return this.Close( false, true );
		}


		//-------------------------------------------------
		public bool IsOpen
		{
			get
			{
				if( (this._Stream == null) )
					return false;
				if( (this._Blocks == null) )
					return false;
				return true;
			}
		}


		//-------------------------------------------------
		public bool IsDirty
		{
			get
			{
				if( (this._Stream == null) )
					return false;
				if( (this._Blocks == null) )
					return false;
				return this._BlocksDirty;
			}
		}


		//-------------------------------------------------
		public bool CanWrite
		{
			get
			{
				if( (this._Stream == null) )
					return false;
				if( (this._Blocks == null) )
					return false;
				return this._Stream.CanWrite;
			}
		}


		//==========================================================================
		//===   Block Info
		//==========================================================================

		//-------------------------------------------------
		public long DataSize()
		{
			if( (this._Blocks.Count == 0) )
				return 0;
			StreamControlBlock BLK = (StreamControlBlock)this._Blocks[ this._Blocks.Count - 1 ];
			return (BLK.Offset + BLK.Length);
		}


		////-------------------------------------------------
		//public List<Guid> BlockIDs
		//{
		//    get
		//    {
		//        if( (this._Stream == null) )
		//            return null;
		//        if( (this._Blocks == null) )
		//            return null;
		//        List<Guid> list = new List<Guid>();
		//        int ndx;
		//        StreamControlBlock BLK;
		//        for( ndx = 1; ndx <= this._Blocks.Count; ndx++ )
		//        {
		//            BLK = (StreamControlBlock)this._Blocks[ ndx - 1 ];
		//            list.Add( BLK.ID );
		//        }
		//        return list;
		//    }
		//}


		//-------------------------------------------------
		public List<Guid> GetBlockIDs()
		{
			if( (this._Stream == null) )
				return null;
			if( (this._Blocks == null) )
				return null;
			List<Guid> list = new List<Guid>();
			int ndx;
			StreamControlBlock BLK;
			for( ndx = 1; ndx <= this._Blocks.Count; ndx++ )
			{
				BLK = (StreamControlBlock)this._Blocks[ ndx - 1 ];
				list.Add( BLK.ID );
			}
			return list;
		}


		////-------------------------------------------------
		//public Hashtable BlockTags
		//{
		//    get
		//    {
		//        int ndx;
		//        StreamControlBlock BLK;
		//        if( !Internal_FindBlock( BlockID_in, ndx ) ) return null;
		//        BLK = (StreamControlBlock)Blocks_[ ndx ];
		//        return BLK.Tags;
		//    }
		//    set
		//    {
		//        int ndx;
		//        StreamControlBlock BLK;
		//        if( !Internal_FindBlock( BlockID_in, ndx ) ) return;
		//        BLK = (StreamControlBlock)Blocks_[ ndx ];
		//        BLK.Tags = (Hashtable)ObjectCloner.Clone( value );
		//        BlocksDirty_ = true;
		//    }
		//}

		//-------------------------------------------------
		public Hashtable GetBlockTags( Guid BlockID_in )
		{
			int ndx = 0;
			StreamControlBlock BLK;
			if( !this.Internal_FindBlock( BlockID_in, ref ndx ) ) { return null; }
			BLK = (StreamControlBlock)this._Blocks[ ndx ];
			return BLK.Tags;
		}

		//-------------------------------------------------
		public void SetBlockTags( Guid BlockID_in, Hashtable Tags_in )
		{
			int ndx = 0;
			StreamControlBlock BLK;
			if( !Internal_FindBlock( BlockID_in, ref ndx ) ) { return; }
			BLK = (StreamControlBlock)this._Blocks[ ndx ];
			BLK.Tags = BlockStream.CloneObject<Hashtable>( Tags_in );
			this._BlocksDirty = true;
			return;
		}


		//==========================================================================
		//===   Block Maintenance
		//==========================================================================

		//-------------------------------------------------
		private bool Internal_FindBlock( Guid in_BlockID, ref int out_BlockIndex )
		{
			//- - - - - - - - - - - - - - - - - - - - - - - - -
			out_BlockIndex = -1;
			if( (this._Blocks == null) )
				return false;
			StreamControlBlock BLK;
			int ndx;
			for( ndx = 0; ndx <= (this._Blocks.Count - 1); ndx++ )
			{
				BLK = (StreamControlBlock)this._Blocks[ ndx ];
				if( (BLK.ID.CompareTo( in_BlockID ) == 0) )
				{
					out_BlockIndex = ndx;
					return true;
				}
			}
			return false;
		}


		//-------------------------------------------------
		private bool Internal_AllocateBlock( Guid in_BlockID, int in_BlockLength, ref int out_BlockIndex )
		{
			//- - - - - - - - - - - - - - - - - - - - - - - - -
			out_BlockIndex = -1;
			if( (this._Blocks == null) )
				return false;
			StreamControlBlock BLK;
			StreamControlBlock BLK2;
			int ndx = 0;
			// Try to Reclaim an Existing Empty Block
			for( ndx = 0; ndx <= (this._Blocks.Count - 1); ndx++ )
			{
				BLK = (StreamControlBlock)this._Blocks[ ndx ];
				if( (BLK.ID.CompareTo( Guid.Empty ) == 0) )
				{
					if( (BLK.Length == in_BlockLength) )
					{
						// Exact Size Match Found, just Reuse this Block
						BLK.ID = in_BlockID;
						out_BlockIndex = ndx;
						this._BlocksDirty = true;
						return true;
					}
					else if( (BLK.Length > in_BlockLength) )
					{
						// Larger Block Found, Split it into a Used Block and a Free Block
						BLK2 = new StreamControlBlock( Guid.Empty );
						this._Blocks.Insert( ndx + 1, BLK2 );
						BLK2.Offset = (BLK.Offset + in_BlockLength);
						BLK2.Length = (BLK.Length - in_BlockLength);
						BLK.ID = in_BlockID;
						BLK.Length = in_BlockLength;
						out_BlockIndex = ndx;
						this._BlocksDirty = true;
						return true;
					}
				}
			}
			// Append a New Block
			BLK = new StreamControlBlock( in_BlockID );
			BLK.Offset = 0;
			BLK.Length = in_BlockLength;
			ndx = _Blocks.Add( BLK );
			if( (ndx > 0) )
			{
				BLK2 = (StreamControlBlock)_Blocks[ ndx - 1 ];
				BLK.Offset = (BLK2.Offset + BLK2.Length);
			}
			out_BlockIndex = ndx;
			_BlocksDirty = true;
			return true;
		}


		//-------------------------------------------------
		private bool Internal_FreeBlock( int in_BlockIndex )
		{
			//- - - - - - - - - - - - - - - - - - - - - - - - -
			if( (this._Blocks == null) )
				return false;

			StreamControlBlock BLK;
			// Release Block
			BLK = (StreamControlBlock)this._Blocks[ in_BlockIndex ];
			if( (BLK.ID.CompareTo( Guid.Empty ) != 0) )
			{
				BLK.ID = Guid.Empty;
				BLK.Tags.Clear();
				this._BlocksDirty = true;
			}
			// Adjust End of Data
			while( (_Blocks.Count > 0) )
			{
				BLK = (StreamControlBlock)this._Blocks[ _Blocks.Count - 1 ];
				if( (BLK.ID.CompareTo( Guid.Empty ) != 0) ) { break; }
				this._Blocks.RemoveAt( this._Blocks.Count - 1 );
				this._BlocksDirty = true;
			}
			// Return, OK
			return true;
		}


		//-------------------------------------------------
		public bool CreateBlock( int InitialSize_in, ref Guid BlockID_out )
		{
			if( (this._Stream == null) )
				return false;
			if( !this._Stream.CanWrite )
				return false;
			if( (this._Blocks == null) )
				return false;
			BlockID_out = Guid.NewGuid();
			int ndx = 0;
			if( !this.Internal_AllocateBlock( BlockID_out, InitialSize_in, ref ndx ) )
				return false;
			return true;
		}


		//-------------------------------------------------
		public bool DestroyBlock( Guid BlockID_in )
		{
			if( (this._Stream == null) )
				return false;
			if( !this._Stream.CanWrite )
				return false;
			if( (this._Blocks == null) )
				return false;
			// Release Block
			int ndx = 0;
			if( !this.Internal_FindBlock( BlockID_in, ref  ndx ) )
				return false;
			if( !this.Internal_FreeBlock( ndx ) )
				return false;
			return true;
		}


		//-------------------------------------------------
		public bool WriteBlock( Guid BlockID_in, Hashtable BlockTags_in, byte[] Bytes_in )
		{
			if( (this._Stream == null) )
				return false;
			if( !this._Stream.CanWrite )
				return false;
			if( (this._Blocks == null) )
				return false;
			int ndx = 0;
			if( this.Internal_FindBlock( BlockID_in, ref  ndx ) )
			{
				// Free Existing Block
				if( !this.Internal_FreeBlock( ndx ) )
					return false;
			}
			// Encode Block
			byte[] outbytes = { };
			if( !this._EncodeBytes( BlockTags_in, Bytes_in, ref outbytes ) )
			{
				return false;
			}
			// Allocate a New Block
			if( !this.Internal_AllocateBlock( BlockID_in, outbytes.Length, ref  ndx ) )
				return false;
			StreamControlBlock BLK = (StreamControlBlock)this._Blocks[ ndx ];
			try
			{
				this._Stream.Seek( this._Header.DataOffset + BLK.Offset, SeekOrigin.Begin );
				this._Stream.Write( outbytes, 0, outbytes.Length );
				BLK.Tags = BlockStream.CloneObject<Hashtable>( BlockTags_in );
				this._BlocksDirty = true;
			}
			catch( Exception ex )
			{
				throw ex;
			}
			return true;
		}


		//-------------------------------------------------
		public bool WriteBlock( Guid BlockID_in, Hashtable BlockTags_in, Stream Stream_in )
		{
			byte[] bytes = new byte[ Convert.ToInt32( Stream_in.Length - 1 ) + 1 ];
			Stream_in.Seek( 0, SeekOrigin.Begin );
			Stream_in.Read( bytes, 0, bytes.Length );
			return this.WriteBlock( BlockID_in, BlockTags_in, bytes );
		}


		//-------------------------------------------------
		public bool ReadBlock( Guid BlockID_in, ref Hashtable BlockTags_out, ref byte[] Bytes_out )
		{
			if( (this._Stream == null) )
				return false;
			if( (this._Blocks == null) )
				return false;
			int ndx = 0;
			if( !this.Internal_FindBlock( BlockID_in, ref  ndx ) )
				return false;
			StreamControlBlock BLK = (StreamControlBlock)this._Blocks[ ndx ];
			BlockTags_out = BlockStream.CloneObject<Hashtable>( BLK.Tags );
			try
			{
				this._Stream.Seek( this._Header.DataOffset + BLK.Offset, SeekOrigin.Begin );
				byte[] inbytes = new byte[ BLK.Length ];
				this._Stream.Read( inbytes, 0, inbytes.Length );
				if( !this._DecodeBytes( BlockTags_out, inbytes, ref  Bytes_out ) )
				{
					return false;
				}
			}
			catch( Exception ex )
			{
				throw ex;
			}
			return true;
		}


		//-------------------------------------------------
		public bool ReadBlock( Guid BlockID_in, ref Hashtable BlockTags_out, Stream Stream_inout )
		{
			byte[] bytes = { };
			if( !this.ReadBlock( BlockID_in, ref BlockTags_out, ref  bytes ) )
				return false;
			Stream_inout.SetLength( bytes.Length );
			Stream_inout.Write( bytes, 0, bytes.Length );
			return true;
		}


	}


	//==========================================================================


}
