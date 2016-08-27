

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{


		//-------------------------------------------------
		public class GenericNodeCloner<T>
		{

			//-------------------------------------------------
			public GenericNode<T> CloneNode( GenericNode<T> SourceNode )
			{
				GenericNode<T> node = new GenericNode<T>();
				node.Key = SourceNode.Key;
				node.Value = SourceNode.Value;
				return node;
			}

		}


		//-------------------------------------------------
		public class GenericCloningVisitor<T> : GenericNode<T>.INodeVisitor
		{

			//-------------------------------------------------
			public GenericNodeCloner<T> NodeCloner = null;
			public GenericNode<T> SourceRoot = null;
			public GenericNode<T> TargetRoot = null;
			private GenericNode<T> _LastNode = null;

			//-------------------------------------------------
			public GenericCloningVisitor( GenericNode<T> SourceRoot_in, GenericNodeCloner<T> NodeCloner_in )
			{
				this.SourceRoot = SourceRoot_in;
				this.NodeCloner = NodeCloner_in;
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
					this.TargetRoot.Value = this.SourceRoot.Value;
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
			public bool VisitNode( GenericNode<T> Node_in )
			{
				// Create a new node.
				GenericNode<T> node = new GenericNode<T>();
				node.Key = Node_in.Key;
				node.Value = Node_in.Value;
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


	}
}
