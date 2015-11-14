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

using StreetEngine.EngineConsole;

namespace StreetEngine.EngineConfig
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    public class IniFile
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        public Dictionary<string, Dictionary<string, string>> Elements = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, string> currentGroup = null;

        public static System.Action<System.String> Event = msg => EngineConsole.Log.Infos("Config.IniConfig", msg);

        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

        public void ReadSettings()
        {
            this.Elements.Clear();

            //Check if config exists
            if (!File.Exists(this.Path))
            {

                Event.Invoke("No config.ini found.");
                File.WriteAllText(this.Path, "; StreetEngine " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion + @" config file
; author link: http://github.com/greatmaes
; project link: http://github.com/greatmaes/StreetEngine-Emulator
; chat link: http://gitter.im/greatmaes/StreetEngine-Emulator/~chat#
; version link: https://raw.githubusercontent.com/greatmaes/StreetEngine-Emulator/master/VERSION.md 
; setup link: https://raw.githubusercontent.com/greatmaes/StreetEngine-Emulator/blob/master/README.md

; all comments are optional and can be removed
; use your local ip '127.0.0.1' to test stuff
; you can also use a local database

; auth server ip and port
[Auth]
Host=127.0.0.1
Port=443

; world server ip and port
[World]
Host=127.0.0.1
Port=590

; lobby server ip and port
[Lobby]
Host=127.0.0.1
Port=546

; msg server ip and port
[Msg]
Host=127.0.0.1
Port=569

; database server account and additional informations
[Database]
Host=null
User=null
Password=null
Database=null

; world settings stuff
; \""start\"" parameters are stuff that the player save when he creates an account
; \""MaximumUsers\"" is not only world based, it is for all servers
[WorldSettings]
startGpotatos = 999999
startRupees = 999999
startQuestPoints = 999999
startCoins = 999999
startLevel = 45
startLiscence = 4
startType = 3
MaximumUsers = 500

; Settings of the Client
; Language must be fr, de or kr
[GameSettings]
language=fr
gameLocation=C:\the\folder\of\streetgears\
");
                Event.Invoke("Config.ini created. Please check your SQL Settings!");
            }
            StreamReader reader = new StreamReader(this.Path);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line != "" && !line.StartsWith(";"))
                {
                    if (line.StartsWith("["))
                    {
                        currentGroup = new Dictionary<string, string>();
                        this.Elements.Add(line.Replace("[", "").Replace("]", ""), currentGroup);
                    }
                    else if (currentGroup != null)
                    {
                        string[] data = line.Trim().Split('=');
                        string key = data[0].Trim();
                        string value = data[1].Trim();
                        currentGroup.Add(key, value);
                    }
                }
            }
            reader.Close();
        }
    }
}