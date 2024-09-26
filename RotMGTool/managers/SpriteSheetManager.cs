using System.Drawing;
using System.IO;

namespace RotMGTool.Manager
{
    public class SpriteSheetManager
    {
        private Bitmap _spriteSheet;
        private int _spriteWidth;
        private int _spriteHeight;
        private int _sheetWidth;
        private int _sheetHeight;
        public SpriteSheetManager(int spriteWidth, int spriteHeight, int sheetWidth, int sheetHeight)
        {
            _spriteWidth = spriteWidth;
            _spriteHeight = spriteHeight;
            _sheetWidth = sheetWidth;
            _sheetHeight = sheetHeight;
            _spriteSheet = new Bitmap(_sheetWidth, _sheetHeight);
        }
        public void LoadSpriteSheet(string filePath)
        {
            if (File.Exists(filePath))
            {
                _spriteSheet = (Bitmap)Image.FromFile(filePath);
            }
        }
        public void AddSprite(string spritePath, uint index)
        {
            using (var sprite = (Bitmap)Image.FromFile(spritePath))
            {
                int cols = _sheetWidth / _spriteWidth;
                int row = (int)(index / cols);
                int col = (int)(index % cols);

                using (Graphics g = Graphics.FromImage(_spriteSheet))
                {
                    g.DrawImage(sprite, new Rectangle(col * _spriteWidth, row * _spriteHeight, _spriteWidth, _spriteHeight));
                }
            }
        }
        public void SaveSpriteSheet(string filePath)
        {
            _spriteSheet.Save(filePath);
        }
        public Image GetSpriteSheetImage()
        {
            return _spriteSheet;
        }
    }

}
