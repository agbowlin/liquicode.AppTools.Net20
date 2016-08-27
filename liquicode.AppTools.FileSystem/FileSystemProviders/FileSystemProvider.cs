
using System;
using System.Collections;

namespace liquicode.AppTools
{

	public abstract partial class FileSystemProvider
	{

		//---------------------------------------------------------------------
		public abstract void Settings( Commands.SettingsCommand Command );
		public abstract void List( Commands.ListEntriesCommand Command );
		public abstract void Create( Commands.CreateItemCommand Command );
		public abstract void Read( Commands.ReadItemCommand Command );
		public abstract void Update( Commands.UpdateItemCommand Command );
		public abstract void Delete( Commands.DeleteItemCommand Command );
		public abstract void ReadFileContent( Commands.ReadFileContentCommand Command );
		public abstract void WriteFileContent( Commands.WriteFileContentCommand Command );

		//---------------------------------------------------------------------
		public void Invoke( CommandContext Command )
		{
			if( Command == null )
			{
			}
			else if( Command is Commands.SettingsCommand )
			{
				Commands.SettingsCommand SettingsCommand = (Commands.SettingsCommand)Command;
				this.Settings( SettingsCommand );
			}
			else if( Command is Commands.ListEntriesCommand )
			{
				Commands.ListEntriesCommand ListCommand = (Commands.ListEntriesCommand)Command;
				this.List( ListCommand );
			}
			else if( Command is Commands.CreateItemCommand )
			{
				Commands.CreateItemCommand CreateCommand = (Commands.CreateItemCommand)Command;
				this.Create( CreateCommand );
			}
			else if( Command is Commands.ReadItemCommand )
			{
				Commands.ReadItemCommand ReadCommand = (Commands.ReadItemCommand)Command;
				this.Read( ReadCommand );
			}
			else if( Command is Commands.UpdateItemCommand )
			{
				Commands.UpdateItemCommand UpdateCommand = (Commands.UpdateItemCommand)Command;
				this.Update( UpdateCommand );
			}
			else if( Command is Commands.DeleteItemCommand )
			{
				Commands.DeleteItemCommand DeleteCommand = (Commands.DeleteItemCommand)Command;
				this.Delete( DeleteCommand );
			}
			else if( Command is Commands.ReadFileContentCommand )
			{
				Commands.ReadFileContentCommand ReadContentCommand = (Commands.ReadFileContentCommand)Command;
				this.ReadFileContent( ReadContentCommand );
			}
			else if( Command is Commands.WriteFileContentCommand )
			{
				Commands.WriteFileContentCommand WriteContentCommand = (Commands.WriteFileContentCommand)Command;
				this.WriteFileContent( WriteContentCommand );
			}
			else
			{
			}
			return;
		}

		//---------------------------------------------------------------------
		public Hashtable Settings()
		{
			Commands.SettingsCommand SettingsCommand = new Commands.SettingsCommand();
			this.Settings( SettingsCommand );
			return SettingsCommand.out_Settings;
		}

		//---------------------------------------------------------------------
		public FileSystemItemList List( string Path, bool IncludeFolders, bool IncludeLinks, bool IncludeFiles )
		{
			Commands.ListEntriesCommand ListCommand = new Commands.ListEntriesCommand( Path, IncludeFolders, IncludeLinks, IncludeFiles );
			this.List( ListCommand );
			return ListCommand.out_ItemList;
		}

		//---------------------------------------------------------------------
		public FileSystemItem Create( string Pathname, bool IsFolder, bool CreatePath )
		{
			Commands.CreateItemCommand CreateCommand = new Commands.CreateItemCommand( Pathname, IsFolder, CreatePath );
			this.Create( CreateCommand );
			return CreateCommand.out_Item;
		}

		//---------------------------------------------------------------------
		public FileSystemItem Read( string Pathname )
		{
			Commands.ReadItemCommand ReadCommand = new Commands.ReadItemCommand( Pathname );
			this.Read( ReadCommand );
			return ReadCommand.out_Item;
		}

		//---------------------------------------------------------------------
		public FileSystemItem Update( string Path, bool IsFolder, FileSystemItem Item )
		{
			Commands.UpdateItemCommand UpdateCommand = new Commands.UpdateItemCommand( Path, IsFolder, Item );
			this.Update( UpdateCommand );
			return UpdateCommand.out_Item;
		}

		//---------------------------------------------------------------------
		public void Delete( string Pathname, bool IsFolder )
		{
			Commands.DeleteItemCommand DeleteCommand = new Commands.DeleteItemCommand( Pathname, IsFolder );
			this.Delete( DeleteCommand );
			return;
		}

		//---------------------------------------------------------------------
		public byte[] ReadFileContent( string Pathname, int Offset, int Length )
		{
			Commands.ReadFileContentCommand ReadContentCommand = new Commands.ReadFileContentCommand( Pathname, Offset, Length );
			this.ReadFileContent( ReadContentCommand );
			return ReadContentCommand.out_Content;
		}

		//---------------------------------------------------------------------
		public FileSystemItem WriteFileContent( string Pathname, int Offset, byte[] Content, bool Truncate )
		{
			Commands.WriteFileContentCommand WriteContentCommand = new Commands.WriteFileContentCommand( Pathname, Offset, Content, Truncate );
			this.WriteFileContent( WriteContentCommand );
			return WriteContentCommand.out_Item;
		}


	}


}
