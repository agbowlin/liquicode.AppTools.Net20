

using System;
using System.Drawing;
using System.Drawing.Imaging;


namespace liquicode.AppTools
{
	public static partial class Imaging
	{


		public class ItemLayout
		{


			//-------------------------------------------------
			public Rectangle Bounds;
			public Rectangle Content;
			public Rectangle Image;
			public Rectangle Text;


			//-------------------------------------------------
			public void Offset( int X, int Y )
			{
				this.Bounds.Offset( X, Y );
				this.Content.Offset( X, Y );
				this.Image.Offset( X, Y );
				this.Text.Offset( X, Y );
				return;
			}


			//-------------------------------------------------
			public ItemLayout Clone()
			{
				ItemLayout layout = new ItemLayout();
				layout.Bounds = new Rectangle( this.Bounds.Location, this.Bounds.Size );
				layout.Content = new Rectangle( this.Content.Location, this.Content.Size );
				layout.Image = new Rectangle( this.Image.Location, this.Image.Size );
				layout.Text = new Rectangle( this.Text.Location, this.Text.Size );
				return layout;
			}


		}


	}
}
