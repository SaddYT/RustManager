using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustManager.WebSockets
{
    public class ListenerModel
    {
        public int Identifier { get; set; }
        public string Command { get; set; }
        public Action Callback { get; set; }

        public ListenerModel(int identifier, string command, Action callback)
        {

        }
    }
}
