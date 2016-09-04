
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace liquicode.AppTools
{

	public class BlockStreamFileSystem : FileSystemProvider
	{


		////--------------------------------------------------------------------------------
		//internal Guid ID_;
		//internal Guid ParentID_;
		//private string Type_;


		////--------------------------------------------------------------------------------
		//public BlockStreamFileSystemItem( string Filename_in )
		//{
		//    // Check to Create new Archive File
		//    if( !File.Exists( Filename_in ) )
		//    {
		//        BlockStream BlockStream = new BlockStream();
		//        BlockStream.CreateFile( Filename_in );
		//        BlockStream.Close( false, true );
		//    }
		//    // New Root Node
		//    IsRoot_ = true;
		//    IsFolder_ = true;
		//    Root_ = Filename_in;
		//    Path_ = "";
		//    Name_ = "";
		//    ID_ = Guid.Empty;
		//    ParentID_ = Guid.Empty;
		//    Type_ = "folder";
		//    Size_ = 0;
		//    DateCreated_ = File.GetCreationTimeUtc( Root_ );
		//    DateLastRead_ = File.GetLastAccessTimeUtc( Root_ );
		//    DateLastWrite_ = File.GetLastWriteTimeUtc( Root_ );
		//    return;
		//}


		////--------------------------------------------------------------------------------
		//internal BlockStreamFileSystemItem( string Root_in, string Path_in, Guid ID_in, Hashtable Tags_in )
		//{
		//    // New Child Node
		//    IsRoot_ = false;
		//    Root_ = Root_in;
		//    Path_ = Path_in;
		//    ID_ = ID_in;
		//    ParentID_ = (System.Guid)Tags_in[ "ParentID" ];
		//    Type_ = (string)Tags_in[ "Type" ];
		//    IsFolder_ = (string.Compare( Type_, "folder", true ) == 0);
		//    Name_ = (string)Tags_in[ "Name" ];
		//    Size_ = (long)Tags_in[ "Size" ];
		//    DateCreated_ = (DateTime)Tags_in[ "DateCreated" ];
		//    DateLastRead_ = (DateTime)Tags_in[ "DateLastRead" ];
		//    DateLastWrite_ = (DateTime)Tags_in[ "DateLastWrite" ];
		//    return;
		//}


		////--------------------------------------------------------------------------------
		//internal bool WriteTags_()
		//{
		//    BlockStream BlockStream = new BlockStream();
		//    BlockStream.OpenFile( Root_ );
		//    Hashtable BlockTags = BlockStream.GetBlockTags( ID_ );
		//    BlockTags[ "ID" ] = ID_;
		//    BlockTags[ "ParentID" ] = ParentID_;
		//    BlockTags[ "Type" ] = Type_;
		//    BlockTags[ "Name" ] = Name_;
		//    BlockTags[ "Size" ] = Size_;
		//    BlockTags[ "DateCreated" ] = DateCreated_;
		//    BlockTags[ "DateLastRead" ] = DateLastRead_;
		//    BlockTags[ "DateLastWrite" ] = DateLastWrite_;
		//    BlockStream.SetBlockTags( ID_, BlockTags );
		//    BlockStream.Close( false, true );
		//    return true;
		//}


		////--------------------------------------------------------------------------------
		//public override long Size
		//{
		//    get { return Size_; }
		//}


		////--------------------------------------------------------------------------------
		//public override DateTime DateCreated
		//{
		//    get
		//    {
		//        if( IsRoot_ )
		//        {
		//            return null;
		//        }
		//        else
		//        {
		//            return DateCreated_;
		//        }
		//    }
		//    set
		//    {
		//        if( IsRoot_ )
		//        {
		//            return;
		//        }
		//        else
		//        {
		//            DateCreated_ = value;
		//            WriteTags_();
		//        }
		//    }
		//}


		////--------------------------------------------------------------------------------
		//public override DateTime DateLastRead
		//{
		//    get
		//    {
		//        if( IsRoot_ )
		//        {
		//            return null;
		//        }
		//        else
		//        {
		//            return DateLastRead_;
		//        }
		//    }
		//    set
		//    {
		//        if( IsRoot_ )
		//        {
		//            return;
		//        }
		//        else
		//        {
		//            DateLastRead_ = value;
		//            WriteTags_();
		//        }
		//    }
		//}


		////--------------------------------------------------------------------------------
		//public override DateTime DateLastWrite
		//{
		//    get
		//    {
		//        if( IsRoot_ )
		//        {
		//            return null;
		//        }
		//        else
		//        {
		//            return DateLastWrite_;
		//        }
		//    }
		//    set
		//    {
		//        if( IsRoot_ )
		//        {
		//            return;
		//        }
		//        else
		//        {
		//            DateLastWrite_ = value;
		//            WriteTags_();
		//        }
		//    }
		//}


		////--------------------------------------------------------------------------------
		//public override SortedList ListFolders( string Name_in )
		//{
		//    if( !IsFolder_ ) return null;
		//    SortedList lst = new SortedList();
		//    BlockStream BlockStream = new BlockStream();
		//    Guid ParentBlockID;
		//    Hashtable BlockTags;
		//    BlockStreamFileSystemItem Item;
		//    string sPath = Path_;
		//    if( (Path_ != "") & (Name_ != "") ) { sPath += "\\"; }
		//    sPath += this.Name_;
		//    BlockStream.OpenFile( Root_ );
		//    foreach( Guid BlockID in BlockStream.BlockIDs )
		//    {
		//        BlockTags = BlockStream.GetBlockTags( BlockID );
		//        if( (string.Compare( BlockTags[ "Type" ].ToString(), "folder", true ) == 0) )
		//        {
		//            ParentBlockID = (System.Guid)BlockTags[ "ParentID" ];
		//            if( ParentBlockID.Equals( ID_ ) )
		//            {
		//                Item = new BlockStreamFileSystemItem( Root_, sPath, BlockID, BlockTags );
		//                lst.Add( BlockTags[ "Name" ], Item );
		//            }
		//        }
		//    }
		//    BlockStream.Close( false, true );
		//    return lst;
		//}


		////--------------------------------------------------------------------------------
		//public override SortedList ListFiles( string Name_in )
		//{
		//    if( !IsFolder_ ) return null;
		//    SortedList lst = new SortedList();
		//    BlockStream BlockStream = new BlockStream();
		//    Guid ParentBlockID;
		//    Hashtable BlockTags;
		//    BlockStreamFileSystemItem Item;
		//    string sPath = Path_;
		//    if( (Path_ != "") & (Name_ != "") ) { sPath += "\\"; }
		//    sPath += this.Name_;
		//    BlockStream.OpenFile( Root_ );
		//    foreach( Guid BlockID in BlockStream.BlockIDs )
		//    {
		//        BlockTags = BlockStream.GetBlockTags( BlockID );
		//        if( (string.Compare( (string)BlockTags[ "Type" ], "folder", true ) != 0) )
		//        {
		//            ParentBlockID = (System.Guid)BlockTags[ "ParentID" ];
		//            if( ParentBlockID.Equals( ID_ ) )
		//            {
		//                Item = new BlockStreamFileSystemItem( Root_, sPath, BlockID, BlockTags );
		//                lst.Add( BlockTags[ "Name" ], Item );
		//            }
		//        }
		//    }
		//    BlockStream.Close( false, true );
		//    return lst;
		//}


		////--------------------------------------------------------------------------------
		//public override bool AddFolder( string Name_in )
		//{
		//    if( !IsFolder_ ) return false;
		//    BlockStream BlockStream = new BlockStream();
		//    Guid BlockID = Guid.Empty;
		//    Hashtable BlockTags;
		//    DateTime dtNow = DateTime.UtcNow;
		//    BlockStream.OpenFile( Root_ );
		//    BlockStream.CreateBlock( 0, ref BlockID );
		//    BlockTags = new Hashtable();
		//    BlockTags[ "ID" ] = BlockID;
		//    BlockTags[ "ParentID" ] = ID_;
		//    BlockTags[ "Type" ] = "folder";
		//    BlockTags[ "Name" ] = Name_in;
		//    BlockTags[ "Size" ] = 0;
		//    BlockTags[ "DateCreated" ] = dtNow;
		//    BlockTags[ "DateLastRead" ] = dtNow;
		//    BlockTags[ "DateLastWrite" ] = dtNow;
		//    BlockStream.SetBlockTags( BlockID, BlockTags );
		//    BlockStream.Close( false, true );
		//    return true;
		//}


		////--------------------------------------------------------------------------------
		//public override bool AddFile( string Name_in )
		//{
		//    if( !IsFolder_ ) return false;
		//    BlockStream BlockStream = new BlockStream();
		//    Guid BlockID = Guid.Empty;
		//    Hashtable BlockTags;
		//    DateTime dtNow = DateTime.UtcNow;
		//    BlockStream.OpenFile( Root_ );
		//    BlockStream.CreateBlock( 0, ref BlockID );
		//    BlockTags = new Hashtable();
		//    BlockTags[ "ID" ] = BlockID;
		//    BlockTags[ "ParentID" ] = ID_;
		//    BlockTags[ "Type" ] = "file";
		//    BlockTags[ "Name" ] = Name_in;
		//    BlockTags[ "Size" ] = 0;
		//    BlockTags[ "DateCreated" ] = dtNow;
		//    BlockTags[ "DateLastRead" ] = dtNow;
		//    BlockTags[ "DateLastWrite" ] = dtNow;
		//    BlockStream.SetBlockTags( BlockID, BlockTags );
		//    BlockStream.Close( false, true );
		//    return true;
		//}


		////--------------------------------------------------------------------------------
		//public override bool Rename( string Name_in )
		//{
		//    if( IsRoot_ ) return false;
		//    Name_ = Name_in;
		//    WriteTags_();
		//    return true;
		//}


		////--------------------------------------------------------------------------------
		//private void ListChildren_( BlockStream BlockStream_in, Guid ParentID_in, SortedList IDs_inout )
		//{
		//    Guid ParentBlockID;
		//    Hashtable BlockTags;
		//    SortedList BlockIDs = new SortedList();
		//    foreach( Guid BlockID in BlockStream_in.BlockIDs )
		//    {
		//        BlockTags = BlockStream_in.GetBlockTags( BlockID );
		//        ParentBlockID = (System.Guid)BlockTags[ "ParentID" ];
		//        if( ParentBlockID.Equals( ParentID_in ) )
		//        {
		//            IDs_inout.Add( null, BlockID );
		//            BlockIDs.Add( null, BlockID );
		//        }
		//    }
		//    foreach( Guid BlockID in BlockIDs )
		//    {
		//        ListChildren_( BlockStream_in, BlockID, IDs_inout );
		//    }
		//}


		////--------------------------------------------------------------------------------
		//public override bool Remove()
		//{
		//    if( IsRoot_ ) return false;
		//    BlockStream BlockStream = new BlockStream();
		//    SortedList BlockIDs = new SortedList();
		//    BlockStream.OpenFile( Root_ );
		//    BlockIDs.Add( null, ID_ );
		//    ListChildren_( BlockStream, ID_, BlockIDs );
		//    foreach( Guid BlockID in BlockIDs )
		//    {
		//        BlockStream.DestroyBlock( BlockID );
		//    }
		//    BlockStream.Close( false, true );
		//    IsRoot_ = false;
		//    IsFolder_ = false;
		//    Root_ = "";
		//    Path_ = "";
		//    ID_ = Guid.Empty;
		//    ParentID_ = Guid.Empty;
		//    Type_ = "";
		//    Name_ = "";
		//    Size_ = 0;
		//    DateCreated_ = DateTime.MaxValue;
		//    DateLastRead_ = DateTime.MaxValue;
		//    DateLastWrite_ = DateTime.MaxValue;
		//    return true;
		//}


		////--------------------------------------------------------------------------------
		//public override Stream ReadFile()
		//{
		//    if( IsRoot_ ) return null;
		//    if( IsFolder_ ) return null;
		//    //TODO:
		//    return null;
		//}


		////--------------------------------------------------------------------------------
		//public override bool WriteFile( Stream Stream_in )
		//{
		//    if( IsRoot_ ) return false;
		//    if( IsFolder_ ) return false;
		//    //TODO:
		//    return false;
		//}



		//--------------------------------------------------------------------------------
		private BlockStream _BlockStream = null;

		//---------------------------------------------------------------------
		public BlockStreamFileSystem()
		{
			return;
		}

		//---------------------------------------------------------------------
		public BlockStreamFileSystem( string ThisFileName )
		{
			this._BlockStream = new BlockStream();
			this._BlockStream.OpenFile( ThisFileName );
			return;
		}

		//--------------------------------------------------------------------------------
		private FileSystemItem Tags2Item( Hashtable Tags )
		{
			FileSystemItem entry = new FileSystemItem();
			entry.Exists = true;
			entry.IsFolder = (bool)Tags[ "IsFolder" ];
			entry.Path = "";
			entry.Name = (string)Tags[ "Name" ];
			entry.DateCreated = (DateTime?)Tags[ "DateCreated" ];
			entry.DateLastWrite = (DateTime?)Tags[ "DateLastWrite" ];
			entry.DateLastRead = (DateTime?)Tags[ "DateLastRead" ];
			entry.Size = (long?)Tags[ "Size" ];
			return entry;
		}

		//--------------------------------------------------------------------------------
		private Hashtable Item2Tags( FileSystemItem Item, Guid ParentID )
		{
			Hashtable tags = new Hashtable();
			tags[ "IsFolder" ] = Item.IsFolder;
			tags[ "ParentID" ] = ParentID;
			tags[ "Name" ] = Item.Name;
			tags[ "DateCreated" ] = Item.DateCreated;
			tags[ "DateLastWrite" ] = Item.DateLastWrite;
			tags[ "DateLastRead" ] = Item.DateLastRead;
			tags[ "Size" ] = Item.Size;
			return tags;
		}

		//--------------------------------------------------------------------------------
		private void LoadPath_Recurse( Guid in_ParentID, List<string> in_PathList, FileSystemItemList out_ItemList, List<Guid> out_IdList )
		{
			if( in_PathList.Count == 0 ) { return; }
			foreach( Guid id in this._BlockStream.GetBlockIDs() )
			{
				Hashtable tags = this._BlockStream.GetBlockTags( id );
				Guid parent_id = (Guid)tags[ "ParentID" ];
				string entry_name = (string)tags[ "Name" ];
				if( parent_id.Equals( in_ParentID ) )
				{
					if( entry_name.Equals( in_PathList[ 0 ] ) )
					{
						in_PathList.RemoveAt( 0 );
						if( out_ItemList != null )
						{
							FileSystemItem entry = this.Tags2Item( tags );
							if( out_ItemList.Count > 0 )
							{ entry.Path = out_ItemList[ out_ItemList.Count - 1 ].Pathname; }
							out_ItemList.Add( entry );
						}
						if( out_IdList != null )
						{
							out_IdList.Add( id );
						}
						this.LoadPath_Recurse( id, in_PathList, out_ItemList, out_IdList );
						return;
					}
				}
			}
			return;
		}

		//--------------------------------------------------------------------------------
		private void LoadPath( string in_Pathname, FileSystemItemList out_ItemList, List<Guid> out_IdList )
		{
			List<string> path_list = new List<string>();
			path_list.AddRange( Pathname.PathnameItems( in_Pathname ) );
			this.LoadPath_Recurse( Guid.Empty, path_list, out_ItemList, out_IdList );
			return;
		}

		////--------------------------------------------------------------------------------
		//private FileSystemItem GetItemFromID( Guid ID )
		//{
		//    if( ID.Equals( Guid.Empty ) ) { return null; }
		//    Hashtable tags = this._BlockStream.GetBlockTags( ID );
		//    FileSystemItem entry = this.Tags2Item( tags );
		//    Guid parent_id = (Guid)tags[ "ParentID" ];
		//    if( parent_id.Equals( Guid.Empty ) == false )
		//    {
		//        FileSystemItem parent_entry = this.GetItemFromID( parent_id );
		//        entry.Path = parent_entry.Pathname;
		//    }
		//    return entry;
		//}

		////--------------------------------------------------------------------------------
		//private FileSystemItem FindName( Guid ParentID, string Name )
		//{
		//    FileSystemItem entry = null;
		//    foreach( Guid id in this._BlockStream.BlockIDs )
		//    {
		//        Hashtable tags = this._BlockStream.GetBlockTags( id );

		//    }
		//    return entry;
		//}

		////--------------------------------------------------------------------------------
		//private FileSystemItem ItemFromPathname( string Pathname )
		//{
		//    FileSystemItem entry = new FileSystemItem();
		//    entry.Pathname = Pathname;


		//    return entry;
		//}

		//--------------------------------------------------------------------------------
		private FileSystemItemList ListChildren( Guid ParentID, string ParentPath, bool ListFolders )
		{
			FileSystemItemList entry_list = new FileSystemItemList();
			foreach( Guid id in this._BlockStream.GetBlockIDs() )
			{
				Hashtable tags = this._BlockStream.GetBlockTags( id );
				if( ParentID.Equals( (Guid)tags[ "ParentID" ] ) )
				{
					FileSystemItem entry = this.Tags2Item( tags );
					if( (ListFolders && entry.IsFolder) || (!ListFolders && !entry.IsFolder) )
					{
						entry.Path = ParentPath;
						entry_list.AddSorted( entry );
					}
				}
			}
			return entry_list;
		}

		//---------------------------------------------------------------------
		public override void Settings( Commands.SettingsCommand Command )
		{
			Command.out_Settings.Add( "TypeName", "BlockStreamFileSystem" );
			return;
		}

		//--------------------------------------------------------------------------------
		public override void List( Commands.ListEntriesCommand Command )
		{
			if( this._BlockStream == null ) { throw new Exceptions.InvalidOperationException( "List", "FileSystem root is undefined" ); }
			string search_path = Command.in_Pathname;
			// Load the path entries and ids.
			FileSystemItemList path_entries = new FileSystemItemList();
			List<Guid> path_ids = new List<Guid>();
			this.LoadPath( search_path, path_entries, path_ids );
			// Get parent path and id.
			string parent_path = "";
			if( path_entries.Count > 0 )
			{ parent_path = path_entries[ path_entries.Count - 1 ].Pathname; }
			Guid parent_id = Guid.Empty;
			if( path_ids.Count > 0 )
			{ parent_id = path_ids[ path_ids.Count - 1 ]; }
			// List child folders and files.
			FileSystemItemList entry_list = new FileSystemItemList();
			if( Command.in_IncludeFolders )
			{ entry_list.AddRange( this.ListChildren( parent_id, parent_path, true ) ); }
			if( Command.in_IncludeFiles )
			{ entry_list.AddRange( this.ListChildren( parent_id, parent_path, false ) ); }
			// Return, OK
			Command.out_ItemList = entry_list;
			return;
		}

		//--------------------------------------------------------------------------------
		public override void Create( Commands.CreateItemCommand Command )
		{
			throw new NotImplementedException();
		}

		//--------------------------------------------------------------------------------
		public override void Read( Commands.ReadItemCommand Command )
		{
			throw new NotImplementedException();
		}

		//--------------------------------------------------------------------------------
		public override void Update( Commands.UpdateItemCommand Command )
		{
			throw new NotImplementedException();
		}

		//--------------------------------------------------------------------------------
		public override void Delete( Commands.DeleteItemCommand Command )
		{
			throw new NotImplementedException();
		}

		//--------------------------------------------------------------------------------
		public override void ReadFileContent( Commands.ReadFileContentCommand Command )
		{
			throw new NotImplementedException();
		}

		//--------------------------------------------------------------------------------
		public override void WriteFileContent( Commands.WriteFileContentCommand Command )
		{
			throw new NotImplementedException();
		}

	}

}
