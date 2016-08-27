

using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace liquicode.AppTools
{


	[XmlRoot( "StringDictionary" )]
	public class StringDictionary
		: Dictionary<string, string>
		, IXmlSerializable
	{


		//---------------------------------------------------------------------
		public System.Xml.Schema.XmlSchema GetSchema()
		{ return null; }


		//---------------------------------------------------------------------
		public void ReadXml( System.Xml.XmlReader reader )
		{
			XmlSerializer KeySerializer = new XmlSerializer( typeof( string ) );
			XmlSerializer ValueSerializer = new XmlSerializer( typeof( string ) );
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
				string Value = (string)KeySerializer.Deserialize( reader );
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
			XmlSerializer ValueSerializer = new XmlSerializer( typeof( string ) );
			foreach( string Key in this.Keys )
			{
				writer.WriteStartElement( "item" );

				writer.WriteStartElement( "key" );
				KeySerializer.Serialize( writer, Key );
				writer.WriteEndElement();

				writer.WriteStartElement( "value" );
				string Value = this[ Key ];
				KeySerializer.Serialize( writer, Value );
				writer.WriteEndElement();

				writer.WriteEndElement();
			}
			return;
		}



	}


}
