

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


// Adapted from CodeProject article at: http://www.codeproject.com/KB/miscctrl/NestedControlDesigner.aspx

namespace liquicode.AppTools
{


	//=====================================================================
	[Designer( typeof( liquicode.AppTools.Designers.CaptionContainerDesigner ) )]
	public partial class CaptionContainerControl
							: UserControl
	{


		//---------------------------------------------------------------------
		public CaptionContainerControl()
		{
			this.SetStyle( ControlStyles.SupportsTransparentBackColor, true );
			this.BackColor = Color.Transparent;
			this.InitializeComponent();
			return;
		}


		//=====================================================================
		//		Caption Properties
		//=====================================================================

		//---------------------------------------------------------------------
		public string CaptionText
		{
			get { return this.lblCaption.Text; }
			set { this.lblCaption.Text = value; }
		}


		//---------------------------------------------------------------------
		public bool CaptionBold
		{
			get { return this.lblCaption.Font.Bold; }
			set
			{
				if( value )
				{ this.lblCaption.Font = new Font( this.lblCaption.Font, FontStyle.Bold ); }
				else
				{ this.lblCaption.Font = new Font( this.lblCaption.Font, FontStyle.Regular ); }
				return;
			}
		}


		//---------------------------------------------------------------------
		public Color CaptionBackcolor
		{
			get { return this.lblCaption.BackColor; }
			set { this.lblCaption.BackColor = value; }
		}


		//---------------------------------------------------------------------
		public BorderStyle CaptionBorderStyle
		{
			get { return this.lblCaption.BorderStyle; }
			set { this.lblCaption.BorderStyle = value; }
		}


		//---------------------------------------------------------------------
		public ContentAlignment CaptionTextAlign
		{
			get { return this.lblCaption.TextAlign; }
			set { this.lblCaption.TextAlign = value; }
		}


		//---------------------------------------------------------------------
		public Size CaptionSize
		{
			get { return this.pnlHeader.Size; }
			set { this.pnlHeader.Size = value; }
		}


		//---------------------------------------------------------------------
		public DockStyle CaptionDock
		{
			get { return this.pnlHeader.Dock; }
			set { this.pnlHeader.Dock = value; }
		}


		////---------------------------------------------------------------------
		//private Orientation _Orientation = Orientation.Horizontal;
		//public Orientation Orientation
		//{
		//    get { return this._Orientation; }
		//    set
		//    {
		//        this._Orientation = value;
		//        if( this._Orientation == Orientation.Horizontal )
		//        {
		//            this.pnlHeader.Dock = DockStyle.Left;
		//        }
		//        else
		//        {
		//            this.pnlHeader.Dock = DockStyle.Top;
		//        }
		//    }
		//}


		//=====================================================================
		//		Container Properties
		//=====================================================================


		//---------------------------------------------------------------------
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
		public Panel WorkingArea
		{
			get { return this.pnlWorkingArea; }
		}


	}
}
