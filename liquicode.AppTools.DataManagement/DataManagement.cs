

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace liquicode.AppTools
{
	public static class DataManagement
	{


		//--------------------------------------------------------------------
		public static DateTime InvalidDate { get { return DateTime.MinValue; } }


		//=====================================================================
		//		STRING MANIPULATION
		//=====================================================================


		//---------------------------------------------------------------------
		public static string SafeFilename( string Filename )
		{
			Filename = Filename.Replace( "\\", "_" );
			Filename = Filename.Replace( "/", "_" );
			Filename = Filename.Replace( ":", "_" );
			Filename = Filename.Replace( "*", "_" );
			Filename = Filename.Replace( "?", "_" );
			Filename = Filename.Replace( "\"", "_" );
			Filename = Filename.Replace( "<", "_" );
			Filename = Filename.Replace( ">", "_" );
			Filename = Filename.Replace( "|", "_" );
			return Filename;
		}


		//=====================================================================
		//		SQL CONSTRUCTION
		//=====================================================================


		//--------------------------------------------------------------------
		public static string SqlSafeValue( string Value )
		{
			if( Value == null )
			{
				return "";
			}
			Value = Value.Replace( "'", "''" );
			return Value;
		}


		//---------------------------------------------------------------------
		public static string Strings2SqlInTerms( string[] Strings )
		{
			string sTerms = "";
			foreach( string s in Strings )
			{
				if( sTerms.Length > 0 )
					sTerms += ", ";
				sTerms += "'" + SqlSafeValue( s ) + "'";
			}
			return "(" + sTerms + ")";
		}


		//=====================================================================
		//		TYPE CASTING and CONVERSIONS
		//=====================================================================


		//--------------------------------------------------------------------
		public static string CastString( object Value, string DefaultValue )
		{
			if( (Value == null) ) { return DefaultValue; }
			if( Value is System.DBNull ) { return DefaultValue; }
			return Value.ToString();
		}
		public static string CastString( object Value )
		{ return CastString( Value, "" ); }


		//--------------------------------------------------------------------
		public static System.DateTime CastDate( object Value, System.DateTime DefaultValue, bool StripTime )
		{
			if( Value == null ) { return DefaultValue; }
			if( Value is System.DBNull ) { return DefaultValue; }
			if( !(Value is System.DateTime) ) { return DefaultValue; }
			System.DateTime dt = Convert.ToDateTime( Value );
			if( StripTime ) { dt = new System.DateTime( dt.Year, dt.Month, dt.Day ); }
			return dt;
		}
		public static System.DateTime CastDate( object Value )
		{ return CastDate( Value, DataManagement.InvalidDate, false ); }


		//--------------------------------------------------------------------
		public static string CastDate2String( object Value, string Format_in, string DefaultValue )
		{
			if( Value == null ) { return DefaultValue; }
			if( Value is System.DBNull ) { return DefaultValue; }
			if( !(Value is System.DateTime) ) { return DefaultValue; }
			System.DateTime dt = Convert.ToDateTime( Value );
			if( dt.Ticks == 0 ) { return DefaultValue; }
			return dt.ToString( Format_in );
		}
		public static string CastDate2String( object Value )
		{ return CastDate2String( Value, "", "" ); }


		//--------------------------------------------------------------------
		public static System.DateTime? CastNullableDate( object Value, System.DateTime? DefaultValue, bool StripTime )
		{
			if( Value == null ) { return DefaultValue; }
			if( Value is System.DBNull ) { return DefaultValue; }
			//if( !(Value is System.DateTime) ) { return DefaultValue; }
			System.DateTime dt;
			try { dt = Convert.ToDateTime( Value ); }
			catch( Exception ) { return DefaultValue; }
			if( StripTime ) { dt = new System.DateTime( dt.Year, dt.Month, dt.Day ); }
			return dt;
		}
		public static System.DateTime? CastNullableDate( object Value )
		{ return CastNullableDate( Value, null, false ); }


		//--------------------------------------------------------------------
		public static decimal CastDecimal( object Value, decimal DefaultValue )
		{
			if( Value == null ) { return DefaultValue; }
			if( Value is System.DBNull ) { return DefaultValue; }
			try { return Convert.ToDecimal( Value ); }
			catch( Exception ) { return DefaultValue; }
		}
		public static decimal CastDecimal( object Value )
		{ return CastDecimal( Value, 0.0m ); }


		//--------------------------------------------------------------------
		public static decimal? CastNullableDecimal( object Value, decimal? DefaultValue )
		{
			if( Value == null ) { return DefaultValue; }
			if( Value is System.DBNull ) { return DefaultValue; }
			try { return Convert.ToDecimal( Value ); }
			catch( Exception ) { return DefaultValue; }
		}
		public static decimal? CastNullableDecimal( object Value )
		{ return CastNullableDecimal( Value, null ); }


		//--------------------------------------------------------------------
		public static double CastDouble( object Value, double DefaultValue )
		{
			if( Value == null ) { return DefaultValue; }
			if( Value is System.DBNull ) { return DefaultValue; }
			try { return Convert.ToDouble( Value ); }
			catch( Exception ) { return DefaultValue; }
		}
		public static double CastDouble( object Value )
		{ return CastDouble( Value, 0.0 ); }


		//--------------------------------------------------------------------
		public static int CastInteger( object Value, int DefaultValue )
		{
			if( Value == null ) { return DefaultValue; }
			if( Value is System.DBNull ) { return DefaultValue; }
			try { return Convert.ToInt32( Value ); }
			catch( Exception ) { return DefaultValue; }
		}
		public static int CastInteger( object Value )
		{ return CastInteger( Value, 0 ); }


		//--------------------------------------------------------------------
		public static bool CastBoolean( object Value, bool DefaultValue )
		{
			if( Value == null ) { return DefaultValue; }
			if( Value is System.DBNull ) { return DefaultValue; }
			switch( Value.ToString().ToUpper() )
			{
				case "1":
				case "-1":
				case "T":
				case "TRUE":
				case "Y":
				case "YES":
				case "ON":
					return true;
				default:
					return false;
			}
		}
		public static bool CastBoolean( object Value )
		{ return CastBoolean( Value, false ); }


		//--------------------------------------------------------------------
		public static byte[] CastByteArray( object Value )
		{
			if( Value == null ) { return null; }
			if( Value is System.DBNull ) { return null; }
			return (byte[])Value;
		}


		//---------------------------------------------------------------------
		public static string Bytes2String( byte[] Bytes )
		{
			if( Bytes == null ) { return ""; }
			if( Bytes.Length == 0 ) { return ""; }
			string String = System.Text.Encoding.UTF8.GetString( Bytes );
			return String;
		}


		//---------------------------------------------------------------------
		public static byte[] String2Bytes( string String )
		{
			if( String == null ) { return new byte[] { }; }
			if( String.Length == 0 ) { return new byte[] { }; }
			byte[] Bytes = System.Text.Encoding.UTF8.GetBytes( String );
			return Bytes;
		}


		//---------------------------------------------------------------------
		public static string Bytes2Base64String( byte[] Bytes )
		{
			if( Bytes == null ) { return ""; }
			if( Bytes.Length == 0 ) { return ""; }
			string String = Convert.ToBase64String( Bytes );
			return String;
		}


		//---------------------------------------------------------------------
		public static byte[] Base64String2Bytes( string String )
		{
			if( String == null ) { return new byte[] { }; }
			if( String.Length == 0 ) { return new byte[] { }; }
			byte[] Bytes = Convert.FromBase64String( String );
			return Bytes;
		}


		//=====================================================================
		//		CLONING
		//=====================================================================


		//--------------------------------------------------------------------
		public static object CloneObject( object obj )
		{
			using( MemoryStream buffer = new MemoryStream() )
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize( buffer, obj );
				buffer.Position = 0;
				object temp = formatter.Deserialize( buffer );
				return temp;
			}
		}


		//--------------------------------------------------------------------
		public static T CloneObject<T>( T obj )
		{
			using( MemoryStream buffer = new MemoryStream() )
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize( buffer, obj );
				buffer.Position = 0;
				T temp = (T)formatter.Deserialize( buffer );
				return temp;
			}
		}


		//--------------------------------------------------------------------
		public static bool AreEqual( object obj0, object obj1 )
		{
			byte[] bytes0 = CastByteArray( obj0 );
			byte[] bytes1 = CastByteArray( obj1 );
			if( (bytes0.Length != bytes1.Length) )
			{ return false; }
			for( int ndx = 0; ndx <= (bytes0.Length - 1); ndx++ )
			{
				if( (bytes0[ ndx ] != bytes1[ ndx ]) )
				{ return false; }
			}
			return true;
		}


		//=====================================================================
		//		REGULAR EXPRESSIONS
		//=====================================================================


		//---------------------------------------------------------------------
		public static bool TestRegexMatch( string RegexPattern, string Text, bool MatchExact )
		{
			if( MatchExact == true )
			{
				if( false == RegexPattern.StartsWith( "^" ) ) { RegexPattern = "^" + RegexPattern; }
				if( false == RegexPattern.EndsWith( "$" ) ) { RegexPattern = RegexPattern + "$"; }
			}
			Regex regex = new Regex( RegexPattern );
			Match match = regex.Match( Text );
			return match.Success;
		}


		//=====================================================================
		//		ERROR REPORTING
		//=====================================================================


		//---------------------------------------------------------------------
		public static string UnwindExceptionMessage( Exception ex )
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			while( ex != null )
			{
				builder.AppendLine( ex.Message );
				ex = ex.InnerException;
			}
			return builder.ToString();
		}


		//---------------------------------------------------------------------
		public static string Exception2String( System.Exception Exception, bool IncludeStackTrace )
		{
			if( Exception == null ) { return ""; }
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			int i = 0;
			Exception ex = Exception;
			while( ex != null )
			{
				i++;
				builder.AppendLine( string.Format( "Message {0}: {1}\n", i.ToString( "000" ), ex.Message ) );
				ex = ex.InnerException;
			}
			if( IncludeStackTrace )
			{
				builder.AppendLine( "---------------------------------------------------------------------" );
				builder.AppendLine( "Stack Trace:" );
				builder.AppendLine( Exception.StackTrace );
			}
			return builder.ToString();
		}
		public static string Exception2String( System.Exception Exception )
		{ return Exception2String( Exception, true ); }


	}
}
