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
    using StreetEngine.EnginePacket;

    public class SelectServer
    {
        public Engine.Network.Client clientsocket;
        public EngineEnum.LoginEnum.SelectServerStatus _status;

        public SelectServer(Engine.Network.Client client_socket, EngineEnum.LoginEnum.SelectServerStatus status)
        {
            clientsocket = client_socket;
            _status = status;
        }

        /// <summary>
        /// Create 'SelectServer' packet
        /// </summary>
        /// <returns></returns>
        public byte[] CreateBuff()
        {
            byte[] block = new byte[0x12]; // Create our null byte array
            PacketWriter PW = new PacketWriter(block); // Initialize the reader

            PW.WriteByteArray(0, EngineUtils.PacketUtils.calcPacket(block.Length, 0x3EF)); // Write the packet header
            PW.WriteInt32(5, (Int32)_status); // Write the server status (idle)

            return block;
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
