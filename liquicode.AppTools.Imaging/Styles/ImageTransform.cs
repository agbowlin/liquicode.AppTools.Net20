

using System;
using System.Drawing;
using System.Drawing.Imaging;


namespace liquicode.AppTools
{
	public static partial class Imaging
	{


		public class ImageTransformationSettings
		{


			//-----------------------------------------------------
			public ImagingOptions ImagingOptions = null;

			//-----------------------------------------------------
			public int ImageWidth = 0;
			public int ImageHeight = 0;
			public PixelFormat ImageDepth = PixelFormat.Undefined;

			//-----------------------------------------------------
			public int ImageRotate = 0;
			public bool ImageFlipHorizontal = false;
			public bool ImageFlipVertical = false;

			//-----------------------------------------------------
			public float ImageFade = 0.0f;
			public float ImageBrightness = 0.0f;

			//-----------------------------------------------------
			public bool StampEnabled = false;
			public string StampText = "";
			public ContentAlignment StampAlignment = ContentAlignment.TopLeft;
			public Color StampForeColor = Color.Black;
			public Color StampBackColor = Color.Empty;
			public int StampOpacity = 255;

			//-----------------------------------------------------
			public ImageTransformationSettings()
			{
				this.ImagingOptions = Imaging.ImagingOptionsDefault();
				return;
			}

			//-----------------------------------------------------
			public ImageTransformationSettings( ImagingOptions ImagingOptions )
			{
				this.ImagingOptions = ImagingOptions;
				return;
			}

		}


		//-----------------------------------------------------
		public static Image TransformImage( Image Image, ImageTransformationSettings Settings )
		{
			// Get the image dimensions.
			int new_width = Settings.ImageWidth;
			if( new_width == 0 ) { new_width = Image.Width; }
			int new_height = Settings.ImageHeight;
			if( new_height == 0 ) { new_height = Image.Height; }
			PixelFormat new_depth = Settings.ImageDepth;
			if( new_depth == PixelFormat.Undefined ) { new_depth = Image.PixelFormat; }

			// Create the new image.
			Image new_image = new Bitmap( new_width, new_height, new_depth );

			// Create the graphics and draw the new image.
			using( Graphics graphics = Graphics.FromImage( new_image ) )
			{
				Settings.ImagingOptions.CopyTo( graphics );
				Rectangle rect_image = new Rectangle( 0, 0, Image.Width, Image.Height );
				Rectangle new_rect_image = new Rectangle( 0, 0, new_image.Width, new_image.Height );
				graphics.DrawImage( Image, new_rect_image, rect_image, GraphicsUnit.Pixel );
			}

			// Rotate
			if( Settings.ImageRotate == 90 )
			{
				new_image.RotateFlip( RotateFlipType.Rotate90FlipNone );
			}
			else if( Settings.ImageRotate == 180 )
			{
				new_image.RotateFlip( RotateFlipType.Rotate180FlipNone );
			}
			else if( Settings.ImageRotate == 270 )
			{
				new_image.RotateFlip( RotateFlipType.Rotate270FlipNone );
			}

			// Flip
			if( Settings.ImageFlipHorizontal && Settings.ImageFlipVertical )
			{
				new_image.RotateFlip( RotateFlipType.RotateNoneFlipXY );
			}
			else if( Settings.ImageFlipHorizontal )
			{
				new_image.RotateFlip( RotateFlipType.RotateNoneFlipX );
			}
			else if( Settings.ImageFlipVertical )
			{
				new_image.RotateFlip( RotateFlipType.RotateNoneFlipY );
			}

			// Transparency
			if( Settings.ImageFade > 0.0f )
			{
				Image new_image_2 = new Bitmap( new_image.Width, new_image.Height, new_image.PixelFormat );
				using( Graphics graphics = Graphics.FromImage( new_image_2 ) )
				{
					Settings.ImagingOptions.CopyTo( graphics );
					Rectangle rect_image = new Rectangle( 0, 0, new_image.Width, new_image.Height );
					float opacity = (1 - Settings.ImageFade);
					if( opacity < 0.0f ) { opacity = 0.0f; }
					if( opacity > 1.0f ) { opacity = 1.0f; }
					Imaging.DrawTranslucentImage( graphics, new_image, rect_image, opacity );
				}
				new_image.Dispose(); // ???
				new_image = new_image_2;
			}

			// Brightness
			if( Settings.ImageBrightness > 0.0f )
			{
				Image new_image_2 = new Bitmap( new_image.Width, new_image.Height, new_image.PixelFormat );
				using( Graphics graphics = Graphics.FromImage( new_image_2 ) )
				{
					Settings.ImagingOptions.CopyTo( graphics );
					Rectangle rect_image = new Rectangle( 0, 0, new_image.Width, new_image.Height );
					float brightness = Settings.ImageBrightness;
					if( brightness < 0.0f ) { brightness = 0.0f; }
					if( brightness > 1.0f ) { brightness = 1.0f; }
					new_image_2 = Imaging.ApplyBrightness( new_image, brightness );
				}
				new_image.Dispose(); // ???
				new_image = new_image_2;
			}

			//// Fade
			//if( Settings.ImageFadeAngle >= 0.0f )
			//{
			//    Image new_image_2 = new Bitmap( new_image.Width, new_image.Height, new_image.PixelFormat );
			//    using( Graphics graphics = Graphics.FromImage( new_image_2 ) )
			//    {
			//        Settings.ImagingOptions.CopyTo( graphics );
			//        Rectangle rect_image = new Rectangle( 0, 0, new_image.Width, new_image.Height );
			//        float angle = Settings.ImageFadeAngle;
			//        new_image_2 = Imaging.FadeImage( new_image, Settings.ImageFadeBackColor, angle );
			//    }
			//    new_image.Dispose(); // ???
			//    new_image = new_image_2;
			//}

			// Return, OK
			return new_image;
		}


	}
}
