

using System;
using System.Drawing;


namespace liquicode.AppTools
{
	public static partial class Imaging
	{


		public class TextStyle
		{


			//-----------------------------------------------------
			private static Font _DefaultFont = new Font( FontFamily.GenericSansSerif, 8, FontStyle.Regular, GraphicsUnit.Point );
			public static Font DefaultFont
			{
				get { return TextStyle._DefaultFont; }
			}


			//-----------------------------------------------------
			public ImagingOptions ImagingOptions = new ImagingOptions();
			public Font Font = TextStyle.DefaultFont;
			public Color BackColor = Color.White;
			public Color ForeColor = Color.Black;
			public StringFormat StringFormat = new StringFormat(); // ???


			//-----------------------------------------------------
			public TextStyle Clone()
			{
				TextStyle value = new TextStyle();
				value.ImagingOptions = this.ImagingOptions.Clone();
				value.Font = (Font)this.Font.Clone();
				value.BackColor = this.BackColor;
				value.ForeColor = this.ForeColor;
				value.StringFormat = (StringFormat)this.StringFormat.Clone();
				return value;
			}


			//-----------------------------------------------------
			public void EraseBackground( Graphics Graphics, Rectangle Rectangle )
			{
				Graphics.FillRectangle( new SolidBrush( this.BackColor ), Rectangle );
				return;
			}


			//-----------------------------------------------------
			public Size Measure( Graphics Graphics, string Text, Size MaxSize )
			{
				if( Text.StartsWith( "<HTML>", StringComparison.InvariantCultureIgnoreCase ) )
				{
#if SYSTEM_DATA_HTML
					System.Drawing.Html.InitialContainer html = new System.Drawing.Html.InitialContainer( Text );
					html.SetBounds( new Rectangle( 0, 0, MaxSize.Width, MaxSize.Height ) );
					html.MeasureBounds( Graphics );
					return Size.Round( html.Size );
#elif NABU_HTMLAYOUT
					Nabu.Forms.Html.HtmlWindowless renderer = new Nabu.Forms.Html.HtmlWindowless();
					renderer.BaseUrl = new System.Uri( "http://liquicode.com" );
					//renderer.Root.Attributes[ "padding" ] = "0";
					//renderer.Root.Attributes[ "border" ] = "0";
					//renderer.Root.Attributes[ "margin" ] = "0";
					renderer.Html = Text;

					Nabu.Range<int> intrinsic_widths = renderer.Root.GetIntrinsicWidth();
					int width = intrinsic_widths.End;
					if( MaxSize.Width > 0 )
					{
						if( width > MaxSize.Width )
						{ width = MaxSize.Width; }
					}
					int height = renderer.Root.GetIntrinsicHeight( width );

					return new Size( width, height );
#else
					throw new NotImplementedException( "HTML rendering not implemented." );
#endif
				}
				else
				{
					Font font = (this.Font != null ? this.Font : TextStyle.DefaultFont);
					int chars = 0;
					int lines = 0;
					SizeF sizef = Graphics.MeasureString( Text, font, MaxSize, this.StringFormat, out chars, out lines );
					return new Size( (int)Math.Ceiling( sizef.Width ), (int)Math.Ceiling( sizef.Height ) );
				}
			}


			//-----------------------------------------------------
			public Rectangle Draw( Graphics Graphics, string Text, Rectangle Rectangle, bool EraseBackground )
			{
				if( (this.ImagingOptions != null) )
				{
					this.ImagingOptions.CopyTo( Graphics );
				}

				// Erase background.

				// Get font and colors.
				Font font = (this.Font != null ? this.Font : TextStyle.DefaultFont);
				Color backcolor = (!this.BackColor.Equals( Color.Empty ) ? this.BackColor : Color.White);
				Color forecolor = (!this.ForeColor.Equals( Color.Empty ) ? this.ForeColor : Color.Black);

				// Draw text.
				if( Text.StartsWith( "<HTML>", StringComparison.InvariantCultureIgnoreCase ) )
				{
#if SYSTEM_DATA_HTML
					if( EraseBackground ) { Graphics.FillRectangle( new SolidBrush( backcolor ), Rectangle ); }
					System.Drawing.Html.InitialContainer html = new System.Drawing.Html.InitialContainer( Text );
					html.SetBounds( new Rectangle( 0, 0, Rectangle.Width, Rectangle.Height ) );
					html.ScrollOffset = Rectangle.Location;
					Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
					html.MeasureBounds( Graphics );
					html.Paint( Graphics );
#elif NABU_HTMLAYOUT
					Nabu.Forms.Html.HtmlWindowless renderer = new Nabu.Forms.Html.HtmlWindowless();
					renderer.BaseUrl = new System.Uri( "http://liquicode.com" );
					renderer.Html = Text;
					renderer.Measure( Rectangle.Width, Rectangle.Height );
					renderer.Update();

					Image image = new Bitmap( Rectangle.Width, Rectangle.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
					using( Graphics image_graphics = System.Drawing.Graphics.FromImage( image ) )
					{
						//if( EraseBackground )
						//{ image_graphics.Clear( backcolor ); }
						//else
						//{ image_graphics.Clear( Color.Transparent ); }
						using( GraphicsHdc hdc = new GraphicsHdc( image_graphics ) )
						{
							renderer.RenderOnDC( hdc.Hdc, new Rectangle( 0, 0, Rectangle.Width, Rectangle.Height ) );
						}
					}
					Graphics.DrawImage( image, Rectangle );
#else
					throw new NotImplementedException( "HTML rendering not implemented." );
#endif
				}
				else
				{
					if( EraseBackground ) { Graphics.FillRectangle( new SolidBrush( backcolor ), Rectangle ); }
					Graphics.DrawString( Text, font, new SolidBrush( forecolor ), Rectangle, this.StringFormat );
				}

				// Return
				return Rectangle;
			}


		}


	}
}
