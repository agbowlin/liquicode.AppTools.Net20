

using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Windows.Forms;


namespace liquicode.AppTools
{


	[Serializable]
	public class ApplicationOption
	{
		public string OptionName = "";
		public object OptionValue = "";
	}



	[Serializable]
	public class ApplicationOptions
	{


		//=====================================================================
		private string _FilenameBase = "";
		public List<ApplicationOption> Options = new List<ApplicationOption>();


		//=====================================================================
		public static string DefaultFilename()
		{
			string codebase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
			Uri uri = new Uri( codebase );
			string filename = uri.AbsolutePath + ".options.xml";
			return filename;
		}


		//=====================================================================
		public ApplicationOptions()
		{
			string codebase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
			Uri uri = new Uri( codebase );
			this._FilenameBase = uri.AbsolutePath;
			return;
		}


		//=====================================================================
		public ApplicationOptions( string FilenameBase )
		{
			this._FilenameBase = FilenameBase;
			return;
		}


		//=====================================================================
		public ApplicationOption FindOption( string OptionName )
		{
			if( this.Options == null ) { return null; }
			foreach( ApplicationOption option in this.Options )
			{
				if( option.OptionName == OptionName )
				{
					return option;
				}
			}
			return null;
		}


		//=====================================================================
		public ApplicationOption FindOrCreateOption( string OptionName )
		{
			if( this.Options == null ) { return null; }
			foreach( ApplicationOption option in this.Options )
			{
				if( option.OptionName == OptionName )
				{
					return option;
				}
			}
			ApplicationOption new_option = new ApplicationOption();
			new_option.OptionName = OptionName;
			this.Options.Add( new_option );
			return new_option;
		}


		//=====================================================================
		public object SetOption( string OptionName, object OptionValue )
		{
			ApplicationOption option = this.FindOrCreateOption( OptionName );
			option.OptionValue = OptionValue;
			return option.OptionValue;
		}


		//=====================================================================
		public object GetOption( string OptionName )
		{
			if( this.Options == null ) { return null; }
			ApplicationOption option = this.FindOption( OptionName );
			if( option == null ) { return null; }
			return option.OptionValue;
		}


		//=====================================================================
		public string GetOptionString( string OptionName )
		{
			return (string)this.GetOption( OptionName );
		}


		//=====================================================================
		public void SetOption_WindowCoordinates( string WindowName, Form Window )
		{
			this.FindOrCreateOption( WindowName + ".Window.Location.X" ).OptionValue = Window.Location.X;
			this.FindOrCreateOption( WindowName + ".Window.Location.Y" ).OptionValue = Window.Location.Y;
			this.FindOrCreateOption( WindowName + ".Window.Size.Width" ).OptionValue = Window.Size.Width;
			this.FindOrCreateOption( WindowName + ".Window.Size.Height" ).OptionValue = Window.Size.Height;
			return;
		}


		//=====================================================================
		public void GetOption_WindowCoordinates( string WindowName, Form Window )
		{
			ApplicationOption location_x = this.FindOption( WindowName + ".Window.Location.X" );
			ApplicationOption location_y = this.FindOption( WindowName + ".Window.Location.Y" );
			ApplicationOption size_width = this.FindOption( WindowName + ".Window.Size.Width" );
			ApplicationOption size_height = this.FindOption( WindowName + ".Window.Size.Height" );
			if( (location_x != null) && (location_y != null) )
			{
				Window.Location = new Point( (int)location_x.OptionValue, (int)location_y.OptionValue );
			}
			if( (size_width != null) && (size_height != null) )
			{
				Window.Size = new Size( (int)size_width.OptionValue, (int)size_height.OptionValue );
			}
			return;
		}


		//=====================================================================
		public ApplicationOption RemoveOption( string OptionName )
		{
			ApplicationOption option = this.FindOption( OptionName );
			if( option == null ) { return null; }
			this.Options.Remove( option );
			return option;
		}


		//=====================================================================
		public List<ApplicationOption> GetOptionsStartingWith( string OptionNameStart )
		{
			if( this.Options == null ) { return null; }
			List<ApplicationOption> options = new List<ApplicationOption>();
			foreach( ApplicationOption option in this.Options )
			{
				if( option.OptionName.StartsWith( OptionNameStart ) )
				{
					options.Add( option );
				}
			}
			return options;
		}


		//=====================================================================
		public void RemoveOptionsStartingWith( string OptionNameStart )
		{
			if( this.Options == null ) { return; }
			List<ApplicationOption> options = this.GetOptionsStartingWith( OptionNameStart );
			foreach( ApplicationOption option in options )
			{
				this.Options.Remove( option );
			}
			return;
		}


		//=====================================================================
		public void ClearOptions()
		{
			this.Options.Clear();
			return;
		}


		//=====================================================================
		public static ApplicationOptions FromXml( string XmlString )
		{
			ApplicationOptions options = default( ApplicationOptions );
			using( StringReader reader = new StringReader( XmlString ) )
			{
				XmlSerializer serializer = new XmlSerializer( typeof( ApplicationOptions ) );
				options = (ApplicationOptions)serializer.Deserialize( reader );
			}
			return options;
		}


		//=====================================================================
		public string GetXml()
		{
			StringBuilder sb = new StringBuilder();
			using( StringWriter writer = new StringWriter( sb ) )
			{
				XmlSerializer serializer = new XmlSerializer( typeof( ApplicationOptions ) );
				serializer.Serialize( writer, this );
				writer.Flush();
				writer.Close();
			}
			return sb.ToString();
		}


		//=====================================================================
		public static ApplicationOptions LoadApplicationOptions( string Filename )
		{
			if( File.Exists( Filename ) )
			{
				return ApplicationOptions.FromXml( File.ReadAllText( Filename ) );
			}
			else
			{
				return new ApplicationOptions();
			}
		}


		//=====================================================================
		public static ApplicationOptions LoadApplicationOptions()
		{
			return ApplicationOptions.LoadApplicationOptions( ApplicationOptions.DefaultFilename() );
		}


		//=====================================================================
		public void SaveApplicationOptions( string Filename )
		{
			File.WriteAllText( Filename, this.GetXml() );
			return;
		}


		//=====================================================================
		public void SaveApplicationOptions()
		{
			this.SaveApplicationOptions( ApplicationOptions.DefaultFilename() );
			return;
		}

	}
}
