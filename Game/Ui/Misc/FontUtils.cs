using Raylib_cs;

namespace Game.Ui.Misc;

public class FontUtils
{
    public static Font Font;
    public static Font ButtonFont;

    public FontUtils()
    {
        Font = Raylib.LoadFontEx(Directory.GetCurrentDirectory() + "/assets/font/main.otf", 256, null, 250);
        ButtonFont = Raylib.LoadFontEx(Directory.GetCurrentDirectory() + "/assets/font/button.otf", 256, null, 250);
    }
}