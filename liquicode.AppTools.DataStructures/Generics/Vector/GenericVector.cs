

using System;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		[Serializable]
		public partial class GenericVector<T> : List<T>
		{


			//---------------------------------------------------------------------
			public GenericVector()
				: base()
			{ return; }


			//---------------------------------------------------------------------
			public void Fill( T value )
			{
				for( int ndx = 0; ndx < this.Count; ndx++ )
				{ this[ ndx ] = value; }
				return;
			}


			//---------------------------------------------------------------------
			public void Fill( T value, int length )
			{
				this.Clear();
				this.AddRange( new T[ length ] );
				this.Fill( value );
				return;
			}


			//---------------------------------------------------------------------
			public void Substitute( T from, T to )
			{
				for( int ndx = 0; ndx < this.Count; ndx++ )
				{
					if( this[ ndx ].Equals( from ) )
					{ this[ ndx ] = to; }
				}
				return;
			}


		}


	}
}
