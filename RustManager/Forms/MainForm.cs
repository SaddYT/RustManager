using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RustManager.Data;
using RustManager.General;
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
            
            General.Events.RegisterEvents();

            RefreshServerList();

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

            ServerItem item = DataFileSystem.Data.AllServers.FirstOrDefault(x => x.Name == ServerList.Text);
            if (item == null)
            {
                RefreshServerList();
                return;
            }

            ServerManager.Connect(item);
        }

        private void TabPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            TabControl control = (TabControl)sender;
            control.SelectTab(control.TabCount - 1);
        }

        private void TabPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = TabPanel.SelectedIndex;
            var page = TabPanel.TabPages[selectedIndex];
            var foundArray = page.Controls.Find("OutputBox", false);
            if (foundArray.Count() == 0) return;
            var outputBox = (TextBox)foundArray[0];

            outputBox.SelectionStart = outputBox.Text.Length;
            outputBox.ScrollToCaret();
        }

        private void ConnectAllButton_Click(object sender, EventArgs e)
        {
            ServerManager.ConnectToAll();
        }

        public void OutputText(string tabName, string text)
        {
            int index = TabPanel.TabPages.IndexOfKey(tabName);
            var page = TabPanel.TabPages[index];
            var foundArray = page.Controls.Find("OutputBox", false);
            if (foundArray.Count() == 0) return;
            var outputBox = (TextBox)foundArray[0];

            if (outputBox.InvokeRequired)
            {
                var callback = new OutputTextCallback(OutputText);
                if (MainForm.Instance.IsDisposed) return;
                Invoke(callback, new object[] { tabName, text });
                return;
            }

            text = text.Replace("\n", "\r\n");

            string currentTime = DateTime.Now.ToShortTimeString();

            outputBox.AppendText($"{currentTime} | {text}\r\n");
        }

        public void RefreshServerList()
        {
            ServerList.DataSource = null;
            ServerList.DataSource = DataFileSystem.Data.AllServers.Select(x => x.Name).ToList();
        }
    }
}