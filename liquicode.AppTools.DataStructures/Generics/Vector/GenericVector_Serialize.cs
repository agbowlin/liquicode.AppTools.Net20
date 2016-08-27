

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		public partial class GenericVector<T> : List<T>
		{


			//---------------------------------------------------------------------
			public void CopyFromStream( Stream Stream_in )
			{
				this.Deserialize( Stream_in );
				return;
			}


			//---------------------------------------------------------------------
			public Stream ToStream()
			{
				MemoryStream buffer = new MemoryStream();
				this.Serialize( buffer );
				buffer.Position = 0L;
				return buffer;
			}


			//---------------------------------------------------------------------
			public byte[] ToByteArray()
			{
				byte[] bytes = null;
				using( MemoryStream buffer = new MemoryStream() )
				{
					this.Serialize( buffer );
					bytes = buffer.ToArray();
					buffer.Close();
				}
				return bytes;
			}


			//---------------------------------------------------------------------
			public void CopyFromByteArray( byte[] Bytes_in )
			{
				using( MemoryStream buffer = new MemoryStream( Bytes_in ) )
				{
					BinaryFormatter formatter = new BinaryFormatter();
					GenericVector<T> vector = (GenericVector<T>)formatter.Deserialize( buffer );
					this.CopyFromArray( vector.ToArray() );
					buffer.Close();
				}
				return;
			}


			//---------------------------------------------------------------------
			public void Serialize( Stream Stream_in )
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize( Stream_in, this );
				return;
			}


			//---------------------------------------------------------------------
			public void Deserialize( Stream Stream_in )
			{
				BinaryFormatter formatter = new BinaryFormatter();
				GenericVector<T> vector = (GenericVector<T>)formatter.Deserialize( Stream_in );
				this.CopyFromArray( vector.ToArray() );
				return;
			}


		}


	}
}
