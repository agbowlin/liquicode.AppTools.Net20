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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace liquicode.AppTools
{
	[
	Designer( typeof( liquicode.AppTools.Designers.ContainerDesigner ) )
	]
	public partial class ContainerControl : Panel
	{
		public ContainerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}
