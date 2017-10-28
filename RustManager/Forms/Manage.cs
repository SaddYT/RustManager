using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using RustManager.Managers;
using RustManager.ServerManagement;

namespace RustManager.Forms
{
    public partial class Manage : Form
    {
        private static Manage Instance;
        private object ipAddress;

        public Manage()
        {
            InitializeComponent();

            RefreshServerList();
        }

        public static void ShowForm()
        {
            if (Instance == null || Instance.IsDisposed)
            {
                Instance = new Manage();
                Instance.Show();
                return;
            }

            if (Instance.Visible)
            {
                Instance.BringToFront();
                return;
            }

            Instance.Show();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameBox.Text))
            {
                MessageBox.Show("You must enter a server name.");
                return;
            }
            IPAddress ipAddress;
            if (string.IsNullOrEmpty(AddressBox.Text) || IPAddress.TryParse(AddressBox.Text, out ipAddress))
            {
                MessageBox.Show("You must enter a (valid) IP address.");
                return;
            }

            if (string.IsNullOrEmpty(PasswordBox.Text))
            {
                MessageBox.Show("You must enter a password.");
                return;
            }
            
            var serverItem = new ServerModel(NameBox.Text, ipAddress.ToString(),
                (int)ServerPort.Value, (int)RCONPort.Value, PasswordBox.Text, ConnectOnStartupCheck.Checked,
                LegacyServer.Checked);
            var search = DataFileManager.Data.AllServers.Where(x => x.Name == serverItem.Name);

            if (search.Any())
            {
                DataFileManager.Data.AllServers.Remove(search.First());
            }

            DataFileManager.Data.AllServers.Add(serverItem);
            DataFileManager.SaveData();
            
            RefreshServerList();
        }

        void RefreshServerList()
        {
            ServerList.DataSource = null;
            ServerList.DataSource = DataFileManager.Data.AllServers.Select(x => x.Name).ToList();
        }

        private void ServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentItem = ServerList.Text;

            ServerModel model = null;

            if (!string.IsNullOrEmpty(currentItem))
            {
                model = DataFileManager.Data.AllServers.FirstOrDefault(x => x.Name == currentItem);
            }

            if (model == null)
            {
                model = new ServerModel();
            }

            NameBox.Text = model.Name;
            AddressBox.Text = model.Address;
            ServerPort.Value = model.Port;
            RCONPort.Value = model.RconPort;
            PasswordBox.Text = model.Password;
            ConnectOnStartupCheck.Checked = model.ConnectOnLoad;
            LegacyServer.Checked = model.LegacyServer;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var currentItem = ServerList.Text;

            if (string.IsNullOrEmpty(currentItem))
            {
                MessageBox.Show("There is no server to delete.");
                return;
            }

            var item = DataFileManager.Data.AllServers.FirstOrDefault(x => x.Name == currentItem);

            if (item == null)
            {
                RefreshServerList();
                return;
            }

            DataFileManager.Data.AllServers.Remove(item);
            DataFileManager.SaveData();

            RefreshServerList();
        }

        private void Manage_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.Instance.RefreshServerList();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            NameBox.Text = "";
            AddressBox.Text = "";
            ServerPort.Value = 0;
            RCONPort.Value = 0;
            PasswordBox.Text = "";
            ConnectOnStartupCheck.Checked = false;
            LegacyServer.Checked = false;
        }
    }
}
