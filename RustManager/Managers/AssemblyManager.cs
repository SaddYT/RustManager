using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace RustManager.Managers
{
    class AssemblyManager
    {
        private static Dictionary<string, Assembly> _libraries = new Dictionary<string, Assembly>();

        public static void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var keyName = new AssemblyName(args.Name).Name;

            if (keyName.Contains("System.Reactive.Debugger"))
            {
                return null;
            }

            if (_libraries.ContainsKey(keyName))
            {
                return _libraries[keyName];
            }

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"RustManager.Resources.{keyName}.dll"))
            {
                var buffer = new BinaryReader(stream).ReadBytes((int)stream.Length);
                var assembly = Assembly.Load(buffer);
                _libraries.Add(keyName, assembly);

                return assembly;
            }
        }
    }
}