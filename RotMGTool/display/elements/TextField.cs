namespace RotMGTool.display.elements
{
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Windows.Forms;
    public class TextField : Label
    {
        public TextField(string type, FontSizes size = FontSizes.Normal, uint color = 0xffffff) : base()
        {
            AutoSize = true;
            Font = Tool.Fonts.GetFont(type, size);
        }

        public void Init(string text, string center = "", Viewpoint view = null)
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

        public void SetPos(int x, int y)
        {
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
