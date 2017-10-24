using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using RustManager.Data;
using RustManager.ServerManagement;

namespace RustManager.Forms
{
    public partial class Manage : Form
    {
        static Manage Instance;

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

            if (string.IsNullOrEmpty(AddressBox.Text) || !IPAddress.TryParse(AddressBox.Text, out IPAddress ipAddress))
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
                (int)ServerPort.Value, (int)RCONPort.Value, PasswordBox.Text, ConnectOnStartupCheck.Checked);
            var search = DataFileSystem.Data.AllServers.Where(x => x.Name == serverItem.Name);

            if (search.Any())
            {
                DataFileSystem.Data.AllServers.Remove(search.First());
            }

            DataFileSystem.Data.AllServers.Add(serverItem);
            DataFileSystem.SaveData();
            
            RefreshServerList();
        }

        void RefreshServerList()
        {
            ServerList.DataSource = null;
            ServerList.DataSource = DataFileSystem.Data.AllServers.Select(x => x.Name).ToList();
        }

        private void ServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentItem = ServerList.Text;

            ServerModel item = null;

            if (!string.IsNullOrEmpty(currentItem))
            {
                item = DataFileSystem.Data.AllServers.FirstOrDefault(x => x.Name == currentItem);
            }

            if (item == null)
            {
                item = new ServerModel();
            }

            NameBox.Text = item.Name;
            AddressBox.Text = item.Address;
            ServerPort.Value = item.Port;
            RCONPort.Value = item.RconPort;
            PasswordBox.Text = item.Password;
            ConnectOnStartupCheck.Checked = item.ConnectOnLoad;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var currentItem = ServerList.Text;

            if (string.IsNullOrEmpty(currentItem))
            {
                MessageBox.Show("There is no server to delete.");
                return;
            }

            var item = DataFileSystem.Data.AllServers.FirstOrDefault(x => x.Name == currentItem);

            if (item == null)
            {
                RefreshServerList();
                return;
            }

            DataFileSystem.Data.AllServers.Remove(item);
            DataFileSystem.SaveData();

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
        }
    }
}
