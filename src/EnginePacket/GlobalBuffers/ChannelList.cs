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

    public class ChannelList
    {
        public Action<String> Error = msg => EngineConsole.Log.Error(msg);

        Engine.Network.mmoClient mmoclient;
        Engine.Network.lobbyClient lobbyclient;
        public string channelname = "";

        public ChannelList(Engine.Network.mmoClient mmo_client = null, Engine.Network.lobbyClient lobby_client = null, string channel_name = "")
        {
            channelname = channel_name;
            mmoclient = mmo_client;
            lobbyclient = lobby_client;
        }

        /// <summary>
        /// Create 'ChannelList' buffer
        /// </summary>
        /// <returns></returns>
        public byte[] CreateBuff() // Still no multi channel handle
        {
            byte[] block = new byte[0x40]; // Create our null byte array
            PacketWriter PW = new PacketWriter(block); // Initialize the reader

            PW.WriteByteArray(0, EngineUtils.PacketUtils.calcPacket(block.Length, 0x7D6)); // Write the packet header
            PW.WriteString(5, EngineEnum.PacketEnum.PacketCommand.success_0); // Write the success string cmd
            PW.WriteUInt16(29, 1); // I don't know yet what is this 
            PW.WriteUInt16(31, 1); // I don't know yet what is this
            PW.WriteString(32, channelname); // Write channel name 
            PW.WriteUInt32(44, 5); // I don't know yet what is this - multichannel
            PW.WriteUInt32(48, 6); // I don't know yet what is this - multichannel
            PW.WriteUInt32(52, 7); // I don't know yet what is this - multichannel
            PW.WriteUInt32(56, 9); // I don't know yet what is this - multichannel
            PW.WriteUInt32(60, 10); // I don't know yet what is this - multichannel

            if (channelname.Length > 11) // Channel name can't contains over 11 letters
            {
                Error.Invoke("Channel name is too long");
                return null;
            }

            return block;
        }

        /// <summary>
        /// Send a buffer as packet
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
