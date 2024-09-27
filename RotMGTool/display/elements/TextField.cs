namespace RotMGTool.display.elements
{
    using RotMGTool.util;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using View = View;

    public class TextField : Label
    {
        public TextField(string type, FontSizes size = FontSizes.Normal, uint color = 0xffffff) : base()
        {
            AutoSize = true;
            Font = Tool.Fonts.GetFont(type, size);
        }

        public void Init(string text, Viewport view = null)
        {
            if (view == null)
                view = Tool.View.Viewport;
            if (view == null)
                return;

            Text = text;
            view.AddToViewport(view, this);
        }

        public void Init(string text, View view)
        {
            if (view == null)
                view = Tool.View;
            if (view == null)
                return;

            var viewport = view.Viewport;
            if (viewport == null)
                viewport = Tool.View.Viewport;
            if (viewport == null)
                return;

            Text = text;
            viewport.AddToView(view, this);
        }

        public void SetSize(int width, int height)
        {
            Size = new Size(width, height);
        }

        public void SetPos(int x, int y, int xSpacing = 0, int ySpacing = 0)
        {
            if (xSpacing > 0)
            {
                x += Window.BufferX;
                Window.BufferX += xSpacing;
            }
            if (ySpacing > 0)
            {
                y += Window.BufferY;
                Window.BufferY += ySpacing;
            }
            Location = new Point(x, y);
        }

        public void SetX(int x)
        {
            Location = new Point(x, Location.Y);
        }

        public void SetY(int y)
        {
            Location = new Point(Location.X, y);
        }
    }
}
