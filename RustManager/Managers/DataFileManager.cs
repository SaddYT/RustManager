using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

using Newtonsoft.Json;

using RustManager.ServerManagement;

namespace RustManager.Managers
{
    public class DataFileManager
    {
        private const string FileName = "Data.json";
        private static string _dataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RustManager");
        private static string _dataFile => Path.Combine(_dataPath, FileName);

        public static StoredData Data;

        public static void CheckDirectoryExists()
        {
            if (!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
            }
        }

        public static void SaveData()
        {
            CheckDirectoryExists();

            var data = JsonConvert.SerializeObject(Data, Formatting.Indented);
            File.WriteAllText(_dataFile, data);
        }

        public static void ReadData()
        {
            CheckDirectoryExists();

            if (!File.Exists(_dataFile))
            {
                Data = new StoredData();
                return;
            }

            var data = File.ReadAllText(_dataFile);

            try
            {
                Data = JsonConvert.DeserializeObject<StoredData>(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error reading your data file. Please fix it or delete it and restart.\n\n{_dataFile}");
            }
        }
    }

    public class StoredData
    {
        public List<ServerModel> AllServers = new List<ServerModel>();
    }
}
