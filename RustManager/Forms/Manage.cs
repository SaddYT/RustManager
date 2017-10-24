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
            if (Manage.Instance == null || Manage.Instance.IsDisposed)
            {
                Manage.Instance = new Manage();
                Manage.Instance.Show();
                return;
            }

            if (Manage.Instance.Visible)
            {
                Manage.Instance.BringToFront();
                return;
            }

            Manage.Instance.Show();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameBox.Text))
            {
                MessageBox.Show("You must enter a server name.");
                return;
            }

            if (string.IsNullOrEmpty(IPBox.Text) || !IPAddress.TryParse(IPBox.Text, out IPAddress ipAddress))
            {
                MessageBox.Show("You must enter a (valid) IP address.");
                return;
            }

            if (string.IsNullOrEmpty(PasswordBox.Text))
            {
                MessageBox.Show("You must enter a password.");
                return;
            }
            
            ServerItem serverItem = new ServerItem(NameBox.Text, ipAddress.ToString(), (int)ServerPort.Value, (int)RCONPort.Value, PasswordBox.Text, ConnectOnStartupCheck.Checked);

            var search = DataFileSystem.Data.AllServers.Where(x => x.Name == serverItem.Name);
            if (search.Any()) DataFileSystem.Data.AllServers.Remove(search.First());

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
            string currentItem = ServerList.Text;

            ServerItem item = null;
            if (!string.IsNullOrEmpty(currentItem))
            {
                item = DataFileSystem.Data.AllServers.FirstOrDefault(x => x.Name == currentItem);
            }
            if (item == null) item = new ServerItem();

            NameBox.Text = item.Name;
            IPBox.Text = item.IP;
            ServerPort.Value = item.Port;
            RCONPort.Value = item.RconPort;
            PasswordBox.Text = item.Password;
            ConnectOnStartupCheck.Checked = item.ConnectOnLoad;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string currentItem = ServerList.Text;
            if (string.IsNullOrEmpty(currentItem))
            {
                MessageBox.Show("There is no server to delete.");
                return;
            }

            ServerItem item = DataFileSystem.Data.AllServers.FirstOrDefault(x => x.Name == currentItem);
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
            IPBox.Text = "";
            ServerPort.Value = 0;
            RCONPort.Value = 0;
            PasswordBox.Text = "";
            ConnectOnStartupCheck.Checked = false;
        }
    }
}
