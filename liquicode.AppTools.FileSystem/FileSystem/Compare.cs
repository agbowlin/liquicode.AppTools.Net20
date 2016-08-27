

using System;
using System.Collections.Generic;
using System.Text;

namespace liquicode.AppTools
{

	//---------------------------------------------------------------------
	public enum ComparisonResult
	{
		Item1IsLesser = -1,
		Equal = 0,
		Item1IsGreater = 1
	}


	//---------------------------------------------------------------------
	public class CompareItem
	{
		public FileSystemItem Item1 = null;
		public FileSystemItem Item2 = null;
		public ComparisonResult Comparison = ComparisonResult.Equal;
	}


	//---------------------------------------------------------------------
	public class CompareItemList : List<CompareItem> { }


	//---------------------------------------------------------------------
	public class CompareItemFactory
	{

		//---------------------------------------------------------------------
		public static CompareItemList ComparePaths( FileSystemItem Item1, string Path1, FileSystemItem Item2, string Path2, FileSystemFields Fields )
		{
			
			return null;
		}


	}


}
