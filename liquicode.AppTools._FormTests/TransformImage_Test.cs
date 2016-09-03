

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using liquicode.AppTools;


namespace liquicode.AppTools._FormTests
{
	public partial class TransformImage_Test : Form
	{


		//-----------------------------------------------------
		public TransformImage_Test()
		{
			this.InitializeComponent();

			this.ImageList1.ColorDepth = ColorDepth.Depth32Bit;
			this.ImageList1.ImageSize = new Size( 256, 256 );

			this.ListView1.LargeImageList = this.ImageList1;
			this.ListView1.View = View.Tile;
			this.ListView1.TileSize = this.ImageList1.ImageSize;
			this.ListView1.ShowItemToolTips = true;

			this.mnuOriginal.PerformClick();

			return;
		}


		//-----------------------------------------------------
		private void ResetList()
		{
			this.ListView1.Items.Clear();
			this.ImageList1.Images.Clear();
			return;
		}


		//-----------------------------------------------------
		private void UpdateList()
		{
			foreach( ListViewItem item in this.ListView1.Items )
			{
				item.ToolTipText = item.Text;
			}
			return;
		}


		//-----------------------------------------------------
		private void mnuOriginal_Click( object sender, EventArgs e )
		{
			string image_key = "";
			Image image = Image.FromFile( "../_support/test-image-256x256x32.png" );
			this.ResetList();

			// Add the original image.
			image_key = Guid.NewGuid().ToString();
			this.ImageList1.Images.Add( image_key, image );
			this.ListView1.Items.Add( "Original Image", image_key );

			this.UpdateList();
			return;
		}


		//-----------------------------------------------------
		private void mnuRotate_Click( object sender, EventArgs e )
		{
			string image_key = "";
			Imaging.ImageTransformationSettings settings = null;
			Image image = Image.FromFile( "../_support/test-image-256x256x32.png" );
			this.ResetList();

			// Add the original image.
			image_key = Guid.NewGuid().ToString();
			this.ImageList1.Images.Add( image_key, image );
			this.ListView1.Items.Add( "Original Image", image_key );

			// Rotate 90
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageRotate = 90;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Rotate 90", image_key );

			// Rotate 180
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageRotate = 180;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Rotate 180", image_key );

			// Rotate 270
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageRotate = 270;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Rotate 270", image_key );

			this.UpdateList();
			return;
		}


		//-----------------------------------------------------
		private void mnuFlip_Click( object sender, EventArgs e )
		{
			string image_key = "";
			Imaging.ImageTransformationSettings settings = null;
			Image image = Image.FromFile( "../_support/test-image-256x256x32.png" );
			this.ResetList();

			// Add the original image.
			image_key = Guid.NewGuid().ToString();
			this.ImageList1.Images.Add( image_key, image );
			this.ListView1.Items.Add( "Original Image", image_key );

			// Flip Vertical
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageFlipVertical = true;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Flip Vertical", image_key );

			// Flip Horizontal
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageFlipHorizontal = true;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Flip Horizontal", image_key );

			this.UpdateList();
			return;
		}


		//-----------------------------------------------------
		private void mnuFade_Click( object sender, EventArgs e )
		{
			string image_key = "";
			Imaging.ImageTransformationSettings settings = null;
			Image image = Image.FromFile( "../_support/test-image-256x256x32.png" );
			this.ResetList();

			// Add the original image.
			image_key = Guid.NewGuid().ToString();
			this.ImageList1.Images.Add( image_key, image );
			this.ListView1.Items.Add( "Original Image", image_key );

			// Fade 0.10f
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageFade = 0.10f;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Fade 0.10f", image_key );

			// Fade 0.25f
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageFade = 0.25f;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Fade 0.25f", image_key );

			// Fade 0.50f
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageFade = 0.50f;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Fade 0.50f", image_key );

			// Fade 0.75f
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageFade = 0.75f;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Fade 0.75f", image_key );

			// Fade 1.00f
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageFade = 1.00f;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Fade 1.00f", image_key );

			this.UpdateList();
			return;
		}


		//-----------------------------------------------------
		private void mnuBrightness_Click( object sender, EventArgs e )
		{
			string image_key = "";
			Imaging.ImageTransformationSettings settings = null;
			Image image = Image.FromFile( "../_support/test-image-256x256x32.png" );
			this.ResetList();

			// Add the original image.
			image_key = Guid.NewGuid().ToString();
			this.ImageList1.Images.Add( image_key, image );
			this.ListView1.Items.Add( "Original Image", image_key );

			// Brightness 0.10f
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageBrightness = 0.10f;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Brightness 0.10f", image_key );

			// Brightness 0.25f
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageBrightness = 0.25f;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Brightness 0.25f", image_key );

			// Brightness 0.50f
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageBrightness = 0.50f;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Brightness 0.50f", image_key );

			// Brightness 0.75f
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageBrightness = 0.75f;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Brightness 0.75f", image_key );

			// Brightness 1.00f
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings( Imaging.ImagingOptionsHighQuality() );
			settings.ImageBrightness = 1.00f;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Brightness 1.00f", image_key );

			this.UpdateList();
			return;
		}


		//-----------------------------------------------------
		private void mnuDepth_Click( object sender, EventArgs e )
		{
			string image_key = "";
			Imaging.ImageTransformationSettings settings = null;
			Image image = Image.FromFile( "../_support/test-image-256x256x32.png" );
			this.ResetList();

			// Add the original image.
			image_key = Guid.NewGuid().ToString();
			this.ImageList1.Images.Add( image_key, image );
			this.ListView1.Items.Add( "Original Image", image_key );

			// Depth: Format16bppRgb555
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings();
			settings.ImageDepth = PixelFormat.Format16bppRgb555;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Depth: Format16bppRgb555", image_key );

			// Depth: Format16bppRgb565
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings();
			settings.ImageDepth = PixelFormat.Format16bppRgb565;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Depth: Format16bppRgb565", image_key );

			// Depth: Format24bppRgb
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings();
			settings.ImageDepth = PixelFormat.Format24bppRgb;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Depth: Format24bppRgb", image_key );

			// Depth: Format32bppArgb
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings();
			settings.ImageDepth = PixelFormat.Format32bppArgb;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Depth: Format32bppArgb", image_key );

			// Depth: Format32bppPArgb
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings();
			settings.ImageDepth = PixelFormat.Format32bppPArgb;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Depth: Format32bppPArgb", image_key );

			// Depth: Format32bppRgb
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings();
			settings.ImageDepth = PixelFormat.Format32bppRgb;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Depth: Format32bppRgb", image_key );

			// Depth: Format48bppRgb
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings();
			settings.ImageDepth = PixelFormat.Format48bppRgb;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Depth: Format48bppRgb", image_key );

			// Depth: Format64bppArgb
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings();
			settings.ImageDepth = PixelFormat.Format64bppArgb;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Depth: Format64bppArgb", image_key );

			// Depth: Format64bppPArgb
			image_key = Guid.NewGuid().ToString();
			settings = new Imaging.ImageTransformationSettings();
			settings.ImageDepth = PixelFormat.Format64bppPArgb;
			this.ImageList1.Images.Add( image_key, Imaging.TransformImage( image, settings ) );
			this.ListView1.Items.Add( "Depth: Format64bppPArgb", image_key );

			this.UpdateList();
			return;
		}


	}
}
