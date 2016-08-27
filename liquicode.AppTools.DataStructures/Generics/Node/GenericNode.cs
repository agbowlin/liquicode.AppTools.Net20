

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		public partial class GenericNode<T>
		{


			//-------------------------------------------------
			private int _Indent = 0;
			private string _Key = "";
			private T _Value = default( T );
			private INodeNotificationListener _Listener = null;

			//-------------------------------------------------
			private GenericNode<T> _PrevNode = null;
			private GenericNode<T> _NextNode = null;


			////-------------------------------------------------
			//public enum NodeRelationships
			//{
			//    None = 0,
			//    PrevNode,
			//    // D3 = C2.Prev        A1
			//    NextNode,
			//    // C3 = C2.Next         +- B1
			//    FirstNode,
			//    // A1 = C1.First        |   +- C1
			//    LastNode,
			//    // C7 = C1.Last         |   |   +- D1
			//    RootNode,
			//    // A1 = C1.Root         |   |   +- D2
			//    ParentNode,
			//    // B1 = C1.Parent       |   |   +- D3
			//    PrevSibNode,
			//    // C1 = C2.PrevSib      |   +- C2
			//    NextSibNode,
			//    // C2 = C1.NextSib      |   +- C3
			//    FirstSibNode,
			//    // C1 = C1.FirstSib     +- B2
			//    LastSibNode,
			//    // C3 = C1.LastSib      |   +- C4
			//    FirstChildNode,
			//    // B1 = A1.FirstChild   |   +- C5
			//    LastChildNode,
			//    // B3 = A1.LastChild    |   +- C6
			//    FirstDescNode,
			//    // B1 = A1.FirstDesc    +- B3
			//    LastDescNode
			//    // C7 = A1.LastDesc         +- C7
			//}


			////-------------------------------------------------
			//public interface INodeReference
			//{
			//    GenericNode<T> Node
			//    {
			//        get;
			//    }
			//}


			//-------------------------------------------------
			public GenericNode()
			{
				this._Key = Guid.NewGuid().ToString( "N" );
				this._Value = default( T );
				this._Listener = null;
			}


			//-------------------------------------------------
			public GenericNode
			(
				string Key_in
				, T Value_in
				, INodeNotificationListener Listener_in
			)
			{
				if( Key_in.Length == 0 )
				{ this._Key = Guid.NewGuid().ToString( "N" ); }
				else
				{ this._Key = Key_in; }
				this._Value = Value_in;
				this._Listener = Listener_in;
			}


			//-------------------------------------------------
			~GenericNode()
			{
				this._NextNode = null;
				this._PrevNode = null;
				this._Listener = null;
				this._Value = default( T );
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
			public T Value
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
				GenericNode<T> nodeNext = _NextNode;
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


		}

	}
}
