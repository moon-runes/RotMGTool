namespace RotMGTool.util
{
    using System.Drawing;
    public class ColorConverter
    {
        public static Color FromUInt(uint colorValue)
        {
            byte r = (byte)((colorValue >> 16) & 0xFF);
            byte g = (byte)((colorValue >> 8) & 0xFF);
            byte b = (byte)(colorValue & 0xFF);
            return Color.FromArgb(r, g, b);
        }
    }
}
