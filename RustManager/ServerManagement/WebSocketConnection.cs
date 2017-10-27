using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RustManager.WebSocketPackets;
using WebSocketSharp;

namespace RustManager.ServerManagement
{
    public class WebSocketConnection
    {
        private string _address;
        private int _rconPort;
        private string _password;
        private Action<string> _outputFunction;

        private WebSocket _webSocket;

        public WebSocketConnection(string ip, int rconPort, string password, Action<string> outputFunction)
        {
            _address = ip;
            _rconPort = rconPort;
            _password = password;
            _outputFunction = outputFunction;
        }

        public bool IsOpen => _webSocket.ReadyState == WebSocketState.Open;

        public void Connect()
        {
            _webSocket = new WebSocket($"ws://{_address}:{_rconPort}/{_password}")
            {
                WaitTime = TimeSpan.FromSeconds(5)
            };

            _webSocket.OnOpen += OnOpen;
            _webSocket.OnMessage += OnMessage;
            _webSocket.OnError += OnError;
            _webSocket.OnClose += OnClose;

            _webSocket.ConnectAsync();

            _outputFunction($"Connecting to {_address}:{_rconPort}...");
        }

        public void Disconnect()
        {
            _webSocket.Close();
        }

        public void SendCommand(string command)
        {
            PacketModel packet = new PacketModel()
            {
                Identifier = 1,
                Message = command,
                Name = "RustManager"
            };
            string packetString = JsonConvert.SerializeObject(packet);
            _webSocket.SendAsync(packetString, null);
        }

        private void OnOpen(object sender, EventArgs e)
        {
            _outputFunction("Successfully connected!");
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            PacketModel packet = PacketModel.ReadData(e.Data);

            if (string.IsNullOrEmpty(packet.Message)) return;

            if (packet.Type == "Chat")
            {
                ChatPacketModel chat = packet as ChatPacketModel;
                _outputFunction($"[CHAT] | {chat.Username} [{chat.UserID}]: {chat.ChatMessage}");
                return;
            }

            if (packet.Type == "Generic")
            {
                _outputFunction(packet.Message);
                return;
            }

            _outputFunction($"Error! Uncaught packet type: {packet.Type}");
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            _outputFunction($"Error: {e.Message}");
        }

        private void OnClose(object sender, CloseEventArgs e)
        {
            if (e.Code == 1002)
            {
                _outputFunction($"Failed to connect: invalid password.");
                return;
            }

            if (e.Code == 1006)
            {
                _outputFunction($"Failed to connect: no response from server.");
                return;
            }

            _outputFunction($"Connection closed. Code: {e.Code}. Reason: {e.Reason}");
        }
    }
}
