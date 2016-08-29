

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace liquicode.AppTools
{
	public class CaptionTextBox
		: CaptionContainerControl
	{

		//---------------------------------------------------------------------
		private TextBox _TextBox = null;


		//---------------------------------------------------------------------
		public CaptionTextBox()
		{
			this._TextBox = new TextBox();
			this._TextBox.Dock = DockStyle.Fill;
			this.WorkingArea.Controls.Add( this._TextBox );
			this._TextBox.TextChanged += new EventHandler( _TextBox_TextChanged );
			return;
		}


		//---------------------------------------------------------------------
		public string Value
		{
			get { return this.TextBoxText; }
			set { this.TextBoxText = value; }
		}


		//=====================================================================
		//		TextBox Properties
		//=====================================================================


		//---------------------------------------------------------------------
		public string TextBoxText
		{
			get { return this._TextBox.Text; }
			set { this._TextBox.Text = value; }
		}


		//---------------------------------------------------------------------
		public bool TextBoxReadOnly
		{
			get { return this._TextBox.ReadOnly; }
			set { this._TextBox.ReadOnly = value; }
		}


		//---------------------------------------------------------------------
		public bool TextBoxMultiline
		{
			get { return this._TextBox.Multiline; }
			set { this._TextBox.Multiline = value; }
		}


		//---------------------------------------------------------------------
		public bool TextBoxWordWrap
		{
			get { return this._TextBox.WordWrap; }
			set { this._TextBox.WordWrap = value; }
		}


		//---------------------------------------------------------------------
		public ScrollBars TextBoxScrollBars
		{
			get { return this._TextBox.ScrollBars; }
			set { this._TextBox.ScrollBars = value; }
		}


		//=====================================================================
		//		Events
		//=====================================================================


		//---------------------------------------------------------------------
		public delegate void OnValueChangedEventHandler( object sender, EventArgs e );
		public event OnValueChangedEventHandler OnValueChanged;


		//---------------------------------------------------------------------
		public void RaiseValueChangedEvent( object sender, EventArgs e )
		{
			if( this.OnValueChanged != null )
			{
				this.OnValueChanged( this, e );
			}
			return;
		}


		//---------------------------------------------------------------------
		void _TextBox_TextChanged( object sender, EventArgs e )
		{
			this.RaiseValueChangedEvent( sender, e );
			return;
		}


	}
}
