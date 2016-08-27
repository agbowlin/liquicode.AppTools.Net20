
using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture]
public class WindowsShellLocal : FileSystemTests
{

	//---------------------------------------------------------------------
	[SetUp]
	public void Setup()
	{
		this._FileSystem = new LocalFileSystem( "C:\\temp" );
		return;
	}

	//---------------------------------------------------------------------
	[TearDown]
	public void TearDown()
	{
		this._FileSystem = null;
		return;
	}

}
