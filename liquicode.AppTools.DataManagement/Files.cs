

using System;
using System.IO;
using System.Collections.Generic;


namespace liquicode.AppTools
{


	public static class Files
	{


		//---------------------------------------------------------------------
		public static bool FileExistsAndNotEmpty( string Filename )
		{
			if( System.IO.File.Exists( Filename ) == false ) { return false; }
			FileInfo file_info = new FileInfo( Filename );
			if( file_info.Length == 0 ) { return false; }
			return true;
		}


		//---------------------------------------------------------------------
		public static string GetExecutingAssemblyFilename()
		{
			string codebase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
			Uri uri = new Uri( codebase );
			return uri.AbsolutePath;
		}


		//---------------------------------------------------------------------
		public static string GetExecutingAssemblyFolder()
		{
			return Path.GetDirectoryName( Files.GetExecutingAssemblyFilename() );
		}


	}


}
