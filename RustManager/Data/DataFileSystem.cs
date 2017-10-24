using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RustManager.Data
{
    class DataFileSystem
    {
        private const string Filename = "Data.json";
        private static string DataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RustManager");
        private static string DataFile => Path.Combine(DataPath, Filename);

        public static StoredData Data;

        public static void CheckDirectoryExists()
        {
            if (!Directory.Exists(DataPath))
                Directory.CreateDirectory(DataPath);
        }

        public static void SaveData()
        {
            CheckDirectoryExists();

            string data = JsonConvert.SerializeObject(Data, Formatting.Indented);
            File.WriteAllText(DataFile, data);
        }

        public static void ReadData()
        {
            CheckDirectoryExists();

            if (!File.Exists(DataFile))
            {
                Data = new StoredData();
                return;
            }

            string data = File.ReadAllText(DataFile);
            try
            {
                Data = JsonConvert.DeserializeObject<StoredData>(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error reading your data file. Please fix it or delete it and restart.\n\n{DataFile}");
            }
        }
    }
}
