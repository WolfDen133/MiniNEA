using System.Numerics;
using Game.Ui.Misc;
using Game.Ui.Screens;
using Game.Window.Render.Renderers;
using Raylib_cs;

namespace Game.Ui.Render;

public class MainMenuScreenRenderer : UiRenderer
{
    private MainMenuScreen _parent;

    public MainMenuScreenRenderer(MainMenuScreen parent) : base("game:window." + MainMenuScreen.UI_ID)
    {
        _parent = parent;
    }

    public override void Draw()
    {
        Raylib.DrawRectangleGradientH(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), Color.GREEN, Color.BLUE);

        int x = (Raylib.GetScreenWidth() / 2) - Raylib.MeasureText(GameConst.Name, 28);
        Raylib.DrawTextEx(FontUtils.Font, GameConst.Name, new Vector2(x, Raylib.GetScreenHeight() / 2 - ((Raylib.GetScreenHeight() / 8) * 3)), 48, 2, Color.WHITE);
        
        
        foreach (var pair in _parent.Buttons)
        {
            pair.Value.Renderer.Draw();
        }
    }
}