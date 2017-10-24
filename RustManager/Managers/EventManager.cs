using System;

using RustManager.ServerManagement;

namespace RustManager.Managers
{
    public class EventManager
    {
        public static void RegisterEvents()
        {
            OnProcessLoad();

            AppDomain.CurrentDomain.ProcessExit += ProcessExit;
        }

        private static void OnProcessLoad()
        {
            DataFileManager.ReadData();
        }
        
        private static void ProcessExit(object sender, EventArgs e)
        {
            DataFileManager.SaveData();

            ServerManager.ConnectedServers.ForEach(x => x.Disconnect());
        }
    }
}
