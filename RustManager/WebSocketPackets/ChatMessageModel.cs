using Newtonsoft.Json;

namespace RustManager.WebSocketPackets
{
    class ChatMessageModel
    {
        [JsonProperty("Message")]
        public string ChatMessage;
        public ulong UserID;
        public string Username;
        public string Color;
        public int Time;
    }
}
