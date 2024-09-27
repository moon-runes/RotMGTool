namespace RotMGTool
{
    using RotMGTool.display;
    using RotMGTool.display.content;
    using RotMGTool.util;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    public partial class View : Form
    {
        public Viewport Viewport;
        public View(string text, string icon)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = Window.Bounds;
            Text = text;
            Icon = new Icon(icon);
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await Task.Run(() =>
            {
                InitializeComponent();
            });
            FirstTimeStartup();
        }

        private void FirstTimeStartup()
        {
            Viewport = new Startup();
            Viewport.Draw();
            Viewport.Listeners();
        }

        public string BrowseForFolder()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                    return folderDialog.SelectedPath;
                return string.Empty;
            }
        }
    }
}
