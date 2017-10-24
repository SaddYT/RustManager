using System.Linq;
using System.Collections.Generic;

using RustManager.Forms;
using RustManager.Managers;
using RustManager.WebSockets;

namespace RustManager.ServerManagement
{
    public class ServerManager
    {
        public static List<ServerConnection> ConnectedServers = new List<ServerConnection>();

        public static void Connect(ServerModel model)
        {
            MainForm.Instance.Tabs.TabPages.RemoveByKey("No Connection");

            if (ConnectedServers.Any(x => x.ServerInfo.Name == model.Name))
            {
                var tabIndex = MainForm.Instance.Tabs.TabPages.IndexOfKey(model.Name);
                MainForm.Instance.Tabs.SelectTab(tabIndex);

                return;
            }

            var tabManager = TabManager.Instance;
            var page = tabManager.DefaultPage;
            page.Name = model.Name;
            page.Text = model.Name;

            if (model.LegacyServer)
            {
                var connection = new ServerConnection(model, tabManager);

                tabManager.commandBox.KeyUp += connection.OnCommandBoxKey;
                tabManager.sayBox.KeyUp += connection.OnChatBoxKey;
                MainForm.Instance.Tabs.TabPages.Add(page);

                ConnectedServers.Add(connection);
                connection.Connect();

                return;
            }
        }
        
        public static void ConnectToAll(bool connectOnLoad = false)
        {
            var servers = DataFileManager.Data.AllServers.Where(x => (connectOnLoad) ? x.ConnectOnLoad : true);
            servers.ToList().ForEach(x => Connect(x));
        }
    }
}