//ADAPTED FROM: http://www.codeguru.com/csharp/csharp/cs_syntax/controls/article.php/c5849/Docking-Control-in-C-That-Can-Be-Dragged-and-Resized.htm
//AGB: Changed the HotLength to 25% of window size when dragging the control around to re-dock it.
//AGB: Added a show/hide functionality.


using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;


//class ExampleForm : Form
//{
//    public static void Main()
//    {
//        Application.Run( new ExampleForm() );
//    }

//    public ExampleForm()
//    {
//        Controls.Add( new DockingControl( this, DockStyle.Left, new MonthCalendar() ) );
//        Controls.Add( new DockingControl( this, DockStyle.Top, new RichTextBox() ) );
//    }
//}


namespace liquicode.AppTools
{

	//=====================================================================
	//=====================================================================


	// Allow an arbitrary control to be moved between docking positions and resized
	
	public class DockingControl
					: UserControl
	{


		//=====================================================================
		private Form _HostForm;							// The Form hosting this docking control
		private DockingControlResizer _ResizeControl;	// Provide resizing functionality
		private DockingControlDragger _HandleControl;	// Handle for grabbing and moving
		private DockingControlBorder _BorderControl;	// Wrapper to place border around user control


		//=====================================================================
		public int MinimumControlSize = 10;
		public int HiddenControlSize = 30;

		private bool _IsHidden = false;
		private int _LastShownSize = 300;
		private int _LastShownClientSize = 300;


		//=====================================================================
		public DockingControl( Form form, DockStyle ds, Control userControl )
		{
			// Remember the form we are hosted on
			this._HostForm = form;

			// Create the resizing bar, gripper handle and border control
			this._ResizeControl = new DockingControlResizer( ds );
			this._HandleControl = new DockingControlDragger( this, ds );
			this._BorderControl = new DockingControlBorder( userControl );

			// Wrapper should always fill remaining area
			this._BorderControl.Dock = DockStyle.Fill;

			// Define our own initial docking position for when we are added to host form
			this.Dock = ds;

			// NOTE: Order of array contents is important
			// Controls in the array are positioned from right to left when the 
			// form makes size/position calculations for docking controls, so the
			// _BorderControl is placed last in calculation (therefore first in array) 
			// because we want it to fill the remaining space.
			Controls.AddRange( new Control[] { this._BorderControl, this._HandleControl, this._ResizeControl } );

			return;
		}


		//=====================================================================
		public Form HostForm
		{
			get
			{
				return this._HostForm;
			}
		}


		//=====================================================================
		public int LastShownSize
		{
			get
			{
				return this._LastShownSize;
			}
		}


		//=====================================================================
		public bool IsHidden
		{
			get
			{
				return this._IsHidden;
			}
			set
			{
				if( value )
				{
					if( (this.Dock == DockStyle.Top) || (this.Dock == DockStyle.Bottom) )
					{
						if( this._IsHidden == false )
						{
							this._LastShownClientSize = this.ClientSize.Height;
						}
						this._IsHidden = true;
						this.ClientSize = new Size( 0, this.HiddenControlSize );
					}
					else if( (this.Dock == DockStyle.Left) || (this.Dock == DockStyle.Right) )
					{
						if( this._IsHidden == false )
						{
							this._LastShownClientSize = this.ClientSize.Width;
						}
						this._IsHidden = true;
						this.ClientSize = new Size( this.HiddenControlSize, 0 );
					}
				}
				else
				{
					if( (this.Dock == DockStyle.Top) || (this.Dock == DockStyle.Bottom) )
					{
						this._IsHidden = false;
						this.ClientSize = new Size( 0, this._LastShownClientSize );
					}
					else if( (this.Dock == DockStyle.Left) || (this.Dock == DockStyle.Right) )
					{
						this._IsHidden = false;
						this.ClientSize = new Size( this._LastShownClientSize, 0 );
					}
				}
				this.Invalidate();
				return;
			}
		}


		//=====================================================================
		protected override void OnResize( EventArgs e )
		{
			if( this._IsHidden == false )
			{
				if( (this.Dock == DockStyle.Top) || (this.Dock == DockStyle.Bottom) )
				{
					if( this.Height < this.MinimumControlSize )
					{
						this.Height = this.MinimumControlSize;
					}
					this._LastShownSize = this.Height;
					this._LastShownClientSize = this.ClientSize.Height;
				}
				else if( (this.Dock == DockStyle.Left) || (this.Dock == DockStyle.Right) )
				{
					if( this.Width < this.MinimumControlSize )
					{
						this.Width = this.MinimumControlSize;
					}
					this._LastShownSize = this.Width;
					this._LastShownClientSize = this.ClientSize.Width;
				}
			}
			base.OnResize( e );
			return;
		}


		//=====================================================================
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}

			set
			{
				// Our size before docking position is changed
				Size size = this.ClientSize;

				// Remember the current docking position
				DockStyle dsOldResize = this._ResizeControl.Dock;

				// New handle size is dependant on the orientation of the new docking position
				this._HandleControl.SizeToOrientation( value );

				// Modify docking position of child controls based on our new docking position
				this._ResizeControl.Dock = DockingControl.ResizeStyleFromControlStyle( value );
				this._HandleControl.Dock = DockingControl.HandleStyleFromControlStyle( value );

				// Now safe to update ourself through base class
				base.Dock = value;

				// Change in orientation occured?
				if( dsOldResize != this._ResizeControl.Dock )
				{
					// Must update our client size to ensure the correct size is used when
					// the docking position changes.  We have to transfer the value that determines
					// the vector of the control to the opposite dimension
					if( (this.Dock == DockStyle.Top) || (this.Dock == DockStyle.Bottom) )
					{
						size.Height = size.Width;
					}
					else if( (this.Dock == DockStyle.Left) || (this.Dock == DockStyle.Right) )
					{
						size.Width = size.Height;
					}
					this.ClientSize = size;
				}

				// Repaint of the our controls 
				_HandleControl.Invalidate();
				_ResizeControl.Invalidate();
				return;
			}
		}


		//=====================================================================
		// Static variables defining colours for drawing
		private static Pen _lightPen = new Pen( Color.FromKnownColor( KnownColor.ControlLightLight ) );
		private static Pen _darkPen = new Pen( Color.FromKnownColor( KnownColor.ControlDark ) );
		private static Brush _plainBrush = Brushes.LightGray;


		//=====================================================================
		// Static properties for read-only access to drawing colours
		public static Pen LightPen { get { return _lightPen; } }
		public static Pen DarkPen { get { return _darkPen; } }
		public static Brush PlainBrush { get { return _plainBrush; } }


		//=====================================================================
		public static DockStyle ResizeStyleFromControlStyle( DockStyle ds )
		{
			switch( ds )
			{
				case DockStyle.Left:
					return DockStyle.Right;
				case DockStyle.Top:
					return DockStyle.Bottom;
				case DockStyle.Right:
					return DockStyle.Left;
				case DockStyle.Bottom:
					return DockStyle.Top;
				default:
					// Should never happen!
					throw new ApplicationException( "Invalid DockStyle argument" );
			}
		}


		//=====================================================================
		public static DockStyle HandleStyleFromControlStyle( DockStyle ds )
		{
			switch( ds )
			{
				case DockStyle.Left:
					return DockStyle.Top;
				case DockStyle.Top:
					return DockStyle.Left;
				case DockStyle.Right:
					return DockStyle.Top;
				case DockStyle.Bottom:
					return DockStyle.Left;
				default:
					// Should never happen!
					throw new ApplicationException( "Invalid DockStyle argument" );
			}
		}


	}


	//=====================================================================
	//=====================================================================


	// A bar used to resize the parent DockingControl

	public class DockingControlResizer
					: UserControl
	{


		//=====================================================================
		private const int FIXED_LENGTH = 6;


		//=====================================================================
		private Point _PointStart;
		private Point _PointLast;
		private Size _Size;


		//=====================================================================
		public DockingControlResizer( DockStyle ds )
		{
			this.Dock = DockingControl.ResizeStyleFromControlStyle( ds );
			this.Size = new Size( DockingControlResizer.FIXED_LENGTH, DockingControlResizer.FIXED_LENGTH );
			return;
		}


		//=====================================================================
		protected override void OnMouseDown( MouseEventArgs e )
		{
			// Remember the mouse position and client size when capture occured
			this._PointStart = this._PointLast = PointToScreen( new Point( e.X, e.Y ) );
			this._Size = Parent.ClientSize;

			// Ensure delegates are called
			base.OnMouseDown( e );
			return;
		}


		//=====================================================================
		protected override void OnMouseMove( MouseEventArgs e )
		{
			DockingControl docking_control = (DockingControl)this.Parent;

			if( docking_control.IsHidden )
			{
				this.Cursor = Cursors.Default;
				base.OnMouseMove( e );
				return;
			}

			// Cursor depends on if we a vertical or horizontal resize
			if( (this.Dock == DockStyle.Top) || (this.Dock == DockStyle.Bottom) )
			{
				this.Cursor = Cursors.HSplit;
			}
			else if( (this.Dock == DockStyle.Left) || (this.Dock == DockStyle.Right) )
			{
				this.Cursor = Cursors.VSplit;
			}

			// Can only resize if we have captured the mouse
			if( this.Capture )
			{
				// Find the new mouse position
				Point point = PointToScreen( new Point( e.X, e.Y ) );

				// Have we actually moved the mouse?
				if( point != this._PointLast )
				{
					// Update the last processed mouse position
					this._PointLast = point;

					// Find delta from original position
					int xDelta = this._PointLast.X - this._PointStart.X;
					int yDelta = this._PointLast.Y - this._PointStart.Y;

					// Resizing from bottom or right of form means inverse movements
					if( (this.Dock == DockStyle.Top) ||
						(this.Dock == DockStyle.Left) )
					{
						xDelta = -xDelta;
						yDelta = -yDelta;
					}

					// New size is original size plus delta
					if( (this.Dock == DockStyle.Top) || (this.Dock == DockStyle.Bottom) )
					{
						Parent.ClientSize = new Size( this._Size.Width, this._Size.Height + yDelta );
					}
					else if( (this.Dock == DockStyle.Left) || (this.Dock == DockStyle.Right) )
					{
						Parent.ClientSize = new Size( this._Size.Width + xDelta, this._Size.Height );
					}

					// Force a repaint of parent so we can see changed appearance
					Parent.Refresh();
				}
			}

			// Ensure delegates are called
			base.OnMouseMove( e );
			return;
		}


		//=====================================================================
		protected override void OnPaint( PaintEventArgs pe )
		{
			// Create objects used for drawing
			Point[] ptLight = new Point[ 2 ];
			Point[] ptDark = new Point[ 2 ];
			Rectangle rectMiddle = new Rectangle();

			// Drawing is relative to client area
			Size sizeClient = this.ClientSize;

			// Painting depends on orientation
			if( (this.Dock == DockStyle.Top) || (this.Dock == DockStyle.Bottom) )
			{
				// Draw as a horizontal bar
				ptDark[ 1 ].Y = ptDark[ 0 ].Y = sizeClient.Height - 1;
				ptLight[ 1 ].X = ptDark[ 1 ].X = sizeClient.Width;
				rectMiddle.Width = sizeClient.Width;
				rectMiddle.Height = sizeClient.Height - 2;
				rectMiddle.X = 0;
				rectMiddle.Y = 1;
			}
			else if( (this.Dock == DockStyle.Left) || (this.Dock == DockStyle.Right) )
			{
				// Draw as a vertical bar
				ptDark[ 1 ].X = ptDark[ 0 ].X = sizeClient.Width - 1;
				ptLight[ 1 ].Y = ptDark[ 1 ].Y = sizeClient.Height;
				rectMiddle.Width = sizeClient.Width - 2;
				rectMiddle.Height = sizeClient.Height;
				rectMiddle.X = 1;
				rectMiddle.Y = 0;
			}

			// Use colours defined by docking control that is using us
			pe.Graphics.DrawLine( DockingControl.LightPen, ptLight[ 0 ], ptLight[ 1 ] );
			pe.Graphics.DrawLine( DockingControl.DarkPen, ptDark[ 0 ], ptDark[ 1 ] );
			pe.Graphics.FillRectangle( DockingControl.PlainBrush, rectMiddle );

			// Ensure delegates are called
			base.OnPaint( pe );
			return;
		}


	}


	//=====================================================================
	//=====================================================================


	// The visible handle used to drag the parent DockingControl around.

	public class DockingControlDragger
					: UserControl
	{


		//=====================================================================
		// Class constants
		private const int FIXED_LENGTH = 12;
		private const int MINIMUM_HOT_LENGTH = 20;
		private const int OFFSET = 3;
		private const int INSET = 3;


		//=====================================================================
		private DockingControl _DockingControl = null;


		//=====================================================================
		public DockingControlDragger( DockingControl DockingControl, DockStyle ds )
		{
			this.Cursor = Cursors.Hand;
			this._DockingControl = DockingControl;
			this.Dock = DockingControl.HandleStyleFromControlStyle( ds );
			SizeToOrientation( ds );
			return;
		}


		//=====================================================================
		public void SizeToOrientation( DockStyle ds )
		{
			if( (ds == DockStyle.Top) || (ds == DockStyle.Bottom) )
			{
				this.ClientSize = new Size( DockingControlDragger.FIXED_LENGTH, 0 );
			}
			else if( (ds == DockStyle.Left) || (ds == DockStyle.Right) )
			{
				this.ClientSize = new Size( 0, DockingControlDragger.FIXED_LENGTH );
			}
			return;
		}


		//=====================================================================
		protected override void OnMouseDoubleClick( MouseEventArgs e )
		{
			this._DockingControl.IsHidden = !this._DockingControl.IsHidden;

			// Ensure delegates are called
			base.OnMouseDoubleClick( e );
			return;
		}


		//=====================================================================
		protected override void OnMouseMove( MouseEventArgs e )
		{
			// Can only move the DockingControl is we have captured the
			// mouse otherwise the mouse is not currently pressed
			if( this.Capture )
			{
				// Must have reference to parent object
				if( null != this._DockingControl )
				{
					this.Cursor = Cursors.SizeAll;

					// Convert from client point of DockingHandle to client of DockingControl
					Point screenPoint = PointToScreen( new Point( e.X, e.Y ) );
					Point parentPoint = this._DockingControl.HostForm.PointToClient( screenPoint );

					// Find the client rectangle of the form
					int hot_length = 0;
					Size parentSize = this._DockingControl.HostForm.ClientSize;
					if( parentSize.Width > parentSize.Height )
					{
						hot_length = (int)(parentSize.Height * 0.25);
					}
					else
					{
						hot_length = (int)(parentSize.Width * 0.25);
					}
					if( hot_length < DockingControlDragger.MINIMUM_HOT_LENGTH )
					{
						hot_length = DockingControlDragger.MINIMUM_HOT_LENGTH;
					}

					// New docking position is defaulted to current style
					DockStyle ds = this._DockingControl.Dock;

					// Find new docking position
					if( parentPoint.X < hot_length )
					{
						ds = DockStyle.Left;
					}
					else if( parentPoint.Y < hot_length )
					{
						ds = DockStyle.Top;
					}
					else if( parentPoint.X >= (parentSize.Width - hot_length) )
					{
						ds = DockStyle.Right;
					}
					else if( parentPoint.Y >= (parentSize.Height - hot_length) )
					{
						ds = DockStyle.Bottom;
					}

					// Update docking position of DockingControl we are part of
					if( this._DockingControl.Dock != ds )
						this._DockingControl.Dock = ds;
				}
			}
			else
			{
				this.Cursor = Cursors.Hand;
			}

			// Ensure delegates are called
			base.OnMouseMove( e );
			return;
		}


		//=====================================================================
		protected override void OnPaint( PaintEventArgs pe )
		{
			Size sizeClient = this.ClientSize;
			Point[] ptLight = new Point[ 4 ];
			Point[] ptDark = new Point[ 4 ];

			// Depends on orientation
			if( (this._DockingControl.Dock == DockStyle.Top) ||
				(this._DockingControl.Dock == DockStyle.Bottom) )
			{
				int iBottom = sizeClient.Height - INSET - 1;
				int iRight = OFFSET + 2;

				ptLight[ 3 ].X = ptLight[ 2 ].X = ptLight[ 0 ].X = OFFSET;
				ptLight[ 2 ].Y = ptLight[ 1 ].Y = ptLight[ 0 ].Y = INSET;
				ptLight[ 1 ].X = OFFSET + 1;
				ptLight[ 3 ].Y = iBottom;

				ptDark[ 2 ].X = ptDark[ 1 ].X = ptDark[ 0 ].X = iRight;
				ptDark[ 3 ].Y = ptDark[ 2 ].Y = ptDark[ 1 ].Y = iBottom;
				ptDark[ 0 ].Y = INSET;
				ptDark[ 3 ].X = iRight - 1;
			}
			else
			{
				int iBottom = OFFSET + 2;
				int iRight = sizeClient.Width - INSET - 1;

				ptLight[ 3 ].X = ptLight[ 2 ].X = ptLight[ 0 ].X = INSET;
				ptLight[ 1 ].Y = ptLight[ 2 ].Y = ptLight[ 0 ].Y = OFFSET;
				ptLight[ 1 ].X = iRight;
				ptLight[ 3 ].Y = OFFSET + 1;

				ptDark[ 2 ].X = ptDark[ 1 ].X = ptDark[ 0 ].X = iRight;
				ptDark[ 3 ].Y = ptDark[ 2 ].Y = ptDark[ 1 ].Y = iBottom;
				ptDark[ 0 ].Y = OFFSET;
				ptDark[ 3 ].X = INSET;
			}

			Pen lightPen = DockingControl.LightPen;
			Pen darkPen = DockingControl.DarkPen;

			pe.Graphics.DrawLine( lightPen, ptLight[ 0 ], ptLight[ 1 ] );
			pe.Graphics.DrawLine( lightPen, ptLight[ 2 ], ptLight[ 3 ] );
			pe.Graphics.DrawLine( darkPen, ptDark[ 0 ], ptDark[ 1 ] );
			pe.Graphics.DrawLine( darkPen, ptDark[ 2 ], ptDark[ 3 ] );

			// Shift coordinates to draw section grab bar
			if( (this._DockingControl.Dock == DockStyle.Top) ||
				(this._DockingControl.Dock == DockStyle.Bottom) )
			{
				for( int i = 0; i < 4; i++ )
				{
					ptLight[ i ].X += 4;
					ptDark[ i ].X += 4;
				}
			}
			else
			{
				for( int i = 0; i < 4; i++ )
				{
					ptLight[ i ].Y += 4;
					ptDark[ i ].Y += 4;
				}
			}

			pe.Graphics.DrawLine( lightPen, ptLight[ 0 ], ptLight[ 1 ] );
			pe.Graphics.DrawLine( lightPen, ptLight[ 2 ], ptLight[ 3 ] );
			pe.Graphics.DrawLine( darkPen, ptDark[ 0 ], ptDark[ 1 ] );
			pe.Graphics.DrawLine( darkPen, ptDark[ 2 ], ptDark[ 3 ] );

			// Ensure delegates are called
			base.OnPaint( pe );
			return;
		}


	}


	//=====================================================================
	//=====================================================================


	// Position the provided control inside a border to give a portrait picture effect.

	public class DockingControlBorder
					: UserControl
	{


		//=====================================================================
		private int _BorderWidth = 3;
		private Control _UserControl = null;


		//=====================================================================
		public DockingControlBorder( Control userControl )
		{
			this._UserControl = userControl;
			Controls.Add( this._UserControl );
			return;
		}


		//=====================================================================
		public int BorderWidth
		{
			get
			{
				return this._BorderWidth;
			}

			set
			{
				// Only interested if value has changed
				if( this._BorderWidth != value )
				{
					this._BorderWidth = value;

					// Force resize of control to get the embedded control 
					// respositioned according to new border width
					this.Size = this.Size;
				}
				return;
			}
		}


		//=====================================================================
		// Must reposition the embedded control whenever we change size
		protected override void OnResize( EventArgs e )
		{
			// Can be called before instance constructor
			if( null != this._UserControl )
			{
				Size sizeClient = this.Size;

				// Move the user control to enforce the border area we want	
				this._UserControl.Location = new Point( this._BorderWidth, this._BorderWidth );
				this._UserControl.Size = new Size( sizeClient.Width - (2 * this._BorderWidth), sizeClient.Height - (2 * this._BorderWidth) );
			}

			// Ensure delegates are called
			base.OnResize( e );
			return;
		}


	}

}
