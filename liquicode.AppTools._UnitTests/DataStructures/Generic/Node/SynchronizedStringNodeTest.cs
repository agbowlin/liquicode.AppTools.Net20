

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture]
public class SynchronizedStringNodeTest
{


	//--------------------------------------------------------------------
	[SetUp]
	protected void SetUp()
	{
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_20_Copy()
	{
		DataStructures.StringNode root = new DataStructures.StringNode();
		GenericNodeTestData.AddTestChildren( root );
		DataStructures.SynchronizedStringNode snode = new DataStructures.SynchronizedStringNode( root );
		GenericNodeTestData.AssertTestChildren( root );
		GenericNodeTestData.AssertTestChildren( snode );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_21_Synchronize()
	{
		// Create the source root node.
		DataStructures.StringNode root = new DataStructures.StringNode();
		// Create the synchronized root node.
		DataStructures.SynchronizedStringNode snode = new DataStructures.SynchronizedStringNode( root );
		// Wire the source node to push notifications to the synchronized nodes.
		root.Listener = snode;
		// Modify the source root node.
		GenericNodeTestData.AddTestChildren( root );
		// Validate the synchronized nodes.
		GenericNodeTestData.AssertTestChildren( root );
		GenericNodeTestData.AssertTestChildren( snode );
		// Return, OK.
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_22_TripleSynchronize()
	{
		// Create the source root node.
		DataStructures.StringNode root = new DataStructures.StringNode();
		// Create the synchronized root nodes.
		DataStructures.SynchronizedStringNode snode1 = new DataStructures.SynchronizedStringNode( root );
		DataStructures.SynchronizedStringNode snode2 = new DataStructures.SynchronizedStringNode( root );
		DataStructures.SynchronizedStringNode snode3 = new DataStructures.SynchronizedStringNode( root );
		// Create a notification broadcaster to push notifications to all synchronized nodes.
		DataStructures.StringNode.NodeNotificationBroadcaster broadcaster = new DataStructures.StringNode.NodeNotificationBroadcaster();
		broadcaster.Listeners.Add( snode1 );
		broadcaster.Listeners.Add( snode2 );
		broadcaster.Listeners.Add( snode3 );
		root.Listener = broadcaster;
		// Modify the source root node.
		GenericNodeTestData.AddTestChildren( root );
		// Validate the synchronized nodes.
		GenericNodeTestData.AssertTestChildren( root );
		GenericNodeTestData.AssertTestChildren( snode1 );
		GenericNodeTestData.AssertTestChildren( snode2 );
		GenericNodeTestData.AssertTestChildren( snode3 );
		// Return, OK.
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_23_JumbledTripleSynchronize()
	{
		// Create the source root node.
		DataStructures.StringNode root = new DataStructures.StringNode();
		// Create the synchronized root nodes.
		DataStructures.SynchronizedStringNode snode1 = new DataStructures.SynchronizedStringNode( root );
		DataStructures.SynchronizedStringNode snode2 = new DataStructures.SynchronizedStringNode( root );
		DataStructures.SynchronizedStringNode snode3 = new DataStructures.SynchronizedStringNode( root );
		// Create a notification broadcaster to push notifications to all synchronized nodes.
		DataStructures.StringNode.NodeNotificationBroadcaster broadcaster = new DataStructures.StringNode.NodeNotificationBroadcaster();
		broadcaster.Listeners.Add( snode1 );
		broadcaster.Listeners.Add( snode2 );
		broadcaster.Listeners.Add( snode3 );
		root.Listener = broadcaster;
		// Modify the source root node.
		GenericNodeTestData.AddTestChildren( root );
		GenericNodeTestData.JumbleTestChildren( root );
		// Validate the synchronized nodes.
		GenericNodeTestData.AssertTestChildren( root );
		GenericNodeTestData.AssertTestChildren( snode1 );
		GenericNodeTestData.AssertTestChildren( snode2 );
		GenericNodeTestData.AssertTestChildren( snode3 );
		// Return, OK.
		return;
	}


}
