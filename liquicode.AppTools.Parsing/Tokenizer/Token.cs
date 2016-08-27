

using System;
using System.Collections.Generic;


namespace liquicode.AppTools
{


	//---------------------------------------------------------------------
	public enum TokenType
	{
		Unknown = 0,
		Whitespace,
		Symbol,
		Literal,
		Identifier,
		Numeric,
		Keyword
	}


	//---------------------------------------------------------------------
	public class Token
	{
		public TokenType Type { get; set; }
		public string Text { get; set; }
		public int StartPosition { get; set; }

		public Token() { }
		public Token( TokenType ThisType, string ThisText )
		{
			this.Type = ThisType;
			this.Text = ThisText;
		}
		public Token( Token ThisToken )
		{
			this.Type = ThisToken.Type;
			this.Text = ThisToken.Text;
			this.StartPosition = ThisToken.StartPosition;
		}
		public Token Clone()
		{ return new Token( this ); }
	}


	//---------------------------------------------------------------------
	public class TokenList : List<Token>
	{

		public string ToDebugString()
		{
			System.Text.StringBuilder stringbuilder = new System.Text.StringBuilder();
			foreach( Token token in this )
			{
				string sText = token.Text;
				string sType = token.Type.ToString().PadRight( 15 );
				string sStart = token.StartPosition.ToString().PadRight( 5 );
				if( token.Type == TokenType.Whitespace ) { sText = ""; }
				stringbuilder.AppendFormat( "{0}{1} : {2}\n", sType, sStart, sText );
			}
			return stringbuilder.ToString();
		}

	}


}