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
namespace StreetEngine.EngineLobby.Packet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Threading;

    public class PacketHandle
    {
        public static EngineDatabase.DatabaseManager MySQL = new EngineDatabase.DatabaseManager();

        /// <summary>
        /// Handle socket's data from lobby room recv.
        /// </summary>
        /// <remarks>
        /// TODO:
        /// Clean this mess
        /// </remarks>
        /// <param name="data"></param>
        /// <param name="Socket"></param>
        public static void HandleData(byte[] data, Engine.Network.lobbyClient Socket)
        {
            // Initialize our handles
            EnginePacket.GlobalHandlers.ChatMessage HandleChatMessage = new EnginePacket.GlobalHandlers.ChatMessage(data, lobby_client: Socket);

            // Initialize our packets
            EnginePacket.GlobalBuffers.PlayerCharacterList PlayerCharacterList = new EnginePacket.GlobalBuffers.PlayerCharacterList(lobby_client: Socket, char_name: Socket.info.username, char_type: (EngineEnum.PlayerEnum.CharactersType)Socket.info.type, last_login: Convert.ToBoolean(Socket.info.last_login));
            EnginePacket.GlobalBuffers.TrickList TrickList = new EnginePacket.GlobalBuffers.TrickList(lobby_client: Socket, grind_level: Socket.info.grind_level, backflip_level: Socket.info.backflip_level, frontflip_level: Socket.info.frontflip_level, airtwist_level: Socket.info.airtwist_level, powerswing_level: Socket.info.powerswing_level, gripturn_level: Socket.info.gripturn_level, dash_level: Socket.info.dash_level, backskating_level: Socket.info.backskating_level, jumpingsteer_level: Socket.info.jumpingsteer_level, butting_level: Socket.info.butting_level, powerslide_level: Socket.info.powerslide_level, powerjump_level: Socket.info.powerjump_level, wallride_level: Socket.info.wallride_level);
            EnginePacket.GlobalBuffers.BalanceInfo BalanceInfo = new EnginePacket.GlobalBuffers.BalanceInfo(lobby_client: Socket, coins: Socket.info.coins, rupees: Socket.info.rupees, gpotatos: Socket.info.gpotatos, trickpoints: Socket.info.questpoints);
            EnginePacket.GlobalBuffers.ChannelList ChannelList = new EnginePacket.GlobalBuffers.ChannelList(lobby_client: Socket, channel_name: (String)EngineEnum.WorldEnum.Channel.channel_a);
            EnginePacket.GlobalBuffers.EnterChannel EnterChannel = new EnginePacket.GlobalBuffers.EnterChannel(lobby_client: Socket);
            EnginePacket.GlobalBuffers.CashBalance CashBalance = new EnginePacket.GlobalBuffers.CashBalance(lobby_client: Socket, gpotatos: Socket.info.gpotatos);
            EnginePacket.GlobalBuffers.LevelInfo LevelInfo = new EnginePacket.GlobalBuffers.LevelInfo(lobby_client: Socket, exp: Socket.info.exp, level: Socket.info.level, license: Socket.info.licence);
            EnginePacket.GlobalBuffers.PlayerInfo PlayerInfo = new EnginePacket.GlobalBuffers.PlayerInfo(lobby_client: Socket, level: (SByte)Socket.info.level);
            EnginePacket.GlobalBuffers.InventoryItems InventoryItems = new EnginePacket.GlobalBuffers.InventoryItems(lobby_client: Socket, char_type: (EngineEnum.PlayerEnum.CharactersType)Socket.info.type);
            EnginePacket.GlobalBuffers.MateInfo MateInfo = new EnginePacket.GlobalBuffers.MateInfo(lobby_client: Socket);

            // Initialize our responses packets
            EnginePacket.GlobalBuffers.SuccessResponse[] SuccessResponse =
            {
                new EnginePacket.GlobalBuffers.SuccessResponse((Int16)EngineEnum.HeadersEnum.Send.BM_SC.BM_SC_LOGIN, lobby_socket: Socket),
                new EnginePacket.GlobalBuffers.SuccessResponse((Int16)EngineEnum.HeadersEnum.Send.BM_SC.BM_SC_SELECT_CHARACTER, lobby_socket: Socket),
                new EnginePacket.GlobalBuffers.SuccessResponse((Int16)EngineEnum.HeadersEnum.Send.BM_SC.BM_SC_SELECT_TRICK, lobby_socket: Socket),
                new EnginePacket.GlobalBuffers.SuccessResponse((Int16)EngineEnum.HeadersEnum.Send.BM_SC.BM_SC_ENTER_INVENTORY, lobby_socket: Socket),
                new EnginePacket.GlobalBuffers.SuccessResponse((Int16)EngineEnum.HeadersEnum.Send.BM_SC.BM_SC_LEAVE_INVENTORY, lobby_socket: Socket),
                new EnginePacket.GlobalBuffers.SuccessResponse((Int16)EngineEnum.HeadersEnum.Send.BM_SC.BM_SC_LEAVE_CHANNEL, lobby_socket: Socket),
                new EnginePacket.GlobalBuffers.SuccessResponse((Int16)EngineEnum.HeadersEnum.Send.ID_BZ_SC.ID_BZ_SC_ENTER_LOBBY, lobby_socket: Socket),
                new EnginePacket.GlobalBuffers.SuccessResponse((Int16)EngineEnum.HeadersEnum.Send.BM_SC.BM_SC_CREATE_ROOM, lobby_socket: Socket),
                new EnginePacket.GlobalBuffers.SuccessResponse((Int16)EngineEnum.HeadersEnum.Send.ID_BZ_SC.ID_BZ_SC_ENTER_LOBBY_ROOM, lobby_socket: Socket),
                new EnginePacket.GlobalBuffers.SuccessResponse((Int16)EngineEnum.HeadersEnum.Send.ID_BZ_SC.ID_BZ_SC_UNKNOW_LOBBY, lobby_socket: Socket),
            };

            // That's a quick code I wrote to detect a header, it's a bit laggy but you can find a better way.
            Int16 header = 0;
            byte[] buff = new byte[0x255];
            var Reader = new BinaryReader(new MemoryStream(data));

            for (int Index = 0; Index < data.Length; Index++)
            {
                Reader.BaseStream.Seek((int)Index, SeekOrigin.Begin);
                byte[] headerBuff = Reader.ReadBytes(2);
                headerBuff.CopyTo(buff, 0);
                header = BitConverter.ToInt16(buff, 0);

                switch (header)
                {
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_LOGIN): new Thread(() => SuccessResponse[0].Send()).Start(); break; // Login response because the player left the channel
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_SELECT_CHARACTER): new Thread(() => SuccessResponse[1].Send()).Start(); break;  // Select character response
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_CHAT_MESSAGE): new Thread(() => HandleChatMessage.Handle()).Start(); break; // Send a command or a message
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_PLAYER_INFO): new Thread(()=> PlayerInfo.Send()).Start();  break; // The server need this we don't actually know what this packet can do
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_TRICK_LIST): new Thread(()=> TrickList.Send()).Start();  break; // Send each tricks level to the player
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_BALANCE_INFO): new Thread(()=> BalanceInfo.Send()).Start();  break; // Send player's database balance stuff to the world server
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_GET_CHANNEL_LIST): new Thread(()=> ChannelList.Send()).Start();  break; // Create our channel list, still only one channel coded
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_ENTER_CHANNEL): new Thread(()=> EnterChannel.Send()).Start();  new Thread(()=> CashBalance.Send()).Start(); new Thread(()=> LevelInfo.Send()).Start();  break; // Enter in Parktown and some server-need stuff (cash, level stuff..)
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_INVENTORY): new Thread(()=> InventoryItems.Send()).Start(); break; // Send every items to the player's character type inventory
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_SELECT_TRICK): new Thread(() => SuccessResponse[2].Send()).Start(); break; // Select the desired trick (still in progress, you can't unselect..)
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_ENTER_INVENTORY): new Thread(() => SuccessResponse[3].Send()).Start(); break; // Enter in the inventory room
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_MATE_INFO): new Thread(()=> MateInfo.Handle(data)).Start(); new Thread(()=> MateInfo.Send()).Start(); break; // Show up infos of the selected player -- TODO: update infos code
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_LEAVE_CHANNEL): new Thread(() => SuccessResponse[5].Send()).Start(); break; // Leave the current channel
                    case ((Int16)EngineEnum.HeadersEnum.Recv.ID_BZ_SC.ID_BZ_SC_ENTER_LOBBY): new Thread(() => SuccessResponse[6].Send()).Start(); break; // Enter lobby room response
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_LEAVE_INVENTORY): new Thread(() => SuccessResponse[4].Send()).Start(); break; // Leave inventory and enter the tuning shop
                    case ((Int16)EngineEnum.HeadersEnum.Recv.BM_SC.BM_SC_CREATE_ROOM): new Thread(() => SuccessResponse[7].Send()).Start(); break; // Create a room with the selected options and desired name
                    case ((Int16)EngineEnum.HeadersEnum.Recv.ID_BZ_SC.ID_BZ_SC_ENTER_LOBBY_ROOM): new Thread(() => SuccessResponse[8].Send()).Start(); break; // Enter the lobby waiting room
                    case ((Int16)EngineEnum.HeadersEnum.Recv.ID_BZ_SC.ID_BZ_SC_UNKNOW_LOBBY): new Thread(() => SuccessResponse[9].Send()).Start(); break; // Enter the lobby waiting room
                }
            }
        }
    }
}
