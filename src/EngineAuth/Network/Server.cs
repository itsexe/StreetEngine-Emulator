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

    public class Server
    {
        /// <summary>
        /// Auth server definition
        /// </summary>
        public static SilverServer server;

        /// <summary>
        /// New empty list
        /// </summary>
        public static List<Client> Clients = new List<Client>();

        /// <summary>
        /// Gets the auth ip from config file
        /// </summary>
        public static String ip = EngineConfig.IniConfig.Ini.Elements["Auth"]["Host"];

        /// <summary>
        /// Gets the auth port from config file
        /// </summary>
        public static Int32 port = Int32.Parse(EngineConfig.IniConfig.Ini.Elements["Auth"]["Port"]);

        /// <summary>
        /// Optional message actions
        /// </summary>
        public static Action<String> Event = msg => EngineConsole.Log.Infos("Auth.Server", msg);
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
        /// Check the socket's ip in the whitelist + add it to the clients list
        /// </summary>
        /// <param name="socket"></param>
        public static void ServerOnAcceptSocketEvent(SilverSocket socket)
        {
            try
            {
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\whitelist.txt"))
                {
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\whitelist.txt","127.0.0.1");
                }
                StreamReader Reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\whitelist.txt");
                lock (Clients)
                {
                    string[] splitIP = socket.IP.Split(':');
                    if (Reader.ReadToEnd().Contains(splitIP[0]))
                    { // IP Whitelisted check 1/1
                        Client _client = new Client(socket); // Add the connected socket to the client list
                        Clients.Add(_client);

                        _client.info.ip = splitIP[0]; // splitIP[0] = players ip, splitIP[1] = players port
                    }
                    else
                    {
                        Error.Invoke("'" + socket.IP + "', is not whitelisted yet."); // Optional
                        
                    }
                    Reader.Dispose(); // Optional
                }
            }
            catch (Exception ex)
            { 
                Error.Invoke(ex.ToString()); 
            }
        }

        /// <summary>
        /// Server is up, send a message
        /// </summary>
        public static void ServerOnListeningEvent()
        {
            try 
            {
                Event.Invoke(" Running "); // Optional
            }
            catch (Exception ex) 
            { 
                Error.Invoke(ex.ToString());
            }
        }
    }
}
