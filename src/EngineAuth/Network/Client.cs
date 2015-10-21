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
    using SilverSock;
    using System.Threading;

    public class Client
    {
        /// <summary>
        /// Auth client socket
        /// </summary>
        public static SilverSocket Socket { get; set; }

        /// <summary>
        /// PlayerStruct definition for auth clients
        /// </summary>
        public EngineGame.Player.PlayerStruct.Information info = new EngineGame.Player.PlayerStruct.Information();

        /// <summary>
        /// Optional message actions
        /// </summary>
        public static Action<String> Event = msg => EngineConsole.Log.Infos("Auth.Client", msg);
        public static Action<String> Error = msg => EngineConsole.Log.Error(msg);
        public static Action<String> Debug = msg => EngineConsole.Log.Debug(msg);

        public Client(SilverSocket socket)
        {
            Socket = socket;
            Socket.OnDataArrivalEvent += new SilverEvents.DataArrival(SocketOnDataArrivalEvent);
            Socket.OnSocketClosedEvent += new SilverEvents.SocketClosed(SocketOnClosedEvent);
            Socket.OnConnected += new SilverEvents.Connected(SocketOnConnected);
        }

        /// <summary>
        /// Active some booleans for server informations
        /// </summary>
        public void SocketOnConnected() 
        {
            //lock(Server.Clients)
            //{
                this.info.isConnected = true;
                this.info.isInAuth = true;
                this.info.isInWorld = false;
                this.info.isInLobby = false;
            //}
        }

        /// <summary>
        /// Disconnect a socket connection
        /// </summary>
        public void SocketOnClosedEvent()
        {
            lock(Server.Clients)
            {
                if (Server.Clients.Contains(this))
                {
                    this.info.isConnected = false;
                    this.info.isInAuth = false;

                    Console.Write('\n');
                    Event.Invoke("'" + this.info.username + "', left the server.");

                    Thread.Sleep(1250);

                    Server.Clients.Remove(this);
                }
                Socket.CloseSocket();
            }
        }

        /// <summary>
        /// Handle auth server packets
        /// </summary>
        /// <param name="data"></param>
        public void SocketOnDataArrivalEvent(byte[] data)
        {
            try 
            {
                EngineAuth.PacketHandle.HandleData(data, this);
            }
            catch (Exception ex) 
            { 
                Error.Invoke(ex.ToString()); 
            }
        }

        /// <summary>
        /// Send a packet
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

        /// <summary>
        /// Close any socket connection
        /// </summary>
        public void Close()
        {
            Socket.CloseSocket();
        }
    }
}
