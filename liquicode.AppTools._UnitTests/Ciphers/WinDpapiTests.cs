

using System;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture()]
public class WinDpapiTests
{


	//-------------------------------------------------
	public const string WeakPassword = "password";
	public const string StrongPassword = "sd@wUYervU#Iu0eF";
	public const string Passphrase = "This is my passphrase.";
	public const string Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam pharetra dolor sed justo hendrerit cursus. Suspendisse vel velit non magna ultricies aliquam vel sed nulla. In in risus sit amet lorem ultricies mollis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac faucibus orci. In in justo nibh, nec elementum neque. Curabitur vitae viverra arcu. Suspendisse interdum commodo odio, lacinia lacinia turpis luctus eget. Pellentesque rutrum dui nec nulla hendrerit id sodales eros eleifend. Curabitur nec eros vel ante elementum imperdiet.";


	//-------------------------------------------------
	public void Test_ProtectString( bool UseMachineKey, string PasswordType, string Password )
	{
		string CipherText = "";
		string PlainText = "";
		WinDpapi.KeyType key_type = WinDpapi.KeyType.UserKey;

		Console.Out.Write( " * Protect Test: " );
		if( UseMachineKey )
		{
			Console.Out.Write( "MachineKey" );
			key_type = WinDpapi.KeyType.MachineKey;
		}
		else
		{
			Console.Out.Write( "UserKey" );
			key_type = WinDpapi.KeyType.UserKey;
		}
		Console.Out.Write( string.Format( ", {0} [{1}].", PasswordType, Password ) );
		Console.Out.WriteLine();
		Console.Out.WriteLine( string.Format( "Plain Text  = [{0}]", Message ) );

		CipherText = WinDpapi.ProtectString( Message, key_type, Password, "" );
		Console.Out.WriteLine( string.Format( "Cipher Text = [{0}]", CipherText ) );
		Assert.AreNotEqual( Message, CipherText );

		PlainText = WinDpapi.UnprotectString( CipherText, key_type, Password, "" );
		Console.Out.WriteLine( string.Format( "Plain Text  = [{0}]", PlainText ) );
		Assert.AreEqual( Message, PlainText );

		return;
	}


	//-------------------------------------------------
	[Test()]
	public void Test_001_ProtectString()
	{
		Console.Out.WriteLine( "Running DPAPI Tests" );

		Test_ProtectString( false, "WeakPassword", WeakPassword );
		Test_ProtectString( false, "StrongPassword", StrongPassword );
		Test_ProtectString( false, "Passphrase", Passphrase );

		Test_ProtectString( true, "WeakPassword", WeakPassword );
		Test_ProtectString( true, "StrongPassword", StrongPassword );
		Test_ProtectString( true, "Passphrase", Passphrase );

		return;
	}


}
