
using System;
using System.Collections.Generic;
using System.Text;


public class SimpleRandom
{

	//-------------------------------------------------
	private Random _Random = new Random( (int)DateTime.Now.Ticks % System.Int32.MaxValue );


	//-------------------------------------------------
	public SimpleRandom()
	{
		this._Random = new Random( (int)DateTime.Now.Ticks % System.Int32.MaxValue );
		return;
	}


	//-------------------------------------------------
	public SimpleRandom( int Seed )
	{
		this._Random = new Random( Seed );
		return;
	}


	//-------------------------------------------------
	public byte RandomByte()
	{
		byte[] b = new byte[ 1 ];
		this._Random.NextBytes( b );
		return b[ 0 ];
	}


	//-------------------------------------------------
	public byte[] RandomBytes( int Length_in )
	{
		byte[] b = new byte[ Length_in ];
		this._Random.NextBytes( b );
		return b;
	}


	//-------------------------------------------------
	public string RandomString( int Length_in )
	{
		string s = "";
		byte[] bs = new byte[ 1 ];
		while( (s.Length < Length_in) )
		{
			this._Random.NextBytes( bs );
			byte b = bs[ 0 ];
			if( (b >= 48) & (b <= 57) ) { s += b; }		// 0..9
			if( (b >= 65) & (b <= 90) ) { s += b; } 	// A..Z
			if( (b >= 97) & (b <= 122) ) { s += b; }	// a..z
		}
		return s;
	}

	//-------------------------------------------------
	public int RandomInteger( int Min_in, int Max_in )
	{
		return this._Random.Next( Min_in, Max_in + 1 );
	}
	public int RandomInteger()
	{
		return this._Random.Next( Int32.MinValue, Int32.MaxValue );
	}


	//-------------------------------------------------
	public double RandomNumber()
	{
		return this._Random.NextDouble();
	}

}
