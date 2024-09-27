namespace RotMGTool.display.content
{
    using Newtonsoft.Json;
    using RotMGTool.display.elements;
    using RotMGTool.util;
    using System;
    using System.IO;
    using Formatting = Newtonsoft.Json.Formatting;
    using Viewpoint = Viewpoint;

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

        private TextInputField embedAssetsBox;
        private TextInputField assetLoaderBox;
        private TextInputField cliAssetsBox;
        private TextInputField cliXmlBox;
        private TextInputField srcXmlBox;

        private TextButton embedAssetsBrowse;
        private TextButton assetLoaderBrowse;
        private TextButton cliAssetsBrowse;
        private TextButton cliXmlBrowse;
        private TextButton srcXmlBrowse;

        public Startup() : base(410, 450, "RotMGTool v1.0", "First-time startup.")
        {
            LoadDirectories();
        }
        public override void Draw()
        {
            Labels();
            InputFields();
            Buttons();
            Window.ResetBuffer();
            base.Draw();
        }

        public override void Listeners()
        {
            embedAssetsBrowse.Click += new EventHandler(onBrowseEmbedAssets);
            assetLoaderBrowse.Click += new EventHandler(onBrowseAssetLoader);
            cliAssetsBrowse.Click += new EventHandler(onBrowseCliAssets);
            cliXmlBrowse.Click += new EventHandler(onBrowseCliXml);
            srcXmlBrowse.Click += new EventHandler(onBrowseSrcXml);

            base.Listeners();
        }

        private void Labels()
        {
            offset = Desc.Location.Y + Desc.Height + 6;
            Window.BufferY = offset;

            embedAssetsLabel = new TextField("Standard");
            embedAssetsLabel.SetPos(10, 0, 0, padding);
            embedAssetsLabel.Init("EmbeddedAssets.as", this);

            assetLoaderLabel = new TextField("Standard");
            assetLoaderLabel.SetPos(10, 0, 0, padding);
            assetLoaderLabel.Init("AssetLoader.as", this);

            cliAssetsLabel = new TextField("Standard");
            cliAssetsLabel.SetPos(10, 0, 0, padding);
            cliAssetsLabel.Init("Assets folder (client)", this);

            cliXmlLabel = new TextField("Standard");
            cliXmlLabel.SetPos(10, 0, 0, padding);
            cliXmlLabel.Init("XML folder (client)", this);

            srcXmlLabel = new TextField("Standard");
            srcXmlLabel.SetPos(10, 0, 0, padding);
            srcXmlLabel.Init("XML folder (source)", this);
        }

        private void InputFields()
        {
            Window.BufferY = offset + 20;

            embedAssetsBox = new TextInputField(300, 30);
            embedAssetsBox.SetPos(10, 0, 0, padding);
            embedAssetsBox.Init(embeddedAssetsPath + "1", this);

            assetLoaderBox = new TextInputField(300, 30);
            assetLoaderBox.SetPos(10, 0, 0, padding);
            assetLoaderBox.Init(assetLoaderPath + "2", this);

            cliAssetsBox = new TextInputField(300, 30);
            cliAssetsBox.SetPos(10, 0, 0, padding);
            cliAssetsBox.Init(clientAssetsPath + "3", this);

            cliXmlBox = new TextInputField(300, 30);
            cliXmlBox.SetPos(10, 0, 0, padding);
            cliXmlBox.Init(clientXmlsPath + "4", this);

            srcXmlBox = new TextInputField(300, 30);
            srcXmlBox.SetPos(10, 0, 0, padding);
            srcXmlBox.Init(serverXmlsPath + "5", this);
        }

        private void Buttons()
        {
            Window.BufferY = offset + 19;

            string t = "Browse";
            embedAssetsBrowse = new TextButton(80, 28);
            embedAssetsBrowse.Init(t, this);
            embedAssetsBrowse.SetPos(320, 0, 0, 70);

            assetLoaderBrowse = new TextButton(80, 28);
            assetLoaderBrowse.Init(t, this);
            assetLoaderBrowse.SetPos(320, 0, 0, 70);

            cliAssetsBrowse = new TextButton(80, 28);
            cliAssetsBrowse.Init(t, this);
            cliAssetsBrowse.SetPos(320, 0, 0, 70);

            cliXmlBrowse = new TextButton(80, 28);
            cliXmlBrowse.Init(t, this);
            cliXmlBrowse.SetPos(320, 0, 0, 70);

            srcXmlBrowse = new TextButton(80, 28);
            srcXmlBrowse.Init(t, this);
            srcXmlBrowse.SetPos(320, 0, 0, 70);

            t = "Continue";
            AddButton(160, 40, t, true);
            SetButtonCoords("BottomCenter");

            /* If you set the x/y of an element to where it's position is dependent *
             * on the element's height, then you have to Init() the text first.     */
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
