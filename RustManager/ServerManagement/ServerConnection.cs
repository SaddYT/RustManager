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
        public RconConnection RconInstance;

        public ServerConnection(ServerModel model, TabManager tab, TabPage page)
        {
            ServerInfo = model;
            Tab = tab;
            Page = page;
            RconInstance = new RconConnection(model.Address, model.RconPort, model.Password, (message) => OnMessage(message));
        }

        public void Connect() => RconInstance.Connect();

        public void Disconnect() => RconInstance.Disconnect();

        public void OnMessage(string output) => MainForm.Instance.OutputText(ServerInfo.Name, output);
        
        internal void OnCommandBoxKey(object sender, KeyEventArgs e)
        {
            var textbox = sender as TextBox;

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            RconInstance.SendCommand(textbox.Text);
            textbox.Text = "";
        }

        internal void OnChatBoxKey(object sender, KeyEventArgs e)
        {
            var textbox = sender as TextBox;

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            RconInstance.SendCommand($"global.say \"{textbox.Text}\"");
            textbox.Text = "";
        }     
    }
}
