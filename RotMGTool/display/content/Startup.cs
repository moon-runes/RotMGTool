namespace RotMGTool.display.content
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using System.IO;
    using Formatting = Newtonsoft.Json.Formatting;
    using Screen = Screen;

    internal class Startup : Screen
    {
        private string embeddedAssetsPath;
        private string assetLoaderPath;
        private string clientAssetsPath;
        private string clientXmlsPath;
        private string serverXmlsPath;

        private Label embedAssetsLabel;
        private Label assetLoaderLabel;
        private Label cliAssetsLabel;
        private Label cliXmlLabel;
        private Label srcXmlLabel;

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
            int spacing = 70;
            int yPosition = 60;

            embedAssetsLabel = new Label { Text = "EmbeddedAssets.as", Location = new Point(10, yPosition), Width = 300, Font = FontFormats.Standard[1], AutoSize = true };
            yPosition += spacing;
            assetLoaderLabel = new Label { Text = "AssetLoader.as", Location = new Point(10, yPosition), Width = 300, Font = FontFormats.Standard[1], AutoSize = true };
            yPosition += spacing;
            cliAssetsLabel = new Label { Text = "Assets folder (client)", Location = new Point(10, yPosition), Width = 300, Font = FontFormats.Standard[1], AutoSize = true };
            yPosition += spacing;
            cliXmlLabel = new Label { Text = "XMLs folder (client)", Location = new Point(10, yPosition), Width = 300, Font = FontFormats.Standard[1], AutoSize = true };
            yPosition += spacing;
            srcXmlLabel = new Label { Text = "XMLs folder (server)", Location = new Point(10, yPosition), Width = 300, Font = FontFormats.Standard[1], AutoSize = true };

            yPosition = 80;

            embedAssetsBox = new TextBox { Location = new Point(10, yPosition), Width = 300, Height = 30, Text = embeddedAssetsPath };
            yPosition += spacing;
            assetLoaderBox = new TextBox { Location = new Point(10, yPosition), Width = 300, Height = 30, Text = assetLoaderPath };
            yPosition += spacing;
            cliAssetsBox = new TextBox { Location = new Point(10, yPosition), Width = 300, Height = 30, Text = clientAssetsPath };
            yPosition += spacing;
            cliXmlBox = new TextBox { Location = new Point(10, yPosition), Width = 300, Height = 30, Text = clientXmlsPath };
            yPosition += spacing;
            srcXmlBox = new TextBox { Location = new Point(10, yPosition), Width = 300, Height = 30, Text = serverXmlsPath };

            yPosition = 72;

            embedAssetsBrowse = new Button { Text = "Browse", Location = new Point(320, yPosition), Width = 80, Height = 28 };
            yPosition += spacing;
            assetLoaderBrowse = new Button { Text = "Browse", Location = new Point(320, yPosition), Width = 80, Height = 28 };
            yPosition += spacing;
            cliAssetsBrowse = new Button { Text = "Browse", Location = new Point(320, yPosition), Width = 80, Height = 28 };
            yPosition += spacing;
            cliXmlBrowse = new Button { Text = "Browse", Location = new Point(320, yPosition), Width = 80, Height = 28 };
            yPosition += spacing;
            srcXmlBrowse = new Button { Text = "Browse", Location = new Point(320, yPosition), Width = 80, Height = 28 };

            continueButton = new Button { Text = "Continue", Width = 160, Height = 40 };
            Tool.View.Controls.Add(continueButton);
            continueButton.Location = new Point((Tool.screenSize.Width - continueButton.Width) / 2, Tool.screenSize.Height - continueButton.Height - 10);

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

            embedAssetsBrowse.Click += new EventHandler(onBrowseEmbedAssets);
            assetLoaderBrowse.Click += new EventHandler(onBrowseAssetLoader);
            cliAssetsBrowse.Click += new EventHandler(onBrowseCliAssets);
            cliXmlBrowse.Click += new EventHandler(onBrowseCliXml);
            srcXmlBrowse.Click += new EventHandler(onBrowseSrcXml);

            base.Draw();
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
