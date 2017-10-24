using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace RustManager.WebSockets
{
    public class ConnectionModel
    {
        private WebSocket _webSocket;

        public ConnectionModel(string url)
        {
            _webSocket = new WebSocket(url);

            _webSocket.OnOpen += OnOpen;
            _webSocket.OnMessage += OnMessage;
            _webSocket.OnError += OnError;
            _webSocket.OnClose += OnClose;
        }

        public bool IsOpen => _webSocket.ReadyState == WebSocketState.Open;

        public void ConnectAsync()
        {
            _webSocket.ConnectAsync();
        }

        public void DisconnectAsync()
        {
            _webSocket.CloseAsync();
        }

        public void SendAsync(string packet, Action<bool> completed)
        {
            _webSocket.SendAsync(packet, completed);
        }

        private void OnOpen(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnClose(object sender, CloseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
