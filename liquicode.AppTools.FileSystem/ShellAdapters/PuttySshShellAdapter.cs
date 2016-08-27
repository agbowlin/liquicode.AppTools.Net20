
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace liquicode.AppTools
{
	public class PuttySshShellAdapter : ShellAdapter
	{

		//---------------------------------------------------------------------
		//	PuTTY Scripting Command Reference:
		//		http://the.earth.li/~sgtatham/putty/0.60/htmldoc/
		//---------------------------------------------------------------------

		//---------------------------------------------------------------------
		public override string ProtocolName
		{
			get { return "ssh"; }
		}


		//---------------------------------------------------------------------
		protected string _PuttyPlinkFilename = "";
		public string PuttyPlinkFilename
		{
			get { return this._PuttyPlinkFilename; }
			set { this._PuttyPlinkFilename = value; }
		}


		//---------------------------------------------------------------------
		public PuttySshShellAdapter()
		{
			this._PuttyPlinkFilename = "";
			this._Host = "";
			this._Port = 22;
			this._User = "";
			this._Password = "";
			this._Root = "";
			return;
		}


		//---------------------------------------------------------------------
		private bool _AcceptUncachedHostKeys = true;
		public bool AcceptUncachedHostKeys
		{
			get { return this._AcceptUncachedHostKeys; }
			set { this._AcceptUncachedHostKeys = value; }
		}


		//---------------------------------------------------------------------
		private bool _CacheUncachedHostKeys = false;
		public bool CacheUncachedHostKeys
		{
			get { return this._CacheUncachedHostKeys; }
			set { this._CacheUncachedHostKeys = value; }
		}


		//---------------------------------------------------------------------
		private string _PlinkVersion = "";
		public string GetPlinkVersion()
		{
			if( this._PlinkVersion.Length == 0 )
			{
				Process CommandProcess = new Process();
				CommandProcess.StartInfo.FileName = this._PuttyPlinkFilename;

				CommandProcess.StartInfo.UseShellExecute = false;
				CommandProcess.StartInfo.RedirectStandardInput = true;
				CommandProcess.StartInfo.RedirectStandardError = true;
				CommandProcess.StartInfo.RedirectStandardOutput = true;

				CommandProcess.StartInfo.CreateNoWindow = true;
				CommandProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

				// Get Plink version.
				CommandProcess.StartInfo.Arguments = "-V";
				string ErrString = "";
				string OutString = "";
				try
				{
					CommandProcess.Start();
					ErrString = this.Reader2String( CommandProcess.StandardError );
					if( ErrString.Length > 0 ) { throw new Exception( ErrString ); }
					OutString = this.Reader2String( CommandProcess.StandardOutput );
					CommandProcess.WaitForExit();
				}
				catch( Exception ex )
				{ throw ex; }
				this._PlinkVersion = OutString;
			}
			return this._PlinkVersion;
		}


		//---------------------------------------------------------------------
		public override string ExecuteCommand( string SshCommand )
		{
			if( this._PuttyPlinkFilename.Length == 0 ) { throw new Exceptions.InvalidOperationException( "ExecuteCommand", "PuttyPlinkFilename is undefined" ); }
			if( this._Host.Length == 0 ) { throw new Exceptions.InvalidOperationException( "ExecuteCommand", "Host is undefined" ); }

			string Arguments = "";
			string ErrString = "";
			string OutString = "";
			Process CommandProcess = new Process();
			CommandProcess.StartInfo.FileName = this._PuttyPlinkFilename;

			CommandProcess.StartInfo.UseShellExecute = false;
			CommandProcess.StartInfo.RedirectStandardInput = true;
			CommandProcess.StartInfo.RedirectStandardError = true;
			CommandProcess.StartInfo.RedirectStandardOutput = true;

			CommandProcess.StartInfo.CreateNoWindow = true;
			CommandProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

			// Execute command.
			Arguments = "-ssh -x -a -T -noagent";
			if( this._User.Length > 0 ) { Arguments += " -l " + this._User; }
			if( this._Password.Length > 0 ) { Arguments += " -pw " + this._Password; }
			Arguments += " -P " + this._Port.ToString();
			Arguments += " " + this._Host;
			Arguments += " " + SshCommand;
			CommandProcess.StartInfo.Arguments = Arguments;
			try
			{
				CommandProcess.Start();
				ErrString = "";
				OutString = "";
			ContinueProcess:
				while( true )
				{
					ErrString += this.Reader2String( CommandProcess.StandardError );
					if( ErrString.Length > 0 ) { break; }
					OutString += this.Reader2String( CommandProcess.StandardOutput );
					if( CommandProcess.HasExited ) { break; }
				}
				if( ErrString.StartsWith( "The server's host key is not cached in the registry." ) )
				{
					StreamWriter writer = CommandProcess.StandardInput;
					if( !this._AcceptUncachedHostKeys )
					{
						writer.WriteLine( "n" );
						writer.Flush();
						CommandProcess.WaitForExit();
						throw new Exception( ErrString );
					}
					ErrString = "";
					if( this._CacheUncachedHostKeys )
					{
						writer.WriteLine( "y" );
						writer.Flush();
					}
					else
					{
						writer.WriteLine( "n" );
						writer.Flush();
					}
					goto ContinueProcess;
				}
				else if( CommandProcess.HasExited == false ) { goto ContinueProcess; }

				if( ErrString.Length > 0 )
				{ throw new Exception( ErrString ); }
			}
			catch( Exception ex )
			{ throw ex; }

			// Return, OK.
			return OutString;
		}


	}


}
