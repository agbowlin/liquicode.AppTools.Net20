

using System;
using System.Collections.Generic;


namespace liquicode.AppTools
{


	//---------------------------------------------------------------------
	public class Identifier { }
	public class IdentifierList : List<Identifier> { }


	//---------------------------------------------------------------------
	public enum VariableType
	{
		None = 0,
		String = 1,
		Numeric = 2,
		Date = 3
	}


	//---------------------------------------------------------------------
	public class Variable : Identifier
	{
		public string Name { get; set; }
		public VariableType Type { get; set; }
		public string Value { get; set; }
		public Variable()
		{ Name = ""; Type = VariableType.None; Value = ""; }
		public Variable( string ThisName, VariableType ThisType )
		{ Name = ThisName; Type = ThisType; Value = ""; }
	}


	//---------------------------------------------------------------------
	public class VariableList : List<Variable> { }


}
