

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture]
public class NodeCountingVisitorTest
{


	//--------------------------------------------------------------------
	[SetUp]
	protected void SetUp()
	{
	}


	//--------------------------------------------------------------------
	private void _AssertCountingVisitor( DataStructures.Node Node, DataStructures.VisitationType VisitType, int ExpectedCount )
	{
		//Console.WriteLine( "Testing VisitNodes( " + VisitType.ToString() + " )." );
		DataStructures.Node.CountingVisitor visitor = new DataStructures.Node.CountingVisitor();
		Node.VisitNodes( visitor, VisitType );
		Assert.AreEqual( ExpectedCount, visitor.Count, "Visitor count mismatch after VisitNodes( " + VisitType.ToString() + " )." );
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_10_CountingVisitorOnRoot()
	{
		DataStructures.Node root = new DataStructures.Node();
		NodeTestData.AddTestChildren( root );

		this._AssertCountingVisitor( root, DataStructures.VisitationType.None, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.Parents, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.PreviousSiblings, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.NextSiblings, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.PreviousNodes, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.NextNodes, 14 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.Children, 1 );
		// Not Implemented: this._AssertCountingVisitor( root, DataStructures.VisitationType.DecendentsBreadthFirst, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.DecendentsDepthFirst, 14 );

		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_11_CountingVisitorOnA1()
	{
		DataStructures.Node root = new DataStructures.Node();
		NodeTestData.AddTestChildren( root );
		root = root.FindDescNode( "A1" );

		this._AssertCountingVisitor( root, DataStructures.VisitationType.None, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.Parents, 1 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.PreviousSiblings, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.NextSiblings, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.PreviousNodes, 1 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.NextNodes, 13 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.Children, 3 );
		// Not Implemented: this._AssertCountingVisitor( root, DataStructures.VisitationType.DecendentsBreadthFirst, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.DecendentsDepthFirst, 13 );

		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_12_CountingVisitorOnC1()
	{
		DataStructures.Node root = new DataStructures.Node();
		NodeTestData.AddTestChildren( root );
		root = root.FindDescNode( "C1" );

		this._AssertCountingVisitor( root, DataStructures.VisitationType.None, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.Parents, 3 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.PreviousSiblings, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.NextSiblings, 2 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.PreviousNodes, 3 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.NextNodes, 11 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.Children, 3 );
		// Not Implemented: this._AssertCountingVisitor( root, DataStructures.VisitationType.DecendentsBreadthFirst, 0 );
		this._AssertCountingVisitor( root, DataStructures.VisitationType.DecendentsDepthFirst, 3 );

		return;
	}


}
