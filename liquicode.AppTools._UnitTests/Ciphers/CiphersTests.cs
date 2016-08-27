

using System;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture()]
public class CiphersTests
{


	//-------------------------------------------------
	public const string WeakPassword = "password";
	public const string StrongPassword = "sd@wUYervU#Iu0eF";
	public const string Passphrase = "This is my passphrase.";
	public const string Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam pharetra dolor sed justo hendrerit cursus. Suspendisse vel velit non magna ultricies aliquam vel sed nulla. In in risus sit amet lorem ultricies mollis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse ac faucibus orci. In in justo nibh, nec elementum neque. Curabitur vitae viverra arcu. Suspendisse interdum commodo odio, lacinia lacinia turpis luctus eget. Pellentesque rutrum dui nec nulla hendrerit id sodales eros eleifend. Curabitur nec eros vel ante elementum imperdiet.";


	//-------------------------------------------------
	[Test()]
	public void Test_001_Base64String()
	{
		byte[] MessageBytes = DataManagement.String2Bytes( Message );
		string Base64Message = DataManagement.Bytes2Base64String( MessageBytes );
		byte[] Base64Bytes = DataManagement.Base64String2Bytes( Base64Message );
		Assert.AreEqual( MessageBytes.Length, Base64Bytes.Length );
		string MessageText = DataManagement.Bytes2String( Base64Bytes );
		Assert.AreEqual( Message, MessageText );
		return;
	}


	//-------------------------------------------------
	[Test()]
	public void Test_002_HashString()
	{
		int i32a = Ciphers.HashString32( Message, WeakPassword );
		Console.Out.WriteLine( "32-bit Hash with WeakPassword = {0}", i32a );
		int i32b = Ciphers.HashString32( Message, StrongPassword );
		Console.Out.WriteLine( "32-bit Hash with StrongPassword = {0}", i32b );
		int i32c = Ciphers.HashString32( Message, Passphrase );
		Console.Out.WriteLine( "32-bit Hash with Passphrase = {0}", i32c );
		Assert.AreNotEqual( i32a, i32b );
		Assert.AreNotEqual( i32a, i32c );
		Assert.AreNotEqual( i32b, i32c );

		long i64a = Ciphers.HashString64( Message, WeakPassword );
		Console.Out.WriteLine( "64-bit Hash with WeakPassword = {0}", i64a );
		long i64b = Ciphers.HashString64( Message, StrongPassword );
		Console.Out.WriteLine( "64-bit Hash with StrongPassword = {0}", i64b );
		long i64c = Ciphers.HashString64( Message, Passphrase );
		Console.Out.WriteLine( "64-bit Hash with Passphrase = {0}", i64c );
		Assert.AreNotEqual( i64a, i64b );
		Assert.AreNotEqual( i64a, i64c );
		Assert.AreNotEqual( i64b, i64c );

		return;
	}


	//-------------------------------------------------
	private void EncryptStringTest( Ciphers.SymmetricAlgorithms Algorithm )
	{
		Console.Out.WriteLine( "Running EncryptString Tests for {0}", Algorithm );
		string CipherText = "";
		string PlainText = "";

		Console.Out.WriteLine( " * WeakPassword Test." );
		CipherText = Ciphers.EncryptString( Algorithm, Message, WeakPassword );
		PlainText = Ciphers.DecryptString( Algorithm, CipherText, WeakPassword );
		Assert.AreNotEqual( Message, CipherText );
		Assert.AreEqual( Message, PlainText );

		Console.Out.WriteLine( " * StrongPassword Test." );
		CipherText = Ciphers.EncryptString( Algorithm, Message, StrongPassword );
		PlainText = Ciphers.DecryptString( Algorithm, CipherText, StrongPassword );
		Assert.AreNotEqual( Message, CipherText );
		Assert.AreEqual( Message, PlainText );

		Console.Out.WriteLine( " * Passphrase Test." );
		CipherText = Ciphers.EncryptString( Algorithm, Message, Passphrase );
		PlainText = Ciphers.DecryptString( Algorithm, CipherText, Passphrase );
		Assert.AreNotEqual( Message, CipherText );
		Assert.AreEqual( Message, PlainText );

		return;
	}


	//-------------------------------------------------
	[Test()]
	public void Test_003_EncryptString()
	{
		EncryptStringTest( Ciphers.SymmetricAlgorithms.RC2_64_40 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.RC2_64_48 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.RC2_64_56 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.RC2_64_64 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.RC2_64_72 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.RC2_64_80 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.RC2_64_88 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.RC2_64_96 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.RC2_64_104 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.RC2_64_112 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.RC2_64_120 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.RC2_64_128 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.DES_64_64 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.DES3_64_128 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.DES3_64_192 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.AES_128_128 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.AES_128_192 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.AES_128_256 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.AES_192_128 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.AES_192_192 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.AES_192_256 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.AES_256_128 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.AES_256_192 );
		EncryptStringTest( Ciphers.SymmetricAlgorithms.AES_256_256 );
		return;
	}


}
