

using System;
using System.Security.Cryptography;


namespace liquicode.AppTools
{


	//=================================================================


	public static class Ciphers
	{


		//=================================================================


		//=================================================
		//
		//       CRYPTOGRAPHY DECLARATIONS
		//
		//=================================================


		//---------------------------------------------------------------------
		public enum HashAlgorithms
		{
			None = 0,
			MD5_128 = 1,
			// 16 bytes
			SHA1_160 = 2,
			// 20 bytes
			SHA_256 = 4,
			// 32 bytes
			SHA_384 = 8,
			// 48 bytes
			SHA_512 = 16
			// 64 bytes
		}


		//---------------------------------------------------------------------
		public enum KeyedHashAlgorithms
		{
			None = 0,
			HMACSHA1_160 = 1,
			// 20 bytes
			MACTripleDES_64 = 2
			// 8 bytes
		}


		//---------------------------------------------------------------------
		public enum SymmetricAlgorithms
		{
			// Name_BlockSize_KeySize
			None = 0,
			Unused_1 = 1,
			RC2_64_40 = 2,
			// 8/5 bytes
			RC2_64_48 = 4,
			// 8/6 bytes
			RC2_64_56 = 8,
			// 8/7 bytes
			RC2_64_64 = 16,
			// 8/8 bytes
			RC2_64_72 = 32,
			// 8/9 bytes
			RC2_64_80 = 64,
			// 8/10 bytes
			RC2_64_88 = 128,
			// 8/11 bytes
			RC2_64_96 = 256,
			// 8/12 bytes
			RC2_64_104 = 512,
			// 8/13 bytes
			RC2_64_112 = 1024,
			// 8/14 bytes
			RC2_64_120 = 2048,
			// 8/15 bytes
			RC2_64_128 = 4096,
			// 8/16 bytes
			DES_64_64 = 8192,
			// 8/8 bytes
			DES3_64_128 = 16384,
			// 8/16 bytes
			DES3_64_192 = 32768,
			// 8/24 bytes
			AES_128_128 = 65536,
			// 16/16 bytes
			AES_128_192 = 131072,
			// 16/24 bytes
			AES_128_256 = 262144,
			// 16/32 bytes
			AES_192_128 = 524288,
			// 24/16 bytes
			AES_192_192 = 1048576,
			// 24/24 bytes
			AES_192_256 = 2097152,
			// 24/32 bytes
			AES_256_128 = 4194304,
			// 32/16 bytes
			AES_256_192 = 8388608,
			// 32/24 bytes
			AES_256_256 = 16777216
			// 32/32 bytes
		}


		//---------------------------------------------------------------------
		public static SymmetricAlgorithms String2SymmetricAlgorithm( string s )
		{
			switch( s.ToUpper() )
			{
				case "RC2_64_40":
					return SymmetricAlgorithms.RC2_64_40;
				case "RC2_64_48":
					return SymmetricAlgorithms.RC2_64_48;
				case "RC2_64_56":
					return SymmetricAlgorithms.RC2_64_56;
				case "RC2_64_64":
					return SymmetricAlgorithms.RC2_64_64;
				case "RC2_64_72":
					return SymmetricAlgorithms.RC2_64_72;
				case "RC2_64_80":
					return SymmetricAlgorithms.RC2_64_80;
				case "RC2_64_88":
					return SymmetricAlgorithms.RC2_64_88;
				case "RC2_64_96":
					return SymmetricAlgorithms.RC2_64_96;
				case "RC2_64_104":
					return SymmetricAlgorithms.RC2_64_104;
				case "RC2_64_112":
					return SymmetricAlgorithms.RC2_64_112;
				case "RC2_64_120":
					return SymmetricAlgorithms.RC2_64_120;
				case "RC2_64_128":
					return SymmetricAlgorithms.RC2_64_128;
				case "DES_64_64":
					return SymmetricAlgorithms.DES_64_64;
				case "DES3_64_128":
					return SymmetricAlgorithms.DES3_64_128;
				case "DES3_64_192":
					return SymmetricAlgorithms.DES3_64_192;
				case "AES_128_128":
					return SymmetricAlgorithms.AES_128_128;
				case "AES_128_192":
					return SymmetricAlgorithms.AES_128_192;
				case "AES_128_256":
					return SymmetricAlgorithms.AES_128_256;
				case "AES_192_128":
					return SymmetricAlgorithms.AES_192_128;
				case "AES_192_192":
					return SymmetricAlgorithms.AES_192_192;
				case "AES_192_256":
					return SymmetricAlgorithms.AES_192_256;
				case "AES_256_128":
					return SymmetricAlgorithms.AES_256_128;
				case "AES_256_192":
					return SymmetricAlgorithms.AES_256_192;
				case "AES_256_256":
					return SymmetricAlgorithms.AES_256_256;
				default:
					return SymmetricAlgorithms.None;
			}
		}


		//=================================================================


		//=================================================
		//
		//       CRYPTOGRAPHY FACTORIES
		//
		//=================================================


		//---------------------------------------------------------------------
		public static HashAlgorithm Create_HashAlgorithm( HashAlgorithms HashAlgorithm_in )
		{
			switch( HashAlgorithm_in )
			{
				case HashAlgorithms.None:
					return null;
				case HashAlgorithms.MD5_128:
					return new MD5CryptoServiceProvider();
				case HashAlgorithms.SHA1_160:
					return new SHA1CryptoServiceProvider();
				case HashAlgorithms.SHA_256:
					return new SHA256Managed();
				case HashAlgorithms.SHA_384:
					return new SHA384Managed();
				case HashAlgorithms.SHA_512:
					return new SHA512Managed();
				default:
					return null;
			}
		}


		//---------------------------------------------------------------------
		public static byte[] Create_CryptographicKey( HashAlgorithms HashAlgorithm_in, int KeyBitSize_in, byte[] PasswordBytes_in )
		{
			HashAlgorithm HA = Create_HashAlgorithm( HashAlgorithm_in );
			if( (HA == null) )
				return null;
			if( (HA.HashSize < KeyBitSize_in) )
				throw new Exception( "Requested key size is too large." );
			byte[] KeyHash = null;
			KeyHash = HA.ComputeHash( PasswordBytes_in );
			int KeyByteSize = Convert.ToInt32( (KeyBitSize_in / 8) );
			if( (KeyHash.Length == KeyByteSize) )
				return KeyHash;
			byte[] Key = new byte[ KeyByteSize ];
			Array.Copy( KeyHash, Key, KeyByteSize );
			return Key;
		}


		//---------------------------------------------------------------------
		public static byte[] Create_CryptographicKey( HashAlgorithms HashAlgorithm_in, int KeyBitSize_in, string Password_in )
		{
			byte[] KeyText = new System.Text.ASCIIEncoding().GetBytes( Password_in );
			return Create_CryptographicKey( HashAlgorithm_in, KeyBitSize_in, KeyText );
		}


		//---------------------------------------------------------------------
		public static byte[] Create_CryptographicIV( HashAlgorithms HashAlgorithm_in, int IVBitSize_in, byte[] PasswordBytes_in )
		{
			// Derive an IV by XOR-ing the Key with a predefined mask
			byte[] KeyMask_256 =
			{
				30, 2, 141, 78, 82, 33, 76, 248, 56, 182, 
				254, 34, 59, 188, 225, 1, 180, 209, 15, 220, 
				52, 69, 2, 188, 235, 27, 175, 116, 234, 127, 
				28, 179
			};
			byte[] KeyBase = new byte[ PasswordBytes_in.Length ];
			int ndxPassword = 0;
			int ndxMask = 0;
			for( ndxPassword = PasswordBytes_in.GetLowerBound( 0 ); ndxPassword <= PasswordBytes_in.GetUpperBound( 0 ); ndxPassword++ )
			{
				KeyBase[ ndxPassword ] = (byte)(PasswordBytes_in[ ndxPassword ] ^ KeyMask_256[ ndxMask ]);
				ndxMask += 1;
				if( (ndxMask == KeyMask_256.Length) )
					ndxMask = 0;
			}
			return Create_CryptographicKey( HashAlgorithm_in, IVBitSize_in, KeyBase );
		}


		//---------------------------------------------------------------------
		public static byte[] Create_CryptographicIV( HashAlgorithms HashAlgorithm_in, int IVBitSize_in, string Password_in )
		{
			byte[] KeyText = new System.Text.ASCIIEncoding().GetBytes( Password_in );
			return Create_CryptographicIV( HashAlgorithm_in, IVBitSize_in, KeyText );
		}


		//---------------------------------------------------------------------
		public static SymmetricAlgorithm Create_SymmetricAlgorithm( SymmetricAlgorithms SymmetricAlgorithm_in, byte[] PasswordBytes_in )
		{
			HashAlgorithm HA = Create_HashAlgorithm( HashAlgorithms.SHA_512 );
			SymmetricAlgorithm SA = null;
			switch( SymmetricAlgorithm_in )
			{
				case SymmetricAlgorithms.None:
					return null;
				case SymmetricAlgorithms.Unused_1:
					return null;
				case SymmetricAlgorithms.RC2_64_40:
					SA = new RC2CryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 40;
					break;
				case SymmetricAlgorithms.RC2_64_48:
					SA = new RC2CryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 48;
					break;
				case SymmetricAlgorithms.RC2_64_56:
					SA = new RC2CryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 56;
					break;
				case SymmetricAlgorithms.RC2_64_64:
					SA = new RC2CryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 64;
					break;
				case SymmetricAlgorithms.RC2_64_72:
					SA = new RC2CryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 72;
					break;
				case SymmetricAlgorithms.RC2_64_80:
					SA = new RC2CryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 80;
					break;
				case SymmetricAlgorithms.RC2_64_88:
					SA = new RC2CryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 88;
					break;
				case SymmetricAlgorithms.RC2_64_96:
					SA = new RC2CryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 96;
					break;
				case SymmetricAlgorithms.RC2_64_104:
					SA = new RC2CryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 104;
					break;
				case SymmetricAlgorithms.RC2_64_112:
					SA = new RC2CryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 112;
					break;
				case SymmetricAlgorithms.RC2_64_120:
					SA = new RC2CryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 120;
					break;
				case SymmetricAlgorithms.RC2_64_128:
					SA = new RC2CryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 128;
					break;
				case SymmetricAlgorithms.DES_64_64:
					SA = new DESCryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 64;
					break;
				case SymmetricAlgorithms.DES3_64_128:
					SA = new TripleDESCryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 128;
					break;
				case SymmetricAlgorithms.DES3_64_192:
					SA = new TripleDESCryptoServiceProvider();
					SA.BlockSize = 64;
					SA.KeySize = 192;
					break;
				case SymmetricAlgorithms.AES_128_128:
					SA = new RijndaelManaged();
					SA.BlockSize = 128;
					SA.KeySize = 128;
					break;
				case SymmetricAlgorithms.AES_128_192:
					SA = new RijndaelManaged();
					SA.BlockSize = 128;
					SA.KeySize = 192;
					break;
				case SymmetricAlgorithms.AES_128_256:
					SA = new RijndaelManaged();
					SA.BlockSize = 128;
					SA.KeySize = 256;
					break;
				case SymmetricAlgorithms.AES_192_128:
					SA = new RijndaelManaged();
					SA.BlockSize = 192;
					SA.KeySize = 128;
					break;
				case SymmetricAlgorithms.AES_192_192:
					SA = new RijndaelManaged();
					SA.BlockSize = 192;
					SA.KeySize = 192;
					break;
				case SymmetricAlgorithms.AES_192_256:
					SA = new RijndaelManaged();
					SA.BlockSize = 192;
					SA.KeySize = 256;
					break;
				case SymmetricAlgorithms.AES_256_128:
					SA = new RijndaelManaged();
					SA.BlockSize = 256;
					SA.KeySize = 128;
					break;
				case SymmetricAlgorithms.AES_256_192:
					SA = new RijndaelManaged();
					SA.BlockSize = 256;
					SA.KeySize = 192;
					break;
				case SymmetricAlgorithms.AES_256_256:
					SA = new RijndaelManaged();
					SA.BlockSize = 256;
					SA.KeySize = 256;
					break;
				default:
					return null;
			}
			SA.Key = Create_CryptographicKey( HashAlgorithms.SHA_256, SA.KeySize, PasswordBytes_in );
			SA.IV = Create_CryptographicIV( HashAlgorithms.SHA_256, SA.BlockSize, PasswordBytes_in );
			return SA;
		}


		//---------------------------------------------------------------------
		public static SymmetricAlgorithm Create_SymmetricAlgorithm( SymmetricAlgorithms SymmetricAlgorithm_in, string Password_in )
		{
			byte[] KeyText = new System.Text.ASCIIEncoding().GetBytes( Password_in );
			return Create_SymmetricAlgorithm( SymmetricAlgorithm_in, KeyText );
		}


		//---------------------------------------------------------------------
		public static KeyedHashAlgorithm Create_KeyedHashAlgorithm( KeyedHashAlgorithms KeyedHashAlgorithm_in, string Password_in )
		{
			KeyedHashAlgorithm KHA = null;
			switch( KeyedHashAlgorithm_in )
			{
				case KeyedHashAlgorithms.None:
					return null;
				case KeyedHashAlgorithms.HMACSHA1_160:
					KHA = new HMACSHA1( System.Text.Encoding.ASCII.GetBytes( Password_in ) );
					break;
				case KeyedHashAlgorithms.MACTripleDES_64:
					if( (Password_in.Length <= 16) )
					{
						Password_in = Password_in.PadRight( 16, "~"[ 0 ] );
					}
					else if( (Password_in.Length <= 24) )
					{
						Password_in = Password_in.PadRight( 24, "~"[ 0 ] );
					}
					else
					{
						Password_in = Password_in.Substring( 0, 24 );
					}
					KHA = new MACTripleDES( System.Text.Encoding.ASCII.GetBytes( Password_in ) );
					break;
				default:
					return null;
			}
			return KHA;
		}


		//=================================================================


		//=================================================
		//
		//       ENCRYPTION
		//
		//=================================================


		//---------------------------------------------------------------------
		private static byte[] EncryptBytes_( SymmetricAlgorithm SymmetricAlgorithm_in, byte[] Bytes_in )
		{
			if( SymmetricAlgorithm_in == null ) { return null; }
			System.IO.MemoryStream msCipherText = new System.IO.MemoryStream();
			CryptoStream csCipherText = new CryptoStream( msCipherText, SymmetricAlgorithm_in.CreateEncryptor(), CryptoStreamMode.Write );
			csCipherText.Write( Bytes_in, 0, Bytes_in.Length );
			csCipherText.Close();
			byte[] Bytes_out = msCipherText.ToArray();
			msCipherText.Close();
			return Bytes_out;
		}


		//---------------------------------------------------------------------
		public static byte[] EncryptBytes( SymmetricAlgorithms SymmetricAlgorithm_in, byte[] Bytes_in, string Password_in )
		{
			return EncryptBytes_( Create_SymmetricAlgorithm( SymmetricAlgorithm_in, Password_in ), Bytes_in );
		}


		//---------------------------------------------------------------------
		public static byte[] EncryptBytes( SymmetricAlgorithms SymmetricAlgorithm_in, byte[] Bytes_in, byte[] Password_in )
		{
			return EncryptBytes_( Create_SymmetricAlgorithm( SymmetricAlgorithm_in, Password_in ), Bytes_in );
		}


		//'-------------------------------------------------
		//Public Function EncryptBytes( _
		//                                ByVal SymmetricAlgorithm_in As SymmetricAlgorithms _
		//                                , ByVal Text_in As String _
		//                                , ByVal Password_in As String _
		//                            ) As Byte()
		//    Dim bytes() As Byte = Text.ASCIIEncoding.ASCII.GetBytes(Text_in.ToCharArray)
		//    Return EncryptBytes_(Create_SymmetricAlgorithm(SymmetricAlgorithm_in, Password_in), bytes)
		//End Function


		//---------------------------------------------------------------------
		public static string EncryptString( SymmetricAlgorithms SymmetricAlgorithm_in, string String_in, byte[] Password_in )
		{
			return Convert.ToBase64String
			(
				EncryptBytes_
				(
					  Create_SymmetricAlgorithm( SymmetricAlgorithm_in, Password_in )
					, System.Text.UTF8Encoding.UTF8.GetBytes( String_in.ToCharArray() )
				)
			);
		}


		//---------------------------------------------------------------------
		public static string EncryptString( SymmetricAlgorithms SymmetricAlgorithm_in, string String_in, string Password_in )
		{
			return Convert.ToBase64String
			(
				EncryptBytes_
				(
					  Create_SymmetricAlgorithm( SymmetricAlgorithm_in, Password_in )
					, System.Text.UTF8Encoding.UTF8.GetBytes( String_in.ToCharArray() )
				)
			);
		}


		//=================================================================


		//=================================================
		//
		//       DECRYPTION
		//
		//=================================================


		//---------------------------------------------------------------------
		private static byte[] DecryptBytes_( SymmetricAlgorithm SymmetricAlgorithm_in, byte[] Bytes_in )
		{
			if( (SymmetricAlgorithm_in == null) )
				return null;
			System.IO.MemoryStream msCipherText = new System.IO.MemoryStream( Bytes_in );
			System.IO.MemoryStream msPlainText = new System.IO.MemoryStream();
			CryptoStream csCipher = new CryptoStream( msCipherText, SymmetricAlgorithm_in.CreateDecryptor(), CryptoStreamMode.Read );
			while( true )
			{
				byte[] Block = new byte[ SymmetricAlgorithm_in.BlockSize ];
				int nBytes = csCipher.Read( Block, 0, Block.Length );
				if( (nBytes == 0) )
				{
					break; // TODO: might not be correct. Was : Exit Do
				}
				msPlainText.Write( Block, 0, nBytes );
			}
			msCipherText.Close();
			csCipher.Close();
			byte[] Bytes_out = msPlainText.ToArray();
			msPlainText.Close();
			return Bytes_out;
		}


		//---------------------------------------------------------------------
		public static byte[] DecryptBytes( SymmetricAlgorithms SymmetricAlgorithm_in, byte[] Bytes_in, string Password_in )
		{
			return DecryptBytes_( Create_SymmetricAlgorithm( SymmetricAlgorithm_in, Password_in ), Bytes_in );
		}


		//---------------------------------------------------------------------
		public static byte[] DecryptBytes( SymmetricAlgorithms SymmetricAlgorithm_in, byte[] Bytes_in, byte[] Password_in )
		{
			return DecryptBytes_( Create_SymmetricAlgorithm( SymmetricAlgorithm_in, Password_in ), Bytes_in );
		}


		//---------------------------------------------------------------------
		public static string DecryptString( SymmetricAlgorithms SymmetricAlgorithm_in, string String_in, byte[] Password_in )
		{
			return System.Text.UTF8Encoding.UTF8.GetString
			(
				DecryptBytes_
				(
					  Create_SymmetricAlgorithm( SymmetricAlgorithm_in, Password_in )
					, Convert.FromBase64String( String_in )
				)
			);
		}


		//---------------------------------------------------------------------
		public static string DecryptString( SymmetricAlgorithms SymmetricAlgorithm_in, string String_in, string Password_in )
		{
			return System.Text.UTF8Encoding.UTF8.GetString
			(
				DecryptBytes_
				(
					  Create_SymmetricAlgorithm( SymmetricAlgorithm_in, Password_in )
					, Convert.FromBase64String( String_in )
				)
			);
		}


		//=================================================================


		//=================================================
		//
		//       RANDOM
		//
		//=================================================


		//---------------------------------------------------------------------
		private static Random Random_ = new Random( Convert.ToInt32( DateTime.Now.Ticks % Int32.MaxValue ) );


		//---------------------------------------------------------------------
		public static byte[] RandomBytes( int Length_in )
		{
			byte[] b = new byte[ Length_in ];
			Random_.NextBytes( b );
			return b;
		}


		//---------------------------------------------------------------------
		public static string RandomString( int Length_in )
		{
			string s = "";
			byte[] b = new byte[ 1 ];
			while( (s.Length < Length_in) )
			{
				Random_.NextBytes( b );
				if( (b[ 0 ] >= 48) & (b[ 0 ] <= 57) )
					s += b[ 0 ];
				// 0..9
				if( (b[ 0 ] >= 65) & (b[ 0 ] <= 90) )
					s += b[ 0 ];
				// A..Z
				if( (b[ 0 ] >= 97) & (b[ 0 ] <= 122) )
					s += b[ 0 ];
				// a..z
			}
			return s;
		}

		//---------------------------------------------------------------------
		public static int RandomInteger( int Min_in, int Max_in )
		{
			return Random_.Next( Min_in, Max_in + 1 );
		}


		//---------------------------------------------------------------------
		public static double RandomNumber()
		{
			return Random_.NextDouble();
		}


		//---------------------------------------------------------------------
		public static byte[] RandomBits( int BitCount )
		{
			int ByteCount = (BitCount / 8);
			if( ((ByteCount * 8) < BitCount) )
			{
				ByteCount += 1;
			}
			return RandomBytes( ByteCount );
		}


		//=================================================================


		//=================================================
		//
		//       HASH FUNCTIONS
		//
		//=================================================


		//---------------------------------------------------------------------
		public static long HashString64( string String_in, string Key_in )
		{
			KeyedHashAlgorithm hash = Create_KeyedHashAlgorithm( KeyedHashAlgorithms.MACTripleDES_64, Key_in );
			byte[] rgbData = System.Text.Encoding.ASCII.GetBytes( String_in );
			byte[] rgbHash = hash.ComputeHash( rgbData );
			long hash64 = BitConverter.ToInt64( rgbHash, 0 );
			//Dim sHash As String = Text.Encoding.ASCII.GetString(rgbHash)
			return hash64;
		}


		//---------------------------------------------------------------------
		public static int HashString32( string String_in, string Key_in )
		{
			KeyedHashAlgorithm hash = Create_KeyedHashAlgorithm( KeyedHashAlgorithms.MACTripleDES_64, Key_in );
			byte[] rgbData = System.Text.Encoding.ASCII.GetBytes( String_in );
			byte[] rgbHash = hash.ComputeHash( rgbData );
			long hash32a = BitConverter.ToInt32( rgbHash, 0 );
			long hash32b = BitConverter.ToInt32( rgbHash, 4 );
			long hash32c = Convert.ToInt32( ((hash32a + hash32b) / 2) );
			if( (hash32c < int.MinValue) )
				hash32c = int.MinValue;
			if( (hash32c > int.MaxValue) )
				hash32c = int.MaxValue;
			int hash32 = Convert.ToInt32( hash32c );
			return hash32;
		}


		//=================================================================


		//=================================================
		//
		//       CRC32 FUNCTIONS
		//
		//=================================================


		//---------------------------------------------------------------------
		private static int[] crc32Table;


		//---------------------------------------------------------------------
		private static void CRCInitialize()
		{
			//- - - - - - - - - - - - - - - - - - - - - - - - -
			// This is the official polynomial used by CRC32 in PKZip.
			// Often the polynomial is shown reversed (04C11DB7).
			int dwPolynomial = unchecked( (int)0xedb88320 );
			int i = 0;
			int j = 0;
			crc32Table = new int[ 257 ];
			long dwCrc = 0;
			for( i = 0; i <= 255; i++ )
			{
				dwCrc = i;
				for( j = 8; j >= 1; j += -1 )
				{
					if( Convert.ToBoolean( dwCrc & 1 ) )
					{
						dwCrc = ((dwCrc & 0xfffffffe) / 2L) & 0x7fffffff;
						dwCrc = dwCrc ^ dwPolynomial;
					}
					else
					{
						dwCrc = ((dwCrc & 0xfffffffe) / 2L) & 0x7fffffff;
					}
				}
				crc32Table[ i ] = Convert.ToInt32( dwCrc );
			}
		}


		//---------------------------------------------------------------------
		public static int CRCString32( string String_in )
		{
			//- - - - - - - - - - - - - - - - - - - - - - - - -
			int Hex_0xffffffff = unchecked( (int)0xffffffff );
			int Hex_0xffffff00 = unchecked( (int)0xffffff00 );
			if( (crc32Table == null) ) { CRCInitialize(); }
			int crc32Result = Hex_0xffffffff;
			int i = 0;
			int iLookup = 0;
			byte val = 0;
			for( i = 1; i <= String_in.Length; i++ )
			{
				val = Convert.ToByte( String_in[ i - 1 ] );
				iLookup = (crc32Result & 0xff) ^ val;
				crc32Result = ((crc32Result & Hex_0xffffff00) / 0x100) & 16777215;
				// nasty shr 8 with vb :/
				crc32Result = crc32Result ^ crc32Table[ iLookup ];
			}
			return ~(crc32Result);
		}


		//---------------------------------------------------------------------
		public static int CRCByteArray32( ref byte[] buffer )
		{
			//- - - - - - - - - - - - - - - - - - - - - - - - -
			int Hex_0xffffffff = unchecked( (int)0xffffffff );
			int Hex_0xffffff00 = unchecked( (int)0xffffff00 );
			if( (crc32Table == null) ) { CRCInitialize(); }
			int crc32Result = Hex_0xffffffff;
			int i = 0;
			int iLookup = 0;
			for( i = buffer.GetLowerBound( 0 ); i <= buffer.GetUpperBound( 0 ); i++ )
			{
				iLookup = (crc32Result & 0xff) ^ buffer[ i ];
				crc32Result = ((crc32Result & Hex_0xffffff00) / 0x100) & 16777215;
				// nasty shr 8 with vb :/
				crc32Result = crc32Result ^ crc32Table[ iLookup ];
			}
			return ~(crc32Result);
		}


	}

}
