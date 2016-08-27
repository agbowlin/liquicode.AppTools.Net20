

using System;
using System.Collections.Generic;


namespace liquicode.AppTools
{


	public class ProgramScope
	{
		public ProgramScope ParentScope { get; set; }
		public VariableList Variables { get; set; }
		public StatementList Statements { get; set; }
		public ProgramScope( ProgramScope ThisParentScope )
		{
			this.ParentScope = ThisParentScope;
			this.Variables = new VariableList();
			this.Statements = new StatementList();
		}

		public Variable FindVariable( string VariableName, bool CheckParent )
		{
			foreach( Variable var in this.Variables )
			{
				if( string.Equals( var.Name, VariableName, System.StringComparison.InvariantCultureIgnoreCase ) )
				{ return var; }
			}
			if( this.ParentScope == null ) { return null; }
			if( false == CheckParent ) { return null; }
			return this.ParentScope.FindVariable( VariableName, true );
		}

		public void Execute()
		{
			foreach( Statement statement in this.Statements )
			{
				statement.Execute( this );
			}
			return;
		}

	}


}