

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		public partial class Node
		{


			//-------------------------------------------------
			public Node AddChild( Node Node_in, int Index_in, bool Silent_in )
			{
				// Validate.
				if( (Node_in == null) )
				{
					return null;
				}
				// Notify Before Operation
				if( !Silent_in )
				{
					this.Notify( new NodeAddChildNotification( NodeNotification.NotificationStatus.Pending, this, Node_in, Index_in ) );
				}
				// Adjust index.
				int nodeIndex = Index_in;
				// Get the insertion point.
				Node nodeInsertAfter = null;
				Node nodeInsertBefore = this.ChildNode( nodeIndex );
				if( (nodeInsertBefore == null) )
				{
					// Append the node.
					nodeInsertAfter = this.LastDescNode;
					if( (nodeInsertAfter == null) )
					{
						nodeInsertAfter = this;
					}
					nodeInsertBefore = nodeInsertAfter.NextNode;
				}
				else
				{
					// Insert the node.
					nodeInsertAfter = nodeInsertBefore.PrevNode;
				}
				// Get node range.
				Node nodeFirst = Node_in;
				Node nodeLast = nodeFirst.LastDescNode;
				if( (nodeLast == null) )
				{
					nodeLast = nodeFirst;
				}
				// Unlink nodes from source.
				if( (nodeFirst.PrevNode != null) )
				{
					nodeFirst.PrevNode.SetNextNode( nodeLast.NextNode );
				}
				if( (nodeLast.NextNode != null) )
				{
					nodeLast.NextNode.SetPrevNode( nodeFirst.PrevNode );
				}
				// Fix node indents.
				int nodeIndentDelta = ((this.Indent - Node_in.Indent) + 1);
				Node nodeNext = nodeFirst;
				while( (nodeNext != null) )
				{
					nodeNext.SetIndent( nodeNext.Indent + nodeIndentDelta );
					nodeNext = nodeNext.NextNode;
				}
				// Link nodes to target.
				nodeInsertAfter.SetNextNode( nodeFirst );
				nodeFirst.SetPrevNode( nodeInsertAfter );
				if( (nodeInsertBefore != null) )
				{
					nodeInsertBefore.SetPrevNode( nodeLast );
					nodeLast.SetNextNode( nodeInsertBefore );
				}
				// Notify After Operation
				if( !Silent_in )
				{
					this.Notify( new NodeAddChildNotification( NodeNotification.NotificationStatus.Completed, this, Node_in, Index_in ) );
				}
				// Return nodes.
				return Node_in;
			}


			//-------------------------------------------------
			public Node RemoveChild( int Index_in, bool Silent_in )
			{
				// Adjust index.
				int nodeIndex = Index_in;
				// Get node range.
				Node nodeFirst = this.ChildNode( nodeIndex );
				if( (nodeFirst == null) )
				{
					return null;
				}
				Node nodeLast = nodeFirst.LastDescNode;
				if( (nodeLast == null) )
				{
					nodeLast = nodeFirst;
				}
				// Notify Before Operation
				if( !Silent_in )
				{
					this.Notify( new NodeRemoveChildNotification( NodeNotification.NotificationStatus.Pending, this, nodeFirst, Index_in ) );
				}
				// Unlink nodes.
				this.SetNextNode( nodeLast.NextNode );
				if( (this.NextNode != null) )
				{
					this.NextNode.SetPrevNode( this );
				}
				nodeFirst.SetPrevNode( null );
				nodeLast.SetNextNode( null );
				// Notify After Operation
				if( !Silent_in )
				{
					this.Notify( new NodeRemoveChildNotification( NodeNotification.NotificationStatus.Completed, this, nodeFirst, Index_in ) );
				}
				// Return nodes.
				return nodeFirst;
			}


			//-------------------------------------------------
			public Node ClearChildren( bool Silent_in )
			{
				// Notify Before Operation
				if( !Silent_in )
				{
					this.Notify( new NodeClearChildrenNotification( NodeNotification.NotificationStatus.Pending, this ) );
				}
				// Get node range.
				Node nodeFirst = this.FirstDescNode;
				if( (nodeFirst == null) )
				{
					return null;
				}
				Node nodeLast = this.LastDescNode;
				// Unlink nodes.
				this.SetNextNode( nodeLast.NextNode );
				if( (this.NextNode != null) )
				{
					this.NextNode.SetPrevNode( this );
				}
				nodeFirst.SetPrevNode( null );
				nodeLast.SetNextNode( null );
				// Notify After Operation
				if( !Silent_in )
				{
					this.Notify( new NodeClearChildrenNotification( NodeNotification.NotificationStatus.Completed, this ) );
				}
				// Return nodes.
				return nodeFirst;
			}


		}

	}
}
