

using System;
using System.Collections.Generic;
using System.Text;


namespace liquicode.AppTools
{
	public static partial class DataStructures
	{

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class FloatVector : GenericVector<float>
		{
			// Default Constructor
			public FloatVector() : base() { }

			// Constructor: floatVector v2 = new floatVector( v1 );
			public FloatVector( FloatVector that ) : base() { this.CopyFromArray( that.ToArray() ); return; }
			// Converter: floatVector v2 = floatVector.FromVector( v1 );
			public static FloatVector FromVector( FloatVector that ) { return new FloatVector( that ); }

			// Constructor: floatVector v2 = new floatVector( v1.ToArray() );
			public FloatVector( float[] that ) : base() { this.CopyFromArray( that ); return; }
			// Converter: floatVector v2 = floatVector.FromArray( v1.ToArray() );
			public static FloatVector FromArray( float[] that ) { return new FloatVector( that ); }

			// Constructor: floatVector v2 = new floatVector( v1.ToByteArray() );
			public FloatVector( byte[] that ) : base() { this.CopyFromByteArray( that ); return; }
			// Converter: floatVector v2 = floatVector.FromByteArray( v1.ToByteArray() );
			public static FloatVector FromByteArray( byte[] that ) { return new FloatVector( that ); }

			// Constructor: floatVector v2 = new floatVector( v1.ToStream() );
			public FloatVector( System.IO.Stream that ) : base() { this.CopyFromStream( that ); return; }
			// Converter: floatVector v2 = floatVector.FromStream( v1.ToStream() );
			public static FloatVector FromStream( System.IO.Stream that ) { return new FloatVector( that ); }

			// Clone: floatVector v2 = v1.Clone();
			public FloatVector Clone() { return new FloatVector( this ); }
		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class DoubleVector : GenericVector<double>
		{
			// Default Constructor
			public DoubleVector() : base() { }

			// Constructor: doubleVector v2 = new doubleVector( v1 );
			public DoubleVector( DoubleVector that ) : base() { this.CopyFromArray( that.ToArray() ); return; }
			// Converter: doubleVector v2 = doubleVector.FromVector( v1 );
			public static DoubleVector FromVector( DoubleVector that ) { return new DoubleVector( that ); }

			// Constructor: doubleVector v2 = new doubleVector( v1.ToArray() );
			public DoubleVector( double[] that ) : base() { this.CopyFromArray( that ); return; }
			// Converter: doubleVector v2 = doubleVector.FromArray( v1.ToArray() );
			public static DoubleVector FromArray( double[] that ) { return new DoubleVector( that ); }

			// Constructor: doubleVector v2 = new doubleVector( v1.ToByteArray() );
			public DoubleVector( byte[] that ) : base() { this.CopyFromByteArray( that ); return; }
			// Converter: doubleVector v2 = doubleVector.FromByteArray( v1.ToByteArray() );
			public static DoubleVector FromByteArray( byte[] that ) { return new DoubleVector( that ); }

			// Constructor: doubleVector v2 = new doubleVector( v1.ToStream() );
			public DoubleVector( System.IO.Stream that ) : base() { this.CopyFromStream( that ); return; }
			// Converter: doubleVector v2 = doubleVector.FromStream( v1.ToStream() );
			public static DoubleVector FromStream( System.IO.Stream that ) { return new DoubleVector( that ); }

			// Clone: doubleVector v2 = v1.Clone();
			public DoubleVector Clone() { return new DoubleVector( this ); }
		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class DecimalVector : GenericVector<decimal>
		{
			// Default Constructor
			public DecimalVector() : base() { }

			// Constructor: decimalVector v2 = new decimalVector( v1 );
			public DecimalVector( DecimalVector that ) : base() { this.CopyFromArray( that.ToArray() ); return; }
			// Converter: decimalVector v2 = decimalVector.FromVector( v1 );
			public static DecimalVector FromVector( DecimalVector that ) { return new DecimalVector( that ); }

			// Constructor: decimalVector v2 = new decimalVector( v1.ToArray() );
			public DecimalVector( decimal[] that ) : base() { this.CopyFromArray( that ); return; }
			// Converter: decimalVector v2 = decimalVector.FromArray( v1.ToArray() );
			public static DecimalVector FromArray( decimal[] that ) { return new DecimalVector( that ); }

			// Constructor: decimalVector v2 = new decimalVector( v1.ToByteArray() );
			public DecimalVector( byte[] that ) : base() { this.CopyFromByteArray( that ); return; }
			// Converter: decimalVector v2 = decimalVector.FromByteArray( v1.ToByteArray() );
			public static DecimalVector FromByteArray( byte[] that ) { return new DecimalVector( that ); }

			// Constructor: decimalVector v2 = new decimalVector( v1.ToStream() );
			public DecimalVector( System.IO.Stream that ) : base() { this.CopyFromStream( that ); return; }
			// Converter: decimalVector v2 = decimalVector.FromStream( v1.ToStream() );
			public static DecimalVector FromStream( System.IO.Stream that ) { return new DecimalVector( that ); }

			// Clone: decimalVector v2 = v1.Clone();
			public DecimalVector Clone() { return new DecimalVector( this ); }
		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class BoolVector : GenericVector<bool>
		{
			// Default Constructor
			public BoolVector() : base() { }

			// Constructor: boolVector v2 = new boolVector( v1 );
			public BoolVector( BoolVector that ) : base() { this.CopyFromArray( that.ToArray() ); return; }
			// Converter: boolVector v2 = boolVector.FromVector( v1 );
			public static BoolVector FromVector( BoolVector that ) { return new BoolVector( that ); }

			// Constructor: boolVector v2 = new boolVector( v1.ToArray() );
			public BoolVector( bool[] that ) : base() { this.CopyFromArray( that ); return; }
			// Converter: boolVector v2 = boolVector.FromArray( v1.ToArray() );
			public static BoolVector FromArray( bool[] that ) { return new BoolVector( that ); }

			// Constructor: boolVector v2 = new boolVector( v1.ToByteArray() );
			public BoolVector( byte[] that ) : base() { this.CopyFromByteArray( that ); return; }
			// Converter: boolVector v2 = boolVector.FromByteArray( v1.ToByteArray() );
			public static BoolVector FromByteArray( byte[] that ) { return new BoolVector( that ); }

			// Constructor: boolVector v2 = new boolVector( v1.ToStream() );
			public BoolVector( System.IO.Stream that ) : base() { this.CopyFromStream( that ); return; }
			// Converter: boolVector v2 = boolVector.FromStream( v1.ToStream() );
			public static BoolVector FromStream( System.IO.Stream that ) { return new BoolVector( that ); }

			// Clone: boolVector v2 = v1.Clone();
			public BoolVector Clone() { return new BoolVector( this ); }
		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class ByteVector : GenericVector<byte>
		{
			// Default Constructor
			public ByteVector() : base() { }

			// Constructor: byteVector v2 = new byteVector( v1 );
			public ByteVector( ByteVector that ) : base() { this.CopyFromArray( that.ToArray() ); return; }
			// Converter: byteVector v2 = byteVector.FromVector( v1 );
			public static ByteVector FromVector( ByteVector that ) { return new ByteVector( that ); }

			// Constructor: byteVector v2 = new byteVector( v1.ToArray() );
			public ByteVector( byte[] that ) : base() { this.CopyFromArray( that ); return; }
			// Converter: byteVector v2 = byteVector.FromArray( v1.ToArray() );
			public static ByteVector FromArray( byte[] that ) { return new ByteVector( that ); }

			//NOTE: Duplicate definitions are generated when base type is 'byte'.
			//// Constructor: byteVector v2 = new byteVector( v1.ToByteArray() );
			//public byteVector( byte[] that ) : base() { this.CopyFromByteArray( that ); return; }
			//// Converter: byteVector v2 = byteVector.FromByteArray( v1.ToByteArray() );
			//public static byteVector FromByteArray( byte[] that ) { return new byteVector( that ); }

			// Constructor: byteVector v2 = new byteVector( v1.ToStream() );
			public ByteVector( System.IO.Stream that ) : base() { this.CopyFromStream( that ); return; }
			// Converter: byteVector v2 = byteVector.FromStream( v1.ToStream() );
			public static ByteVector FromStream( System.IO.Stream that ) { return new ByteVector( that ); }

			// Clone: byteVector v2 = v1.Clone();
			public ByteVector Clone() { return new ByteVector( this ); }
		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class ShortVector : GenericVector<short>
		{
			// Default Constructor
			public ShortVector() : base() { }

			// Constructor: shortVector v2 = new shortVector( v1 );
			public ShortVector( ShortVector that ) : base() { this.CopyFromArray( that.ToArray() ); return; }
			// Converter: shortVector v2 = shortVector.FromVector( v1 );
			public static ShortVector FromVector( ShortVector that ) { return new ShortVector( that ); }

			// Constructor: shortVector v2 = new shortVector( v1.ToArray() );
			public ShortVector( short[] that ) : base() { this.CopyFromArray( that ); return; }
			// Converter: shortVector v2 = shortVector.FromArray( v1.ToArray() );
			public static ShortVector FromArray( short[] that ) { return new ShortVector( that ); }

			// Constructor: shortVector v2 = new shortVector( v1.ToByteArray() );
			public ShortVector( byte[] that ) : base() { this.CopyFromByteArray( that ); return; }
			// Converter: shortVector v2 = shortVector.FromByteArray( v1.ToByteArray() );
			public static ShortVector FromByteArray( byte[] that ) { return new ShortVector( that ); }

			// Constructor: shortVector v2 = new shortVector( v1.ToStream() );
			public ShortVector( System.IO.Stream that ) : base() { this.CopyFromStream( that ); return; }
			// Converter: shortVector v2 = shortVector.FromStream( v1.ToStream() );
			public static ShortVector FromStream( System.IO.Stream that ) { return new ShortVector( that ); }

			// Clone: shortVector v2 = v1.Clone();
			public ShortVector Clone() { return new ShortVector( this ); }
		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class IntVector : GenericVector<int>
		{
			// Default Constructor
			public IntVector() : base() { }

			// Constructor: intVector v2 = new intVector( v1 );
			public IntVector( IntVector that ) : base() { this.CopyFromArray( that.ToArray() ); return; }
			// Converter: intVector v2 = intVector.FromVector( v1 );
			public static IntVector FromVector( IntVector that ) { return new IntVector( that ); }

			// Constructor: intVector v2 = new intVector( v1.ToArray() );
			public IntVector( int[] that ) : base() { this.CopyFromArray( that ); return; }
			// Converter: intVector v2 = intVector.FromArray( v1.ToArray() );
			public static IntVector FromArray( int[] that ) { return new IntVector( that ); }

			// Constructor: intVector v2 = new intVector( v1.ToByteArray() );
			public IntVector( byte[] that ) : base() { this.CopyFromByteArray( that ); return; }
			// Converter: intVector v2 = intVector.FromByteArray( v1.ToByteArray() );
			public static IntVector FromByteArray( byte[] that ) { return new IntVector( that ); }

			// Constructor: intVector v2 = new intVector( v1.ToStream() );
			public IntVector( System.IO.Stream that ) : base() { this.CopyFromStream( that ); return; }
			// Converter: intVector v2 = intVector.FromStream( v1.ToStream() );
			public static IntVector FromStream( System.IO.Stream that ) { return new IntVector( that ); }

			// Clone: intVector v2 = v1.Clone();
			public IntVector Clone() { return new IntVector( this ); }
		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class LongVector : GenericVector<long>
		{
			// Default Constructor
			public LongVector() : base() { }

			// Constructor: longVector v2 = new longVector( v1 );
			public LongVector( LongVector that ) : base() { this.CopyFromArray( that.ToArray() ); return; }
			// Converter: longVector v2 = longVector.FromVector( v1 );
			public static LongVector FromVector( LongVector that ) { return new LongVector( that ); }

			// Constructor: longVector v2 = new longVector( v1.ToArray() );
			public LongVector( long[] that ) : base() { this.CopyFromArray( that ); return; }
			// Converter: longVector v2 = longVector.FromArray( v1.ToArray() );
			public static LongVector FromArray( long[] that ) { return new LongVector( that ); }

			// Constructor: longVector v2 = new longVector( v1.ToByteArray() );
			public LongVector( byte[] that ) : base() { this.CopyFromByteArray( that ); return; }
			// Converter: longVector v2 = longVector.FromByteArray( v1.ToByteArray() );
			public static LongVector FromByteArray( byte[] that ) { return new LongVector( that ); }

			// Constructor: longVector v2 = new longVector( v1.ToStream() );
			public LongVector( System.IO.Stream that ) : base() { this.CopyFromStream( that ); return; }
			// Converter: longVector v2 = longVector.FromStream( v1.ToStream() );
			public static LongVector FromStream( System.IO.Stream that ) { return new LongVector( that ); }

			// Clone: longVector v2 = v1.Clone();
			public LongVector Clone() { return new LongVector( this ); }
		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class StringVector : GenericVector<string>
		{
			// Default Constructor
			public StringVector() : base() { }

			// Constructor: stringVector v2 = new stringVector( v1 );
			public StringVector( StringVector that ) : base() { this.CopyFromArray( that.ToArray() ); return; }
			// Converter: stringVector v2 = stringVector.FromVector( v1 );
			public static StringVector FromVector( StringVector that ) { return new StringVector( that ); }

			// Constructor: stringVector v2 = new stringVector( v1.ToArray() );
			public StringVector( string[] that ) : base() { this.CopyFromArray( that ); return; }
			// Converter: stringVector v2 = stringVector.FromArray( v1.ToArray() );
			public static StringVector FromArray( string[] that ) { return new StringVector( that ); }

			// Constructor: stringVector v2 = new stringVector( v1.ToByteArray() );
			public StringVector( byte[] that ) : base() { this.CopyFromByteArray( that ); return; }
			// Converter: stringVector v2 = stringVector.FromByteArray( v1.ToByteArray() );
			public static StringVector FromByteArray( byte[] that ) { return new StringVector( that ); }

			// Constructor: stringVector v2 = new stringVector( v1.ToStream() );
			public StringVector( System.IO.Stream that ) : base() { this.CopyFromStream( that ); return; }
			// Converter: stringVector v2 = stringVector.FromStream( v1.ToStream() );
			public static StringVector FromStream( System.IO.Stream that ) { return new StringVector( that ); }

			// Clone: stringVector v2 = v1.Clone();
			public StringVector Clone() { return new StringVector( this ); }
		}

		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\
		//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\\

		[Serializable]
		public class DateTimeVector : GenericVector<DateTime>
		{
			// Default Constructor
			public DateTimeVector() : base() { }

			// Constructor: DateTimeVector v2 = new DateTimeVector( v1 );
			public DateTimeVector( DateTimeVector that ) : base() { this.CopyFromArray( that.ToArray() ); return; }
			// Converter: DateTimeVector v2 = DateTimeVector.FromVector( v1 );
			public static DateTimeVector FromVector( DateTimeVector that ) { return new DateTimeVector( that ); }

			// Constructor: DateTimeVector v2 = new DateTimeVector( v1.ToArray() );
			public DateTimeVector( DateTime[] that ) : base() { this.CopyFromArray( that ); return; }
			// Converter: DateTimeVector v2 = DateTimeVector.FromArray( v1.ToArray() );
			public static DateTimeVector FromArray( DateTime[] that ) { return new DateTimeVector( that ); }

			// Constructor: DateTimeVector v2 = new DateTimeVector( v1.ToByteArray() );
			public DateTimeVector( byte[] that ) : base() { this.CopyFromByteArray( that ); return; }
			// Converter: DateTimeVector v2 = DateTimeVector.FromByteArray( v1.ToByteArray() );
			public static DateTimeVector FromByteArray( byte[] that ) { return new DateTimeVector( that ); }

			// Constructor: DateTimeVector v2 = new DateTimeVector( v1.ToStream() );
			public DateTimeVector( System.IO.Stream that ) : base() { this.CopyFromStream( that ); return; }
			// Converter: DateTimeVector v2 = DateTimeVector.FromStream( v1.ToStream() );
			public static DateTimeVector FromStream( System.IO.Stream that ) { return new DateTimeVector( that ); }

			// Clone: DateTimeVector v2 = v1.Clone();
			public DateTimeVector Clone() { return new DateTimeVector( this ); }
		}

	}
}
