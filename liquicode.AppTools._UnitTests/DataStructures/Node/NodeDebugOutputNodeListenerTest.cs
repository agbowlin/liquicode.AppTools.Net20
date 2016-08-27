

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture]
public class NodeDebugOutputNodeListenerTest
{


	//--------------------------------------------------------------------
	[SetUp]
	protected void SetUp()
	{
	}


	//--------------------------------------------------------------------
	public class DebugOutputNodeListener
		: DataStructures.Node.INodeNotificationListener
	{
		void DataStructures.Node.INodeNotificationListener.Notify( DataStructures.Node.NodeNotification Notification_in )
		{
			string text = "";
			if( Notification_in is DataStructures.Node.NodeUpdateNotification )
			{ text += "NodeUpdateNotification"; }
			else if( Notification_in is DataStructures.Node.NodeClearChildrenNotification )
			{ text += "NodeClearChildrenNotification"; }
			else if( Notification_in is DataStructures.Node.NodeChildNotification )
			{ text += "NodeChildNotification"; }
			else if( Notification_in is DataStructures.Node.NodeAddChildNotification )
			{ text += "NodeAddChildNotification"; }
			else if( Notification_in is DataStructures.Node.NodeRemoveChildNotification )
			{ text += "NodeRemoveChildNotification"; }
			else if( Notification_in is DataStructures.Node.NodeNotification )
			{ text += "NodeNotification"; }
			text += "; " + Notification_in.Status;
			text += "; " + Notification_in.Node.Value;
			System.Diagnostics.Debug.Print( text );
			return;
		}
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_00_DebugOutput()
	{
		DataStructures.Node root = new DataStructures.Node( "", "", new DebugOutputNodeListener() );
		root.Value = "root";
		root.Notify(
			new DataStructures.Node.NodeUpdateNotification(
				DataStructures.Node.NodeNotification.NotificationStatus.Completed
				, root ) );
		NodeTestData.AddTestChildren( root );
		return;
	}


}
