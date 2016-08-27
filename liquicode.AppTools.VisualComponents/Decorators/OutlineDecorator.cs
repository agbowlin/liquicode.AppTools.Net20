

using System;
using System.Drawing;


namespace liquicode.AppTools
{
	public class OutlineDecorator
		: ComponentDecorator
	{


		//=====================================================================
		//		Public Properties
		//=====================================================================


		//---------------------------------------------------------------------
		public int LineWidth = 1;
		public Color LineColor = Color.Black;


		//=====================================================================
		//		Constructors and Destructors
		//=====================================================================


		//---------------------------------------------------------------------
		public OutlineDecorator( IVisualComponent ThisComponent )
			: base( ThisComponent )
		{ return; }


		//---------------------------------------------------------------------
		public OutlineDecorator( IVisualComponent ThisComponent, int ThisLineWidth, Color ThisLineColor )
			: base( ThisComponent )
		{
			this.LineWidth = ThisLineWidth;
			this.LineColor = ThisLineColor;
			return;
		}


		//=====================================================================
		//		ComponentDecorator implementation
		//=====================================================================


		//---------------------------------------------------------------------
		protected override Size Decorator_GetPreferredSize( int? Width, int? Height )
		{
			if( Width.HasValue ) { Width -= (2 * this.LineWidth); }
			if( Height.HasValue ) { Height -= (2 * this.LineWidth); }
			Size size = new Size();
			if( this._VisualComponent != null )
			{
				size = this._VisualComponent.GetPreferredSize( Width, Height );
			}
			size.Width += (2 * this.LineWidth);
			size.Height += (2 * this.LineWidth);
			return size;
		}


		//---------------------------------------------------------------------
		protected override void Decorator_Draw( Graphics Graphics, Rectangle Rectangle )
		{
			using( Pen pen = new Pen( this.LineColor ) )
			{
				int n = 0;
				while( n < this.LineWidth )
				{
					Graphics.DrawRectangle( pen, Rectangle );
					Rectangle.Inflate( -1, -1 );
					n++;
				}
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
				inner_rect.Inflate( 0 - this.LineWidth, 0 - this.LineWidth );
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
