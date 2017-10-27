using Newtonsoft.Json;

namespace RustManager.WebSocketPackets
{
    public class PacketModel
    {
        public int Identifier;
        public string Message;
        public string Type;
        public string Stacktrace;
        public string Name;

        public static PacketModel ReadData(string message)
        {
            PacketModel model = JsonConvert.DeserializeObject<PacketModel>(message);

            if (model.Type == "Chat")
            {
                ChatMessageModel chatMessage = JsonConvert.DeserializeObject<ChatMessageModel>(model.Message);

                ChatPacketModel chatPacket = new ChatPacketModel()
                {
                    Identifier = model.Identifier,
                    Message = model.Message,
                    Type = model.Type,
                    Stacktrace = model.Stacktrace,
                    Name = model.Name
                };
                chatPacket.Create(chatMessage);

                return chatPacket;
            }

            return model;
        }
    }
}
