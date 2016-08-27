

using System;
using System.Drawing;


namespace liquicode.AppTools
{
	public interface IVisualComponent
	{


		//=====================================================================
		//		Interface Methods
		//=====================================================================


		//---------------------------------------------------------------------
		void SetContent( object Content );
		Size GetPreferredSize( int? Width, int? Height );
		void Draw( Graphics Graphics, Rectangle Rectangle );
		IVisualComponent HitTest( Rectangle Rectangle, Point Point );


	}
}
