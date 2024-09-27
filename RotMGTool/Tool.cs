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
        public static Size screenSize = new Size(410, 450);

        public static int BufferX = 0;
        public static int BufferY = 0;

        [STAThread]
        public static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Fonts = new FontFormats();
            View = new View();

            Application.EnableVisualStyles();
            Application.Run(View);
        }

        public static void ResetBuffer()
        {
            BufferX = 0;
            BufferY = 0;
        }
    }
}
