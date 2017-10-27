using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RustManager.Managers;

namespace RustManager.SourceRconPackets
{
    public class PacketModel
    {
        internal int Id;
        internal PacketTypeModel Type;
        internal string Body;

        public PacketModel()
        {
            Id = PacketManager.GeneratePacketID();
        }

        public static PacketModel ReadData(byte[] data)
        {
            // Skip length header (4 bytes)
            data = data.Skip(4).ToArray();

            // Grab packet ID (4 bytes)
            var packetID = BitConverter.ToInt32(data.Take(4).ToArray(), 0);
            data = data.Skip(4).ToArray();

            // Grab packet type (4 bytes)
            var packetType = (PacketTypeModel)BitConverter.ToInt32(data.Take(4).ToArray(), 0);
            data = data.Skip(4).ToArray();

            // Get body, remove last two bytes (string terminator)
            data = data.Take(data.Length - 2).ToArray();
            string packetBody = Encoding.UTF8.GetString(data);

            if (packetType == PacketTypeModel.AUTH_RESPONSE)
            {
                return new AuthReponsePacketModel()
                {
                    Id = packetID,
                    Type = packetType,
                    Body = packetBody
                };
            }

            return new PacketModel()
            {
                Id = packetID,
                Type = packetType,
                Body = packetBody
            };
        }

        public byte[] ToBytes()
        {
            var byteList = new List<byte>();

            var reqID = BitConverter.GetBytes(Id);
            var type = BitConverter.GetBytes((int)Type);
            var data = Encoding.UTF8.GetBytes(Body);
            var whitespace = new byte[] { 0, 0 };
            var length = BitConverter.GetBytes(4 + data.Length);

            byteList.AddRange(length);
            byteList.AddRange(reqID);
            byteList.AddRange(type);
            byteList.AddRange(data);
            byteList.AddRange(whitespace);

            return byteList.ToArray();
        }

        public void Dispose()
        {
            PacketManager.UsedPacket(Id);
        }
    }
}
