using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RustManager.Data;
using RustManager.Forms;
using RustManager.General;

namespace RustManager.ServerManagement
{
    class ServerManager
    {
        public static List<ServerConnection> ConnectedServers = new List<ServerConnection>();

        public static void Connect(ServerItem item)
        {
            MainForm.Instance.Tabs.TabPages.RemoveByKey("No Connection");

            if (ConnectedServers.Any(x => x.ServerInfo.Name == item.Name))
            {
                int tabIndex = MainForm.Instance.Tabs.TabPages.IndexOfKey(item.Name);
                MainForm.Instance.Tabs.SelectTab(tabIndex);
                return;
            }

            var tabManager = TabManager.Instance;
            TabPage page = tabManager.DefaultPage;
            page.Name = item.Name;
            page.Text = item.Name;

            ServerConnection connection = new ServerConnection(item, tabManager);

            tabManager.commandBox.KeyUp += connection.OnCommandBoxKey;
            tabManager.sayBox.KeyUp += connection.OnChatBoxKey;
            MainForm.Instance.Tabs.TabPages.Add(page);

            ConnectedServers.Add(connection);
            connection.Connect();
        }
        
        public static void ConnectToAll(bool connectOnLoad = false)
        {
            var servers = DataFileSystem.Data.AllServers.Where(x => (connectOnLoad) ? x.ConnectOnLoad : true);
            servers.ToList().ForEach(x => ServerManager.Connect(x));
        }
    }
}