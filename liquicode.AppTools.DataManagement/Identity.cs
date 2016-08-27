

using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Principal;


namespace liquicode.AppTools
{


	public static class Identity
	{


		//--------------------------------------------------------------------
		public static string DomainName
		{
			get
			{
				WindowsIdentity identity = WindowsIdentity.GetCurrent();
				if( identity == null ) { return ""; }
				string name = identity.Name;
				int ich = name.IndexOf( "\\" );
				if( ich < 0 ) { name = ""; }
				else { name = name.Substring( 0, ich ); }
				return name;
			}
		}


		//--------------------------------------------------------------------
		public static string UserName
		{
			get
			{
				WindowsIdentity identity = WindowsIdentity.GetCurrent();
				if( identity == null ) { return ""; }
				string name = identity.Name;
				int ich = name.IndexOf( "\\" );
				if( ich < 0 ) { /* do nothing */ }
				else { name = name.Substring( ich + 1 ); }
				return name;
			}
		}


	}


}
