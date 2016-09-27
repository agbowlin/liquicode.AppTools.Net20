

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		public partial class GenericNode<T>
		{


			//-------------------------------------------------
			public class NodeNotification
			{

				public enum NotificationStatus
				{
					Pending = 0,
					Completed = 1
				}

				public NotificationStatus Status = NotificationStatus.Pending;
				public GenericNode<T> Node = null;

				public NodeNotification( NotificationStatus Status_in, GenericNode<T> Node_in )
				{
					this.Status = Status_in;
					this.Node = Node_in;
				}

			}


			//-------------------------------------------------
			public interface INodeNotificationListener
			{
				void Notify( NodeNotification Notification_in );
			}


			//-------------------------------------------------
			public class NodeUpdateNotification : NodeNotification
			{

				public NodeUpdateNotification( NotificationStatus Status_in, GenericNode<T> Node_in )
					: base( Status_in, Node_in )
				{
				}

			}


			//-------------------------------------------------
			public class NodeClearChildrenNotification : NodeNotification
			{

				public NodeClearChildrenNotification( NotificationStatus Status_in, GenericNode<T> Node_in )
					: base( Status_in, Node_in )
				{
				}

			}


			//-------------------------------------------------
			public class NodeChildNotification : NodeNotification
			{

				public GenericNode<T> ChildNode = null;
				public int ChildIndex = -1;

				public NodeChildNotification( NotificationStatus Status_in, GenericNode<T> Node_in, GenericNode<T> ChildNode_in, int ChildIndex_in )
					: base( Status_in, Node_in )
				{
					this.ChildNode = ChildNode_in;
					this.ChildIndex = ChildIndex_in;
				}

			}


			//-------------------------------------------------
			public class NodeAddChildNotification : NodeChildNotification
			{

				public NodeAddChildNotification( NotificationStatus Status_in, GenericNode<T> Node_in, GenericNode<T> ChildNode_in, int ChildIndex_in )
					: base( Status_in, Node_in, ChildNode_in, ChildIndex_in )
				{
				}

			}


			//-------------------------------------------------
			public class NodeRemoveChildNotification : NodeChildNotification
			{

				public NodeRemoveChildNotification( NotificationStatus Status_in, GenericNode<T> Node_in, GenericNode<T> ChildNode_in, int ChildIndex_in )
					: base( Status_in, Node_in, ChildNode_in, ChildIndex_in )
				{
				}

			}


			//-------------------------------------------------
			public class NodeNotificationBroadcaster : NodeNotification, INodeNotificationListener
			{

				public ArrayList Listeners = new ArrayList();

				public NodeNotificationBroadcaster()
					: base( NotificationStatus.Pending, null )
				{
				}

				public void Notify( NodeNotification Notification_in )
				{
					foreach( INodeNotificationListener listener in this.Listeners )
					{
						listener.Notify( Notification_in );
					}
					return;
				}

			}


			//-------------------------------------------------
			public void Notify( NodeNotification Notification_in )
			{
				// Notify on this node.
				if( (this._Listener != null) )
				{
					this._Listener.Notify( Notification_in );
					return;
				}

				if( Notification_in is NodeUpdateNotification )
				{
					this.Fire_NodeUpdate( new NodeUpdate_EventArgs( (NodeUpdateNotification)Notification_in ) );
				}
				else if( Notification_in is NodeClearChildrenNotification )
				{
					this.Fire_NodeClearChildren( new NodeClearChildren_EventArgs( (NodeClearChildrenNotification)Notification_in ) );
				}
				else if( Notification_in is NodeAddChildNotification )
				{
					this.Fire_NodeAddChild( new NodeAddChild_EventArgs( (NodeAddChildNotification)Notification_in ) );
				}
				else if( Notification_in is NodeRemoveChildNotification )
				{
					this.Fire_NodeRemoveChild( new NodeRemoveChild_EventArgs( (NodeRemoveChildNotification)Notification_in ) );
				}

				// Propogate notification upwards
				GenericNode<T> parent = this.ParentNode;
				if( (parent != null) )
				{
					parent.Notify( Notification_in );
				}
				return;
			}


			//-------------------------------------------------
			public void NotifyUpdate( NodeNotification.NotificationStatus Status_in )
			{
				this.Notify( new NodeNotification( Status_in, this ) );
			}


			//=====================================================================
			public class NodeUpdate_EventArgs : EventArgs
			{
				public NodeUpdateNotification Notification = null;
				public NodeUpdate_EventArgs( NodeUpdateNotification Notification )
				{
					this.Notification = Notification;
					return;
				}
			}
			public delegate void NodeUpdate_EventHandler( object sender, NodeUpdate_EventArgs e );
			public event NodeUpdate_EventHandler Event_NodeUpdate = null;
			public virtual void Fire_NodeUpdate( NodeUpdate_EventArgs e )
			{
				if( this.Event_NodeUpdate == null ) { return; }
				this.Event_NodeUpdate( this, e );
				return;
			}


			//=====================================================================
			public class NodeClearChildren_EventArgs : EventArgs
			{
				public NodeClearChildrenNotification Notification = null;
				public NodeClearChildren_EventArgs( NodeClearChildrenNotification Notification )
				{
					this.Notification = Notification;
					return;
				}
			}
			public delegate void NodeClearChildren_EventHandler( object sender, NodeClearChildren_EventArgs e );
			public event NodeClearChildren_EventHandler Event_NodeClearChildren = null;
			public virtual void Fire_NodeClearChildren( NodeClearChildren_EventArgs e )
			{
				if( this.Event_NodeClearChildren == null ) { return; }
				this.Event_NodeClearChildren( this, e );
				return;
			}


			//=====================================================================
			public class NodeAddChild_EventArgs : EventArgs
			{
				public NodeAddChildNotification Notification = null;
				public NodeAddChild_EventArgs( NodeAddChildNotification Notification )
				{
					this.Notification = Notification;
					return;
				}
			}
			public delegate void NodeAddChild_EventHandler( object sender, NodeAddChild_EventArgs e );
			public event NodeAddChild_EventHandler Event_NodeAddChild = null;
			public virtual void Fire_NodeAddChild( NodeAddChild_EventArgs e )
			{
				if( this.Event_NodeAddChild == null ) { return; }
				this.Event_NodeAddChild( this, e );
				return;
			}


			//=====================================================================
			public class NodeRemoveChild_EventArgs : EventArgs
			{
				public NodeRemoveChildNotification Notification = null;
				public NodeRemoveChild_EventArgs( NodeRemoveChildNotification Notification )
				{
					this.Notification = Notification;
					return;
				}
			}
			public delegate void NodeRemoveChild_EventHandler( object sender, NodeRemoveChild_EventArgs e );
			public event NodeRemoveChild_EventHandler Event_NodeRemoveChild = null;
			public virtual void Fire_NodeRemoveChild( NodeRemoveChild_EventArgs e )
			{
				if( this.Event_NodeRemoveChild == null ) { return; }
				this.Event_NodeRemoveChild( this, e );
				return;
			}


		}

	}
}
