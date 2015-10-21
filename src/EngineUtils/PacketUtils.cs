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
namespace StreetEngine.EngineUtils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    /// <summary>
    /// I do not use my packet structure to read here because it doesn't works like I thought it will works.
    /// </summary>
    public class PacketUtils
    {
        /// <summary>
        /// Calc packet header (first 5 bytes), made by itsexe.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static byte[] calcPacket(int size, Int16 header)
        {
            byte[] calc_packet = new byte[size];
            EnginePacket.PacketWriter PW = new EnginePacket.PacketWriter(calc_packet);
            PW.WriteInt32(0, size);
            PW.WriteInt16(2, header);
            byte[] len = BitConverter.GetBytes((UInt16)(calc_packet.Length));
            byte[] head = BitConverter.GetBytes(header);
            byte[] hash = BitConverter.GetBytes((UInt32)(Convert.ToDouble(len[0]) + Convert.ToDouble(len[1]) + Convert.ToDouble(head[0]) + Convert.ToDouble(head[1])));
            PW.WriteByteArray(4, hash);
            return calc_packet;
        }
    }
}
