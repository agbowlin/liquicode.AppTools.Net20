

using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace liquicode.AppTools
{


		[XmlRoot( "dictionary" )]
		public class SerializableDictionary<TKey, TValue>
			: Dictionary<TKey, TValue>
			, IXmlSerializable
		{


			//---------------------------------------------------------------------
			public System.Xml.Schema.XmlSchema GetSchema()
			{ return null; }


			//---------------------------------------------------------------------
			public void ReadXml( System.Xml.XmlReader reader )
			{
				XmlSerializer KeySerializer = new XmlSerializer( typeof( TKey ) );
				XmlSerializer ValueSerializer = new XmlSerializer( typeof( TValue ) );
				bool WasEmpty = reader.IsEmptyElement;
				reader.Read();
				if( WasEmpty ) { return; }
				while( reader.NodeType != System.Xml.XmlNodeType.EndElement )
				{
					reader.ReadStartElement( "item" );

					reader.ReadStartElement( "key" );
					TKey Key = (TKey)KeySerializer.Deserialize( reader );
					reader.ReadEndElement();

					reader.ReadStartElement( "value" );
					TValue Value = (TValue)ValueSerializer.Deserialize( reader );
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
				XmlSerializer KeySerializer = new XmlSerializer( typeof( TKey ) );
				XmlSerializer ValueSerializer = new XmlSerializer( typeof( TValue ) );
				foreach( TKey Key in this.Keys )
				{
					writer.WriteStartElement( "item" );

					writer.WriteStartElement( "key" );
					KeySerializer.Serialize( writer, Key );
					writer.WriteEndElement();

					writer.WriteStartElement( "value" );
					TValue Value = this[ Key ];
					ValueSerializer.Serialize( writer, Value );
					writer.WriteEndElement();

					writer.WriteEndElement();
				}
				return;
			}



		}


}
