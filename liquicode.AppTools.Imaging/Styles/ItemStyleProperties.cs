

using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace liquicode.AppTools
{
	public static partial class Imaging
	{


		public class ItemStyleProperties
		{


			//-----------------------------------------------------
			[System.Xml.Serialization.XmlIgnore()]
			public Imaging.ItemStyle ItemStyle = new Imaging.ItemStyle();

			//-----------------------------------------------------
			public ItemStyleProperties()
			{
				return;
			}

			//-----------------------------------------------------
			public ItemStyleProperties( Imaging.ItemStyle ItemStyle )
			{
				this.ItemStyle = ItemStyle;
				return;
			}


			//-----------------------------------------------------
			//NOTE: For this Clone function to worrk, the ItemStyleProperties class cannot be defined within another static class.
			//public ItemStyleProperties Clone()
			//{
			//    System.Xml.Serialization.XmlSerializer xmls = new System.Xml.Serialization.XmlSerializer( typeof( ItemStyleProperties ) );
			//    System.IO.MemoryStream stream = new System.IO.MemoryStream();
			//    xmls.Serialize( stream, this );
			//    stream.Position = 0;
			//    ItemStyleProperties obj = (ItemStyleProperties)xmls.Deserialize( stream );
			//    return obj;
			//}


			//=====================================================
			//
			//       BORDER
			//
			//=====================================================

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public CompositingMode BorderOption_CompositingMode
			{
				get { return this.ItemStyle.BorderStyle.ImagingOptions.CompositingMode; }
				set { this.ItemStyle.BorderStyle.ImagingOptions.CompositingMode = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public CompositingQuality BorderOption_CompositingQuality
			{
				get { return this.ItemStyle.BorderStyle.ImagingOptions.CompositingQuality; }
				set
				{
					if( (value == CompositingQuality.Invalid) )
					{
						value = CompositingQuality.Default;
					}
					this.ItemStyle.BorderStyle.ImagingOptions.CompositingQuality = value;
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public InterpolationMode BorderOption_InterpolationMode
			{
				get { return this.ItemStyle.BorderStyle.ImagingOptions.InterpolationMode; }
				set
				{
					if( (value == InterpolationMode.Invalid) )
					{
						value = InterpolationMode.Default;
					}
					this.ItemStyle.BorderStyle.ImagingOptions.InterpolationMode = value;
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public PixelOffsetMode BorderOption_PixelOffsetMode
			{
				get { return this.ItemStyle.BorderStyle.ImagingOptions.PixelOffsetMode; }
				set
				{
					if( (value == PixelOffsetMode.Invalid) )
					{
						value = PixelOffsetMode.Default;
					}
					this.ItemStyle.BorderStyle.ImagingOptions.PixelOffsetMode = value;
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public SmoothingMode BorderOption_SmoothingMode
			{
				get { return this.ItemStyle.BorderStyle.ImagingOptions.SmoothingMode; }
				set
				{
					if( (value == SmoothingMode.Invalid) )
					{
						value = SmoothingMode.Default;
					}
					this.ItemStyle.BorderStyle.ImagingOptions.SmoothingMode = value;
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public int BorderOption_TextContrast
			{
				get { return this.ItemStyle.BorderStyle.ImagingOptions.TextContrast; }
				set { this.ItemStyle.BorderStyle.ImagingOptions.TextContrast = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public System.Drawing.Text.TextRenderingHint BorderOption_TextRenderingHint
			{
				get { return this.ItemStyle.BorderStyle.ImagingOptions.TextRenderingHint; }
				set { this.ItemStyle.BorderStyle.ImagingOptions.TextRenderingHint = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public int BorderStyle_OutsideEdgeSize
			{
				get { return this.ItemStyle.BorderStyle.OutsideEdgeSize; }
				set { this.ItemStyle.BorderStyle.OutsideEdgeSize = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public int BorderStyle_InsideEdgeSize
			{
				get { return this.ItemStyle.BorderStyle.InsideEdgeSize; }
				set { this.ItemStyle.BorderStyle.InsideEdgeSize = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public int BorderStyle_BorderSize
			{
				get { return this.ItemStyle.BorderStyle.BorderSize; }
				set { this.ItemStyle.BorderStyle.BorderSize = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public int BorderStyle_BevelOuter
			{
				get { return this.ItemStyle.BorderStyle.BevelOuter; }
				set { this.ItemStyle.BorderStyle.BevelOuter = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public int BorderStyle_BevelInner
			{
				get { return this.ItemStyle.BorderStyle.BevelInner; }
				set { this.ItemStyle.BorderStyle.BevelInner = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public float BorderStyle_ArcRadius
			{
				get { return this.ItemStyle.BorderStyle.ArcRadius; }
				set { this.ItemStyle.BorderStyle.ArcRadius = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" ), System.Xml.Serialization.XmlIgnore()]
			public Color BorderStyle_OutsideEdgeColor
			{
				get { return this.ItemStyle.BorderStyle.OutsideEdgeColor; }
				set { this.ItemStyle.BorderStyle.OutsideEdgeColor = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public int BorderStyle_OutsideEdgeColorArgb
			{
				get { return this.ItemStyle.BorderStyle.OutsideEdgeColor.ToArgb(); }
				set { this.ItemStyle.BorderStyle.OutsideEdgeColor = Color.FromArgb( value ); }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" ), System.Xml.Serialization.XmlIgnore()]
			public Color BorderStyle_InsideEdgeColor
			{
				get { return this.ItemStyle.BorderStyle.InsideEdgeColor; }
				set { this.ItemStyle.BorderStyle.InsideEdgeColor = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public int BorderStyle_InsideEdgeColorArgb
			{
				get { return this.ItemStyle.BorderStyle.InsideEdgeColor.ToArgb(); }
				set { this.ItemStyle.BorderStyle.InsideEdgeColor = Color.FromArgb( value ); }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" ), System.Xml.Serialization.XmlIgnore()]
			public Color BorderStyle_BorderColor
			{
				get { return this.ItemStyle.BorderStyle.BorderColor; }
				set { this.ItemStyle.BorderStyle.BorderColor = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public int BorderStyle_BorderColorArgb
			{
				get { return this.ItemStyle.BorderStyle.BorderColor.ToArgb(); }
				set { this.ItemStyle.BorderStyle.BorderColor = Color.FromArgb( value ); }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" ), System.Xml.Serialization.XmlIgnore()]
			public Color BorderStyle_LightColor
			{
				get { return this.ItemStyle.BorderStyle.LightColor; }
				set { this.ItemStyle.BorderStyle.LightColor = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public int BorderStyle_LightColorArgb
			{
				get { return this.ItemStyle.BorderStyle.LightColor.ToArgb(); }
				set { this.ItemStyle.BorderStyle.LightColor = Color.FromArgb( value ); }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" ), System.Xml.Serialization.XmlIgnore()]
			public Color BorderStyle_DarkColor
			{
				get { return this.ItemStyle.BorderStyle.DarkColor; }
				set { this.ItemStyle.BorderStyle.DarkColor = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Border" )]
			public int BorderStyle_DarkColorArgb
			{
				get { return this.ItemStyle.BorderStyle.DarkColor.ToArgb(); }
				set { this.ItemStyle.BorderStyle.DarkColor = Color.FromArgb( value ); }
			}


			//=====================================================
			//
			//       IMAGE
			//
			//=====================================================

			//'-----------------------------------------------------
			//<System.ComponentModel.Category("Image"), Xml.Serialization.XmlIgnore()> _
			//Public Property Image() As Drawing.Image
			//    Get
			//        Return Me.NodeStyle.Image
			//    End Get
			//    Set(ByVal value As Drawing.Image)
			//        Me.NodeStyle.Image = value
			//    End Set
			//End Property

			//'-----------------------------------------------------
			//<System.ComponentModel.Category("Image")> _
			//Public Property ImageBytes() As Byte()
			//    Get
			//        If IsNothing(Me.NodeStyle.Image) Then Return New Byte() {}
			//        Dim stream As New IO.MemoryStream
			//        Me.NodeStyle.Image.Save(stream, Drawing.Imaging.ImageFormat.Png)
			//        Dim rg() As Byte = stream.ToArray()
			//        stream.Close()
			//        Return rg
			//    End Get
			//    Set(ByVal value As Byte())
			//        Me.NodeStyle.Image = Nothing
			//        If IsNothing(value) Then Return
			//        If (value.Length = 0) Then Return
			//        Dim stream As New IO.MemoryStream(value)
			//        Me.NodeStyle.Image = Drawing.Image.FromStream(stream)
			//        stream.Close()
			//        Return
			//    End Set
			//End Property

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Image" )]
			public CompositingMode ImageOption_CompositingMode
			{
				get { return this.ItemStyle.ImageStyle.ImagingOptions.CompositingMode; }
				set { this.ItemStyle.ImageStyle.ImagingOptions.CompositingMode = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Image" )]
			public CompositingQuality ImageOption_CompositingQuality
			{
				get { return this.ItemStyle.ImageStyle.ImagingOptions.CompositingQuality; }
				set
				{
					if( (value == CompositingQuality.Invalid) )
					{
						value = CompositingQuality.Default;
					}
					this.ItemStyle.ImageStyle.ImagingOptions.CompositingQuality = value;
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Image" )]
			public InterpolationMode ImageOption_InterpolationMode
			{
				get { return this.ItemStyle.ImageStyle.ImagingOptions.InterpolationMode; }
				set
				{
					if( (value == InterpolationMode.Invalid) )
					{
						value = InterpolationMode.Default;
					}
					this.ItemStyle.ImageStyle.ImagingOptions.InterpolationMode = value;
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Image" )]
			public PixelOffsetMode ImageOption_PixelOffsetMode
			{
				get { return this.ItemStyle.ImageStyle.ImagingOptions.PixelOffsetMode; }
				set
				{
					if( (value == PixelOffsetMode.Invalid) )
					{
						value = PixelOffsetMode.Default;
					}
					this.ItemStyle.ImageStyle.ImagingOptions.PixelOffsetMode = value;
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Image" )]
			public SmoothingMode ImageOption_SmoothingMode
			{
				get { return this.ItemStyle.ImageStyle.ImagingOptions.SmoothingMode; }
				set
				{
					if( (value == SmoothingMode.Invalid) )
					{
						value = SmoothingMode.Default;
					}
					this.ItemStyle.ImageStyle.ImagingOptions.SmoothingMode = value;
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Image" )]
			public int ImageOption_TextContrast
			{
				get { return this.ItemStyle.ImageStyle.ImagingOptions.TextContrast; }
				set { this.ItemStyle.ImageStyle.ImagingOptions.TextContrast = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Image" )]
			public System.Drawing.Text.TextRenderingHint ImageOption_TextRenderingHint
			{
				get { return this.ItemStyle.ImageStyle.ImagingOptions.TextRenderingHint; }
				set { this.ItemStyle.ImageStyle.ImagingOptions.TextRenderingHint = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Image" )]
			public int ImageStyle_Opacity
			{
				get { return this.ItemStyle.ImageStyle.Opacity; }
				set { this.ItemStyle.ImageStyle.Opacity = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Image" ), System.Xml.Serialization.XmlIgnore()]
			public Color ImageStyle_BackColor
			{
				get { return this.ItemStyle.ImageStyle.BackColor; }
				set { this.ItemStyle.ImageStyle.BackColor = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Image" )]
			public int ImageStyle_BackColorArgb
			{
				get { return this.ItemStyle.ImageStyle.BackColor.ToArgb(); }
				set { this.ItemStyle.ImageStyle.BackColor = Color.FromArgb( value ); }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Image" )]
			public ContentAlignment ImageStyle_Alignment
			{
				get { return this.ItemStyle.ImageStyle.Alignment; }
				set { this.ItemStyle.ImageStyle.Alignment = value; }
			}


			//=====================================================
			//
			//       TEXT
			//
			//=====================================================

			//'-----------------------------------------------------
			//<System.ComponentModel.Category("Text")> _
			//Public Property Text() As String
			//    Get
			//        Return Me.NodeStyle.Text
			//    End Get
			//    Set(ByVal value As String)
			//        Me.NodeStyle.Text = value
			//    End Set
			//End Property

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public CompositingMode TextOption_CompositingMode
			{
				get { return this.ItemStyle.TextStyle.ImagingOptions.CompositingMode; }
				set { this.ItemStyle.TextStyle.ImagingOptions.CompositingMode = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public CompositingQuality TextOption_CompositingQuality
			{
				get { return this.ItemStyle.TextStyle.ImagingOptions.CompositingQuality; }
				set
				{
					if( (value == CompositingQuality.Invalid) )
					{
						value = CompositingQuality.Default;
					}
					this.ItemStyle.TextStyle.ImagingOptions.CompositingQuality = value;
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public InterpolationMode TextOption_InterpolationMode
			{
				get { return this.ItemStyle.TextStyle.ImagingOptions.InterpolationMode; }
				set
				{
					if( (value == InterpolationMode.Invalid) )
					{
						value = InterpolationMode.Default;
					}
					this.ItemStyle.TextStyle.ImagingOptions.InterpolationMode = value;
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public PixelOffsetMode TextOption_PixelOffsetMode
			{
				get { return this.ItemStyle.TextStyle.ImagingOptions.PixelOffsetMode; }
				set
				{
					if( (value == PixelOffsetMode.Invalid) )
					{
						value = PixelOffsetMode.Default;
					}
					this.ItemStyle.TextStyle.ImagingOptions.PixelOffsetMode = value;
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public SmoothingMode TextOption_SmoothingMode
			{
				get { return this.ItemStyle.TextStyle.ImagingOptions.SmoothingMode; }
				set
				{
					if( (value == SmoothingMode.Invalid) )
					{
						value = SmoothingMode.Default;
					}
					this.ItemStyle.TextStyle.ImagingOptions.SmoothingMode = value;
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public int TextOption_TextContrast
			{
				get { return this.ItemStyle.TextStyle.ImagingOptions.TextContrast; }
				set { this.ItemStyle.TextStyle.ImagingOptions.TextContrast = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public System.Drawing.Text.TextRenderingHint TextOption_TextRenderingHint
			{
				get { return this.ItemStyle.TextStyle.ImagingOptions.TextRenderingHint; }
				set { this.ItemStyle.TextStyle.ImagingOptions.TextRenderingHint = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" ), System.Xml.Serialization.XmlIgnore()]
			public Font TextStyle_Font
			{
				get { return this.ItemStyle.TextStyle.Font; }
				set { this.ItemStyle.TextStyle.Font = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public string TextStyle_FontFamily
			{
				get { return this.ItemStyle.TextStyle.Font.Name; }
				set
				{
					Font fnt = this.ItemStyle.TextStyle.Font;
					this.ItemStyle.TextStyle.Font = new Font( value, fnt.Size, fnt.Style, fnt.Unit );
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public float TextStyle_FontSize
			{
				get { return this.ItemStyle.TextStyle.Font.Size; }
				set
				{
					Font fnt = this.ItemStyle.TextStyle.Font;
					this.ItemStyle.TextStyle.Font = new Font( fnt.FontFamily, value, fnt.Style, fnt.Unit );
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public FontStyle TextStyle_FontStyle
			{
				get { return this.ItemStyle.TextStyle.Font.Style; }
				set
				{
					Font fnt = this.ItemStyle.TextStyle.Font;
					this.ItemStyle.TextStyle.Font = new Font( fnt.FontFamily, fnt.Size, value, fnt.Unit );
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public GraphicsUnit TextStyle_FontUnit
			{
				get { return this.ItemStyle.TextStyle.Font.Unit; }
				set
				{
					Font fnt = this.ItemStyle.TextStyle.Font;
					this.ItemStyle.TextStyle.Font = new Font( fnt.FontFamily, fnt.Size, fnt.Style, value );
				}
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" ), System.Xml.Serialization.XmlIgnore()]
			public Color TextStyle_BackColor
			{
				get { return this.ItemStyle.TextStyle.BackColor; }
				set { this.ItemStyle.TextStyle.BackColor = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public int TextStyle_BackColorArgb
			{
				get { return this.ItemStyle.TextStyle.BackColor.ToArgb(); }
				set { this.ItemStyle.TextStyle.BackColor = Color.FromArgb( value ); }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" ), System.Xml.Serialization.XmlIgnore()]
			public Color TextStyle_ForeColor
			{
				get { return this.ItemStyle.TextStyle.ForeColor; }
				set { this.ItemStyle.TextStyle.ForeColor = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public int TextStyle_ForeColorArgb
			{
				get { return this.ItemStyle.TextStyle.ForeColor.ToArgb(); }
				set { this.ItemStyle.TextStyle.ForeColor = Color.FromArgb( value ); }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public StringAlignment TextFormat_StringAlignment
			{
				get { return this.ItemStyle.TextStyle.StringFormat.Alignment; }
				set { this.ItemStyle.TextStyle.StringFormat.Alignment = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public StringAlignment TextFormat_LineAlignment
			{
				get { return this.ItemStyle.TextStyle.StringFormat.LineAlignment; }
				set { this.ItemStyle.TextStyle.StringFormat.LineAlignment = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public StringTrimming TextFormat_Trimming
			{
				get { return this.ItemStyle.TextStyle.StringFormat.Trimming; }
				set { this.ItemStyle.TextStyle.StringFormat.Trimming = value; }
			}

			//-----------------------------------------------------
			[System.ComponentModel.Category( "Text" )]
			public StringFormatFlags TextFormat_FormatFlags
			{
				get { return this.ItemStyle.TextStyle.StringFormat.FormatFlags; }
				set { this.ItemStyle.TextStyle.StringFormat.FormatFlags = value; }
			}


		}


	}
}
