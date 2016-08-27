using System;
using System.Collections.Generic;
using System.Text;

namespace liquicode.AppTools
{

	public class Exceptions
	{

		//---------------------------------------------------------------------
		public class FileSystemException : ApplicationException
		{
			public FileSystemException( string message, Exception exception )
				: base( message, exception ) { }
			public FileSystemException( string message )
				: this( message, null ) { }
		}

		//---------------------------------------------------------------------
		public class InvalidOperationException : FileSystemException
		{
			public InvalidOperationException( string OperationName, string message, Exception exception )
				: base( "Invalid operation '" + OperationName + "'. " + message, exception ) { }
			public InvalidOperationException( string OperationName, string message )
				: this( OperationName, message, null ) { }
			public InvalidOperationException( string OperationName, Exception exception )
				: base( "Invalid operation '" + OperationName + "'.", exception ) { }
			public InvalidOperationException( string OperationName )
				: this( OperationName, (Exception)null ) { }
		}

		////---------------------------------------------------------------------
		//public class InvalidOperationOnRootItemException : FileSystemException
		//{
		//    public InvalidOperationOnRootItemException( string OperationName, Exception exception )
		//        : base( "Invalid operation '" + OperationName + "' on root item.", exception ) { }
		//    public InvalidOperationOnRootItemException( string OperationName )
		//        : this( OperationName, null ) { }
		//}

		////---------------------------------------------------------------------
		//public class InvalidOperationOnLeafItemException : FileSystemException
		//{
		//    public InvalidOperationOnLeafItemException( string OperationName, Exception exception )
		//        : base( "Invalid operation '" + OperationName + "' on leaf item.", exception ) { }
		//    public InvalidOperationOnLeafItemException( string OperationName )
		//        : this( OperationName, null ) { }
		//}

		////---------------------------------------------------------------------
		//public class RootIsUndefinedException : FileSystemException
		//{
		//    public RootIsUndefinedException( string OperationName, Exception exception )
		//        : base( "Root is undefined in operation '" + OperationName + "'.", exception ) { }
		//    public RootIsUndefinedException( string OperationName )
		//        : this( OperationName, null ) { }
		//}

		////---------------------------------------------------------------------
		//public class ItemAlreadyExistsException : FileSystemException
		//{
		//    public ItemAlreadyExistsException( string OperationName, Exception exception )
		//        : base( "Item already exists in operation '" + OperationName + "'.", exception ) { }
		//    public ItemAlreadyExistsException( string OperationName )
		//        : this( OperationName, null ) { }
		//}

		////---------------------------------------------------------------------
		//public class ItemDoesNotExistException : FileSystemException
		//{
		//    public ItemDoesNotExistException( string OperationName, Exception exception )
		//        : base( "Item does not exist in operation '" + OperationName + "'.", exception ) { }
		//    public ItemDoesNotExistException( string OperationName )
		//        : this( OperationName, null ) { }
		//}

	}

}
