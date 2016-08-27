

using System;
using System.Drawing;


namespace liquicode.AppTools
{
	public class RectangleComponent
		: IVisualComponent
	{

		//=====================================================================
		//		Public Properties
		//=====================================================================


		//---------------------------------------------------------------------
		public Color Color = Color.White;
		public int Opacity = 255;


		//=====================================================================
		//		Constructors and Destructors
		//=====================================================================


		//---------------------------------------------------------------------
		public RectangleComponent()
		{ return; }


		//---------------------------------------------------------------------
		public RectangleComponent( Color ThisColor )
		{
			this.Color = ThisColor;
			return;
		}


		//---------------------------------------------------------------------
		public RectangleComponent( Color ThisColor, int ThisOpacity )
		{
			this.Color = ThisColor;
			this.Opacity = ThisOpacity;
			return;
		}


		//=====================================================================
		//		VisualComponent implementation
		//=====================================================================


		//---------------------------------------------------------------------
		void IVisualComponent.SetContent( object Content )
		{
			if( Content == null )
			{
				this.Color = Color.White;
			}
			else
			{
				if( Content == typeof( Color ) )
				{
					this.Color = (Color)Content;
				}
			}
			return;
		}


		//---------------------------------------------------------------------
		Size IVisualComponent.GetPreferredSize( int? Width, int? Height )
		{
			return new Size( 0, 0 );
		}


		//---------------------------------------------------------------------
		void IVisualComponent.Draw( Graphics Graphics, Rectangle Rectangle )
		{
			using( Brush brush = new SolidBrush( Color.FromArgb( this.Opacity, this.Color ) ) )
			{
				Rectangle.Width++;
				Rectangle.Height++;
				Graphics.FillRectangle( brush, Rectangle );
				Rectangle.Width--;
				Rectangle.Height--;
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
