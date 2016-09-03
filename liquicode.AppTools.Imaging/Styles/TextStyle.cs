

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
			public StringFormat StringFormat = new StringFormat(); // provides additional formatting and layout options.


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
				Font font = (this.Font != null ? this.Font : TextStyle.DefaultFont);
				int chars = 0;
				int lines = 0;
				SizeF sizef = Graphics.MeasureString( Text, font, MaxSize, this.StringFormat, out chars, out lines );
				return new Size( (int)Math.Ceiling( sizef.Width ), (int)Math.Ceiling( sizef.Height ) );
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
				if( EraseBackground ) { Graphics.FillRectangle( new SolidBrush( backcolor ), Rectangle ); }
				Graphics.DrawString( Text, font, new SolidBrush( forecolor ), Rectangle, this.StringFormat );

				// Return
				return Rectangle;
			}


		}


	}
}
