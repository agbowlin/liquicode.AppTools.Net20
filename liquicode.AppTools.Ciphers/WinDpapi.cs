

using System;
using System.Runtime.InteropServices;


namespace liquicode.AppTools
{
	public static class WinDpapi
	{


		//=================================================
		//       DPAPI DECLARATIONS
		//=================================================


		//---------------------------------------------------------------------
		// Wrapper for DPAPI CryptProtectData function.
		[
			DllImport
			(
				  "crypt32.dll"
				, SetLastError = true
				, CharSet = CharSet.Auto
			)
		]
		private static extern bool CryptProtectData(
										ref DATA_BLOB pPlainText
										, string szDescription
										, ref DATA_BLOB pEntropy
										, IntPtr pReserved
										, ref CRYPTPROTECT_PROMPTSTRUCT pPrompt
										, int dwFlags
										, ref DATA_BLOB pCipherText );


		//---------------------------------------------------------------------
		// Wrapper for DPAPI CryptUnprotectData function.
		[
			DllImport
			(
				  "crypt32.dll"
				, SetLastError = true
				, CharSet = CharSet.Auto
			)
		]
		private static extern bool CryptUnprotectData(
										ref DATA_BLOB pCipherText
										, ref string pszDescription
										, ref DATA_BLOB pEntropy
										, IntPtr pReserved
										, ref CRYPTPROTECT_PROMPTSTRUCT pPrompt
										, int dwFlags
										, ref DATA_BLOB pPlainText );


		//---------------------------------------------------------------------
		// BLOB structure used to pass data to DPAPI functions.
		[
			StructLayout
			(
				LayoutKind.Sequential
				, CharSet = CharSet.Unicode
			)
		]
		private struct DATA_BLOB
		{
			public int cbData;
			public IntPtr pbData;
		}


		//---------------------------------------------------------------------
		// Prompt structure to be used for required parameters.
		[
			StructLayout
			(
				LayoutKind.Sequential
				, CharSet = CharSet.Unicode
			)
		]
		private struct CRYPTPROTECT_PROMPTSTRUCT
		{
			public int cbSize;
			public int dwPromptFlags;
			public IntPtr hwndApp;
			public string szPrompt;
		}


		//---------------------------------------------------------------------
		// DPAPI key initialization flags.
		private const int CRYPTPROTECT_UI_FORBIDDEN = 1;
		private const int CRYPTPROTECT_LOCAL_MACHINE = 4;


		//---------------------------------------------------------------------
		public enum KeyType
		{
			UserKey = 1,
			MachineKey
		}


		//=================================================
		//
		//       DPAPI PRIVATE METHODS
		//
		//=================================================


		//---------------------------------------------------------------------
		private class ProtectBlob
		{
			public DATA_BLOB BLOB = new DATA_BLOB();
			public ProtectBlob()
			{
				BLOB.cbData = 0;
				BLOB.pbData = IntPtr.Zero;
				return;
			}
			public ProtectBlob( byte[] Bytes_in )
			{
				BLOB.cbData = 0;
				BLOB.pbData = IntPtr.Zero;
				if( (Bytes_in != null) )
				{
					BLOB.pbData = Marshal.AllocHGlobal( Bytes_in.Length );
					if( BLOB.pbData.Equals( IntPtr.Zero ) )
						throw new OutOfMemoryException();
					BLOB.cbData = Bytes_in.Length;
					Marshal.Copy( Bytes_in, 0, BLOB.pbData, Bytes_in.Length );
				}
				return;
			}
			~ProtectBlob()
			{
				Free();
			}
			public void Free()
			{
				if( !BLOB.pbData.Equals( IntPtr.Zero ) )
				{
					Marshal.FreeHGlobal( BLOB.pbData );
				}
				BLOB.cbData = 0;
				BLOB.pbData = IntPtr.Zero;
				return;
			}
		}


		//---------------------------------------------------------------------
		private static void InitPrompt( ref CRYPTPROTECT_PROMPTSTRUCT PromptStruct )
		{
			PromptStruct.cbSize = Marshal.SizeOf( typeof( CRYPTPROTECT_PROMPTSTRUCT ) );
			PromptStruct.dwPromptFlags = 0;
			PromptStruct.hwndApp = IntPtr.Zero;
			PromptStruct.szPrompt = null;
		}


		//---------------------------------------------------------------------
		private static string Bytes2String( byte[] Bytes )
		{
			if( Bytes == null ) { return ""; }
			if( Bytes.Length == 0 ) { return ""; }
			string String = System.Text.Encoding.UTF8.GetString( Bytes );
			return String;
		}


		//---------------------------------------------------------------------
		private static byte[] String2Bytes( string String )
		{
			if( String == null ) { return new byte[] { }; }
			if( String.Length == 0 ) { return new byte[] { }; }
			byte[] Bytes = System.Text.Encoding.UTF8.GetBytes( String );
			return Bytes;
		}


		//---------------------------------------------------------------------
		private static string Bytes2Base64String( byte[] Bytes )
		{
			if( Bytes == null ) { return ""; }
			if( Bytes.Length == 0 ) { return ""; }
			string String = Convert.ToBase64String( Bytes );
			return String;
		}


		//---------------------------------------------------------------------
		private static byte[] Base64String2Bytes( string String )
		{
			if( String == null ) { return new byte[] { }; }
			if( String.Length == 0 ) { return new byte[] { }; }
			byte[] Bytes = Convert.FromBase64String( String );
			return Bytes;
		}


		//=================================================
		//
		//       DPAPI ENCRYPTION METHODS
		//
		//=================================================


		//---------------------------------------------------------------------
		public static string ProtectString( string PlainText, KeyType KeyType, string Entropy, string Description )
		{
			if( PlainText == null ) { return ""; }
			if( PlainText.Length == 0 ) { return ""; }
			if( Entropy.Length == 0 ) { Entropy = string.Empty; }
			byte[] plain_text_bytes = String2Bytes( PlainText );
			byte[] entropy_bytes = String2Bytes( Entropy );
			byte[] cipher_text_bytes = ProtectBytes( plain_text_bytes, KeyType, entropy_bytes, Description );
			string cipher_text = Bytes2Base64String( cipher_text_bytes );
			return cipher_text;
		}



		//---------------------------------------------------------------------
		public static byte[] ProtectBytes( byte[] PlainTextBytes, KeyType KeyType, byte[] EntropyBytes, string Description )
		{
			byte[] cipher_text_bytes = { };

			// Validate Parameters.
			if( PlainTextBytes == null ) { return cipher_text_bytes; }
			if( PlainTextBytes.Length == 0 ) { return cipher_text_bytes; }
			if( EntropyBytes == null ) { EntropyBytes = new byte[] { }; }
			if( Description == null ) { Description = string.Empty; }

			ProtectBlob plain_text = new ProtectBlob( PlainTextBytes );
			ProtectBlob entropy = new ProtectBlob( EntropyBytes );
			ProtectBlob cipher_text = new ProtectBlob();

			CRYPTPROTECT_PROMPTSTRUCT prompt_struct = new CRYPTPROTECT_PROMPTSTRUCT();
			InitPrompt( ref prompt_struct );

			// Get the Flags
			int flags = CRYPTPROTECT_UI_FORBIDDEN;
			if( KeyType == KeyType.MachineKey ) { flags = flags | CRYPTPROTECT_LOCAL_MACHINE; }

			// Call DPAPI to encrypt data.
			bool success = CryptProtectData(
								ref plain_text.BLOB
								, Description
								, ref entropy.BLOB
								, IntPtr.Zero
								, ref prompt_struct
								, flags
								, ref cipher_text.BLOB );
			plain_text.Free();
			entropy.Free();
			if( !success )
			{
				throw new System.ComponentModel.Win32Exception( Marshal.GetLastWin32Error() );
			}

			// Copy the BLOB.
			cipher_text_bytes = new byte[ cipher_text.BLOB.cbData ];
			Marshal.Copy( cipher_text.BLOB.pbData, cipher_text_bytes, 0, cipher_text.BLOB.cbData );
			cipher_text.Free();

			// Return Encrypted Bytes
			return cipher_text_bytes;
		}


		//=================================================
		//
		//       DPAPI DECRYPTION METHODS
		//
		//=================================================


		//---------------------------------------------------------------------
		public static string UnprotectString( string CipherText, KeyType KeyType, string Entropy, string Description )
		{
			if( CipherText == null ) { return ""; }
			if( CipherText.Length == 0 ) { return ""; }
			if( Entropy.Length == 0 ) { Entropy = string.Empty; }
			byte[] cipher_bytes = Base64String2Bytes( CipherText );
			byte[] entropy_bytes = String2Bytes( Entropy );
			byte[] plain_text_bytes = UnprotectBytes( cipher_bytes, KeyType, entropy_bytes, Description );
			string plain_text = Bytes2String( plain_text_bytes );
			return plain_text;
		}


		//---------------------------------------------------------------------
		public static byte[] UnprotectBytes( byte[] CipherTextBytes, KeyType KeyType, byte[] EntropyBytes, string Description )
		{
			byte[] plain_text_bytes = { };

			// Validate Parameters.
			if( CipherTextBytes == null ) { return plain_text_bytes; }
			if( CipherTextBytes.Length == 0 ) { return plain_text_bytes; }
			if( EntropyBytes == null ) { EntropyBytes = new byte[] { }; }
			if( Description == null ) { Description = string.Empty; }

			ProtectBlob cipher_text = new ProtectBlob( CipherTextBytes );
			ProtectBlob entropy = new ProtectBlob( EntropyBytes );
			ProtectBlob plain_text = new ProtectBlob();

			CRYPTPROTECT_PROMPTSTRUCT prompt_struct = new CRYPTPROTECT_PROMPTSTRUCT();
			InitPrompt( ref prompt_struct );

			// Get the Flags
			int flags = CRYPTPROTECT_UI_FORBIDDEN;
			if( (KeyType == KeyType.MachineKey) )
				flags = flags | CRYPTPROTECT_LOCAL_MACHINE;

			// Call DPAPI to decrypt data.
			bool success = CryptUnprotectData(
								ref cipher_text.BLOB
								, ref Description
								, ref entropy.BLOB
								, IntPtr.Zero
								, ref prompt_struct
								, flags
								, ref plain_text.BLOB );
			cipher_text.Free();
			entropy.Free();
			if( !success )
			{
				throw new System.ComponentModel.Win32Exception( Marshal.GetLastWin32Error() );
			}

			// Copy the BLOB.
			plain_text_bytes = new byte[ plain_text.BLOB.cbData ];
			Marshal.Copy( plain_text.BLOB.pbData, plain_text_bytes, 0, plain_text.BLOB.cbData );
			plain_text.Free();

			// Return Encrypted Bytes
			return plain_text_bytes;
		}


	}
}
