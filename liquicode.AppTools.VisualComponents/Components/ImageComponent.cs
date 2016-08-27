

using System;
using System.Drawing;


namespace liquicode.AppTools
{
	public class ImageComponent
		: IVisualComponent
	{

		//=====================================================================
		//		Public Properties
		//=====================================================================


		//---------------------------------------------------------------------
		public Image Image = null;
		public Color BackColor = Color.Empty;
		public int Opacity = 255;


		//=====================================================================
		//		VisualComponent implementation
		//=====================================================================


		//---------------------------------------------------------------------
		void IVisualComponent.SetContent( object Content )
		{
			if( Content == null )
			{
				this.Image = null;
			}
			else
			{
				if( Content == typeof( Image ) )
				{
					this.Image = (Image)Content;
				}
			}
			return;
		}


		//---------------------------------------------------------------------
		Size IVisualComponent.GetPreferredSize( int? Width, int? Height )
		{
			if( this.Image == null ) { return new Size( 0, 0 ); }
			return this.Image.Size;
		}


		//---------------------------------------------------------------------
		void IVisualComponent.Draw( Graphics Graphics, Rectangle Rectangle )
		{
			if( this.Image == null ) { return; }
			Graphics.DrawImage( Image, Rectangle );
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
