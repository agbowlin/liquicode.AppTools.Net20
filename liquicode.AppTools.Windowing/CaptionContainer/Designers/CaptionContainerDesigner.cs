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
	public class CaptionContainerDesigner : ParentControlDesigner
	{
		public override void Initialize(System.ComponentModel.IComponent component)
		{
			base.Initialize(component);

			if (this.Control is CaptionContainerControl)
			{
				this.EnableDesignMode(((CaptionContainerControl)this.Control).WorkingArea, "WorkingArea");
			}
		}

		public override bool CanParent(System.Windows.Forms.Control control)
		{
			return control is System.Windows.Forms.Control;
		}

		public override bool CanParent(System.Windows.Forms.Design.ControlDesigner controlDesigner)
		{
			if (controlDesigner != null && controlDesigner.Control is System.Windows.Forms.Control)
			{
				return true;
			}
			return false;
		}

	}
}
