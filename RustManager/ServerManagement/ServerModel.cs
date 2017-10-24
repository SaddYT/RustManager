namespace RustManager.ServerManagement
{
    class ServerModel
    {
        public string Name { get; set; } = "";

        public string Address { get; set; } = "";

        public int Port { get; set; } = 0;

        public int RconPort { get; set; } = 0;

        public string Password { get; set; } = "";

        public bool ConnectOnLoad { get; set; } = false;

        public ServerModel() { }

        public ServerModel(string name, string ip, int port, int rconPort, string password, bool connectOnLoad)
        {
            Name = name;
            Address = ip;
            Port = port;
            RconPort = rconPort;
            Password = password;
            ConnectOnLoad = connectOnLoad;
        }
    }
}
