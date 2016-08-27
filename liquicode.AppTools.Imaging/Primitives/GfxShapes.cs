

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;


namespace liquicode.AppTools
{
	public static partial class Imaging
	{


		//-----------------------------------------------------
		/// <summary>
		/// Represents a null rectangle with a width and height of -1.
		/// </summary>
		public static Rectangle NullRectangle = new Rectangle( 0, 0, -1, -1 );


		//-----------------------------------------------------
		/// <summary>
		/// Represents the largest rectangle possible.
		/// </summary>
		public static Rectangle LargestRectangle = new Rectangle( int.MinValue, int.MinValue, int.MaxValue, int.MaxValue );


		//-----------------------------------------------------
		/// <summary>
		/// Represents a null region which contains a single NullRectangle.
		/// </summary>
		public static Region NullRegion = new Region( NullRectangle );


		//-----------------------------------------------------
		/// <summary>
		/// Arranges a content rectangle in relation to another rectangle.
		/// </summary>
		/// <param name="Fixed">The fixed rectangle. The content rectangle will be arranged around this rectangle.</param>
		/// <param name="Content">The size of the content.</param>
		/// <param name="Alignment">The alignment of the content. This determines how the content rectangle will be arranged.</param>
		/// <returns>The content rectangle. This rectangle will be located within the bounding rectangle.</returns>
		public static Rectangle ArrangeContentRectangle( Rectangle Fixed, Size Content, ContentAlignment Alignment )
		{
			Point location = new Point();
			switch( Alignment )
			{
				case ContentAlignment.TopLeft:
					location.Y = Fixed.Top - Content.Height;
					location.X = Fixed.Left - Content.Width;
					break;
				case ContentAlignment.TopCenter:
					location.Y = Fixed.Top - Content.Height;
					location.X = (Fixed.Left + Convert.ToInt32( (Fixed.Width / 2) - (Content.Width / 2) ));
					break;
				case ContentAlignment.TopRight:
					location.Y = Fixed.Top - Content.Height;
					location.X = Fixed.Right;
					break;
				case ContentAlignment.MiddleLeft:
					location.Y = (Fixed.Top + Convert.ToInt32( (Fixed.Height / 2) - (Content.Height / 2) ));
					location.X = Fixed.Left - Content.Width;
					break;
				case ContentAlignment.MiddleCenter:
					location.Y = (Fixed.Top + Convert.ToInt32( (Fixed.Height / 2) - (Content.Height / 2) ));
					location.X = (Fixed.Left + Convert.ToInt32( (Fixed.Width / 2) - (Content.Width / 2) ));
					break;
				case ContentAlignment.MiddleRight:
					location.Y = (Fixed.Top + Convert.ToInt32( (Fixed.Height / 2) - (Content.Height / 2) ));
					location.X = Fixed.Right;
					break;
				case ContentAlignment.BottomLeft:
					location.Y = Fixed.Bottom;
					location.X = Fixed.Left - Content.Width;
					break;
				case ContentAlignment.BottomCenter:
					location.Y = Fixed.Bottom;
					location.X = (Fixed.Left + Convert.ToInt32( (Fixed.Width / 2) - (Content.Width / 2) ));
					break;
				case ContentAlignment.BottomRight:
					location.Y = Fixed.Bottom;
					location.X = Fixed.Right;
					break;
			}
			return new Rectangle( location, Content );
		}

		//-----------------------------------------------------
		/// <summary>
		/// Determines the location and size of content within a bounding rectangle.
		/// </summary>
		/// <param name="Bounds">The bounding rectangle. The content rectangle will be located within this rectangle.</param>
		/// <param name="Content">The size of the content.</param>
		/// <param name="Alignment">The alignment of the content. This determines the location of the content withing the bounding rectangle.</param>
		/// <returns>The content rectangle. This rectangle will be located within the bounding rectangle.</returns>
		public static Rectangle LocateContentRectangle( Rectangle Bounds, Size Content, ContentAlignment Alignment )
		{
			// Adjust content size.
			Size size = new Size();
			if( (Content.Width > Bounds.Width) && (Content.Height > Bounds.Height) )
			{
				if( (Content.Width - Bounds.Width) >= (Content.Height - Bounds.Height) )
				{
					size.Height = Convert.ToInt32( Content.Height * (Bounds.Width / Content.Width) );
					size.Width = Bounds.Width;
				}
				else
				{
					size.Width = Convert.ToInt32( Content.Width * (Bounds.Height / Content.Height) );
					size.Height = Bounds.Height;
				}
			}
			else if( Content.Width > Bounds.Width )
			{
				size.Height = Convert.ToInt32( Content.Height * (Bounds.Width / Content.Width) );
				size.Width = Bounds.Width;
			}
			else if( Content.Height > Bounds.Height )
			{
				size.Width = Convert.ToInt32( Content.Width * (Bounds.Height / Content.Height) );
				size.Height = Bounds.Height;
			}
			else
			{
				size.Width = Content.Width;
				size.Height = Content.Height;
			}
			// Adjust content location.
			Point location = new Point();
			switch( Alignment )
			{
				case ContentAlignment.TopLeft:
					location.Y = Bounds.Top;
					location.X = Bounds.Left;
					break;
				case ContentAlignment.TopCenter:
					location.Y = Bounds.Top;
					location.X = (Bounds.Left + Convert.ToInt32( (Bounds.Width / 2) - (size.Width / 2) ));
					break;
				case ContentAlignment.TopRight:
					location.Y = Bounds.Top;
					location.X = (Bounds.Right - size.Width);
					break;
				case ContentAlignment.MiddleLeft:
					location.Y = (Bounds.Top + Convert.ToInt32( (Bounds.Height / 2) - (size.Height / 2) ));
					location.X = Bounds.Left;
					break;
				case ContentAlignment.MiddleCenter:
					location.Y = (Bounds.Top + Convert.ToInt32( (Bounds.Height / 2) - (size.Height / 2) ));
					location.X = (Bounds.Left + Convert.ToInt32( (Bounds.Width / 2) - (size.Width / 2) ));
					break;
				case ContentAlignment.MiddleRight:
					location.Y = (Bounds.Top + Convert.ToInt32( (Bounds.Height / 2) - (size.Height / 2) ));
					location.X = (Bounds.Right - size.Width);
					break;
				case ContentAlignment.BottomLeft:
					location.Y = (Bounds.Bottom - size.Height);
					location.X = Bounds.Left;
					break;
				case ContentAlignment.BottomCenter:
					location.Y = (Bounds.Bottom - size.Height);
					location.X = (Bounds.Left + Convert.ToInt32( (Bounds.Width / 2) - (size.Width / 2) ));
					break;
				case ContentAlignment.BottomRight:
					location.Y = (Bounds.Bottom - size.Height);
					location.X = (Bounds.Right - size.Width);
					break;
			}
			return new Rectangle( location, size );
		}


		//-----------------------------------------------------
		/// <summary>
		/// Gets the list of rectangles within a bounding rectangle which describe the area not being used by the occupied rectangle.
		/// </summary>
		/// <param name="Bounds">The bounding rectangle. The unoccupied areas will be located within this rectangle.</param>
		/// <param name="Occupied">The occupied area within the bounding rectangle.</param>
		/// <returns>A list of rectangles. </returns>
		public static List<Rectangle> GetUnoccupiedRectangles( Rectangle Bounds, Rectangle Occupied )
		{
			List<Rectangle> rectangles = new List<Rectangle>();
			// Left side.
			rectangles.Add( new Rectangle( Bounds.Left, Bounds.Top, Occupied.Left - Bounds.Left, Bounds.Height ) );
			// Top side.
			rectangles.Add( new Rectangle( Bounds.Left, Bounds.Top, Bounds.Width, Occupied.Top - Bounds.Top ) );
			// Right side.
			rectangles.Add( new Rectangle( Occupied.Right, Bounds.Top, Bounds.Right - Occupied.Right, Bounds.Height ) );
			// Bottom side.
			rectangles.Add( new Rectangle( Bounds.Left, Occupied.Bottom, Bounds.Width, Bounds.Bottom - Occupied.Bottom ) );
			return rectangles;
		}


		//-----------------------------------------------------
		/// <summary>
		/// Gets the largest rectangle from a list of rectangles.
		/// </summary>
		/// <param name="Rectangles">The list of rectangles.</param>
		/// <returns>The rectangle with the largest area.</returns>
		public static Rectangle GetLargestRectangle( List<Rectangle> Rectangles )
		{
			Rectangle largest = new Rectangle();
			foreach( Rectangle rectangle in Rectangles )
			{
				if( ((long)rectangle.Width * rectangle.Height) > ((long)largest.Width * largest.Height) )
				{
					largest = rectangle;
				}
			}
			return largest;
		}


		//-----------------------------------------------------
		public static Region GetRoundedRectangleRegion( RectangleF Bounds, float Radius )
		{
			// If corner radius is less than or equal to zero, return the original(rectangle)
			if( Radius <= 0 ) { return new Region( Bounds ); }

			// If corner radius is greater than or equal to half the width or height (whichever is shorter) then
			// return a capsule instead of a lozenge.
			if( Radius >= (Math.Min( Bounds.Width, Bounds.Height ) / 2.0) ) { return GetCapsuleRegion( Bounds ); }

			float Diameter = Radius + Radius;
			RectangleF ArcRect = new RectangleF( Bounds.Location, new SizeF( Diameter, Diameter ) );
			GraphicsPath gpath = new GraphicsPath();
			{
				// top left arc
				gpath.AddArc( ArcRect, 180, 90 );
				// top right arc
				ArcRect.X = Bounds.Right - Diameter;
				gpath.AddArc( ArcRect, 270, 90 );
				// bottom right arc
				ArcRect.Y = Bounds.Bottom - Diameter;
				gpath.AddArc( ArcRect, 0, 90 );
				// bottom left arc
				ArcRect.X = Bounds.Left;
				gpath.AddArc( ArcRect, 90, 90 );
				// Close figure
				gpath.CloseFigure();
			}
			return new Region( gpath );
		}


		//-----------------------------------------------------
		public static Region GetCapsuleRegion( RectangleF Bounds )
		{
			float Diameter = 0;
			RectangleF ArcRect = default( RectangleF );
			GraphicsPath gpath = new GraphicsPath();
			try
			{
				if( Bounds.Width > Bounds.Height )
				{
					// Return horizontal capsule
					Diameter = Bounds.Height;
					ArcRect = new RectangleF( Bounds.Location, new SizeF( Diameter, Diameter ) );
					gpath.AddArc( ArcRect, 90, 180 );
					ArcRect.X = Bounds.Right - Diameter;
					gpath.AddArc( ArcRect, 270, 180 );
				}
				else if( Bounds.Height > Bounds.Width )
				{
					// Return vertical capsule
					Diameter = Bounds.Width;
					ArcRect = new RectangleF( Bounds.Location, new SizeF( Diameter, Diameter ) );
					gpath.AddArc( ArcRect, 180, 180 );
					ArcRect.Y = Bounds.Bottom - Diameter;
					gpath.AddArc( ArcRect, 0, 180 );
				}
				else
				{
					// return circle
					gpath.AddEllipse( Bounds );
				}
			}
			catch( Exception )
			{
				gpath.AddEllipse( Bounds );
			}
			finally
			{
				gpath.CloseFigure();
			}
			return new Region( gpath );
		}


		//-----------------------------------------------------
		public static Rectangle Size2Rectangle( Size Size )
		{
			return new Rectangle( 0, 0, Size.Width, Size.Height );
		}


		//-----------------------------------------------------
		public static Rectangle CloneRectangle( Rectangle Rectangle )
		{
			return new Rectangle( Rectangle.Location, Rectangle.Size );
		}


	}


}
