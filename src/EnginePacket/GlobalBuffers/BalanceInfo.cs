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
    using EnginePacket;

    public class BalanceInfo
    {
        Engine.Network.mmoClient mmoclient;
        Engine.Network.lobbyClient lobbyclient;

        public Int32 _coins = 0;
        public Int32 _rupees = 0;
        public Int32 _gpotatos = 0;
        public Int32 _trickpoints = 0;

        public BalanceInfo(Engine.Network.mmoClient mmo_client = null, Engine.Network.lobbyClient lobby_client = null, Int32 coins = 0, Int32 rupees = 0, Int32 gpotatos = 0, Int32 trickpoints = 0)
        {
            mmoclient = mmo_client;
            lobbyclient = lobby_client;
            _coins = coins;
            _rupees = rupees;
            _gpotatos = gpotatos;
            _trickpoints = trickpoints;
        }

        /// <summary>
        /// Create 'BalanceInfo' packet
        /// </summary>
        /// <returns></returns>
        public byte[] CreateBuff()
        {
            byte[] block = new byte[0x2D]; // Create our null byte array
            PacketWriter PW = new PacketWriter(block); // Initialize the writer

            PW.WriteByteArray(0, EngineUtils.PacketUtils.calcPacket(block.Length, 0x82F)); // Write the packet header
            PW.WriteString(5, EngineEnum.PacketEnum.PacketCommand.success_0); // Writer the success string cmd
            PW.WriteUInt32(29, (UInt32)_rupees); // Write the rupees number
            PW.WriteUInt32(33, (UInt32)_coins); // Write the coins number
            PW.WriteUInt32(37, (UInt32)_gpotatos); // Write the gpotatos number
            PW.WriteUInt32(41, (UInt32)_trickpoints); // Write the trickpoints number

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
