

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;


namespace liquicode.AppTools
{
	public class XmlObjectSerializer<T>
	{


		//=====================================================================
		//		PUBLIC MEMBERS
		//=====================================================================


		//---------------------------------------------------------------------
		public T Content { get; set; }


		//---------------------------------------------------------------------
		public XmlObjectSerializer()
		{
			this.Content = (T)Activator.CreateInstance( typeof( T ) );
			return;
		}


		//---------------------------------------------------------------------
		public XmlObjectSerializer( T ThisContent )
		{
			this.Content = ThisContent;
			return;
		}


		//=====================================================================
		//		DOCUMENT SERIALIZATION
		//=====================================================================


		//---------------------------------------------------------------------
		/// <summary>
		/// Loads the document from an XML file.
		/// </summary>
		/// <param name="Path">The path.</param>
		public void LoadFromXmlFile( string Path )
		{
			this.Content = (T)Activator.CreateInstance( typeof( T ) );
			using( StreamReader reader = File.OpenText( Path ) )
			{
				XmlSerializer serializer = new XmlSerializer( typeof( T ) );
				this.Content = (T)serializer.Deserialize( reader );
			}
			return;
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Loads the document from an XML file.
		/// </summary>
		/// <param name="Path">The path.</param>
		public void CreateOrLoadFromXmlFile( string Path )
		{
			this.Content = (T)Activator.CreateInstance( typeof( T ) );
			if( File.Exists( Path ) == false )
			{
				this.SaveToXmlFile( Path );
			}
			using( StreamReader reader = File.OpenText( Path ) )
			{
				XmlSerializer serializer = new XmlSerializer( typeof( T ) );
				this.Content = (T)serializer.Deserialize( reader );
			}
			return;
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Saves the document to an XML file.
		/// </summary>
		/// <param name="Path">The path.</param>
		public void SaveToXmlFile( string Path )
		{
			using( StreamWriter writer = File.CreateText( Path ) )
			{
				XmlSerializer serializer = new XmlSerializer( typeof( T ) );
				serializer.Serialize( writer, this.Content );
				writer.Flush();
				writer.Close();
			}
			return;
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Loads the document from an XML string.
		/// </summary>
		/// <param name="Xml">A string with xml formatted content.</param>
		public void LoadFromXmlString( string Xml )
		{
			this.Content = default( T );
			using( StringReader reader = new StringReader( Xml ) )
			{
				XmlSerializer serializer = new XmlSerializer( typeof( T ) );
				this.Content = (T)serializer.Deserialize( reader );
			}
			return;
		}


		//---------------------------------------------------------------------
		/// <summary>
		/// Saves the document to an XML string.
		/// </summary>
		/// <returns>A string with xml formatted content.</returns>
		public string SaveToXmlString()
		{
			StringBuilder sb = new StringBuilder();
			using( StringWriter writer = new StringWriter( sb ) )
			{
				XmlSerializer serializer = new XmlSerializer( typeof( T ) );
				serializer.Serialize( writer, this.Content );
				writer.Flush();
				writer.Close();
			}
			return sb.ToString();
		}


	}
}
