

//using System;
//using System.Drawing;


//namespace liquicode.AppTools
//{
//    public static partial class Imaging
//    {


//        //-----------------------------------------------------
//        public class TextCell
//        {

//            public string Text = "";
//            public Font Font = null;
//            public string Width = "";
//            public string Height = "";
//            public string Align = "";
//            public string VAlign = "";
//            public Rectangle Rectangle = new Rectangle();

//        }


//        //-----------------------------------------------------
//        public class TextRow
//        {
//            public TextCell[] TextCells = { };
//        }


//        //-----------------------------------------------------
//        public static TextCell ParseTextCell( string String_in )
//        {
//            TextCell cell = new TextCell();
//            int ich = String_in.IndexOf( '>' );
//            if( ich < 0 ) { return cell; }
//            if( ich > 5 )
//            {
//                cell.Text = String_in.Substring( ich + 1 );
//            }
//            string sFormat = String_in.Substring( 5, (ich - 5) ).Trim();
//            string[] rgformats = sFormat.Split( ' ' );
//            foreach( string sFormat_loopVariable in rgformats )
//            {
//                sFormat = sFormat_loopVariable;
//                if( sFormat.StartsWith( "width=", StringComparison.OrdinalIgnoreCase ) )
//                {
//                    cell.Width = sFormat.Substring( 6 ).Trim().ToLower();
//                }
//                else if( sFormat.StartsWith( "height=", StringComparison.OrdinalIgnoreCase ) )
//                {
//                    cell.Height = sFormat.Substring( 7 ).Trim().ToLower();
//                }
//                else if( sFormat.StartsWith( "align=", StringComparison.OrdinalIgnoreCase ) )
//                {
//                    cell.Align = sFormat.Substring( 6 ).Trim().ToLower();
//                }
//                else if( sFormat.StartsWith( "valign=", StringComparison.OrdinalIgnoreCase ) )
//                {
//                    cell.VAlign = sFormat.Substring( 7 ).Trim().ToLower();
//                }
//            }
//            return cell;
//        }


//        //-----------------------------------------------------
//        public static TextRow[] ParseTextRows( string String_in )
//        {
//            string[] stringrows = String_in.Split( new string[] { "<row>" }, StringSplitOptions.RemoveEmptyEntries );
//            TextRow[] textrows = { };
//            textrows = new TextRow[ stringrows.Length ];
//            for( int ndxrow = 0; ndxrow <= (stringrows.Length - 1); ndxrow++ )
//            {
//                string[] stringcells = stringrows[ ndxrow ].Split( new string[] { "</cell>" }, StringSplitOptions.RemoveEmptyEntries );
//                textrows[ ndxrow ].TextCells = new TextCell[ stringcells.Length ];
//                for( int ndxcell = 0; ndxcell <= (stringcells.Length - 1); ndxcell++ )
//                {
//                    textrows[ ndxrow ].TextCells[ ndxcell ] = ParseTextCell( stringcells[ ndxcell ] );
//                }
//            }
//            return textrows;
//        }


//        //-----------------------------------------------------
//        public static Size MeasureText( Graphics Graphics, string Text, Font Font )
//        {
//            SizeF sizef = Graphics.MeasureString( Text, Font );
//            return new Size( (int)Math.Ceiling( sizef.Width ), (int)Math.Ceiling( sizef.Height ) );
//        }


//        //-----------------------------------------------------
//        public static void MeasureTextCell( Graphics Graphics_in, TextCell Cell_in, Font Font_in, Color Forecolor_in, Rectangle Rectangle_in )
//        {
//            Cell_in.Rectangle = new Rectangle();
//            if( (Cell_in.Width.Length > 0) )
//            {
//                if( Cell_in.Width.EndsWith( "px" ) )
//                {
//                    Cell_in.Rectangle.Width = Convert.ToInt32( Cell_in.Width );
//                }
//            }
//            return;
//        }


//        //-----------------------------------------------------
//        public static void DrawFormattedString( Graphics Graphics_in, string Text_in, Font Font_in, Color Forecolor_in, Rectangle Rectangle_in, StringFormat StringFormat_in )
//        {
//            return;
//        }


//    }


//}
