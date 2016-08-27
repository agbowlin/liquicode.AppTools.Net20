
using System;
using System.Collections.Generic;

namespace liquicode.AppTools
{

	public class FileSystemItemList : List<FileSystemItem>
	{

		//---------------------------------------------------------------------
		public void AddSorted( FileSystemItem Item )
		{
			for( int ndx = 0; ndx < this.Count; ndx++ )
			{
				if( string.Compare( this[ ndx ].Pathname, Item.Pathname, true ) >= 0 )
				{
					this.Insert( ndx, Item );
					return;
				}
			}
			this.Add( Item );
			return;
		}


	}

}
