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

        public static Dictionary<View, List<Control>> ObjectPool;
        public static Dictionary<Viewport, List<Control>> ObjectPoolViewport;

        [STAThread]
        public static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Fonts = new FontFormats();
            View = new View("RotMGTool", "rotmgtool.ico");

            Application.EnableVisualStyles();
            Application.Run(View);
        }
    }
}
