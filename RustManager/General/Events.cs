using System;
using RustManager.Data;
using RustManager.ServerManagement;

namespace RustManager.General
{
    class Events
    {
        public static void RegisterEvents()
        {
            OnProcessLoad();

            AppDomain.CurrentDomain.ProcessExit += ProcessExit;
        }

        private static void OnProcessLoad()
        {
            DataFileSystem.ReadData();
        }
        
        private static void ProcessExit(object sender, EventArgs e)
        {
            DataFileSystem.SaveData();

            ServerManager.ConnectedServers.ForEach(x => x.Disconnect());
        }
    }
}
