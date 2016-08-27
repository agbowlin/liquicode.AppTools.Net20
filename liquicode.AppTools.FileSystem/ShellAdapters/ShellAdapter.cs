

using System;
using System.Collections.Generic;
using System.Text;

namespace liquicode.AppTools
{
	public abstract class ShellAdapter
	{

		//---------------------------------------------------------------------
		protected string _Host = "";
		public string Host
		{
			get { return this._Host; }
			set { this._Host = value; }
		}

		//---------------------------------------------------------------------
		protected int _Port = 0;
		public int Port
		{
			get { return this._Port; }
			set { this._Port = value; }
		}

		//---------------------------------------------------------------------
		protected string _User = "";
		public string User
		{
			get { return this._User; }
			set { this._User = value; }
		}


		//---------------------------------------------------------------------
		protected string _Password = "";
		public string Password
		{
			//get { return this._Password; }
			set { this._Password = value; }
		}

		//---------------------------------------------------------------------
		protected string _Root = "";
		public string Root
		{
			get { return this._Root; }
			set { this._Root = value; }
		}


		//---------------------------------------------------------------------
		protected string Reader2String( System.IO.StreamReader Reader )
		{
			string s = "";
			lock( Reader )
			{
				if( Reader.BaseStream.CanRead )
				{
					if( !Reader.EndOfStream )
					{
						while( Reader.Peek() >= 0 )
						{
							int ich = Reader.Read();
							s += Convert.ToChar( ich );
						}
					}
				}
			}
			return s;
		}


		//---------------------------------------------------------------------
		public abstract string ProtocolName { get; }


		//---------------------------------------------------------------------
		public abstract string ExecuteCommand( string ShellCommand );

	}
}
