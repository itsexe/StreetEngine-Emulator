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

    public class TrickList
    {
        Engine.Network.mmoClient mmoclient;
        Engine.Network.lobbyClient lobbyclient;

        public List<EngineEnum.PlayerEnum.TrickCode> Tricks = new List<EngineEnum.PlayerEnum.TrickCode>();

        int[] tricksLevel = new int[13];

        public TrickList(Engine.Network.mmoClient mmo_client = null, Engine.Network.lobbyClient lobby_client = null, int grind_level = 0, int backflip_level = 0, int frontflip_level = 0, int airtwist_level = 0, int powerswing_level = 0, int gripturn_level = 0, int dash_level = 0, int backskating_level = 0, int jumpingsteer_level = 0, int butting_level = 0, int powerslide_level = 0, int powerjump_level = 0, int wallride_level = 0)
        {
            mmoclient = mmo_client;
            lobbyclient = lobby_client;

            tricksLevel[0] = grind_level;
            tricksLevel[1] = dash_level;
            tricksLevel[2] = backskating_level;
            tricksLevel[3] = butting_level;
            tricksLevel[4] = powerslide_level;
            tricksLevel[5] = backflip_level;
            tricksLevel[6] = frontflip_level;
            tricksLevel[7] = airtwist_level;
            tricksLevel[8] = powerswing_level;
            tricksLevel[9] = gripturn_level;
            tricksLevel[10] = jumpingsteer_level;
            tricksLevel[11] = powerjump_level;
            tricksLevel[12] = wallride_level;
        }

        /// <summary>
        /// Create 'TrickList' packet
        /// </summary>
        /// <returns></returns>
        public byte[] CreateBuff()
        {
            byte[] block = new byte[0x95]; // Create our null byte array
            PacketWriter PW = new PacketWriter(block); // Initialize the writer

            PW.WriteByteArray(0, EngineUtils.PacketUtils.calcPacket(block.Length, 0x839)); // Write the packet header
            PW.WriteString(5, EngineEnum.PacketEnum.PacketCommand.success_0); // Write the success string cmd

            // Create our trick list
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.GRIND);
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.DASH);
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.BACK_SKATING);
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.BUTTING);
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.POWER_SLIDE);
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.BACK_FLIP);
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.FRONT_FLIP);
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.AIR_TWIST);
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.POWER_SWING);
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.GRIP_TURN);
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.JUMPING_STEER);
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.POWER_JUMP);
            Tricks.Add(EngineEnum.PlayerEnum.TrickCode.WALL_RIDE);

            PW.WriteSByte(30, 0xD); // Write tricks number

            // Write each trick in our packet
            for (int i = 32, j = 0; i < 149 && j < Tricks.Count; i += 9, j++)
            {
                PW.WriteInt16(i, (Int16)Tricks[j]);
            }
            // Write each trick level in our packet
            for (int i = 0x24, j = 0; i < 0x95 && j < tricksLevel.Count(); i += 9, j++)
            {
                PW.WriteInt32(i, (Int32)tricksLevel[j]);
            }
            // Apply each trick (1/0 - Apply Yes/No)
            for (int i = 40; i < 149; i += 9) // TODO: Put this in the database
            {
                PW.WriteSByte(i, 1);
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
