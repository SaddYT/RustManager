using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace RustManager.General
{
    class AssemblyManager
    {
        private static Dictionary<string, Assembly> Libraries = new Dictionary<string, Assembly>();

        public static void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string keyName = new AssemblyName(args.Name).Name;

            if (keyName.Contains("System.Reactive.Debugger")) return null;
            
            if (Libraries.ContainsKey(keyName))
            {
                return Libraries[keyName];
            }

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"RustManager.Resources.{keyName}.dll"))
            {
                byte[] buffer = new BinaryReader(stream).ReadBytes((int)stream.Length);
                Assembly assembly = Assembly.Load(buffer);
                Libraries.Add(keyName, assembly);
                return assembly;
            }
        }
    }
}