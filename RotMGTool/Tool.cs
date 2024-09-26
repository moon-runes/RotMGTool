using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RotMGTool
{
    internal static class Tool
    {
        public static View View { get; private set; }

        public static Size screenSize = new Size(410, 450);

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            View = new View();
            Application.Run(View);
        }
    }
}
