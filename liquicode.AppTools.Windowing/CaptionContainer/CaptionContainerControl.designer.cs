namespace liquicode.AppTools
{
	partial class CaptionContainerControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.lblCaption = new System.Windows.Forms.Label();
			this.pnlWorkingArea = new liquicode.AppTools.ContainerControl();
			this.pnlHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
			this.pnlHeader.Controls.Add( this.lblCaption );
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlHeader.Location = new System.Drawing.Point( 0, 0 );
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size( 100, 24 );
			this.pnlHeader.TabIndex = 0;
			// 
			// lblCaption
			// 
			this.lblCaption.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblCaption.Location = new System.Drawing.Point( 0, 0 );
			this.lblCaption.Name = "lblCaption";
			this.lblCaption.Size = new System.Drawing.Size( 100, 24 );
			this.lblCaption.TabIndex = 0;
			this.lblCaption.Text = "Caption";
			this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlWorkingArea
			// 
			this.pnlWorkingArea.BackColor = System.Drawing.Color.Transparent;
			this.pnlWorkingArea.Location = new System.Drawing.Point( 100, 0 );
			this.pnlWorkingArea.Name = "pnlWorkingArea";
			this.pnlWorkingArea.Size = new System.Drawing.Size( 200, 24 );
			this.pnlWorkingArea.TabIndex = 1;
			// 
			// CaptionContainerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add( this.pnlWorkingArea );
			this.Controls.Add( this.pnlHeader );
			this.Name = "CaptionContainerControl";
			this.Size = new System.Drawing.Size( 300, 24 );
			this.pnlHeader.ResumeLayout( false );
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Label lblCaption;
		private ContainerControl pnlWorkingArea;
	}
}
