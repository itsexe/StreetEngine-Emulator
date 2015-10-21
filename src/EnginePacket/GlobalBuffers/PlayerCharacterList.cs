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

    public class PlayerCharacterList
    {
        Engine.Network.mmoClient mmoclient;
        Engine.Network.lobbyClient lobbyclient;

        public string charname = "";
        public bool lastlogin = false;

        public EngineEnum.PlayerEnum.CharactersType chartype;

        public PlayerCharacterList(Engine.Network.mmoClient mmo_client = null, Engine.Network.lobbyClient lobby_client = null, string char_name = "", EngineEnum.PlayerEnum.CharactersType char_type = 0, bool last_login = true)
        {
            mmoclient = mmo_client;
            lobbyclient = lobby_client;
            charname = char_name;
            chartype = char_type;
            lastlogin = last_login;
        }

        /// <summary>
        /// Create 'PlayerCharacterList' packet
        /// </summary>
        /// <returns></returns>
        public byte[] CreateBuff()
        {
            byte[] block = new byte[0x120]; // Create our null byte array
            PacketWriter PW = new PacketWriter(block); // Initialize the writer

            PW.WriteByteArray(0, EngineUtils.PacketUtils.calcPacket(block.Length, 0x90E)); // Write the packet header
            PW.WriteString(5, EngineEnum.PacketEnum.PacketCommand.success_0); // Write the success string cmd
            PW.WriteString(30, charname); // Write the character name, you can use some html code like: <#ff0000> and <glow>
            PW.WriteInt32(73, lastlogin ? 1 : 0); // Push the player in the account creation tab
            PW.WriteUInt32(75, 0); // I don't know yet what is it
            PW.WriteUInt32(79, (UInt32)chartype); // Write the selected character
            PW.WriteUInt32(83, 0); // I don't know yet what is it

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
