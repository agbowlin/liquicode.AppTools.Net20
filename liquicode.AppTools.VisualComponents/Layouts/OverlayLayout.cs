

//using System;
//using System.Drawing;


//namespace liquicode.AppTools
//{
//    public class OverlayLayout
//        : ComponentLayout
//    {


//        //=====================================================================
//        //		Constructors and Destructors
//        //=====================================================================


//        //---------------------------------------------------------------------
//        public OverlayLayout()
//            : base()
//        { return; }


//        //---------------------------------------------------------------------
//        public OverlayLayout( IVisualComponent[] TheseVisualComponents )
//            : base( TheseVisualComponents )
//        { return; }


//        //=====================================================================
//        //		ComponentLayout implementation
//        //=====================================================================


//        //---------------------------------------------------------------------
//        protected override Size Layout_GetPreferredSize( int? Width, int? Height )
//        {
//            Size max_size = new Size( 0, 0 );
//            foreach( ComponentRectangle item in this )
//            {
//                Size size = item.VisualComponent.GetPreferredSize( Width, Height );
//                if( max_size.Width < size.Width ) { max_size.Width = size.Width; }
//                if( max_size.Height < size.Height ) { max_size.Height = size.Height; }
//            }
//            return max_size;
//        }


//        //---------------------------------------------------------------------
//        protected override void Layout_Resize( Size Size )
//        {
//            foreach( ComponentRectangle item in this )
//            {
//                item.Rectangle = new Rectangle( new Point( 0, 0 ), Size );
//            }
//            return;
//        }


//    }
//}
