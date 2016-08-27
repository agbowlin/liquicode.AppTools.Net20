

using System;
using System.Collections.Generic;
using System.Text;

using liquicode.AppTools;
using NUnit.Framework;


public class TestMatrixDimension
{


	//--------------------------------------------------------------------
	protected DataStructures.stringMatrix NewEvenMatrix( int XMin_in, int XCount_in, int YMin_in, int YCount_in )
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
	protected void AssertEvenMatrix( DataStructures.GenericMatrix<string> Matrix_in )
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
	protected void Test_Swap_Forward( DataStructures.GenericMatrixDimension<string> MD )
	{

		string[] vector = { };

		for( int ndxRound = MD.Min; ndxRound <= MD.Max; ndxRound++ )
		{
			vector = MD.GetVector( MD.Min );
			for( int ndx = MD.Min; ndx <= (MD.Max - 1); ndx++ )
			{
				MD.Swap( ndx + 1, ndx );
			}
			MD.SetVector( MD.Max, vector );
		}

		AssertEvenMatrix( MD.Matrix );
		return;

	}


	//--------------------------------------------------------------------
	protected void Test_Swap_Backward( DataStructures.GenericMatrixDimension<string> MD )
	{

		string[] vector = { };

		for( int ndxRound = MD.Min; ndxRound <= MD.Max; ndxRound++ )
		{
			vector = MD.GetVector( MD.Max );
			for( int ndx = MD.Max; ndx >= (MD.Min + 1); ndx += -1 )
			{
				MD.Swap( ndx, ndx - 1 );
			}
			MD.SetVector( MD.Min, vector );
		}

		AssertEvenMatrix( MD.Matrix );
		return;

	}


	//--------------------------------------------------------------------
	protected void Test_Copy_Forward( DataStructures.GenericMatrixDimension<string> MD )
	{

		string[] vector = { };

		for( int ndxRound = MD.Min; ndxRound <= MD.Max; ndxRound++ )
		{
			vector = MD.GetVector( MD.Min );
			for( int ndx = MD.Min; ndx <= (MD.Max - 1); ndx++ )
			{
				MD.Copy( ndx + 1, ndx );
			}
			MD.SetVector( MD.Max, vector );
		}

		AssertEvenMatrix( MD.Matrix );
		return;

	}


	//--------------------------------------------------------------------
	protected void Test_Copy_Backward( DataStructures.GenericMatrixDimension<string> MD )
	{

		string[] vector = { };

		for( int ndxRound = MD.Min; ndxRound <= MD.Max; ndxRound++ )
		{
			vector = MD.GetVector( MD.Max );
			for( int ndx = MD.Max; ndx >= (MD.Min + 1); ndx += -1 )
			{
				MD.Copy( ndx - 1, ndx );
			}
			MD.SetVector( MD.Min, vector );
		}

		AssertEvenMatrix( MD.Matrix );
		return;

	}


	//--------------------------------------------------------------------
	protected void Test_Move_Forward( DataStructures.GenericMatrixDimension<string> MD )
	{

		string[] vector = { };

		for( int ndxRound = MD.Min; ndxRound <= MD.Max; ndxRound++ )
		{
			vector = MD.GetVector( MD.Min );
			for( int ndx = MD.Min; ndx <= (MD.Max - 1); ndx++ )
			{
				MD.Move( ndx + 1, ndx );
			}
			MD.SetVector( MD.Max , vector);
		}

		AssertEvenMatrix( MD.Matrix );
		return;

	}


	//--------------------------------------------------------------------
	protected void Test_Move_Backward( DataStructures.GenericMatrixDimension<string> MD )
	{

		string[] vector = { };

		for( int ndxRound = MD.Min; ndxRound <= MD.Max; ndxRound++ )
		{
			vector = MD.GetVector( MD.Max );
			for( int ndx = MD.Max; ndx >= (MD.Min + 1); ndx += -1 )
			{
				MD.Move( ndx - 1, ndx );
			}
			MD.SetVector( MD.Min , vector);
		}

		AssertEvenMatrix( MD.Matrix );
		return;

	}


	//--------------------------------------------------------------------
	protected void Test_AppendDelete( DataStructures.GenericMatrixDimension<string> MD )
	{
		int ndx = MD.Max;
		MD.Append( 10 );
		MD.Delete( ndx + 1, 10 );
		AssertEvenMatrix( MD.Matrix );
		return;
	}


	//--------------------------------------------------------------------
	protected void Test_InsertDelete( DataStructures.GenericMatrixDimension<string> MD )
	{
		int ndx = 0;
		int n = 0;

		// Insert/Delete small in middle
		ndx = (int)(MD.Max - MD.Min) / 2;
		n = (int)MD.Count / 2;
		MD.Insert( ndx, n );
		MD.Delete( ndx, n );
		AssertEvenMatrix( MD.Matrix );

		// Insert/Delete small in front
		ndx = MD.Min;
		n = (int)MD.Count / 2;
		MD.Insert( ndx, n );
		MD.Delete( ndx, n );
		AssertEvenMatrix( MD.Matrix );

		// Insert/Delete large in middle
		ndx = (int)(MD.Max - MD.Min) / 2;
		n = (MD.Count * 2);
		MD.Insert( ndx, n );
		MD.Delete( ndx, n );
		AssertEvenMatrix( MD.Matrix );

		// Insert/Delete large in front
		ndx = MD.Min;
		n = (MD.Count * 2);
		MD.Insert( ndx, n );
		MD.Delete( ndx, n );
		AssertEvenMatrix( MD.Matrix );

		return;
	}


	//--------------------------------------------------------------------
	protected void Test_InsertMoveDelete( DataStructures.GenericMatrixDimension<string> MD )
	{
		int n = 0;
		n = (int)MD.Count / 3;
		MD.Insert( n + 1, n );
		MD.Move( MD.Min, n + 1, n );
		MD.Move( n + 1, MD.Min, n );
		MD.Delete( n + 1, n );
		AssertEvenMatrix( MD.Matrix );
		return;
	}


}

