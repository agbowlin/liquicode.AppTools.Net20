namespace liquicode.AppTools._FormTests
{
	partial class TransformImage_Test
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.ListView1 = new System.Windows.Forms.ListView();
			this.ImageList1 = new System.Windows.Forms.ImageList( this.components );
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mnuOriginal = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRotate = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFlip = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFade = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuBrightness = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDepth = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ListView1
			// 
			this.ListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListView1.Location = new System.Drawing.Point( 0, 24 );
			this.ListView1.Name = "ListView1";
			this.ListView1.Size = new System.Drawing.Size( 825, 529 );
			this.ListView1.TabIndex = 0;
			this.ListView1.UseCompatibleStateImageBehavior = false;
			// 
			// ImageList1
			// 
			this.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.ImageList1.ImageSize = new System.Drawing.Size( 16, 16 );
			this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.mnuOriginal,
            this.mnuRotate,
            this.mnuFlip,
            this.mnuFade,
            this.mnuBrightness,
            this.mnuDepth} );
			this.menuStrip1.Location = new System.Drawing.Point( 0, 0 );
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size( 825, 24 );
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// mnuOriginal
			// 
			this.mnuOriginal.Name = "mnuOriginal";
			this.mnuOriginal.Size = new System.Drawing.Size( 75, 20 );
			this.mnuOriginal.Text = "[ Original ]";
			this.mnuOriginal.Click += new System.EventHandler( this.mnuOriginal_Click );
			// 
			// mnuRotate
			// 
			this.mnuRotate.Name = "mnuRotate";
			this.mnuRotate.Size = new System.Drawing.Size( 67, 20 );
			this.mnuRotate.Text = "[ Rotate ]";
			this.mnuRotate.Click += new System.EventHandler( this.mnuRotate_Click );
			// 
			// mnuFlip
			// 
			this.mnuFlip.Name = "mnuFlip";
			this.mnuFlip.Size = new System.Drawing.Size( 52, 20 );
			this.mnuFlip.Text = "[ Flip ]";
			this.mnuFlip.Click += new System.EventHandler( this.mnuFlip_Click );
			// 
			// mnuFade
			// 
			this.mnuFade.Name = "mnuFade";
			this.mnuFade.Size = new System.Drawing.Size( 58, 20 );
			this.mnuFade.Text = "[ Fade ]";
			this.mnuFade.Click += new System.EventHandler( this.mnuFade_Click );
			// 
			// mnuBrightness
			// 
			this.mnuBrightness.Name = "mnuBrightness";
			this.mnuBrightness.Size = new System.Drawing.Size( 88, 20 );
			this.mnuBrightness.Text = "[ Brightness ]";
			this.mnuBrightness.Click += new System.EventHandler( this.mnuBrightness_Click );
			// 
			// mnuDepth
			// 
			this.mnuDepth.Name = "mnuDepth";
			this.mnuDepth.Size = new System.Drawing.Size( 65, 20 );
			this.mnuDepth.Text = "[ Depth ]";
			this.mnuDepth.Click += new System.EventHandler( this.mnuDepth_Click );
			// 
			// TransformImage_Test
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 825, 553 );
			this.Controls.Add( this.ListView1 );
			this.Controls.Add( this.menuStrip1 );
			this.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "TransformImage_Test";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TransformImage Test";
			this.menuStrip1.ResumeLayout( false );
			this.menuStrip1.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView ListView1;
		private System.Windows.Forms.ImageList ImageList1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuOriginal;
		private System.Windows.Forms.ToolStripMenuItem mnuRotate;
		private System.Windows.Forms.ToolStripMenuItem mnuFlip;
		private System.Windows.Forms.ToolStripMenuItem mnuFade;
		private System.Windows.Forms.ToolStripMenuItem mnuBrightness;
		private System.Windows.Forms.ToolStripMenuItem mnuDepth;
	}
}