

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;


namespace liquicode.AppTools
{
	public static partial class Imaging
	{


		public class ImagingOptions
		{


			//-----------------------------------------------------
			public CompositingMode CompositingMode = CompositingMode.SourceOver;
			public CompositingQuality CompositingQuality = CompositingQuality.Default;
			public InterpolationMode InterpolationMode = InterpolationMode.Default;
			public PixelOffsetMode PixelOffsetMode = PixelOffsetMode.Default;
			public SmoothingMode SmoothingMode = SmoothingMode.Default;
			public TextRenderingHint TextRenderingHint = TextRenderingHint.SystemDefault;
			public int TextContrast = 4; // The gamma correction value used for rendering antialiased and ClearType text.


			//-----------------------------------------------------
			public ImagingOptions()
			{
				return;
			}


			//-----------------------------------------------------
			public ImagingOptions( Graphics Graphics_in )
			{
				this.CopyFrom( Graphics_in );
				return;
			}


			//-----------------------------------------------------
			public ImagingOptions Clone()
			{
				ImagingOptions value = new ImagingOptions();
				value.CompositingMode = this.CompositingMode;
				value.CompositingQuality = this.CompositingQuality;
				value.InterpolationMode = this.InterpolationMode;
				value.PixelOffsetMode = this.PixelOffsetMode;
				value.SmoothingMode = this.SmoothingMode;
				value.TextRenderingHint = this.TextRenderingHint;
				value.TextContrast = this.TextContrast;
				return value;
			}


			//-----------------------------------------------------
			public void CopyFrom( Graphics Graphics_in )
			{
				this.CompositingMode = Graphics_in.CompositingMode;
				this.CompositingQuality = Graphics_in.CompositingQuality;
				this.InterpolationMode = Graphics_in.InterpolationMode;
				this.PixelOffsetMode = Graphics_in.PixelOffsetMode;
				this.SmoothingMode = Graphics_in.SmoothingMode;
				this.TextRenderingHint = Graphics_in.TextRenderingHint;
				this.TextContrast = Graphics_in.TextContrast;
				return;
			}


			//-----------------------------------------------------
			public void CopyTo( Graphics Graphics_in )
			{
				Graphics_in.CompositingMode = this.CompositingMode;
				Graphics_in.CompositingQuality = this.CompositingQuality;
				Graphics_in.InterpolationMode = this.InterpolationMode;
				Graphics_in.PixelOffsetMode = this.PixelOffsetMode;
				Graphics_in.SmoothingMode = this.SmoothingMode;
				Graphics_in.TextRenderingHint = this.TextRenderingHint;
				Graphics_in.TextContrast = this.TextContrast;
				return;
			}


			//-----------------------------------------------------
			public void SelectDefault()
			{
				this.CompositingMode = CompositingMode.SourceCopy;
				this.CompositingQuality = CompositingQuality.Default;
				this.InterpolationMode = InterpolationMode.Default;
				this.PixelOffsetMode = PixelOffsetMode.Default;
				this.SmoothingMode = SmoothingMode.Default;
				this.TextRenderingHint = TextRenderingHint.SystemDefault;
				this.TextContrast = 4;
				return;
			}


			//-----------------------------------------------------
			public void SelectHighSpeed()
			{
				this.CompositingQuality = CompositingQuality.HighSpeed;
				this.InterpolationMode = InterpolationMode.Low;
				this.PixelOffsetMode = PixelOffsetMode.HighSpeed;
				this.SmoothingMode = SmoothingMode.HighSpeed;
				this.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
				return;
			}


			//-----------------------------------------------------
			public void SelectHighQuality()
			{
				this.CompositingQuality = CompositingQuality.HighQuality;
				this.InterpolationMode = InterpolationMode.HighQualityBicubic;
				this.PixelOffsetMode = PixelOffsetMode.HighQuality;
				this.SmoothingMode = SmoothingMode.HighQuality;
				this.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
				return;
			}


		}


	}
}
