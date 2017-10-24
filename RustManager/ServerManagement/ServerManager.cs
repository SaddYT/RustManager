using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RustManager.Managers;
using RustManager.Forms;
using RustManager.Managers;

namespace RustManager.ServerManagement
{
    class ServerManager
    {
        public static List<ServerConnection> ConnectedServers = new List<ServerConnection>();

        public static void Connect(ServerModel item)
        {
            MainForm.Instance.Tabs.TabPages.RemoveByKey("No Connection");

            if (ConnectedServers.Any(x => x.ServerInfo.Name == item.Name))
            {
                var tabIndex = MainForm.Instance.Tabs.TabPages.IndexOfKey(item.Name);
                MainForm.Instance.Tabs.SelectTab(tabIndex);

                return;
            }

            var tabManager = TabManager.Instance;
            var page = tabManager.DefaultPage;
            page.Name = item.Name;
            page.Text = item.Name;

            var connection = new ServerConnection(item, tabManager);

            tabManager.commandBox.KeyUp += connection.OnCommandBoxKey;
            tabManager.sayBox.KeyUp += connection.OnChatBoxKey;
            MainForm.Instance.Tabs.TabPages.Add(page);

            ConnectedServers.Add(connection);
            connection.Connect();
        }
        
        public static void ConnectToAll(bool connectOnLoad = false)
        {
            var servers = DataFileManager.Data.AllServers.Where(x => (connectOnLoad) ? x.ConnectOnLoad : true);
            servers.ToList().ForEach(x => Connect(x));
        }
    }
}