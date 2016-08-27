

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

public abstract class Test_Vector<TVector, TValue> where TVector : DataStructures.GenericVector<TValue>, new()
{

	//--------------------------------------------------------------------
	protected abstract TValue TestValue( int Index_in );


	//--------------------------------------------------------------------
	private TVector NewTestVector( int Length_in )
	{
		TVector vector = new TVector();
		for( int ndx = 0; ndx < Length_in; ndx++ )
		{
			vector.Add( this.TestValue( ndx ) );
		}
		return vector;
	}


	//--------------------------------------------------------------------
	private void AssertTestVector( TVector Vector_in, int Length_in )
	{
		Assert.AreEqual( Length_in, Vector_in.Count, "Invalid Vector.Count." );
		for( int ndx = 0; ndx < Length_in; ndx++ )
		{
			Assert.AreEqual( this.TestValue( ndx ), Vector_in[ ndx ], "Assert.AreEqual failed at element " + ndx );
		}
		return;
	}



	//--------------------------------------------------------------------
	[Test]
	public void Test_00_DefaultConstructor()
	{
		TVector vector = new TVector();
		Assert.IsNotNull( vector );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_01_DisallowUnintentionalDeepCopyConstructor()
	{
		// Make sure we don't define a copy constructor which performs an unintentional deep copy.
		TVector vector = this.NewTestVector( 1000 );
		TVector vector2 = vector;
		vector.Clear();
		Assert.AreEqual( vector.Count, vector2.Count );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_02_TestVector()
	{
		TVector vector = this.NewTestVector( 1000 );
		this.AssertTestVector( vector, 1000 );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_10_FillLength()
	{
		TVector vector = new TVector();
		TValue value42 = this.TestValue( 42 );
		vector.Fill( value42, 1000 );
		for( int ndx = 0; ndx < 1000; ndx++ )
		{ Assert.AreEqual( value42, vector[ ndx ] ); }
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_11_FillAll()
	{
		TVector vector = new TVector();
		TValue value8 = this.TestValue( 8 );
		TValue value42 = this.TestValue( 42 );
		vector.Fill( value8, 1000 );
		vector.Fill( value42 );
		for( int ndx = 0; ndx < 1000; ndx++ )
		{ Assert.AreEqual( value42, vector[ ndx ] ); }
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_12_Substitute()
	{
		TVector vector = new TVector();
		TValue value8 = this.TestValue( 8 );
		TValue value42 = this.TestValue( 42 );
		vector.Fill( value8, 1000 );
		vector.Substitute( value8, value42 );
		for( int ndx = 0; ndx < 1000; ndx++ )
		{ Assert.AreEqual( value42, vector[ ndx ] ); }
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_20_ExplicitArrayCast()
	{
		TVector vector = this.NewTestVector( 1000 );
		TValue[] array = (TValue[])vector;
		Assert.AreEqual( array.Length, vector.Count, "Array/Vector lentgh mismatch." );
		TVector vector2 = new TVector();
		vector2.CopyFromArray( array );
		this.AssertTestVector( vector2, 1000 );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_21_ImplicitArrayCast()
	{
		TVector vector = this.NewTestVector( 1000 );
		TValue[] array = vector;
		Assert.AreEqual( array.Length, vector.Count, "Array/Vector lentgh mismatch." );
		TVector vector2 = new TVector();
		vector2.CopyFromArray( array );
		this.AssertTestVector( vector2, 1000 );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_30_Equality()
	{
		TVector vector = this.NewTestVector( 1000 );
		TVector vector2 = new TVector();
		vector2.CopyFromArray( vector.ToArray() );
		Assert.IsTrue( vector.Equals( vector2 ) );
		Assert.IsTrue( (vector == vector2) );
		Assert.IsFalse( (vector != vector2) );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_31_Inequality()
	{
		TVector vector = this.NewTestVector( 1000 );
		TVector vector2 = new TVector();
		vector2.CopyFromArray( vector.ToArray() );
		vector2[ vector2.Count - 1 ] = default( TValue );
		Assert.IsFalse( vector.Equals( vector2 ) );
		Assert.IsFalse( (vector == vector2) );
		Assert.IsTrue( (vector != vector2) );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_40_SerializeByteArray()
	{
		TVector vector = this.NewTestVector( 1000 );
		TVector vector2 = new TVector();
		byte[] bytes = vector.ToByteArray();
		vector2.CopyFromByteArray( bytes );
		this.AssertTestVector( vector2, 1000 );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_41_SerializeStream()
	{
		TVector vector = this.NewTestVector( 1000 );
		TVector vector2 = new TVector();
		using( System.IO.MemoryStream buffer = new System.IO.MemoryStream() )
		{
			vector.Serialize( buffer );
			buffer.Position = 0;
			vector2.Deserialize( buffer );
		}
		this.AssertTestVector( vector2, 1000 );
		return;
	}


}

//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

[TestFixture]
public class Test_floatVector : Test_Vector<DataStructures.FloatVector, float>
{
	protected override float TestValue( int Index_in )
	{ return Index_in; }
}

//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

[TestFixture]
public class Test_doubleVector : Test_Vector<DataStructures.DoubleVector, double>
{
	protected override double TestValue( int Index_in )
	{ return Index_in; }
}

//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

[TestFixture]
public class Test_byteVector : Test_Vector<DataStructures.ByteVector, byte>
{
	protected override byte TestValue( int Index_in )
	{ return (byte)(Index_in % (byte.MaxValue + 1)); }
}

//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

[TestFixture]
public class Test_intVector : Test_Vector<DataStructures.IntVector, int>
{
	protected override int TestValue( int Index_in )
	{ return Index_in; }
}

//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

[TestFixture]
public class Test_longVector : Test_Vector<DataStructures.LongVector, long>
{
	protected override long TestValue( int Index_in )
	{ return Index_in; }
}

//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

[TestFixture]
public class Test_stringVector : Test_Vector<DataStructures.StringVector, string>
{
	protected override string TestValue( int Index_in )
	{ return Index_in.ToString(); }
}

//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

[TestFixture]
public class Test_DateTimeVector : Test_Vector<DataStructures.DateTimeVector, DateTime>
{
	protected override DateTime TestValue( int Index_in )
	{ return (new DateTime( 2000, 1, 1 )).AddDays( Index_in ); }
}


