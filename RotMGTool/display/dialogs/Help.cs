namespace RotMGTool.display.dialogs
{
    using RotMGTool.display.elements;
    using System.Drawing;
    using System.Windows.Forms;
    using View = View;

    public partial class Help : View
    {
        public TextField Content;

        public Help(string title, string content) : base("Help!", "rotmgtool.ico")
        {
            ClientSize = new Size(300, 200);
            Content = new TextField("Standard", FontSizes.Small);
            Content.Init(content, this);
            Content.Dock = DockStyle.Fill;
            InitializeComponent();
        }

        public void Run()
        {
            ShowDialog();
        }
    }
}
