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

    public class ServerList
    {
        public Engine.Network.Client clientsocket;
        public string _ip = "";
        public int msgport = 0;
        public int lobbyport = 0;
        public int mmoport = 0;
        public short unkclientnumb = 0;
        public short maximumusers = 0;
        public sbyte serverid = 0;

        public ServerList(Engine.Network.Client client_socket, string ip, int msg_port, int lobby_port, int mmo_port, sbyte server_id, Int16 unk_clientnumb, Int16 maximum_users)
        {
            clientsocket = client_socket;
            _ip = ip;
            msgport = msg_port;
            lobbyport = lobby_port;
            mmoport = mmo_port;
            serverid = server_id;
            unkclientnumb = unk_clientnumb;
            maximumusers = maximum_users;
        }

        /// <summary>
        /// Create 'ServerList' packet
        /// </summary>
        /// <returns></returns>
        public byte[] CreateBuff()
        {
            byte[] block = new byte[0x4F]; // Create our null byte array
            PacketWriter PW = new PacketWriter(block); // Initialize the writer

            PW.WriteByteArray(0, EngineUtils.PacketUtils.calcPacket(block.Length, 0x3F4)); // Write the packet header
            PW.WriteSByte(5, 1); // I don't know what is it
            PW.WriteSByte(7, serverid); // Write the server id number - Int16?
            PW.WriteSByte(9, 2); // I don't know what is it
            PW.WriteSByte(11, 3); // I don't know what is it
            PW.WriteSByte(13, 4); // I don't know what is it
            PW.WriteString(15, _ip); // Write the msg ip
            PW.WriteString(31, _ip); // Write the lobby ip
            PW.WriteString(47, _ip); // Write the mmo ip 
            PW.WriteInt16(63, (Int16)lobbyport); // Write the lobby port
            PW.WriteInt16(67, (Int16)msgport); // Write the msg port
            PW.WriteInt16(71, (Int16)mmoport); // Write the mmo port
            PW.WriteInt16(75, (Int16)unkclientnumb); // Still don't know what is it, something to do with players
            PW.WriteInt16(77, (Int16)maximumusers); // Write maximum users that can join the server

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
