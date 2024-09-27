using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public enum FontSizes
{
    Small = 0,
    Normal = 1,
    Large = 2,
    ExtraLarge = 3
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
        Console.WriteLine((int)size);
        return FontsCache[type][trueSize];
    }
    private static Font[] CreateFonts(string fontName, FontStyle style)
    {
        Console.WriteLine($"Checking for font: {fontName}");
        return new[]
        {
            CreateFont(fontName, 10, style),
            CreateFont(fontName, 12, style),
            CreateFont(fontName, 16, style),
            CreateFont(fontName, 20, style)
        };
    }
    private static Font CreateFont(string fontName, int size, FontStyle style)
    {
        var availableFontName = FontFamily.Families.Any(f => f.Name == fontName) ? fontName : "Arial";

        if (availableFontName != fontName)
        {
            Console.WriteLine($"Font '{fontName}' not found. Falling back to '{availableFontName}'.");
        }

        return new Font(availableFontName, size, style);
    }
    public static Font[] Standard => FontsCache["Standard"];
    public static Font[] Bold => FontsCache["Bold"];
    public static Font[] Italic => FontsCache["Italic"];
    public static Font[] BoldItalic => FontsCache["BoldItalic"];
}