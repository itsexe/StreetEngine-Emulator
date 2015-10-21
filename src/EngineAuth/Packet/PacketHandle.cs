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
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public class PacketHandle
    {
        /// <summary>
        /// Handle socket's data from auth recv.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Socket"></param>
        public static void HandleData(byte[] data, Engine.Network.Client Socket)
        {
            // Initialize our packets
            EngineAuth.ServerList ServerList = new EngineAuth.ServerList(Socket, EngineConfig.IniConfig.Ini.Elements["Auth"]["Host"], Int32.Parse(EngineConfig.IniConfig.Ini.Elements["Lobby"]["Port"]), Int32.Parse(EngineConfig.IniConfig.Ini.Elements["Msg"]["Port"]), Int32.Parse(EngineConfig.IniConfig.Ini.Elements["World"]["Port"]), (SByte)1, 800, Int16.Parse(EngineConfig.IniConfig.Ini.Elements["WorldSettings"]["MaximumUsers"]));
            EngineAuth.SelectServer SelectServer = new EngineAuth.SelectServer(Socket, EngineEnum.LoginEnum.SelectServerStatus.CONNECTION_SUCCESS);
            EngineAuth.HandleLogin HandleLogin = new EngineAuth.HandleLogin(Socket, data);

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

                switch (header) // If my code detect a header
                {
                    case ((Int16)EngineEnum.HeadersEnum.Recv.GLOBAL_TCP_RECV.KEEP_ALIVE): Thread.Sleep(345); break; // I still don't know what to do there
                    case ((Int16)EngineEnum.HeadersEnum.Recv.TM_SC.TM_SC_WE_LOGIN_RECV): new Thread(() => HandleLogin.HandleBuff()).Start(); break; // Connect or no the user
                    case ((Int16)EngineEnum.HeadersEnum.Recv.TM_SC.TM_SC_SERVER_LIST_RECV): new Thread(() => ServerList.Send()).Start(); break; // Connect the user's game to our server
                    case ((Int16)EngineEnum.HeadersEnum.Recv.TM_SC.TM_SC_SELECT_SERVER_RECV): new Thread(() => SelectServer.Send()).Start(); break; // Select the server the user just connected
                }
            }
        }
    }
}
