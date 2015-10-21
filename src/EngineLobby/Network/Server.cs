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
namespace StreetEngine.Engine.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SilverSock;

    public class lobbyServer
    {
        /// <summary>
        /// lobbyServer definition
        /// </summary>
        private static SilverServer server;

        /// <summary>
        /// New empty list
        /// </summary>
        public static List<lobbyClient> Clients = new List<lobbyClient>();

        /// <summary>
        /// Gets the lobby ip from config file
        /// </summary>
        public static String ip = EngineConfig.IniConfig.Ini.Elements["Lobby"]["Host"];

        /// <summary>
        /// Gets the lobby port from config file
        /// </summary>
        public static Int32 port = int.Parse(EngineConfig.IniConfig.Ini.Elements["Lobby"]["Port"]);

        /// <summary>
        /// Optional message actions
        /// </summary>
        public static Action<String> Event = msg => EngineConsole.Log.Infos("Lobby.Server", msg);
        public static Action<String> Error = msg => EngineConsole.Log.Error(msg);
        public static Action<String> Debug = msg => EngineConsole.Log.Debug(msg);

        /// <summary>
        /// Start the server on the desired ip and port.
        /// </summary>
        public static void Start()
        {
            try
            {
                server = new SilverServer(ip, port);
                server.OnAcceptSocketEvent += new SilverEvents.AcceptSocket(ServerOnAcceptSocketEvent);
                server.OnListeningEvent += new SilverEvents.Listening(ServerOnListeningEvent);
                server.WaitConnection();
            }
            catch (Exception ex) 
            { 
                Error.Invoke((ex.ToString()));
            }
        }

        /// <summary>
        /// Server accept socket event, create a new session for the socket.
        /// </summary>
        /// <param name="socket"></param>
        public static void ServerOnAcceptSocketEvent(SilverSocket socket)
        {
            try
            {
                StreamReader Reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\whitelist.txt");
                lock (Clients)
                {
                    string[] splitIP = socket.IP.Split(':');
                    if (Reader.ReadToEnd().Contains(splitIP[0]))
                    { // IP Whitelisted Check 1/1
                        lobbyClient _client = new lobbyClient(socket); // Add the connected socket to the client list
                        Clients.Add(_client); 

                        // Repeat what we did with world server
                        var client = mmoServer.Clients.Find(x => x.info.ip == splitIP[0]); // Search the right client in the list by IP
                        if (client.info.sessionKey != null)
                        {// If client exists

                            // Switch client's data from world server to lobby server
                            _client.info.ip = client.info.ip;
                            _client.info.id = client.info.id;
                            _client.info.last_login = client.info.last_login;
                            _client.info.type = client.info.type;
                            _client.info.username = client.info.username;
                            _client.info.sessionKey = client.info.sessionKey;
                            _client.info.last_login = client.info.last_login;
                            _client.info.rank = client.info.rank;
                            _client.info.level = client.info.level;
                            _client.info.type = client.info.type;
                            _client.info.exp = client.info.exp;
                            _client.info.licence = client.info.licence;
                            _client.info.gpotatos = client.info.gpotatos;
                            _client.info.rupees = client.info.rupees;
                            _client.info.coins = client.info.coins;
                            _client.info.questpoints = client.info.questpoints;
                            _client.info.bio = client.info.bio;
                            _client.info.s_zone = client.info.s_zone;
                            _client.info.clan_id = client.info.clan_id;
                            _client.info.clan_name = client.info.clan_name;
                        }
                    }
                    else
                    {
                        Error.Invoke("'" + socket.IP + "', is not whitelisted yet.");
                    }
                }
            }
            catch (Exception ex) 
            { 
                Error.Invoke((ex.ToString()));
            }
        }

        /// <summary>
        /// Server is up, send a message.
        /// </summary>
        public static void ServerOnListeningEvent()
        {
            try
            {
                Event.Invoke(" Running "); // Optional
            }
            catch (Exception ex) 
            { 
                Error.Invoke((ex.ToString())); 
            }
        }
    }
}
