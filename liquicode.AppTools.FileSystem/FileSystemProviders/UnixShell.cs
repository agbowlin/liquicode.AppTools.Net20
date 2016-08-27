
using System;
using System.IO;
using System.Collections.Generic;

namespace liquicode.AppTools
{


	public class UnixShell : FileSystemProvider
	{

		//---------------------------------------------------------------------
		private static List<string> ShortMonths = new List<string>( new string[] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" } );


		//---------------------------------------------------------------------
		private ShellAdapter _ShellAdapter = null;
		public ShellAdapter ShellAdapter
		{
			get { return this._ShellAdapter; }
			set { this._ShellAdapter = value; }
		}


		//---------------------------------------------------------------------
		public UnixShell()
		{
			return;
		}
		public UnixShell( ShellAdapter ThisProtocol )
		{
			this._ShellAdapter = ThisProtocol;
			return;
		}


		//---------------------------------------------------------------------
		private static int ParseNextWordSize( string Text )
		{
			int ich = 0;
			ich = Text.IndexOf( " " );
			if( ich < 0 )
			{ ich = Text.Length; }
			return ich;
		}

		//---------------------------------------------------------------------
		private static string ParseGetNextWord( ref string Text )
		{
			int ws = ParseNextWordSize( Text );
			if( ws <= 0 ) { return ""; }
			string word = Text.Substring( 0, ws );
			Text = Text.Substring( ws );
			Text = Text.TrimStart();
			return word;
		}

		//---------------------------------------------------------------------
		public static FileSystemItem ParseLsOutput( string ItemPath, string LsOutputLine )
		{
			if( LsOutputLine.Length < 40 ) { return null; }
			FileSystemItem item = new FileSystemItem();
			item.Path = ItemPath;
			item.Exists = true;

			string Permissions = ParseGetNextWord( ref LsOutputLine );
			if( Permissions.Length == 0 ) { return null; }
			switch( Permissions.Substring( 0, 1 ) )
			{
				case "d":
					item.IsFolder = true;
					break;
				case "l":
					item.IsLink = true;
					break;
				case "-":
					break;
				default:
					return null;
			}

			string LinkCount = ParseGetNextWord( ref LsOutputLine );
			if( LinkCount.Length == 0 ) { return null; }

			string Owner = ParseGetNextWord( ref LsOutputLine );
			if( Owner.Length == 0 ) { return null; }

			string Group = ParseGetNextWord( ref LsOutputLine );
			if( Group.Length == 0 ) { return null; }

			string Size = ParseGetNextWord( ref LsOutputLine );
			if( Size.Length == 0 ) { return null; }

			string Date = ParseGetNextWord( ref LsOutputLine );
			if( Date.Length == 0 ) { return null; }
			if( Date.Length == 3 )
			{
				if( UnixShell.ShortMonths.Contains( Date.ToUpper() ) )
				{
					Date += " " + ParseGetNextWord( ref LsOutputLine );
					try
					{
						int iYear = Convert.ToInt16( LsOutputLine.Substring( 0, 5 ) );
						Date += " " + LsOutputLine.Substring( 0, 5 );
						LsOutputLine = LsOutputLine.Substring( 5 ).Trim();
					}
					catch
					{
						Date += " " + DateTime.Today.Year.ToString();
					}
				}

			}

			string Time = ParseGetNextWord( ref LsOutputLine );
			if( Time.Length < 3 ) { return null; }
			if( !Time.Substring( 1, 1 ).Equals( ":" ) && !Time.Substring( 2, 1 ).Equals( ":" ) ) { return null; }

			string Name = "";
			string LinkTarget = "";
			if( item.IsLink )
			{
				int ich = LsOutputLine.IndexOf( "->" );
				if( ich < 0 )
				{
					Name = LsOutputLine;
				}
				else
				{
					Name = LsOutputLine.Substring( 0, ich ).Trim();
					LinkTarget = LsOutputLine.Substring( ich + 2 ).Trim();
				}
			}
			else
			{
				Name = LsOutputLine;
			}

			item.Name = Name;
			item.LinkTarget = LinkTarget;
			item.Size = Convert.ToInt64( Size );
			try
			{
				item.DateLastWrite = DateTime.Parse( Date + " " + Time );
			}
			catch( Exception ex )
			{
				throw ex;
			}

			return item;
		}


		//---------------------------------------------------------------------
		public override void Settings( Commands.SettingsCommand Command )
		{
			Command.out_Settings.Add( "TypeName", "UnixShell" );
			Command.out_Settings.Add( "ProtocolName", this._ShellAdapter.ProtocolName );
			Command.out_Settings.Add( "Host", this._ShellAdapter.Host );
			Command.out_Settings.Add( "User", this._ShellAdapter.User );
			Command.out_Settings.Add( "Port", this._ShellAdapter.Port );
			Command.out_Settings.Add( "Root", this._ShellAdapter.Root );
			return;
		}


		//---------------------------------------------------------------------
		private string MakeSystemPathname( string Root, string Path )
		{
			Pathname pathname = Pathname.Append( Root, Path );
			pathname.Separator = "/";
			return ("/" + pathname);
		}


		//---------------------------------------------------------------------
		public override void List( Commands.ListEntriesCommand Command )
		{
			if( this._ShellAdapter.Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "List", "FileSystem root is undefined" ); }
			string syspath = this.MakeSystemPathname( this._ShellAdapter.Root, Command.in_Pathname );

			// Get file listing.
			string ShellCommand = "";
			if( this._ShellAdapter.ProtocolName.Equals( "ssh", StringComparison.InvariantCultureIgnoreCase ) )
			{
				ShellCommand = "ls -l " + syspath + "/";
			}
			else if( this._ShellAdapter.ProtocolName.Equals( "scp", StringComparison.InvariantCultureIgnoreCase ) )
			{
				ShellCommand = "ls " + syspath + "/";
			}
			string output = this._ShellAdapter.ExecuteCommand( ShellCommand );
			string[] lines = output.Split( new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries );

			// Parse listing output.
			FileSystemItemList items = new FileSystemItemList();
			foreach( string line in lines )
			{
				FileSystemItem item = UnixShell.ParseLsOutput( Command.in_Pathname, line );
				if( item != null )
				{
					if( item.IsLink )
					{
						if( Command.in_IncludeLinks )
						{
							items.AddSorted( item );
						}
					}
					else if( item.IsFolder )
					{
						if( Command.in_IncludeFolders )
						{
							items.AddSorted( item );
						}
					}
					else
					{
						if( Command.in_IncludeFiles )
						{
							items.AddSorted( item );
						}
					}
				}
			}

			// Return, OK.
			Command.out_ItemList = items;
			return;
		}


		//---------------------------------------------------------------------
		public override void Create( Commands.CreateItemCommand Command )
		{
			if( this._ShellAdapter.Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Create", "FileSystem root is undefined" ); }
			string syspath = this.MakeSystemPathname( this._ShellAdapter.Root, Command.in_Pathname );

			string ShellCommand = "";
			if( Command.in_IsFolder )
			{
				ShellCommand = "mkdir " + syspath + "/";
			}
			else
			{
				ShellCommand = "touch " + syspath;
			}
			string output = this._ShellAdapter.ExecuteCommand( ShellCommand );

			// Get item.
			Commands.ReadItemCommand ReadCmd = new Commands.ReadItemCommand();
			ReadCmd.in_Pathname = Command.in_Pathname;
			this.Read( ReadCmd );
			if( ReadCmd.out_Item == null ) { throw new Exceptions.InvalidOperationException( "Create", "Item does not exist." ); }
			if( Command.in_CreatePath ) { }
			Command.out_Item = ReadCmd.out_Item;
			return;
		}


		//---------------------------------------------------------------------
		public override void Read( Commands.ReadItemCommand Command )
		{
			if( this._ShellAdapter.Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Read", "FileSystem root is undefined" ); }
			string syspath = this.MakeSystemPathname( this._ShellAdapter.Root, Command.in_Pathname );

			// Get file listing.
			string ShellCommand = "";
			if( this._ShellAdapter.ProtocolName.Equals( "ssh", StringComparison.InvariantCultureIgnoreCase ) )
			{
				ShellCommand = "ls -l " + syspath;
			}
			else if( this._ShellAdapter.ProtocolName.Equals( "scp", StringComparison.InvariantCultureIgnoreCase ) )
			{
				ShellCommand = "ls " + syspath;
			}
			string output = this._ShellAdapter.ExecuteCommand( ShellCommand );
			string[] lines = output.Split( new string[] { "\n" }, StringSplitOptions.None );

			// Parse listing output.
			FileSystemItemList items = new FileSystemItemList();
			foreach( string line in lines )
			{
				FileSystemItem item = UnixShell.ParseLsOutput( Command.in_Pathname, line );
				if( item != null )
				{
					if( string.Equals( item.Pathname, syspath, StringComparison.InvariantCultureIgnoreCase ) == true )
					{
						item.Pathname = Command.in_Pathname;
						items.AddSorted( item );
					}
				}
			}
			if( items.Count == 1 )
			{
				Command.out_Item = items[0];
			}
			else
			{
				Command.out_Item = new FileSystemItem();
				Command.out_Item.Pathname = Command.in_Pathname;
				Command.out_Item.IsFolder = true;
				Command.out_Item.Exists = true;
			}

			// Return, OK.
			return;
		}


		//---------------------------------------------------------------------
		public override void Update( Commands.UpdateItemCommand Command )
		{
			if( this._ShellAdapter.Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Update", "FileSystem root is undefined" ); }
			string syspath = this.MakeSystemPathname( this._ShellAdapter.Root, Command.in_Pathname );

			string ShellCommand = "";
			if( Command.in_Item.Pathname.Equals( Command.in_Pathname ) )
			{
				ShellCommand = "touch " + syspath;
			}
			else
			{
				Pathname new_syspath = this.MakeSystemPathname( this._ShellAdapter.Root, Command.in_Item.Pathname );
				new_syspath.Separator = "/";
				ShellCommand = "mv " + syspath + " " + new_syspath;
				;
			}
			string output = this._ShellAdapter.ExecuteCommand( ShellCommand );

			Commands.ReadItemCommand ReadCmd = new Commands.ReadItemCommand();
			ReadCmd.in_Pathname = Command.in_Item.Pathname;
			this.Read( ReadCmd );
			if( ReadCmd.out_Item == null ) { throw new Exceptions.InvalidOperationException( "Update", "Item does not exist." ); }

			// Return, OK.
			Command.out_Item = ReadCmd.out_Item;
			return;
		}


		//---------------------------------------------------------------------
		public override void Delete( Commands.DeleteItemCommand Command )
		{
			if( this._ShellAdapter.Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Delete", "FileSystem root is undefined" ); }
			string syspath = this.MakeSystemPathname( this._ShellAdapter.Root, Command.in_Pathname );

			string ShellCommand = "";
			if( Command.in_IsFolder )
			{
				ShellCommand = "rmdir " + syspath;
			}
			else
			{
				ShellCommand = "rm " + syspath;
			}
			string output = this._ShellAdapter.ExecuteCommand( ShellCommand );
			string[] lines = output.Split( new string[] { "\n" }, StringSplitOptions.None );

			// Return, OK.
			return;
		}


		//---------------------------------------------------------------------
		public override void ReadFileContent( Commands.ReadFileContentCommand Command )
		{
			if( this._ShellAdapter.Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "ReadContent", "FileSystem root is undefined" ); }
			string syspath = this.MakeSystemPathname( this._ShellAdapter.Root, Command.in_Pathname );

			// Check file.
			Commands.ReadItemCommand ReadCmd = new Commands.ReadItemCommand();
			ReadCmd.in_Pathname = syspath;
			this.Read( ReadCmd );
			if( ReadCmd.out_Item == null )
			{ throw new Exceptions.InvalidOperationException( "ReadContent", "Item does not exist." ); }
			if( ReadCmd.out_Item.IsFolder )
			{ throw new Exceptions.InvalidOperationException( "ReadContent", "Cannot read content from a folder." ); }
			if( (Command.in_Offset < 0) || (Command.in_Offset >= ReadCmd.out_Item.Size) )
			{ throw new Exceptions.InvalidOperationException( "ReadContent", "Offset is outside of expected range." ); }

			// Get file.
			string TempFilename = Path.GetTempFileName();
			string ShellCommand = "get -preservetime " + syspath + " " + TempFilename;
			string output = this._ShellAdapter.ExecuteCommand( ShellCommand );
			string[] lines = output.Split( new string[] { "\n" }, StringSplitOptions.None );

			// Read file bytes.
			using( FileStream stream = File.OpenRead( TempFilename ) )
			{
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

			// Return, OK.
			return;
		}


		//---------------------------------------------------------------------
		public override void WriteFileContent( Commands.WriteFileContentCommand Command )
		{
			//if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "WriteContent", "FileSystem root is undefined" ); }
			//string syspath = this.MakeSystemPathname( this._Root, Command.in_Pathname );
			//FileSystemItem item = this.ItemFromPathname( item_path_name );
			//if( item.IsFolder )
			//{
			//    throw new Exceptions.InvalidOperationException( "WriteContent", "Cannot write content to a folder." );
			//}
			//else
			//{
			//    using( FileStream stream = File.OpenWrite( item_path_name ) )
			//    {
			//        if( (Command.in_Offset < 0) || (Command.in_Offset >= stream.Length) )
			//        { throw new Exceptions.InvalidOperationException( "WriteContent", "Offset is outside of expected range." ); }
			//        stream.Position = Command.in_Offset;
			//        stream.Write( Command.in_Content, 0, Command.in_Content.Length );
			//        if( Command.in_Truncate )
			//        { stream.SetLength( stream.Position ); }
			//    }
			//    Command.out_Item = this.ItemFromPathname( item_path_name );
			//    return;
			//}



			//$Command = "put -preservetime " + $Record.LocalPath + " " + $ThisRemoteFolder + '/'
			//$Lines = WinScp-Command $ThisConnectionString $Command $False



			return;
		}


	}


}
