using System.Windows.Forms;
using RustManager.Forms;
using RustManager.Managers;

namespace RustManager.ServerManagement
{
    public class ServerConnection
    {
        public ServerModel ServerInfo;
        public TabManager Tab;
        public TabPage Page;
        public SourceRconConnection RconInstance;
        public WebSocketConnection WebSocketInstance;

        public ServerConnection(ServerModel model, TabManager tab, TabPage page)
        {
            ServerInfo = model;
            Tab = tab;
            Page = page;
        }

        public void Connect()
        {
            if (ServerInfo.LegacyServer)
            {
                if (RconInstance == null)
                {
                    RconInstance = new SourceRconConnection(ServerInfo.Address, ServerInfo.RconPort, ServerInfo.Password, message => OnMessage(message));
                }

                RconInstance.Connect();
                return;
            }

            WebSocketInstance = new WebSocketConnection(ServerInfo.Address, ServerInfo.RconPort, ServerInfo.Password, message => OnMessage(message));
            WebSocketInstance.Connect();
        }

        public void Disconnect()
        {
            if (ServerInfo.LegacyServer)
            {
                RconInstance?.Disconnect();
                return;
            }

            WebSocketInstance?.Disconnect();
        }

        public void OnMessage(string output) => MainForm.Instance.OutputText(ServerInfo.Name, output);

        internal void OnCommandBoxKey(object sender, KeyEventArgs e)
        {
            var textbox = sender as TextBox;

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            SendCommand(textbox.Text);
            textbox.Text = "";
        }

        internal void OnChatBoxKey(object sender, KeyEventArgs e)
        {
            var textbox = sender as TextBox;

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            SendCommand($"global.say \"{textbox.Text}\"");
            textbox.Text = "";
        }

        private void SendCommand(string command)
        {
            if (ServerInfo.LegacyServer)
            {
                RconInstance.SendCommand(command);
                return;
            }

            WebSocketInstance.SendCommand(command);
        }
    }
}
