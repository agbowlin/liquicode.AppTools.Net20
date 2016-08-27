

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture]
public class TestVectorConversions
{

	//--------------------------------------------------------------------
	private DataStructures.IntVector NewTestVector( int Length_in )
	{
		DataStructures.IntVector vector = new DataStructures.IntVector();
		for( int ndx = 0; ndx < Length_in; ndx++ )
		{
			vector.Add( ndx );
		}
		return vector;
	}


	//--------------------------------------------------------------------
	private void AssertTestVector( DataStructures.IntVector Vector_in, int Length_in )
	{
		Assert.AreEqual( Length_in, Vector_in.Count, "Invalid Vector.Count." );
		for( int ndx = 0; ndx < Length_in; ndx++ )
		{
			Assert.AreEqual( ndx, Vector_in[ ndx ], "Assert.AreEqual failed at element " + ndx );
		}
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_00_DefaultConstructor()
	{
		DataStructures.IntVector vector = new DataStructures.IntVector();
		Assert.IsNotNull( vector );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_01_ConstructorFromObject()
	{
		DataStructures.IntVector vector = this.NewTestVector( 1000 );
		DataStructures.IntVector vector2 = new DataStructures.IntVector( vector );
		this.AssertTestVector( vector2, 1000 );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_02_ConstructorFromArray()
	{
		DataStructures.IntVector vector = this.NewTestVector( 1000 );
		DataStructures.IntVector vector2 = new DataStructures.IntVector( vector.ToArray() );
		this.AssertTestVector( vector2, 1000 );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_03_ConstructorFromByteArray()
	{
		DataStructures.IntVector vector = this.NewTestVector( 1000 );
		DataStructures.IntVector vector2 = new DataStructures.IntVector( vector.ToByteArray() );
		this.AssertTestVector( vector2, 1000 );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_04_ConstructorFromStream()
	{
		DataStructures.IntVector vector = this.NewTestVector( 1000 );
		using( System.IO.Stream stream = vector.ToStream() )
		{
			DataStructures.IntVector vector2 = new DataStructures.IntVector( stream );
			this.AssertTestVector( vector2, 1000 );
		}
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_11_FromObject()
	{
		DataStructures.IntVector vector = this.NewTestVector( 1000 );
		DataStructures.IntVector vector2 = DataStructures.IntVector.FromVector( vector );
		this.AssertTestVector( vector2, 1000 );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_12_FromArray()
	{
		DataStructures.IntVector vector = this.NewTestVector( 1000 );
		DataStructures.IntVector vector2 = DataStructures.IntVector.FromArray( vector.ToArray() );
		this.AssertTestVector( vector2, 1000 );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_13_FromByteArray()
	{
		DataStructures.IntVector vector = this.NewTestVector( 1000 );
		DataStructures.IntVector vector2 = DataStructures.IntVector.FromByteArray( vector.ToByteArray() );
		this.AssertTestVector( vector2, 1000 );
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_14_FromStream()
	{
		DataStructures.IntVector vector = this.NewTestVector( 1000 );
		using( System.IO.Stream stream = vector.ToStream() )
		{
			DataStructures.IntVector vector2 = DataStructures.IntVector.FromStream( stream );
			this.AssertTestVector( vector2, 1000 );
		}
		return;
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_20_Clone()
	{
		DataStructures.IntVector vector = this.NewTestVector( 1000 );
		DataStructures.IntVector vector2 = vector.Clone();
		return;
	}


}
