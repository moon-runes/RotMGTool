using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotMGTool.display.elements
{
    internal class TextButton : Button
    {
        public TextButton(int w, int h) : base()
        {
            Width = w;
            Height = h;
        }

        public void Init(string text, Viewpoint view = null, string center = "")
        {
            if (view == null)
                view = Tool.View.Viewpoint;
            if (view == null)
                return;

            Text = text;
            view.AddToScreen(view, this);

            Point middle = view.GetCenter(this, center);
            switch (center)
            {
                case "x": Location = new Point(middle.X, Location.Y); break;
                case "y": Location = new Point(Location.X, middle.Y); break;
                case "xy": Location = middle; break;
            }
        }

        public void SetPos(int x, int y, int xSpacing = 0, int ySpacing = 0)
        {
            if (xSpacing > 0)
            {
                x += Tool.BufferX;
                Tool.BufferX += xSpacing;
            }
            if (ySpacing > 0)
            {
                y += Tool.BufferY;
                Tool.BufferY += ySpacing;
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
