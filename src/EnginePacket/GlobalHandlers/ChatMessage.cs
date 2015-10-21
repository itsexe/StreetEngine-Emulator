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
namespace StreetEngine.EnginePacket.GlobalHandlers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public class ChatMessage
    {
        Engine.Network.mmoClient mmoclient;
        Engine.Network.lobbyClient lobbyclient;

        public List<string> Commands = new List<string>();

        public byte[] _data;

        public ChatMessage(byte[] data, Engine.Network.mmoClient mmo_client = null, Engine.Network.lobbyClient lobby_client = null)
        {
            _data = data;
            mmoclient = mmo_client;
            lobbyclient = lobby_client;
        }

        /// <summary>
        /// Handle chat messages
        /// </summary>
        /// <remarks>
        /// TODO:
        /// Clean command handle code
        /// </remarks>
        public void Handle()
        {
            // Create our command list
            Commands.Add(EngineEnum.CommandsEnum.CommandName.command_a);

            // Initialize our responses
            EnginePacket.GlobalBuffers.SuccessResponse mSuccessResponse = new EnginePacket.GlobalBuffers.SuccessResponse((Int16)EngineEnum.HeadersEnum.Send.BM_SC.BM_SC_CHAT_MESSAGE, mmo_socket: mmoclient);
            EnginePacket.GlobalBuffers.SuccessResponse bSuccessResponse = new EnginePacket.GlobalBuffers.SuccessResponse((Int16)EngineEnum.HeadersEnum.Send.BM_SC.BM_SC_CHAT_MESSAGE, lobby_socket: lobbyclient);

            string message = Encoding.ASCII.GetString(_data);

            if (message.Length > 1)
            { // Message check 1/1
                if (Commands.Any(x => message.Contains(x)))
                { // Command check 1/2
                    if (message.Replace("\0", "").Contains(EngineEnum.CommandsEnum.CommandName.command_a))
                    { // Command check 2/2
                        if(mmoclient.info.rank == EngineEnum.PlayerEnum.RankInfo.rank_a || mmoclient.info.rank == EngineEnum.PlayerEnum.RankInfo.rank_b)
                        { // Privileges check 1/1
                            var Reader = new BinaryReader(new MemoryStream(_data));
                            Reader.BaseStream.Seek(51, SeekOrigin.Begin);

                            int parsed_level_numb = int.Parse(System.Text.Encoding.UTF8.GetString(Reader.ReadBytes(2)).Replace("\0", ""));

                            EnginePacket.GlobalBuffers.LevelInfo mLevelInfo = new EnginePacket.GlobalBuffers.LevelInfo(mmo_client: mmoclient, level: parsed_level_numb, license: 4, exp: 102400);
                            EnginePacket.GlobalBuffers.LevelInfo lLevelInfo = new EnginePacket.GlobalBuffers.LevelInfo(lobby_client: lobbyclient, level: parsed_level_numb, license: 4, exp: 102400);

                            if (mmoclient != null)
                                new Thread(() => mLevelInfo.Send()).Start();
                            else if (lobbyclient != null)
                                new Thread(() => lLevelInfo.Send()).Start();
                        } // Send command
                    }
                }
                else
                { // Send response packet
                    if (mmoclient != null)
                        new Thread(() => mSuccessResponse.Send()).Start();
                    else if (lobbyclient != null)
                        new Thread(() => bSuccessResponse.Send()).Start();
                }
            }
        }
    }
}
