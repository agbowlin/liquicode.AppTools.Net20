

using System;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		public partial class GenericVector<T> : List<T>
		{


			//---------------------------------------------------------------------
			public void CopyFromArray( T[] that )
			{
				this.Clear();
				this.AddRange( that );
				return;
			}


			//---------------------------------------------------------------------
			public new T[] ToArray()
			{ return this.ToArray( 0, this.Count ); }


			//---------------------------------------------------------------------
			public T[] ToArray( int StartIndex )
			{ return this.ToArray( StartIndex, this.Count ); }


			//---------------------------------------------------------------------
			public T[] ToArray( int StartIndex, int Count )
			{
				T[] array = new T[ Count ];
				for( int ndx = 0; ndx < Count; ndx++ )
				{ array[ ndx ] = this[ StartIndex + ndx ]; }
				return array;
			}


			//---------------------------------------------------------------------
			public static implicit operator T[]( GenericVector<T> rhs )
			{ return rhs.ToArray(); }


		}


	}
}
