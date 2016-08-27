

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

using liquicode.AppTools;

using NUnit.Framework;


public class FileSystemTests
{

	//---------------------------------------------------------------------
	protected FileSystemProvider _FileSystem = new LocalFileSystem( Path.GetTempPath() );


	//---------------------------------------------------------------------
	protected string GetConfigValue( string ConfigPathname, string ValueName )
	{
		foreach( string fileline in File.ReadAllLines( ConfigPathname ) )
		{
			string line = fileline.Trim();
			if( !line.StartsWith( ValueName, StringComparison.InvariantCultureIgnoreCase ) ) { continue; }
			line = line.Substring( ValueName.Length ).Trim();
			if( !line.StartsWith( "=" ) ) { continue; }
			line = line.Substring( 1 ).Trim();
			return line;
		}
		return "";
	}

	//---------------------------------------------------------------------
	private string FitString( string Text, int size )
	{
		if( Text.Length > size ) { Text = Text.Substring( 0, size ); }
		if( Text.Length < size ) { Text = Text.PadRight( size ); }
		return Text;
	}

	//---------------------------------------------------------------------
	protected void WriteItemSummary( FileSystemItem item )
	{
		string s = "";

		s = " --- ";
		s += item.Size.ToString();
		s = this.FitString( s, 25 );

		if( item.DateLastWrite.HasValue ) { s += item.DateLastWrite; }
		s = this.FitString( s, 50 );

		if( item.DateLastRead.HasValue ) { s += item.DateLastRead; }
		s = this.FitString( s, 75 );

		if( item.DateCreated.HasValue ) { s += item.DateCreated; }
		s = this.FitString( s, 100 );

		Console.Out.WriteLine( s );

		if( item.IsLink )
		{ Console.Out.WriteLine( "  -> " + item.LinkTarget ); }

		s = item.Pathname;
		if( item.IsFolder ) { s = "[" + s + "]"; }
		if( item.IsLink ) { s = "<" + s + ">"; }
		Console.Out.WriteLine( s );

		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T01_Settings()
	{
		Console.Out.WriteLine( "Settings." );
		Hashtable settings = this._FileSystem.Settings();
		foreach( string sKey in settings.Keys )
		{
			string sValue = settings[ sKey ].ToString();
			Console.WriteLine( sKey + " = " + sValue );
		}
		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T11_ListFolders()
	{
		Commands.ListEntriesCommand ListCommand = new Commands.ListEntriesCommand();
		ListCommand.in_Pathname = "";
		ListCommand.in_IncludeFolders = true;
		ListCommand.in_IncludeLinks = true;
		this._FileSystem.List( ListCommand );

		Console.Out.WriteLine( "ListFolders for '" + ListCommand.in_Pathname + "' found " + ListCommand.out_ItemList.Count.ToString() + " item(s)." );
		foreach( FileSystemItem item in ListCommand.out_ItemList )
		{ this.WriteItemSummary( item ); }

		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T12_ListFolderItems()
	{
		Commands.ListEntriesCommand ListCommand = new Commands.ListEntriesCommand();
		ListCommand.in_Pathname = "";
		ListCommand.in_IncludeFolders = true;
		ListCommand.in_IncludeLinks = true;
		this._FileSystem.List( ListCommand );

		Console.Out.WriteLine( "ListFolders2 for '" + ListCommand.in_Pathname + "' found " + ListCommand.out_ItemList.Count.ToString() + " item(s)." );
		foreach( FileSystemItem item in ListCommand.out_ItemList )
		{
			Commands.ReadItemCommand ReadCommand = new Commands.ReadItemCommand();
			ReadCommand.in_Pathname = item.Pathname;
			this._FileSystem.Read( ReadCommand );
			this.WriteItemSummary( ReadCommand.out_Item );
		}

		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T13_CreateFolder()
	{
		Commands.CreateItemCommand CreateCommand = new Commands.CreateItemCommand();
		CreateCommand.in_Pathname = "TestFolder1";
		CreateCommand.in_IsFolder = true;
		CreateCommand.in_CreatePath = true;
		this._FileSystem.Create( CreateCommand );
		Console.Out.WriteLine( "Created '" + CreateCommand.out_Item.Pathname + "'." );
		this.WriteItemSummary( CreateCommand.out_Item );
		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T14_ReadFolder()
	{
		Commands.ReadItemCommand ReadCommand = new Commands.ReadItemCommand();
		ReadCommand.in_Pathname = "TestFolder1";
		this._FileSystem.Read( ReadCommand );
		Console.Out.WriteLine( "Read '" + ReadCommand.out_Item.Pathname + "'." );
		this.WriteItemSummary( ReadCommand.out_Item );
		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T15_UpdateFolder()
	{
		Commands.ReadItemCommand ReadCommand = new Commands.ReadItemCommand();
		ReadCommand.in_Pathname = "TestFolder1";
		this._FileSystem.Read( ReadCommand );
		if( ReadCommand.out_Item == null ) { throw new Exception( "Folder '" + ReadCommand.in_Pathname + "' does not exist.'" ); }

		Commands.UpdateItemCommand UpdateCommand = new Commands.UpdateItemCommand();
		UpdateCommand.in_Pathname = ReadCommand.out_Item.Pathname;
		UpdateCommand.in_Item = ReadCommand.out_Item;
		UpdateCommand.in_Item.Name = "RenamedTestFolder1";
		this._FileSystem.Update( UpdateCommand );

		UpdateCommand.in_Pathname = "RenamedTestFolder1";
		UpdateCommand.in_Item = UpdateCommand.out_Item;
		UpdateCommand.in_Item.Name = "TestFolder1";
		this._FileSystem.Update( UpdateCommand );

		Console.Out.WriteLine( "Updated '" + UpdateCommand.out_Item.Pathname + "'." );
		this.WriteItemSummary( UpdateCommand.out_Item );
		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T16_DeleteFolder()
	{
		Commands.DeleteItemCommand DeleteCommand = new Commands.DeleteItemCommand();
		DeleteCommand.in_Pathname = "TestFolder1";
		DeleteCommand.in_IsFolder = true;
		this._FileSystem.Delete( DeleteCommand );
		Console.Out.WriteLine( "Deleted '" + DeleteCommand.in_Pathname + "'." );
		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T17_FolderCRUD()
	{
		Commands.CreateItemCommand CreateCommand = new Commands.CreateItemCommand();
		CreateCommand.in_Pathname = "TestFolder1";
		CreateCommand.in_IsFolder = true;
		CreateCommand.in_CreatePath = true;
		this._FileSystem.Create( CreateCommand );
		Console.Out.WriteLine( "Created '" + CreateCommand.out_Item.Pathname + "'." );
		this.WriteItemSummary( CreateCommand.out_Item );

		Commands.ReadItemCommand ReadCommand = new Commands.ReadItemCommand();
		ReadCommand.in_Pathname = CreateCommand.out_Item.Pathname;
		this._FileSystem.Read( ReadCommand );
		Console.Out.WriteLine( "Read '" + ReadCommand.out_Item.Pathname + "'." );
		this.WriteItemSummary( ReadCommand.out_Item );

		Commands.UpdateItemCommand UpdateCommand = new Commands.UpdateItemCommand();
		UpdateCommand.in_Pathname = ReadCommand.out_Item.Pathname;
		UpdateCommand.in_Item = ReadCommand.out_Item;
		UpdateCommand.in_Item.Name = "RenamedTestFolder1";
		this._FileSystem.Update( UpdateCommand );
		Console.Out.WriteLine( "Updated '" + UpdateCommand.out_Item.Pathname + "'." );
		this.WriteItemSummary( UpdateCommand.out_Item );

		Commands.DeleteItemCommand DeleteCommand = new Commands.DeleteItemCommand();
		DeleteCommand.in_Pathname = UpdateCommand.out_Item.Pathname;
		DeleteCommand.in_IsFolder = true;
		this._FileSystem.Delete( DeleteCommand );
		Console.Out.WriteLine( "Deleted '" + DeleteCommand.in_Pathname + "'." );

		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T21_ListFiles()
	{
		Commands.ListEntriesCommand ListCommand = new Commands.ListEntriesCommand();
		ListCommand.in_Pathname = "";
		ListCommand.in_IncludeFiles = true;
		this._FileSystem.List( ListCommand );

		Console.Out.WriteLine( "ListFiles for '" + ListCommand.in_Pathname + "' found " + ListCommand.out_ItemList.Count.ToString() + " item(s)." );
		foreach( FileSystemItem item in ListCommand.out_ItemList )
		{ this.WriteItemSummary( item ); }

		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T22_ListFileItems()
	{
		Commands.ListEntriesCommand ListCommand = new Commands.ListEntriesCommand();
		ListCommand.in_Pathname = "";
		ListCommand.in_IncludeFiles = true;
		this._FileSystem.List( ListCommand );

		Console.Out.WriteLine( "ListFiles2 for '" + ListCommand.in_Pathname + "' found " + ListCommand.out_ItemList.Count.ToString() + " item(s)." );
		foreach( FileSystemItem item in ListCommand.out_ItemList )
		{
			Commands.ReadItemCommand ReadCommand = new Commands.ReadItemCommand();
			ReadCommand.in_Pathname = item.Pathname;
			this._FileSystem.Read( ReadCommand );
			this.WriteItemSummary( ReadCommand.out_Item );
		}

		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T23_CreateFile()
	{
		Commands.CreateItemCommand CreateCommand = new Commands.CreateItemCommand();
		CreateCommand.in_Pathname = "TestFile1";
		CreateCommand.in_IsFolder = false;
		CreateCommand.in_CreatePath = true;
		this._FileSystem.Create( CreateCommand );
		Console.Out.WriteLine( "Created '" + CreateCommand.out_Item.Pathname + "'." );
		this.WriteItemSummary( CreateCommand.out_Item );
		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T24_ReadFile()
	{
		Commands.ReadItemCommand ReadCommand = new Commands.ReadItemCommand();
		ReadCommand.in_Pathname = "TestFile1";
		this._FileSystem.Read( ReadCommand );
		Console.Out.WriteLine( "Read '" + ReadCommand.out_Item.Pathname + "'." );
		this.WriteItemSummary( ReadCommand.out_Item );
		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T25_UpdateFile()
	{
		Commands.ReadItemCommand ReadCommand = new Commands.ReadItemCommand();
		ReadCommand.in_Pathname = "TestFile1";
		this._FileSystem.Read( ReadCommand );
		if( ReadCommand.out_Item == null ) { throw new Exception( "Folder '" + ReadCommand.in_Pathname + "' does not exist.'" ); }

		Commands.UpdateItemCommand UpdateCommand = new Commands.UpdateItemCommand();
		UpdateCommand.in_Pathname = ReadCommand.out_Item.Pathname;
		UpdateCommand.in_Item = ReadCommand.out_Item;
		UpdateCommand.in_Item.Name = "RenamedTestFile1";
		this._FileSystem.Update( UpdateCommand );

		UpdateCommand.in_Pathname = "RenamedTestFile1";
		UpdateCommand.in_Item = UpdateCommand.out_Item;
		UpdateCommand.in_Item.Name = "TestFile1";
		this._FileSystem.Update( UpdateCommand );

		Console.Out.WriteLine( "Updated '" + UpdateCommand.out_Item.Pathname + "'." );
		this.WriteItemSummary( UpdateCommand.out_Item );
		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T26_DeleteFile()
	{
		Commands.DeleteItemCommand DeleteCommand = new Commands.DeleteItemCommand();
		DeleteCommand.in_Pathname = "TestFile1";
		DeleteCommand.in_IsFolder = false;
		this._FileSystem.Delete( DeleteCommand );
		Console.Out.WriteLine( "Deleted '" + DeleteCommand.in_Pathname + "'." );
		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T27_FileCRUD()
	{
		Commands.CreateItemCommand CreateCommand = new Commands.CreateItemCommand();
		CreateCommand.in_Pathname = "TestFile1";
		CreateCommand.in_IsFolder = false;
		CreateCommand.in_CreatePath = true;
		this._FileSystem.Create( CreateCommand );
		Console.Out.WriteLine( "Created '" + CreateCommand.out_Item.Pathname + "'." );
		this.WriteItemSummary( CreateCommand.out_Item );

		Commands.ReadItemCommand ReadCommand = new Commands.ReadItemCommand();
		ReadCommand.in_Pathname = CreateCommand.out_Item.Pathname;
		this._FileSystem.Read( ReadCommand );
		Console.Out.WriteLine( "Read '" + ReadCommand.out_Item.Pathname + "'." );
		this.WriteItemSummary( ReadCommand.out_Item );

		Commands.UpdateItemCommand UpdateCommand = new Commands.UpdateItemCommand();
		UpdateCommand.in_Pathname = ReadCommand.out_Item.Pathname;
		UpdateCommand.in_Item = ReadCommand.out_Item;
		UpdateCommand.in_Item.Name = "RenamedTestFile1";
		this._FileSystem.Update( UpdateCommand );
		Console.Out.WriteLine( "Updated '" + UpdateCommand.out_Item.Pathname + "'." );
		this.WriteItemSummary( UpdateCommand.out_Item );

		Commands.DeleteItemCommand DeleteCommand = new Commands.DeleteItemCommand();
		DeleteCommand.in_Pathname = UpdateCommand.out_Item.Pathname;
		DeleteCommand.in_IsFolder = false;
		this._FileSystem.Delete( DeleteCommand );
		Console.Out.WriteLine( "Deleted '" + DeleteCommand.in_Pathname + "'." );

		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T31_Compare()
	{
		FileSystemItem item1 = this._FileSystem.Create( "TestFile1", false, true );
		FileSystemItem item2 = this._FileSystem.Create( "TestFile2", false, true );
		ComparisonResult comparison = ComparisonResult.Equal;

		comparison = FileSystemItem.CompareItems( item1, item2, FileSystemFields.FieldsDateLastWrite );
		//Assert.AreEqual( ComparisonResult.Item1IsLesser, comparison, "Item1 is older than Item2." );
		Console.Out.WriteLine( "Comparison = " + comparison.ToString() + "." );

		Console.Out.WriteLine( "item1.DateLastWrite = item2.DateLastWrite;" );
		item1.DateLastWrite = item2.DateLastWrite;

		item1 = this._FileSystem.Update( item1.Pathname, item1.IsFolder, item1 );
		comparison = FileSystemItem.CompareItems( item1, item2, FileSystemFields.FieldsDateLastWrite );
		//Assert.AreEqual( ComparisonResult.Equal, comparison, "Item1 and Item2 are equal." );
		Console.Out.WriteLine( "Comparison = " + comparison.ToString() + "." );

		this._FileSystem.Delete( "TestFile1", false );
		this._FileSystem.Delete( "TestFile2", false );

		return;
	}

}
