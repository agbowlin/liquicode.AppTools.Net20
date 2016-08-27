

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		public partial class GenericNode<T>
		{


			//-------------------------------------------------
			public interface INodeVisitor
			{
				bool Reset( VisitationType VisitationType_in );
				bool VisitNode( GenericNode<T> Node_in );
			}


			//-------------------------------------------------
			public class CountingVisitor : INodeVisitor
			{

				public int Count = 0;

				public bool Reset( VisitationType VisitationType_in )
				{
					this.Count = 0;
					return true;
				}

				public bool VisitNode( GenericNode<T> Node_in )
				{
					Count += 1;
					return true;
				}

			}


			//-------------------------------------------------
			public class CollectingVisitor : INodeVisitor
			{

				public ArrayList List = null;

				public bool Reset( VisitationType VisitationType_in )
				{
					this.List = new ArrayList();
					return true;
				}

				public bool VisitNode( GenericNode<T> Node_in )
				{
					List.Add( Node_in );
					return true;
				}

			}


			//-------------------------------------------------
			public class FindKeyVisitor : INodeVisitor
			{

				public string FindKey = "";
				public GenericNode<T> FoundNode = null;

				public FindKeyVisitor( string FindKey_in )
				{
					this.FindKey = FindKey_in;
				}

				public bool Reset( VisitationType VisitationType_in )
				{
					this.FoundNode = null;
					return true;
				}

				public bool VisitNode( GenericNode<T> Node_in )
				{
					if( (Node_in.Key == this.FindKey) )
					{
						this.FoundNode = Node_in;
						return false;
					}
					return true;
				}

			}


			//-------------------------------------------------
			public class FindIndexVisitor : INodeVisitor
			{

				public int FindIndex = 0;
				public GenericNode<T> FoundNode = null;
				private int _Count = 0;

				public FindIndexVisitor( int FindIndex_in )
				{
					this.FindIndex = FindIndex_in;
				}

				public bool Reset( VisitationType VisitationType_in )
				{
					this.FoundNode = null;
					this._Count = 0;
					return true;
				}

				public bool VisitNode( GenericNode<T> Node_in )
				{
					if( (this._Count == this.FindIndex) )
					{
						this.FoundNode = Node_in;
						return false;
					}
					this._Count += 1;
					return true;
				}

			}


			//-------------------------------------------------
			public void VisitNodes( INodeVisitor Visitor_in, VisitationType VisitationType_in )
			{
				if( !Visitor_in.Reset( VisitationType_in ) )
				{
					throw new ApplicationException( "Unable to reset this visitor; possibly invalid visitation type for this visitor." );
				}
				if( (VisitationType_in == VisitationType.None) )
				{
				}
				else if( (VisitationType_in == VisitationType.PreviousNodes) )
				{
					GenericNode<T> nodePrev = this.ParentNode;
					while( true )
					{
						if( (nodePrev == null) )
							break;

						if( !Visitor_in.VisitNode( nodePrev ) )
							break;

						nodePrev = nodePrev.ParentNode;
					}
				}
				else if( (VisitationType_in == VisitationType.NextNodes) )
				{
					GenericNode<T> nodeNext = _NextNode;
					while( true )
					{
						if( (nodeNext == null) )
							break;

						if( !Visitor_in.VisitNode( nodeNext ) )
							break;

						nodeNext = nodeNext._NextNode;
					}
				}
				else if( (VisitationType_in == VisitationType.Parents) )
				{
					GenericNode<T> nodePrev = this.ParentNode;
					while( true )
					{
						if( (nodePrev == null) )
							break;

						if( !Visitor_in.VisitNode( nodePrev ) )
							break;

						nodePrev = nodePrev.ParentNode;
					}
				}
				else if( (VisitationType_in == VisitationType.PreviousSiblings) )
				{
					GenericNode<T> nodePrev = this.ParentNode;
					while( true )
					{
						if( (nodePrev == null) )
							break;

						if( (nodePrev._Indent < _Indent) )
							break;

						if( (nodePrev._Indent == _Indent) )
						{
							if( !Visitor_in.VisitNode( nodePrev ) )
								break;

						}
						nodePrev = nodePrev.ParentNode;
					}
				}
				else if( (VisitationType_in == VisitationType.NextSiblings) )
				{
					GenericNode<T> nodeNext = _NextNode;
					while( true )
					{
						if( (nodeNext == null) )
							break;

						if( (nodeNext._Indent < _Indent) )
							break;

						if( (nodeNext._Indent == _Indent) )
						{
							if( !Visitor_in.VisitNode( nodeNext ) )
								break;

						}
						nodeNext = nodeNext._NextNode;
					}
				}
				else if( (VisitationType_in == VisitationType.Children) )
				{
					GenericNode<T> nodeNext = _NextNode;
					while( true )
					{
						if( (nodeNext == null) )
							break;

						if( (nodeNext._Indent <= _Indent) )
							break;

						if( (nodeNext._Indent == (_Indent + 1)) )
						{
							if( !Visitor_in.VisitNode( nodeNext ) )
								break;

						}
						nodeNext = nodeNext._NextNode;
					}
				}
				else if( (VisitationType_in == VisitationType.DecendentsDepthFirst) )
				{
					GenericNode<T> nodeNext = _NextNode;
					while( true )
					{
						if( (nodeNext == null) )
							break;

						if( (nodeNext._Indent <= _Indent) )
							break;

						if( !Visitor_in.VisitNode( nodeNext ) )
							break;

						nodeNext = nodeNext._NextNode;
					}
				}
				else if( (VisitationType_in == VisitationType.DecendentsBreadthFirst) )
				{
					throw new NotImplementedException();
				}
				else
				{
					throw new NotSupportedException();
				}
				return;
			}


			//-------------------------------------------------
			public void VisitPreviousNodes( INodeVisitor Visitor_in )
			{
				this.VisitNodes( Visitor_in, VisitationType.PreviousNodes );
			}
			public void VisitNextNodes( INodeVisitor Visitor_in )
			{
				this.VisitNodes( Visitor_in, VisitationType.NextNodes );
			}
			public void VisitParents( INodeVisitor Visitor_in )
			{
				this.VisitNodes( Visitor_in, VisitationType.Parents );
			}
			public void VisitPreviousSiblings( INodeVisitor Visitor_in )
			{
				this.VisitNodes( Visitor_in, VisitationType.PreviousSiblings );
			}
			public void VisitNextSiblings( INodeVisitor Visitor_in )
			{
				this.VisitNodes( Visitor_in, VisitationType.NextSiblings );
			}
			public void VisitChildren( INodeVisitor Visitor_in )
			{
				this.VisitNodes( Visitor_in, VisitationType.Children );
			}
			public void VisitDecendentsDepthFirst( INodeVisitor Visitor_in )
			{
				this.VisitNodes( Visitor_in, VisitationType.DecendentsDepthFirst );
			}
			public void VisitDecendentsBreadthFirst( INodeVisitor Visitor_in )
			{
				this.VisitNodes( Visitor_in, VisitationType.DecendentsBreadthFirst );
			}


		}

	}
}
