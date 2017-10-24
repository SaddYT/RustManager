using System.Windows.Forms;
using RustManager.Forms;
using RustManager.General;

namespace RustManager.ServerManagement
{
    class ServerConnection
    {
        public ServerItem ServerInfo;
        public TabManager Tab;
        public RCONConnection RCONInstance;

        public ServerConnection(ServerItem item, TabManager tab)
        {
            this.ServerInfo = item;
            this.Tab = tab;
            this.RCONInstance = new RCONConnection(item.IP, item.RconPort, item.Password, (message) => OnMessage(message));
        }

        public void Connect() => RCONInstance.Connect();

        public void Disconnect() => RCONInstance.Disconnect();

        public void OnMessage(string output) => MainForm.Instance.OutputText(ServerInfo.Name, output);

        internal void OnCommandBoxKey(object sender, KeyEventArgs e)
        {
            TextBox textbox = (TextBox)sender;

            if (e.KeyCode != Keys.Enter) return;

            RCONInstance.SendCommand(textbox.Text);
            textbox.Text = "";
        }

        internal void OnChatBoxKey(object sender, KeyEventArgs e)
        {
            TextBox textbox = (TextBox)sender;

            if (e.KeyCode != Keys.Enter) return;

            RCONInstance.SendCommand($"global.say \"{textbox.Text}\"");
            textbox.Text = "";
        }
    }
}
