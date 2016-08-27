

using System;
using System.Drawing;


namespace liquicode.AppTools
{
	public abstract class ComponentDecorator
		: IVisualComponent
	{


		//=====================================================================
		//		Constructors and Destructors
		//=====================================================================


		//---------------------------------------------------------------------
		public ComponentDecorator( IVisualComponent ThisComponent )
		{
			this._VisualComponent = ThisComponent;
			return;
		}


		//=====================================================================
		//		Public Properties
		//=====================================================================


		//---------------------------------------------------------------------
		protected IVisualComponent _VisualComponent = null;
		public IVisualComponent VisualComponent
		{
			get { return this._VisualComponent; }
			set { this._VisualComponent = value; }
		}


		//=====================================================================
		//		VisualComponent implementation
		//=====================================================================


		//---------------------------------------------------------------------
		protected abstract Size Decorator_GetPreferredSize( int? Width, int? Height );
		protected abstract void Decorator_Draw( Graphics Graphics, Rectangle Rectangle );
		protected abstract IVisualComponent Decorator_HitTest( Rectangle Rectangle, Point Point );


		//---------------------------------------------------------------------
		void IVisualComponent.SetContent( object Content )
		{
			this._VisualComponent.SetContent( Content );
			return;
		}


		//---------------------------------------------------------------------
		Size IVisualComponent.GetPreferredSize( int? Width, int? Height )
		{
			return this.Decorator_GetPreferredSize( Width, Height );
		}


		//---------------------------------------------------------------------
		void IVisualComponent.Draw( Graphics Graphics, Rectangle Rectangle )
		{
			this.Decorator_Draw( Graphics, Rectangle );
			return;
		}


		//---------------------------------------------------------------------
		IVisualComponent IVisualComponent.HitTest( Rectangle Rectangle, Point Point )
		{
			return this.Decorator_HitTest( Rectangle, Point );
		}


	}
}
