namespace RotMGTool.display
{
    using RotMGTool.display.elements;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class Viewpoint
    {
        private TextField Name;
        private TextField Desc;
        public Viewpoint(int w, int h, string n, string d = "") 
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

            Name = new TextField("Bold", FontSizes.Large);
            Name.Init(n, "x", this);
            Name.SetY(10);

            if (d != "")
            {
                Desc = new TextField("Standard", FontSizes.Small);
                Desc.Init(d, "x", this);
                Desc.SetY(Name.Location.Y + Name.Height - 3);
            }

            var targetSize = new Size(width, height);
            if (Tool.screenSize != targetSize)
                Tool.View.ClientSize = targetSize;
        }
        public void AddToScreen(Viewpoint scr, Control obj)
        {
            if (Tool.ObjectPool == null)
                Tool.ObjectPool = new Dictionary<Viewpoint, List<Control>>();
            if (!Tool.ObjectPool.TryGetValue(scr, out var objs))
            {
                objs = new List<Control>();
                Tool.ObjectPool[scr] = objs;
            }

            Tool.View.Controls.Add(obj);
            objs.Add(obj);
            Tool.ObjectPool[scr] = objs;
        }
        public List<Control> CreateObjectPool(Viewpoint scr)
        {
            if (Tool.ObjectPool == null)
                Tool.ObjectPool = new Dictionary<Viewpoint, List<Control>>();
            if (!Tool.ObjectPool.TryGetValue(scr, out var objs))
            {
                objs = new List<Control>();
                Tool.ObjectPool[scr] = objs;
            }
            return objs;
        }
        public void BulkAddToScreen(Viewpoint scr, Control[] objects)
        {
            List<Control> pool = CreateObjectPool(scr);
            foreach (var obj in objects)
            {
                Tool.View.Controls.Add(obj);
                pool.Add(obj);
            }
            Tool.ObjectPool[scr] = pool;
        }
        public Point AddAndCenter(Viewpoint scr, Control obj, string axis = "")
        {
            List<Control> pool = CreateObjectPool(scr);

            Tool.View.Controls.Add(obj);
            pool.Add(obj);
            Tool.ObjectPool[scr] = pool;
            return GetCenter(obj, axis);
        }
        public Point GetCenter(Control obj, string axis = "")
        {
            int x = -1;
            int y = -1;
            switch (axis)
            {
                case "x": x = (Tool.screenSize.Width - obj.Width) / 2; break;
                case "y": y = (Tool.screenSize.Height - obj.Height) / 2; break;
                default:
                    x = (Tool.screenSize.Width - obj.Width) / 2;
                    y = (Tool.screenSize.Height - obj.Height) / 2;
                    break;
            }
            return new Point(x, y);
        }

        public void RemoveFromScreen(Viewpoint scr, Control obj)
        {
            List<Control> pool = Tool.ObjectPool[scr];
            foreach (var objs in pool)
                if (objs == obj)
                {
                    Tool.View.Controls.Remove(obj);
                    pool.Remove(obj);
                }
            Tool.ObjectPool[scr] = pool;
        }
        public void RemoveAllFromScreen(Viewpoint scr)
        {
            var controls = Tool.ObjectPool[scr];
            foreach (var ctrl in controls)
            {
                Tool.View.Controls.Remove(ctrl);
                controls.Remove(ctrl);
            }
            Tool.ObjectPool[scr] = controls;
        }
        public virtual void Draw() { }
        public virtual void AddListeners() { }
    }
}