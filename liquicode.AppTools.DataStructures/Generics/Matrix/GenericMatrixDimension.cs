

using System;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		public abstract class GenericMatrixDimension<T>
		{


			//--------------------------------------------------------------------
			protected GenericMatrix<T> _matrix = null;
			protected int _min = 0;


			//--------------------------------------------------------------------
			protected GenericMatrixDimension()
			{
			}
			//Friend Sub New(ByVal Matrix_in As Matrix2, Optional ByVal Min_in As Integer = 0)
			//	Me._matrix = Matrix_in
			//	Me._min = Min_in
			//End Sub


			//--------------------------------------------------------------------
			public virtual GenericMatrix<T> Matrix
			{
				get { return this._matrix; }
			}


			//--------------------------------------------------------------------
			public abstract int Count
			{
				get;
			}
			//public abstract T[] Vector
			//{
			//    get;
			//    set;
			//}
			public abstract T[] GetVector( int Index_in );
			public abstract void SetVector( int Index_in, T[] Vector_in );


			//--------------------------------------------------------------------
			public virtual int Min
			{
				get { return this._min; }
				set { this._min = value; }
			}


			//--------------------------------------------------------------------
			public virtual int Max
			{
				get { return (this._min + (this.Count - 1)); }
				set { this._min = (value - (this.Count - 1)); }
			}


			//--------------------------------------------------------------------
			public virtual bool Validate( int Index_in )
			{
				if( (Index_in < this._min) )
					return false;
				if( (Index_in > (this._min + (this.Count - 1))) )
					return false;
				return true;
			}


			//--------------------------------------------------------------------
			public virtual void Swap( int Index1_in, int Index2_in )
			{
				if( !this.Validate( Index1_in ) )
					throw new IndexOutOfRangeException();
				if( !this.Validate( Index2_in ) )
					throw new IndexOutOfRangeException();
				T[] values = this.GetVector( Index1_in );
				this.SetVector( Index1_in, this.GetVector( Index2_in ) );
				this.SetVector( Index2_in, values );
				return;
			}


			//--------------------------------------------------------------------
			public virtual void Copy( int Index1_in, int Index2_in, int Count_in )
			{
				if( (Count_in < 0) )
					throw new IndexOutOfRangeException();
				if( !this.Validate( Index1_in ) )
					throw new IndexOutOfRangeException();
				if( !this.Validate( Index2_in + (Count_in - 1) ) )
					throw new IndexOutOfRangeException();
				if( (Count_in == 0) )
					return;
				if( (Index1_in < Index2_in) )
				{
					for( int ndx = (Count_in - 1); ndx >= 0; ndx += -1 )
					{
						this.SetVector( Index2_in + ndx, this.GetVector( Index1_in + ndx ) );
					}
				}
				else if( (Index1_in > Index2_in) )
				{
					for( int ndx = 0; ndx <= (Count_in - 1); ndx++ )
					{
						this.SetVector( Index2_in + ndx, this.GetVector( Index1_in + ndx ) );
					}
				}
				return;
			}


			//--------------------------------------------------------------------
			public void Copy( int Index1_in, int Index2_in )
			{
				this.Copy( Index1_in, Index2_in, 1 );
				return;
			}


			//--------------------------------------------------------------------
			public virtual void Move( int Index1_in, int Index2_in, int Count_in )
			{
				if( (Count_in < 0) )
					throw new IndexOutOfRangeException();
				if( !this.Validate( Index1_in ) )
					throw new IndexOutOfRangeException();
				if( !this.Validate( Index2_in + (Count_in - 1) ) )
					throw new IndexOutOfRangeException();
				if( (Count_in == 0) )
					return;
				if( (Index1_in < Index2_in) )
				{
					for( int ndx = (Count_in - 1); ndx >= 0; ndx += -1 )
					{
						this.SetVector( Index2_in + ndx, this.GetVector( Index1_in + ndx ) );
						this.SetVector( Index1_in + ndx, new T[] { } );
					}
				}
				else if( (Index1_in > Index2_in) )
				{
					for( int ndx = 0; ndx <= (Count_in - 1); ndx++ )
					{
						this.SetVector( Index2_in + ndx, this.GetVector( Index1_in + ndx ) );
						this.SetVector( Index1_in + ndx, new T[] { } );
					}
				}
				return;
			}


			//--------------------------------------------------------------------
			public void Move( int Index1_in, int Index2_in )
			{
				this.Move( Index1_in, Index2_in, 1 );
				return;
			}


			//--------------------------------------------------------------------
			public abstract void Append( int Count_in );


			//--------------------------------------------------------------------
			public void Append()
			{
				this.Append( 1 );
				return;
			}


			//--------------------------------------------------------------------
			public abstract void Delete( int Index_in, int Count_in );


			//--------------------------------------------------------------------
			public void Delete( int Index_in )
			{
				this.Delete( Index_in, 1 );
				return;
			}


			//--------------------------------------------------------------------
			public virtual void Insert( int Index_in, int Count_in )
			{
				if( !this.Validate( Index_in ) )
					throw new IndexOutOfRangeException();
				if( (Count_in < 0) )
					throw new IndexOutOfRangeException();
				if( (Count_in == 0) )
					return;
				int n = ((this.Max - Index_in) + 1);
				this.Append( Count_in );
				this.Move( Index_in, (Index_in + Count_in), n );
				return;
			}


			//--------------------------------------------------------------------
			public void Insert( int Index_in )
			{
				this.Insert( Index_in, 1 );
				return;
			}


		}

	}
}
