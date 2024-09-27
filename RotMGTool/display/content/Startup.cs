namespace RotMGTool.display.content
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using System.IO;
    using Formatting = Newtonsoft.Json.Formatting;
    using Viewpoint = Viewpoint;
    using RotMGTool.display.elements;

    internal class Startup : Viewpoint
    {
        private string embeddedAssetsPath;
        private string assetLoaderPath;
        private string clientAssetsPath;
        private string clientXmlsPath;
        private string serverXmlsPath;

        private TextField embedAssetsLabel;
        private TextField assetLoaderLabel;
        private TextField cliAssetsLabel;
        private TextField cliXmlLabel;
        private TextField srcXmlLabel;

        private TextBox embedAssetsBox;
        private TextBox assetLoaderBox;
        private TextBox cliAssetsBox;
        private TextBox cliXmlBox;
        private TextBox srcXmlBox;

        private Button embedAssetsBrowse;
        private Button assetLoaderBrowse;
        private Button cliAssetsBrowse;
        private Button cliXmlBrowse;
        private Button srcXmlBrowse;

        private Button continueButton;

        public Startup() : base(410, 450, "RotMGTool v1.0", "First-time startup.")
        {
            LoadDirectories();
        }
        public override void Draw()
        {
            Position();
            Tool.View.Controls.Add(embedAssetsLabel);
            Tool.View.Controls.Add(assetLoaderLabel);
            Tool.View.Controls.Add(cliAssetsLabel);
            Tool.View.Controls.Add(cliXmlLabel);
            Tool.View.Controls.Add(srcXmlLabel);

            Tool.View.Controls.Add(embedAssetsBrowse);
            Tool.View.Controls.Add(assetLoaderBrowse);
            Tool.View.Controls.Add(cliAssetsBrowse);
            Tool.View.Controls.Add(cliXmlBrowse);
            Tool.View.Controls.Add(srcXmlBrowse);

            Tool.View.Controls.Add(embedAssetsBox);
            Tool.View.Controls.Add(assetLoaderBox);
            Tool.View.Controls.Add(cliAssetsBox);
            Tool.View.Controls.Add(cliXmlBox);
            Tool.View.Controls.Add(srcXmlBox);

            continueButton = new Button { Text = "Continue", Width = 160, Height = 40 };
            Tool.View.Controls.Add(continueButton);
            continueButton.Location = new Point((Tool.screenSize.Width - continueButton.Width) / 2, Tool.screenSize.Height - continueButton.Height - 10);
            
            base.Draw();
        }
        public override void AddListeners()
        {
            embedAssetsBrowse.Click += new EventHandler(onBrowseEmbedAssets);
            assetLoaderBrowse.Click += new EventHandler(onBrowseAssetLoader);
            cliAssetsBrowse.Click += new EventHandler(onBrowseCliAssets);
            cliXmlBrowse.Click += new EventHandler(onBrowseCliXml);
            srcXmlBrowse.Click += new EventHandler(onBrowseSrcXml);

            base.AddListeners();
        }
        private void Position()
        {
            embedAssetsLabel = new TextField("Standard");
            embedAssetsLabel.SetPos(10, 60);
            embedAssetsLabel.Init("EmbeddedAssets.as", "", this);

            assetLoaderLabel = new TextField("Standard");
            assetLoaderLabel.SetPos(10, 130);
            assetLoaderLabel.Init("AssetLoader.as", "", this);

            cliAssetsLabel = new TextField("Standard");
            cliAssetsLabel.SetPos(10, 200);
            cliAssetsLabel.Init("Assets folder (client)", "", this);

            cliXmlLabel = new TextField("Standard");
            cliXmlLabel.SetPos(10, 270);
            cliXmlLabel.Init("XML folder (client)", "", this);

            srcXmlLabel = new TextField("Standard");
            srcXmlLabel.SetPos(10, 340);
            srcXmlLabel.Init("XML folder (source)", "", this);

            embedAssetsBox = new TextBox { Location = new Point(10, 80), Width = 300, Height = 30, Text = embeddedAssetsPath };
            assetLoaderBox = new TextBox { Location = new Point(10, 150), Width = 300, Height = 30, Text = assetLoaderPath };
            cliAssetsBox = new TextBox { Location = new Point(10, 220), Width = 300, Height = 30, Text = clientAssetsPath };
            cliXmlBox = new TextBox { Location = new Point(10, 290), Width = 300, Height = 30, Text = clientXmlsPath };
            srcXmlBox = new TextBox { Location = new Point(10, 360), Width = 300, Height = 30, Text = serverXmlsPath };

            embedAssetsBrowse = new Button { Text = "Browse", Location = new Point(320, 72), Width = 80, Height = 28 };
            assetLoaderBrowse = new Button { Text = "Browse", Location = new Point(320, 142), Width = 80, Height = 28 };
            cliAssetsBrowse = new Button { Text = "Browse", Location = new Point(320, 212), Width = 80, Height = 28 };
            cliXmlBrowse = new Button { Text = "Browse", Location = new Point(320, 282), Width = 80, Height = 28 };
            srcXmlBrowse = new Button { Text = "Browse", Location = new Point(320, 352), Width = 80, Height = 28 };
        }
        private void onBrowseEmbedAssets(object sender, EventArgs e)
        {
            embeddedAssetsPath = Tool.View.BrowseForFolder();
            embedAssetsBox.Text = embeddedAssetsPath;
            SaveDirectories(); // Save changes when a path is selected
        }

        private void onBrowseAssetLoader(object sender, EventArgs e)
        {
            assetLoaderPath = Tool.View.BrowseForFolder();
            assetLoaderBox.Text = assetLoaderPath;
            SaveDirectories();
        }

        private void onBrowseCliAssets(object sender, EventArgs e)
        {
            clientAssetsPath = Tool.View.BrowseForFolder();
            cliAssetsBox.Text = clientAssetsPath;
            SaveDirectories();
        }

        private void onBrowseCliXml(object sender, EventArgs e)
        {
            clientXmlsPath = Tool.View.BrowseForFolder();
            cliXmlBox.Text = clientXmlsPath;
            SaveDirectories();
        }

        private void onBrowseSrcXml(object sender, EventArgs e)
        {
            serverXmlsPath = Tool.View.BrowseForFolder();
            srcXmlBox.Text = serverXmlsPath;
            SaveDirectories();
        }

        private void LoadDirectories()
        {
            if (File.Exists("config.json"))
            {
                string json = File.ReadAllText("config.json");
                dynamic config = JsonConvert.DeserializeObject(json);

                embeddedAssetsPath = config.EmbeddedAssetsPath;
                assetLoaderPath = config.AssetLoaderPath;
                clientAssetsPath = config.ClientAssetsPath;
                clientXmlsPath = config.ClientXmlsPath;
                serverXmlsPath = config.ServerXmlsPath;
            }
        }

        private void SaveDirectories()
        {
            var config = new
            {
                EmbeddedAssetsPath = embeddedAssetsPath,
                AssetLoaderPath = assetLoaderPath,
                ClientAssetsPath = clientAssetsPath,
                ClientXmlsPath = clientXmlsPath,
                ServerXmlsPath = serverXmlsPath
            };

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText("config.json", json);
        }
    }
}
