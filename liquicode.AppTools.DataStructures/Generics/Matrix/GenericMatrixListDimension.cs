

using System;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		public class GenericMatrixListDimension<T> : GenericMatrixDimension<T>
		{


			//--------------------------------------------------------------------
			internal GenericMatrixListDimension( GenericMatrix<T> Matrix_in, int Min_in )
			{
				this._matrix = Matrix_in;
				this._min = Min_in;
				return;
			}


			//--------------------------------------------------------------------
			internal GenericMatrixListDimension( GenericMatrix<T> Matrix_in )
				: this( Matrix_in, 0 )
			{
				return;
			}


			//--------------------------------------------------------------------
			public override int Count
			{
				get { return this._matrix._List.Count; }
			}


			//--------------------------------------------------------------------
			public override T[] GetVector( int Index_in )
			{
				if( !this.Validate( Index_in ) )
					throw new IndexOutOfRangeException();
				return this._matrix._List[ Index_in - this._min ];
			}


			//--------------------------------------------------------------------
			public override void SetVector( int Index_in, T[] Vector_in )
			{
				if( !this.Validate( Index_in ) )
					throw new IndexOutOfRangeException();
				this._matrix._List[ Index_in - this._min ] = Vector_in;
				return;
			}


			//--------------------------------------------------------------------
			public override void Append( int Count_in )
			{
				if( (Count_in < 0) )
					throw new IndexOutOfRangeException();
				if( (Count_in == 0) )
					return;
				for( int ndx = 0; ndx <= (Count_in - 1); ndx++ )
				{
					this._matrix._List.Add( new T[] { } );
				}
				return;
			}


			//--------------------------------------------------------------------
			public override void Delete( int Index_in, int Count_in )
			{
				if( (Count_in < 0) )
					throw new IndexOutOfRangeException();
				if( (Count_in == 0) )
					return;
				this._matrix._List.RemoveRange( (Index_in - this._min), Count_in );
				return;
			}


		}

	}
}
