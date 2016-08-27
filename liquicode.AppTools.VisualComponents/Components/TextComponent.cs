

using System;
using System.Drawing;
using System.Drawing.Imaging;


namespace liquicode.AppTools
{
	public class TextComponent
		: IVisualComponent
	{


		//---------------------------------------------------------------------
		private static Font _DefaultFont = new Font( FontFamily.GenericSansSerif, 8, FontStyle.Regular, GraphicsUnit.Point );
		public static Font DefaultFont
		{
			get { return TextComponent._DefaultFont; }
		}


		//=====================================================================
		//		Public Properties
		//=====================================================================


		//---------------------------------------------------------------------
		public string Text = "";
		public Font Font = TextComponent.DefaultFont;
		public Color BackColor = Color.White;
		public Color ForeColor = Color.Black;
		public StringFormat StringFormat = new StringFormat(); // ???


		//=====================================================================
		//		Constructors and Destructors
		//=====================================================================


		//---------------------------------------------------------------------
		public TextComponent()
		{ return; }


		//---------------------------------------------------------------------
		public TextComponent( string ThisText )
		{
			this.Text = ThisText;
			return;
		}


		//---------------------------------------------------------------------
		public TextComponent( string ThisText, Font ThisFont, Color ThisBackColor, Color ThisForeColor )
		{
			this.Text = ThisText;
			this.Font = ThisFont;
			this.BackColor = ThisBackColor;
			this.ForeColor = ThisForeColor;
			return;
		}


		//=====================================================================
		//		IVisualComponent implementation
		//=====================================================================


		//---------------------------------------------------------------------
		void IVisualComponent.SetContent( object Content )
		{
			if( Content == null )
			{
				this.Text = "";
			}
			else
			{
				this.Text = Content.ToString();
			}
			return;
		}


		//---------------------------------------------------------------------
		Size IVisualComponent.GetPreferredSize( int? Width, int? Height )
		{
			Size max_size = new Size();
			max_size.Width = (Width.HasValue ? Width.Value : int.MaxValue);
			max_size.Height = (Height.HasValue ? Height.Value : int.MaxValue);

			Size measured_size;
			Image image = new Bitmap( 1, 1, PixelFormat.Format32bppArgb );
			using( Graphics image_graphics = Graphics.FromImage( image ) )
			{
				Font font = (this.Font != null ? this.Font : TextComponent.DefaultFont);
				int chars = 0;
				int lines = 0;
				SizeF sizef = image_graphics.MeasureString( Text, font, max_size, this.StringFormat, out chars, out lines );
				measured_size = new Size( (int)Math.Ceiling( sizef.Width ), (int)Math.Ceiling( sizef.Height ) );
			}

			return measured_size;
		}


		//---------------------------------------------------------------------
		void IVisualComponent.Draw( Graphics Graphics, Rectangle Rectangle )
		{
			if( string.IsNullOrEmpty( this.Text ) ) { return; }
			if( this.BackColor != Color.Empty )
			{
				using( Brush brush = new SolidBrush( this.BackColor ) )
				{
					Rectangle.Width++;
					Rectangle.Height++;
					Graphics.FillRectangle( brush, Rectangle );
					Rectangle.Width--;
					Rectangle.Height--;
				}
			}
			using( Brush brush = new SolidBrush( this.ForeColor ) )
			{
				Graphics.DrawString( this.Text, this.Font, brush, Rectangle, this.StringFormat );
			}
			return;
		}


		//---------------------------------------------------------------------
		IVisualComponent IVisualComponent.HitTest( Rectangle Rectangle, Point Point )
		{
			if( Rectangle.Contains( Point ) ) { return this; }
			return null;
		}


	}
}
