

using System;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		//--------------------------------------------------------------------
		public class ObjectNode
			: GenericNode<object>
		{
			public ObjectNode() : base() { return; }
			public ObjectNode( string Key_in, object Value_in, INodeNotificationListener Listener_in ) : base( Key_in, Value_in, Listener_in ) { return; }
		}


		//--------------------------------------------------------------------
		public class StringNode
			: GenericNode<string>
		{
			public StringNode() : base() { return; }
			public StringNode( string Key_in, string Value_in, INodeNotificationListener Listener_in ) : base( Key_in, Value_in, Listener_in ) { return; }
		}


		//-------------------------------------------------
		public class NodeConverter_String2String
			: GenericNodeConverter<string, string>
		{
			public override void Convert( GenericNode<string> SourceNode, GenericNode<string> TargetNode )
			{
				TargetNode.Value = SourceNode.Value;
				return;
			}
		}


		//--------------------------------------------------------------------
		public class SynchronizedStringNode
			: GenericSynchronizedNode<string, string>
		{
			public SynchronizedStringNode( StringNode TargetNode_in )
				: base( TargetNode_in, new NodeConverter_String2String() )
			{ return; }
		}


	}
}
