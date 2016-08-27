

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
			// Finds the first Child with the specified Key
			public GenericNode<T> FindChildNode( string Key_in )
			{
				FindKeyVisitor visitor = new FindKeyVisitor( Key_in );
				this.VisitChildren( visitor );
				return visitor.FoundNode;
			}


			//-------------------------------------------------
			// Finds the first Descendent with the specified Key
			public GenericNode<T> FindDescNode( string Key_in )
			{
				FindKeyVisitor visitor = new FindKeyVisitor( Key_in );
				this.VisitDecendentsDepthFirst( visitor );
				return visitor.FoundNode;
			}


			//-------------------------------------------------
			public string Path( string Delimiter_in, bool IncludeLeadingDelimiter_in )
			{
				string sPath = _Key;
				GenericNode<T> nodePrev = this.ParentNode;
				while( (nodePrev != null) )
				{
					sPath = nodePrev._Key + Delimiter_in + sPath;
					nodePrev = nodePrev.ParentNode;
				}
				if( IncludeLeadingDelimiter_in )
				{
					sPath = Delimiter_in + sPath;
				}
				return sPath;
			}


			//-------------------------------------------------
			// Finds the Descendent Node specified by Path
			// The first character of the Path serves as the Path Delimiter
			// "\aaa" will Find the Child Node whose Key is "aaa"
			// "/aaa/bbb" will Find the Child "aaa" and return its Child Keyed "bbb"
			// "\" or "" will return This Node
			public GenericNode<T> FindPath( string Path_in )
			{
				if( (string.IsNullOrEmpty( Path_in )) )
				{
					return this;
				}
				string[] rgKeys = Path_in.Split( Path_in[ 0 ] );
				//string[] rgKeys = Converting.String2StringArray(Path_in, Path_in.Substring(0, 1), true);
				GenericNode<T> nodeChild = this;
				GenericNode<T> nodeNext = null;
				for( int ndx = 0; ndx <= (rgKeys.Length - 1); ndx++ )
				{
					nodeNext = nodeChild.FindChildNode( rgKeys[ ndx ] );
					if( (nodeNext == null) )
					{
						return null;
					}
					nodeNext = nodeChild;
				}
				return nodeChild;
			}


		}

	}
}
