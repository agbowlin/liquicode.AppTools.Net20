

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace liquicode.AppTools
{
	public class ApplicationBusy : IDisposable
	{


		//---------------------------------------------------------------------
		private Cursor _PreviousCursor = Cursors.Default;
		private DateTime _StartTime = DateTime.Now;


		//---------------------------------------------------------------------
		public ApplicationBusy( int YieldMS = 0 )
		{
			this._PreviousCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			this.Yield( YieldMS );
			this._StartTime = DateTime.Now;
			return;
		}


		//---------------------------------------------------------------------
		public void Yield( int YieldMS )
		{
			if( YieldMS > 0 )
			{
				System.Threading.Thread.Sleep( YieldMS );
			}
			return;
		}


		//---------------------------------------------------------------------
		public double ElapsedMilliseconds
		{
			get
			{
				double ms = (DateTime.Now - this._StartTime).TotalMilliseconds;
				return ms;
			}
		}


		//---------------------------------------------------------------------
		void IDisposable.Dispose()
		{
			Cursor.Current = this._PreviousCursor;
			return;
		}


	}
}
