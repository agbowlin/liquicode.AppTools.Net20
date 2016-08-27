

using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

using System.ComponentModel;
using System.Windows.Forms;


namespace liquicode.AppTools
{
	public partial class MessageWindow : Form
	{


		//--------------------------------------------------------------------
		public class MessageWindowParameters
		{
			public IWin32Window Owner = null;
			public string Text = "";
			public string Caption = "Message";
			public MessageBoxButtons Buttons = MessageBoxButtons.OK;
			public MessageBoxIcon Icon = MessageBoxIcon.None;
			public MessageBoxDefaultButton DefaultButton = MessageBoxDefaultButton.Button1;
			public MessageBoxOptions Options = MessageBoxOptions.ServiceNotification;
			public Exception Exception = null;
			public bool GetUserInput = false;
			public string[] UserChoices = { };
		}


		//--------------------------------------------------------------------
		private MessageWindowParameters _Parameters = null;


		//--------------------------------------------------------------------
		public MessageWindow()
		{
			InitializeComponent();
			return;
		}


		//--------------------------------------------------------------------
		public DialogResult Show( MessageWindowParameters Parameters )
		{
			this._Parameters = Parameters;

			// Set Window Title
			if( Parameters.Caption.Length == 0 )
			{
				if( Parameters.Icon.Equals( MessageBoxIcon.None ) )
				{ this.Text = "Message"; }
				else
				{ this.Text = Parameters.Icon.ToString(); }
			}
			else
			{
				this.Text = Parameters.Caption;
			}

			// Set Text
			string text = Parameters.Text;
			if( string.IsNullOrEmpty( text ) ) { text = ""; }
			text = text.Replace( "\r\n", "\n" );
			text = text.Replace( "\n", "\r\n" );
			this.lblMessage.Text = text;
			this.txtMessage.Text = text;

			// Get Client Size
			Size ClientSize = this.lblMessage.GetPreferredSize( this.lblMessage.Size );
			int ButtonWidth = this.cmdOK.Width + this.cmdOK.Margin.Horizontal;
			int IconHeight = this.picIcon.Size.Height + this.picIcon.Margin.Vertical;
			if( Parameters.GetUserInput )
			{
				this.pnlUserInput.Visible = true;
				ClientSize.Height += this.pnlUserInput.Height;
				if( Parameters.UserChoices.Length == 0 )
				{
					this.txtUserInput.Dock = DockStyle.Fill;
					this.txtUserInput.Visible = true;
					this.cboUserInput.Dock = DockStyle.None;
					this.cboUserInput.Visible = false;
				}
				else
				{
					this.txtUserInput.Dock = DockStyle.None;
					this.txtUserInput.Visible = false;
					this.cboUserInput.Dock = DockStyle.Fill;
					this.cboUserInput.Visible = true;
					this.cboUserInput.Items.AddRange( Parameters.UserChoices );
				}
			}
			ClientSize.Height += this.CommandPanel.Height + this.CommandPanel.Margin.Vertical;
			ClientSize.Height += 50; // Fudge factor.

			// Get minimum size.
			int min_width = (3 * ButtonWidth);
			if( Parameters.Icon == MessageBoxIcon.None )
			{
				this.picIcon.Visible = false;
			}
			else
			{
				this.picIcon.Visible = true;
				min_width += this.picIcon.Width + this.picIcon.Margin.Horizontal;
			}
			if( ClientSize.Width < min_width )
			{
				ClientSize.Width = min_width;
			}

			// Set Window Size
			int max_width = (int)(Screen.PrimaryScreen.Bounds.Width * 0.5);
			int max_height = (int)(Screen.PrimaryScreen.Bounds.Height * 0.75);
			Size WindowSize = this.SizeFromClientSize( ClientSize );
			if( WindowSize.Width > max_width )
			{
				WindowSize.Width = max_width;
			}
			if( WindowSize.Height > max_height )
			{
				WindowSize.Height = max_height;
			}
			this.Size = WindowSize;

			// Set Buttons
			int X = this.CommandPanel.Width;
			int Y = Convert.ToInt32( (this.CommandPanel.Height / 2) - (this.cmdOK.Height / 2) );
			switch( Parameters.Buttons )
			{

				case MessageBoxButtons.OK:

					X -= ButtonWidth;
					this.cmdOK.Location = new Point( X, Y );
					this.cmdOK.Visible = true;

					this.AcceptButton = this.cmdOK;
					break;

				case MessageBoxButtons.OKCancel:

					X -= ButtonWidth;
					this.cmdCancel.Location = new Point( X, Y );
					this.cmdCancel.Visible = true;
					X -= ButtonWidth;
					this.cmdOK.Location = new Point( X, Y );
					this.cmdOK.Visible = true;

					this.AcceptButton = this.cmdOK;
					this.CancelButton = this.cmdCancel;
					break;

				case MessageBoxButtons.AbortRetryIgnore:

					X -= ButtonWidth;
					this.cmdIgnore.Location = new Point( X, Y );
					this.cmdIgnore.Visible = true;
					X -= ButtonWidth;
					this.cmdRetry.Location = new Point( X, Y );
					this.cmdRetry.Visible = true;
					X -= ButtonWidth;
					this.cmdAbort.Location = new Point( X, Y );
					this.cmdAbort.Visible = true;

					if( Parameters.DefaultButton == MessageBoxDefaultButton.Button1 )
					{
						this.AcceptButton = this.cmdAbort;
						this.CancelButton = this.cmdIgnore;
					}
					else if( Parameters.DefaultButton == MessageBoxDefaultButton.Button2 )
					{
						this.AcceptButton = this.cmdRetry;
						this.CancelButton = this.cmdIgnore;
					}
					else if( Parameters.DefaultButton == MessageBoxDefaultButton.Button3 )
					{
						this.AcceptButton = this.cmdIgnore;
					}
					break;

				case MessageBoxButtons.YesNoCancel:

					X -= ButtonWidth;
					this.cmdCancel.Location = new Point( X, Y );
					this.cmdCancel.Visible = true;
					X -= ButtonWidth;
					this.cmdNo.Location = new Point( X, Y );
					this.cmdNo.Visible = true;
					X -= ButtonWidth;
					this.cmdYes.Location = new Point( X, Y );
					this.cmdYes.Visible = true;

					if( Parameters.DefaultButton == MessageBoxDefaultButton.Button1 )
					{
						this.AcceptButton = this.cmdYes;
						this.CancelButton = this.cmdCancel;
					}
					else if( Parameters.DefaultButton == MessageBoxDefaultButton.Button2 )
					{
						this.AcceptButton = this.cmdNo;
						this.CancelButton = this.cmdCancel;
					}
					else if( Parameters.DefaultButton == MessageBoxDefaultButton.Button3 )
					{
						this.AcceptButton = this.cmdCancel;
					}
					break;

				case MessageBoxButtons.YesNo:

					X -= ButtonWidth;
					this.cmdNo.Location = new Point( X, Y );
					this.cmdNo.Visible = true;
					X -= ButtonWidth;
					this.cmdYes.Location = new Point( X, Y );
					this.cmdYes.Visible = true;

					if( Parameters.DefaultButton == MessageBoxDefaultButton.Button1 )
					{
						this.AcceptButton = this.cmdYes;
						this.CancelButton = this.cmdNo;
					}
					else if( Parameters.DefaultButton == MessageBoxDefaultButton.Button2 )
					{
						this.AcceptButton = this.cmdNo;
						this.CancelButton = this.cmdYes;
					}
					else if( Parameters.DefaultButton == MessageBoxDefaultButton.Button3 )
					{
						this.AcceptButton = this.cmdYes;
						this.CancelButton = this.cmdNo;
					}
					break;

				case MessageBoxButtons.RetryCancel:

					X -= ButtonWidth;
					this.cmdCancel.Location = new Point( X, Y );
					this.cmdCancel.Visible = true;
					X -= ButtonWidth;
					this.cmdRetry.Location = new Point( X, Y );
					this.cmdRetry.Visible = true;

					if( Parameters.DefaultButton == MessageBoxDefaultButton.Button1 )
					{
						this.AcceptButton = this.cmdRetry;
						this.CancelButton = this.cmdCancel;
					}
					else if( Parameters.DefaultButton == MessageBoxDefaultButton.Button2 )
					{
						this.AcceptButton = this.cmdCancel;
						this.CancelButton = this.cmdRetry;
					}
					else if( Parameters.DefaultButton == MessageBoxDefaultButton.Button3 )
					{
						this.AcceptButton = this.cmdRetry;
						this.CancelButton = this.cmdCancel;
					}
					break;

			}
			if( Parameters.Exception != null )
			{
				X -= ButtonWidth;
				this.cmdMoreInfo.Location = new Point( X, Y );
				this.cmdMoreInfo.Visible = true;
			}

			// Set Icon
			System.IO.MemoryStream ms = null;
			switch( Parameters.Icon )
			{
				case MessageBoxIcon.None:
					// 0
					break;
				case MessageBoxIcon.Error:
					// 16
					ms = new System.IO.MemoryStream( System.Convert.FromBase64String( this.lblIconError.Text ) );
					break;
				case MessageBoxIcon.Question:
					// 32
					ms = new System.IO.MemoryStream( System.Convert.FromBase64String( this.lblIconQuestion.Text ) );
					break;
				case MessageBoxIcon.Exclamation:
					// 48
					ms = new System.IO.MemoryStream( System.Convert.FromBase64String( this.lblIconWarning.Text ) );
					break;
				case MessageBoxIcon.Asterisk:
					// 64
					ms = new System.IO.MemoryStream( System.Convert.FromBase64String( this.lblIconInformation.Text ) );
					break;
			}
			if( ms != null )
			{
				try { this.picIcon.Image = Image.FromStream( ms ); }
				catch( Exception ) { }
			}

			// Set window icon.
			if( Parameters.Owner != null )
			{
				try
				{
					Form form = (Form)Parameters.Owner;
					this.Icon = form.Icon;
				}
				catch { }
			}

			// Show Window
			DialogResult result = DialogResult.Cancel;
			try
			{
				result = this.ShowDialog( Parameters.Owner );
			}
			catch { }
			//catch( Exception exception )
			//{
			//    SystemLog.LogError( "Exception during MessageWindow.ShowDialog(). Message text = '" + Parameters.Text + "'.", exception );
			//}
			return result;
		}


		//--------------------------------------------------------------------
		public static DialogResult ShowMessage( IWin32Window Owner, string Text )
		{
			MessageWindowParameters Parameters = new MessageWindowParameters();
			Parameters.Owner = Owner;
			Parameters.Text = Text;
			MessageWindow frm = new MessageWindow();
			DialogResult result = frm.Show( Parameters );
			return result;
		}


		//--------------------------------------------------------------------
		public static DialogResult ShowInformation( IWin32Window Owner, string Text )
		{
			MessageWindowParameters Parameters = new MessageWindowParameters();
			Parameters.Owner = Owner;
			Parameters.Text = Text;
			Parameters.Icon = MessageBoxIcon.Information;
			MessageWindow frm = new MessageWindow();
			DialogResult result = frm.Show( Parameters );
			return result;
		}


		//--------------------------------------------------------------------
		public static DialogResult ShowQuestion( IWin32Window Owner, string Text, MessageBoxButtons Buttons )
		{
			MessageWindowParameters Parameters = new MessageWindowParameters();
			Parameters.Owner = Owner;
			Parameters.Text = Text;
			Parameters.Buttons = Buttons;
			Parameters.Icon = MessageBoxIcon.Question;
			MessageWindow frm = new MessageWindow();
			DialogResult result = frm.Show( Parameters );
			return result;
		}


		//--------------------------------------------------------------------
		public static string GetUserInput( IWin32Window Owner, string Prompt, string DefaultValue )
		{
			MessageWindowParameters Parameters = new MessageWindowParameters();
			Parameters.Owner = Owner;
			Parameters.Caption = "User Input";
			Parameters.Text = Prompt;
			Parameters.Buttons = MessageBoxButtons.OKCancel;
			Parameters.Icon = MessageBoxIcon.Question;
			Parameters.GetUserInput = true;
			MessageWindow frm = new MessageWindow();
			frm.txtUserInput.Text = DefaultValue;
			DialogResult result = frm.Show( Parameters );
			if( result == DialogResult.Cancel ) { return ""; }
			return frm.txtUserInput.Text;
		}


		//--------------------------------------------------------------------
		public static string GetUserInput( IWin32Window Owner, string Prompt, string[] Choices )
		{
			MessageWindowParameters Parameters = new MessageWindowParameters();
			Parameters.Owner = Owner;
			Parameters.Caption = "User Input";
			Parameters.Text = Prompt;
			Parameters.Buttons = MessageBoxButtons.OKCancel;
			Parameters.Icon = MessageBoxIcon.Question;
			Parameters.GetUserInput = true;
			Parameters.UserChoices = Choices;
			MessageWindow frm = new MessageWindow();
			DialogResult result = frm.Show( Parameters );
			if( result == DialogResult.Cancel ) { return ""; }
			if( frm.cboUserInput.SelectedItem == null ) { return ""; }
			string value = frm.cboUserInput.SelectedItem.ToString();
			return value;
		}


		//--------------------------------------------------------------------
		public static DialogResult ShowError( IWin32Window Owner, Exception Exception )
		{
			MessageWindowParameters Parameters = new MessageWindowParameters();
			Parameters.Owner = Owner;
			Parameters.Text = Exception.Message;
			Parameters.Icon = MessageBoxIcon.Error;
			Parameters.Exception = Exception;
			MessageWindow frm = new MessageWindow();
			DialogResult result = frm.Show( Parameters );
			return result;
		}


		//--------------------------------------------------------------------
		public static DialogResult ShowError( IWin32Window Owner, string Text, Exception Exception )
		{
			MessageWindowParameters Parameters = new MessageWindowParameters();
			Parameters.Text = Text;
			Parameters.Owner = Owner;
			if( Exception != null ) { Parameters.Text += "\r\n" + Exception.Message; }
			Parameters.Icon = MessageBoxIcon.Error;
			Parameters.Exception = Exception;

			MessageWindow frm = new MessageWindow();
			DialogResult result = frm.Show( Parameters );
			return result;
		}


		//--------------------------------------------------------------------
		public static DialogResult ShowError( IWin32Window Owner, string Text )
		{
			return ShowError( Owner, Text );
		}


		//---------------------------------------------------------------------
		public static string Exception2String( System.Exception Exception, bool IncludeStackTrace )
		{
			if( Exception == null ) { return ""; }
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			int i = 0;
			Exception ex = Exception;
			while( ex != null )
			{
				i++;
				builder.AppendLine( string.Format( "Message {0}: {1}\n", i.ToString( "000" ), ex.Message ) );
				ex = ex.InnerException;
			}
			if( IncludeStackTrace )
			{
				builder.AppendLine( "---------------------------------------------------------------------" );
				builder.AppendLine( "Stack Trace:" );
				builder.AppendLine( Exception.StackTrace );
			}
			return builder.ToString();
		}
		public static string Exception2String( System.Exception Exception )
		{ return Exception2String( Exception, true ); }


		//--------------------------------------------------------------------
		private void cmdOK_Click( System.Object sender, System.EventArgs e )
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
			return;
		}
		private void cmdNo_Click( System.Object sender, System.EventArgs e )
		{
			this.DialogResult = DialogResult.No;
			this.Close();
			return;
		}
		private void cmdYes_Click( System.Object sender, System.EventArgs e )
		{
			this.DialogResult = DialogResult.Yes;
			this.Close();
			return;
		}
		private void cmdIgnore_Click( System.Object sender, System.EventArgs e )
		{
			this.DialogResult = DialogResult.Ignore;
			this.Close();
			return;
		}
		private void cmdRetry_Click( System.Object sender, System.EventArgs e )
		{
			this.DialogResult = DialogResult.Retry;
			this.Close();
			return;
		}
		private void cmdAbort_Click( System.Object sender, System.EventArgs e )
		{
			this.DialogResult = DialogResult.Abort;
			this.Close();
			return;
		}
		private void cmdCancel_Click( System.Object sender, System.EventArgs e )
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
			return;
		}
		private void cmdMoreInfo_Click( object sender, EventArgs e )
		{
			MessageWindowParameters Parameters = new MessageWindowParameters();
			Parameters.Owner = this;
			string exception_text = Exception2String( this._Parameters.Exception );
			Parameters.Text = string.Format( "{0}\r\n{1}", this._Parameters.Text, exception_text );
			Parameters.Icon = MessageBoxIcon.Error;
			MessageWindow frm = new MessageWindow();
			frm.Show( Parameters );
			return;
		}


	}
}
