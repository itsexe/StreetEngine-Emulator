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
namespace StreetEngine.EngineExternal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class LoadTxt
    {
        /// <summary>
        /// Optional message actions
        /// </summary>
        public static Action<String> Event = msg => EngineConsole.Log.Infos("External.Txt", msg);
        public static Action<String> Error = msg => EngineConsole.Log.Error(msg);
        public static Action<String> Debug = msg => EngineConsole.Log.Debug(msg);

        public class Channels
        {
            /// <summary>
            /// Load channels names from world//channels.txt
            /// </summary>
            /// <param name="filename"></param>
            public static void LoadChannels(string filename = "channels.txt")
            {
                try
                {
                    StreamReader Reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "world\\" + filename);
                    if (Reader != null)
                    {
                        string[] Channels = Reader.ReadToEnd().Split(',');
                        if (Channels != null)
                        {
                            EngineEnum.WorldEnum.Channel.channel_a = Channels[0];
                            EngineEnum.WorldEnum.Channel.channel_b = Channels[1];
                            EngineEnum.WorldEnum.Channel.channel_c = Channels[2];
                            EngineEnum.WorldEnum.Channel.channel_d = Channels[3];
                            EngineEnum.WorldEnum.Channel.channel_e = Channels[4];
                            EngineEnum.WorldEnum.Channel.channel_f = Channels[5];
                            EngineEnum.WorldEnum.Channel.channel_g = Channels[6];
                            EngineEnum.WorldEnum.Channel.channel_h = Channels[7];
                            EngineEnum.WorldEnum.Channel.channel_i = Channels[8];
                            EngineEnum.WorldEnum.Channel.channel_j = Channels[9];
                        }
                        Event("Loaded '" + Channels.Count() + "' channels names");
                    }
                    Reader = null;
                }
                catch(Exception ex)
                {
                    Error.Invoke(ex.ToString());
                }
            }
        }
    }
}
