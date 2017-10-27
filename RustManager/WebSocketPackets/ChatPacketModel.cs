namespace RustManager.WebSocketPackets
{
    class ChatPacketModel : PacketModel
    {
        public string ChatMessage;
        public ulong UserID;
        public string Username;
        public string Color;
        public int Time;

        public void Create(ChatMessageModel model)
        {
            this.ChatMessage = model.ChatMessage;
            this.UserID = model.UserID;
            this.Username = model.Username;
            this.Color = model.Color;
            this.Time = model.Time;
        }
    }
}
