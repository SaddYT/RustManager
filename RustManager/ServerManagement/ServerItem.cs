namespace RustManager.ServerManagement
{
    class ServerItem
    {
        public string Name { get; set; } = "";

        public string IP { get; set; } = "";

        public int Port { get; set; } = 0;

        public int RconPort { get; set; } = 0;

        public string Password { get; set; } = "";

        public bool ConnectOnLoad { get; set; } = false;

        public ServerItem() { }

        public ServerItem(string name, string ip, int port, int rconPort, string password, bool connectOnLoad)
        {
            this.Name = name;
            this.IP = ip;
            this.Port = port;
            this.RconPort = rconPort;
            this.Password = password;
            this.ConnectOnLoad = connectOnLoad;
        }
    }
}
