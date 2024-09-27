using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotMGTool.util
{
    internal static class Window
    {
        public static int BufferX = 0;
        public static int BufferY = 0;

        public static Size Bounds;

        public static Point TopLeft(Control obj, int padding)
        {
            return new Point(padding, padding);
        }

        public static Point TopRight(Control obj, int padding)
        {
            return new Point(Bounds.Width - obj.Width - padding, padding);
        }

        public static Point BottomLeft(Control obj, int padding)
        {
            return new Point(padding, Bounds.Height - obj.Height - padding);
        }

        public static Point BottomRight(Control obj, int padding)
        {
            return new Point(Bounds.Width - obj.Width - padding, Bounds.Height - obj.Height - padding);
        }

        public static Point BottomCenter(Control obj, int padding)
        {
            return new Point((Bounds.Width - obj.Width) / 2, Bounds.Height - obj.Height - padding);
        }

        public static int CenterX(Control obj)
        {
            return (Bounds.Width - obj.Width) / 2;
        }

        public static int CenterY(Control obj)
        {
            return (Bounds.Height - obj.Height) / 2;
        }

        public static Point CenterXY(Control obj)
        {
            int x = Bounds.Width / 2 - obj.Width / 2;
            int y = Bounds.Height / 2 - obj.Height / 2;
            return new Point(x, y);
        }

        public static Dictionary<string, Action<Control, int?>> AnchorTo = new Dictionary<string, Action<Control, int?>>()
        {
            { "TopLeft", (Control obj, int? padding ) => obj.Location = TopLeft(obj, padding ?? 5) },
            { "TopRight", (Control obj, int? padding) => obj.Location = TopRight(obj, padding ?? 5) },
            { "BottomLeft", (Control obj, int? padding) => obj.Location = BottomLeft(obj, padding ?? 5) },
            { "BottomRight", (Control obj, int? padding) => obj.Location = BottomRight(obj, padding ?? 5) },
            { "BottomCenter", (Control obj, int? padding) => obj.Location = BottomCenter(obj, padding ?? 5) },
            { "CenterX", (Control obj, int? padding) => obj.Location = new Point(CenterX(obj), obj.Location.Y) },
            { "CenterY", (Control obj, int? padding) => obj.Location = new Point(obj.Location.X, CenterY(obj)) },
            { "CenterXY", (Control obj, int? padding) => obj.Location = CenterXY(obj) }
        };

        public static void ResetBuffer()
        {
            BufferX = 0;
            BufferY = 0;
        }
    }
}
