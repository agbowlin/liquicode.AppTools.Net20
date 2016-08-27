

using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace liquicode.AppTools
{
	public static partial class Imaging
	{


		public class BorderStyle
		{


			//-----------------------------------------------------
			public ImagingOptions ImagingOptions = new ImagingOptions();

			public int OutsideEdgeSize = 0;
			public int InsideEdgeSize = 0;
			public int BorderSize = 0;
			public int BevelOuter = 0;

			public int BevelInner = 0;
			public Color OutsideEdgeColor = Color.Black;
			public Color InsideEdgeColor = Color.Black;
			public Color BorderColor = Color.DimGray;
			public Color LightColor = Color.LightGray;
			public Color DarkColor = Color.DarkGray;

			public float ArcRadius = 0.0F;


			//-----------------------------------------------------
			public BorderStyle Clone()
			{
				BorderStyle value = new BorderStyle();
				value.ImagingOptions = this.ImagingOptions.Clone();
				value.OutsideEdgeSize = this.OutsideEdgeSize;
				value.InsideEdgeSize = this.InsideEdgeSize;
				value.BorderSize = this.BorderSize;
				value.BevelOuter = this.BevelOuter;
				value.BevelInner = this.BevelInner;
				value.OutsideEdgeColor = this.OutsideEdgeColor;
				value.InsideEdgeColor = this.InsideEdgeColor;
				value.BorderColor = this.BorderColor;
				value.LightColor = this.LightColor;
				value.DarkColor = this.DarkColor;
				value.ArcRadius = this.ArcRadius;
				return value;
			}


			//-----------------------------------------------------
			public int EdgeThickness
			{
				get
				{
					return (this.BorderSize
							+ Math.Abs( this.BevelOuter )
							+ Math.Abs( this.BevelInner )
							+ this.OutsideEdgeSize
							+ this.InsideEdgeSize
							+ Convert.ToInt32( this.ArcRadius ));
				}
			}


			//-----------------------------------------------------
			public class BorderRegions
			{
				//         Outside
				//   +----------------+
				//   |\    Border    /|
				//   | \   Inner    / |
				//   |  +----------+  |
				//   |  |  Inside  |  |
				//   |  |  Middle  |  |
				//   |  +----------+  |
				//   | /            \ |
				//   |/              \|
				//   +----------------+
				//   

				public Rectangle OutsideRectangle = NullRectangle;
				public Region OutsideRegion = NullRegion;

				public Rectangle OuterRectangle = NullRectangle;
				public Region OuterRegion = NullRegion;

				public Rectangle BorderRectangle = NullRectangle;
				public Region BorderRegion = NullRegion;

				public Rectangle InnerRectangle = NullRectangle;
				public Region InnerRegion = NullRegion;

				public Rectangle InsideRectangle = NullRectangle;
				public Region InsideRegion = NullRegion;

				public Rectangle MiddleRectangle = NullRectangle;
				public Region MiddleRegion = NullRegion;
			}


			//-----------------------------------------------------
			public BorderRegions GetBorderRegions( Rectangle Bounds )
			{
				BorderRegions regions = new BorderRegions();
				int iOutsideEdgeSize = this.OutsideEdgeSize;
				int iInsideEdgeSize = this.InsideEdgeSize;
				int iOuterSize = Math.Abs( this.BevelOuter );
				int iBorderSize = this.BorderSize;
				int iInnerSize = Math.Abs( this.BevelInner );

				regions.OutsideRectangle = Bounds;
				regions.OutsideRegion = GetRoundedRectangleRegion( regions.OutsideRectangle, this.ArcRadius );

				regions.OuterRectangle = regions.OutsideRectangle;
				regions.OuterRegion = regions.OutsideRegion.Clone();
				if( iOutsideEdgeSize > 0 )
				{
					regions.OuterRectangle.Inflate( -iOutsideEdgeSize, -iOutsideEdgeSize );
					regions.OuterRegion = GetRoundedRectangleRegion( regions.OuterRectangle, this.ArcRadius );
				}
				regions.OutsideRegion.Exclude( regions.OuterRegion.Clone() );

				regions.BorderRectangle = regions.OuterRectangle;
				regions.BorderRegion = regions.OuterRegion.Clone();
				if( iOuterSize > 0 )
				{
					regions.BorderRectangle.Inflate( -iOuterSize, -iOuterSize );
					regions.BorderRegion = GetRoundedRectangleRegion( regions.BorderRectangle, this.ArcRadius );
				}
				regions.OuterRegion.Exclude( regions.BorderRegion.Clone() );

				regions.InnerRectangle = regions.BorderRectangle;
				regions.InnerRegion = regions.BorderRegion.Clone();
				if( iBorderSize > 0 )
				{
					regions.InnerRectangle.Inflate( -iBorderSize, -iBorderSize );
					regions.InnerRegion = GetRoundedRectangleRegion( regions.InnerRectangle, this.ArcRadius );
				}
				regions.BorderRegion.Exclude( regions.InnerRegion.Clone() );

				regions.InsideRectangle = regions.InnerRectangle;
				regions.InsideRegion = regions.InnerRegion.Clone();
				if( iInnerSize > 0 )
				{
					regions.InsideRectangle.Inflate( -iInnerSize, -iInnerSize );
					regions.InsideRegion = GetRoundedRectangleRegion( regions.InsideRectangle, this.ArcRadius );
				}
				regions.InnerRegion.Exclude( regions.InsideRegion.Clone() );

				regions.MiddleRectangle = regions.InsideRectangle;
				regions.MiddleRegion = regions.InsideRegion.Clone();
				if( iInsideEdgeSize > 0 )
				{
					regions.MiddleRectangle.Inflate( -iInsideEdgeSize, -iInsideEdgeSize );
					regions.MiddleRegion = GetRoundedRectangleRegion( regions.MiddleRectangle, this.ArcRadius );
				}
				regions.InsideRegion.Exclude( regions.MiddleRegion.Clone() );

				return regions;
			}


			//-----------------------------------------------------
			public void Draw_Bicolor( Graphics Graphics, Rectangle Bounds, BorderRegions Regions )
			{
				Pen penLight = new Pen( this.LightColor, 1 );
				Pen penDark = new Pen( this.DarkColor, 1 );
				Pen penBorder = new Pen( this.BorderColor, this.BorderSize );
				penBorder.Alignment = PenAlignment.Inset;
				penLight.Alignment = PenAlignment.Center;
				penDark.Alignment = PenAlignment.Center;

				// Draw border.
				if( this.BorderSize > 0 )
				{
					Rectangle rect = Bounds;
					if( this.BorderSize == 1 )
					{
						rect.Width -= 1;
						rect.Height -= 1;
					}
					Graphics.DrawRectangle( penBorder, rect );
				}

				// Draw outer bevel.
				if( this.BevelOuter != 0 )
				{
					Rectangle rect = Bounds;
					rect.Width -= 1;
					rect.Height -= 1;
					int iLineWidth = Convert.ToInt32( ((this.BevelOuter > 0) ? this.BevelOuter : -this.BevelOuter) );
					for( int i = 1; i <= iLineWidth; i++ )
					{
						Graphics.DrawLine( penLight, rect.Left, rect.Top, rect.Right, rect.Top );
						Graphics.DrawLine( penLight, rect.Left, rect.Top, rect.Left, rect.Bottom );
						Graphics.DrawLine( penDark, rect.Right, rect.Top + 1, rect.Right, rect.Bottom );
						Graphics.DrawLine( penDark, rect.Left + 1, rect.Bottom, rect.Right, rect.Bottom );
						rect.Inflate( -1, -1 );
					}
				}

				// Draw inner bevel.
				if( this.BevelInner != 0 )
				{
					Rectangle rect = Bounds;
					rect.Inflate( -(this.BorderSize - 1), -(this.BorderSize - 1) );
					rect.Width -= 1;
					rect.Height -= 1;
					int iLineWidth = Convert.ToInt32( ((this.BevelInner > 0) ? this.BevelInner : -this.BevelInner) );
					for( int i = 1; i <= iLineWidth; i++ )
					{
						Graphics.DrawLine( penLight, rect.Right, rect.Top, rect.Right, rect.Bottom );
						Graphics.DrawLine( penLight, rect.Left, rect.Bottom, rect.Right, rect.Bottom );
						Graphics.DrawLine( penDark, rect.Left, rect.Top, rect.Right - 1, rect.Top );
						Graphics.DrawLine( penDark, rect.Left, rect.Top, rect.Left, rect.Bottom - 1 );
						rect.Inflate( 1, 1 );
					}
				}

				return;
			}


			//-----------------------------------------------------
			public void Draw_RoundedGradient( Graphics Graphics, BorderRegions Regions )
			{
				// Draw outside edge.
				if( this.OutsideEdgeSize > 0 )
				{
					SolidBrush brush = new SolidBrush( this.OutsideEdgeColor );
					Graphics.FillRegion( brush, Regions.OutsideRegion );
				}

				// Draw outer bevel.
				if( this.BevelOuter > 0 )
				{
					LinearGradientBrush brush = new LinearGradientBrush(
														Regions.OuterRectangle.Location
														, new Point(
																Regions.OuterRectangle.Right
																, Regions.OuterRectangle.Bottom )
														, this.LightColor
														, this.DarkColor );
					Graphics.FillRegion( brush, Regions.OuterRegion );
				}
				else if( this.BevelOuter < 0 )
				{
					LinearGradientBrush brush = new LinearGradientBrush(
														Regions.OuterRectangle.Location
														, new Point(
																Regions.OuterRectangle.Right
																, Regions.OuterRectangle.Bottom )
														, this.DarkColor
														, this.LightColor );
					Graphics.FillRegion( brush, Regions.OuterRegion );
				}
				else
				{
				}

				// Draw border.
				if( this.BorderSize > 0 )
				{
					SolidBrush brush = new SolidBrush( this.BorderColor );
					Graphics.FillRegion( brush, Regions.BorderRegion );
				}

				// Draw inner bevel.
				if( this.BevelInner > 0 )
				{
					LinearGradientBrush brush = new LinearGradientBrush(
														Regions.InnerRectangle.Location
														, new Point(
																Regions.InnerRectangle.Right
																, Regions.InnerRectangle.Bottom )
														, this.LightColor
														, this.DarkColor );
					Graphics.FillRegion( brush, Regions.InnerRegion );
				}
				else if( this.BevelInner < 0 )
				{
					LinearGradientBrush brush = new LinearGradientBrush(
														Regions.InnerRectangle.Location
														, new Point(
																Regions.InnerRectangle.Right
																, Regions.InnerRectangle.Bottom )
														, this.DarkColor
														, this.LightColor );
					Graphics.FillRegion( brush, Regions.InnerRegion );
				}
				else
				{
				}

				// Draw inside edge.
				if( this.InsideEdgeSize > 0 )
				{
					SolidBrush brush = new SolidBrush( this.InsideEdgeColor );
					Graphics.FillRegion( brush, Regions.InsideRegion );
				}

				return;
			}


			//-----------------------------------------------------
			public void Draw_RoundedBicolor( Graphics Graphics, Rectangle Bounds, BorderRegions Regions )
			{
				//   0                1
				//   ------------------
				//   |               /|
				//   |  3           / |
				//   |  ------------  |
				//   | /           2  |
				//   |/               |
				//   ------------------
				//   4                0
				GraphicsPath gpath = new GraphicsPath();
				Point ptTopLeft = new Point( Bounds.Left, Bounds.Top );
				Point ptTopRight = new Point( Bounds.Right, Bounds.Top );
				Point ptBottomRight = new Point( Bounds.Right, Bounds.Bottom );
				Point ptBottomLeft = new Point( Bounds.Left, Bounds.Bottom );
				int iDiagonal = 0;
				if( Bounds.Height > Bounds.Width )
				{
					iDiagonal = Convert.ToInt32( Bounds.Width / 2 );
				}
				else
				{
					iDiagonal = Convert.ToInt32( Bounds.Height / 2 );
				}
				Point ptTopRightMiddle = new Point( Bounds.Right - iDiagonal, Bounds.Top + iDiagonal );
				Point ptBottomLeftMiddle = new Point( Bounds.Left + iDiagonal, Bounds.Bottom - iDiagonal );

				Region rgnTopLeft = new Region( Bounds );
				gpath.Reset();
				gpath.AddLine( ptTopLeft, ptTopRight );
				gpath.AddLine( ptTopRight, ptTopRightMiddle );
				gpath.AddLine( ptTopRightMiddle, ptBottomLeftMiddle );
				gpath.AddLine( ptBottomLeftMiddle, ptBottomLeft );
				gpath.AddLine( ptBottomLeft, ptTopLeft );
				gpath.CloseFigure();
				rgnTopLeft.Exclude( gpath );

				Region rgnBottomRight = new Region( Bounds );
				gpath.Reset();
				gpath.AddLine( ptBottomRight, ptTopRight );
				gpath.AddLine( ptTopRight, ptTopRightMiddle );
				gpath.AddLine( ptTopRightMiddle, ptBottomLeftMiddle );
				gpath.AddLine( ptBottomLeftMiddle, ptBottomLeft );
				gpath.AddLine( ptBottomLeft, ptBottomRight );
				gpath.CloseFigure();
				rgnBottomRight.Exclude( gpath );

				// Draw outside edge.
				if( this.OutsideEdgeSize > 0 )
				{
					SolidBrush brush = new SolidBrush( this.OutsideEdgeColor );
					Graphics.FillRegion( brush, Regions.OutsideRegion );
				}

				Color clr1 = default( Color );
				Color clr2 = default( Color );

				// Draw outer bevel.
				if( this.BevelOuter != 0 )
				{
					if( this.BevelOuter > 0 )
					{
						clr1 = this.DarkColor;
						clr2 = this.LightColor;
					}
					else if( this.BevelOuter < 0 )
					{
						clr1 = this.LightColor;
						clr2 = this.DarkColor;
					}
					SolidBrush brush = null;
					Region rgn = null;
					brush = new SolidBrush( clr1 );
					rgn = Regions.OuterRegion.Clone();
					rgn.Exclude( rgnBottomRight );
					Graphics.FillRegion( brush, rgn );
					brush = new SolidBrush( clr2 );
					rgn = Regions.OuterRegion.Clone();
					rgn.Exclude( rgnTopLeft );
					Graphics.FillRegion( brush, rgn );
				}

				// Draw border.
				if( this.BorderSize > 0 )
				{
					SolidBrush brush = new SolidBrush( this.BorderColor );
					Graphics.FillRegion( brush, Regions.BorderRegion );
				}

				// Draw inner bevel.
				if( this.BevelInner != 0 )
				{
					if( this.BevelInner > 0 )
					{
						clr1 = this.DarkColor;
						clr2 = this.LightColor;
					}
					else if( this.BevelInner < 0 )
					{
						clr1 = this.LightColor;
						clr2 = this.DarkColor;
					}
					SolidBrush brush = null;
					Region rgn = null;
					brush = new SolidBrush( clr1 );
					rgn = Regions.InnerRegion.Clone();
					rgn.Exclude( rgnBottomRight );
					Graphics.FillRegion( brush, rgn );
					brush = new SolidBrush( clr2 );
					rgn = Regions.InnerRegion.Clone();
					rgn.Exclude( rgnTopLeft );
					Graphics.FillRegion( brush, rgn );
				}

				// Draw inside edge.
				if( this.InsideEdgeSize > 0 )
				{
					SolidBrush brush = new SolidBrush( this.InsideEdgeColor );
					Graphics.FillRegion( brush, Regions.InsideRegion );
				}

				return;
			}


			//-----------------------------------------------------
			public Rectangle Draw( Graphics Graphics, Rectangle Bounds, Color ClientBackColor )
			{
				if( this.ImagingOptions != null ) { this.ImagingOptions.CopyTo( Graphics ); }
				BorderRegions regions = this.GetBorderRegions( Bounds );

				// Draw the border.
				//this.Draw_Bicolor( Graphics, Bounds, regions );
				//this.Draw_RoundedGradient( Graphics, regions );
				this.Draw_RoundedBicolor( Graphics, Bounds, regions );

				// Erase client area.
				if( ClientBackColor != Color.Empty )
				{
					Graphics.FillRegion( new SolidBrush( ClientBackColor ), regions.MiddleRegion );
				}

				RectangleF rect_middleF = regions.MiddleRegion.GetBounds( Graphics );
				Rectangle rect_middle = new Rectangle(
												(int)rect_middleF.X, (int)rect_middleF.Y
												, (int)rect_middleF.Width, (int)rect_middleF.Height );
				return rect_middle;
			}


			//-----------------------------------------------------
			public void Draw( Graphics Graphics, Rectangle Bounds )
			{
				this.Draw( Graphics, Bounds, Color.Empty );
				return;
			}


			//-----------------------------------------------------
			public Rectangle GetInnerRectangle( Rectangle OuterRectangle )
			{
				Rectangle rectangle = new Rectangle( OuterRectangle.Location, OuterRectangle.Size );
				int n = this.EdgeThickness;
				rectangle.Inflate( -n, -n );
				return rectangle;
			}


			//-----------------------------------------------------
			public Rectangle GetOuterRectangle( Rectangle InnerRectangle )
			{
				Rectangle rectangle = new Rectangle( InnerRectangle.Location, InnerRectangle.Size );
				int n = this.EdgeThickness;
				rectangle.Inflate( n, n );
				return rectangle;
			}


		}


	}
}
