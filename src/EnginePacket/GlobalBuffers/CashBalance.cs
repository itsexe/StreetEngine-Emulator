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
    using StreetEngine.EnginePacket;

    public class CashBalance
    {
        Engine.Network.mmoClient mmoclient;
        Engine.Network.lobbyClient lobbyclient;

        public Int32 _gpotatos;

        public CashBalance(Engine.Network.mmoClient mmo_client = null, Engine.Network.lobbyClient lobby_client = null, Int32 gpotatos = 0)
        {
            _gpotatos = gpotatos;
            mmoclient = mmo_client;
            lobbyclient = lobby_client;
        }

        /// <summary>
        /// Create 'CashBalance' packet
        /// </summary>
        /// <returns></returns>
        public byte[] CreateBuff()
        {
            byte[] block = new byte[0x21]; // Create our null byte array
            PacketWriter PW = new PacketWriter(block); // Initialize the reader

            PW.WriteByteArray(0, EngineUtils.PacketUtils.calcPacket(block.Length, 0x8E0)); // Create the packet header
            PW.WriteString(5, EngineEnum.PacketEnum.PacketCommand.success_0); // Write the success string cmd
            PW.WriteInt32(29, _gpotatos); // Write the gpotatos number

            return block;
        }
        
        /// <summary>
        /// Send a packet
        /// </summary>
        public void Send()
        {
            if (mmoclient != null)
                mmoclient.Send(CreateBuff());
            else if (lobbyclient != null)
                lobbyclient.Send(CreateBuff());
        }
    }
}
