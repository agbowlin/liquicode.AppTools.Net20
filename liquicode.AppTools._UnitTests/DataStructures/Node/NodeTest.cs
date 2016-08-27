

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture]
public class NodeTest
{


	//--------------------------------------------------------------------
	[SetUp]
	protected void SetUp()
	{
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_00_TextGraph()
	{
		DataStructures.Node root = new DataStructures.Node();
		NodeTestData.AddTestChildren( root );
		Console.WriteLine( root.TextGraph() );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_10_Navigation()
	{
		DataStructures.Node root = new DataStructures.Node();
		NodeTestData.AddTestChildren( root );
		NodeTestData.AssertTestChildren( root );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_20_Clone()
	{
		DataStructures.Node root = new DataStructures.Node();
		NodeTestData.AddTestChildren( root );
		DataStructures.Node.CloningVisitor visitor = new DataStructures.Node.CloningVisitor( root );
		root.VisitNodes( visitor, DataStructures.VisitationType.DecendentsDepthFirst );
		NodeTestData.AssertTestChildren( visitor.TargetRoot );
		return;
	}


}
