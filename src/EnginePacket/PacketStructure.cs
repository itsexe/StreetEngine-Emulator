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
namespace StreetEngine.EnginePacket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class PacketWriter : BinaryWriter
    {
        public PacketWriter(byte[] packet) : base(new MemoryStream(packet)) {}

        public void WriteInt16(int offset, Int16 value)
        {
            Seek(offset, SeekOrigin.Begin);
            Write((Int16)value);
        }
        public void WriteInt32(int offset, Int32 value)
        {
            Seek(offset, SeekOrigin.Begin);
            Write((Int32)value);
        }
        public void WriteInt64(int offset, Int64 value)
        {
            Seek(offset, SeekOrigin.Begin);
            Write((Int64)value);
        }
        public void WriteUInt16(int offset, UInt16 value)
        {
            Seek(offset, SeekOrigin.Begin);
            Write((UInt16)value);
        }
        public void WriteUInt32(int offset, UInt32 value)
        {
            Seek(offset, SeekOrigin.Begin);
            Write((UInt32)value);
        }
        public void WriteUInt64(int offset, UInt64 value)
        {
            Seek(offset, SeekOrigin.Begin);
            Write((UInt64)value);
        }
        public void WriteByteArray(int offset, Byte[] value)
        {
            Seek(offset, SeekOrigin.Begin);
            Write((Byte[])value);
        }
        public void WriteSByte(int offset, SByte value)
        {
            Seek(offset, SeekOrigin.Begin);
            Write((SByte)value);
        }
        public void WriteString(int offset, String value)
        {
            Byte[] chars
                = ASCIIEncoding.UTF8.GetBytes(value);

            Seek(offset, SeekOrigin.Begin);
            Write((Byte[])chars);
        }
    }

    public class PacketReader : BinaryReader
    {
        public PacketReader(byte[] packet) : base(new MemoryStream(packet)) {}

        public string ReadString(int offset, Int32 size)
        {
            BaseStream.Seek(offset, SeekOrigin.Begin);
            byte[] _string = ReadBytes(size);
            return  System.Text.Encoding.UTF8.GetString(_string);
        }
    }
}
