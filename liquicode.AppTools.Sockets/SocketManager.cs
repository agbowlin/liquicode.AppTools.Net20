

using System;
using System.Collections;
using System.Collections.Generic;


namespace liquicode.AppTools
{
	public class SocketManager
					: IDisposable
	{


		//---------------------------------------------------------------------
		public System.Collections.SortedList Sockets = null;
		private AsyncCallback Callback_Accept_ = null;
		private AsyncCallback Callback_Receive_ = null;
		private AsyncCallback Callback_Send_ = null;

		//---------------------------------------------------------------------
		public SocketManager()
		{
			return;
		}


		//---------------------------------------------------------------------
		void IDisposable.Dispose()
		{
			this.Shutdown();
			this.Sockets = null;
			this.Callback_Accept_ = null;
			this.Callback_Receive_ = null;
			this.Callback_Send_ = null;
			return;
		}


		//---------------------------------------------------------------------
		public bool Startup()
		{
			this.Sockets = System.Collections.SortedList.Synchronized( new System.Collections.SortedList() );
			this.Callback_Accept_ = new AsyncCallback( this.Callback_Accept );
			this.Callback_Receive_ = new AsyncCallback( this.Callback_Receive );
			this.Callback_Send_ = new AsyncCallback( this.Callback_Send );
			return true;
		}


		//---------------------------------------------------------------------
		public void Shutdown()
		{
			while( Sockets.Count > 0 )
			{
				this.Close( (string)this.Sockets.GetKey( 0 ) );
			}
			return;
		}


		//---------------------------------------------------------------------
		public bool Close( string SocketID_in )
		{
			SocketHandler Connection = default( SocketHandler );
			Connection = (SocketHandler)this.Sockets[ SocketID_in ];
			if( (Connection == null) )
				return false;
			try
			{
				if( (Connection.Socket != null) )
				{
					if( Connection.Socket.Connected )
					{
						//TraceText("T", "SocketManager::Close", "Closing [" & Connection.Socket.RemoteEndPoint.ToString() & "].")
						Connection.Socket.Shutdown( System.Net.Sockets.SocketShutdown.Both );
					}
				}
			}
			catch( Exception ex )
			{
				//TraceText("E", "SocketManager::Close", ex.ToString())
				Sockets.Remove( Connection.SocketID );
				return false;
			}
			Sockets.Remove( Connection.SocketID );
			return true;
		}


		//---------------------------------------------------------------------
		public string AddListener( string ServiceAddress_in, int ServicePort_in, SocketHandler SocketHandler_in )
		{
			System.Net.IPHostEntry lipa = System.Net.Dns.GetHostEntry( ServiceAddress_in );
			System.Net.IPEndPoint lep = new System.Net.IPEndPoint( lipa.AddressList[ 0 ], ServicePort_in );
			SocketHandler_in.Manager = this;
			SocketHandler_in.Socket = new System.Net.Sockets.Socket( lep.Address.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp );
			SocketHandler_in.SocketType = "L";
			try
			{
				SocketHandler_in.Socket.Bind( lep );
				SocketHandler_in.Socket.Listen( 1000 );
				Sockets.Add( SocketHandler_in.SocketID, SocketHandler_in );
				//TraceText("T", "SocketManager::AddListener", "Waiting for a connection on [" & lep.ToString() & "].")
				SocketHandler_in.Socket.BeginAccept( Callback_Accept_, SocketHandler_in );
			}
			catch( Exception ex )
			{
				//TraceText("E", "SocketManager::AddListener", ex.ToString())
				return null;
			}
			return SocketHandler_in.SocketID;
		}


		//---------------------------------------------------------------------
		private void Callback_Accept( System.IAsyncResult ar )
		{
			SocketHandler Listener = default( SocketHandler );
			Listener = (SocketHandler)ar.AsyncState;
			System.Net.Sockets.Socket Skt = Listener.Socket.EndAccept( ar );
			try
			{
				SocketHandler Connection = default( SocketHandler );
				Connection = Listener.DaemonSpawn();
				Connection.Manager = this;
				Connection.Socket = Skt;
				Connection.SocketType = "I";
				Sockets.Add( Connection.SocketID, Connection );
				//TraceText("T", "SocketManager::Callback_Accept" _
				//                    , "Accepted connection on [" & Listener.Socket.LocalEndPoint.ToString() _
				//                    & "], from [" & Connection.Socket.RemoteEndPoint.ToString() & "].")
				Connection.Socket.BeginReceive( Connection.RecvBuffer.Bytes, 0, Connection.RecvBuffer.Length, System.Net.Sockets.SocketFlags.None, Callback_Receive_, Connection );
				Connection.SocketConnected();
			}
			catch( Exception ex )
			{
				//TraceText("E", "SocketManager::Callback_Accept", ex.ToString())
			}
			Listener.Socket.BeginAccept( Callback_Accept_, Listener );
			return;
		}


		//---------------------------------------------------------------------
		public string AddConnection( string ServiceAddress_in, int ServicePort_in, SocketHandler SocketHandler_in )
		{
			try
			{
				System.Net.IPHostEntry lipa = System.Net.Dns.GetHostEntry( ServiceAddress_in );
				System.Net.IPEndPoint lep = new System.Net.IPEndPoint( lipa.AddressList[ 0 ], ServicePort_in );
				SocketHandler_in.Manager = this;
				SocketHandler_in.Socket = new System.Net.Sockets.Socket( lep.Address.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp );
				SocketHandler_in.SocketType = "O";
				Sockets.Add( SocketHandler_in.SocketID, SocketHandler_in );
				SocketHandler_in.Socket.Connect( lep );
				//TraceText("T", "SocketManager::AddConnection" _
				//                    , "Waiting for for data on [" & SocketHandler_in.Socket.RemoteEndPoint.ToString() & "].")
				SocketHandler_in.Socket.BeginReceive( SocketHandler_in.RecvBuffer.Bytes, 0, SocketHandler_in.RecvBuffer.Length, System.Net.Sockets.SocketFlags.None, Callback_Receive_, SocketHandler_in );
				SocketHandler_in.SocketConnected();
			}
			catch( Exception ex )
			{
				//TraceText("E", "SocketManager::AddConnection", ex.ToString())
				return "";
			}
			return SocketHandler_in.SocketID;
		}


		//---------------------------------------------------------------------
		private void Callback_Receive( System.IAsyncResult ar )
		{
			SocketHandler Connection = default( SocketHandler );
			Connection = (SocketHandler)ar.AsyncState;
			try
			{
				int cb = 0;
				cb = Connection.Socket.EndReceive( ar );
				if( (cb == 0) )
				{
					Connection.Socket.Shutdown( System.Net.Sockets.SocketShutdown.Both );
					return;
				}
				//TraceText("S", "SocketManager::Callback_Receive" _
				//                    , "Received " & cb & " bytes on [" & Connection.Socket.RemoteEndPoint.ToString() & "].")
				if( !Connection.SocketReceive( cb ) )
				{
					return;
				}
				//TraceText("T", "SocketManager::Callback_Receive" _
				//                    , "Waiting for for data on [" & Connection.Socket.RemoteEndPoint.ToString() & "].")
				lock( Connection.RecvBuffer_Lock )
				{
					Connection.Socket.BeginReceive( Connection.RecvBuffer.Bytes, 0, Connection.RecvBuffer.Length, System.Net.Sockets.SocketFlags.None, Callback_Receive_, Connection );
				}
			}
			catch( Exception ex )
			{
				//TraceText("E", "SocketManager::Callback_Receive", ex.ToString())
				return;
			}
			return;
		}


		//---------------------------------------------------------------------
		public bool Send( string SocketID_in, byte[] Bytes_in, int ByteCount_in )
		{
			SocketHandler Connection = default( SocketHandler );
			Connection = (SocketHandler)Sockets[ SocketID_in ];
			if( (Connection == null) )
				return false;
			try
			{
				//Dim cb As Integer
				//cb = Connection.Socket.Send(Bytes_in, ByteCount_in, ystem.Net.Sockets.SocketFlags.None)
				//If (cb = 0) Then Return False
				byte[] ba = new byte[ ByteCount_in ];
				Array.Copy( Bytes_in, ba, ByteCount_in );
				Connection.Socket.BeginSend( ba, 0, ByteCount_in, System.Net.Sockets.SocketFlags.None, Callback_Send_, Connection );
			}
			catch( Exception ex )
			{
				//TraceText("E", "SocketManager::Send", ex.ToString())
				return false;
			}
			return true;
		}


		//---------------------------------------------------------------------
		private void Callback_Send( System.IAsyncResult ar )
		{
			SocketHandler Connection = default( SocketHandler );
			Connection = (SocketHandler)ar.AsyncState;
			Connection.Socket.EndSend( ar );
			return;
		}



	}
}
