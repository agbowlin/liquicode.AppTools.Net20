

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		public partial class Node
		{


			//-------------------------------------------------
			private int _Indent = 0;
			private string _Key = "";
			private object _Value = null;
			private INodeNotificationListener _Listener = null;

			//-------------------------------------------------
			private Node _PrevNode = null;
			private Node _NextNode = null;


			//-------------------------------------------------
			public Node()
			{
				this._Key = Guid.NewGuid().ToString( "N" );
				this._Value = null;
				this._Listener = null;
			}


			//-------------------------------------------------
			public Node( string ThisKey, object ThisValue, INodeNotificationListener ThisListener )
			{
				if( string.IsNullOrEmpty( ThisKey ) )
				{ this._Key = Guid.NewGuid().ToString( "N" ); }
				else
				{ this._Key = ThisKey; }
				this._Value = ThisValue;
				this._Listener = ThisListener;
				return;
			}


			//-------------------------------------------------
			~Node()
			{
				this._NextNode = null;
				this._PrevNode = null;
				this._Listener = null;
				this._Value = null;
				this._Key = "";
				this._Indent = 0;
			}


			//-------------------------------------------------
			public string Key
			{
				get { return this._Key; }
				set { this._Key = value; }
			}


			//-------------------------------------------------
			public int Indent
			{
				get { return this._Indent; }
			}
			internal void SetIndent( int Value )
			{
				this._Indent = Value;
			}


			//-------------------------------------------------
			public object Value
			{
				get { return this._Value; }
				set { this._Value = value; }
			}


			//-------------------------------------------------
			public INodeNotificationListener Listener
			{
				get { return this._Listener; }
				set { this._Listener = value; }
			}


			//-------------------------------------------------
			public string TextGraph()
			{
				string sGraph = _Key;
				Node nodeNext = _NextNode;
				int ndx = 0;
				while( true )
				{
					if( (nodeNext == null) )
						break;

					if( (nodeNext._Indent <= _Indent) )
						break;

					sGraph = sGraph + "\n";
					for( ndx = 1; ndx <= (nodeNext._Indent - _Indent); ndx++ )
					{
						sGraph = sGraph + "\t";
					}
					sGraph = sGraph + nodeNext._Key;
					nodeNext = nodeNext._NextNode;
				}
				return sGraph;
			}


			//-------------------------------------------------
			public override string ToString()
			{
				string value = "";
				value += "(Indent=" + this._Indent.ToString();
				value += "; Key=" + this._Key.ToString();
				if( this._Value == null )
				{ value += "; Value=null"; }
				else
				{ value += "; Value=" + this._Value.ToString(); }
				value += ")";
				return value;
			}


		}

	}
}
