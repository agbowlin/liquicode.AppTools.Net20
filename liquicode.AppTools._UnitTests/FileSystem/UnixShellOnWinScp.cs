
//using System;
//using System.Collections.Generic;
//using System.Text;

//using liquicode.AppTools;
//using NUnit.Framework;


//[TestFixture]
//public class UnixShellOnWinScp : FileSystemTests
//{

//    //---------------------------------------------------------------------
//    [SetUp]
//    public void Setup()
//    {
//        WinScpShellAdapter shell = new WinScpShellAdapter();
//        string ConfigPathname = "..\\_private\\UnixShellOnWinScp.config.txt";
//        shell.WinScpFilename = this.GetConfigValue( ConfigPathname, "WinScpFilename" );
//        shell.Host = this.GetConfigValue( ConfigPathname, "Host" );
//        shell.User = this.GetConfigValue( ConfigPathname, "User" );
//        shell.Password = this.GetConfigValue( ConfigPathname, "Password" );
//        try { shell.Port = Convert.ToInt32( this.GetConfigValue( ConfigPathname, "Port" ) ); }
//        catch { }
//        shell.Root = this.GetConfigValue( ConfigPathname, "Root" );
//        this._FileSystem = new UnixShell( shell );
//        return;
//    }

//    //---------------------------------------------------------------------
//    [TearDown]
//    public void TearDown()
//    {
//        this._FileSystem = null;
//        return;
//    }

//}
