using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RustManager.General;

namespace RustManager.RCONPackets
{
    class RCONPacket
    {
        internal int ID;
        internal RCONPacketType Type;
        internal string Body;

        public RCONPacket()
        {
            this.ID = PacketIDHelper.GeneratePacketID();
        }

        public static RCONPacket ReadData(byte[] data)
        {
            // Skip length header (4 bytes)
            data = data.Skip(4).ToArray();

            // Grab packet ID (4 bytes)
            int packetID = BitConverter.ToInt32(data.Take(4).ToArray(), 0);
            data = data.Skip(4).ToArray();

            // Grab packet type (4 bytes)
            RCONPacketType packetType = (RCONPacketType)BitConverter.ToInt32(data.Take(4).ToArray(), 0);
            data = data.Skip(4).ToArray();

            // Get body, remove last two bytes (string terminator)
            data = data.Take(data.Length - 2).ToArray();
            string packetBody = Encoding.UTF8.GetString(data);

            if (packetType == RCONPacketType.AUTH_RESPONSE)
            {
                return new RCONAuthResponsePacket()
                {
                    ID = packetID,
                    Type = packetType,
                    Body = packetBody
                };
            }

            return new RCONPacket()
            {
                ID = packetID,
                Type = packetType,
                Body = packetBody
            };
        }

        public byte[] ToBytes()
        {
            List<byte> byteList = new List<byte>();

            byte[] reqID = BitConverter.GetBytes(this.ID);
            byte[] type = BitConverter.GetBytes((int)this.Type);
            byte[] data = UTF8Encoding.UTF8.GetBytes(this.Body);
            byte[] whitespace = new byte[] { 0, 0 };

            byte[] length = BitConverter.GetBytes(4 + data.Length);

            byteList.AddRange(length);
            byteList.AddRange(reqID);
            byteList.AddRange(type);
            byteList.AddRange(data);
            byteList.AddRange(whitespace);

            return byteList.ToArray();
        }

        public void Dispose()
        {
            PacketIDHelper.UsedPacket(this.ID);
        }
    }
}
