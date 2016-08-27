

using System;
using System.Collections.Generic;
using System.Text;

namespace liquicode.AppTools
{

	public class FileSystemFields
		: ICloneable, IEquatable<FileSystemFields>
	{

		//---------------------------------------------------------------------
		public readonly bool Path = false;
		public readonly bool Name = false;
		public readonly bool LinkTarget = false;
		public readonly bool DateCreated = false;
		public readonly bool DateLastRead = false;
		public readonly bool DateLastWrite = false;
		public readonly bool Size = false;


		//---------------------------------------------------------------------
		public FileSystemFields()
		{
			return;
		}
		public FileSystemFields( bool Value )
		{
			this.Path = Value;
			this.Name = Value;
			this.LinkTarget = Value;
			this.DateCreated = Value;
			this.DateLastRead = Value;
			this.DateLastWrite = Value;
			this.Size = Value;
			return;
		}
		public FileSystemFields
		(
			  bool ThisPath
			, bool ThisName
			, bool ThisLinkTarget
			, bool ThisDateCreated
			, bool ThisDateLastRead
			, bool ThisDateLastWrite
			, bool ThisSize
		)
		{
			this.Path = ThisPath;
			this.Name = ThisName;
			this.LinkTarget = ThisLinkTarget;
			this.DateCreated = ThisDateCreated;
			this.DateLastRead = ThisDateLastRead;
			this.DateLastWrite = ThisDateLastWrite;
			this.Size = ThisSize;
			return;
		}


		//---------------------------------------------------------------------
		public static FileSystemFields Clone( FileSystemFields Fields )
		{
			return new FileSystemFields
			(
				  Fields.Path
				, Fields.Name
				, Fields.LinkTarget
				, Fields.DateCreated
				, Fields.DateLastRead
				, Fields.DateLastWrite
				, Fields.Size
			);
		}


		//---------------------------------------------------------------------
		Object ICloneable.Clone()
		{
			return FileSystemFields.Clone( this );
		}


		//---------------------------------------------------------------------
		bool IEquatable<FileSystemFields>.Equals( FileSystemFields Fields )
		{
			if( Fields == null ) { return false; }
			if( bool.Equals( this.Path, Fields.Path ) == false ) { return false; }
			if( bool.Equals( this.Name, Fields.Name ) == false ) { return false; }
			if( bool.Equals( this.LinkTarget, Fields.LinkTarget ) == false ) { return false; }
			if( bool.Equals( this.DateCreated, Fields.DateCreated ) == false ) { return false; }
			if( bool.Equals( this.DateLastRead, Fields.DateLastRead ) == false ) { return false; }
			if( bool.Equals( this.DateLastWrite, Fields.DateLastWrite ) == false ) { return false; }
			if( bool.Equals( this.Size, Fields.Size ) == false ) { return false; }
			return true;
		}


		//---------------------------------------------------------------------
		public static FileSystemFields AndFields( FileSystemFields Fields1, FileSystemFields Fields2 )
		{
			return new FileSystemFields
			(
				  (Fields1.Path && Fields2.Path)
				, (Fields1.Name && Fields2.Name)
				, (Fields1.LinkTarget && Fields2.LinkTarget)
				, (Fields1.DateCreated && Fields2.DateCreated)
				, (Fields1.DateLastRead && Fields2.DateLastRead)
				, (Fields1.DateLastWrite && Fields2.DateLastWrite)
				, (Fields1.Size && Fields2.Size)
			);
		}


		//---------------------------------------------------------------------
		public static FileSystemFields OrFields( FileSystemFields Fields1, FileSystemFields Fields2 )
		{
			return new FileSystemFields
			(
				  (Fields1.Path || Fields2.Path)
				, (Fields1.Name || Fields2.Name)
				, (Fields1.LinkTarget || Fields2.LinkTarget)
				, (Fields1.DateCreated || Fields2.DateCreated)
				, (Fields1.DateLastRead || Fields2.DateLastRead)
				, (Fields1.DateLastWrite || Fields2.DateLastWrite)
				, (Fields1.Size || Fields2.Size)
			);
		}


		//---------------------------------------------------------------------
		public static FileSystemFields FieldsDateLastWrite = new FileSystemFields( false, false, false, false, false, true, false );
		public static FileSystemFields FieldsSize = new FileSystemFields( false, false, false, false, false, false, true );


	}


}
