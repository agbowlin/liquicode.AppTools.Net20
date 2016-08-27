

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
			public static bool operator ==( GenericVector<T> lhs, GenericVector<T> rhs )
			{ return lhs.Equals( rhs ); }


			//---------------------------------------------------------------------
			public static bool operator !=( GenericVector<T> lhs, GenericVector<T> rhs )
			{ return !(lhs.Equals( rhs )); }


			//---------------------------------------------------------------------
			public override bool Equals( object x )
			{
				try
				{
					GenericVector<T> that = (GenericVector<T>)x;
					if( this.Count != that.Count )
					{ return false; }
					for( int n = 0; n < this.Count; n++ )
					{
						if( !(this[ n ].Equals( that[ n ] )) )
						{ return false; }
					}
				}
				catch
				{
					return false;
				}
				return true;
			}


			//---------------------------------------------------------------------
			public override int GetHashCode()
			{ return base.GetHashCode(); }


		}


	}
}
