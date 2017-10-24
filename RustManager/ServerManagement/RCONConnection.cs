using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using ReactiveSockets;
using RustManager.RconPacketModels;

namespace RustManager.ServerManagement
{
    class RconConnection
    {
        private string _ip;
        private int _rconPort;
        private string _password;
        private Action<string> _outputFunction;

        private ReactiveClient _client;

        public RconConnection(string ip, int rconPort, string password, Action<string> outputFunction)
        {
            _ip = ip;
            _rconPort = rconPort;
            _password = password;
            _outputFunction = outputFunction;
        }

        public void Connect()
        {
            // Connect
            _client = new ReactiveClient(_ip, _rconPort);

            // Setup IObservable message recievr 
            var messages = from header in _client.Receiver.Buffer(4) // read packet length
                                           let length = BitConverter.ToInt32(header.ToArray(), 0) // calc. packet length
                                           let body = _client.Receiver.Take(length).ToEnumerable().ToArray() // read body
                                           select header.ToArray().Concat(body).ToArray(); // return original packet

            // Setup events for on new TCP message
            messages.SubscribeOn(TaskPoolScheduler.Default).Subscribe(message => OnClientMessage(message));

            // Connection event & connect
            _client.Connected += Client_Connected;
            _client.ConnectAsync();
        }

        public void Disconnect()
        {
            if (_client.IsConnected)
            {
                _client.Disconnect();
            }
        }

        public async void SendCommand(string command)
        {
            // Create CommandPacket and send
            var commandPacket = new CommandPacketModel(command);
            await _client.SendAsync(commandPacket.ToBytes());
        }

        private void OnClientMessage(byte[] message)
        {
            // Read RCON message 
            PacketModel packet = PacketModel.ReadData(message);

            // Check if packet is server response (0) or invalid 
            // RCON auth response (-1)
            if (packet.Id != 0 && packet.Id != -1) return;

            switch (packet.Type)
            {
                case PacketTypeModel.AUTH_RESPONSE:
                    {
                        // Read packet as AuthResponse
                        var authPacket = packet as AuthReponsePacketModel;

                        if (!authPacket.WasSuccessful())
                        {
                            _outputFunction.Invoke("Failed to connect: incorrect password.");
                            break;
                        }

                        _outputFunction("Successfully connected!");
                        break;
                    }

                case PacketTypeModel.RESPONSE_VALUE:
                    {
                        if (string.IsNullOrEmpty(packet.Body))
                        {
                            break;
                        }

                        _outputFunction(packet.Body);
                        break;
                    }

                case PacketTypeModel.DATA:
                    {
                        if (string.IsNullOrEmpty(packet.Body))
                        {
                            break;
                        }

                        _outputFunction(packet.Body);
                        break;
                    }

                default:
                    {
                        _outputFunction($"Received uncaught packet of type {packet.Type}");
                        break;
                    }
            }

            packet.Dispose();
        }

        private async void Client_Connected(object sender, EventArgs e)
        {
            _outputFunction($"Connecting to {_ip}:{_rconPort}...");

            // Send auth
            var authPacket = new AuthPacketModel(_password);
            await _client.SendAsync(authPacket.ToBytes());
        }
    }
}
