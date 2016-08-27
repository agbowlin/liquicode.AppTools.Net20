
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace liquicode.AppTools
{
	public class WinScpShellAdapter : ShellAdapter
	{

		//---------------------------------------------------------------------
		//	WinScp Scripting Command Reference:
		//		http://winscp.net/eng/docs/scripting
		//---------------------------------------------------------------------

		//---------------------------------------------------------------------
		public override string ProtocolName
		{
			get { return "scp"; }
		}


		//---------------------------------------------------------------------
		protected string _WinScpFilename = "";
		public string WinScpFilename
		{
			get { return this._WinScpFilename; }
			set { this._WinScpFilename = value; }
		}


		//---------------------------------------------------------------------
		public WinScpShellAdapter()
		{
			this._WinScpFilename = "";
			this._Host = "";
			this._Port = 22;
			this._User = "";
			this._Password = "";
			this._Root = "";
			return;
		}


		//---------------------------------------------------------------------
		public override string ExecuteCommand( string SshCommand )
		{
			if( this._WinScpFilename.Length == 0 ) { throw new Exceptions.InvalidOperationException( "ExecuteCommand", "WinScpFilename is undefined" ); }
			if( this._Host.Length == 0 ) { throw new Exceptions.InvalidOperationException( "ExecuteCommand", "Host is undefined" ); }

			string Arguments = "";
			string ErrString = "";
			string OutString = "";
			Process CommandProcess = new Process();
			CommandProcess.StartInfo.FileName = this._WinScpFilename;

			CommandProcess.StartInfo.UseShellExecute = false;
			CommandProcess.StartInfo.RedirectStandardInput = true;
			CommandProcess.StartInfo.RedirectStandardError = true;
			CommandProcess.StartInfo.RedirectStandardOutput = true;

			CommandProcess.StartInfo.CreateNoWindow = true;
			CommandProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

			string ScriptFilename = Path.GetTempFileName();
			string ScriptText =
			@"
				option batch on
				option confirm off
				open " + this._User + ":" + this._Password + "@" + this._Host + ":" + this._Port.ToString() + @"
				" + SshCommand + @"
				close
				exit
			";
			File.WriteAllText( ScriptFilename, ScriptText );

			string LogFilename = Path.GetTempFileName();
			Arguments = " /script=\"" + ScriptFilename + "\"" + " /log=\"" + LogFilename + "\"";

			// Execute command.
			CommandProcess.StartInfo.Arguments = Arguments;
			ErrString = "";
			OutString = "";
			try
			{
				CommandProcess.Start();
				ErrString = this.Reader2String( CommandProcess.StandardError );
				if( ErrString.Length > 0 )
				{ throw new Exception( ErrString ); }
				CommandProcess.WaitForExit();
				OutString = this.Reader2String( CommandProcess.StandardOutput );
			}
			catch( Exception ex )
			{ throw ex; }

			string[] rgLines = File.ReadAllLines( LogFilename );
			System.Collections.Generic.List<string> LogLines = new System.Collections.Generic.List<string>();
			foreach( string LogLine in rgLines )
			{
				if( !LogLine.StartsWith( "<" ) ) { continue; }
				if( LogLine.Length < 27 ) { continue; }
				string line = LogLine.Substring( 26 );
				if( line.StartsWith( "WinSCP:" ) ) { continue; }
				if( line.Length == 0 ) { continue; }
				LogLines.Add( line );

				//TODO: Detect and throw errors.

			}
			OutString = string.Join( "\n", LogLines.ToArray() );

			// Return, OK.
			return OutString;
		}


	}


}


//public class WinScpFileSystem : FileSystemProvider
//{


//    //---------------------------------------------------------------------
//    private string _WinScpFilename = "";
//    public string WinScpFilename
//    {
//        get { return this._WinScpFilename; }
//        set { this._WinScpFilename = value; }
//    }


//    //---------------------------------------------------------------------
//    private string _WinScpConnectionString = "";
//    public string WinScpConnectionString
//    {
//        get { return this._WinScpConnectionString; }
//        set { this._WinScpConnectionString = value; }
//    }


//    //---------------------------------------------------------------------
//    private string _Root = "";
//    public string Root
//    {
//        get { return this._Root; }
//        set { this._Root = value; }
//    }


//    //---------------------------------------------------------------------
//    public WinScpFileSystem()
//    {
//        this._WinScpFilename = "";
//        this._WinScpConnectionString = "";
//        this._Root = "";
//        return;
//    }
//    public WinScpFileSystem( string ThisWinScpFilename, string ThisWinScpConnectionString )
//    {
//        this._WinScpFilename = ThisWinScpFilename;
//        this._WinScpConnectionString = ThisWinScpConnectionString;
//        this._Root = "";
//        return;
//    }
//    public WinScpFileSystem( string ThisWinScpFilename, string ThisWinScpConnectionString, string ThisRoot )
//    {
//        this._WinScpFilename = ThisWinScpFilename;
//        this._WinScpConnectionString = ThisWinScpConnectionString;
//        this._Root = ThisRoot;
//        return;
//    }


//    //---------------------------------------------------------------------
//    private FileSystemItem ItemFromLsOutput( string ItemPath, string LsOutputLine )
//    {
//        if( LsOutputLine.Length < 35 ) { return null; }
//        string Type = LsOutputLine.Substring( 0, 1 );
//        if( Type.Equals( "l" ) ) { return null; }
//        string Permissions = LsOutputLine.Substring( 1, 9 );
//        string LinkCount = LsOutputLine.Substring( 10, 5 ).Trim();
//        string Owner = LsOutputLine.Substring( 16, 8 ).Trim();
//        string Group = LsOutputLine.Substring( 25, 8 ).Trim();
//        string remainder = LsOutputLine.Substring( 33 ).Trim();
//        int ich = remainder.IndexOf( " " );
//        if( ich < 0 ) { return null; }
//        string Size = remainder.Substring( 0, ich );
//        remainder = remainder.Substring( ich + 1 ).Trim();
//        string Date = remainder.Substring( 0, 6 );
//        remainder = remainder.Substring( 7 ).Trim();
//        string Time = remainder.Substring( 0, 5 );
//        remainder = remainder.Substring( 6 ).Trim();
//        string Name = remainder;

//        FileSystemItem item = new FileSystemItem();
//        item.Path = ItemPath;
//        item.Name = Name;
//        item.Exists = true;
//        item.IsFolder = Type.Equals( "d" );
//        item.Size = Convert.ToInt64( Size );
//        item.DateLastWrite = DateTime.Parse( Date + " " + Time );

//        return item;
//    }

//    //---------------------------------------------------------------------
//    private string[] ExecuteCommand( string WinScpCommand )
//    {
//        if( this._WinScpFilename.Length == 0 ) { throw new Exceptions.InvalidOperationException( "ExecuteCommand", "WinScpFilename is undefined" ); }
//        if( this._WinScpConnectionString.Length == 0 ) { throw new Exceptions.InvalidOperationException( "ExecuteCommand", "WinScpConnectionString is undefined" ); }

//        string ScriptFilename = Path.GetTempFileName();
//        string ScriptText =
//        @"
//				option batch on
//				option confirm off
//				open " + this._WinScpConnectionString + @"
//				" + WinScpCommand + @"
//				close
//				exit
//			";
//        File.WriteAllText( ScriptFilename, ScriptText );

//        string LogFilename = Path.GetTempFileName();
//        string Arguments =
//            " /script=\"" + ScriptFilename + "\""
//            + " /log=\"" + LogFilename + "\"";

//        Process Process = Process.Start( this._WinScpFilename, Arguments );
//        Process.WaitForExit();

//        System.Collections.Generic.List<string> LogLines = new System.Collections.Generic.List<string>();
//        foreach( string LogLine in File.ReadAllLines( LogFilename ) )
//        {
//            if( !LogLine.StartsWith( "<" ) ) { continue; }
//            if( LogLine.Length < 27 ) { continue; }
//            string line = LogLine.Substring( 26 );
//            if( line.StartsWith( "WinSCP:" ) ) { continue; }
//            if( line.Length == 0 ) { continue; }
//            LogLines.Add( line );

//            //TODO: Detect and throw errors.

//        }

//        return LogLines.ToArray();
//    }

//    //---------------------------------------------------------------------
//    public override void List( Commands.ListEntriesCommand Command )
//    {
//        if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "List", "FileSystem root is undefined" ); }
//        string item_path = FileSystemPathname.PathnameCombine( this._Root, Command.in_Path, "/" );

//        // Get file listing.
//        string WinScpCommand = "ls " + item_path;
//        string[] lines = ExecuteCommand( WinScpCommand );

//        // Parse listing output.
//        FileSystemItemList items = new FileSystemItemList();
//        foreach( string line in lines )
//        {
//            FileSystemItem item = this.ItemFromLsOutput( item_path, line );
//            if( item != null )
//            {
//                if( item.IsFolder )
//                {
//                    if( Command.in_IncludeFolders )
//                    {
//                        items.AddSorted( item );
//                    }
//                }
//                else
//                {
//                    if( Command.in_IncludeFiles )
//                    {
//                        items.AddSorted( item );
//                    }
//                }
//            }
//        }

//        // Return, OK.
//        Command.out_ItemList = items;
//        return;
//    }

//    //---------------------------------------------------------------------
//    public override void Create( Commands.CreateItemCommand Command )
//    {
//        if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Create", "FileSystem root is undefined" ); }
//        string item_path = FileSystemPathname.PathnameCombine( this._Root, Command.in_Pathname, "/" );

//        //if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Create", "FileSystem root is undefined" ); }
//        //string item_path_name = Pathname.Append( this._Root, Command.in_Path );
//        //string item_path = FileSystemProvider.PathnameGetPath( item_path_name, "\\" );
//        //if( Directory.Exists( item_path ) == false ) { throw new InvalidOperationException(); }
//        //if( Command.in_IsFolder )
//        //{
//        //    Directory.CreateDirectory( item_path_name );
//        //}
//        //else
//        //{
//        //    FileStream stream = File.Create( item_path_name );
//        //    stream.Close();
//        //}
//        //Command.out_Item = this.ItemFromPathname( item_path_name );
//        return;
//    }

//    //---------------------------------------------------------------------
//    public override void Read( Commands.ReadItemCommand Command )
//    {
//        if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Read", "FileSystem root is undefined" ); }
//        string item_path = FileSystemPathname.PathnameCombine( this._Root, Command.in_Pathname, "/" );

//        // Get file listing.
//        string WinScpCommand = "ls " + item_path;
//        string[] lines = ExecuteCommand( WinScpCommand );

//        // Parse listing output.
//        FileSystemItem item = this.ItemFromLsOutput( Command.in_Pathname, lines[0] );

//        // Return, OK.
//        Command.out_Item = item;
//        return;
//    }

//    //---------------------------------------------------------------------
//    public override void Update( Commands.UpdateItemCommand Command )
//    {
//        if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Update", "FileSystem root is undefined" ); }
//        string item_path = FileSystemPathname.PathnameCombine( this._Root, Command.in_Pathname, "/" );

//        // Check file.
//        Commands.ReadItemCommand ReadCmd = new Commands.ReadItemCommand();
//        ReadCmd.in_Pathname = item_path;
//        this.Read( ReadCmd );
//        if( ReadCmd.out_Item == null ) { throw new Exceptions.InvalidOperationException( "Update", "Item does not exist." ); }

//        // Return, OK.
//        Command.out_Item = ReadCmd.out_Item;
//        return;
//    }

//    //---------------------------------------------------------------------
//    public override void Delete( Commands.DeleteItemCommand Command )
//    {
//        if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "Delete", "FileSystem root is undefined" ); }
//        string item_path = FileSystemPathname.PathnameCombine( this._Root, Command.in_Pathname, "/" );

//        string WinScpCommand = "";
//        if( Command.in_IsFolder )
//        {
//            WinScpCommand = "rm " + item_path;
//        }
//        else
//        {
//            WinScpCommand = "rmdir " + item_path;
//        }
//        string[] lines = ExecuteCommand( WinScpCommand );

//        // Return, OK.
//        return;
//    }

//    //---------------------------------------------------------------------
//    public override void ReadFileContent( Commands.ReadFileContentCommand Command )
//    {
//        if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "ReadContent", "FileSystem root is undefined" ); }
//        string item_path = FileSystemPathname.PathnameCombine( this._Root, Command.in_Pathname, "/" );

//        // Check file.
//        Commands.ReadItemCommand ReadCmd = new Commands.ReadItemCommand();
//        ReadCmd.in_Pathname = item_path;
//        this.Read( ReadCmd );
//        if( ReadCmd.out_Item == null )
//        { throw new Exceptions.InvalidOperationException( "ReadContent", "Item does not exist." ); }
//        if( ReadCmd.out_Item.IsFolder )
//        { throw new Exceptions.InvalidOperationException( "ReadContent", "Cannot read content from a folder." ); }
//        if( (Command.in_Offset < 0) || (Command.in_Offset >= ReadCmd.out_Item.Size) )
//        { throw new Exceptions.InvalidOperationException( "ReadContent", "Offset is outside of expected range." ); }

//        // Get file.
//        string TempFilename = Path.GetTempFileName();
//        string WinScpCommand = "get -preservetime " + item_path + " " + TempFilename;
//        string[] lines = ExecuteCommand( WinScpCommand );

//        // Read file bytes.
//        using( FileStream stream = File.OpenRead( TempFilename ) )
//        {
//            long nLength = Command.in_Length;
//            if( nLength < 0 )
//            { nLength = (stream.Length - Command.in_Offset); }
//            if( (Command.in_Offset + nLength) > stream.Length )
//            { nLength = (stream.Length - Command.in_Offset); }
//            byte[] bytes = new byte[nLength];
//            stream.Position = Command.in_Offset;
//            int cb = stream.Read( bytes, 0, bytes.Length );
//            if( cb != nLength )
//            { throw new Exceptions.InvalidOperationException( "ReadContent", "Read error." ); }
//            Command.out_Content = bytes;
//        }

//        // Return, OK.
//        return;
//    }

//    //---------------------------------------------------------------------
//    public override void WriteFileContent( Commands.WriteFileContentCommand Command )
//    {
//        //if( this._Root.Length == 0 ) { throw new Exceptions.InvalidOperationException( "WriteContent", "FileSystem root is undefined" ); }
//        //string item_path_name = Pathname.Append( this._Root, Command.in_Path );
//        //FileSystemItem item = this.ItemFromPathname( item_path_name );
//        //if( item.IsFolder )
//        //{
//        //    throw new Exceptions.InvalidOperationException( "WriteContent", "Cannot write content to a folder." );
//        //}
//        //else
//        //{
//        //    using( FileStream stream = File.OpenWrite( item_path_name ) )
//        //    {
//        //        if( (Command.in_Offset < 0) || (Command.in_Offset >= stream.Length) )
//        //        { throw new Exceptions.InvalidOperationException( "WriteContent", "Offset is outside of expected range." ); }
//        //        stream.Position = Command.in_Offset;
//        //        stream.Write( Command.in_Content, 0, Command.in_Content.Length );
//        //        if( Command.in_Truncate )
//        //        { stream.SetLength( stream.Position ); }
//        //    }
//        //    Command.out_Item = this.ItemFromPathname( item_path_name );
//        //    return;
//        //}



//        //$Command = "put -preservetime " + $Record.LocalPath + " " + $ThisRemoteFolder + '/'
//        //$Lines = WinScp-Command $ThisConnectionString $Command $False



//        return;
//    }

//}
