

using System;
using System.Collections.Generic;


namespace liquicode.AppTools
{


	public static class Strings
	{


		//---------------------------------------------------------------------
		public static bool IsNotNullOrEmpty( string Value )
		{
			return (!string.IsNullOrEmpty( Value ));
		}


		//---------------------------------------------------------------------
		public static bool AreEqual( string Value1, string Value2, bool CaseSensitive )
		{
			if( CaseSensitive )
			{
				return string.Equals( Value1, Value2, StringComparison.InvariantCulture );
			}
			else
			{
				return string.Equals( Value1, Value2, StringComparison.InvariantCultureIgnoreCase );
			}
		}


		//---------------------------------------------------------------------
		public static bool AreNotEqual( string Value1, string Value2, bool CaseSensitive )
		{
			return (!AreEqual( Value1, Value2, CaseSensitive ));
		}


		//---------------------------------------------------------------------
		public static string SafeAssign( string Value )
		{
			if( string.IsNullOrEmpty( Value ) ) { return ""; }
			return Value;
		}


		//---------------------------------------------------------------------
		public static string SafeAssign( string Value, int MaxLength )
		{
			if( string.IsNullOrEmpty( Value ) ) { Value = ""; }
			if( Value.Length > MaxLength ) { Value = Value.Substring( 0, MaxLength ); }
			return Value;
		}


		//=====================================================================
		//		CONVERSIONS
		//=====================================================================


		//---------------------------------------------------------------------
		public static string MakeHexString( Int16 Value_in )
		{
			string s = string.Format( "{0:x2} ", Value_in ).ToLower();
			return s;
		}


		//---------------------------------------------------------------------
		public static string MakeHexString( Int32 Value_in, bool Split_in )
		{
			string s = string.Format( "{0:x4} ", Value_in ).ToLower();
			if( Split_in ) { s = s.Insert( 4, "-" ); }
			return s;
		}


		//---------------------------------------------------------------------
		public static string MakeHexString( Int64 Value_in, bool Split_in )
		{
			string s = string.Format( "{0:x8} ", Value_in ).ToLower();
			if( Split_in )
			{
				s = s.Insert( 12, "-" );
				s = s.Insert( 8, "-" );
				s = s.Insert( 4, "-" );
			}
			return s;
		}


		//---------------------------------------------------------------------
		public static string MeshString( string Mesh_in, string String_in )
		{
			System.Text.StringBuilder sbmesh = new System.Text.StringBuilder();
			if( (Mesh_in.Length > String_in.Length) )
			{
				for( int ndx = 0; ndx <= (String_in.Length - 1); ndx++ )
				{
					sbmesh.Append( Mesh_in.Substring( ndx, 1 ) );
					sbmesh.Append( String_in.Substring( ndx, 1 ) );
				}
				sbmesh.Append( Mesh_in.Substring( String_in.Length ) );
			}
			else if( (Mesh_in.Length < String_in.Length) )
			{
				for( int ndx = 0; ndx <= (Mesh_in.Length - 1); ndx++ )
				{
					sbmesh.Append( Mesh_in.Substring( ndx, 1 ) );
					sbmesh.Append( String_in.Substring( ndx, 1 ) );
				}
				sbmesh.Append( String_in.Substring( Mesh_in.Length ) );
			}
			else
			{
				for( int ndx = 0; ndx <= (Mesh_in.Length - 1); ndx++ )
				{
					sbmesh.Append( Mesh_in.Substring( ndx, 1 ) );
					sbmesh.Append( String_in.Substring( ndx, 1 ) );
				}
			}
			return sbmesh.ToString();
		}


		//=====================================================================
		//		SPLIT OPERATIONS
		//=====================================================================

		//---------------------------------------------------------------------
		public static string JoinString( string[] Values, string Delimiter, bool RemoveEmptyEntries )
		{
			List<string> elements = new List<string>();
			if( RemoveEmptyEntries )
			{
				foreach( string element in Values )
				{
					if( Strings.IsNotNullOrEmpty( element ) )
					{ elements.Add( element ); }
				}
			}
			else
			{
				elements.AddRange( Values );
			}
			return string.Join( Delimiter, elements.ToArray() );
		}


		//---------------------------------------------------------------------
		public static string[] SplitOnString( string Value, string Delimiter, bool RemoveEmptyEntries )
		{
			StringSplitOptions options = StringSplitOptions.None;
			if( RemoveEmptyEntries ) { options = StringSplitOptions.RemoveEmptyEntries; }
			return Value.Split( new string[] { Delimiter }, options );
		}


		//---------------------------------------------------------------------
		public static string[] SplitOnTab( string Value, bool RemoveEmptyEntries )
		{
			StringSplitOptions options = StringSplitOptions.None;
			if( RemoveEmptyEntries ) { options = StringSplitOptions.RemoveEmptyEntries; }
			return Value.Split( new string[] { "\t" }, options );
		}


		//---------------------------------------------------------------------
		public static string[] SplitOnCrLf( string Value, bool RemoveEmptyEntries )
		{
			StringSplitOptions options = StringSplitOptions.None;
			if( RemoveEmptyEntries ) { options = StringSplitOptions.RemoveEmptyEntries; }
			return Value.Split( new string[] { "\n\r" }, options );
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Parses a list of key value pairs from a string.
		/// The keys must be unique.
		/// The values are simple text
		/// This function is useful for parsing connection strings.
		/// Example: SplitKeyValuePairList( "Data Source=Sql01; Initial Catalog=TestDB; Integrated Security=SSPI", "=", ";" )
		/// </summary>
		/// <param name="KeyValuePairList">The list of key-valur pairs.</param>
		/// <param name="KeyValueDelimiter">The delimiter which separates the key and the value.</param>
		/// <param name="KeyValuePairDelimiter">The delimiter which separates the key-value pairs.</param>
		/// <returns>A string dictionary containing the key-value pairs.</returns>
		public static Dictionary<string, string> SplitKeyValuePairList(
														string KeyValuePairList
														, string KeyValueDelimiter
														, string KeyValuePairDelimiter )
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string[] pairs = SplitOnString( KeyValuePairList, KeyValuePairDelimiter, true );
			foreach( string pair in pairs )
			{
				string[] key_value = SplitOnString( pair, KeyValueDelimiter, false );
				if( key_value.Length != 2 )
				{ throw new Exception( string.Format( "The input string is not of the correct format. Missing a '{0}'.", KeyValueDelimiter ) ); }
				dictionary[ key_value[ 0 ] ] = key_value[ 1 ];
			}
			return dictionary;
		}


		//=====================================================================
		//		LEXICAL OPERATIONS
		//=====================================================================

		//---------------------------------------------------------------------
		public static string WordifyIdentifier( string Identifier )
		{
			string sWords = "";
			int nUpper = 0;
			for( int ndx = 0; ndx <= (Identifier.Length - 1); ndx++ )
			{
				string s = Identifier.Substring( ndx, 1 );
				if( s == "_" )
				{
					s = " ";
					nUpper = 0;
				}
				else if( char.IsUpper( s, 0 ) )
				{
					nUpper += 1;
					if( nUpper == 1 )
					{
						s = " " + s.ToUpper();
					}
				}
				else
				{
					nUpper = 0;
				}
				sWords += s;
			}
			return sWords;
		}


		//---------------------------------------------------------------------
		public enum HumanizeSpacing
		{
			None = 0,
			SingleSpaceBetweenWords
		}

		public enum HumanizeCaptilization
		{
			None = 0,
			CapitalizeFirstWord,
			CapitalizeAllWords,
			ProperCaseAllWords
		}


		//---------------------------------------------------------------------
		public static string Humanize
		(
			string Text
			, Dictionary<string, string> Replacements
			, HumanizeSpacing Spacing
			, HumanizeCaptilization Captilization
		)
		{
			List<string> tokens = Tokenize( Text );
			string token = "";
			string NewText = "";
			foreach( string token_loopVariable in tokens )
			{
				token = token_loopVariable;
				if( (NewText.Length > 0) )
				{
					if( (Spacing == HumanizeSpacing.SingleSpaceBetweenWords) )
					{
						NewText += " ";
					}
				}
				if( (Captilization == HumanizeCaptilization.CapitalizeAllWords) )
				{
					token = Capitalize( token );
				}
				else if( (Captilization == HumanizeCaptilization.ProperCaseAllWords) )
				{
					token = Capitalize( token.ToLower() );
				}
				if( Replacements.ContainsKey( token.ToLower() ) )
				{
					token = Replacements[ token.ToLower() ];
				}
				NewText += token;
			}
			if( (Captilization == HumanizeCaptilization.CapitalizeFirstWord) )
			{
				NewText = Capitalize( NewText );
			}
			return NewText;
		}


		////---------------------------------------------------------------------
		//public static string Replace( string Text, Dictionary<string, string> Replacements )
		//{
		//    if( Replacements.ContainsKey( Text.ToLower() ) )
		//    { Text = Replacements[ Text.ToLower() ]; }
		//    return Text;
		//}


		////---------------------------------------------------------------------
		//public static string SearchReplace( string Content, Dictionary<string, string> Replacements )
		//{
		//    foreach( string key in Replacements.Keys )
		//    {
		//        Content = Content.Replace( key, Replacements[ key ] );
		//    }
		//    return Content;
		//}


		//---------------------------------------------------------------------
		public static string SearchReplaceDictionary( string Content, Dictionary<string, string> Replacements, bool CaseSensitive )
		{
			foreach( string key in Replacements.Keys )
			{
				if( CaseSensitive )
				{ Content = Content.Replace( key, Replacements[ key ] ); }
				else
				{ Content = SearchReplaceCaseInsensitive( Content, key, Replacements[ key ] ); }
			}
			return Content;
		}


		//---------------------------------------------------------------------
		public static string SearchReplace( string Content, string Pattern, string Replacement, bool CaseSensitive )
		{
			if( CaseSensitive )
			{ Content = Content.Replace( Pattern, Replacement ); }
			else
			{ Content = SearchReplaceCaseInsensitive( Content, Pattern, Replacement ); }
			return Content;
		}


		//---------------------------------------------------------------------
		public static string SearchReplace( string Content, string Pattern, string Replacement )
		{
			return SearchReplace( Content, Pattern, Replacement, true );
		}


		//---------------------------------------------------------------------
		public enum StringFilters
		{
			None = 0,
			Control = 1,
			Letter = 2,
			Number = 4,
			Punctuation = 8,
			WhiteSpace = 16,
		}


		//---------------------------------------------------------------------
		public static string Filter( string Content, StringFilters Filters )
		{
			string value = "";
			foreach( char ch in Content )
			{
				bool keep_char = false;
				if( Char.IsControl( ch ) ) { keep_char = ((Filters & StringFilters.Control) == StringFilters.Control); }
				else if( Char.IsLetter( ch ) ) { keep_char = ((Filters & StringFilters.Letter) == StringFilters.Letter); }
				else if( Char.IsNumber( ch ) ) { keep_char = ((Filters & StringFilters.Number) == StringFilters.Number); }
				else if( Char.IsPunctuation( ch ) ) { keep_char = ((Filters & StringFilters.Punctuation) == StringFilters.Punctuation); }
				else if( Char.IsWhiteSpace( ch ) ) { keep_char = ((Filters & StringFilters.WhiteSpace) == StringFilters.WhiteSpace); }
				if( keep_char ) { value += ch; }
			}
			return value;
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Filters out unwanted characters from a string keeping only the character types specified.
		/// </summary>
		/// <param name="Content">The string to filters characters out of.</param>
		/// <param name="Filters">The type of character to keep. Can be one or more of: 
		/// 'C' - Control characters;
		/// 'L' - Letter characters;
		/// 'N' - Numeric characters;
		/// 'P' - Punctuation characters;
		/// 'W' - Whitespace characters
		/// </param>
		/// <returns></returns>
		public static string Filter( string Content, string Filters )
		{
			Filters = Filters.ToUpper();
			string value = "";
			foreach( char ch in Content )
			{
				bool keep_char = false;
				if( Char.IsControl( ch ) ) { keep_char = Filters.Contains( "C" ); }
				else if( Char.IsLetter( ch ) ) { keep_char = Filters.Contains( "L" ); }
				else if( Char.IsNumber( ch ) ) { keep_char = Filters.Contains( "N" ); }
				else if( Char.IsPunctuation( ch ) ) { keep_char = Filters.Contains( "P" ); }
				else if( Char.IsWhiteSpace( ch ) ) { keep_char = Filters.Contains( "W" ); }
				if( keep_char ) { value += ch; }
			}
			return value;
		}


		//---------------------------------------------------------------------
		// From: http://www.codeproject.com/Articles/10890/Fastest-C-Case-Insenstive-String-Replace
		public static string SearchReplaceCaseInsensitive( string Original, string Pattern, string Replacement )
		{
			int count, position0, position1;
			count = position0 = position1 = 0;
			string upperString = Original.ToUpper();
			string upperPattern = Pattern.ToUpper();
			int inc = (Original.Length / Pattern.Length) * (Replacement.Length - Pattern.Length);
			char[] chars = new char[ Original.Length + Math.Max( 0, inc ) ];
			while( (position1 = upperString.IndexOf( upperPattern, position0 )) != -1 )
			{
				for( int i = position0; i < position1; ++i )
				{ chars[ count++ ] = Original[ i ]; }
				for( int i = 0; i < Replacement.Length; ++i )
				{ chars[ count++ ] = Replacement[ i ]; }
				position0 = position1 + Pattern.Length;
			}
			if( position0 == 0 )
			{ return Original; }
			for( int i = position0; i < Original.Length; ++i )
			{ chars[ count++ ] = Original[ i ]; }
			return new string( chars, 0, count );
		}


		//---------------------------------------------------------------------
		public static List<string> FindDelimitedText( string Text, string StartDelimiter, string StopDelimiter )
		{
			List<string> Terms = new List<string>();
			int ich = 0;
			while( true )
			{
				ich = Text.IndexOf( StartDelimiter, ich );
				if( ich < 0 ) { break; }
				ich += 1;
				int ich2 = Text.IndexOf( StopDelimiter, ich );
				if( ich2 < 0 ) { break; }
				Terms.Add( Text.Substring( ich, (ich2 - ich) ) );
				ich = ich2 + 1;
			}
			return Terms;
		}


		//---------------------------------------------------------------------
		public static string Capitalize( string Text )
		{
			if( Text.Length > 0 )
			{ Text = char.ToUpper( Text[ 0 ] ) + Text.Substring( 1, Text.Length - 1 ); }
			return Text;
		}


		//---------------------------------------------------------------------
		public static List<string> Tokenize( string Text )
		{
			List<string> tokens = new List<string>();
			string token = "";
			bool OnNumber = false;
			bool OnWord = false;
			bool OnWordWithLower = false;
			foreach( char ch in Text )
			{
				if( char.IsLetterOrDigit( ch ) )
				{
					if( char.IsDigit( ch ) )
					{
						if( OnNumber )
						{
							token += ch;
						}
						else
						{
							if( (token.Length > 0) )
							{
								tokens.Add( token );
								token = "";
							}
							token += ch;
							OnNumber = true;
						}
						OnWord = false;
						OnWordWithLower = false;
					}
					else
					{
						if( OnNumber )
						{
							if( (token.Length > 0) )
							{
								tokens.Add( token );
								token = "";
							}
							OnNumber = false;
							OnWord = false;
							OnWordWithLower = false;
						}
						if( char.IsUpper( ch ) )
						{
							if( OnWordWithLower )
							{
								if( (token.Length > 0) )
								{
									tokens.Add( token );
									token = "";
								}
								token += ch;
								OnWordWithLower = false;
							}
							else
							{
								token += ch;
							}
							OnNumber = false;
							OnWord = true;
						}
						else
						{
							token += ch;
							OnWord = true;
							OnWordWithLower = true;
						}
					}
				}
				else
				{
					if( (token.Length > 0) )
					{
						tokens.Add( token );
						token = "";
					}
					OnNumber = false;
					OnWord = false;
					OnWordWithLower = false;
				}
			}
			if( (token.Length > 0) )
			{
				tokens.Add( token );
			}
			if( OnWord ) { /* Do nothing, this is here to avoid compiler warning CS0219. */ }
			return tokens;
		}


	}


}
