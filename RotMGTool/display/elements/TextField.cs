﻿namespace RotMGTool.display.elements
{
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Windows.Forms;
    using System.Xml.Linq;

    public class TextField : Label
    {
        public TextField(string type, FontSizes size = FontSizes.Normal, uint color = 0xffffff) : base()
        {
            AutoSize = true;
            Font = Tool.Fonts.GetFont(type, size);
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
