

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
		public static Image GetImagePreview( Image Image_in, int PreviewSize_in, int PreviewDepth_in, Color PreviewBackcolor_in )
		{

			//Return Image_in.GetThumbnailImage(PreviewSize_in, PreviewSize_in, New Drawing.Image.GetThumbnailImageAbort(AddressOf Me._GetThumbnailImageAbort), IntPtr.Zero)

			// Get the image pixel format.
			PixelFormat formatPreview = PixelFormat.DontCare;
			if( (PreviewDepth_in <= 1) )
			{
				formatPreview = PixelFormat.Format1bppIndexed;
			}
			else if( (PreviewDepth_in <= 4) )
			{
				formatPreview = PixelFormat.Format4bppIndexed;
			}
			else if( (PreviewDepth_in <= 8) )
			{
				formatPreview = PixelFormat.Format8bppIndexed;
			}
			else if( (PreviewDepth_in <= 16) )
			{
				formatPreview = PixelFormat.Format16bppArgb1555;
			}
			else if( (PreviewDepth_in <= 24) )
			{
				formatPreview = PixelFormat.Format24bppRgb;
			}
			else if( (PreviewDepth_in <= 32) )
			{
				formatPreview = PixelFormat.Format32bppArgb;
			}
			else if( (PreviewDepth_in <= 48) )
			{
				formatPreview = PixelFormat.Format48bppRgb;
			}
			else if( (PreviewDepth_in <= 64) )
			{
				formatPreview = PixelFormat.Format64bppArgb;
			}
			else
			{
				formatPreview = PixelFormat.Format24bppRgb;
			}

			// Determine the preview layout.
			Rectangle rectImage = new Rectangle( 0, 0, Image_in.Width, Image_in.Height );
			Rectangle rectPreview = new Rectangle( 0, 0, PreviewSize_in, PreviewSize_in );
			Rectangle rectPreviewDraw = GetImagePreviewRect( Image_in, PreviewSize_in );

			// Create the preview image.
			Image imagePreview = new Bitmap( PreviewSize_in, PreviewSize_in, formatPreview );

			// Create the preview graphics.
			Graphics gfxPreview = Graphics.FromImage( imagePreview );
			gfxPreview.InterpolationMode = InterpolationMode.HighQualityBicubic;
			gfxPreview.CompositingQuality = CompositingQuality.HighQuality;
#if( NodeTransparency == True)
			gfxPreview.CompositingMode = CompositingMode.SourceCopy;
			gfxPreview.FillRectangle( new SolidBrush( Color.FromArgb( 255, 255, 255, 255 ) ), rectPreview );
#else
			gfxPreview.FillRectangle(new SolidBrush(PreviewBackcolor_in), rectPreview);
#endif

			// Render the image to the preview.
			gfxPreview.DrawImage( Image_in, rectPreviewDraw, rectImage, GraphicsUnit.Pixel );

			// Return, OK
			gfxPreview.Dispose();
			return imagePreview;

		}


		//-----------------------------------------------------
		public static void DrawTranslucentImage( Graphics Graphics_in, Image Image_in, Rectangle Rectangle_in, float Opacity_in )
		{
			float[][] colormatrix_values =
			{
				new float[] {		1,				0,				0,				0,				0			},
				new float[] {		0,				1,				0,				0,				0			},
				new float[] {		0,				0,				1,				0,				0			},
				new float[] {		0,				0,				0,				Opacity_in,		0			},
				new float[] {		0,				0,				0,				0,				1			}
			};
			ColorMatrix colormatrix = new ColorMatrix( colormatrix_values );
			ImageAttributes attributes = new ImageAttributes();
			attributes.SetColorMatrix( colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap );
			Graphics_in.DrawImage( Image_in, Rectangle_in, 0, 0, Image_in.Width, Image_in.Height, GraphicsUnit.Pixel, attributes );
			return;
		}


		//-----------------------------------------------------
		public static Image FadeImage( Image Image_in, Color BackColor_in, float Angle_in )
		{
			Image image = new Bitmap( Image_in.Width, Image_in.Height );
			// Draw the source image.
			Graphics gfx = Graphics.FromImage( image );
			gfx.DrawImage( Image_in, 0, 0 );
			// Apply a transparent gradient.
			Rectangle rc = new Rectangle( 0, -1, image.Width, image.Height );
			LinearGradientBrush br = new LinearGradientBrush( rc, Color.Transparent, BackColor_in, Angle_in, false );
			gfx.FillRectangle( Brushes.Brown, rc );
			// Cleanup and return.
			gfx.Dispose();
			return image;
		}


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
