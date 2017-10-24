namespace RustManager.WebSockets
{
    public class PacketModel
    {
        public int Identifier { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }

        public PacketModel(string message) : this(1, message, "RustManager")
        {

        }

        public PacketModel(int identifier, string message, string name)
        {
            Identifier = identifier;
            Message = message;
            Name = name;
        }
    }
}
