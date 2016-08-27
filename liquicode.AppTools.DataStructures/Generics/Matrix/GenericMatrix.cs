

using System;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		public class GenericMatrix<T>
		{


			//--------------------------------------------------------------------
			internal List<T[]> _List = null;
			internal GenericMatrixListDimension<T> _X = null;
			internal GenericMatrixArrayDimension<T> _Y = null;


			//--------------------------------------------------------------------
			public GenericMatrix()
			{
				this._List = new List<T[]>();
				this._X = new GenericMatrixListDimension<T>( this );
				this._Y = new GenericMatrixArrayDimension<T>( this );
				return;
			}


			//--------------------------------------------------------------------
			public GenericMatrixDimension<T> X
			{
				get { return this._X; }
			}


			//--------------------------------------------------------------------
			public GenericMatrixDimension<T> Y
			{
				get { return this._Y; }
			}


			//--------------------------------------------------------------------
			public bool Validate( int CellX, int CellY )
			{
				if( this._X.Validate( CellX ) == false ) { return false; }
				if( this._Y.Validate( CellY ) == false ) { return false; }
				return true;
			}


			//--------------------------------------------------------------------
			public T GetCell( int CellX, int CellY )
			{
				if( this.Validate( CellX, CellY ) == false ) { throw new IndexOutOfRangeException(); }
				T[] values = this._X.GetVector( CellX );
				T value = default( T );
				if( (CellY - this._Y.Min) < values.Length )
				{
					value = values[ CellY - this._Y.Min ];
				}
				if( value == null )
				{
					value = default( T ); // was: new T();
					this.SetCell( CellX, CellY, value );
				}
				return value;
			}


			//--------------------------------------------------------------------
			public void SetCell( int CellX, int CellY, T Value )
			{
				if( this.Validate( CellX, CellY ) == false ) { throw new IndexOutOfRangeException(); }
				T[] values = this._X.GetVector( CellX );
				if( (CellY - this._Y.Min) < values.Length )
				{
					values[ CellY - this._Y.Min ] = Value;
					this._X.SetVector( CellX, values );
				}
				else
				{
					if( Value != null )
					{
						System.Array.Resize<T>( ref values, ((CellY - this._Y.Min) + 1) );
						values[ CellY - this._Y.Min ] = Value;
						this._X.SetVector( CellX, values );
					}
				}
				return;
			}


			//--------------------------------------------------------------------
			public void Clear()
			{
				this._List.Clear();
				if( this._Y.Count > 0 )
				{
					this._Y.Delete( this._Y.Count );
				}
				return;
			}


		}

	}
}
