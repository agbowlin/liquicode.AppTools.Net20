namespace liquicode.AppTools
{
	partial class MessageWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MessageWindow ) );
			this.CommandPanel = new System.Windows.Forms.Panel();
			this.lblMessage = new System.Windows.Forms.Label();
			this.cmdMoreInfo = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdRetry = new System.Windows.Forms.Button();
			this.cmdYes = new System.Windows.Forms.Button();
			this.cmdAbort = new System.Windows.Forms.Button();
			this.cmdIgnore = new System.Windows.Forms.Button();
			this.cmdNo = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.lblIconQuestion = new System.Windows.Forms.Label();
			this.lblIconWarning = new System.Windows.Forms.Label();
			this.lblIconInformation = new System.Windows.Forms.Label();
			this.lblIconError = new System.Windows.Forms.Label();
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.txtUserInput = new System.Windows.Forms.TextBox();
			this.pnlUserInput = new System.Windows.Forms.Label();
			this.cboUserInput = new System.Windows.Forms.ComboBox();
			this.lblUserInput = new System.Windows.Forms.Label();
			this.CommandPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
			this.pnlUserInput.SuspendLayout();
			this.SuspendLayout();
			// 
			// CommandPanel
			// 
			this.CommandPanel.Controls.Add( this.lblMessage );
			this.CommandPanel.Controls.Add( this.cmdMoreInfo );
			this.CommandPanel.Controls.Add( this.cmdCancel );
			this.CommandPanel.Controls.Add( this.cmdRetry );
			this.CommandPanel.Controls.Add( this.cmdYes );
			this.CommandPanel.Controls.Add( this.cmdAbort );
			this.CommandPanel.Controls.Add( this.cmdIgnore );
			this.CommandPanel.Controls.Add( this.cmdNo );
			this.CommandPanel.Controls.Add( this.cmdOK );
			this.CommandPanel.Controls.Add( this.lblIconQuestion );
			this.CommandPanel.Controls.Add( this.lblIconWarning );
			this.CommandPanel.Controls.Add( this.lblIconInformation );
			this.CommandPanel.Controls.Add( this.lblIconError );
			this.CommandPanel.Controls.Add( this.picIcon );
			this.CommandPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.CommandPanel.Location = new System.Drawing.Point( 0, 80 );
			this.CommandPanel.Name = "CommandPanel";
			this.CommandPanel.Size = new System.Drawing.Size( 451, 54 );
			this.CommandPanel.TabIndex = 0;
			// 
			// lblMessage
			// 
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new System.Drawing.Point( 103, 9 );
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size( 36, 39 );
			this.lblMessage.TabIndex = 2;
			this.lblMessage.Text = "Line 1\r\nLine 2\r\nLine 3";
			this.lblMessage.Visible = false;
			// 
			// cmdMoreInfo
			// 
			this.cmdMoreInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdMoreInfo.Location = new System.Drawing.Point( 130, 28 );
			this.cmdMoreInfo.Name = "cmdMoreInfo";
			this.cmdMoreInfo.Size = new System.Drawing.Size( 75, 23 );
			this.cmdMoreInfo.TabIndex = 10;
			this.cmdMoreInfo.Text = "&More Info";
			this.cmdMoreInfo.Visible = false;
			this.cmdMoreInfo.Click += new System.EventHandler( this.cmdMoreInfo_Click );
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Location = new System.Drawing.Point( 373, 29 );
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size( 75, 23 );
			this.cmdCancel.TabIndex = 6;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.Visible = false;
			this.cmdCancel.Click += new System.EventHandler( this.cmdCancel_Click );
			// 
			// cmdRetry
			// 
			this.cmdRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdRetry.Location = new System.Drawing.Point( 292, 28 );
			this.cmdRetry.Name = "cmdRetry";
			this.cmdRetry.Size = new System.Drawing.Size( 75, 23 );
			this.cmdRetry.TabIndex = 4;
			this.cmdRetry.Text = "&Retry";
			this.cmdRetry.Visible = false;
			this.cmdRetry.Click += new System.EventHandler( this.cmdRetry_Click );
			// 
			// cmdYes
			// 
			this.cmdYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdYes.Location = new System.Drawing.Point( 211, 28 );
			this.cmdYes.Name = "cmdYes";
			this.cmdYes.Size = new System.Drawing.Size( 75, 23 );
			this.cmdYes.TabIndex = 2;
			this.cmdYes.Text = "&Yes";
			this.cmdYes.Visible = false;
			this.cmdYes.Click += new System.EventHandler( this.cmdYes_Click );
			// 
			// cmdAbort
			// 
			this.cmdAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAbort.Location = new System.Drawing.Point( 373, 3 );
			this.cmdAbort.Name = "cmdAbort";
			this.cmdAbort.Size = new System.Drawing.Size( 75, 23 );
			this.cmdAbort.TabIndex = 5;
			this.cmdAbort.Text = "&Abort";
			this.cmdAbort.Visible = false;
			this.cmdAbort.Click += new System.EventHandler( this.cmdAbort_Click );
			// 
			// cmdIgnore
			// 
			this.cmdIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdIgnore.Location = new System.Drawing.Point( 292, 3 );
			this.cmdIgnore.Name = "cmdIgnore";
			this.cmdIgnore.Size = new System.Drawing.Size( 75, 23 );
			this.cmdIgnore.TabIndex = 3;
			this.cmdIgnore.Text = "&Ignore";
			this.cmdIgnore.Visible = false;
			this.cmdIgnore.Click += new System.EventHandler( this.cmdIgnore_Click );
			// 
			// cmdNo
			// 
			this.cmdNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdNo.Location = new System.Drawing.Point( 211, 3 );
			this.cmdNo.Name = "cmdNo";
			this.cmdNo.Size = new System.Drawing.Size( 75, 23 );
			this.cmdNo.TabIndex = 1;
			this.cmdNo.Text = "&No";
			this.cmdNo.Visible = false;
			this.cmdNo.Click += new System.EventHandler( this.cmdNo_Click );
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Location = new System.Drawing.Point( 130, 3 );
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size( 75, 23 );
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "&OK";
			this.cmdOK.Visible = false;
			this.cmdOK.Click += new System.EventHandler( this.cmdOK_Click );
			// 
			// lblIconQuestion
			// 
			this.lblIconQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblIconQuestion.Location = new System.Drawing.Point( 57, 48 );
			this.lblIconQuestion.Name = "lblIconQuestion";
			this.lblIconQuestion.Size = new System.Drawing.Size( 40, 15 );
			this.lblIconQuestion.TabIndex = 14;
			this.lblIconQuestion.Text = resources.GetString( "lblIconQuestion.Text" );
			this.lblIconQuestion.Visible = false;
			// 
			// lblIconWarning
			// 
			this.lblIconWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblIconWarning.Location = new System.Drawing.Point( 57, 33 );
			this.lblIconWarning.Name = "lblIconWarning";
			this.lblIconWarning.Size = new System.Drawing.Size( 40, 15 );
			this.lblIconWarning.TabIndex = 13;
			this.lblIconWarning.Text = resources.GetString( "lblIconWarning.Text" );
			this.lblIconWarning.Visible = false;
			// 
			// lblIconInformation
			// 
			this.lblIconInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblIconInformation.Location = new System.Drawing.Point( 57, 18 );
			this.lblIconInformation.Name = "lblIconInformation";
			this.lblIconInformation.Size = new System.Drawing.Size( 40, 15 );
			this.lblIconInformation.TabIndex = 12;
			this.lblIconInformation.Text = resources.GetString( "lblIconInformation.Text" );
			this.lblIconInformation.Visible = false;
			// 
			// lblIconError
			// 
			this.lblIconError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblIconError.Location = new System.Drawing.Point( 57, 3 );
			this.lblIconError.Name = "lblIconError";
			this.lblIconError.Size = new System.Drawing.Size( 40, 15 );
			this.lblIconError.TabIndex = 11;
			this.lblIconError.Text = resources.GetString( "lblIconError.Text" );
			this.lblIconError.Visible = false;
			// 
			// picIcon
			// 
			this.picIcon.BackColor = System.Drawing.Color.Transparent;
			this.picIcon.Location = new System.Drawing.Point( 3, 3 );
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size( 48, 48 );
			this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picIcon.TabIndex = 10;
			this.picIcon.TabStop = false;
			// 
			// txtMessage
			// 
			this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtMessage.HideSelection = false;
			this.txtMessage.Location = new System.Drawing.Point( 0, 0 );
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ReadOnly = true;
			this.txtMessage.Size = new System.Drawing.Size( 451, 58 );
			this.txtMessage.TabIndex = 1;
			this.txtMessage.Text = "Line 1\r\nLine 2\r\nLine 3";
			this.txtMessage.WordWrap = false;
			// 
			// txtUserInput
			// 
			this.txtUserInput.Location = new System.Drawing.Point( 188, 1 );
			this.txtUserInput.Name = "txtUserInput";
			this.txtUserInput.Size = new System.Drawing.Size( 131, 20 );
			this.txtUserInput.TabIndex = 0;
			// 
			// pnlUserInput
			// 
			this.pnlUserInput.Controls.Add( this.cboUserInput );
			this.pnlUserInput.Controls.Add( this.txtUserInput );
			this.pnlUserInput.Controls.Add( this.lblUserInput );
			this.pnlUserInput.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlUserInput.Location = new System.Drawing.Point( 0, 58 );
			this.pnlUserInput.Name = "pnlUserInput";
			this.pnlUserInput.Size = new System.Drawing.Size( 451, 22 );
			this.pnlUserInput.TabIndex = 2;
			this.pnlUserInput.Visible = false;
			// 
			// cboUserInput
			// 
			this.cboUserInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboUserInput.FormattingEnabled = true;
			this.cboUserInput.Location = new System.Drawing.Point( 325, 1 );
			this.cboUserInput.Name = "cboUserInput";
			this.cboUserInput.Size = new System.Drawing.Size( 114, 21 );
			this.cboUserInput.TabIndex = 2;
			// 
			// lblUserInput
			// 
			this.lblUserInput.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblUserInput.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
			this.lblUserInput.Location = new System.Drawing.Point( 0, 0 );
			this.lblUserInput.Name = "lblUserInput";
			this.lblUserInput.Size = new System.Drawing.Size( 181, 22 );
			this.lblUserInput.TabIndex = 1;
			this.lblUserInput.Text = "Please supply your answer :";
			// 
			// MessageWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 451, 134 );
			this.Controls.Add( this.txtMessage );
			this.Controls.Add( this.pnlUserInput );
			this.Controls.Add( this.CommandPanel );
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MessageWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Message";
			this.CommandPanel.ResumeLayout( false );
			this.CommandPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
			this.pnlUserInput.ResumeLayout( false );
			this.pnlUserInput.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel CommandPanel;
		private System.Windows.Forms.Button cmdOK;
		internal System.Windows.Forms.Label lblIconQuestion;
		internal System.Windows.Forms.Label lblIconWarning;
		internal System.Windows.Forms.Label lblIconInformation;
		internal System.Windows.Forms.Label lblIconError;
		internal System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdRetry;
		private System.Windows.Forms.Button cmdYes;
		private System.Windows.Forms.Button cmdAbort;
		private System.Windows.Forms.Button cmdIgnore;
		private System.Windows.Forms.Button cmdNo;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Button cmdMoreInfo;
		private System.Windows.Forms.TextBox txtUserInput;
		private System.Windows.Forms.Label pnlUserInput;
		private System.Windows.Forms.Label lblUserInput;
		private System.Windows.Forms.ComboBox cboUserInput;
	}
}

