

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture]
public class TypedNodeTest
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
		DataStructures.StringNode root = new DataStructures.StringNode();
		GenericNodeTestData.AddTestChildren( root );
		Console.WriteLine( root.TextGraph() );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_10_Navigation()
	{
		DataStructures.StringNode root = new DataStructures.StringNode();
		GenericNodeTestData.AddTestChildren( root );
		GenericNodeTestData.AssertTestChildren( root );
		return;
	}


}
