

using System;


namespace liquicode.AppTools
{
	public class XmlObjectPersonalizedSerializer<T>
		: XmlObjectSerializer<T>
	{


		//=====================================================================
		//		PUBLIC MEMBERS
		//=====================================================================


		//---------------------------------------------------------------------
		public XmlObjectPersonalizedSerializer()
			: base()
		{
			return;
		}


		//---------------------------------------------------------------------
		public XmlObjectPersonalizedSerializer( T ThisContent )
			: base( ThisContent )
		{
			return;
		}


		//=====================================================================
		//		DOCUMENT SERIALIZATION
		//=====================================================================


		//---------------------------------------------------------------------
		public string PersonalizedContentFilename
		{
			get
			{
				string file_name = typeof( T ).Name;
				if( Strings.IsNotNullOrEmpty( Identity.UserName ) ) { file_name += "." + Identity.UserName; }
				if( Strings.IsNotNullOrEmpty( Identity.DomainName ) ) { file_name += "." + Identity.DomainName; }
				file_name += ".xml";
				file_name = System.IO.Path.Combine( Files.GetExecutingAssemblyFolder(), file_name );
				return file_name;
			}
		}


		//---------------------------------------------------------------------
		public void LoadContent()
		{
			string filename = this.PersonalizedContentFilename;
			this.CreateOrLoadFromXmlFile( filename );
			return;
		}


		//---------------------------------------------------------------------
		public void SaveContent()
		{
			string filename = this.PersonalizedContentFilename;
			this.SaveToXmlFile( filename );
			return;
		}


	}
}
