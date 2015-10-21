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

    public class PlayerInfo
    {
        Engine.Network.mmoClient mmoclient;
        Engine.Network.lobbyClient lobbyclient;

        public sbyte _level;

        public PlayerInfo(Engine.Network.mmoClient mmo_client = null, Engine.Network.lobbyClient lobby_client = null, sbyte level = 0)
        {
            mmoclient = mmo_client;
            lobbyclient = lobby_client;
            _level = level;
        }

        /// <summary>
        /// Create 'PlayerInfo' packet
        /// </summary>
        /// <returns></returns>
        public byte[] CreateBuff()
        {
            byte[] block = new byte[0xD7]; // Create our null byte array
            PacketWriter PW = new PacketWriter(block); // Initialize the writer

            PW.WriteByteArray(0, EngineUtils.PacketUtils.calcPacket(block.Length, 0x90A)); // Write the packet header
            PW.WriteSByte(5, 4); // I don't know yet what is it
            PW.WriteSByte(9, 0x2D); // I don't know yet what is it
            PW.WriteString(11, EngineEnum.PacketEnum.PacketCommand.success_0); // Write the success cmd string
            PW.WriteSByte(41, 5); // I don't know yet what is it
            PW.WriteSByte(45, 1); // I don't know yet what is it
            PW.WriteSByte(47, 0x32); // I don't know yet what is it
            PW.WriteString(49, "ID1_Testo_2"); // I don't know yet what is it
            PW.WriteString(90, "ID1_Test"); // I don't know yet what is it
            PW.WriteSByte(99, 2); // I don't know yet what is it
            PW.WriteSByte(101, 0xC); // I don't know yet what is it
            PW.WriteSByte(103, 5); // I don't know yet what is it
            PW.WriteSByte(105, 5); // I don't know yet what is it
            PW.WriteSByte(107, _level); // Write the player level
            PW.WriteSByte(115, 4); // I don't know yet what is it
            PW.WriteSByte(117, 0xC); // I don't know yet what is it
            PW.WriteSByte(119, 5); // I don't know yet what is it
            PW.WriteInt32(123, 6); // I don't know yet what is it
            PW.WriteInt32(127, 7); // I don't know yet what is it
            PW.WriteSByte(131, 0x40); // I don't know yet what is it
            PW.WriteSByte(133, 0x10); // I don't know yet what is it
            PW.WriteSByte(135, 7); // I don't know yet what is it
            PW.WriteSByte(139, 8); // I don't know yet what is it
            PW.WriteSByte(143, 9); // I don't know yet what is it
            PW.WriteSByte(147, 10); // I don't know yet what is it
            PW.WriteInt16(152, 11266); // I don't know yet what is it
            PW.WriteInt16(200, 3076); // I don't know yet what is it
            PW.WriteSByte(203, 2); // I don't know yet what is it
            PW.WriteSByte(207, 3); // I don't know yet what is it
            PW.WriteSByte(211, 3); // I don't know yet what is it

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
