

using System;
using System.Collections.Generic;


namespace liquicode.AppTools
{


	public class Expression : TokenList
	{


		//---------------------------------------------------------------------
		public Expression()
		{
			return;
		}


		//---------------------------------------------------------------------
		public override string ToString()
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			string space = "";
			foreach( Token token in this )
			{
				builder.AppendFormat( "{0}{1}", space, token.Text );
				space = " ";
			}
			return builder.ToString();
		}


		//---------------------------------------------------------------------
		public static int OperatorPriority( string Operator )
		{
			switch( Operator )
			{
				case "^":
					return 300;

				case "*":
					return 200;
				case "/":
					return 200;
				case "%":
					return 200;

				case "+":
					return 100;
				case "-":
					return 100;

			}
			return 0;
		}


		//---------------------------------------------------------------------
		public string Evaluate( ProgramScope Scope )
		{
			Stack<Token> stack = new Stack<Token>();
			foreach( Token token in this )
			{
				if( token.Type == TokenType.Numeric )
				{ stack.Push( token ); }
				else if( token.Type == TokenType.Literal )
				{ stack.Push( token ); }
				else if( token.Type == TokenType.Identifier )
				{ stack.Push( token ); }
				else if( token.Type == TokenType.Symbol )
				{
					switch( token.Text )
					{
						case "^":
							this.EvaluateBinaryOperation_( Scope, stack, token );
							break;
						case "*":
							this.EvaluateBinaryOperation_( Scope, stack, token );
							break;
						case "/":
							this.EvaluateBinaryOperation_( Scope, stack, token );
							break;
						case "%":
							this.EvaluateBinaryOperation_( Scope, stack, token );
							break;
						case "+":
							this.EvaluateBinaryOperation_( Scope, stack, token );
							break;
						case "-":
							this.EvaluateBinaryOperation_( Scope, stack, token );
							break;
					}
				}
			}
			if( stack.Count != 1 ) { throw new Exception( "Internal error; Non-empty stack after expression evaluation." ); }
			return stack.Pop().Text;
		}


		//---------------------------------------------------------------------
		private void EvaluateBinaryOperation_( ProgramScope Scope, Stack<Token> Stack, Token Token )
		{
			if( Stack.Count < 2 ) { throw new Exception( string.Format( "Insufficent number of operands for binary operation '{0}'.", Token.Text ) ); }

			Token result = new Token();
			result.Type = TokenType.Numeric;

			Token token2 = Stack.Pop();
			if( token2.Type == TokenType.Numeric ) { }
			else if( token2.Type == TokenType.Literal ) { result.Type = TokenType.Literal; }
			else if( token2.Type == TokenType.Identifier )
			{
				Variable variable = Scope.FindVariable( token2.Text, true );
				if( variable.Type == VariableType.String )
				{
					token2 = new Token( TokenType.Literal, variable.Value );
					result.Type = TokenType.Literal;
				}
				else if( variable.Type == VariableType.Numeric )
				{
					token2 = new Token( TokenType.Numeric, variable.Value );
					result.Type = TokenType.Numeric;
				}
				else if( variable.Type == VariableType.Date )
				{ throw new NotImplementedException(); }
			}
			else { throw new Exception( string.Format( "Invalid operand '{0}' for this type of operation '{1}'.", token2.Text, Token.Text ) ); }

			Token token1 = Stack.Pop();
			if( token1.Type == TokenType.Numeric ) { }
			else if( token1.Type == TokenType.Literal ) { result.Type = TokenType.Literal; }
			else if( token1.Type == TokenType.Identifier )
			{
				Variable variable = Scope.FindVariable( token1.Text, true );
				if( variable.Type == VariableType.String )
				{
					token1 = new Token( TokenType.Literal, variable.Value );
					result.Type = TokenType.Literal;
				}
				else if( variable.Type == VariableType.Numeric )
				{
					token1 = new Token( TokenType.Numeric, variable.Value );
					result.Type = TokenType.Numeric;
				}
				else if( variable.Type == VariableType.Date )
				{ throw new NotImplementedException(); }
			}
			else { throw new Exception( string.Format( "Invalid operand '{0}' for this type of operation '{1}'.", token1.Text, Token.Text ) ); }

			if( result.Type == TokenType.Numeric )
			{
				double op1 = 0;
				double.TryParse( token1.Text, out op1 );
				double op2 = 0;
				double.TryParse( token2.Text, out op2 );
				double val = 0.0;
				switch( Token.Text )
				{
					case "^":
						val = Math.Pow( op1, op2 );
						break;
					case "*":
						val = (op1 * op2);
						break;
					case "/":
						val = (op1 / op2);
						break;
					case "%":
						val = (op1 % op2);
						break;
					case "+":
						val = (op1 + op2);
						break;
					case "-":
						val = (op1 - op2);
						break;
					//case "&": break;
					//case "|": break;
					default:
						throw new Exception( string.Format( "Unknown binary operation '{0}'.", Token.Text ) );
				}
				result.Text = val.ToString();
			}
			else if( result.Type == TokenType.Literal )
			{
				string op1 = token1.Text;
				string op2 = token2.Text;
				string val = "";
				switch( Token.Text )
				{
					case "+":
						val = (op1 + op2);
						break;
					default:
						throw new Exception( string.Format( "Unknown literal operation '{0}'.", Token.Text ) );
				}
				result.Text = val;
			}
			else { throw new Exception( "Internal error; Unsupported result type." ); }

			// Store result.
			Stack.Push( result );
			return;
		}


		//---------------------------------------------------------------------
		public void SetInfixExpression( Token[] InfixTokens )
		{
			Stack<Token> stack = new Stack<Token>();
			this.Clear();
			int ndxInfixToken = 0;
			bool expect_operand = true;
			bool negate_operand = false;
			while( true )
			{
				if( ndxInfixToken >= InfixTokens.Length ) { break; }
				Token token = InfixTokens[ ndxInfixToken ];
				if( token.Type == TokenType.Whitespace )
				{
					// Skip whitespace tokens.
				}
				else if( token.Type == TokenType.Numeric )
				{
					if( false == expect_operand ) { throw new Exception( "Unexpected operand." ); }
					Token new_token = token.Clone();
					// Append numeric tokens to the output list.
					if( negate_operand )
					{
						new_token.Text = string.Format( "-{0}", new_token.Text );
						negate_operand = false;
					}
					this.Add( new_token );
					expect_operand = false;
				}
				else if( token.Type == TokenType.Literal )
				{
					if( false == expect_operand ) { throw new Exception( "Unexpected operand." ); }
					Token new_token = token.Clone();
					// Append literal tokens to the output list.
					this.Add( new_token );
					expect_operand = false;
				}
				else if( token.Type == TokenType.Identifier )
				{
					if( false == expect_operand ) { throw new Exception( "Unexpected operand." ); }
					Token new_token = token.Clone();
					// Append identifier tokens to the output list.
					this.Add( new_token );
					expect_operand = false;
				}
				else if( token.Type == TokenType.Symbol )
				{
					// Use the stack to maintain the symbols.
					if( token.Text == "(" )
					{
						// Push opening parenthesis to stack.
						stack.Push( token.Clone() );
						expect_operand = true;
					}
					else if( token.Text == ")" )
					{
						// Pop tokens from stack and append to output list until matching open parenthesis is found.
						while( true )
						{
							if( stack.Count == 0 ) { throw new Exception( "Mismatched parentheses." ); }
							Token next_token = stack.Pop();
							if( next_token.Text == "(" ) { break; }
							this.Add( next_token );
						}
						expect_operand = false;
					}
					else if
					(
						(token.Text == "^")
						| (token.Text == "*")
						| (token.Text == "/")
						| (token.Text == "%")
						| (token.Text == "+")
						| (token.Text == "-")
					)
					{
						if( (token.Text == "-") & expect_operand )
						{
							negate_operand = true;
							expect_operand = true;
						}
						else
						{
							int priority = Expression.OperatorPriority( token.Text );
							if( priority == 0 ) { throw new Exception( string.Format( "Internal error; Operator has no priority '{0}'.", token.Text ) ); }
							if( stack.Count == 0 )
							{
								stack.Push( token.Clone() );
							}
							else
							{
								Token next_token = null;
								while( true )
								{
									next_token = stack.Pop();
									if( stack.Count == 0 ) { break; }
									int next_priority = Expression.OperatorPriority( next_token.Text );
									if( next_priority < priority ) { break; }
									this.Add( next_token );
								}
								stack.Push( next_token );
								stack.Push( token.Clone() );
							}
						}
						expect_operand = true;
					}
					else
					{
						// Unsupported symbol.
						throw new Exception( string.Format( "Unsupported symbol '{0}'.", token.Text ) );
					}

				}
				else
				{
					// Unsupported token.
					throw new Exception( string.Format( "Unsupported token type [{0}] '{1}'.", token.Type, token.Text ) );
				}
				ndxInfixToken++;
			}
			// Append any tokens remaining on the stack to the output list.
			while( stack.Count > 0 )
			{ this.Add( stack.Pop() ); }
			// Return
			return;
		}


	}


	public class ExpressionList : List<Expression>
	{

		//---------------------------------------------------------------------
		public override string ToString()
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			string space = "";
			foreach( Expression expression in this )
			{
				builder.AppendFormat( "{0}{1}", space, expression.ToString() );
				space = ", ";
			}
			return builder.ToString();
		}

	}


}
