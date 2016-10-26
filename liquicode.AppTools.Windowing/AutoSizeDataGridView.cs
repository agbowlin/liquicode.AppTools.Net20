

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;


namespace liquicode.AppTools
{
	public class AutoSizeDataGridView : DataGridView
	{


		//=====================================================================
		public bool HasFixedWidth { get; set; }
		public bool HasFixedHeight { get; set; }


		//=====================================================================
		public bool HideSelection
		{
			get
			{
				return (this.DefaultCellStyle.SelectionBackColor == this.DefaultCellStyle.BackColor);
			}
			set
			{
				if( value )
				{
					this.DefaultCellStyle.SelectionBackColor = this.DefaultCellStyle.BackColor;
					this.DefaultCellStyle.SelectionForeColor = this.DefaultCellStyle.ForeColor;
				}
				else
				{
					this.DefaultCellStyle.SelectionBackColor = Color.AliceBlue;
					this.DefaultCellStyle.SelectionForeColor = Color.Black;
				}
				return;
			}
		}


		//=====================================================================
		public AutoSizeDataGridView()
			: base()
		{
			return;
		}


		//=====================================================================
		public class LayoutUpdated_EventArgs : EventArgs
		{
			public Size ControlSize = new Size();
			public LayoutUpdated_EventArgs( Size NewControlSize )
			{
				this.ControlSize = NewControlSize;
				return;
			}
		}
		public delegate void LayoutUpdated_EventHandler( object sender, LayoutUpdated_EventArgs e );
		public event LayoutUpdated_EventHandler LayoutUpdated = null;
		public virtual void Fire_LayoutUpdated( LayoutUpdated_EventArgs e )
		{
			if( this.LayoutUpdated == null ) { return; }
			this.LayoutUpdated( this, e );
			return;
		}


		//=====================================================================
		public Size AutoSizeLayout()
		{
			//------------------------------------------
			// Calculate the control width.
			//------------------------------------------

			int control_width = 0;

			// Involve the control border.
			if( this.BorderStyle == BorderStyle.FixedSingle )
			{
				control_width += 2;
			}
			else if( this.BorderStyle == BorderStyle.Fixed3D )
			{
				control_width += 2;
			}

			// Involve the row headers.
			if( this.RowHeadersVisible )
			{
				control_width += this.RowHeadersWidth;
			}

			// Involve the data columns.
			if( this.HasFixedWidth )
			{
				if( this.ColumnCount > 0 )
				{
					// Fit all columns into the existing width.
					// - AutoSize the leftward columns.
					for( int index = 0; index < (this.ColumnCount - 1); index++ )
					{
						this.AutoResizeColumn( index, DataGridViewAutoSizeColumnMode.AllCells );
						control_width += this.Columns[ index ].Width;
					}
					// - Fill the last column.
					this.Columns[ this.ColumnCount - 1 ].Width = this.ClientSize.Width - control_width;
				}
			}
			else
			{
				// AutoSize all columns and set the control width.
				this.AutoResizeColumns( DataGridViewAutoSizeColumnsMode.AllCells, this.HasFixedHeight );
				foreach( DataGridViewColumn column in this.Columns )
				{
					//this.AutoResizeColumn( column.Index, DataGridViewAutoSizeColumnMode.AllCells );
					control_width += this.Columns[ column.Index ].Width;
				}
				this.Width = control_width;
			}

			//------------------------------------------
			// Calculate the control height.
			//------------------------------------------

			int control_height = 0;

			// Involve the control border.
			if( this.BorderStyle == BorderStyle.FixedSingle )
			{
				control_height += 2;
			}
			else if( this.BorderStyle == BorderStyle.Fixed3D )
			{
				control_height += 2;
			}

			// Involve the column headers.
			if( this.ColumnHeadersVisible )
			{
				control_height += this.ColumnHeadersHeight;
			}

			// Involve the data rows.
			if( this.HasFixedHeight )
			{
				// Fit all rows into existing height.
				//TODO:?
			}
			else
			{
				// Fit control height to existing rows.
				if( this.Rows.Count > 0 )
				{
					this.UpdateRowHeightInfo( 0, true );
					foreach( DataGridViewRow row in this.Rows )
					{
						control_height += row.Height;
					}
				}

				this.Height = control_height;
			}

			//------------------------------------------
			// Return the control size.
			//------------------------------------------

			Size new_size = new Size( control_width, control_height );
			this.Fire_LayoutUpdated( new LayoutUpdated_EventArgs( new_size ) );
			return new_size;
		}


		//=====================================================================
		protected override void OnResize( EventArgs e )
		{
			this.AutoSizeLayout();
			base.OnResize( e );
			return;
		}
		//protected override void  OnRowHeadersBorderStyleChanged(EventArgs e)
		//{
		//    this.ResizeAndlayout();
		//    return;
		//}
		protected override void OnRowHeadersWidthChanged( EventArgs e )
		{
			this.AutoSizeLayout();
			return;
		}
		protected override void OnColumnHeadersHeightChanged( EventArgs e )
		{
			this.AutoSizeLayout();
			return;
		}
		protected override void OnRowHeightChanged( DataGridViewRowEventArgs e )
		{
			this.AutoSizeLayout();
			return;
		}
		protected override void OnRowsAdded( DataGridViewRowsAddedEventArgs e )
		{
			this.AutoSizeLayout();
			return;
		}
		protected override void OnRowsRemoved( DataGridViewRowsRemovedEventArgs e )
		{
			this.AutoSizeLayout();
			return;
		}
		protected override void OnCellEndEdit( DataGridViewCellEventArgs e )
		{
			this.AutoSizeLayout();
			return;
		}


	}
}
