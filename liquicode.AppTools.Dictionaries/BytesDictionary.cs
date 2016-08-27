

using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace liquicode.AppTools
{


	[XmlRoot( "BytesDictionary" )]
	public class BytesDictionary
		: Dictionary<string, byte[]>
		, IXmlSerializable
	{


		//---------------------------------------------------------------------
		public System.Xml.Schema.XmlSchema GetSchema()
		{ return null; }


		//---------------------------------------------------------------------
		public void ReadXml( System.Xml.XmlReader reader )
		{
			XmlSerializer KeySerializer = new XmlSerializer( typeof( string ) );
			XmlSerializer ValueSerializer = new XmlSerializer( typeof( byte[] ) );
			bool WasEmpty = reader.IsEmptyElement;
			reader.Read();
			if( WasEmpty ) { return; }
			while( reader.NodeType != System.Xml.XmlNodeType.EndElement )
			{
				reader.ReadStartElement( "item" );

				reader.ReadStartElement( "key" );
				string Key = (string)KeySerializer.Deserialize( reader );
				reader.ReadEndElement();

				reader.ReadStartElement( "value" );
				string string_value = (string)KeySerializer.Deserialize( reader );
				byte[] Value = null;
				if( string_value.Length == 0 )
				{ Value = new byte[] { }; }
				else
				{ Value = Convert.FromBase64String( string_value ); }
				reader.ReadEndElement();

				this.Add( Key, Value );
				reader.ReadEndElement();

				reader.MoveToContent();
			}
			reader.ReadEndElement();
			return;
		}


		//---------------------------------------------------------------------
		public void WriteXml( System.Xml.XmlWriter writer )
		{
			XmlSerializer KeySerializer = new XmlSerializer( typeof( string ) );
			XmlSerializer ValueSerializer = new XmlSerializer( typeof( byte[] ) );
			foreach( string Key in this.Keys )
			{
				writer.WriteStartElement( "item" );

				writer.WriteStartElement( "key" );
				KeySerializer.Serialize( writer, Key );
				writer.WriteEndElement();

				writer.WriteStartElement( "value" );
				byte[] Value = this[ Key ];
				if( Value.Length == 0 )
				{ KeySerializer.Serialize( writer, "" ); }
				else
				{ KeySerializer.Serialize( writer, Convert.ToBase64String( Value ) ); }
				writer.WriteEndElement();

				writer.WriteEndElement();
			}
			return;
		}



	}


}
