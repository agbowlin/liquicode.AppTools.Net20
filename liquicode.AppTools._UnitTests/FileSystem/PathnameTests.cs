
using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;

using NUnit.Framework;


[TestFixture]
public class PathnameTests
{

	//---------------------------------------------------------------------
	[SetUp]
	public void Setup()
	{
		return;
	}

	//---------------------------------------------------------------------
	[TearDown]
	public void TearDown()
	{
		return;
	}


	//---------------------------------------------------------------------
	private void Validate_Path_Parent_Name( Pathname Pathname, string Path, string Parent, string Name )
	{
		Assert.AreEqual( Path, Pathname.Path, "Validate Path equality." );
		Assert.AreEqual( Parent, Pathname.Parent, "Validate Parent equality." );
		Assert.AreEqual( Name, Pathname.Name, "Validate Name equality." );
		return;
	}


	//---------------------------------------------------------------------
	[Test]
	public void Test_01_Parse_Path_Parent_Name()
	{
		Pathname path = new Pathname();
		this.Validate_Path_Parent_Name( path, "", "", "." );
		Assert.AreEqual( "/.", (string)path, "Validate Pathname equality." );

		path = "Root/Folder/File";
		this.Validate_Path_Parent_Name( path, "Root/Folder", "Folder", "File" );
		Assert.AreEqual( "Root/Folder/File", (string)path, "Validate Pathname equality." );

		return;
	}


	//---------------------------------------------------------------------
	[Test]
	public void Test_02_Constructors()
	{
		// Default Constructor.
		Pathname path1 = new Pathname();
		Pathname path2 = new Pathname();
		this.Validate_Path_Parent_Name( path1, "", "", "." );
		Assert.AreEqual( "/.", (string)path1, "Validate Pathname equality." );
		Assert.AreEqual( (string)path1, (string)path2, "Validate Pathname equality." );

		// Assignment Constructor.
		path1 = "Root/Folder/File";
		this.Validate_Path_Parent_Name( path1, "Root/Folder", "Folder", "File" );
		Assert.AreEqual( "Root/Folder/File", (string)path1, "Validate Pathname equality." );

		// Reference Assignement.
		path2 = path1;
		Assert.AreEqual( (string)path1, (string)path2, "Validate Pathname equality." );
		path2.Name = "New";
		Assert.AreEqual( (string)path1, (string)path2, "Validate Pathname equality." );

		// Value Assignement.
		path2 = (string)path1;
		Assert.AreEqual( (string)path1, (string)path2, "Validate Pathname equality." );
		path2.Name = "New2";
		Assert.AreNotEqual( (string)path1, (string)path2, "Validate Pathname equality." );

		return;
	}


	//---------------------------------------------------------------------
	[Test]
	public void Test_11()
	{
		Pathname path = "//0/1/2\\3/4/5/6/7/8/9x/.";
		Assert.AreEqual( 11, path.Items.Count, "Validate item count." );
		this.Validate_Path_Parent_Name( path, "0/1/2/3/4/5/6/7/8/9x", "9x", "." );

		path.Separator = "\\";
		this.Validate_Path_Parent_Name( path, "0\\1\\2\\3\\4\\5\\6\\7\\8\\9x", "9x", "." );

		path = "//0/1/2\\3/4/5/6/7/8/9x";
		Assert.AreEqual( 10, path.Items.Count, "Validate item count." );
		this.Validate_Path_Parent_Name( path, "0/1/2/3/4/5/6/7/8", "8", "9x" );

		path.Name = "9";
		this.Validate_Path_Parent_Name( path, "0/1/2/3/4/5/6/7/8", "8", "9" );
		Assert.AreEqual( "0/1/2/3/4/5/6/7/8/9", path.ToString(), "Construct pathname string." );
		Assert.That( path.Equals( "0/1/2/3/4/5/6/7/8/9" ), "Compare string." );
		Assert.That( path.Equals( new Pathname( "0/1/2/3/4/5/6/7/8/9" ) ), "Compare Pathname object." );

		return;
	}


	//---------------------------------------------------------------------
	[Test]
	public void Test_12()
	{
		Pathname path = "//0/1/2\\3/4/5/6/7/8/.";
		path = Pathname.Append( "//0/1/2\\3/4/5/6/7/8/.", "/9x/." );
		Assert.AreEqual( 11, path.Items.Count, "Validate item count." );
		this.Validate_Path_Parent_Name( path, "0/1/2/3/4/5/6/7/8/9x", "9x", "." );
		return;
	}


}
