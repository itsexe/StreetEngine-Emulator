/*
 *****************************************************************
 *                     Street Engine Project                     *
 *                                                               *
 * Author: greatmaes (2015)                                      *
 * URL: https://github.com/greatmaes                             *
 *                                                               *
 * Notes:                                                        *
 * StreetEngine is a non-profit server side emulator for the ga- *
 * -me StreetGear. This is mostly a project to learn how to make *
 * your own emulator as I don't have the knowledge to finish th- *
 * -is project. Feel free to contribute.                         *
 *                                                               *
 * Credits:                                                      *
 * https://github.com/itsexe (help, original project)            *
 * http://www.elitepvpers.com/forum/members/4193997-k1ramox.html *
 *                                                               *
 ***************************************************************** 
*/
namespace StreetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Diagnostics;
    using System.Threading;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Net;

    class Program
    {
        /// <summary>
        /// Database manager definition
        /// </summary>
        public static StreetEngine.EngineDatabase.DatabaseManager MySQL = new StreetEngine.EngineDatabase.DatabaseManager();

        /// <summary>
        /// Create an Ascii art
        /// </summary>
        public static Action DrawAscii = new Action(() => EngineConsole.Log.DrawAscii());

        /// <summary>
        /// New empty WebClient
        /// </summary>
        public static WebClient webClient = new WebClient();

        /// <summary>
        /// Raw link of the current StreetEngine version
        /// </summary>
        public static string raw_VERSION = "http://raw.githubusercontent.com/greatmaes/StreetEngine-Emulator/master/VERSION.md";

        static void Main(string[] args)
        {
            // Version check
            if (!webClient.DownloadString(raw_VERSION).Contains(EngineConsole.Log.fileVersionInfo.FileVersion.ToString()))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Your StreetEngine version is outdated! Check out the new version avaible on the github link of the project.     \n\n");              
            } // Write a kind of error message on top of the console

            // Embed DLLs
            AppDomain.CurrentDomain.AssemblyResolve += EngineExternal.LoadDll.FindDLL;

            // Draw header
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Check it up on github: http://github.com/greatmaes/StreetEngine-Emulator        ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;

            // Draw Ascii art
            DrawAscii();

            // Draw informations and stuff
            Console.Beep();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("StreetEngine Dev Console [version " + EngineConsole.Log.fileVersionInfo.FileVersion + "]");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("");

            // Load settings
            Console.Write('\n');
            EngineConfig.IniConfig.LoadSettings();

            // Load up txt files
            Console.Write('\n');
            EngineExternal.LoadTxt.Channels.LoadChannels(/*File name can be changed here. filename: "yourfilename.txt"*/);

            // Load MySQL stuff
            Console.Write('\n');
            MySQL.OpenConnection();

            // Start every servers
            Console.Write('\n');
            Engine.Network.Server.Start();
            Engine.Network.msgServer.Start();
            Engine.Network.lobbyServer.Start();
            Engine.Network.mmoServer.Start();

            // Start StreetGears
            Process.Start("StreetGear.exe", "/enc /locale:" + EngineEnum.LauncherEnum.Locale.locale_fr + " /auth_ip:" + EngineConfig.IniConfig.Ini.Elements["Auth"]["Host"] + " /auth_port:" + EngineConfig.IniConfig.Ini.Elements["Auth"]["Port"] + " /window /debug");

            Console.ReadKey();
        }
    }
}
