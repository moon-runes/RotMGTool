namespace RotMGTool.display
{
    using RotMGTool.display.elements;
    using RotMGTool.util;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class Viewpoint
    {
        static string Copyright = "© moon-runes 2024.";

        public TextField Watermark;
        public TextField Name;
        public TextField Desc;
        public TextButton Button;

        public static int padding = 70;
        public static int offset;

        public Viewpoint(int w, int h, string n, string d = "") 
        {
            if (Tool.View == null)
                return;
            Tool.View.ClientSize = new Size(w, h);
            Window.Bounds = Tool.View.ClientSize;

            Size windowSize = Tool.View.ClientSize;
            int width = windowSize.Width;
            int height = windowSize.Height;

            if (w != windowSize.Width)
                width = w;
            if (h != windowSize.Height)
                height = h;

            Watermark = new TextField("Standard", FontSizes.ExtraSmall);
            Watermark.Init(Copyright, this);
            Window.AnchorTo["BottomLeft"](Watermark, 5);

            Name = new TextField("Bold", FontSizes.Large);
            Name.SetY(10);
            Name.Init(n, this);
            Window.AnchorTo["CenterX"](Name, 0);

            if (d != "")
            {
                Desc = new TextField("Standard", FontSizes.Small);
                Desc.Init(d, this);
                Desc.SetY(Name.Location.Y + Name.Height - 3);
                Window.AnchorTo["CenterX"](Desc, 0);
            }

            var targetSize = new Size(width, height);
            if (Window.Bounds != targetSize)
                Tool.View.ClientSize = targetSize;
        }

        public void AddButton(int w, int h, string name, bool bold)
        {
            Button = new TextButton(w, h);
            Button.Init(name, this, bold ? "Bold" : "Standard");
        }

        public void SetButtonCoords(string anchor = "", Point? coords = null)
        {
            if (anchor != "")
            {
                Window.AnchorTo[anchor](Button, 10);
                return;
            }
            Button.Location = coords ?? Point.Empty;
        }

        /*
        public void addButtonHandlers(. . .) { } 
        */ 

        public void AddToScreen(Viewpoint view, Control obj)
        {
            if (Tool.ObjectPool == null)
                Tool.ObjectPool = new Dictionary<Viewpoint, List<Control>>();
            if (!Tool.ObjectPool.TryGetValue(view, out var pool))
            {
                pool = new List<Control>();
                Tool.ObjectPool[view] = pool;
            }

            Tool.View.Controls.Add(obj);
            pool.Add(obj);
            Tool.ObjectPool[view] = pool;
        }

        public List<Control> CreateObjectPool(Viewpoint view)
        {
            if (Tool.ObjectPool == null)
                Tool.ObjectPool = new Dictionary<Viewpoint, List<Control>>();
            if (!Tool.ObjectPool.TryGetValue(view, out var pool))
            {
                pool = new List<Control>();
                Tool.ObjectPool[view] = pool;
            }
            return pool;
        }

        public void RemoveFromView(Viewpoint view, Control obj)
        {
            List<Control> pool = Tool.ObjectPool[view];
            foreach (var objs in pool)
                if (objs == obj)
                {
                    Tool.View.Controls.Remove(obj);
                    pool.Remove(obj);
                }
            Tool.ObjectPool[view] = pool;
        }

        public void RemoveAllFromView(Viewpoint view)
        {
            var controls = Tool.ObjectPool[view];
            foreach (var ctrl in controls)
            {
                Tool.View.Controls.Remove(ctrl);
                controls.Remove(ctrl);
            }
            Tool.ObjectPool[view] = controls;
        }

        public virtual void Draw() { }

        public virtual void Listeners() { }
    }
}