

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture]
public class TestMatrixDimensionX : TestMatrixDimension
{


	//--------------------------------------------------------------------
	private DataStructures.stringMatrix _matrix = null;


	//--------------------------------------------------------------------
	[SetUp]
	public void Setup()
	{
		this._matrix = this.NewEvenMatrix( 1, 10, 1, 10 );
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_11_Swap_Forward()
	{
		Test_Swap_Forward( this._matrix.X );
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_12_Swap_Backward()
	{
		Test_Swap_Backward( this._matrix.X );
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_21_Copy_Forward()
	{
		Test_Copy_Forward( this._matrix.X );
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_22_Copy_Backward()
	{
		Test_Copy_Backward( this._matrix.X );
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_31_Move_Forward()
	{
		Test_Move_Forward( this._matrix.X );
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_32_Move_Backward()
	{
		Test_Move_Backward( this._matrix.X );
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_41_AppendDelete()
	{
		Test_AppendDelete( this._matrix.X );
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_51_InsertDelete()
	{
		Test_InsertDelete( this._matrix.X );
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_52_InsertMoveDelete()
	{
		Test_InsertMoveDelete( this._matrix.X );
	}


}
