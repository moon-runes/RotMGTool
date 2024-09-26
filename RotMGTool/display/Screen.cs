namespace RotMGTool.display
{
    using System.Drawing;
    using System.Windows.Forms;
    public class Screen
    {
        private Label Title;
        private Label Desc;
        public Screen(int w, int h, string n, string d = "") 
        {
            if (Tool.View == null)
                return;

            Size windowSize = Tool.View.ClientSize;
            int width = windowSize.Width;
            int height = windowSize.Height;

            if (w != windowSize.Width)
                width = w;
            if (h != windowSize.Height)
                height = h;

            Title = new Label { Text = n, Font = FontFormats.Bold[3], AutoSize = true };
            Tool.View.Controls.Add(Title);
            int targetX = (width - Title.Width) / 2;
            int targetY;

            Title.Location = new Point(targetX, 10);
            if (d != "")
            {
                Desc = new Label { Text = d, Font = FontFormats.Standard[0], AutoSize = true };
                Tool.View.Controls.Add(Desc);
                targetX = (width - Desc.Width) / 2;
                targetY = Title.Location.Y + Title.Height - 3;
                Desc.Location = new Point(targetX, targetY);
            }

            var targetSize = new Size(width, height);
            if (Tool.screenSize != targetSize)
                Tool.View.ClientSize = targetSize;
        }
        public virtual void Draw()
        {
        }
    }
}