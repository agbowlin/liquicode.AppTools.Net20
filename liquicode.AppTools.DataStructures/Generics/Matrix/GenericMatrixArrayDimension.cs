

using System;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		public class GenericMatrixArrayDimension<T> : GenericMatrixDimension<T>
		{


			//--------------------------------------------------------------------
			private int _Count = 0;


			//--------------------------------------------------------------------
			internal GenericMatrixArrayDimension( GenericMatrix<T> Matrix_in, int Min_in )
			{
				this._matrix = Matrix_in;
				this._min = Min_in;
				return;
			}


			//--------------------------------------------------------------------
			internal GenericMatrixArrayDimension( GenericMatrix<T> Matrix_in )
				: this( Matrix_in, 0 )
			{
				return;
			}


			//--------------------------------------------------------------------
			public override int Count
			{
				get { return this._Count; }
			}


			//--------------------------------------------------------------------
			public override T[] GetVector( int Index_in )
			{
				if( !this.Validate( Index_in ) )
					throw new IndexOutOfRangeException();
				int xmax = (this._matrix._X.Count - 1);
				T[] values = new T[] { };
				System.Array.Resize<T>( ref values, (xmax + 1) );
				for( int xindex = 0; xindex <= xmax; xindex++ )
				{
					T[] xvector = this._matrix._X.GetVector( xindex + this._matrix._X.Min );
					if( ((Index_in - this._min) < xvector.Length) )
					{
						values[ xindex ] = xvector[ Index_in - this._min ];
					}
					else
					{
						values[ xindex ] = default( T );
					}
				}
				return values;
			}


			//--------------------------------------------------------------------
			public override void SetVector( int Index_in, T[] Vector_in )
			{
				if( !this.Validate( Index_in ) )
					throw new IndexOutOfRangeException();
				int yindex = (Index_in - this._min);
				T[] xvector = new T[] { };
				int xmax = (this._matrix._X.Count - 1);
				T value = default( T );
				for( int xindex = 0; xindex <= xmax; xindex++ )
				{
					if( (xindex < Vector_in.Length) )
					{
						value = Vector_in[ xindex ];
					}
					else
					{
						value = default( T );
					}
					xvector = this._matrix._X.GetVector( xindex + this._matrix._X.Min );
					if( (yindex < xvector.Length) )
					{
						xvector[ yindex ] = value;
						this._matrix._X.SetVector( xindex + this._matrix._X.Min, xvector );
					}
					else
					{
						if( (value != null) )
						{
							System.Array.Resize<T>( ref xvector, (yindex + 1) );
							xvector[ yindex ] = value;
							this._matrix._X.SetVector( xindex + this._matrix._X.Min, xvector );
						}
					}
				}
				return;
			}


			//--------------------------------------------------------------------
			public override void Append( int Count_in )
			{
				if( (Count_in < 0) )
					throw new IndexOutOfRangeException();
				if( (Count_in == 0) )
					return;
				this._Count += Count_in;
				return;
			}


			//--------------------------------------------------------------------
			public override void Delete( int Index_in, int Count_in )
			{
				if( !this.Validate( Index_in ) )
					throw new IndexOutOfRangeException();
				if( !this.Validate( Index_in + (Count_in - 1) ) )
					throw new IndexOutOfRangeException();
				if( (Count_in < 0) )
					throw new IndexOutOfRangeException();
				if( (Count_in == 0) )
					return;
				int nMoveCount = this._Count - ((Index_in - this._min) + Count_in);
				int ndxMoveFrom = ((this._Count - nMoveCount) + this._min);
				if( (nMoveCount > 0) )
				{
					this.Move( ndxMoveFrom, Index_in, nMoveCount );
				}
				this._Count -= Count_in;
				return;
			}


		}

	}
}
