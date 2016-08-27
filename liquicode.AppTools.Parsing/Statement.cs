

using System;
using System.Collections.Generic;


namespace liquicode.AppTools
{


	//---------------------------------------------------------------------
	public abstract class Statement
	{
		public abstract void Execute( ProgramScope Scope );
	}
	public class StatementList : List<Statement> { }


	//---------------------------------------------------------------------
	public class DeclarationStatement : Statement
	{
		public Variable Variable { get; set; }

		public override string ToString()
		{
			return string.Format( "Dim {0} As {1};", this.Variable.Name, this.Variable.Type );
		}

		public override void Execute( ProgramScope Scope )
		{
			return;
		}

	}


	//---------------------------------------------------------------------
	public class AssignmentStatement : Statement
	{
		public Variable Variable { get; set; }
		public Expression Expression { get; set; }

		public override string ToString()
		{
			return string.Format( "Let {0} = {1};", this.Variable.Name, Expression.ToString() );
		}

		public override void Execute( ProgramScope Scope )
		{
			string result = this.Expression.Evaluate( Scope );
			if( this.Variable.Type == VariableType.String )
			{
				this.Variable.Value = result;
			}
			else if( this.Variable.Type == VariableType.Numeric )
			{
				double val = 0.0;
				double.TryParse( result, out val );
				this.Variable.Value = val.ToString();
			}
			else if( this.Variable.Type == VariableType.Date )
			{
				throw new NotImplementedException();
			}
			return;
		}

	}


	//---------------------------------------------------------------------
	public class ConditionalStatement : Statement
	{
		public Expression Condition { get; set; }
		public ProgramScope TrueConsequence { get; set; }
		public ProgramScope FalseConsequence { get; set; }

		public override string ToString()
		{
			return string.Format( "If( {0} ) Then {1} Else {2}"
									, this.Condition.ToString()
									, this.TrueConsequence.ToString()
									, this.FalseConsequence.ToString()
								);
		}

		public override void Execute( ProgramScope Scope )
		{
			throw new NotImplementedException();
		}

	}


	//---------------------------------------------------------------------
	public class LoopStatement : Statement
	{
		public Expression LoopCondition { get; set; }
		public ProgramScope LoopBody { get; set; }

		public override string ToString()
		{
			return string.Format( "While( {0} ) {1}"
									, this.LoopCondition.ToString()
									, this.LoopBody.ToString()
								);
		}

		public override void Execute( ProgramScope Scope )
		{
			throw new NotImplementedException();
		}

	}


	//---------------------------------------------------------------------
	public class InvocationStatement : Statement
	{
		public string TargetName { get; set; }
		public ExpressionList Arguments { get; set; }

		public override string ToString()
		{
			return string.Format( "Call {0}( {1} )"
									, this.TargetName.ToString()
									, this.Arguments.ToString()
								);
		}

		public override void Execute( ProgramScope Scope )
		{
			throw new NotImplementedException();
		}

	}


}
