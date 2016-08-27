

using System;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture()]
public class ObjectXmlSerializerTests
{


	//-------------------------------------------------
	public class SimpleTestClass
	{
		public string Value1 { get; set; }
	}

	public class SimpleTestClassSerializer : XmlObjectSerializer<SimpleTestClass> { }


	//-------------------------------------------------
	[Test()]
	public void Test_010_SerializeSimpleType()
	{
		XmlObjectSerializer<SimpleTestClass> document = new XmlObjectSerializer<SimpleTestClass>();
		document.Content.Value1 = "Hello World!";
		string xml = document.SaveToXmlString();
		XmlObjectSerializer<SimpleTestClass> document2 = new XmlObjectSerializer<SimpleTestClass>();
		document2.LoadFromXmlString( xml );
		Assert.AreEqual( document.Content.Value1, document2.Content.Value1 );
		return;
	}


	//-------------------------------------------------
	[Test()]
	public void Test_011_SerializeSimpleType_Derived()
	{
		SimpleTestClassSerializer document = new SimpleTestClassSerializer();
		document.Content.Value1 = "Hello World!";
		string xml = document.SaveToXmlString();
		SimpleTestClassSerializer document2 = new SimpleTestClassSerializer();
		document2.LoadFromXmlString( xml );
		Assert.AreEqual( document.Content.Value1, document2.Content.Value1 );
		return;
	}


}
