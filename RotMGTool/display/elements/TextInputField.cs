using RotMGTool.util;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotMGTool.display.elements
{
    internal class TextInputField : TextBox
    {
        public TextInputField(int w = 300, int h = 30) : base()
        {
            Width = w;
            Height = h;
        }

        public void Init(string text, Viewport view = null, string type = "Standard", FontSizes size = FontSizes.Normal)
        {
            if (view == null)
                view = Tool.View.Viewport;
            if (view == null)
                return;

            SetText(text, type, size);
            view.AddToViewport(view, this);
        }

        public void SetText(string text, string type, FontSizes size)
        {
            base.Font = Tool.Fonts.GetFont(type, size);
            Text = text;
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
