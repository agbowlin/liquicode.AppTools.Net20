

using System;
using System.Text;
using System.Xml.Serialization;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture()]
public class SerializableDictionaryTests
{


	//-------------------------------------------------
	[Test()]
	public void Test_010_StringDictionary()
	{
		StringDictionary dictionary = new StringDictionary();
		dictionary[ "abc" ] = "123";
		dictionary[ "def" ] = "456";
		dictionary[ "ghi" ] = "789";

		// Serialize to string.
		StringBuilder sb = new StringBuilder();
		using( System.IO.StringWriter writer = new System.IO.StringWriter( sb ) )
		{
			XmlSerializer serializer = new XmlSerializer( typeof( StringDictionary ) );
			serializer.Serialize( writer, dictionary );
			writer.Flush();
			writer.Close();
		}
		string xml = sb.ToString();

		// Deserialize from string.
		StringDictionary dictionary2 = null;
		using( System.IO.StringReader reader = new System.IO.StringReader( xml ) )
		{
			XmlSerializer serializer = new XmlSerializer( typeof( StringDictionary ) );
			dictionary2 = (StringDictionary)serializer.Deserialize( reader );
		}

		// Test.
		foreach( string key in dictionary.Keys )
		{
			Assert.AreEqual( dictionary[ key ], dictionary2[ key ] );
		}

		return;
	}


}
