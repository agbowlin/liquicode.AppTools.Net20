﻿

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


internal class NodeTestData
{

	//--------------------------------------------------------------------
	public static void AddTestChildren( DataStructures.Node root )
	{
		// D3 = C2.Prev        A1
		// C3 = C2.Next         +- B1
		// A1 = C1.First        |   +- C1
		// C7 = C1.Last         |   |   +- D1
		// A1 = C1.Root         |   |   +- D2
		// B1 = C1.Parent       |   |   +- D3
		// C1 = C2.PrevSib      |   +- C2
		// C2 = C1.NextSib      |   +- C3
		// C1 = C1.FirstSib     +- B2
		// C3 = C1.LastSib      |   +- C4
		// B1 = A1.FirstChild   |   +- C5
		// B3 = A1.LastChild    |   +- C6
		// B1 = A1.FirstDesc    +- B3
		// C7 = A1.LastDesc         +- C7

		DataStructures.Node A1 = new DataStructures.Node( "A1", "A1", null );
		DataStructures.Node B1 = new DataStructures.Node( "B1", "B1", null );
		DataStructures.Node B2 = new DataStructures.Node( "B2", "B2", null );
		DataStructures.Node B3 = new DataStructures.Node( "B3", "B3", null );
		DataStructures.Node C1 = new DataStructures.Node( "C1", "C1", null );
		DataStructures.Node C2 = new DataStructures.Node( "C2", "C2", null );
		DataStructures.Node C3 = new DataStructures.Node( "C3", "C3", null );
		DataStructures.Node C4 = new DataStructures.Node( "C4", "C4", null );
		DataStructures.Node C5 = new DataStructures.Node( "C5", "C5", null );
		DataStructures.Node C6 = new DataStructures.Node( "C6", "C6", null );
		DataStructures.Node C7 = new DataStructures.Node( "C7", "C7", null );
		DataStructures.Node D1 = new DataStructures.Node( "D1", "D1", null );
		DataStructures.Node D2 = new DataStructures.Node( "D2", "D2", null );
		DataStructures.Node D3 = new DataStructures.Node( "D3", "D3", null );

		root.AddChild( A1, -1, false );
		{
			A1.AddChild( B1, -1, false );
			{
				B1.AddChild( C1, -1, false );
				{
					C1.AddChild( D1, -1, false );
					C1.AddChild( D2, -1, false );
					C1.AddChild( D3, -1, false );
				}
				B1.AddChild( C2, -1, false );
				B1.AddChild( C3, -1, false );
			}
			A1.AddChild( B2, -1, false );
			{
				B2.AddChild( C4, -1, false );
				B2.AddChild( C5, -1, false );
				B2.AddChild( C6, -1, false );
			}
			A1.AddChild( B3, -1, false );
			{
				B3.AddChild( C7, -1, false );
			}
		}

		return;
	}


	//--------------------------------------------------------------------
	public static void JumbleTestChildren( DataStructures.Node root )
	{
		DataStructures.Node child = null;

		child = root.FindDescNode( "C1" );
		child.ClearChildren( false );
		child.AddChild( new DataStructures.Node( "D1", "D1", null ), -1, false );
		child.AddChild( new DataStructures.Node( "D2", "D2", null ), -1, false );
		child.AddChild( new DataStructures.Node( "D3", "D3", null ), -1, false );

		child = root.FindDescNode( "C5" );
		child.ClearChildren( false );

		return;
	}


	//--------------------------------------------------------------------
	public static void AssertTestChildren( DataStructures.Node root )
	{
		Assert.AreEqual( "D3", root.FindDescNode( "C2" ).PrevNode.Value.ToString(), "D3 = C2.Prev" );
		Assert.AreEqual( "C3", root.FindDescNode( "C2" ).NextNode.Value.ToString(), "C3 = C2.Next" );
		Assert.AreEqual( "A1", root.FindDescNode( "C1" ).FirstNode.FirstChildNode.Value.ToString(), "A1 = C1.First.FirstChild" ); // Special case.
		Assert.AreEqual( "C7", root.FindDescNode( "C1" ).LastNode.Value.ToString(), "C7 = C1.Last" );
		Assert.AreEqual( "A1", root.FindDescNode( "C1" ).RootNode.FirstChildNode.Value.ToString(), "A1 = C1.Root.FirstChild" ); // Special case.
		Assert.AreEqual( "B1", root.FindDescNode( "C1" ).ParentNode.Value.ToString(), "B1 = C1.Parent" );
		Assert.AreEqual( "C1", root.FindDescNode( "C2" ).PrevSibNode.Value.ToString(), "C1 = C2.PrevSib" );
		Assert.AreEqual( "C2", root.FindDescNode( "C1" ).NextSibNode.Value.ToString(), "C2 = C1.NextSib" );
		Assert.AreEqual( "C1", root.FindDescNode( "C1" ).FirstSibNode.Value.ToString(), "C1 = C1.FirstSib" );
		Assert.AreEqual( "C3", root.FindDescNode( "C1" ).LastSibNode.Value.ToString(), "C3 = C1.LastSib" );
		Assert.AreEqual( "B1", root.FindDescNode( "A1" ).FirstChildNode.Value.ToString(), "B1 = A1.FirstChild" );
		Assert.AreEqual( "B3", root.FindDescNode( "A1" ).LastChildNode.Value.ToString(), "B3 = A1.LastChild" );
		Assert.AreEqual( "B1", root.FindDescNode( "A1" ).FirstDescNode.Value.ToString(), "B1 = A1.FirstDesc" );
		Assert.AreEqual( "C7", root.FindDescNode( "A1" ).LastDescNode.Value.ToString(), "C7 = A1.LastDesc" );
		return;
	}

}

