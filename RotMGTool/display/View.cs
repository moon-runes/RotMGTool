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
        public Viewpoint Viewpoint;
        public View()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = Window.Bounds;
            Text = "RotMGTool";
            Icon = new Icon("rotmgtool.ico");
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
            Viewpoint = new Startup();
            Viewpoint.Draw();
            Viewpoint.Listeners();
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
