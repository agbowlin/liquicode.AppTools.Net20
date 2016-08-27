using System;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		//-------------------------------------------------
		public enum NodeRelationships
		{
			None = 0,
			PrevNode,
			// D3 = C2.Prev        A1
			NextNode,
			// C3 = C2.Next         +- B1
			FirstNode,
			// A1 = C1.First        |   +- C1
			LastNode,
			// C7 = C1.Last         |   |   +- D1
			RootNode,
			// A1 = C1.Root         |   |   +- D2
			ParentNode,
			// B1 = C1.Parent       |   |   +- D3
			PrevSibNode,
			// C1 = C2.PrevSib      |   +- C2
			NextSibNode,
			// C2 = C1.NextSib      |   +- C3
			FirstSibNode,
			// C1 = C1.FirstSib     +- B2
			LastSibNode,
			// C3 = C1.LastSib      |   +- C4
			FirstChildNode,
			// B1 = A1.FirstChild   |   +- C5
			LastChildNode,
			// B3 = A1.LastChild    |   +- C6
			FirstDescNode,
			// B1 = A1.FirstDesc    +- B3
			LastDescNode
			// C7 = A1.LastDesc         +- C7
		}


		//-------------------------------------------------
		public enum VisitationType
		{
			None = 0,
			PreviousNodes,
			NextNodes,
			Parents,
			PreviousSiblings,
			NextSiblings,
			Children,
			DecendentsDepthFirst,
			DecendentsBreadthFirst
		}


	}
}
