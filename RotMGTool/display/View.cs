using RotMGTool.display.content;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotMGTool
{
    public partial class View : Form
    {
        Startup Startup;
        public View()
        {
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = Tool.screenSize;
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
            Startup = new Startup();
            Startup.Draw();
        }
        public string BrowseForFolder()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    return folderDialog.SelectedPath;
                }
                return string.Empty;
            }
        }
    }
}
