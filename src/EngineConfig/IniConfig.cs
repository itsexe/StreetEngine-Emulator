/*
 *****************************************************************
 *                     StreetEngine Project                      *
 *                                                               *
 * Author: http://github.com/greatmaes                           *
 * Project: http://github.com/greatmaes/StreetEngine-Emulator/   *
 * Chat: http://gitter.im/greatmaes/StreetEngine-Emulator/~chat# *
 *                                                               *
 * About the project:                                            *
 * StreetEngine is a non-profit server side emulator for the ga- *
 * -me StreetGears. This is mostly a project to learn how to ma- *
 * -ke your own emulator as I don't have the knowledge to fully  *
 * finish this project. Feel free to contribute. More informat-  *  
 * ations avaible on the github project page.                    *
 *                                                               *
 * Notes:                                                        *
 * All comments '//' and '///' are optional and can be removed.  *   
 * You must move every files you downloaded to your game folder  *
 * to successfully start StreetEngine.                           *
 *                                                               *
 * Contributors (in alphabetical order):                         *
 * - geekogame                                                   *
 *                                                               *
 * Credits:                                                      *
 * https://github.com/itsexe                                     *
 * https://github.com/skeezr/                                    *
 * http://www.elitepvpers.com/forum/members/4193997-k1ramox.html *
 *                                                               *
 ***************************************************************** 
*/
namespace StreetEngine.EngineConfig
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    public class IniConfig
    {
        /// <summary>
        /// New empty list
        /// </summary>
        public static List<String> keysList = new List<String>();

        /// <summary>
        /// New empty list
        /// </summary>
        public static List<Int32> sectionsList = new List<Int32>();

        /// <summary>
        /// Optional message actions
        /// </summary>
        public static Action<String> Event = msg => EngineConsole.Log.Infos("Config.IniConfig", msg);
        public static Action<String> Error = msg => EngineConsole.Log.Error(msg);
        public static Action<String> Debug = msg => EngineConsole.Log.Debug(msg);
        
        /// <summary>
        /// Create/load a config file
        /// </summary>
        public static IniFile Ini = new IniFile("config.ini");

        /// <summary>
        /// New empty int32
        /// </summary>
        public static Int32 count = 0;

        /// <summary>
        /// Load confing.ini settings into a struct.
        /// </summary>
        public static void LoadSettings()
        {
            // Read settings from StreetEngine config
            Ini.ReadSettings();

            // Initialize config.ini sections
            string[] sections = 
            { "Database",
              "WorldSettings",
              "Auth",
              "World",
              "Lobby",
              "Msg" };

            // Load up each values to a list
            foreach(var values in sections)
            {
                count += Ini.Elements[values].Count(); // Count StreetEngine sections
                keysList.Add(Ini.Elements[values].ToString()); // Count StreetEngine keys
                sectionsList.Add(count); // Gets StreetEngine sections count
            }

            // Optional message
            Event.Invoke("'" + keysList.Count() + "' Keys loaded");
            Event.Invoke("'" + sectionsList[5] + "' Sections loaded");
        }
    }
}
