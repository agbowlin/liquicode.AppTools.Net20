

//using System;
//using System.Drawing;
//using System.Collections.Generic;


//namespace liquicode.AppTools
//{
//    public abstract class ComponentLayout
//        : List<ComponentRectangle>
//        , IVisualComponent
//    {


//        //=====================================================================
//        //		Constructors and Destructors
//        //=====================================================================


//        //---------------------------------------------------------------------
//        public ComponentLayout()
//        { return; }


//        //---------------------------------------------------------------------
//        public ComponentLayout( IVisualComponent[] TheseComponents )
//        {
//            foreach( IVisualComponent component in TheseComponents )
//            {
//                this.Add( new ComponentRectangle( component ) );
//            }
//            return;
//        }


//        //=====================================================================
//        //		IVisualComponent implementation
//        //=====================================================================


//        //---------------------------------------------------------------------
//        protected abstract Size Layout_GetPreferredSize( int? Width, int? Height );
//        protected abstract void Layout_Resize( Size Size );


//        //---------------------------------------------------------------------
//        Size IVisualComponent.GetPreferredSize( int? Width, int? Height )
//        {
//            return this.Layout_GetPreferredSize( Width, Height );
//        }


//        //---------------------------------------------------------------------
//        public void Resize( Size Size )
//        {
//            this.Layout_Resize( Size );
//            return;
//        }


//        //---------------------------------------------------------------------
//        void IVisualComponent.Draw( Graphics Graphics, Rectangle Rectangle )
//        {
//            foreach( ComponentRectangle item in this )
//            {
//                if( item.Rectangle.HasValue )
//                {
//                    Rectangle draw_rect = new Rectangle( item.Rectangle.Value.Location, item.Rectangle.Value.Size );
//                    draw_rect.Offset( Rectangle.Location );
//                    if( draw_rect.IntersectsWith( Rectangle ) )
//                    {
//                        item.VisualComponent.Draw( Graphics, Rectangle );
//                    }
//                }
//            }
//            return;
//        }


//        //=====================================================================
//        //		Public Methods
//        //=====================================================================


//        //---------------------------------------------------------------------
//        public ComponentRectangle FindComponent( IVisualComponent Component )
//        {
//            return this.Find(
//                delegate( ComponentRectangle _item )
//                { return _item.VisualComponent == Component; } );
//        }


//        //---------------------------------------------------------------------
//        public void MoveToBottom( ComponentRectangle Item )
//        {
//            int index = this.IndexOf( Item );
//            if( index < 0 ) { throw new Exception( "Item does not exisyt in this list." ); }
//            this.RemoveAt( index );
//            this.Insert( 0, Item );
//            return;
//        }


//        //---------------------------------------------------------------------
//        public void MoveToTop( ComponentRectangle Item )
//        {
//            int index = this.IndexOf( Item );
//            if( index < 0 ) { throw new Exception( "Item does not exisyt in this list." ); }
//            this.RemoveAt( index );
//            this.Add( Item );
//            return;
//        }


//    }
//}
