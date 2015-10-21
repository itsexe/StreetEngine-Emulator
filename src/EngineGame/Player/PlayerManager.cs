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
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    public class PlayerManager
    {
        /// <summary>
        /// New mmoClient list
        /// </summary>
        List<Engine.Network.mmoClient> Client = new List<Engine.Network.mmoClient>();

        /// <summary>
        /// Disconnect selected player.
        /// </summary>
        /// <param name="ip"></param>
        public void DisconnectPlayerByName(string name)
        {
            Engine.Network.mmoClient Player = Client.Find(x => x.info.username == name);

            if(Player.info.sessionKey != null)
                Player.CloseSocket();
        }

        /// <summary>
        /// Disconnect selected player.
        /// </summary>
        /// <param name="ip"></param>
        public void DisconnectPlayerByIp(string ip)
        {
            Engine.Network.mmoClient Player = Client.Find(x => x.info.ip == ip);

            if(Player.info.sessionKey != null)
                Player.CloseSocket();
        }

        /// <summary>
        /// Disconnect every player connected to the server.
        /// </summary>
        public void DisconnectAllPlayers()
        {   
            foreach(var Player in Client.Where(x => x.info.isConnected == true))
            {
                if(Player.info.isInWorld)
                    Player.CloseSocket();
            }
        }

        /// <summary>
        /// Send packet to every player connected to the server.
        /// </summary>
        /// <param name="data"></param>
        public void SendPacketToAllPlayers(byte[] data)
        {
            foreach (var Player in Client.Where(x => x.info.isConnected == true))
            {
                if (Player.info.isInWorld)
                    Player.Send(data);
            }
        }
    }
}
