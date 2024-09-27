using RotMGTool.display;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace RotMGTool
{
    internal static class Tool
    {
        public static View View { get; private set; }
        public static FontFormats Fonts { get; private set; }

        public static Dictionary<Viewpoint, List<Control>> ObjectPool;

        [STAThread]
        public static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Fonts = new FontFormats();
            View = new View();

            Application.EnableVisualStyles();
            Application.Run(View);
        }
    }
}
