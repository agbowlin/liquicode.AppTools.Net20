

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;


namespace liquicode.AppTools
{
	public static partial class Imaging
	{


		public class ItemStyle
		{


			//-------------------------------------------------
			//private BorderStyle _BorderStyle = new BorderStyle();
			private BorderStyle _BorderStyle = null;
			public BorderStyle BorderStyle
			{
				get { return this._BorderStyle; }
				set
				{
					this._BorderStyle = value;
					//if( this._BorderStyle == null ) { this._BorderStyle = new BorderStyle(); }
					return;
				}
			}


			//-------------------------------------------------
			//private ImageStyle _ImageStyle = new ImageStyle();
			private ImageStyle _ImageStyle = null;
			public ImageStyle ImageStyle
			{
				get { return this._ImageStyle; }
				set
				{
					this._ImageStyle = value;
					//if( this._ImageStyle == null ) { this._ImageStyle = new ImageStyle(); }
					return;
				}
			}


			//-------------------------------------------------
			//private TextStyle _TextStyle = new TextStyle();
			private TextStyle _TextStyle = null;
			public TextStyle TextStyle
			{
				get { return this._TextStyle; }
				set
				{
					this._TextStyle = value;
					//if( this._TextStyle == null ) { this._TextStyle = new TextStyle(); }
					return;
				}
			}


			//--------------------------------------------------------------------
			public ItemStyle Clone()
			{
				ItemStyle value = new ItemStyle();
				if( this._BorderStyle != null ) { value.BorderStyle = this._BorderStyle.Clone(); }
				if( this._ImageStyle != null ) { value.ImageStyle = this._ImageStyle.Clone(); }
				if( this._TextStyle != null ) { value.TextStyle = this._TextStyle.Clone(); }
				return value;
			}


			//--------------------------------------------------------------------
			public void MergeWith( ItemStyle Style )
			{
				if( Style == null ) { return; }
				if( Style.BorderStyle != null ) { this.BorderStyle = Style.BorderStyle.Clone(); }
				if( Style.ImageStyle != null ) { this.ImageStyle = Style.ImageStyle.Clone(); }
				if( Style.TextStyle != null ) { this.TextStyle = Style.TextStyle.Clone(); }
				return;
			}


			//-------------------------------------------------
			public ItemLayout InsideOutLayout( Size ImageSize, Size TextSize )
			{
				ItemLayout layout = new ItemLayout();

				// Get the text rectangle.
				layout.Text = new Rectangle( 0, 0, TextSize.Width, TextSize.Height );

				// Get the image rectangle.
				if( this._ImageStyle == null )
				{
					layout.Image = new Rectangle( layout.Content.Location, new Size( 0, 0 ) );
					//layout.Image = new Rectangle( layout.Content.Location, ImageSize );
				}
				else
				{ layout.Image = ArrangeContentRectangle( layout.Text, ImageSize, this._ImageStyle.Alignment ); }

				// Get the content rectangle.
				layout.Content = Rectangle.Union( layout.Text, layout.Image );

				// Get the bounding rectangle.
				if( this._BorderStyle == null )
				{ layout.Bounds = layout.Content; }
				else
				{ layout.Bounds = this._BorderStyle.GetOuterRectangle( layout.Content ); }

				// Offset the rectangles to make the layout zero based.
				layout.Offset( (0 - layout.Bounds.Left), (0 - layout.Bounds.Top) );

				return layout;
			}


			//-------------------------------------------------
			public ItemLayout OutsideInLayout( Graphics Graphics, Size ImageSize, Size MaxSize )
			{
				ItemLayout layout = new ItemLayout();

				// Get the bounding rectangle.
				layout.Bounds = new Rectangle( 0, 0, MaxSize.Width, MaxSize.Height );

				// Get the content rectangle.
				if( this._BorderStyle == null )
				{ layout.Content = layout.Bounds; }
				else
				{ layout.Content = this._BorderStyle.GetInnerRectangle( layout.Bounds ); }

				// Get the image rectangle.
				if( this._ImageStyle == null )
				{ layout.Image = new Rectangle( layout.Content.Location, new Size( 0, 0 ) ); }
				else
				{ layout.Image = LocateContentRectangle( layout.Bounds, ImageSize, this._ImageStyle.Alignment ); }

				// Get the text rectangle.
				List<Rectangle> unoccupied_rectangles = GetUnoccupiedRectangles( layout.Content, layout.Image );
				layout.Text = GetLargestRectangle( unoccupied_rectangles );

				return layout;
			}


			////-------------------------------------------------
			//public ItemLayout WidthConstrainedLayout( Graphics Graphics, Size ImageSize, string Text, int MaxWidth, bool Minimize )
			//{
			//    // Get the layout for the maximum size.
			//    Size max_size = new Size( MaxWidth, int.MaxValue );
			//    ItemLayout layout = this.OutsideInLayout( Graphics, ImageSize, max_size, Minimize );

			//    // Recalculate the text size given the width constraint.
			//    Size text_size = new Size( 0, 0 );
			//    if( string.IsNullOrEmpty( Text ) == false )
			//    {
			//        text_size = this._TextStyle.Measure( Graphics, Text, layout.Text );
			//    }

			//    // Return the final size.
			//    return this.InsideOutLayout( ImageSize, text_size );
			//}


			////-------------------------------------------------
			//public ItemLayout SmallestLayout( Graphics Graphics, Image Image, string Text )
			//{
			//    // Get the text rectangle.
			//    Size text_size = new Size( 0, 0 );
			//    if( string.IsNullOrEmpty( Text ) == false )
			//    {
			//        text_size = this._TextStyle.Measure( Graphics, Text, Imaging.LargestRectangle );
			//    }

			//    // Get the image rectangle.
			//    Size image_size = new Size( 0, 0 );
			//    if( Image != null ) { image_size = Image.Size; }

			//    return this.InsideOutLayout( image_size, text_size );
			//}


			//--------------------------------------------------------------------
			public Size MeasureText( Graphics Graphics, string Text, Size MaxSize )
			{
				Size size = new Size( 0, 0 );
				if( string.IsNullOrEmpty( Text ) != true )
				{
					if( this._TextStyle == null ) { this._TextStyle = new TextStyle(); }
					size = this._TextStyle.Measure( Graphics, Text, MaxSize );
				}
				return size;
			}


			//--------------------------------------------------------------------
			public ItemLayout LayoutItem( Graphics Graphics, Image Image, string Text, Size MaxSize, bool SmallestLayout )
			{
				if( MaxSize.Width <= 0 ) { throw new Exception( "Width is a required parameter but was not supplied." ); }
				ItemLayout actual_layout = null;

				// Get the image size.
				Size image_size = new Size( 0, 0 );
				if( Image != null ) { image_size = Image.Size; }

				if( MaxSize.Height <= 0 )
				{
					// Calculate the layout based upon the given width and maximum height.
					ItemLayout max_layout = this.OutsideInLayout( Graphics, image_size, new Size( MaxSize.Width, int.MaxValue ) );

					// Get the actual text size.
					Size text_size = this.MeasureText( Graphics, Text, max_layout.Text.Size );

					// Readjust layout to fit specifications.
					if( SmallestLayout )
					{
						// Minimize width and height.
						actual_layout = this.InsideOutLayout( image_size, text_size );
					}
					else
					{
						// Minimize height.
						text_size.Width = max_layout.Text.Width;
						actual_layout = this.InsideOutLayout( image_size, text_size );
					}
				}
				else
				{
					// Calculate the layout based upon the maximum size.
					ItemLayout max_layout = this.OutsideInLayout( Graphics, image_size, MaxSize );

					// Readjust layout to fit specifications.
					if( SmallestLayout )
					{
						// Get the actual text size.
						Size text_size = this.MeasureText( Graphics, Text, max_layout.Text.Size );

						// Minimize width and height.
						actual_layout = this.InsideOutLayout( image_size, text_size );
					}
					else
					{
						// No reduction, keep layout at maximum size.
						actual_layout = max_layout;
					}
				}

				return actual_layout;
			}


			//--------------------------------------------------------------------
			public ItemLayout DrawItem( Graphics Graphics, Image Image, string Text, ItemLayout Layout )
			{
				ItemLayout layout = Layout.Clone();

				// Draw the border.
				if( this._BorderStyle != null )
				{
					if( this._TextStyle == null ) { this._TextStyle = new TextStyle(); }
					layout.Content = this._BorderStyle.Draw( Graphics, Layout.Bounds, this._TextStyle.BackColor );
				}

				//// Erase the content background.
				//this.TextStyle.EraseBackground( Graphics, Layout.Content );

				// Draw the image.
				if( (this._ImageStyle != null) && (Image != null) )
				{
					layout.Image = this._ImageStyle.Draw( Graphics, Layout.Image, Image ); 
				}

				// Draw the text.
				if( string.IsNullOrEmpty( Text ) != true )
				{
					if( this._TextStyle == null ) { this._TextStyle = new TextStyle(); }
					layout.Text = this._TextStyle.Draw( Graphics, Text, Layout.Text, false );
				}

				return layout;
			}



		}


	}
}
