using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RustManager.Forms;
using RustManager.Managers;

namespace RustManager.ServerManagement
{
    public class ServerManager
    {
        public static List<ServerConnection> ConnectedServers = new List<ServerConnection>();

        public static void Connect(ServerModel model)
        {
            MainForm.Instance.Tabs.TabPages.RemoveByKey("NoConnectionPage");

            if (ConnectedServers.Any(x => x.ServerInfo.Name == model.Name))
            {
                var tabIndex = MainForm.Instance.Tabs.TabPages.IndexOfKey(model.Name);
                MainForm.Instance.Tabs.SelectTab(tabIndex);

                return;
            }

            var tabManager = TabManager.Instance;
            var page = tabManager.DefaultPage;
            var connection = new ServerConnection(model, tabManager, page);

            page.Name = model.Name;
            page.Text = model.Name;

            tabManager.commandBox.KeyUp += connection.OnCommandBoxKey;
            tabManager.sayBox.KeyUp += connection.OnChatBoxKey;
            MainForm.Instance.Tabs.TabPages.Add(page);

            ConnectedServers.Add(connection);
            connection.Connect();
        }

        public static void Disconnect(ServerConnection connection)
        {
            if (connection == null) return;

            connection.Disconnect();
            ConnectedServers.Remove(connection);
        }

        public static void ConnectToAll(bool connectOnLoad = false)
        {
            var servers = DataFileManager.Data.AllServers.Where(x => (connectOnLoad) ? x.ConnectOnLoad : true);
            servers.ToList().ForEach(x => Connect(x));
        }

        public static ServerConnection FindConnection(string name) => ConnectedServers.FirstOrDefault(x => x.ServerInfo.Name == name);

        public static ServerConnection FindConnection(TabPage page) => ConnectedServers.FirstOrDefault(x => x.Page == page);
    }
}