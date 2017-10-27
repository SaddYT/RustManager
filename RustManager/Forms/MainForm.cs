using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RustManager.Managers;
using RustManager.ServerManagement;

namespace RustManager.Forms
{
    public partial class MainForm : Form
    {
        public static MainForm Instance;
        public TabControl Tabs;

        private delegate void OutputTextCallback(string tabName, string text);

        public MainForm()
        {
            InitializeComponent();

            Instance = this;
            Tabs = TabPanel;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AssemblyManager.Initialize();

            Managers.EventManager.RegisterEvents();

            RefreshServerList();

            var page = TabManager.Instance.DefaultPage;
            TabPanel.TabPages.Add(page);

            ServerManager.ConnectToAll(true);
        }

        private void ManageButton_Click(object sender, EventArgs e)
        {
            Manage.ShowForm();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ServerList.Text))
            {
                MessageBox.Show("You must first add a server before connecting.");
                return;
            }

            var item = DataFileManager.Data.AllServers.FirstOrDefault(x => x.Name == ServerList.Text);

            if (item == null)
            {
                RefreshServerList();
                return;
            }

            ServerManager.Connect(item);
        }

        private void TabPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            var control = sender as TabControl;
            control.SelectTab(control.TabCount - 1);
        }

        private void TabPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedIndex = TabPanel.SelectedIndex;
            if (selectedIndex == -1) return;
            var page = TabPanel.TabPages[selectedIndex];
            var foundArray = page.Controls.Find("OutputBox", false);

            if (foundArray.Count() == 0) return;

            var outputBox = foundArray[0] as TextBox;

            outputBox.SelectionStart = outputBox.Text.Length;
            outputBox.ScrollToCaret();
        }

        private void ConnectAllButton_Click(object sender, EventArgs e)
        {
            ServerManager.ConnectToAll();
        }

        public void OutputText(string tabName, string text)
        {
            var page = Tools.FindTabPage(TabPanel, tabName);
            if (page == null) return;
            var foundArray = page.Controls.Find("OutputBox", false);

            if (foundArray.Count() == 0) return;

            var outputBox = foundArray[0] as TextBox;

            if (outputBox.InvokeRequired)
            {
                if (Instance.IsDisposed) return;

                var callback = new OutputTextCallback(OutputText);
                this.Invoke(callback, tabName, text);
                return;
            }

            text = text.Replace("\n", "\r\n");

            var currentTime = DateTime.Now.ToShortTimeString();

            outputBox.AppendText($"{currentTime} | {text}\r\n");
        }

        public void Disconnect(string tabName)
        {
            var page = Tools.FindTabPage(TabPanel, tabName);
            if (page == null) return;

            var connection = ServerManager.FindConnection(page);
            if (connection == null) return;

            ServerManager.Disconnect(connection);
            TabPanel.TabPages.Remove(page);

            if (TabPanel.TabPages.Count == 0)
            {
                var defaultPage = TabManager.Instance.DefaultPage;
                TabPanel.TabPages.Add(defaultPage);
            }
        }

        public void RefreshServerList()
        {
            ServerList.DataSource = null;
            ServerList.DataSource = DataFileManager.Data.AllServers.Select(x => x.Name).ToList();
        }

        private void TabPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right && e.Button != MouseButtons.Middle) return;

            for (int i = 0; i < TabPanel.TabPages.Count; i++)
            {
                if (TabPanel.GetTabRect(i).Contains(e.Location))
                {
                    var tab = TabPanel.TabPages[i];

                    if (e.Button == MouseButtons.Right)
                    {
                        tab.ContextMenu = new ContextMenu();
                        tab.ContextMenu.MenuItems.Add(new MenuItem("Disconnect", (s, evt) => Disconnect(tab.Name)));
                        tab.ContextMenu.Show(TabPanel, e.Location);
                        return;
                    }

                    if (e.Button == MouseButtons.Middle)
                    {
                        Disconnect(tab.Name);
                        return;
                    }
                }
            }
        }
    }
}