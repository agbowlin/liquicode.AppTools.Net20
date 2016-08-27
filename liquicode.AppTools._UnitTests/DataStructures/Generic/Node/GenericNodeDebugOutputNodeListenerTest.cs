

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture]
public class GenericNodeDebugOutputNodeListenerTest
{


	//--------------------------------------------------------------------
	[SetUp]
	protected void SetUp()
	{
	}


	//--------------------------------------------------------------------
	public class DebugOutputNodeListener
		: DataStructures.GenericNode<string>.INodeNotificationListener
	{
		void DataStructures.GenericNode<string>.INodeNotificationListener.Notify( DataStructures.GenericNode<string>.NodeNotification Notification_in )
		{
			string text = "";
			if( Notification_in is DataStructures.GenericNode<string>.NodeUpdateNotification )
			{ text += "NodeUpdateNotification"; }
			else if( Notification_in is DataStructures.GenericNode<string>.NodeClearChildrenNotification )
			{ text += "NodeClearChildrenNotification"; }
			else if( Notification_in is DataStructures.GenericNode<string>.NodeChildNotification )
			{ text += "NodeChildNotification"; }
			else if( Notification_in is DataStructures.GenericNode<string>.NodeAddChildNotification )
			{ text += "NodeAddChildNotification"; }
			else if( Notification_in is DataStructures.GenericNode<string>.NodeRemoveChildNotification )
			{ text += "NodeRemoveChildNotification"; }
			else if( Notification_in is DataStructures.GenericNode<string>.NodeNotification )
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
		DataStructures.GenericNode<string> root = new DataStructures.GenericNode<string>( "", "", new DebugOutputNodeListener() );
		root.Value = "root";
		root.Notify(
			new DataStructures.GenericNode<string>.NodeUpdateNotification(
				DataStructures.GenericNode<string>.NodeNotification.NotificationStatus.Completed
				, root ) );
		GenericNodeTestData.AddTestChildren( root );
		return;
	}


}
