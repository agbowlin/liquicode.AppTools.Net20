

using System;
using System.Drawing;


namespace liquicode.AppTools
{
	public class DropShadowDecorator
		: ComponentDecorator
	{


		//=====================================================================
		//		Public Properties
		//=====================================================================


		//---------------------------------------------------------------------
		public int ShadowDepth = 5;
		public Color ShadowColor = Color.Black;
		public int ShadowOpacity = 255;


		//=====================================================================
		//		Constructors and Destructors
		//=====================================================================


		//---------------------------------------------------------------------
		public DropShadowDecorator( IVisualComponent ThisComponent )
			: base( ThisComponent )
		{
			return;
		}


		//---------------------------------------------------------------------
		public DropShadowDecorator( IVisualComponent ThisComponent, int ThisShadowDepth, Color ThisShadowColor, int ThisShadowOpacity )
			: base( ThisComponent )
		{
			this.ShadowDepth = ThisShadowDepth;
			this.ShadowColor = ThisShadowColor;
			this.ShadowOpacity = ThisShadowOpacity;
			return;
		}


		//=====================================================================
		//		ComponentDecorator implementation
		//=====================================================================


		//---------------------------------------------------------------------
		protected override Size Decorator_GetPreferredSize( int? Width, int? Height )
		{
			if( Width.HasValue ) { Width -= this.ShadowDepth; }
			if( Height.HasValue ) { Height -= this.ShadowDepth; }
			Size size = new Size();
			if( this._VisualComponent != null )
			{
				size = this._VisualComponent.GetPreferredSize( Width, Height );
			}
			size.Width += this.ShadowDepth;
			size.Height += this.ShadowDepth;
			return size;
		}


		//---------------------------------------------------------------------
		protected override void Decorator_Draw( Graphics Graphics, Rectangle Rectangle )
		{
			Rectangle.Width -= this.ShadowDepth;
			Rectangle.Height -= this.ShadowDepth;
			using( Brush brush = new SolidBrush( Color.FromArgb( this.ShadowOpacity, this.ShadowColor ) ) )
			{
				Rectangle.Width++;
				Rectangle.Height++;
				Rectangle.Offset( this.ShadowDepth, this.ShadowDepth );
				Graphics.FillRectangle( brush, Rectangle );
				Rectangle.Offset( 0 - this.ShadowDepth, 0 - this.ShadowDepth );
				Rectangle.Width--;
				Rectangle.Height--;
			}
			if( this._VisualComponent != null )
			{
				this._VisualComponent.Draw( Graphics, Rectangle );
			}
			return;
		}


		//---------------------------------------------------------------------
		protected override IVisualComponent Decorator_HitTest( Rectangle Rectangle, Point Point )
		{
			if( Rectangle.Contains( Point ) )
			{
				Rectangle inner_rect = new Rectangle( Rectangle.Location, Rectangle.Size );
				inner_rect.Width -= this.ShadowDepth;
				inner_rect.Height -= this.ShadowDepth;
				if( inner_rect.Contains( Point ) )
				{
					return this._VisualComponent.HitTest( Rectangle, Point );
				}
				return this;
			}
			return null;
		}


	}
}
