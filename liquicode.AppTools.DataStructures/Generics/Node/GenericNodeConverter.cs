

using System;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		//-------------------------------------------------
		public abstract class GenericNodeConverter<S, T>
		{
			public abstract void Convert( GenericNode<S> SourceNode, GenericNode<T> TargetNode );
		}


		//-------------------------------------------------
		public class GenericConvertingVisitor<S, T> : GenericNode<S>.INodeVisitor
		{

			//-------------------------------------------------
			public GenericNodeConverter<S, T> NodeConverter = null;
			public GenericNode<S> SourceRoot = null;
			public GenericNode<T> TargetRoot = null;
			private GenericNode<T> _LastNode = null;

			//-------------------------------------------------
			public GenericConvertingVisitor( GenericNode<S> SourceRoot_in, GenericNodeConverter<S, T> NodeConverter_in )
			{
				this.SourceRoot = SourceRoot_in;
				this.NodeConverter = NodeConverter_in;
				this.TargetRoot = null;
				this._LastNode = null;
			}

			//-------------------------------------------------
			public bool Reset( VisitationType VisitationType_in )
			{
				if
				(
					  (VisitationType_in == VisitationType.NextNodes)
					| (VisitationType_in == VisitationType.NextSiblings)
					| (VisitationType_in == VisitationType.Children)
					| (VisitationType_in == VisitationType.DecendentsDepthFirst)
				)
				{
					this.TargetRoot = new GenericNode<T>();
					this.TargetRoot.Key = this.SourceRoot.Key;
					this.NodeConverter.Convert( this.SourceRoot, this.TargetRoot );
					this._LastNode = this.TargetRoot;
					return true;
				}
				else
				{
					this.TargetRoot = null;
					this._LastNode = null;
					return false;
				}
			}

			//-------------------------------------------------
			public bool VisitNode( GenericNode<S> Node_in )
			{
				// Create a new node.
				GenericNode<T> node = new GenericNode<T>();
				node.Key = Node_in.Key;
				this.NodeConverter.Convert( Node_in, node );
				// Position the new node.
				node.SetIndent( Node_in.Indent - this.SourceRoot.Indent );
				// Append the new node.
				this._LastNode.SetNextNode( node );
				node.SetPrevNode( this._LastNode );
				this._LastNode = node;
				// Return, OK
				return true;
			}

		}


		//-------------------------------------------------
		public class GenericObject2StringNodeConvereter : GenericNodeConverter<object, string>
		{
			public override void Convert( GenericNode<object> SourceNode, GenericNode<string> TargetNode )
			{
				TargetNode.Value = SourceNode.Value.ToString();
				return;
			}
		}


	}
}
