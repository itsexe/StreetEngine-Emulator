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
namespace StreetEngine.EnginePacket.GlobalBuffers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    public class SuccessResponse
    {
        public Engine.Network.Client clientsocket;
        public Engine.Network.mmoClient mmosocket;
        public Engine.Network.msgClient msgsocket;
        public Engine.Network.lobbyClient lobbysocket;

        public short _header;

        public SuccessResponse(short header, Engine.Network.mmoClient mmo_socket = null, Engine.Network.Client client_socket = null, Engine.Network.lobbyClient lobby_socket = null, Engine.Network.msgClient msg_socket = null)
        {
            clientsocket = client_socket;
            lobbysocket = lobby_socket;
            _header = header;
            mmosocket = mmo_socket;
            msgsocket = msg_socket;
        }

        /// <summary>
        /// Create 'SuccessResponse' packet
        /// </summary>
        /// <returns></returns>
        public byte[] CreateBuff()
        {
            byte[] block = new byte[0x0D]; // Create our null byte array
            PacketWriter PW = new PacketWriter(block); // Initialize the writer

            PW.WriteByteArray(0, EngineUtils.PacketUtils.calcPacket(block.Length, _header)); // Write the packet header
            PW.WriteString(5, EngineEnum.PacketEnum.PacketCommand.success_0); // Write the success string cmd

            return block;
        }

        /// <summary>
        /// Send a packet
        /// </summary>
        public void Send()
        {
            if (clientsocket != null)
                clientsocket.Send(CreateBuff());
            else if (mmosocket != null)
                mmosocket.Send(CreateBuff());
            else if (msgsocket != null)
                msgsocket.Send(CreateBuff());
            else if (lobbysocket != null)
                lobbysocket.Send(CreateBuff());
        }
    }
}
