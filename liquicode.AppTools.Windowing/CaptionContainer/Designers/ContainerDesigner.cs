// ******************************************************************
//
//	If this code works it was written by:
//		Henry Minute
//		MamSoft / Manniff Computers
//		© 2008 - 2009...
//
//	if not, I have no idea who wrote it.
//
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;

namespace liquicode.AppTools.Designers
{
	public class ContainerDesigner : ScrollableControlDesigner
	{
		protected override void PreFilterProperties(System.Collections.IDictionary properties)
		{
			properties.Remove("Dock");

			base.PreFilterProperties(properties);
		}
	}
}
