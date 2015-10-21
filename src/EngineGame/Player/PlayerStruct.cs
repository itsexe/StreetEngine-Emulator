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
namespace StreetEngine.EngineGame.Player
{
    using System;

    public class PlayerStruct
    {
        /// <summary>
        /// Gets a random 17 long string key
        /// </summary>
        /// <returns></returns>
        public static string GenerateSessionKey() 
        {
            return EngineUtils.ByteUtils.GenerateRandomKey((int)17);
        }

        /// <summary>
        /// Player struct
        /// </summary>
        public class Information
        {
            public Int32 username_position { get; set; }
            public Int32 password_position { get; set; }

            public Boolean isConnected { get; set; }
            public Boolean isInAuth { get; set; }
            public Boolean isInWorld { get; set; }
            public Boolean isInLobby { get; set; }

            public String ip { get; set; }
            public String email { get; set; }
            public String username { get; set; }
            public String password { get; set; }
            public String sessionKey { get; set; }

            public String rank { get; set; }

            public String clan_id { get; set; }
            public String clan_name { get; set; }

            public String bio { get; set; }
            public String s_zone { get; set; }

            public Int32 id { get; set; }
            public Int32 last_login { get; set; }

            public Int32 type { get; set; }
            public Int32 level { get; set; }
            public Int32 exp { get; set; }
            public Int32 licence { get; set; }
            public Int32 gpotatos { get; set; }
            public Int32 rupees { get; set; }
            public Int32 coins { get; set; }
            public Int32 questpoints { get; set; }

            public Int32 grind_level { get; set; }
            public Int32 backflip_level { get; set; }
            public Int32 frontflip_level { get; set; }
            public Int32 airtwist_level { get; set; }
            public Int32 powerswing_level { get; set; }
            public Int32 gripturn_level { get; set; }
            public Int32 dash_level { get; set; }
            public Int32 backskating_level { get; set; }
            public Int32 jumpingsteer_level { get; set; }
            public Int32 butting_level { get; set; }
            public Int32 powerslide_level { get; set; }
            public Int32 powerjump_level { get; set; }
            public Int32 wallride_level { get; set; }

            public Int32 age { get; set; }
            public Int32 c_zone { get; set; }
        };
    }
}
