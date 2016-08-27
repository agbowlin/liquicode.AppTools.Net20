

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


[TestFixture]
public class TestMatrix
{


	//--------------------------------------------------------------------
	[SetUp]
	protected void SetUp()
	{
	}


	//--------------------------------------------------------------------
	private DataStructures.stringMatrix NewEvenMatrix( int XMin_in, int XCount_in, int YMin_in, int YCount_in )
	{
		DataStructures.stringMatrix matrix = new DataStructures.stringMatrix();
		matrix.X.Min = XMin_in;
		matrix.Y.Min = YMin_in;
		matrix.X.Append( XCount_in );
		matrix.Y.Append( YCount_in );
		string s = "";
		for( int x = matrix.X.Min; x <= matrix.X.Max; x++ )
		{
			for( int y = matrix.Y.Min; y <= matrix.Y.Max; y++ )
			{
				s = (x - matrix.X.Min).ToString() + "," + (y - matrix.Y.Min).ToString();
				matrix.SetCell( x, y, s );
			}
		}
		return matrix;
	}


	//--------------------------------------------------------------------
	private void AssertEvenMatrix( DataStructures.stringMatrix Matrix_in )
	{
		string s = "";
		for( int x = Matrix_in.X.Min; x <= Matrix_in.X.Max; x++ )
		{
			for( int y = Matrix_in.Y.Min; y <= Matrix_in.Y.Max; y++ )
			{
				s = (x - Matrix_in.X.Min).ToString() + "," + (y - Matrix_in.Y.Min).ToString();
				Assert.AreEqual( s, (string)Matrix_in.GetCell( x, y ), "Value mismatch at (" + x.ToString() + ", " + y.ToString() + ")." );
			}
		}
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_10_Values()
	{
		int XCount = 4;
		int YCount = 3;
		DataStructures.stringMatrix matrix = this.NewEvenMatrix( 1, XCount, 1, YCount );
		AssertEvenMatrix( matrix );
	}


	//--------------------------------------------------------------------
	[Test]
	public void Test_20_Bounds()
	{
		int XCount = 4;
		int YCount = 3;
		DataStructures.stringMatrix matrix = this.NewEvenMatrix( 1, XCount, 1, YCount );
		Assert.AreEqual( 1, matrix.X.Min, "Wrong XMin." );
		Assert.AreEqual( XCount, matrix.X.Max, "Wrong XMax." );
		Assert.AreEqual( XCount, matrix.X.Count, "Wrong XCount." );
		Assert.AreEqual( 1, matrix.Y.Min, "Wrong YMin." );
		Assert.AreEqual( YCount, matrix.Y.Max, "Wrong YMax." );
		Assert.AreEqual( YCount, matrix.Y.Count, "wrong YCount." );
		AssertEvenMatrix( matrix );
		matrix.X.Min = 0;
		matrix.Y.Min = 0;
		AssertEvenMatrix( matrix );
		matrix.X.Min = -1;
		matrix.Y.Min = -1;
		AssertEvenMatrix( matrix );
	}


}
