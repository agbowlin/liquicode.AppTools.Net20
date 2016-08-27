

using System;
using System.Drawing;
using System.Collections.Generic;


namespace liquicode.AppTools
{
	public static partial class Imaging
	{


		public class ImageStyle
		{


			//-----------------------------------------------------
			public ImagingOptions ImagingOptions = new ImagingOptions();
			public ContentAlignment Alignment = ContentAlignment.TopLeft;
			public Color BackColor = Color.Empty;
			public int Opacity = 255;


			//-----------------------------------------------------
			public ImageStyle Clone()
			{
				ImageStyle value = new ImageStyle();
				value.ImagingOptions = this.ImagingOptions.Clone();
				value.Alignment = this.Alignment;
				value.BackColor = this.BackColor;
				value.Opacity = this.Opacity;
				return value;
			}


			//-----------------------------------------------------
			public Rectangle Draw( Graphics Graphics, Rectangle Rectangle, Image Image )
			{
				if( (this.ImagingOptions != null) )
					this.ImagingOptions.CopyTo( Graphics );
				// Size image.
				Rectangle rectImage = LocateContentRectangle( Rectangle, Image.Size, this.Alignment );
				// Erase background.
				if( !this.BackColor.Equals( Color.Empty ) )
				{
					Graphics.FillRectangle( new SolidBrush( this.BackColor ), rectImage );
				}
				// Draw image.
				if( (this.Opacity >= 255) )
				{
					// Opaque image.
					Graphics.DrawImage( Image, rectImage );
				}
				else if( (this.Opacity > 0) )
				{
					// Translucent image.
					DrawTranslucentImage( Graphics, Image, rectImage, Convert.ToSingle( this.Opacity / 255 ) );
				}
				else
				{
					// No image.
				}
				// Return
				return rectImage;
			}


			//-----------------------------------------------------
			public Rectangle GetInnerRectangle( Rectangle Rectangle, Image Image )
			{
				Rectangle rectImage = LocateContentRectangle( Rectangle, Image.Size, this.Alignment );
				List<Rectangle> rects = GetUnoccupiedRectangles( Rectangle, rectImage );
				Rectangle rectMax = GetLargestRectangle( rects );
				return rectMax;
			}


			//-----------------------------------------------------
			public Rectangle GetInnerRectangle( Rectangle Rectangle, Size ImageSize )
			{
				Rectangle rectImage = LocateContentRectangle( Rectangle, ImageSize, this.Alignment );
				List<Rectangle> rects = GetUnoccupiedRectangles( Rectangle, rectImage );
				Rectangle rectMax = GetLargestRectangle( rects );
				return rectMax;
			}


		}

	}
}
