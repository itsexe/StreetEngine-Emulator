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

    public class InventoryItems
    {
        Engine.Network.mmoClient mmoclient;
        Engine.Network.lobbyClient lobbyclient;

        EngineEnum.PlayerEnum.CharactersType chartype;

        public InventoryItems(Engine.Network.mmoClient mmo_client = null, Engine.Network.lobbyClient lobby_client = null, EngineEnum.PlayerEnum.CharactersType char_type = 0)
        {
            mmoclient = mmo_client;
            lobbyclient = lobby_client;
            chartype = char_type;
        }

        /// <summary>
        /// Create the selected character type items list
        /// </summary>
        /// <param name="data"></param>
        /// <param name="char_type"></param>
        public void CreateItemList(byte[] data, EngineEnum.PlayerEnum.CharactersType char_type)
        {
            // Initializing the packet writer
            PacketWriter PW = new PacketWriter(data);

            // Initializing basic stuff
            SByte unknow_number = 0x0A; 
            Int32 item_number = 0; 

            // Initializing items list
            List<int> rookieItemList = new List<int>();
            List<int> lunaItemList = new List<int>();
            List<int> rushItemList = new List<int>();
            List<int> tippyItemList = new List<int>();

            // Initializing inventory enums
            EngineEnum.IventoryEnum.ROOKIE_ITEM_LIST[] rookie_items = (EngineEnum.IventoryEnum.ROOKIE_ITEM_LIST[])Enum.GetValues(typeof(EngineEnum.IventoryEnum.ROOKIE_ITEM_LIST));
            EngineEnum.IventoryEnum.LUNA_ITEM_LIST[] luna_items = (EngineEnum.IventoryEnum.LUNA_ITEM_LIST[])Enum.GetValues(typeof(EngineEnum.IventoryEnum.LUNA_ITEM_LIST));
            EngineEnum.IventoryEnum.RUSH_ITEM_LIST[] rush_items = (EngineEnum.IventoryEnum.RUSH_ITEM_LIST[])Enum.GetValues(typeof(EngineEnum.IventoryEnum.RUSH_ITEM_LIST));
            EngineEnum.IventoryEnum.TIPPY_ITEM_LIST[] tippy_items = (EngineEnum.IventoryEnum.TIPPY_ITEM_LIST[])Enum.GetValues(typeof(EngineEnum.IventoryEnum.TIPPY_ITEM_LIST));

            switch (char_type)
            {
                case EngineEnum.PlayerEnum.CharactersType.CHARACTER_ROOKIE:
                    item_number = rookie_items.Count();
                    for (int i = 0x24, j = 0; i < (item_number * 0x1D) && j < item_number; i += 0x1D, j++)
                    {
                        PW.WriteInt16(i, (Int16)rookie_items[j]);
                    }
                    break; // Load rookie items
                case EngineEnum.PlayerEnum.CharactersType.CHARACTER_LUNA:
                    item_number = luna_items.Count();
                    for (int i = 0x24, j = 0; i < (item_number * 0x1D) && j < item_number; i += 0x1D, j++)
                    {
                        PW.WriteInt16(i, (Int16)luna_items[j]);
                    }
                    break; // Load luna items
                case EngineEnum.PlayerEnum.CharactersType.CHARACTER_RUSH:
                    item_number = rush_items.Count();
                    for (int i = 0x24, j = 0; i < (item_number * 0x1D) && j < item_number; i += 0x1D, j++)
                    {
                        PW.WriteInt16(i, (Int16)rush_items[j]);
                    }
                    break; // Load rush items
                case EngineEnum.PlayerEnum.CharactersType.CHARACTER_TIPPY:
                    item_number = tippy_items.Count();
                    for (int i = 0x24, j = 0; i < (item_number * 0x1D) && j < item_number; i += 0x1D, j++)
                    {
                        PW.WriteInt16(i, (Int16)tippy_items[j]);
                    }
                    break; // Load tippy items
                case EngineEnum.PlayerEnum.CharactersType.CHARACTER_KLAUS: // There is still no avaiable items for klaus
                case EngineEnum.PlayerEnum.CharactersType.CHARACTER_KARA: // There is still no avaiable items for kara
                    break;
            }
            PW.WriteInt32(30, item_number);

            // Write some unknow number for now (each items) maybe something to do with duration?
            for (int i = 0x2E; i < (item_number * 0x1D); i += 0x1D)
            {
                PW.WriteSByte(i, unknow_number);
            }
            // Write the position of each items
            for (int i = 0x20, j = 0; i < (item_number * 0x1D) && j < (item_number * 0x1D); i += 0x1D, j++)
            {
                PW.WriteInt16(i, (Int16)j);
            }
        }

        /// <summary>
        /// Create 'InventoryItems' packet
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public byte[] CreateBuff()
        {
            byte[] block = new byte[0x118F]; // Create our null byte array
            PacketWriter PW = new PacketWriter(block); // Initialize the writer

            PW.WriteByteArray(0, EngineUtils.PacketUtils.calcPacket(block.Length, 0x833)); // Write the packet header
            PW.WriteString(5, EngineEnum.PacketEnum.PacketCommand.success_0); // Write the string success cmd

            switch (chartype)
            {
                case EngineEnum.PlayerEnum.CharactersType.CHARACTER_ROOKIE:
                    CreateItemList(block, EngineEnum.PlayerEnum.CharactersType.CHARACTER_ROOKIE);
                    break;
                case EngineEnum.PlayerEnum.CharactersType.CHARACTER_LUNA:
                    CreateItemList(block, EngineEnum.PlayerEnum.CharactersType.CHARACTER_LUNA);
                    break;
                case EngineEnum.PlayerEnum.CharactersType.CHARACTER_RUSH:
                    CreateItemList(block, EngineEnum.PlayerEnum.CharactersType.CHARACTER_RUSH);
                    break;
                case EngineEnum.PlayerEnum.CharactersType.CHARACTER_TIPPY:
                    CreateItemList(block, EngineEnum.PlayerEnum.CharactersType.CHARACTER_TIPPY);
                    break;
                case EngineEnum.PlayerEnum.CharactersType.CHARACTER_KLAUS: // There is still no avaiable items for klaus
                case EngineEnum.PlayerEnum.CharactersType.CHARACTER_KARA: // There is still no avaiable items for kara
                    break;
            }

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
