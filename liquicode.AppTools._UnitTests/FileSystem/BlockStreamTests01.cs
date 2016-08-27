

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;

using NUnit.Framework;


[TestFixture]
public class BlockStreamTests01
{


	//---------------------------------------------------------------------
	private Hashtable NewTestTags( int TestNumber )
	{
		Hashtable hash = new Hashtable();
		hash[ "TestNumber" ] = TestNumber;

		int TestStringLength = (TestNumber % 1000);
		string TestString = "";
		for( int ndx = 1; ndx <= TestStringLength; ndx++ )
		{
			TestString += (char)((ndx % 94) + 32); // Select a printable character.
		}
		hash[ "TestStringLength" ] = TestStringLength;
		hash[ "TestString" ] = TestString;
		return hash;
	}


	//---------------------------------------------------------------------
	private byte[] NewTestBytes( int TestNumber )
	{
		byte[] bytes = new byte[ TestNumber ];
		for( int ndx = 0; ndx < TestNumber; ndx++ )
		{ bytes[ ndx ] = (byte)(ndx % 256); }
		return bytes;
	}


	//---------------------------------------------------------------------
	private void AssertIsValid( Hashtable Tags, byte[] Bytes )
	{
		Assert.IsTrue( Tags.ContainsKey( "TestNumber" ) );
		int TestNumber = (int)Tags[ "TestNumber" ];
		Hashtable tags = this.NewTestTags( TestNumber );
		byte[] bytes = this.NewTestBytes( TestNumber );
		//Assert.IsTrue( DataManagement.AreEqual( Tags, tags ) );
		Assert.IsTrue( DataManagement.AreEqual( Bytes, bytes ) );
		return;
	}


	//---------------------------------------------------------------------
	private void AssertIsValid( BlockStream Blocks )
	{
		foreach( Guid id in Blocks.BlockIDs )
		{
			if( !(id.Equals( Guid.Empty )) )
			{
				Hashtable tags = null;
				byte[] bytes = null;
				Blocks.ReadBlock( id, ref tags, ref bytes );
				this.AssertIsValid( tags, bytes );
			}
		}
		return;
	}


	//---------------------------------------------------------------------
	[Test]
	public void T01_SimpleWriteRead()
	{
		BlockStream blocks = new BlockStream();
		blocks.CreateMemoryStream();

		Guid id = Guid.NewGuid();

		Hashtable tags0 = new Hashtable();
		tags0[ "Name" ] = "Test Write";
		tags0[ "Description" ] = "Simple Write/Read Test.";

		byte[] bytes0 = new byte[ 256 ];
		for( int ndx = 0; ndx < 256; ndx++ )
		{ bytes0[ ndx ] = (byte)ndx; }

		blocks.WriteBlock( id, tags0, bytes0 );

		Hashtable tags1 = null;
		byte[] bytes1 = null;
		blocks.ReadBlock( id, ref tags1, ref bytes1 );

		//Assert.IsTrue( DataManagement.AreEqual( tags0, tags1 ) );
		Assert.IsTrue( DataManagement.AreEqual( bytes0, bytes1 ) );

		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T11_SequentialWriteRead()
	{
		BlockStream blocks = new BlockStream();
		blocks.CreateMemoryStream();
		int TestIterations = 300;
		for( int TestNumber = 1; TestNumber <= TestIterations; TestNumber++ )
		{
			Guid id = Guid.NewGuid();

			Hashtable tags0 = this.NewTestTags( TestNumber );
			byte[] bytes0 = this.NewTestBytes( TestNumber );
			blocks.WriteBlock( id, tags0, bytes0 );

			Hashtable tags1 = null;
			byte[] bytes1 = null;
			blocks.ReadBlock( id, ref tags1, ref bytes1 );

			//Assert.IsTrue( DataManagement.AreEqual( tags0, tags1 ) );
			Assert.IsTrue( DataManagement.AreEqual( bytes0, bytes1 ) );
		}
		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T12_SequentialWriteRead()
	{
		BlockStream blocks = new BlockStream();
		blocks.CreateMemoryStream();
		int TestIterations = 300;
		for( int TestNumber = 1; TestNumber <= TestIterations; TestNumber++ )
		{
			Hashtable tags = this.NewTestTags( TestNumber );
			byte[] bytes = this.NewTestBytes( TestNumber );
			blocks.WriteBlock( Guid.NewGuid(), tags, bytes );
		}
		this.AssertIsValid( blocks );
		return;
	}

	//---------------------------------------------------------------------
	[Test]
	public void T21_RandomWriteRead()
	{
		BlockStream blocks = new BlockStream();
		blocks.CreateMemoryStream();
		SimpleRandom random = new SimpleRandom( 102854 );
		int TestIterations = random.RandomInteger( 100, 10000 );
		Console.Out.WriteLine( "Performing " + TestIterations.ToString() + " test iterations." );
		for( int TestNumber = 1; TestNumber <= TestIterations; TestNumber++ )
		{
			int action = random.RandomInteger( 0, 100 );
			if( action < 50 ) // Insert
			{
				Hashtable tags = this.NewTestTags( TestNumber );
				byte[] bytes = this.NewTestBytes( TestNumber );
				blocks.WriteBlock( Guid.NewGuid(), tags, bytes );
			}
			else
			{
				List<Guid> ids = blocks.BlockIDs;
				int ndx = random.RandomInteger( 1, ids.Count );
				Guid id = ids[ ndx - 1 ];
				if( action < 75 ) // Update
				{
					Hashtable tags = this.NewTestTags( TestNumber );
					byte[] bytes = this.NewTestBytes( TestNumber );
					blocks.WriteBlock( id, tags, bytes );
				}
				else if( action <= 100 ) // Delete
				{
					blocks.DestroyBlock( id );
				}
			}
		}
		this.AssertIsValid( blocks );
		blocks.Compact();
		this.AssertIsValid( blocks );
		return;
	}

}
