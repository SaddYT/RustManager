namespace RustManager.ServerManagement
{
    public class ServerModel
    {
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public int Port { get; set; } = 0;
        public int RconPort { get; set; } = 0;
        public string Password { get; set; } = "";
        public bool ConnectOnLoad { get; set; } = false;
        public bool LegacyServer { get; set; } = false;

        public ServerModel() { }

        public ServerModel(string name, string ip, int port, int rconPort, string password, bool connectOnLoad, bool legacyServer)
        {
            Name = name;
            Address = ip;
            Port = port;
            RconPort = rconPort;
            Password = password;
            ConnectOnLoad = connectOnLoad;
            LegacyServer = legacyServer;
        }
    }
}
