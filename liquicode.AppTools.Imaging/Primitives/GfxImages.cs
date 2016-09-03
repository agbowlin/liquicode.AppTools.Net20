

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;


namespace liquicode.AppTools
{
	public static partial class Imaging
	{


		//-----------------------------------------------------
		public static string[] SupportedImageFileExtensions
		{
			get { return new string[] { ".bmp", ".png", ".jpg", ".jpeg", ".gif", ".tif", ".tiff", ".ico", ".wmf", ".emf" }; }
		}


		//-----------------------------------------------------
		public static bool ImageFileExtensionSupported( string FileExtension_in )
		{
			foreach( string s in SupportedImageFileExtensions )
			{
				if( FileExtension_in.Equals( s, StringComparison.OrdinalIgnoreCase ) )
				{
					return true;
				}
			}
			return false;
		}


		//-----------------------------------------------------
		public static string[] SupportedMimeType
		{
			get { return new string[] { "image/bmp", "image/png", "image/jpeg", "image/gif", "image/tiff" }; }
		}


		//-----------------------------------------------------
		public static bool MimeTypeSupported( string MimeType_in )
		{
			foreach( string s in SupportedMimeType )
			{
				if( MimeType_in.Equals( s, StringComparison.OrdinalIgnoreCase ) )
				{
					return true;
				}
			}
			return false;
		}


		//-----------------------------------------------------
		public static Rectangle GetImagePreviewRect( Image Image_in, int PreviewSize_in )
		{
			double aspect = ((double)Image_in.Width / (double)Image_in.Height);
			Rectangle rect = new Rectangle();
			if( (Image_in.Width > Image_in.Height) )
			{
				rect.Size = new Size( PreviewSize_in, Convert.ToInt32( PreviewSize_in / aspect ) );
			}
			else
			{
				rect.Size = new Size( Convert.ToInt32( PreviewSize_in * aspect ), PreviewSize_in );
			}
			rect.Offset( Convert.ToInt32( (PreviewSize_in / 2) - (rect.Width / 2) ), Convert.ToInt32( (PreviewSize_in / 2) - (rect.Height / 2) ) );
			return rect;
		}


		//-----------------------------------------------------
		public static Image GetImagePreview( System.IO.Stream Stream_in, int PreviewSize_in, int PreviewDepth_in, Color PreviewBackcolor_in )
		{
			Image imageStream = null;
			Image imagePreview = null;
			try
			{
				imageStream = Image.FromStream( Stream_in );
				imagePreview = GetImagePreview( imageStream, PreviewSize_in, PreviewDepth_in, PreviewBackcolor_in );
			}
			catch( Exception )
			{
			}
			if( (imageStream != null) )
			{
				imageStream.Dispose();
			}
			return imagePreview;
		}


		//-----------------------------------------------------
		public static PixelFormat ImageDepth2PixelFormat( int ImageDepth )
		{
			// Get the image pixel format.
			if( (ImageDepth <= 1) )
			{
				return PixelFormat.Format1bppIndexed;
			}
			else if( (ImageDepth <= 4) )
			{
				return PixelFormat.Format4bppIndexed;
			}
			else if( (ImageDepth <= 8) )
			{
				return PixelFormat.Format8bppIndexed;
			}
			else if( (ImageDepth <= 16) )
			{
				return PixelFormat.Format16bppArgb1555;
			}
			else if( (ImageDepth <= 24) )
			{
				return PixelFormat.Format24bppRgb;
			}
			else if( (ImageDepth <= 32) )
			{
				return PixelFormat.Format32bppArgb;
			}
			else if( (ImageDepth <= 48) )
			{
				return PixelFormat.Format48bppRgb;
			}
			else if( (ImageDepth <= 64) )
			{
				return PixelFormat.Format64bppArgb;
			}
			else
			{
				return PixelFormat.Format24bppRgb;
			}
		}


		//-----------------------------------------------------
		public static Image GetImagePreview(
								Image Image_in
								, int PreviewSize_in
								, int PreviewDepth_in
								, Color PreviewBackcolor_in )
		{
			// Get the image pixel format.
			PixelFormat formatPreview = Imaging.ImageDepth2PixelFormat( PreviewDepth_in );

			// Determine the preview layout.
			Rectangle rect_image = new Rectangle( 0, 0, Image_in.Width, Image_in.Height );
			Rectangle rect_preview = new Rectangle( 0, 0, PreviewSize_in, PreviewSize_in );
			Rectangle rect_preview_draw = GetImagePreviewRect( Image_in, PreviewSize_in );

			// Create the preview image.
			Image image_preview = new Bitmap( PreviewSize_in, PreviewSize_in, formatPreview );

			// Create the preview graphics.
			Graphics gfxPreview = Graphics.FromImage( image_preview );
			gfxPreview.InterpolationMode = InterpolationMode.HighQualityBicubic;
			gfxPreview.CompositingQuality = CompositingQuality.HighQuality;

			gfxPreview.CompositingMode = CompositingMode.SourceCopy;
			gfxPreview.FillRectangle( new SolidBrush( Color.FromArgb( 255, 255, 255, 255 ) ), rect_preview );
			//gfxPreview.FillRectangle(new SolidBrush(PreviewBackcolor_in), rectPreview);

			// Render the image to the preview.
			gfxPreview.DrawImage( Image_in, rect_preview_draw, rect_image, GraphicsUnit.Pixel );

			// Return, OK
			gfxPreview.Dispose();
			return image_preview;

		}


		////-----------------------------------------------------
		//public static Rectangle GetImagePreviewRect( Image Image_in, int MaxWidth, int MaxHeight )
		//{
		//    double aspect_ratio = ((double)Image_in.Width / (double)Image_in.Height);

		//    int delta_width = MaxWidth - Image_in.Width;
		//    int delta_height = MaxHeight - Image_in.Height;

		//    if( (delta_width >= 0) && (delta_height >= 0) )
		//    {
		//        // Image is smaller than preview area.
		//    }

		//    Rectangle rect = new Rectangle();
		//    if( (Image_in.Width > Image_in.Height) )
		//    {
		//        rect.Size = new Size( PreviewSize_in, Convert.ToInt32( PreviewSize_in / aspect_ratio ) );
		//    }
		//    else
		//    {
		//        rect.Size = new Size( Convert.ToInt32( PreviewSize_in * aspect_ratio ), PreviewSize_in );
		//    }
		//    rect.Offset( Convert.ToInt32( (PreviewSize_in / 2) - (rect.Width / 2) ), Convert.ToInt32( (PreviewSize_in / 2) - (rect.Height / 2) ) );
		//    return rect;
		//}


		//-----------------------------------------------------
		public static Image GetImagePreview(
								Image OriginalImage
								, Size MaxSize
								, int Depth )
		{
			// Get the image pixel format.
			PixelFormat pixel_format = Imaging.ImageDepth2PixelFormat( Depth );

			// Determine the preview layout.
			Rectangle rect_image = new Rectangle( 0, 0, OriginalImage.Width, OriginalImage.Height );
			Rectangle rect_preview_area = new Rectangle( 0, 0, MaxSize.Width, MaxSize.Height );

			Rectangle rect_preview;
			int delta_width = MaxSize.Width - OriginalImage.Width;
			int delta_height = MaxSize.Height - OriginalImage.Height;

			if( (delta_width >= 0) && (delta_height >= 0) )
			{
				// Image is smaller than preview area.
				rect_preview = Imaging.ArrangeContentRectangle( rect_preview_area, rect_image.Size, ContentAlignment.MiddleCenter );
			}
			else if( delta_width <= delta_height )
			{
				// Fit the width
				rect_preview = new Rectangle(
										0, 0
										, MaxSize.Width
										, (int)((double)OriginalImage.Height
													* ((double)MaxSize.Width
														/ (double)OriginalImage.Width))
									);
			}
			else
			{
				// Fit the height
				rect_preview = new Rectangle(
										0, 0
										, (int)((double)OriginalImage.Width
													* ((double)MaxSize.Height
														/ (double)OriginalImage.Height))
										, MaxSize.Height
									);
			}
			//rect_preview_draw = Imaging.ArrangeContentRectangle( rect_preview_area, rect_image.Size, ContentAlignment.MiddleCenter );
			//Rectangle rect_preview_draw = GetImagePreviewRect( OriginalImage, PreviewSize_in );

			// Create the preview image.
			Image image_preview = new Bitmap( rect_preview.Width, rect_preview.Height, pixel_format );

			// Create the preview graphics.
			Graphics gfxPreview = Graphics.FromImage( image_preview );
			gfxPreview.InterpolationMode = InterpolationMode.HighQualityBicubic;
			gfxPreview.CompositingQuality = CompositingQuality.HighQuality;

			gfxPreview.CompositingMode = CompositingMode.SourceCopy;
			//gfxPreview.FillRectangle( new SolidBrush( Color.FromArgb( 255, 255, 255, 255 ) ), rect_preview_area );
			//gfxPreview.FillRectangle(new SolidBrush(PreviewBackcolor_in), rectPreview);

			// Render the image to the preview.
			gfxPreview.DrawImage( OriginalImage, rect_preview, rect_image, GraphicsUnit.Pixel );

			// Return, OK
			gfxPreview.Dispose();
			return image_preview;

		}


		//-----------------------------------------------------
		public static void DrawTranslucentImage( Graphics Graphics_in, Image Image_in, Rectangle Rectangle_in, float Opacity )
		{
			float[][] matrix =
			{
				new float[] {	1,	0,	0,	0,			0	},
				new float[] {	0,	1,	0,	0,			0	},
				new float[] {	0,	0,	1,	0,			0	},
				new float[] {	0,	0,	0,	Opacity,	0	},
				new float[] {	0,	0,	0,	0,			1	}
			};
			ColorMatrix color_matrix = new ColorMatrix( matrix );
			ImageAttributes attributes = new ImageAttributes();
			attributes.SetColorMatrix( color_matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap );
			Graphics_in.DrawImage( Image_in, Rectangle_in, 0, 0, Image_in.Width, Image_in.Height, GraphicsUnit.Pixel, attributes );
			return;
		}


		//-----------------------------------------------------
		public static void DrawTranslucentImage( Image BackgroundImage, Image TranslucentImage, Rectangle TranslucentRectangle, float Opacity )
		{
			using( Graphics graphics = Graphics.FromImage( BackgroundImage ) )
			{
				Imaging.DrawTranslucentImage( graphics, TranslucentImage, TranslucentRectangle, Opacity );
			}
			return;
		}


		////-----------------------------------------------------
		//public static Image ApplyOpacity( Image Image, float Value )
		//{
		//    float[][] matrix = new float[][]
		//    {
		//        new float[] {	1,	0,	0,	0,		0	},
		//        new float[] {	0,	1,	0,	0,		0	},
		//        new float[] {	0,	0,	1,	0,		0	},
		//        new float[] {	0,	0,	0,	Value,	0	},
		//        new float[] {	0,	0,	0,	0,		1	}
		//    };
		//    return ApplyColorMatrix( matrix, Image );
		//}


		//-----------------------------------------------------
		public static Image ApplyBrightness( Image Image, float Value )
		{
			float[][] matrix = new float[][]
			{
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {Value, Value, Value, 1, 1}
            };
			return ApplyColorMatrix( matrix, Image );
		}


		//-----------------------------------------------------
		public static Image ApplyColorMatrix( float[][] Matrix, Image OriginalImage )
		{
			Image new_image = new Bitmap( OriginalImage.Width, OriginalImage.Height );
			using( Graphics graphics = Graphics.FromImage( new_image ) )
			{
				ColorMatrix color_matrix = new ColorMatrix( Matrix );
				using( ImageAttributes attributes = new ImageAttributes() )
				{
					attributes.SetColorMatrix( color_matrix );
					Rectangle rect_image = new Rectangle( 0, 0, OriginalImage.Width, OriginalImage.Height );
					graphics.DrawImage(
						OriginalImage, rect_image
						, 0, 0, rect_image.Width, rect_image.Height
						, GraphicsUnit.Pixel, attributes );
				}
			}
			return new_image;
		}


		////-----------------------------------------------------
		//public static Image FadeImage( Image Image_in, Color BackColor_in, float Angle_in )
		//{
		//    Image image = new Bitmap( Image_in.Width, Image_in.Height );
		//    // Draw the source image.
		//    Graphics gfx = Graphics.FromImage( image );
		//    gfx.DrawImage( Image_in, 0, 0 );
		//    // Apply a transparent gradient.
		//    Rectangle rc = new Rectangle( 0, -1, image.Width, image.Height );
		//    LinearGradientBrush br = new LinearGradientBrush( rc, Color.Transparent, BackColor_in, Angle_in, false );
		//    gfx.FillRectangle( br, rc );
		//    // Cleanup and return.
		//    gfx.Dispose();
		//    return image;
		//}


		//  '-----------------------------------------------------
		//  Public Shared Function RotateImage( _
		//                                        ByVal Image_in As Drawing.Image _
		//                                      , ByVal BackColor_in As Drawing.Color _
		//                                      , ByVal Angle_in As Single _
		//                                  ) As Drawing.Image
		//      Dim image As Drawing.Image = New Drawing.Bitmap(Image_in.Width, Image_in.Height)
		//      ' Draw the source image.
		//      Dim gfx As Drawing.Graphics = Drawing.Graphics.FromImage(image)
		//      gfx.DrawImage(Image_in, 0, 0)
		//      ' Apply a rotation.
		//      gfx.ScaleTransform(1, -1.0F, Drawing.Drawing2D.MatrixOrder.Prepend)
		//      Dim colormatrix As New Drawing.Imaging.ColorMatrix
		//      Dim attributes As New Drawing.Imaging.ImageAttributes
		//      colormatrix.Matrix33 = 0.5
		//      attributes.SetColorMatrix(colormatrix)

		//Rectangle source = new Rectangle( 0, -( height ), mirror.Width, mirror.Height );
		//g.DrawImage( mirror, source, 0, 0, mirror.Width, mirror.Height, GraphicsUnit.Pixel, attributes );

		//      Dim rc As New Drawing.Rectangle(0, -1, image.Width, image.Height)
		//      Dim br As New Drawing.Drawing2D.LinearGradientBrush(rc, Color.Transparent, BackColor_in, Angle_in, False)
		//      gfx.FillRectangle(Brushes.Brown, rc)
		//      ' Cleanup and return.
		//      gfx.Dispose()
		//      Return image
		//  End Function


		//  Private Function Average(ByVal Size As Size, ByVal imageSize As SizeF, ByVal PixelX As Integer, ByVal Pixely As Integer) As Color
		//      Dim pixels As New ArrayList
		//      Dim x As Integer, y As Integer
		//      Dim bmp As Bitmap = PictureBox1.Image.Clone

		//      ' Find the color for each pixel and add it to a new array.
		//      '
		//      ' Remember a 5X5 area on or near the edge will ask for pixels that don't
		//      ' exist in our image, this will filter those out.
		//      '
		//      For x = PixelX - CInt(Size.Width / 2) To PixelX + CInt(Size.Width / 2)
		//          For y = Pixely - CInt(Size.Height / 2) To Pixely + CInt(Size.Height / 2)
		//              If (x > 0 And x < imageSize.Width) And _
		//                 (y > 0 And y < imageSize.Height) Then
		//                  pixels.Add(bmp.GetPixel(x, y))
		//              End If
		//          Next
		//      Next

		//      ' Adverage the A, R, G, B channels 
		//      ' reflected in the array
		//      Dim thisColor As Color

		//      Dim alpha As Integer = 0
		//      Dim red As Integer = 0
		//      Dim green As Integer = 0
		//      Dim blue As Integer = 0

		//      For Each thisColor In pixels
		//          alpha += thisColor.A
		//          red += thisColor.R
		//          green += thisColor.G
		//          blue += thisColor.B
		//      Next

		//      ' Return the sum of the colors / the number of colors (The average)
		//      '
		//      Return Color.FromArgb(alpha / pixels.Count, _
		//                            red / pixels.Count, _
		//                            green / pixels.Count, _
		//                            blue / pixels.Count)
		//  End Function

		//  Private Sub GausianBlurImage(ByVal alphaEdgesOnly As Boolean, ByVal blurSize As Size)
		//      Dim PixelY As Integer
		//      Dim PixelX As Integer
		//      Dim bmp As Bitmap = PictureBox1.Image.Clone

		//      ' UI Stuff
		//      Label1.Text = "Applying Gausian Blur of " & blurSize.ToString

		//      Progress.Maximum = bmp.Height * bmp.Width
		//      Progress.Minimum = 0
		//      Progress.Value = 0

		//      ' Loop the rows of the image
		//      For PixelY = 0 To bmp.Width - 1

		//          ' Loop the cols of the image
		//          For PixelX = 0 To bmp.Height - 1
		//              If Not alphaEdgesOnly Then
		//                  bmp.SetPixel(PixelX, PixelY, Average(blurSize, bmp.PhysicalDimension, PixelX, PixelY))
		//              ElseIf bmp.GetPixel(PixelX, PixelY).A <> 255 Then
		//                  bmp.SetPixel(PixelX, PixelY, Average(blurSize, bmp.PhysicalDimension, PixelX, PixelY))
		//              End If
		//              Progress.Value += 1
		//              Application.DoEvents()
		//          Next
		//      Next

		//      PictureBox1.Image = bmp.Clone
		//      bmp.Dispose()
		//  End Sub


	}


}
