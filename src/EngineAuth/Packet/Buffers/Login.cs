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
    using System.Linq;
    using System.Text;
    using EnginePacket;

    public class Login
    {
        public static Action<String> Error = msg => EngineConsole.Log.Error(msg);

        public Engine.Network.Client clientsocket;

        public string sessionkey = "";

        public Login(string session_key, Engine.Network.Client client_socket)
        {
            clientsocket = client_socket;
            sessionkey = session_key;
        }

        /// <summary>
        /// Returns 'Login' packet buffer
        /// </summary>
        /// <returns></returns>
        public byte[] CreateBuff()
        {
            byte[] packet_base = new byte[0x2F]; // Create our null byte array
            PacketWriter PW = new PacketWriter(packet_base); // Initialize the writer

            PW.WriteByteArray(0, EngineUtils.PacketUtils.calcPacket(packet_base.Length, 1009)); // Write the packet header
            PW.WriteSByte(5, 1); // I don't know yet what is it

            if (sessionkey.Length > 17 || sessionkey.Length < 17) // Session is and will always be 17 lenght long,
            { // so if its not.. you did something wrong.
                Error.Invoke("Session key: '" + sessionkey + "' is too long, can't connect player."); // Optional
                return null; // Returns null. Packet failed to create  
            }
            else
            {
                PW.WriteString(9, sessionkey); // Write the player's auth key
                PW.WriteInt32(42, 700); // I don't know yet what is it - idle time?
                PW.WriteSByte(46, 1); // I don't know yet what is it
            }

            return packet_base;
        }

        /// <summary>
        /// Send a packet
        /// </summary>
        public void Send()
        {
            clientsocket.Send(CreateBuff());
        }
    }
}
 