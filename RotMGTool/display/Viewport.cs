namespace RotMGTool.display
{
    using RotMGTool.display.elements;
    using RotMGTool.util;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Help = dialogs.Help;
    using View = View;

    public class Viewport
    {
        public static string Copyright = "© moon-runes 2024.";
        public static string Header = "";
        public static string Information = "";

        public Help Help;

        public TextField Watermark;
        public TextField Name;
        public TextField Desc;
        public TextButton Button;
        public TextButton HelpButton;

        public static int padding = 70;
        public static int offset;

        public Viewport(int w, int h, string n, string d = "") 
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

        public void AddHelpButton(string anchor)
        {
            HelpButton = new TextButton(30, 30);
            HelpButton.Init("?", this);
            
            Window.AnchorTo[anchor](HelpButton, 5);
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

        public List<Control> CreateObjectPool(View view)
        {
            if (Tool.ObjectPool == null)
                Tool.ObjectPool = new Dictionary<View, List<Control>>();
            if (!Tool.ObjectPool.TryGetValue(view, out var pool))
            {
                pool = new List<Control>();
                Tool.ObjectPool[view] = pool;
            }
            return pool;
        }

        public List<Control> CreateObjectPoolViewport(Viewport view)
        {
            if (Tool.ObjectPoolViewport == null)
                Tool.ObjectPoolViewport = new Dictionary<Viewport, List<Control>>();
            if (!Tool.ObjectPoolViewport.TryGetValue(view, out var pool))
            {
                pool = new List<Control>();
                Tool.ObjectPoolViewport[view] = pool;
            }
            return pool;
        }

        public void AddToViewport(Viewport view, Control obj)
        {
            if (Tool.ObjectPoolViewport == null)
                Tool.ObjectPoolViewport = new Dictionary<Viewport, List<Control>>();
            if (!Tool.ObjectPoolViewport.TryGetValue(view, out var pool))
            {
                pool = new List<Control>();
                Tool.ObjectPoolViewport[view] = pool;
            }

            Tool.View.Controls.Add(obj);
            pool.Add(obj);
            Tool.ObjectPoolViewport[view] = pool;
        }

        public void RemoveFromViewport(Viewport view, Control obj)
        {
            List<Control> pool = Tool.ObjectPoolViewport[view];
            foreach (var objs in pool)
                if (objs == obj)
                {
                    Tool.View.Controls.Remove(obj);
                    pool.Remove(obj);
                }
            Tool.ObjectPoolViewport[view] = pool;
        }

        public void RemoveAllFromViewport(Viewport view)
        {
            var controls = Tool.ObjectPoolViewport[view];
            foreach (var ctrl in controls)
            {
                Tool.View.Controls.Remove(ctrl);
                controls.Remove(ctrl);
            }
            Tool.ObjectPoolViewport[view] = controls;
        }

        public void AddToView(View view, Control obj)
        {
            if (Tool.ObjectPool == null)
                Tool.ObjectPool = new Dictionary<View, List<Control>>();
            if (!Tool.ObjectPool.TryGetValue(view, out var pool))
            {
                pool = new List<Control>();
                Tool.ObjectPool[view] = pool;
            }

            view.Controls.Add(obj);
            pool.Add(obj);
            Tool.ObjectPool[view] = pool;
        }

        private void OpenHelp(object sender, EventArgs e)
        {
            Help = new Help(Header, Information);
            Help.Run();
        }

        public virtual void Draw() { }

        public virtual void Listeners() 
        {
            HelpButton.Click += new EventHandler(OpenHelp);
        }
    }
}