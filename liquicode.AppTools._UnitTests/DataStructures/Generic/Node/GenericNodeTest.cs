

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture]
public class GenericNodeTest
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
		DataStructures.GenericNode<string> root = new DataStructures.GenericNode<string>();
		GenericNodeTestData.AddTestChildren( root );
		Console.WriteLine( root.TextGraph() );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_10_Navigation()
	{
		DataStructures.GenericNode<string> root = new DataStructures.GenericNode<string>();
		GenericNodeTestData.AddTestChildren( root );
		GenericNodeTestData.AssertTestChildren( root );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_20_Clone()
	{
		DataStructures.GenericNode<string> root = new DataStructures.GenericNode<string>();
		GenericNodeTestData.AddTestChildren( root );
		DataStructures.GenericCloningVisitor<string> visitor = new DataStructures.GenericCloningVisitor<string>( root, new DataStructures.GenericNodeCloner<string>() );
		root.VisitNodes( visitor, DataStructures.VisitationType.DecendentsDepthFirst );
		GenericNodeTestData.AssertTestChildren( visitor.TargetRoot );
		return;
	}


}
