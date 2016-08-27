
//using System;
////using System.Collections.Generic;
////using System.Text;
////using System.IO;

//using liquicode.AppTools;
//using NUnit.Framework;


//[TestFixture]
//public class UnixShellOnPuttySsh : FileSystemTests
//{

//    //---------------------------------------------------------------------
//    [SetUp]
//    public void Setup()
//    {
//        PuttySshShellAdapter shell = new PuttySshShellAdapter();
//        string ConfigPathname = "..\\_private\\UnixShellOnPuttySsh.config.txt";
//        shell.PuttyPlinkFilename = this.GetConfigValue( ConfigPathname, "PuttyPlinkFilename" );
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
