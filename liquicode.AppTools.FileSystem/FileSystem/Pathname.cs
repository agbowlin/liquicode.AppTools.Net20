

using System;
using System.Collections.Generic;

namespace liquicode.AppTools
{

	public class Pathname
		: ICloneable
		, IComparable<Pathname>, IComparable<string>
		, IEquatable<Pathname>, IEquatable<string>
	{

		//---------------------------------------------------------------------
		public static string[] Separators = new string[] { "/", "\\" };


		//---------------------------------------------------------------------
		protected string _Separator = "/";
		protected string _Identity = ".";
		protected List<string> _Items = new List<string>();


		//---------------------------------------------------------------------
		public string Separator
		{
			get { return this._Separator; }
			set { this._Separator = value; }
		}


		//---------------------------------------------------------------------
		public string Identity
		{
			get { return this._Identity; }
			set { this._Identity = value; }
		}


		//---------------------------------------------------------------------
		public List<string> Items
		{
			get { return this._Items; }
		}


		//---------------------------------------------------------------------
		public static bool Equals( Pathname Pathname1, Pathname Pathname2 )
		{
			if( Pathname1.Items.Count != Pathname2.Items.Count ) { return false; }
			for( int ndx = 0; ndx < Pathname1.Items.Count; ndx++ )
			{
				if( string.Equals( Pathname1.Items[ndx], Pathname2.Items[ndx], StringComparison.InvariantCultureIgnoreCase ) )
				{ }
				else
				{ return false; }
			}
			return true;
		}
		public bool Equals( Pathname Pathname2 )
		{
			return Pathname.Equals( this, Pathname2 );
		}


		//---------------------------------------------------------------------
		public static Pathname Intersect( Pathname Pathname1, Pathname Pathname2 )
		{
			Pathname path = new Pathname();
			for( int ndx = 0; (ndx < Pathname1.Items.Count) && (ndx < Pathname2.Items.Count); ndx++ )
			{
				if( string.Equals( Pathname1.Items[ndx], Pathname2.Items[ndx], StringComparison.InvariantCultureIgnoreCase ) )
				{ path.Items.Add( Pathname1.Items[ndx] ); }
				else
				{ break; }
			}
			return path;
		}
		public Pathname Intersect( Pathname Pathname2 )
		{
			return Pathname.Intersect( this, Pathname2 );
		}


		//---------------------------------------------------------------------
		public static Pathname Append( Pathname Pathname1, Pathname Pathname2 )
		{
			Pathname path = new Pathname();
			if( Pathname1 != null )
			{
				path.Items.Clear();
				path.Items.AddRange( Pathname1.Items.ToArray() );
			}
			if( Pathname2 != null )
			{
				if( Pathname2.Items.Count > 0 )
				{
					if( path.Items.Count > 0 )
					{
						if( string.Equals( path.Identity, path.Name ) )
						{
							path.Items.RemoveAt( path.Items.Count - 1 );
						}
					}
					path.Items.AddRange( Pathname2.Items.ToArray() );
				}
			}
			return path;
		}
		public Pathname Append( Pathname Pathname2 )
		{
			return Pathname.Append( this, Pathname2 );
		}


		//---------------------------------------------------------------------
		public static List<string> PathnameItems( Pathname Pathname )
		{
			return Pathname.Items;
		}


		//---------------------------------------------------------------------
		public string Path
		{
			get
			{
				if( this._Items.Count == 0 ) { return this._Separator; }
				string[] rgs = new string[this._Items.Count - 1];
				Array.Copy( this._Items.ToArray(), rgs, rgs.Length );
				return string.Join( this._Separator, rgs );
			}
			set
			{
				string ThisName = this.Name;
				string[] rgs = value.Split( Pathname.Separators, StringSplitOptions.RemoveEmptyEntries );
				this._Items.Clear();
				this._Items.AddRange( rgs );
				if( ThisName.Length > 0 )
				{
					this._Items.Add( ThisName );
				}
				return;
			}
		}


		//---------------------------------------------------------------------
		public string Parent
		{
			get
			{
				if( this._Items.Count <= 1 )
				{
					return "";
				}
				else
				{
					return this._Items[this._Items.Count - 2];
				}
			}
		}


		//---------------------------------------------------------------------
		public string Name
		{
			get
			{
				if( this._Items.Count == 0 )
				{
					return this._Identity;
				}
				else
				{
					return this._Items[this._Items.Count - 1];
				}
			}
			set
			{
				if( this._Items.Count == 0 )
				{
					this._Items.Add( value );
				}
				else
				{
					this._Items[this._Items.Count - 1] = value;
				}
				return;
			}
		}


		//---------------------------------------------------------------------
		public override string ToString()
		{
			string pathname = this._Identity;
			if( this._Items.Count == 0 )
			{
				pathname = (this._Separator + this._Identity);
			}
			else if( this._Items.Count == 1 )
			{
				pathname = (this._Separator + this._Items[0] );
			}
			else
			{
				pathname = string.Join( this._Separator, this._Items.ToArray() );
			}
			return pathname;
		}
		public void FromString( string value )
		{
			string[] rgs = value.Split( Pathname.Separators, StringSplitOptions.RemoveEmptyEntries );
			this._Items.Clear();
			this._Items.AddRange( rgs );
			return;
		}


		//---------------------------------------------------------------------
		public static implicit operator string( Pathname value )
		{
			return value.ToString();
		}
		public static implicit operator Pathname( string value )
		{
			return new Pathname( value );
		}


		//---------------------------------------------------------------------
		Object ICloneable.Clone()
		{
			return new Pathname( this.ToString() );
		}


		//---------------------------------------------------------------------
		int IComparable<Pathname>.CompareTo( Pathname ThatPathname )
		{
			int iCompare = string.Compare( this.ToString(), ThatPathname.ToString() );
			return iCompare;
		}


		//---------------------------------------------------------------------
		int IComparable<string>.CompareTo( string ThatPathname )
		{
			int iCompare = string.Compare( this.ToString(), ThatPathname );
			return iCompare;
		}


		//---------------------------------------------------------------------
		bool IEquatable<Pathname>.Equals( Pathname ThatPathname )
		{
			if( string.Equals( this.ToString(), ThatPathname.ToString() ) == false ) { return false; }
			return true;
		}


		//---------------------------------------------------------------------
		bool IEquatable<string>.Equals( string ThatPathname )
		{
			if( string.Equals( this.ToString(), ThatPathname ) == false ) { return false; }
			return true;
		}


		//---------------------------------------------------------------------
		public Pathname()
		{
			this._Items.Add( this._Identity );
			return;
		}
		public Pathname( Pathname ThisPathname )
		{
			this.FromString( ThisPathname.ToString() );
			return;
		}
		public Pathname( string ThisPathname )
		{
			this.FromString( ThisPathname );
			return;
		}
		public Pathname( string ThisPath, string ThisName )
		{
			this.Path = ThisPath;
			this.Name = ThisName;
			return;
		}
		public Pathname( string ThisPath, string ThisName, string ThisSeparator )
		{
			this.Path = ThisPath;
			this.Name = ThisName;
			this.Separator = ThisSeparator;
			return;
		}


	}


}
