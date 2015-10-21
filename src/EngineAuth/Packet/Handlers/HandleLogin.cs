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
namespace StreetEngine.EngineAuth
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using StreetEngine.EnginePacket;

    public class HandleLogin
    {
        public StreetEngine.EngineDatabase.DatabaseManager MySQL = new StreetEngine.EngineDatabase.DatabaseManager();

        public Engine.Network.Client clientsocket;
        public byte[] _data;

        public HandleLogin(Engine.Network.Client _clientsocket, byte[] data)
        {
            clientsocket = _clientsocket;
            _data = data;
        }

        /// <summary>
        /// Handle the login buff
        /// </summary>
        public void HandleBuff()
        {
            PacketReader PR = new PacketReader(_data); // Initialize the reader

            // There is some errors in the login packet you receive when you try to log in.                       
            // You won't receive the same 'login packet' if you enter wrong creditentials first, so we must       
            // check if the player entered wrong creditentials to know where to seek both username and password.  

            if (clientsocket.info.username_position == 5)
            { // Just check if the player add wrong creditentials first
                clientsocket.info.username_position = 5;
                clientsocket.info.password_position = 18;
            }
            else
            {
                clientsocket.info.username_position = 14;
                clientsocket.info.password_position = 32;
            }

            PR.BaseStream.Seek(clientsocket.info.username_position, SeekOrigin.Begin); // Seek where the username is located
            byte[] username = PR.ReadBytes(16);

            PR.BaseStream.Seek(clientsocket.info.password_position, SeekOrigin.Begin); // Seek where the password is located
            byte[] password = PR.ReadBytes(16);

            // Convert both username and password byte array to string and remove all null bytes
            new Thread(() => MySQL.CheckUserAccount(System.Text.Encoding.UTF8.GetString(username).Replace("\0", ""), System.Text.Encoding.UTF8.GetString(password).Replace("\0", ""), clientsocket)).Start(); // Check the user in the database..
        }
    }
}
