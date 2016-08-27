
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace liquicode.AppTools
{

	[Serializable]
	public abstract class CommandContext
	{
	}


	public class Commands
	{

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class SettingsCommand : CommandContext
		{

			//---------------------------------------------------------------------
			private Hashtable _out_Settings = new Hashtable();
			public Hashtable out_Settings
			{
				get { return this._out_Settings; }
				set { this._out_Settings = value; }
			}

			//---------------------------------------------------------------------
			public SettingsCommand()
			{ return; }

		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class ListEntriesCommand : CommandContext
		{

			//---------------------------------------------------------------------
			private string _in_Pathname = "";
			public string in_Pathname
			{
				get { return this._in_Pathname; }
				set { this._in_Pathname = value; }
			}

			////---------------------------------------------------------------------
			//private string _in_Pattern = "";
			//public string in_Pattern
			//{
			//    get { return this._in_Pattern; }
			//    set { this._in_Pattern = value; }
			//}

			//---------------------------------------------------------------------
			private bool _in_IncludeFolders = false;
			public bool in_IncludeFolders
			{
				get { return this._in_IncludeFolders; }
				set { this._in_IncludeFolders = value; }
			}

			//---------------------------------------------------------------------
			private bool _in_IncludeLinks = false;
			public bool in_IncludeLinks
			{
				get { return this._in_IncludeLinks; }
				set { this._in_IncludeLinks = value; }
			}

			//---------------------------------------------------------------------
			private bool _in_IncludeFiles = false;
			public bool in_IncludeFiles
			{
				get { return this._in_IncludeFiles; }
				set { this._in_IncludeFiles = value; }
			}

			////---------------------------------------------------------------------
			//private bool _in_IncludeSubfolders = false;
			//public bool in_IncludeSubfolders
			//{
			//    get { return this._in_IncludeSubfolders; }
			//    set { this._in_IncludeSubfolders = value; }
			//}

			//---------------------------------------------------------------------
			private FileSystemItemList _out_ItemList = null;
			public FileSystemItemList out_ItemList
			{
				get { return this._out_ItemList; }
				set { this._out_ItemList = value; }
			}

			//---------------------------------------------------------------------
			public ListEntriesCommand()
			{ return; }

			//---------------------------------------------------------------------
			public ListEntriesCommand( string Pathname, bool IncludeFolders, bool IncludeLinks, bool IncludeFiles )
			{
				this._in_Pathname = Pathname;
				this._in_IncludeFolders = IncludeFolders;
				this._in_IncludeLinks = IncludeLinks;
				this._in_IncludeFiles = IncludeFiles;
				return;
			}

		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class CreateItemCommand : CommandContext
		{

			//---------------------------------------------------------------------
			private string _in_Pathname = "";
			public string in_Pathname
			{
				get { return this._in_Pathname; }
				set { this._in_Pathname = value; }
			}

			//---------------------------------------------------------------------
			private bool _in_IsFolder = false;
			public bool in_IsFolder
			{
				get { return this._in_IsFolder; }
				set { this._in_IsFolder = value; }
			}

			//---------------------------------------------------------------------
			private bool _in_CreatePath = false;
			public bool in_CreatePath
			{
				get { return this._in_CreatePath; }
				set { this._in_CreatePath = value; }
			}

			//---------------------------------------------------------------------
			private FileSystemItem _out_Item = null;
			public FileSystemItem out_Item
			{
				get { return this._out_Item; }
				set { this._out_Item = value; }
			}

			//---------------------------------------------------------------------
			public CreateItemCommand()
			{ return; }

			//---------------------------------------------------------------------
			public CreateItemCommand( string Pathname, bool IsFolder, bool CreatePath )
			{
				this.in_Pathname = Pathname;
				this._in_IsFolder = IsFolder;
				this._in_CreatePath = CreatePath;
				return;
			}

		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class ReadItemCommand : CommandContext
		{

			//---------------------------------------------------------------------
			private string _in_Pathname = "";
			public string in_Pathname
			{
				get { return this._in_Pathname; }
				set { this._in_Pathname = value; }
			}

			//---------------------------------------------------------------------
			private FileSystemItem _out_Item = null;
			public FileSystemItem out_Item
			{
				get { return this._out_Item; }
				set { this._out_Item = value; }
			}

			//---------------------------------------------------------------------
			public ReadItemCommand()
			{ return; }

			//---------------------------------------------------------------------
			public ReadItemCommand( string Pathname )
			{
				this._in_Pathname = Pathname;
				return;
			}

		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class UpdateItemCommand : CommandContext
		{

			//---------------------------------------------------------------------
			private string _in_Pathname = "";
			public string in_Pathname
			{
				get { return this._in_Pathname; }
				set { this._in_Pathname = value; }
			}

			//---------------------------------------------------------------------
			private FileSystemItem _in_Item = null;
			public FileSystemItem in_Item
			{
				get { return this._in_Item; }
				set { this._in_Item = value; }
			}

			//---------------------------------------------------------------------
			private bool _in_IsFolder = false;
			public bool in_IsFolder
			{
				get { return this._in_IsFolder; }
				set { this._in_IsFolder = value; }
			}

			//---------------------------------------------------------------------
			private FileSystemItem _out_Item = null;
			public FileSystemItem out_Item
			{
				get { return this._out_Item; }
				set { this._out_Item = value; }
			}

			//---------------------------------------------------------------------
			public UpdateItemCommand()
			{ return; }

			//---------------------------------------------------------------------
			public UpdateItemCommand( string Pathname, bool IsFolder, FileSystemItem Item )
			{
				this._in_Pathname = Pathname;
				this._in_IsFolder = IsFolder;
				this._in_Item = Item;
				return;
			}

		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class DeleteItemCommand : CommandContext
		{

			//---------------------------------------------------------------------
			private string _in_Pathname = "";
			public string in_Pathname
			{
				get { return this._in_Pathname; }
				set { this._in_Pathname = value; }
			}

			//---------------------------------------------------------------------
			private bool _in_IsFolder = false;
			public bool in_IsFolder
			{
				get { return this._in_IsFolder; }
				set { this._in_IsFolder = value; }
			}

			//---------------------------------------------------------------------
			public DeleteItemCommand()
			{ return; }

			//---------------------------------------------------------------------
			public DeleteItemCommand( string Pathname, bool IsFolder )
			{
				this._in_Pathname = Pathname;
				this._in_IsFolder = IsFolder;
				return;
			}

		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class ReadFileContentCommand : CommandContext
		{

			//---------------------------------------------------------------------
			private string _in_Pathname = "";
			public string in_Pathname
			{
				get { return this._in_Pathname; }
				set { this._in_Pathname = value; }
			}

			//---------------------------------------------------------------------
			private int _in_Offset = 0;
			public int in_Offset
			{
				get { return this._in_Offset; }
				set { this._in_Offset = value; }
			}

			//---------------------------------------------------------------------
			private int _in_Length = -1;
			public int in_Length
			{
				get { return this._in_Length; }
				set { this._in_Length = value; }
			}

			//---------------------------------------------------------------------
			private byte[] _out_Content = null;
			public byte[] out_Content
			{
				get { return this._out_Content; }
				set { this._out_Content = value; }
			}

			//---------------------------------------------------------------------
			public ReadFileContentCommand()
			{ return; }

			//---------------------------------------------------------------------
			public ReadFileContentCommand( string Pathname, int Offset, int Length )
			{
				this._in_Pathname = Pathname;
				this._in_Offset = Offset;
				this._in_Length = Length;
				return;
			}

			//---------------------------------------------------------------------
			public ReadFileContentCommand( string Pathname, int Offset )
				: this( Pathname, Offset, -1 )
			{ return; }

		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class WriteFileContentCommand : CommandContext
		{

			//---------------------------------------------------------------------
			private string _in_Pathname = "";
			public string in_Pathname
			{
				get { return this._in_Pathname; }
				set { this._in_Pathname = value; }
			}

			//---------------------------------------------------------------------
			private int _in_Offset = 0;
			public int in_Offset
			{
				get { return this._in_Offset; }
				set { this._in_Offset = value; }
			}

			//---------------------------------------------------------------------
			private bool _in_Truncate = false;
			public bool in_Truncate
			{
				get { return this._in_Truncate; }
				set { this._in_Truncate = value; }
			}

			//---------------------------------------------------------------------
			private byte[] _in_Content = null;
			public byte[] in_Content
			{
				get { return this._in_Content; }
				set { this._in_Content = value; }
			}

			//---------------------------------------------------------------------
			private FileSystemItem _out_Item = null;
			public FileSystemItem out_Item
			{
				get { return this._out_Item; }
				set { this._out_Item = value; }
			}

			//---------------------------------------------------------------------
			public WriteFileContentCommand()
			{ return; }

			//---------------------------------------------------------------------
			public WriteFileContentCommand( string Pathname, int Offset, byte[] Content, bool Truncate )
			{
				this._in_Pathname = Pathname;
				this._in_Offset = Offset;
				this._in_Content = Content;
				this._in_Truncate = Truncate;
				return;
			}

			//---------------------------------------------------------------------
			public WriteFileContentCommand( string Pathname, int Offset, byte[] Content )
				: this( Pathname, Offset, Content, true )
			{ return; }

		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

	}

}
