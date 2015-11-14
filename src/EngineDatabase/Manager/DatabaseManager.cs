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
namespace StreetEngine.EngineDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using MySql.Data.MySqlClient;

    public class DatabaseManager
    {
        /// <summary>
        /// Useful MySql definitions..
        /// </summary>
        public static MySqlConnection mysql_connection;
        public static MySqlCommand mysql_command;
        public static MySqlDataReader Reader;

        /// <summary>
        /// New empty list
        /// </summary>
        public static List<String> usersList = new List<String>();

        /// <summary>
        /// New empty list
        /// </summary>
        public static List<String> usernames = new List<String>();

        /// <summary>
        /// New empty string
        /// </summary>
        public static String multiple = "";

        /// <summary>
        /// Optional message actions
        /// </summary>
        public static Action<String> Event = msg => EngineConsole.Log.Infos("MySQL.Manager", msg);
        public static Action<String> Error = msg => EngineConsole.Log.Error(msg);
        public static Action<String> Debug = msg => EngineConsole.Log.Debug(msg);

        #region MySQL
        /// <summary>
        /// Returns the string sql connection from config file
        /// </summary>
        public string GetConnectionString
        {
            get
            {
                return "host=" + EngineConfig.IniConfig.Ini.Elements["Database"]["Host"] + ";"
                    + "user=" + EngineConfig.IniConfig.Ini.Elements["Database"]["User"] + ";"
                    + "password=" + EngineConfig.IniConfig.Ini.Elements["Database"]["Password"] + ";"
                    + "database=" + EngineConfig.IniConfig.Ini.Elements["Database"]["Database"] + ";";
            }
        }

        /// <summary>
        /// Returns the string account sql file name
        /// </summary>
        public string GetSqlFileString
        {
            get
            {
                return "SELECT * FROM `sg_account`";
            }
        }

        /// <summary>
        /// Execute a SQL command.
        /// </summary>
        /// <param name="command"></param>
        public void ExecuteCommand(string command)
        {
            mysql_command.CommandText = command;
            mysql_command.ExecuteNonQuery();
        }

        /// <summary>
        /// Update any row of the desired player's id with any value.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="value"></param>
        /// <param name="id"></param>
        public void UpdateTable(string row, string value, int id)
        {
            ExecuteCommand("Update " + "sg_account"
                + " SET " + row + "='" + value + "' WHERE id='" + id + "'");
        }

        /// <summary>
        /// Open connection to the database and connect to the sg_account sql file.
        /// </summary>
        public void OpenConnection()
        {
            mysql_connection = new MySqlConnection(GetConnectionString); // Initialize connection
            mysql_command = new MySqlCommand(GetSqlFileString); // Initialise sql command

            mysql_command.Connection = mysql_connection; // Initialize connection

            Reader = null; // We must null the reader before opening any connection

            try
            {
                mysql_connection.Open(); // Open the connection
                Reader = mysql_command.ExecuteReader(); // And finally initalize our reader

                while (Reader.Read())
                {
                    // Reader is reading so we must close it once its finished
                    usersList.Add(Reader["id"].ToString()); // Add every id to our user list
                    usernames.Add(Reader["user"].ToString()); // Add every username to our user list
                }
                Event.Invoke("Connection etablished"); // Simple message

                if (usersList.Count > 1 || usersList.Count > usersList.Count + 1) // Check if the user list is over 1
                    multiple = "s"; // if so add a 's' to user

                Event.Invoke("'" + usersList.Count + "' User" + multiple + " loaded"); // Optional message

                Reader.Close(); // Close the reader
            }
            catch (MySqlException)
            {
                Error.Invoke("Could not connect to MySQL! Please check your Databasesettings!");
            }
            catch (Exception x) 
            { 
                Error.Invoke(x.ToString()); 
            }
        }
        #endregion

        #region AccountManipulation
        /// <summary>
        /// Check if the account exist in the database.
        /// </summary>
        /// <remarks>
        /// TODO:
        /// Fix the login issue when a player 
        /// with the same password of
        /// another player try to connect
        /// </remarks>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="socket"></param>
        public void CheckUserAccount(string username, string password, Engine.Network.Client socket)
        {
            Reader.Close();

            // Initialize our packet
            EnginePacket.GlobalBuffers.ErrorMessage ErrorMessage = new EnginePacket.GlobalBuffers.ErrorMessage(EngineEnum.HeadersEnum.Send.TM_SC_RESULT.MSG_SERVER_NOT_EXIST, client_socket: socket);

            try
            {
                Reader = mysql_command.ExecuteReader();

                while (Reader.Read())
                { // Reader is reading so we must close it once its finished
                    if (usernames.Find(x => x == username) == username) // Check the user in the database
                    { // Account login check 1/3
                        if(EngineSecurity.Encryption.MD5_Encryption.CreateMD5(password) == (Reader["password"].ToString())) // Check the password in the database. Must be md5 encrypted
                        { // Account login check 2/3
                            socket.info.last_login = (Int32)Reader["first_login"]; // Store the first_login boolean into the player struct
                            // Account login check 3/3
                            switch (socket.info.last_login) // Check if the player already connected before
                            {
                                case 0: // He did not connected before
                                    CreateAccount(socket); // Create a new account in the database (ingame part is still in progress...)
                                    break;
                                case 1: // He connected before
                                    ReadEntries(socket);  // Load existing account data
                                    break;
                            }

                            if(socket.info.sessionKey != null)
                            {
                                EngineAuth.Login Login = new EngineAuth.Login(socket.info.sessionKey, socket);
                                new Thread(() => Login.Send()).Start(); // Connect the player with his auth key
                            }
                        }
                     }
                     else
                     {
                        // Packet has now changed so we must change the position of both username/password
                        socket.info.username_position = 5;
                        socket.info.password_position = 18;

                        new Thread(() => ErrorMessage.Send()).Start(); // Push an error message
                     }
                }
                Reader.Close(); // Close the reader loop
            }
            catch (Exception ex) { Error.Invoke(ex.ToString()); }
        }
        #endregion

        #region AccountData
        /// <summary>
        /// Read entries of the database and store them in the player struct.
        /// </summary>
        /// <remarks>
        /// TODO:
        /// Clean this mess up
        /// </remarks>
        /// <param name="client"></param>
        public void ReadEntries(Engine.Network.Client client)
        {
            String[] tricksLevel = Reader["char_tricks"].ToString().Split('|'); // Gets tricks level

            // Load basic stuff
            client.info.id = (Int32)Reader["id"];
            client.info.sessionKey = (String)Reader["auth_key"].ToString();
            client.info.rank = (String)Reader["char_rank"].ToString();
            client.info.last_login = (Int32)Reader["first_login"];
            client.info.level = (Int32)Reader["char_level"];
            client.info.type = (Int32)Reader["char_type"];
            client.info.exp = (Int32)Reader["char_exp"];
            client.info.licence = (Int32)Reader["char_liscence"];
            client.info.gpotatos = (Int32)Reader["char_gpotatos"];
            client.info.rupees = (Int32)Reader["char_rupees"];
            client.info.coins = (Int32)Reader["char_coins"];
            client.info.questpoints = (Int32)Reader["char_questpoints"];
            client.info.bio = (String)Reader["char_bio"];
            client.info.s_zone = (String)Reader["char_zoneinfo"];
            client.info.clan_id = (String)Reader["char_clanid"];
            client.info.clan_name = (String)Reader["char_clanname"];

            // Parse each tricks level to player struct
            client.info.grind_level= Int32.Parse(tricksLevel[0]);
            client.info.backflip_level = Int32.Parse(tricksLevel[1]);
            client.info.frontflip_level = Int32.Parse(tricksLevel[2]);
            client.info.airtwist_level = Int32.Parse(tricksLevel[3]);
            client.info.powerswing_level = Int32.Parse(tricksLevel[4]);
            client.info.gripturn_level = Int32.Parse(tricksLevel[5]);
            client.info.dash_level = Int32.Parse(tricksLevel[6]);
            client.info.backskating_level = Int32.Parse(tricksLevel[7]);
            client.info.jumpingsteer_level = Int32.Parse(tricksLevel[8]);
            client.info.butting_level = Int32.Parse(tricksLevel[9]);
            client.info.powerslide_level = Int32.Parse(tricksLevel[10]);
            client.info.powerjump_level = Int32.Parse(tricksLevel[11]);
            client.info.wallride_level = Int32.Parse(tricksLevel[12]);

            // Privileges check
            if(client.info.rank == EngineEnum.PlayerEnum.RankInfo.rank_a) client.info.username = "[ADM] " + (String)Reader["username"].ToString();
            else if(client.info.rank == EngineEnum.PlayerEnum.RankInfo.rank_b) client.info.username = "[GM] " + (String)Reader["username"].ToString();
            else if (client.info.rank == EngineEnum.PlayerEnum.RankInfo.rank_c) client.info.username = (String)Reader["username"].ToString();
            else if (client.info.rank == EngineEnum.PlayerEnum.RankInfo.rank_e) client.info.username = "Bot";

            Debug.Invoke("'" + client.info.username + "', has logged"); // Optional
        }

        /// <summary>
        /// Insert "start" values to the new account created.
        /// </summary>
        /// <remarks>
        /// TODO:
        /// Simply make this "CreateAccount" code in .php
        /// </remarks>
        /// <param name="client"></param>
        public void CreateAccount(Engine.Network.Client client)
        {
            client.info.sessionKey = EngineGame.Player.PlayerStruct.GenerateSessionKey(); // Generate player's unique session key

            Reader.Close(); // Close the reader to be able to use UpdateTable function

            UpdateTable("auth_key", client.info.sessionKey, client.info.id);
            UpdateTable("first_login", EngineEnum.LoginEnum.LoginStatus.second_time, client.info.id);
            UpdateTable("char_type", EngineConfig.IniConfig.Ini.Elements["WorldSettings"]["startType"], client.info.id);
            UpdateTable("char_level", EngineConfig.IniConfig.Ini.Elements["WorldSettings"]["startLevel"], client.info.id);
            UpdateTable("char_liscence", EngineConfig.IniConfig.Ini.Elements["WorldSettings"]["startLiscence"], client.info.id);
            UpdateTable("char_gpotatos", EngineConfig.IniConfig.Ini.Elements["WorldSettings"]["startGpotatos"], client.info.id);
            UpdateTable("char_rupees", EngineConfig.IniConfig.Ini.Elements["WorldSettings"]["startRupees"], client.info.id);
            UpdateTable("char_coins", EngineConfig.IniConfig.Ini.Elements["WorldSettings"]["startCoins"], client.info.id);
            UpdateTable("char_questpoints", EngineConfig.IniConfig.Ini.Elements["WorldSettings"]["startQuestPoints"], client.info.id);

            Reader = mysql_command.ExecuteReader(); // Open the MySQL Reader again

            Debug.Invoke("'" + client.info.username + "', has logged");
        }
        #endregion

        /// <summary>
        /// Search and returns player username
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetUsernameByData(byte[] data)
        {
            byte[] packet = new byte[0x21CF];
            data.CopyTo(packet, 0);
            for (int i = 0; i < Engine.Network.mmoServer.Clients.Count; i++)
            {
                if (System.Text.Encoding.ASCII.GetString(packet).Replace("\0", "").Contains(Engine.Network.mmoServer.Clients[i].info.username))
                {
                    return Engine.Network.mmoServer.Clients[i].info.username;
                }
            }
            return "Admin"; // Just to make sure the emulator doesn't crash if an error happen
        }
    }
}
