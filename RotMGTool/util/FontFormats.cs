using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public enum FontSizes
{
    Small = 0,
    Normal = 1,
    Large = 2,
    ExtraLarge = 3,
    ExtraSmall = 4
}

public class FontFormats
{
    public static Dictionary<string, Font[]> FontsCache;
    public FontFormats()
    {
        FontsCache = new Dictionary<string, Font[]>();
        FontsCache["Standard"] = CreateFonts("Myriad Pro", FontStyle.Regular);
        FontsCache["Bold"] = CreateFonts("Myriad Pro", FontStyle.Bold);
        FontsCache["Italic"] = CreateFonts("Myriad Pro", FontStyle.Italic);
        FontsCache["BoldItalic"] = CreateFonts("Myriad Pro", FontStyle.Bold | FontStyle.Italic);
    }
    public Font GetFont(string type, FontSizes size)
    {
        int trueSize = (int)size;
        return FontsCache[type][trueSize];
    }
    private static Font[] CreateFonts(string fontName, FontStyle style)
    {
        return new[]
        {
            CreateFont(fontName, 10, style),
            CreateFont(fontName, 12, style),
            CreateFont(fontName, 16, style),
            CreateFont(fontName, 20, style),
            CreateFont(fontName, 8, style)
        };
    }
    private static Font CreateFont(string fontName, int size, FontStyle style)
    {
        var availableFontName = FontFamily.Families.Any(f => f.Name == fontName) ? fontName : "Arial";
        return new Font(availableFontName, size, style);
    }
    public static Font[] Standard => FontsCache["Standard"];
    public static Font[] Bold => FontsCache["Bold"];
    public static Font[] Italic => FontsCache["Italic"];
    public static Font[] BoldItalic => FontsCache["BoldItalic"];
}