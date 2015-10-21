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

    public class MateInfo
    {
        public Action<String> Error = msg => EngineConsole.Log.Error(msg);

        public EngineDatabase.DatabaseManager MySQL = new EngineDatabase.DatabaseManager();

        Engine.Network.mmoClient mmoclient;
        Engine.Network.lobbyClient lobbyclient;

        public int chartype = 0;
        public string charname = "";
        public string clanname = "";
        public string clanid = "";
        public int _age = 0;
        public int _level = 0;
        public short _license = 0;
        public string zoneinfo = "";
        public int zoneid = 0;
        public string biostr = "";

        public MateInfo(Engine.Network.mmoClient mmo_client = null, Engine.Network.lobbyClient lobby_client = null)
        {
            mmoclient = mmo_client;
            lobbyclient = lobby_client;
        }

        /// <summary>
        /// Handle the mate info request
        /// </summary>
        /// <param name="data"></param>
        public void Handle(byte[] data)
        {
            string username = MySQL.GetUsernameByData(data); // Search a player name in the packet data we receive
            if(username != null) // If player name exists
            { // Search the selected client in the client list by name
                var selected_client = Engine.Network.mmoServer.Clients.Find(x => x.info.username == MySQL.GetUsernameByData(data));
                if(selected_client.info.sessionKey != null) 
                { // Player exist check 1/2
                    // "Switch" data to create our packet buffer with client infos
                    chartype = selected_client.info.type;
                    charname = selected_client.info.username;
                    clanname = selected_client.info.clan_name;
                    clanid = selected_client.info.clan_id;
                    _age = selected_client.info.age;
                    _level = selected_client.info.level;
                    _license = (Int16)selected_client.info.licence;
                    zoneinfo = selected_client.info.s_zone;
                    zoneid = selected_client.info.c_zone;
                    biostr = selected_client.info.bio;
                }
            }
        }

        /// <summary>
        /// Create a packet buffer
        /// </summary>
        /// <returns></returns>
        public byte[] CreateBuff()
        {
            byte[] block = new byte[0x253]; // Create our null byte array
            PacketWriter PW = new PacketWriter(block); // Initialize the writer

            PW.WriteByteArray(0, EngineUtils.PacketUtils.calcPacket(block.Length, 0x920)); // Write the packet header
            PW.WriteString(5, EngineEnum.PacketEnum.PacketCommand.success_0); // Write the string success cmd
            PW.WriteString(39, charname); // Write the character's name, you can use some html code like: <#ff0000> or <glow>

            if(clanid.Length > 4)
            { // Player's clan id must be under 4 letters
                Error.Invoke("Clan id is too long"); // Optional
                PW.WriteString(156, "null"); // Optional
            } 
            else
            {
                PW.WriteString(156, clanid);
            } // Write the clan's ID, still don't know what is it exactly, I just use "CL#1", its a 4 lenght string

            PW.WriteString(160, clanname); // Write the clan name
            PW.WriteSByte(0x26, (SByte)chartype); // Choose the desired character (luna, etc..)
            PW.WriteSByte(247, (SByte)_age); // Write the age
            PW.WriteInt16(248, (Int16)_level); // Write the level
            PW.WriteInt16(250, (Int16)_license); // Write the license
            PW.WriteInt32(252, (Int32)zoneid); // Write the country (France, etc...) check the list in mate_zip.txt
            PW.WriteString(256, zoneinfo); // Write the zone info
            PW.WriteString(377, biostr); // Write the bio

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
