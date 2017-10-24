using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustManager.WebSockets
{
    public class PacketListenerModel
    {
        public static List<int> Identifiers = new List<int>();

        public void AddListener(ListenerModel listener)
        {
            if (!Identifiers.Contains(listener.Identifier))
            {
                Identifiers.Add(listener.Identifier);
            }
        }

        public void RemoveListener(ListenerModel listener)
        {
            if (Identifiers.Contains(listener.Identifier))
            {
                Identifiers.Remove(listener.Identifier);
            }
        }
    }
}
