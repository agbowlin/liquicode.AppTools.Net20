

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
			public class NodeNotification
			{

				public enum NotificationStatus
				{
					Pending = 0,
					Completed = 1
				}

				public NotificationStatus Status = NotificationStatus.Pending;
				public Node Node = null;

				public NodeNotification( NotificationStatus Status_in, Node Node_in )
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

				public NodeUpdateNotification( NotificationStatus Status_in, Node Node_in )
					: base( Status_in, Node_in )
				{
				}

			}


			//-------------------------------------------------
			public class NodeClearChildrenNotification : NodeNotification
			{

				public NodeClearChildrenNotification( NotificationStatus Status_in, Node Node_in )
					: base( Status_in, Node_in )
				{
				}

			}


			//-------------------------------------------------
			public class NodeChildNotification : NodeNotification
			{

				public Node ChildNode = null;
				public int ChildIndex = -1;

				public NodeChildNotification( NotificationStatus Status_in, Node Node_in, Node ChildNode_in, int ChildIndex_in )
					: base( Status_in, Node_in )
				{
					this.ChildNode = ChildNode_in;
					this.ChildIndex = ChildIndex_in;
				}

			}


			//-------------------------------------------------
			public class NodeAddChildNotification : NodeChildNotification
			{

				public NodeAddChildNotification( NotificationStatus Status_in, Node Node_in, Node ChildNode_in, int ChildIndex_in )
					: base( Status_in, Node_in, ChildNode_in, ChildIndex_in )
				{
				}

			}


			//-------------------------------------------------
			public class NodeRemoveChildNotification : NodeChildNotification
			{

				public NodeRemoveChildNotification( NotificationStatus Status_in, Node Node_in, Node ChildNode_in, int ChildIndex_in )
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
				// Propogate notification upwards
				Node parent = this.ParentNode;
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


		}

	}
}
