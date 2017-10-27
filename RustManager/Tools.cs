using System.Windows.Forms;

namespace RustManager
{
    public static class Tools
    {
        public static TabPage FindTabPage(TabControl control, string name)
        {
            var index = control.TabPages.IndexOfKey(name);
            return (index != -1) ? control.TabPages[index] : null;
        }
    }
}
