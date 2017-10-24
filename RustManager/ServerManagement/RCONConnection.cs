using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using ReactiveSockets;
using RustManager.RCONPackets;

namespace RustManager.ServerManagement
{
    class RCONConnection
    {
        private string IP;
        private int RconPort;
        private string Password;
        private Action<string> OutputFunction;

        private ReactiveClient Client;

        public RCONConnection(string ip, int rconPort, string password, Action<string> outputFunction)
        {
            this.IP = ip;
            this.RconPort = rconPort;
            this.Password = password;
            this.OutputFunction = outputFunction;
        }

        public void Connect()
        {
            // Connect
            Client = new ReactiveClient(IP, RconPort);

            // Setup IObservable message recievr 
            IObservable<byte[]> messages = from header in Client.Receiver.Buffer(4) // read packet length
                                           let length = BitConverter.ToInt32(header.ToArray(), 0) // calc. packet length
                                           let body = Client.Receiver.Take(length).ToEnumerable().ToArray() // read body
                                           select header.ToArray().Concat(body).ToArray(); // return original packet

            // Setup events for on new TCP message
            messages.SubscribeOn(TaskPoolScheduler.Default).Subscribe(message => OnClientMessage(message));

            // Connection event & connect
            Client.Connected += Client_Connected;
            Client.ConnectAsync();
        }

        public void Disconnect()
        {
            if (Client.IsConnected)
            {
                Client.Disconnect();
            }
        }

        public void SendCommand(string command)
        {
            // Create CommandPacket and send
            RCONCommandPacket commandPacket = new RCONCommandPacket(command);
            Client.SendAsync(commandPacket.ToBytes());
        }

        private void OnClientMessage(byte[] message)
        {
            // Read RCON message 
            RCONPacket packet = RCONPacket.ReadData(message);

            // Check if packet is server response (0) or invalid 
            // RCON auth response (-1)
            if (packet.ID != 0 && packet.ID != -1) return;

            switch (packet.Type)
            {
                case RCONPacketType.AUTH_RESPONSE:
                    {
                        // Read packet as AuthResponse
                        RCONAuthResponsePacket authPacket = packet as RCONAuthResponsePacket;
                        if (!authPacket.WasSuccessful())
                        {
                            OutputFunction.Invoke("Failed to connect: incorrect password.");
                            break;
                        }
                        OutputFunction.Invoke("Successfully connected!");
                        break;
                    }

                case RCONPacketType.RESPONSE_VALUE:
                    {
                        if (string.IsNullOrEmpty(packet.Body)) break;
                        OutputFunction.Invoke(packet.Body);
                        break;
                    }

                case RCONPacketType.DATA:
                    {
                        if (string.IsNullOrEmpty(packet.Body)) break;
                        OutputFunction.Invoke(packet.Body);
                        break;
                    }

                default:
                    {
                        OutputFunction.Invoke($"Received uncaught packet of type {packet.Type}");
                        break;
                    }
            }

            packet.Dispose();
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            OutputFunction.Invoke($"Connecting to {IP}:{RconPort}...");

            // Send auth
            RCONAuthPacket authPacket = new RCONAuthPacket(Password);
            Client.SendAsync(authPacket.ToBytes());
        }
    }
}
