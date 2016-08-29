

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace liquicode.AppTools
{
	public class CaptionComboBox
		: CaptionContainerControl
	{

		//---------------------------------------------------------------------
		private ComboBox _ComboBox = null;


		//---------------------------------------------------------------------
		public CaptionComboBox()
		{
			this._ComboBox = new ComboBox();
			this._ComboBox.Dock = DockStyle.Fill;
			this.WorkingArea.Controls.Add( this._ComboBox );
			this._ComboBox.SelectionChangeCommitted += new EventHandler( _ComboBox_SelectionChangeCommitted );
			return;
		}


		//---------------------------------------------------------------------
		public string Value
		{
			get
			{
				if( this.ComboBoxSelectedItem == null )
				{ return ""; }
				else
				{ return this.ComboBoxSelectedItem.ToString(); }
			}
			set
			{
				foreach( object item in this.ComboBoxItems )
				{
					if( value == item.ToString() )
					{
						this.ComboBoxSelectedItem = item;
					}
				}
				return;
			}
		}


		//=====================================================================
		//		ComboBox Properties
		//=====================================================================

		//---------------------------------------------------------------------
		public bool ComboBoxEnabled
		{
			get { return this._ComboBox.Enabled; }
			set { this._ComboBox.Enabled = value; }
		}


		//---------------------------------------------------------------------
		public ComboBoxStyle ComboBoxStyle
		{
			get { return this._ComboBox.DropDownStyle; }
			set { this._ComboBox.DropDownStyle = value; }
		}


		//---------------------------------------------------------------------
		public ComboBox.ObjectCollection ComboBoxItems
		{
			get { return this._ComboBox.Items; }
		}


		//---------------------------------------------------------------------
		public string ComboBoxText
		{
			get { return this._ComboBox.Text; }
			set { this._ComboBox.Text = value; }
		}


		//---------------------------------------------------------------------
		public int ComboBoxSelectedIndex
		{
			get { return this._ComboBox.SelectedIndex; }
			set { this._ComboBox.SelectedIndex = value; }
		}


		//---------------------------------------------------------------------
		public object ComboBoxSelectedItem
		{
			get { return this._ComboBox.SelectedItem; }
			set { this._ComboBox.SelectedItem = value; }
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
		void _ComboBox_SelectionChangeCommitted( object sender, EventArgs e )
		{
			this.RaiseValueChangedEvent( sender, e );
			return;
		}


	}
}
