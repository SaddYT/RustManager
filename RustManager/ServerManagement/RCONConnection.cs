using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using ReactiveSockets;
using RustManager.RconPackets;

namespace RustManager.ServerManagement
{
    public class RconConnection
    {
        private string _address;
        private int _rconPort;
        private string _password;
        private Action<string> _outputFunction;

        private ReactiveClient _client;

        public RconConnection(string ip, int rconPort, string password, Action<string> outputFunction)
        {
            _address = ip;
            _rconPort = rconPort;
            _password = password;
            _outputFunction = outputFunction;
        }

        public void Connect()
        {
            // Connect
            _client = new ReactiveClient(_address, _rconPort);

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

            _outputFunction($"Connecting to {_address}:{_rconPort}...");

            Timer timer = null;
            timer = new Timer((obj) =>
            {
                CheckForConnection();
                timer.Dispose();
            }, null, 5000, Timeout.Infinite);
        }

        public void Disconnect()
        {
            if (_client.IsConnected)
            {
                _client.Disconnect();
            }
        }

        private void CheckForConnection()
        {
            if (_client.IsConnected) return;

            _outputFunction.Invoke($"Failed to connect!");
            _client.Dispose();
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

                        _outputFunction("Successfully authenticated!");
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
            _outputFunction($"Connected! Authenticating...");

            // Send auth
            var authPacket = new AuthPacketModel(_password);
            await _client.SendAsync(authPacket.ToBytes());
        }
    }
}
