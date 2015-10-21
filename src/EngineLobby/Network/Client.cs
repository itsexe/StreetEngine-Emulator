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
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Threading;
    using SilverSock;

    public class lobbyClient
    {
        /// <summary>
        /// Lobby client socket
        /// </summary>
        public static SilverSocket Socket { get; set;}

        /// <summary>
        /// Player struct definition for lobby clients
        /// </summary>
        public EngineGame.Player.PlayerStruct.Information info = new EngineGame.Player.PlayerStruct.Information();

        /// <summary>
        /// Optional message actions
        /// </summary>
        public static Action<String> Event = msg => EngineConsole.Log.Infos("Lobby.Client", msg);
        public static Action<String> Error = msg => EngineConsole.Log.Error(msg);
        public static Action<String> Debug = msg => EngineConsole.Log.Debug(msg);

        public lobbyClient(SilverSocket socket)
        {
            Socket = socket;
            Socket.OnDataArrivalEvent += new SilverEvents.DataArrival(SocketOnDataArrivalEvent);
            Socket.OnSocketClosedEvent += new SilverEvents.SocketClosed(SocketOnClosedEvent);
            Socket.OnConnected += new SilverEvents.Connected(SocketOnConnected);
        }

        /// <summary>
        /// Socket connected event, you can put anything there.
        /// </summary>
        public void SocketOnConnected() 
        {
            this.info.isConnected = true;
            this.info.isInLobby = true;
            this.info.isInWorld = false;
            this.info.isInAuth = false;
        }

        /// <summary>
        /// Socket closed event, you can put anything there.
        /// </summary>
        public void SocketOnClosedEvent()
        {
            lock (lobbyServer.Clients)
            {
                if (lobbyServer.Clients.Contains(this))
                {
                    this.info.isConnected = false;
                    this.info.isInLobby = false;

                    Event.Invoke("'" + this.info.username + "', left the server.");

                    Thread.Sleep(1250);

                    lobbyServer.Clients.Remove(this);
                }
                Socket.CloseSocket();
            }
        }

        /// <summary>
        /// Socket data arrival event, handle game's data.
        /// </summary>
        /// <param name="data"></param>
        public void SocketOnDataArrivalEvent(byte[] data)
        {
            try 
            {
                EngineLobby.Packet.PacketHandle.HandleData(data, this);
            }
            catch (Exception ex) 
            {
                Error.Invoke(ex.ToString());
            }
        }

        /// <summary>
        /// Send a packet buff.
        /// </summary>
        /// <param name="Packet"></param>
        public void Send(byte[] Packet)
        {
            try 
            {
                Socket.Send(Packet);
            }
            catch (Exception ex) 
            {
                Error.Invoke(ex.ToString()); 
            }
        }
    }
}
