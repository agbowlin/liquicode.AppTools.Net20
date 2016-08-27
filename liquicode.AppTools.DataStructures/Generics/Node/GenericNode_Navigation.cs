

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
			public GenericNode<T> RelativeNode( NodeRelationships Relationship_in )
			{
				GenericNode<T> nodePrev = _PrevNode;
				GenericNode<T> nodeNext = _NextNode;
				GenericNode<T> nodeRoot = this;
				GenericNode<T> nodeSib = this;
				GenericNode<T> nodeChild = null;
				while( true )
				{
					if( (Relationship_in == NodeRelationships.None) )
					{
						break;
					}
					else if( (Relationship_in == NodeRelationships.PrevNode) )
					{
						return _PrevNode;
					}
					else if( (Relationship_in == NodeRelationships.NextNode) )
					{
						return _NextNode;
					}
					else if( (Relationship_in == NodeRelationships.FirstNode) )
					{
						if( (nodePrev == null) ) { return this; }
						if( (nodePrev._PrevNode == null) ) { return nodePrev; }
						nodePrev = nodePrev._PrevNode;
					}
					else if( (Relationship_in == NodeRelationships.LastNode) )
					{
						if( (nodeNext == null) )
							return this;
						if( (nodeNext._NextNode == null) )
							return nodeNext;
						nodeNext = nodeNext._NextNode;
					}
					else if( (Relationship_in == NodeRelationships.RootNode) )
					{
						if( (nodePrev == null) )
							return nodeRoot;
						if( (nodePrev._Indent < nodeRoot._Indent) )
							nodeRoot = nodePrev;
						nodePrev = nodePrev._PrevNode;
					}
					else if( (Relationship_in == NodeRelationships.ParentNode) )
					{
						if( (nodePrev == null) )
							break;

						if( (nodePrev._Indent < _Indent) )
							return nodePrev;
						nodePrev = nodePrev._PrevNode;
					}
					else if( (Relationship_in == NodeRelationships.PrevSibNode) )
					{
						if( (nodePrev == null) )
							break;

						if( (nodePrev._Indent < _Indent) )
							break;

						if( (nodePrev._Indent == _Indent) )
							return nodePrev;
						nodePrev = nodePrev._PrevNode;
					}
					else if( (Relationship_in == NodeRelationships.NextSibNode) )
					{
						if( (nodeNext == null) )
							break;

						if( (nodeNext._Indent < _Indent) )
							break;

						if( (nodeNext._Indent == _Indent) )
							return nodeNext;
						nodeNext = nodeNext._NextNode;
					}
					else if( (Relationship_in == NodeRelationships.FirstSibNode) )
					{
						if( (nodePrev == null) )
							return nodeSib;
						if( (nodePrev._Indent < _Indent) )
							return nodeSib;
						if( (nodePrev._Indent == _Indent) )
							nodeSib = nodePrev;
						nodePrev = nodePrev._PrevNode;
					}
					else if( (Relationship_in == NodeRelationships.LastSibNode) )
					{
						if( (nodeNext == null) )
							return nodeSib;
						if( (nodeNext._Indent < _Indent) )
							return nodeSib;
						if( (nodeNext._Indent == _Indent) )
							nodeSib = nodeNext;
						nodeNext = nodeNext._NextNode;
					}
					else if( (Relationship_in == NodeRelationships.FirstChildNode) )
					{
						if( (nodeNext == null) )
							break;

						if( (nodeNext._Indent <= _Indent) )
							break;

						if( (nodeNext._Indent == (_Indent + 1)) )
							return nodeNext;
						nodeNext = nodeNext._NextNode;
					}
					else if( (Relationship_in == NodeRelationships.LastChildNode) )
					{
						if( (nodeNext == null) )
							return nodeChild;
						if( (nodeNext._Indent <= _Indent) )
							return nodeChild;
						if( (nodeNext._Indent == (_Indent + 1)) )
							nodeChild = nodeNext;
						nodeNext = nodeNext._NextNode;
					}
					else if( (Relationship_in == NodeRelationships.FirstDescNode) )
					{
						if( (nodeNext != null) )
						{
							if( (nodeNext._Indent > _Indent) )
								return nodeNext;
						}
						break;
					}
					else if( (Relationship_in == NodeRelationships.LastDescNode) )
					{
						if( (nodeNext == null) )
							return nodeChild;
						if( (nodeNext._Indent <= _Indent) )
							return nodeChild;
						if( (nodeNext._Indent > _Indent) )
							nodeChild = nodeNext;
						nodeNext = nodeNext._NextNode;
					}
					else
					{
						break;
					}
				}
				return null;
			}


			//-------------------------------------------------
			public GenericNode<T> PrevNode
			{
				get { return this._PrevNode; }
			}
			internal void SetPrevNode( GenericNode<T> value )
			{
				this._PrevNode = value;
			}
			public GenericNode<T> NextNode
			{
				get { return this._NextNode; }
			}
			internal void SetNextNode( GenericNode<T> value )
			{
				this._NextNode = value;
			}
			public GenericNode<T> FirstNode
			{
				get { return this.RelativeNode( NodeRelationships.FirstNode ); }
			}
			public GenericNode<T> LastNode
			{
				get { return this.RelativeNode( NodeRelationships.LastNode ); }
			}
			public GenericNode<T> RootNode
			{
				get { return this.RelativeNode( NodeRelationships.RootNode ); }
			}
			public GenericNode<T> ParentNode
			{
				get { return this.RelativeNode( NodeRelationships.ParentNode ); }
			}
			public GenericNode<T> PrevSibNode
			{
				get { return this.RelativeNode( NodeRelationships.PrevSibNode ); }
			}
			public GenericNode<T> NextSibNode
			{
				get { return this.RelativeNode( NodeRelationships.NextSibNode ); }
			}
			public GenericNode<T> FirstSibNode
			{
				get { return this.RelativeNode( NodeRelationships.FirstSibNode ); }
			}
			public GenericNode<T> LastSibNode
			{
				get { return this.RelativeNode( NodeRelationships.LastSibNode ); }
			}
			public GenericNode<T> FirstChildNode
			{
				get { return this.RelativeNode( NodeRelationships.FirstChildNode ); }
			}
			public GenericNode<T> LastChildNode
			{
				get { return this.RelativeNode( NodeRelationships.LastChildNode ); }
			}
			public GenericNode<T> FirstDescNode
			{
				get { return this.RelativeNode( NodeRelationships.FirstDescNode ); }
			}
			public GenericNode<T> LastDescNode
			{
				get { return this.RelativeNode( NodeRelationships.LastDescNode ); }
			}


			//-------------------------------------------------
			public GenericNode<T> ChildNode( int Index_in )
			{
				if( (Index_in < 0) )
					return null;
				FindIndexVisitor visitor = new FindIndexVisitor( Index_in );
				this.VisitChildren( visitor );
				return visitor.FoundNode;
			}


			//-------------------------------------------------
			public GenericNode<T> DescNode( int Index_in )
			{
				if( (Index_in < 0) )
					return null;
				FindIndexVisitor visitor = new FindIndexVisitor( Index_in );
				this.VisitDecendentsDepthFirst( visitor );
				return visitor.FoundNode;
			}


			//-------------------------------------------------
			public int ChildNodeCount
			{
				get
				{
					CountingVisitor visitor = new CountingVisitor();
					this.VisitChildren( visitor );
					return visitor.Count;
				}
			}


			//-------------------------------------------------
			public int DescNodeCount
			{
				get
				{
					CountingVisitor visitor = new CountingVisitor();
					this.VisitDecendentsDepthFirst( visitor );
					return visitor.Count;
				}
			}


			//-------------------------------------------------
			public int ListIndex
			{
				get
				{
					CountingVisitor visitor = new CountingVisitor();
					this.VisitPreviousNodes( visitor );
					return visitor.Count;
				}
			}


			//-------------------------------------------------
			public int ChildIndex
			{
				get
				{
					CountingVisitor visitor = new CountingVisitor();
					this.VisitPreviousSiblings( visitor );
					return visitor.Count;
				}
			}


			//-------------------------------------------------
			public ArrayList ChildNodes()
			{
				CollectingVisitor visitor = new CollectingVisitor();
				this.VisitChildren( visitor );
				return visitor.List;
			}


			//-------------------------------------------------
			public ArrayList DescNodes()
			{
				CollectingVisitor visitor = new CollectingVisitor();
				this.VisitDecendentsDepthFirst( visitor );
				return visitor.List;
			}


		}

	}
}
