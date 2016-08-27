
using System;
using System.IO;

namespace liquicode.AppTools
{
	public class LocalFileSystem : FileSystemProvider
	{

		//---------------------------------------------------------------------
		private string _Root = "";
		public string Root
		{
			get { return this._Root; }
			set { this._Root = value; }
		}


		//---------------------------------------------------------------------
		public LocalFileSystem()
		{
			return;
		}


		//---------------------------------------------------------------------
		public LocalFileSystem( string ThisRoot )
		{
			this._Root = ThisRoot;
			return;
		}


		//---------------------------------------------------------------------
		private FileSystemItem ItemFromPathname( string Pathname )
		{
			FileSystemItem item = new FileSystemItem();
			item.Pathname = Pathname.Substring( this._Root.Length );
			if( Directory.Exists( Pathname ) )
			{
				item.Exists = true;
				item.IsFolder = true;
				item.DateCreated = Directory.GetCreationTimeUtc( Pathname );
				item.DateLastWrite = Directory.GetLastWriteTimeUtc( Pathname );
				item.DateLastRead = Directory.GetLastAccessTimeUtc( Pathname );
			}
			else if( File.Exists( Pathname ) )
			{
				item.Exists = true;
				item.DateCreated = File.GetCreationTimeUtc( Pathname );
				item.DateLastWrite = File.GetLastWriteTimeUtc( Pathname );
				item.DateLastRead = File.GetLastAccessTimeUtc( Pathname );
				FileInfo fi = new FileInfo( Pathname );
				item.Size = fi.Length;
			}
			return item;
		}

		//---------------------------------------------------------------------
		public override void Settings( Commands.SettingsCommand Command )
		{
			Command.out_Settings.Add( "TypeName", "LocalFileSystem" );
			Command.out_Settings.Add( "ProtocolName", "file" );
			Command.out_Settings.Add( "Host", "localhost" );
			Command.out_Settings.Add( "User", "" );
			Command.out_Settings.Add( "Port", "" );
			Command.out_Settings.Add( "Root", this._Root );
			return;
		}

		//---------------------------------------------------------------------
		public override void List( Commands.ListEntriesCommand Command )
		{
			if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "List", "FileSystem root is undefined" ); }
			string search_path = Pathname.Append( this._Root, Command.in_Pathname );
			if( Directory.Exists( search_path ) )
			{
				SearchOption search_option = SearchOption.TopDirectoryOnly;
				Command.out_ItemList = new FileSystemItemList();
				if( Command.in_IncludeFolders )
				{
					FileSystemItemList items = new FileSystemItemList();
					foreach( string localpath in Directory.GetDirectories( search_path, "*", search_option ) )
					{
						string itemname = localpath.Substring( search_path.Length );
						FileSystemItem item = new FileSystemItem( Command.in_Pathname, itemname, true, true );
						item.DateCreated = Directory.GetCreationTimeUtc( localpath );
						item.DateLastRead = Directory.GetLastAccessTimeUtc( localpath );
						item.DateLastWrite = Directory.GetLastWriteTimeUtc( localpath );
						items.AddSorted( item );
					}
					Command.out_ItemList.AddRange( items.ToArray() );
				}
				if( Command.in_IncludeFiles )
				{
					FileSystemItemList items = new FileSystemItemList();
					foreach( string localpath in Directory.GetFiles( search_path, "*", search_option ) )
					{
						string itemname = localpath.Substring( search_path.Length );
						FileSystemItem item = new FileSystemItem( Command.in_Pathname, itemname, false, true );
						item.DateCreated = File.GetCreationTimeUtc( localpath );
						item.DateLastRead = File.GetLastAccessTimeUtc( localpath );
						item.DateLastWrite = File.GetLastWriteTimeUtc( localpath );
						item.Size = (new FileInfo( localpath )).Length;
						items.AddSorted( item );
					}
					Command.out_ItemList.AddRange( items.ToArray() );
				}
			}
			else
			{
				throw new Exceptions.InvalidOperationException( "List", "Path does not exist." );
			}
			return;
		}

		//---------------------------------------------------------------------
		public override void Create( Commands.CreateItemCommand Command )
		{
			if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Create", "FileSystem root is undefined" ); }
			Pathname ItemPathname = Pathname.Append( this._Root, Command.in_Pathname );
			if( Directory.Exists( ItemPathname.Path ) == false ) { throw new InvalidOperationException(); }
			if( Command.in_IsFolder )
			{
				Directory.CreateDirectory( ItemPathname );
			}
			else
			{
				FileStream stream = File.Create( ItemPathname );
				stream.Close();
			}
			Command.out_Item = this.ItemFromPathname( ItemPathname );
			return;
		}

		//---------------------------------------------------------------------
		public override void Read( Commands.ReadItemCommand Command )
		{
			if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Read", "FileSystem root is undefined" ); }
			Pathname ItemPathname = Pathname.Append( this._Root, Command.in_Pathname );
			FileSystemItem item = this.ItemFromPathname( ItemPathname );
			if( item.Exists )
			{
				Command.out_Item = item;
			}
			return;
		}

		//---------------------------------------------------------------------
		public override void Update( Commands.UpdateItemCommand Command )
		{
			if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Update", "FileSystem root is undefined" ); }
			Pathname ItemPathname = Pathname.Append( this._Root, Command.in_Pathname );
			Pathname DestItemPathname = Pathname.Append( this._Root, Command.in_Item.Pathname );
			bool needs_move = !ItemPathname.Equals( DestItemPathname );
			//bool needs_move = !ItemPathname.Path.Equals( DestItemPathname.Path, StringComparison.InvariantCultureIgnoreCase );
			FileSystemItem item = this.ItemFromPathname( ItemPathname );
			if( item.Exists )
			{
				if( item.IsFolder )
				{
					if( needs_move )
					{ Directory.Move( ItemPathname, DestItemPathname ); }
					if( Command.in_Item.DateCreated.HasValue )
					{ Directory.SetCreationTimeUtc( DestItemPathname, (DateTime)Command.in_Item.DateCreated ); }
					else
					{ Directory.SetCreationTimeUtc( DestItemPathname, (DateTime)item.DateCreated ); }
					if( Command.in_Item.DateLastWrite.HasValue )
					{ Directory.SetLastWriteTimeUtc( DestItemPathname, (DateTime)Command.in_Item.DateLastWrite ); }
					else
					{ Directory.SetLastWriteTimeUtc( DestItemPathname, (DateTime)item.DateLastWrite ); }
					if( Command.in_Item.DateLastRead.HasValue )
					{ Directory.SetLastAccessTimeUtc( DestItemPathname, (DateTime)Command.in_Item.DateLastRead ); }
					else
					{ Directory.SetLastAccessTimeUtc( DestItemPathname, (DateTime)item.DateLastRead ); }
				}
				else
				{
					if( needs_move )
					{ File.Move( ItemPathname, DestItemPathname ); }
					if( Command.in_Item.DateCreated.HasValue )
					{ File.SetCreationTimeUtc( DestItemPathname, (DateTime)Command.in_Item.DateCreated ); }
					else
					{ File.SetCreationTimeUtc( DestItemPathname, (DateTime)item.DateCreated ); }
					if( Command.in_Item.DateLastWrite.HasValue )
					{ File.SetLastWriteTimeUtc( DestItemPathname, (DateTime)Command.in_Item.DateLastWrite ); }
					else
					{ File.SetLastWriteTimeUtc( DestItemPathname, (DateTime)item.DateLastWrite ); }
					if( Command.in_Item.DateLastRead.HasValue )
					{ File.SetLastAccessTimeUtc( DestItemPathname, (DateTime)Command.in_Item.DateLastRead ); }
					else
					{ File.SetLastAccessTimeUtc( DestItemPathname, (DateTime)item.DateLastRead ); }
				}
				Command.out_Item = this.ItemFromPathname( DestItemPathname );
			}
			else
			{
				throw new Exceptions.InvalidOperationException( "Update", "Item does not exist." );
			}
			return;
		}

		//---------------------------------------------------------------------
		public override void Delete( Commands.DeleteItemCommand Command )
		{
			if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Delete", "FileSystem root is undefined" ); }
			Pathname ItemPathname = Pathname.Append( this._Root, Command.in_Pathname );
			if( Command.in_IsFolder )
			{
				Directory.Delete( ItemPathname, true );
			}
			else
			{
				File.Delete( ItemPathname );
			}

			//FileSystemItem item = this.ItemFromPathname( item_path_name );
			//if( item.Exists )
			//{
			//    if( item.IsFolder )
			//    {
			//        Directory.Delete( item_path_name, true );
			//    }
			//    else
			//    {
			//        File.Delete( item_path_name );
			//    }
			//}
			//else
			//{
			//    throw new Exceptions.InvalidOperationException( "Delete", "Item does not exist." );
			//}

			return;
		}

		//---------------------------------------------------------------------
		public override void ReadFileContent( Commands.ReadFileContentCommand Command )
		{
			if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "ReadContent", "FileSystem root is undefined" ); }
			Pathname ItemPathname = Pathname.Append( this._Root, Command.in_Pathname );
			using( FileStream stream = File.OpenRead( ItemPathname ) )
			{
				if( (Command.in_Offset < 0) || (Command.in_Offset >= stream.Length) )
				{ throw new Exceptions.InvalidOperationException( "ReadContent", "Offset is outside of expected range." ); }
				long nLength = Command.in_Length;
				if( nLength < 0 )
				{ nLength = (stream.Length - Command.in_Offset); }
				if( (Command.in_Offset + nLength) > stream.Length )
				{ nLength = (stream.Length - Command.in_Offset); }
				byte[] bytes = new byte[nLength];
				stream.Position = Command.in_Offset;
				int cb = stream.Read( bytes, 0, bytes.Length );
				if( cb != nLength )
				{ throw new Exceptions.InvalidOperationException( "ReadContent", "Read error." ); }
				Command.out_Content = bytes;
			}
			return;
		}

		//---------------------------------------------------------------------
		public override void WriteFileContent( Commands.WriteFileContentCommand Command )
		{
			if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "WriteContent", "FileSystem root is undefined" ); }
			Pathname ItemPathname = Pathname.Append( this._Root, Command.in_Pathname );
			using( FileStream stream = File.OpenWrite( ItemPathname ) )
			{
				if( (Command.in_Offset < 0) || (Command.in_Offset >= stream.Length) )
				{ throw new Exceptions.InvalidOperationException( "WriteContent", "Offset is outside of expected range." ); }
				stream.Position = Command.in_Offset;
				stream.Write( Command.in_Content, 0, Command.in_Content.Length );
				if( Command.in_Truncate )
				{ stream.SetLength( stream.Position ); }
			}
			Command.out_Item = this.ItemFromPathname( ItemPathname );
			return;
		}

	}

}
