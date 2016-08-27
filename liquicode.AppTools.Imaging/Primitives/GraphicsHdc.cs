

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace liquicode.AppTools
{
	public static partial class Imaging
	{
		public class GraphicsHdc : IDisposable
		{


			//---------------------------------------------------------------------
			private Graphics _Graphics = null;
			private IntPtr _Hdc = IntPtr.Zero;
			public IntPtr Hdc { get { return this._Hdc; } }


			//---------------------------------------------------------------------
			public GraphicsHdc( Graphics ThisGraphics )
			{
				this._Graphics = ThisGraphics;
				this._Hdc = this._Graphics.GetHdc();
				return;
			}


			//---------------------------------------------------------------------
			void IDisposable.Dispose()
			{
				if( (this._Graphics != null) && (this._Hdc != IntPtr.Zero) )
				{
					this._Graphics.ReleaseHdc( this._Hdc );
				}
				return;
			}


		}
	}
}
