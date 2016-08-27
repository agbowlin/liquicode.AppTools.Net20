

using System;
using System.Collections;
using System.Collections.Generic;


namespace liquicode.AppTools
{
	//=================================================================
	//
	//       CLASS: aops.sockets.SocketHandler
	//
	//=================================================================


	public abstract class SocketHandler
	{


		//---------------------------------------------------------------------
		public SocketManager Manager = null;
		public System.Net.Sockets.Socket Socket = null;
		public string SocketType = "";
		public string SocketID = System.Guid.NewGuid().ToString();
		internal ByteBuffer RecvBuffer = new ByteBuffer( 8192 );

		internal object RecvBuffer_Lock = new object();

		//---------------------------------------------------------------------
		internal virtual SocketHandler DaemonSpawn()
		{
			return null;
		}


		//---------------------------------------------------------------------
		internal virtual bool SocketConnected()
		{
			return true;
		}


		//---------------------------------------------------------------------
		internal abstract bool SocketReceive( int ByteCount_in );


	}


	//=================================================================
	//
	//       CLASS: aops.sockets.Client_SocketHandler
	//
	//=================================================================


	public class Client_SocketHandler : SocketHandler
	{


		//---------------------------------------------------------------------
		public event OnReceiveEventHandler OnReceive;
		public delegate void OnReceiveEventHandler( byte[] Bytes_in, int ByteCount_in );


		//---------------------------------------------------------------------
		internal override bool SocketReceive( int ByteCount_in )
		{
			if( OnReceive != null )
			{
				OnReceive( RecvBuffer.Bytes, ByteCount_in );
			}
			return true;
		}


	}


	//=================================================================
	//
	//       CLASS: aops.sockets.Echo_Server_SocketHandler
	//
	//=================================================================


	public class Echo_Server_SocketHandler : SocketHandler
	{


		//---------------------------------------------------------------------
		internal override SocketHandler DaemonSpawn()
		{
			return new Echo_Server_SocketHandler();
		}


		//---------------------------------------------------------------------
		internal override bool SocketReceive( int ByteCount_in )
		{
			return Manager.Send( SocketID, RecvBuffer.Bytes, ByteCount_in );
		}


	}


	//=================================================================
	//
	//       CLASS: aops.sockets.SMTP_Client_SocketHandler
	//
	//=================================================================


	public class SMTP_Client_SocketHandler : SocketHandler
	{


		//---------------------------------------------------------------------
		public int ServerTimeout = 15000;
		public string ReplyCode;
		public string ReplyText;
		private System.Collections.Specialized.StringCollection Lines_ = new System.Collections.Specialized.StringCollection();

		private string LastLine_ = "";

		//---------------------------------------------------------------------
		private bool WaitForReply()
		{
			long start = System.DateTime.UtcNow.Ticks;
			while( (((System.DateTime.UtcNow.Ticks - start) / TimeSpan.TicksPerMillisecond) < ServerTimeout) )
			{
				if( (Lines_.Count > 0) )
				{
					string s = null;
					s = Lines_[ 0 ];
					Lines_.RemoveAt( 0 );
					if( (!string.IsNullOrEmpty( ReplyText )) )
						ReplyText += "\n";
					ReplyText += s.Substring( 4 );
					if( (s.Substring( 3, 1 ) == " ") )
					{
						ReplyCode = s.Substring( 0, 3 );
						//TraceText("T", "SMTP_Client_SocketHandler::WaitForReply", "Server says : " & ReplyCode & " " & ReplyText)
						return true;
					}
				}
			}
			//TraceText("W", "SMTP_Client_SocketHandler::WaitForReply", "Server timed out.")
			return false;
		}


		//---------------------------------------------------------------------
		internal override bool SocketReceive( int ByteCount_in )
		{
			string s = null;
			s = LastLine_;
			LastLine_ = "";
			s += System.Text.Encoding.ASCII.GetString( RecvBuffer.Bytes, 0, ByteCount_in );
			int i = s.IndexOf( "\n" );
			while( (i >= 0) )
			{
				Lines_.Add( s.Substring( 0, i ) );
				//Audit.TraceMessage("D", "SMTP_Client_SocketHandler::SocketReceive", "Received : '" & Lines_.Item(Lines_.Count - 1) & "'")
				s = s.Remove( 0, i + 2 );
				i = s.IndexOf( "\n" );
			}
			LastLine_ = s;
			return true;
		}


		//---------------------------------------------------------------------
		internal override bool SocketConnected()
		{
			ReplyCode = "";
			ReplyText = "";
			if( !WaitForReply() )
			{
				return false;
			}
			if( (ReplyCode != "220") )
				return false;
			return true;
		}


		//---------------------------------------------------------------------
		public bool Say_NOOP()
		{
			ReplyCode = "";
			ReplyText = "";
			string s = "NOOP" + "\n";
			//TraceText("T", "SMTP_Client_SocketHandler::Say_NOOP", "Sending : " & s)
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes( s );
			if( !Manager.Send( SocketID, bytes, bytes.Length ) )
				return false;
			if( !WaitForReply() )
			{
				return false;
			}
			if( (ReplyCode != "250") )
				return false;
			return true;
		}


		//---------------------------------------------------------------------
		public bool Say_QUIT()
		{
			ReplyCode = "";
			ReplyText = "";
			string s = "QUIT" + "\n";
			//TraceText("T", "SMTP_Client_SocketHandler::Say_QUIT", "Sending : " & s)
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes( s );
			if( !Manager.Send( SocketID, bytes, bytes.Length ) )
				return false;
			if( !WaitForReply() )
			{
				return false;
			}
			if( (ReplyCode != "221") )
				return false;
			return true;
		}


		//---------------------------------------------------------------------
		public bool Say_RSET()
		{
			ReplyCode = "";
			ReplyText = "";
			string s = "RSET" + "\n";
			//TraceText("T", "SMTP_Client_SocketHandler::Say_RSET", "Sending : " & s)
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes( s );
			if( !Manager.Send( SocketID, bytes, bytes.Length ) )
				return false;
			if( !WaitForReply() )
			{
				return false;
			}
			if( (ReplyCode != "221") )
				return false;
			return true;
		}


		//---------------------------------------------------------------------
		public bool Say_HELO( string Domain_in )
		{
			ReplyCode = "";
			ReplyText = "";
			string s = "HELO " + Domain_in + "\n";
			//TraceText("T", "SMTP_Client_SocketHandler::Say_HELO", "Sending : " & s)
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes( s );
			if( !Manager.Send( SocketID, bytes, bytes.Length ) )
				return false;
			if( !WaitForReply() )
			{
				return false;
			}
			if( (ReplyCode != "250") )
				return false;
			return true;
		}


		//---------------------------------------------------------------------
		public bool Say_VRFY( string UserName_in )
		{
			ReplyCode = "";
			ReplyText = "";
			string s = "VRFY " + UserName_in + "\n";
			//TraceText("T", "SMTP_Client_SocketHandler::Say_VRFY", "Sending : " & s)
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes( s );
			if( !Manager.Send( SocketID, bytes, bytes.Length ) )
				return false;
			if( !WaitForReply() )
			{
				return false;
			}
			if( (ReplyCode != "250") )
				return false;
			return true;
		}


		//---------------------------------------------------------------------
		public bool Say_EXPN( string ListName_in )
		{
			ReplyCode = "";
			ReplyText = "";
			string s = "EXPN " + ListName_in + "\n";
			//TraceText("T", "SMTP_Client_SocketHandler::Say_EXPN", "Sending : " & s)
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes( s );
			if( !Manager.Send( SocketID, bytes, bytes.Length ) )
				return false;
			if( !WaitForReply() )
			{
				return false;
			}
			if( (ReplyCode != "250") )
				return false;
			return true;
		}


		//---------------------------------------------------------------------
		public bool Say_MAIL( string FromAddress_in )
		{
			ReplyCode = "";
			ReplyText = "";
			string s = "MAIL FROM:<" + FromAddress_in + ">" + "\n";
			//TraceText("T", "SMTP_Client_SocketHandler::Say_MAIL", "Sending : " & s)
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes( s );
			if( !Manager.Send( SocketID, bytes, bytes.Length ) )
				return false;
			if( !WaitForReply() )
			{
				return false;
			}
			if( (ReplyCode != "250") )
				return false;
			return true;
		}


		//---------------------------------------------------------------------
		public bool Say_RCPT( string ToAddress_in )
		{
			ReplyCode = "";
			ReplyText = "";
			string s = "RCPT TO:<" + ToAddress_in + ">" + "\n";
			//TraceText("T", "SMTP_Client_SocketHandler::Say_RCPT", "Sending : " & s)
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes( s );
			if( !Manager.Send( SocketID, bytes, bytes.Length ) )
				return false;
			if( !WaitForReply() )
			{
				return false;
			}
			if( (ReplyCode != "250") )
				return false;
			return true;
		}


		//---------------------------------------------------------------------
		public bool Say_DATA( string Data_in )
		{
			string s = null;
			byte[] bytes = null;
			// Issue Data Command
			ReplyCode = "";
			ReplyText = "";
			s = "DATA" + "\n";
			//TraceText("T", "SMTP_Client_SocketHandler::Say_RCPT", "Sending : " & s)
			bytes = System.Text.Encoding.ASCII.GetBytes( s );
			if( !Manager.Send( SocketID, bytes, bytes.Length ) )
				return false;
			if( !WaitForReply() )
			{
				return false;
			}
			if( (ReplyCode != "354") )
				return false;
			// Send Data
			ReplyCode = "";
			ReplyText = "";
			s = Data_in;
			if( s.StartsWith( "." ) ) { s = "." + s; }
			s = s.Replace( "\n" + ".", "\n" + ".." );
			s += "\n" + "." + "\n";
			//TraceText("T", "SMTP_Client_SocketHandler::Say_RCPT", "Sending : " & s)
			bytes = System.Text.Encoding.ASCII.GetBytes( s );
			if( !Manager.Send( SocketID, bytes, bytes.Length ) )
				return false;
			if( !WaitForReply() )
			{
				return false;
			}
			if( (ReplyCode != "250") )
				return false;
			// Return, OK
			return true;
		}


	}


	//=================================================================
	//
	//       CLASS: aops.sockets.Hashtable_Client_SocketHandler
	//
	//=================================================================


	public class Hashtable_Client_SocketHandler : SocketHandler
	{


		//---------------------------------------------------------------------
		public int ServerTimeout = 15000;

		private ByteBuffer ReplyBuffer_ = new ByteBuffer();

		//---------------------------------------------------------------------
		public bool SendHashtable( Hashtable HashTable_in )
		{
			ByteBuffer bb = new ByteBuffer();
			if( !bb.Append( HashTable_in ) )
				return false;
			int cb = bb.Length + 4;
			if( !bb.Insert( 0, 4 ) )
				return false;
			bb.BinaryWriter( 0 ).Write( cb );
			return Manager.Send( SocketID, bb.Bytes, bb.Length );
		}


		//---------------------------------------------------------------------
		public Hashtable WaitForReply()
		{
			Hashtable ht = default( Hashtable );
			long start = System.DateTime.UtcNow.Ticks;
			while( (((System.DateTime.UtcNow.Ticks - start) / TimeSpan.TicksPerMillisecond) < ServerTimeout) )
			{
				if( (ReplyBuffer_.Length > 4) )
				{
					int cb = ReplyBuffer_.BinaryReader( 0 ).ReadInt32();
					if( (ReplyBuffer_.Length >= cb) )
					{
						try
						{
							System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
							ht = (Hashtable)bf.Deserialize( ReplyBuffer_.MemoryStream( 4 ) );
						}
						catch( Exception ex )
						{
							//TraceText("E", "Hashtable_Client_SocketHandler::WaitForReply", "Exception during deserialize: " & ex.Message)
							//Debug.Assert( false, "Exception during deserialize: " + ex.Message );
							return null;
						}
						ReplyBuffer_.Remove( 0, cb );
						return ht;
					}
				}
				System.Threading.Thread.Sleep( 1 );
			}
			//TraceText("W", "Hashtable_Client_SocketHandler::WaitForReply", "Server timed out.")
			return null;
		}


		//---------------------------------------------------------------------
		internal override bool SocketReceive( int ByteCount_in )
		{
			ReplyBuffer_.Append( RecvBuffer.Bytes, ByteCount_in );
			return true;
		}


	}


	//=================================================================
	//
	//       CLASS: aops.sockets.Hashtable_Server_SocketHandler
	//
	//=================================================================


	public class Hashtable_Server_SocketHandler : SocketHandler
	{


		//---------------------------------------------------------------------

		private ByteBuffer ReplyBuffer_ = new ByteBuffer();

		//---------------------------------------------------------------------
		public bool SendHashtable( Hashtable HashTable_in )
		{
			ByteBuffer bb = new ByteBuffer();
			bb.Append( HashTable_in );
			int cb = bb.Length + 4;
			bb.Insert( 0, 4 );
			bb.BinaryWriter( 0 ).Write( cb );
			return Manager.Send( SocketID, bb.Bytes, bb.Length );
		}


		//---------------------------------------------------------------------
		public virtual bool ReceiveHashtable( Hashtable HashTable_in )
		{
			//TraceText("I", "Hashtable_Client_SocketHandler::ReceiveHashtable", "echo: " & HashTable_in.Item("TestString"))
			return SendHashtable( HashTable_in );
			//Return True
		}


		//---------------------------------------------------------------------
		internal override SocketHandler DaemonSpawn()
		{
			return new Hashtable_Server_SocketHandler();
		}


		//---------------------------------------------------------------------
		internal override bool SocketReceive( int ByteCount_in )
		{
			lock( (ReplyBuffer_) )
			{
				ReplyBuffer_.Append( RecvBuffer.Bytes, ByteCount_in );
				Hashtable ht = default( Hashtable );
				while( true )
				{
					ht = null;
					if( (ReplyBuffer_.Length < 4) )
						return true;
					int cb = ReplyBuffer_.BinaryReader( 0 ).ReadInt32();
					if( (ReplyBuffer_.Length < cb) )
						return true;
					try
					{
						System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
						ReplyBuffer_.Remove( 0, 4 );
						ht = (Hashtable)bf.Deserialize( ReplyBuffer_.MemoryStream( 0 ) );
						ReplyBuffer_.Remove( 0, cb - 4 );
					}
					catch( Exception ex )
					{
						//TraceText("E", "Hashtable_Server_SocketHandler::SocketReceive", "Exception during deserialize: " & ex.Message)
						//Debug.Assert( false, "Exception during deserialize: " + ex.Message );
						return false;
					}
					if( !ReceiveHashtable( ht ) )
					{
						return false;
					}
					System.Threading.Thread.Sleep( 1 );
				}
			}
			return true;
		}


	}

}
