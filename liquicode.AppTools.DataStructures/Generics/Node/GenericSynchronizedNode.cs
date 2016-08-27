

using System;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		public class GenericSynchronizedNode<S, T> : GenericNode<S>, GenericNode<T>.INodeNotificationListener
		{


			//-------------------------------------------------
			protected GenericNode<T> _SourceNode = default( GenericNode<T> );
			protected GenericNodeConverter<T, S> _NodeConverter = null;
			private bool _Silent = false;


			//-------------------------------------------------
			public GenericNode<T> SourceNode
			{
				get { return this._SourceNode; }
			}


			//-------------------------------------------------
			public bool Silent
			{
				get { return this._Silent; }
				set { this._Silent = value; }
			}


			//-------------------------------------------------
			public GenericSynchronizedNode( GenericNode<T> SourceNode_in, GenericNodeConverter<T, S> Converter )
			{
				this._SourceNode = SourceNode_in;
				this._NodeConverter = Converter;
				GenericConvertingVisitor<T, S> visitor = new GenericConvertingVisitor<T, S>( this._SourceNode, this._NodeConverter );
				this.Key = this._SourceNode.Key;
				this._NodeConverter.Convert( this._SourceNode, this );
				this._SourceNode.VisitDecendentsDepthFirst( visitor );
				this.SetPrevNode( visitor.TargetRoot.PrevNode );
				this.SetNextNode( visitor.TargetRoot.NextNode );
				this._SourceNode.Listener = this;
				return;
			}


			//-------------------------------------------------
			~GenericSynchronizedNode()
			{
				if( this._SourceNode != null ) { this._SourceNode.Listener = null; }
				this._SourceNode = null;
				return;
			}


			//-------------------------------------------------
			private void _Notify( GenericNode<T>.NodeNotification Notification_in )
			{
				// Ignore pending notifications.
				if( (Notification_in.Status == GenericNode<T>.NodeNotification.NotificationStatus.Pending) )
				{
					return;
				}
				// Process notification.
				if( (Notification_in is GenericNode<T>.NodeUpdateNotification) )
				{
					GenericNode<S> node = this.FindDescNode( Notification_in.Node.Key );
					if( !this._Silent )
					{
						// Propogate the update notification.
						node.NotifyUpdate( GenericNode<S>.NodeNotification.NotificationStatus.Completed );
					}
					this.OnNodeUpdate( node );
				}
				else if( (Notification_in is GenericNode<T>.NodeAddChildNotification) )
				{
					// Synchronize and add the new children.
					GenericNode<T>.NodeAddChildNotification notification = (GenericNode<T>.NodeAddChildNotification)Notification_in;
					GenericNode<S> parentnode = null;
					if( this.Key == notification.Node.Key ) { parentnode = this; }
					else { parentnode = this.FindDescNode( notification.Node.Key ); }
					GenericNode<S> childnode = new GenericNode<S>( notification.Node.Key, default( S ), null );
					childnode.Key = notification.ChildNode.Key;
					this._NodeConverter.Convert( notification.ChildNode, childnode );
					GenericConvertingVisitor<T, S> visitor = new GenericConvertingVisitor<T, S>( notification.ChildNode, this._NodeConverter );
					notification.ChildNode.VisitDecendentsDepthFirst( visitor );
					parentnode.AddChild( childnode, notification.ChildIndex, this._Silent );
					this.OnNodeAddChild( parentnode, childnode );
				}
				else if( (Notification_in is GenericNode<T>.NodeRemoveChildNotification) )
				{
					// Remove the children.
					GenericNode<T>.NodeRemoveChildNotification notification = (GenericNode<T>.NodeRemoveChildNotification)Notification_in;
					GenericNode<S> parentnode = this.FindDescNode( notification.Node.Key );
					GenericNode<S> childnode = null;
					childnode = parentnode.RemoveChild( notification.ChildIndex, this._Silent );
					this.OnRemoveChild( parentnode, childnode );
				}
				else if( (Notification_in is GenericNode<T>.NodeClearChildrenNotification) )
				{
					// Clear the children.
					GenericNode<T>.NodeClearChildrenNotification notification = (GenericNode<T>.NodeClearChildrenNotification)Notification_in;
					GenericNode<S> parentnode = this.FindDescNode( notification.Node.Key );
					parentnode.ClearChildren( this._Silent );
					this.OnClearChildren( parentnode );
				}
				return;
			}


			//-------------------------------------------------
			void GenericNode<T>.INodeNotificationListener.Notify( GenericNode<T>.NodeNotification Notification_in )
			{
				this._Notify( Notification_in );
				return;
			}


			//-------------------------------------------------
			public virtual void OnNodeUpdate( GenericNode<S> Node_in )
			{
				return;
			}


			//-------------------------------------------------
			public virtual void OnNodeAddChild( GenericNode<S> Node_in, GenericNode<S> ChildNode_in )
			{
				return;
			}


			//-------------------------------------------------
			public virtual void OnRemoveChild( GenericNode<S> Node_in, GenericNode<S> ChildNode_in )
			{
				return;
			}


			//-------------------------------------------------
			public virtual void OnClearChildren( GenericNode<S> Node_in )
			{
				return;
			}


		}

	}
}
