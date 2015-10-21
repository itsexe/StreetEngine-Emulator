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

    public class mmoServer
    {
        /// <summary>
        /// mmoServer definition
        /// </summary>
        private static SilverServer server;

        /// <summary>
        /// New empty list
        /// </summary>
        public static List<mmoClient> Clients = new List<mmoClient>();

        /// <summary>
        /// Gets the world server ip from config file
        /// </summary>
        public static String ip = EngineConfig.IniConfig.Ini.Elements["World"]["Host"];

        /// <summary>
        /// Gets the world server port from config file
        /// </summary>
        public static Int32 port = int.Parse(EngineConfig.IniConfig.Ini.Elements["World"]["Port"]);

        /// <summary>
        /// Optional message actions
        /// </summary>
        public static Action<String> Event = msg => EngineConsole.Log.Infos("World.Server", msg);
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
                Error.Invoke(ex.ToString()); 
            }
        }

        /// <summary>
        /// Server accept socket event, create a new session for the socket.
        /// </summary>
        /// <remarks>
        /// TODO: 
        /// Clean this mess
        /// </remarks>
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
                        mmoClient _client = new mmoClient(socket); // Add the connected socket to the client list
                        Clients.Add(_client);
                        
                        // That's why we stored the ip in the Auth's acceptsocketevent, because we can
                        // easily get it again here and so we can search the right client in the list.
                        // Because there is still no data in World's client so we can't use the search function
                        // for selected client, so we must use the IP thing.
                        var client = Server.Clients.Find(x => x.info.ip == splitIP[0]); // Search the right client in the list by IP
                        if (client.info.sessionKey != null)
                        { // If client exists

                            // Switch client's data from auth server to world server
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

                            // Switch client's data from auth server to world server
                            _client.info.grind_level = client.info.grind_level;
                            _client.info.backflip_level = client.info.backflip_level;
                            _client.info.frontflip_level = client.info.frontflip_level;
                            _client.info.airtwist_level = client.info.airtwist_level;
                            _client.info.powerswing_level = client.info.powerswing_level;
                            _client.info.gripturn_level = client.info.gripturn_level;
                            _client.info.dash_level = client.info.dash_level;
                            _client.info.backskating_level = client.info.backskating_level;
                            _client.info.jumpingsteer_level = client.info.jumpingsteer_level;
                            _client.info.butting_level = client.info.butting_level;
                            _client.info.powerslide_level = client.info.powerslide_level;
                            _client.info.powerjump_level = client.info.powerjump_level;
                            _client.info.wallride_level = client.info.wallride_level;
                        }
                        else
                        { // If client "does not" exist then we are supposed to think that the client is just trying to join the lobby room
                            var l_client = lobbyServer.Clients.Find(x => x.info.ip == splitIP[0]); // Search the right client in the list by IP
                            if (l_client.info.sessionKey != null)
                            { // If client exists

                                // Switch client's data from world server to lobby server
                                l_client.info.ip = client.info.ip;
                                l_client.info.id = client.info.id;
                                l_client.info.last_login = client.info.last_login;
                                l_client.info.type = client.info.type;
                                l_client.info.username = client.info.username;
                                l_client.info.sessionKey = client.info.sessionKey;
                                l_client.info.last_login = client.info.last_login;
                                l_client.info.rank = client.info.rank;
                                l_client.info.level = client.info.level;
                                l_client.info.type = client.info.type;
                                l_client.info.exp = client.info.exp;
                                l_client.info.licence = client.info.licence;
                                l_client.info.gpotatos = client.info.gpotatos;
                                l_client.info.rupees = client.info.rupees;
                                l_client.info.coins = client.info.coins;
                                l_client.info.questpoints = client.info.questpoints;
                                l_client.info.bio = client.info.bio;
                                l_client.info.s_zone = client.info.s_zone;
                                l_client.info.clan_id = client.info.clan_id;
                                l_client.info.clan_name = client.info.clan_name;

                                // Switch client's data from world server to lobby server
                                l_client.info.grind_level = client.info.grind_level;
                                l_client.info.backflip_level = client.info.backflip_level;
                                l_client.info.frontflip_level = client.info.frontflip_level;
                                l_client.info.airtwist_level = client.info.airtwist_level;
                                l_client.info.powerswing_level = client.info.powerswing_level;
                                l_client.info.gripturn_level = client.info.gripturn_level;
                                l_client.info.dash_level = client.info.dash_level;
                                l_client.info.backskating_level = client.info.backskating_level;
                                l_client.info.jumpingsteer_level = client.info.jumpingsteer_level;
                                l_client.info.butting_level = client.info.butting_level;
                                l_client.info.powerslide_level = client.info.powerslide_level;
                                l_client.info.powerjump_level = client.info.powerjump_level;
                                l_client.info.wallride_level = client.info.wallride_level;
                            }
                        }
                    }
                    else
                    {
                        Error.Invoke("'" + socket.IP + "', is not whitelisted yet."); // Optional message
                    }
                }
            }
            catch (Exception ex) 
            { 
                Error.Invoke(ex.ToString()); 
            }
        }

        /// <summary>
        /// Server is up, send a message.
        /// </summary>
        public static void ServerOnListeningEvent()
        {
            try
            {
                Event.Invoke(" Running" );
            }
            catch (Exception ex) 
            { 
                Error.Invoke(ex.ToString()); 
            }
        }
    }
}
