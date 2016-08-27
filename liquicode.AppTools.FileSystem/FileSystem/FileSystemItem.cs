
using System;

namespace liquicode.AppTools
{

	[Serializable]
	public class FileSystemItem
		: ICloneable, IComparable, IEquatable<FileSystemItem>
	{

		//---------------------------------------------------------------------
		public static readonly string PathDelimiter = "\\";


		//---------------------------------------------------------------------
		private bool _Exists = false;
		private bool _IsFolder = false;
		private bool _IsLink = false;
		private Pathname _Pathname = "";
		private string _LinkTarget = "";
		private DateTime? _DateCreated = null;
		private DateTime? _DateLastRead = null;
		private DateTime? _DateLastWrite = null;
		private long? _Size = null;


		//---------------------------------------------------------------------
		public bool Exists
		{
			get { return this._Exists; }
			set { this._Exists = value; }
		}

		//---------------------------------------------------------------------
		public bool IsFolder
		{
			get { return this._IsFolder; }
			set { this._IsFolder = value; }
		}

		//---------------------------------------------------------------------
		public bool IsLink
		{
			get { return this._IsLink; }
			set { this._IsLink = value; }
		}

		//---------------------------------------------------------------------
		public Pathname Pathname
		{
			get { return this._Pathname; }
			set { this._Pathname = value; }
		}

		//---------------------------------------------------------------------
		public string Path
		{
			get { return this._Pathname.Path; }
			set { this._Pathname.Path = value; }
		}

		//---------------------------------------------------------------------
		public string Name
		{
			get { return this._Pathname.Name; }
			set { this._Pathname.Name = value; }
		}

		//---------------------------------------------------------------------
		public string LinkTarget
		{
			get { return this._LinkTarget; }
			set { this._LinkTarget = value; }
		}

		//---------------------------------------------------------------------
		public DateTime? DateCreated
		{
			get { return this._DateCreated; }
			set { this._DateCreated = value; }
		}

		//---------------------------------------------------------------------
		public DateTime? DateLastRead
		{
			get { return this._DateLastRead; }
			set { this._DateLastRead = value; }
		}

		//---------------------------------------------------------------------
		public DateTime? DateLastWrite
		{
			get { return this._DateLastWrite; }
			set { this._DateLastWrite = value; }
		}

		//---------------------------------------------------------------------
		public long? Size
		{
			get { return this._Size; }
			set { this._Size = value; }
		}

		//---------------------------------------------------------------------
		public FileSystemItem()
		{
			return;
		}

		//---------------------------------------------------------------------
		public FileSystemItem( string ThisPathname, bool ThisIsFolder )
		{
			this._Pathname = ThisPathname;
			this._IsFolder = ThisIsFolder;
			if( this._IsFolder )
			{
				if( string.Equals( this._Pathname.Identity, this._Pathname.Name ) == false )
				{
					this._Pathname = Pathname.Append( this._Pathname, "." );
				}
			}
			return;
		}

		//---------------------------------------------------------------------
		public FileSystemItem( string ThisPathname, bool ThisIsFolder, bool ThisExists )
			: this( ThisPathname, ThisIsFolder )
		{
			this._Exists = ThisExists;
			return;
		}

		//---------------------------------------------------------------------
		public FileSystemItem( string ThisPath, string ThisName, bool ThisIsFolder )
		{
			this._Pathname = Pathname.Append( ThisPath, ThisName );
			this._IsFolder = ThisIsFolder;
			if( this._IsFolder )
			{
				if( string.Equals( this._Pathname.Identity, this._Pathname.Name ) == false )
				{
					this._Pathname = Pathname.Append( this._Pathname, "." );
				}
			}
			return;
		}

		//---------------------------------------------------------------------
		public FileSystemItem( string ThisPath, string ThisName, bool ThisIsFolder, bool ThisExists )
			: this( ThisPath, ThisName, ThisIsFolder )
		{
			this._Exists = ThisExists;
			return;
		}

		//---------------------------------------------------------------------
		public FileSystemItem( FileSystemItem ThisParent, string ThisName, bool ThisIsFolder )
			: this( ThisParent.Pathname, ThisName, ThisIsFolder )
		{
			return;
		}

		//---------------------------------------------------------------------
		public FileSystemItem( FileSystemItem ThisParent, string ThisName, bool ThisIsFolder, bool ThisExists )
			: this( ThisParent.Pathname, ThisName, ThisIsFolder, ThisExists )
		{
			return;
		}

		////---------------------------------------------------------------------
		//public string Pathname
		//{
		//    get
		//    {
		//        //System.IO.Path.Combine( this._Path, this._Name );
		//        Pathname path = new Pathname();
		//        path.Path = this._Path;
		//        path.Name = this._Name;
		//        return path;
		//    }
		//    set
		//    {
		//        this._Path = System.IO.Path.GetDirectoryName( value );
		//        this._Name = System.IO.Path.GetFileName( value );
		//        //this._Path = value.Substring( 0, (value.Length - this._Name.Length) );
		//    }
		//}


		//---------------------------------------------------------------------
		Object ICloneable.Clone()
		{
			FileSystemItem Item = new FileSystemItem();
			Item.Exists = this._Exists;
			Item.IsFolder = this._IsFolder;
			Item.IsLink = this._IsLink;
			Item.Pathname = this._Pathname.ToString();
			Item.LinkTarget = this._LinkTarget;
			Item.DateCreated = this._DateCreated;
			Item.DateLastRead = this._DateLastRead;
			Item.DateLastWrite = this._DateLastWrite;
			Item.Size = this._Size;
			return Item;
		}


		//---------------------------------------------------------------------
		int IComparable.CompareTo( Object Obj )
		{
			if( Obj == null ) { return 1; }
			FileSystemItem Item = (FileSystemItem)Obj;
			int iCompare = 0;
			if( this._IsFolder == true )
			{
				if( Item.IsFolder == false )
				{
					iCompare = -1;
				}
				else
				{
					iCompare = 0;
				}
			}
			else if( this._IsLink == true )
			{
				if( Item.IsFolder == true )
				{
					iCompare = 1;
				}
				else if( Item.IsLink == false )
				{
					iCompare = -1;
				}
				else
				{
					iCompare = 0;
				}
			}
			else
			{
				if( Item.IsFolder == true )
				{
					iCompare = 1;
				}
				else if( Item.IsLink == true )
				{
					iCompare = 1;
				}
				else
				{
					iCompare = 0;
				}
			}
			if( iCompare == 0 )
			{ iCompare = string.Compare( this._Pathname, Item.Pathname ); }
			return iCompare;
		}


		//---------------------------------------------------------------------
		bool IEquatable<FileSystemItem>.Equals( FileSystemItem Item )
		{
			if( Item == null ) { return false; }
			if( bool.Equals( this._Exists, Item.Exists ) == false ) { return false; }
			if( bool.Equals( this._IsFolder, Item.IsFolder ) == false ) { return false; }
			if( bool.Equals( this._IsLink, Item.IsLink ) == false ) { return false; }
			if( string.Equals( this._Pathname, Item.Pathname ) == false ) { return false; }
			if( string.Equals( this._LinkTarget, Item.LinkTarget ) == false ) { return false; }
			if( DateTime.Equals( this._DateCreated, Item.DateCreated ) == false ) { return false; }
			if( DateTime.Equals( this._DateLastRead, Item.DateLastRead ) == false ) { return false; }
			if( DateTime.Equals( this._DateLastWrite, Item.DateLastWrite ) == false ) { return false; }
			if( long.Equals( this._Size, Item.Size ) == false ) { return false; }
			return true;
		}


		//---------------------------------------------------------------------
		public static ComparisonResult CompareItems( FileSystemItem Item1, FileSystemItem Item2, FileSystemFields Fields )
		{
			int iCompare = 0;
			if( Fields.Path )
			{
				iCompare = string.Compare( Item1.Path, Item2.Path );
				if( iCompare < 0 ) { return ComparisonResult.Item1IsLesser; }
				if( iCompare > 0 ) { return ComparisonResult.Item1IsGreater; }
			}
			if( Fields.Name )
			{
				iCompare = string.Compare( Item1.Name, Item2.Name );
				if( iCompare < 0 ) { return ComparisonResult.Item1IsLesser; }
				if( iCompare > 0 ) { return ComparisonResult.Item1IsGreater; }
			}
			if( Fields.LinkTarget )
			{
				iCompare = string.Compare( Item1.LinkTarget, Item2.LinkTarget );
				if( iCompare < 0 ) { return ComparisonResult.Item1IsLesser; }
				if( iCompare > 0 ) { return ComparisonResult.Item1IsGreater; }
			}
			if( Fields.DateCreated )
			{
				iCompare = DateTime.Compare( Item1.DateCreated.Value, Item2.DateCreated.Value );
				if( iCompare < 0 ) { return ComparisonResult.Item1IsLesser; }
				if( iCompare > 0 ) { return ComparisonResult.Item1IsGreater; }
			}
			if( Fields.DateLastRead )
			{
				iCompare = DateTime.Compare( Item1.DateLastRead.Value, Item2.DateLastRead.Value );
				if( iCompare < 0 ) { return ComparisonResult.Item1IsLesser; }
				if( iCompare > 0 ) { return ComparisonResult.Item1IsGreater; }
			}
			if( Fields.DateLastWrite )
			{
				iCompare = DateTime.Compare( Item1.DateLastWrite.Value, Item2.DateLastWrite.Value );
				if( iCompare < 0 ) { return ComparisonResult.Item1IsLesser; }
				if( iCompare > 0 ) { return ComparisonResult.Item1IsGreater; }
			}
			if( Fields.Size )
			{
				if( Item1.Size.Value < Item2.Size.Value ) { return ComparisonResult.Item1IsLesser; }
				if( Item1.Size.Value > Item2.Size.Value ) { return ComparisonResult.Item1IsGreater; }
			}
			return ComparisonResult.Equal;
		}


	}


}
