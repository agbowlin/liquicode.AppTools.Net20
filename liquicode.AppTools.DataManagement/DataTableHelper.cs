

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace liquicode.AppTools
{
	public static class DataTableHelper
	{


		//---------------------------------------------------------------------
		public class DataRowCopier
		{
			private int[] _ColumnMap = null;

			public DataRowCopier( DataColumnCollection Primary, DataColumnCollection Ancillary )
			{
				this._ColumnMap = new Int32[ Primary.Count ];
				for( int index = 0; index < Primary.Count; index++ )
				{
					string name = Primary[ index ].ColumnName;
					if( Ancillary.Contains( name ) )
					{ this._ColumnMap[ index ] = Ancillary[ name ].Ordinal; }
					else
					{ this._ColumnMap[ index ] = -1; }
				}
				return;
			}

			public void CopyRow( DataRow Primary, DataRow Ancillary, bool AllowOverwriteNonNullData )
			{
				for( int index = 0; index < this._ColumnMap.Length; index++ )
				{
					int ancillary_index = this._ColumnMap[ index ];
					if( ancillary_index >= 0 )
						if( AllowOverwriteNonNullData )
						{ Primary[ index ] = Ancillary[ ancillary_index ]; }
						else if( Primary[ index ] is System.DBNull )
						{ Primary[ index ] = Ancillary[ ancillary_index ]; }
				}
				return;
			}

		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Export the table's column names as a tab-delimited text string.
		/// </summary>
		/// <param name="ThisTable"></param>
		/// <returns></returns>
		public static string ExportHeaderAsText( DataTable ThisTable )
		{
			StringBuilder builder = new StringBuilder();
			string value_delimiter = "";
			foreach( DataColumn column in ThisTable.Columns )
			{
				builder.Append( value_delimiter );
				//Builder.Append( column.ColumnName );
				builder.Append( column.Caption );
				value_delimiter = "\t";
			}
			builder.Append( "\n" );
			return builder.ToString();
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Export the table's data as a tab-delimited text string.
		/// </summary>
		/// <param name="ThisTable"></param>
		/// <returns></returns>
		public static string ExportDataAsText( DataTable ThisTable )
		{
			StringBuilder builder = new StringBuilder();
			foreach( DataRow row in ThisTable.Rows )
			{
				string value_delimiter = "";
				foreach( DataColumn column in ThisTable.Columns )
				{
					builder.Append( value_delimiter );
					string value = row[ column ].ToString();
					value = value.Replace( "\t", " " );
					value = value.Replace( "\n", " | " );
					builder.Append( value );
					value_delimiter = "\t";
				}
				builder.Append( "\n" );
			}
			return builder.ToString();
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Performs a memberwise copy of the given column.
		/// </summary>
		/// <param name="Column"></param>
		/// <returns></returns>
		public static DataColumn CloneColumn( DataColumn Column )
		{
			DataColumn new_column = new DataColumn( Column.ColumnName, Column.DataType );
			new_column.AllowDBNull = Column.AllowDBNull;
			new_column.AutoIncrement = Column.AutoIncrement;
			new_column.AutoIncrementSeed = Column.AutoIncrementSeed;
			new_column.AutoIncrementStep = Column.AutoIncrementStep;
			new_column.Caption = Column.Caption;
			new_column.ColumnMapping = Column.ColumnMapping;
			new_column.DateTimeMode = Column.DateTimeMode;
			new_column.DefaultValue = Column.DefaultValue;
			new_column.Expression = Column.Expression;
			new_column.MaxLength = Column.MaxLength;
			new_column.Namespace = Column.Namespace;
			new_column.Prefix = Column.Prefix;
			new_column.ReadOnly = Column.ReadOnly;
			new_column.Unique = Column.Unique;
			return new_column;
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Performs a memberwise copy of the given column.
		/// </summary>
		/// <param name="Source"></param>
		/// <param name="Target"></param>
		public static void CopyColumn( DataColumn Source, DataColumn Target )
		{
			Target.ColumnName = Source.ColumnName;
			Target.DataType = Source.DataType;
			Target.AllowDBNull = Source.AllowDBNull;
			Target.AutoIncrement = Source.AutoIncrement;
			Target.AutoIncrementSeed = Source.AutoIncrementSeed;
			Target.AutoIncrementStep = Source.AutoIncrementStep;
			Target.Caption = Source.Caption;
			Target.ColumnMapping = Source.ColumnMapping;
			Target.DateTimeMode = Source.DateTimeMode;
			Target.DefaultValue = Source.DefaultValue;
			Target.Expression = Source.Expression;
			Target.MaxLength = Source.MaxLength;
			Target.Namespace = Source.Namespace;
			Target.Prefix = Source.Prefix;
			Target.ReadOnly = Source.ReadOnly;
			Target.Unique = Source.Unique;
			return;
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Ensures that all columns from Ancillary table are included in Primary table.
		/// If a column from Ancillary table does not exist within Primary table, one is
		/// created and appended to the collection of Primary table's columns.
		/// </summary>
		/// <param name="Primary">The table which will be modified.</param>
		/// <param name="Ancillary">The table from which to combine columns.</param>
		public static void CombineColumns( DataTable Primary, DataTable Ancillary )
		{
			if( Ancillary == null ) { return; }
			foreach( DataColumn column in Ancillary.Columns )
			{
				if( false == Primary.Columns.Contains( column.ColumnName ) )
				{
					//DataColumn new_column = DataManagement.CloneObject<DataColumn>( column );
					DataColumn new_column = CloneColumn( column );
					Primary.Columns.Add( new_column );
				}
			}
			return;
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Updates the Primary table with data contained in the Ancillary table.
		/// This method assumes that Primary and Ancillary tables share the exact
		/// same columns and in the same order. Furthermore, this method assumes
		/// that rows contained within the Primary and Ancillary tables are in
		/// the same order. In other words, all updates are performed using row
		/// and column indexes. See the UpdateTable method for updating tables
		/// whose rows and/or columns are not synchronized.
		/// </summary>
		/// <param name="Primary">The table which will be modified.</param>
		/// <param name="Ancillary">The table from which data will be updated.</param>
		/// <param name="AllowRowAppend">Allow this method to append rows to the Primary table.</param>
		public static void UpdateSynchronizedTable( DataTable Primary, DataTable Ancillary, bool AllowRowAppend )
		{
			if( Ancillary == null ) { return; }
			for( int row_index = 0; row_index < Ancillary.Rows.Count; row_index++ )
			{
				DataRow ancillary_row = Ancillary.Rows[ row_index ];
				DataRow primary_row = null;

				// Get the Primary row.
				if( row_index < Primary.Rows.Count )
				{
					primary_row = Primary.Rows[ row_index ];
				}
				else if( AllowRowAppend )
				{
					primary_row = Primary.NewRow();
					Primary.Rows.Add( primary_row );
				}
				else
				{
					break;
				}

				// Update the Primary row.
				int column_index = 0;
				for( column_index = 0; column_index < Ancillary.Columns.Count; column_index++ )
				{
					primary_row[ column_index ] = ancillary_row[ column_index ];
				}
			}
			return;
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Clones the given tables columns and rows.
		/// </summary>
		/// <param name="Table"></param>
		/// <returns></returns>
		public static DataTable CloneTable( DataTable ThisTable )
		{
			DataTable table = new DataTable();
			CombineColumns( table, ThisTable );
			UpdateSynchronizedTable( table, ThisTable, true );
			return table;
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Updates the Primary table with data contained in the Ancillary table.
		/// It is assumed that the 
		/// </summary>
		/// <param name="Primary">The table which will be modified.</param>
		/// <param name="PrimaryKey">The column from the Primary table to use as the key for matching rows.</param>
		/// <param name="Ancillary">The table from which data will be updated.</param>
		/// <param name="AncillaryKey">The column from the Ancillary table to use as the key for matching rows.</param>
		public static void UpdateTable
		(
			DataTable Primary, DataColumn PrimaryKey
			, DataTable Ancillary, DataColumn AncillaryKey
		)
		{
			//UpdateTable_0( Primary, PrimaryKey, Ancillary, AncillaryKey, false, true, false );
			//return;

			if( Primary == null ) { return; }
			if( PrimaryKey == null ) { return; }
			if( Ancillary == null ) { return; }
			if( AncillaryKey == null ) { return; }

			Primary.AcceptChanges();
			Primary.PrimaryKey = new DataColumn[] { PrimaryKey };
			Ancillary.PrimaryKey = new DataColumn[] { AncillaryKey };

			int n = Primary.Rows.Count;
			Primary.Merge( Ancillary, false, MissingSchemaAction.Add );

			return;
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Updates the Primary table with data contained in the Ancillary table.
		/// </summary>
		/// <param name="Primary">The table which will be modified.</param>
		/// <param name="PrimaryKey">The column from the Primary table to use as the key for matching rows.</param>
		/// <param name="Ancillary">The table from which data will be updated.</param>
		/// <param name="AncillaryKey">The column from the Ancillary table to use as the key for matching rows.</param>
		/// <param name="AllowRowAppend">Allow this method to append rows to the Primary table.</param>
		/// <param name="AllowColumnAppend">Allow this method to append columns to the Primary table.</param>
		public static void UpdateTableWithNonUniqueKeys
		(
			DataTable Primary, DataColumn PrimaryKey
			, DataTable Ancillary, DataColumn AncillaryKey
			, bool AllowRowAppend
			, bool AllowColumnAppend
			, bool AllowOverwriteNonNullData
		)
		{
			if( Primary == null ) { return; }
			if( PrimaryKey == null ) { return; }
			if( Ancillary == null ) { return; }
			if( AncillaryKey == null ) { return; }

			if( AllowColumnAppend )
			{
				CombineColumns( Primary, Ancillary );
			}

			DataRowCopier row_copier = new DataRowCopier( Primary.Columns, Ancillary.Columns );
			for( int row_index = 0; row_index < Ancillary.Rows.Count; row_index++ )
			{
				DataRow ancillary_row = Ancillary.Rows[ row_index ];
				DataRow primary_row = null;

				// Get the Primary row.
				//TODO: Implement data type specific filters.
				string primary_filter = string.Format( "[{0}] = '{1}'", PrimaryKey.ColumnName, ancillary_row.ItemArray[ AncillaryKey.Ordinal ].ToString() );
				DataRow[] primary_rows = Primary.Select( primary_filter );
				if( primary_rows == null ) { throw new Exception( "Key matching returned a non-result while updating table." ); }
				else if( primary_rows.Length > 1 )
				{
					//throw new Exception( "Key matching returned a multiple results while updating table." );
					primary_row = primary_rows[ 0 ];
				}
				else if( primary_rows.Length == 1 )
				{
					primary_row = primary_rows[ 0 ];
				}
				else if( AllowRowAppend )
				{
					primary_row = Primary.NewRow();
					Primary.Rows.Add( primary_row );
				}

				// Update the Primary row.
				if( primary_row != null )
				{
					row_copier.CopyRow( primary_row, ancillary_row, AllowOverwriteNonNullData );
				}
			}

			return;
		}


	}
}
