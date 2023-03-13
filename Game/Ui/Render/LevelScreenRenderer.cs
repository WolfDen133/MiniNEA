using System.Numerics;
using Game.Ui.Misc;
using Game.Ui.Screens;
using Game.Window.Render.Renderers;
using Raylib_cs;

namespace Game.Ui.Render;

public class LevelScreenRenderer : UiRenderer
{
    private readonly LevelSelectScreen _parent;
    public LevelScreenRenderer(LevelSelectScreen parent) : base("game:window." + LevelSelectScreen.UI_ID)
    {
        _parent = parent;
    }

    public override void Draw()
    {
        Raylib.DrawRectangle(20, 20, Raylib.GetScreenWidth() - 40, Raylib.GetScreenHeight() - 40, new Color(10, 10, 30, 200));

        foreach (var pair in _parent.Buttons)
        {
            pair.Value.Renderer.Draw();
        }
        
        Raylib.DrawRectangle(20,20, Raylib.GetScreenWidth() - 40, 90, new Color(40, 40, 40, 200));
        Raylib.DrawLine(20, 110, Raylib.GetScreenWidth() - 20, 110, Color.DARKGRAY);
        Raylib.DrawTextEx(FontUtils.Font, "Level Select", new Vector2(50, 40), 50, 2, Color.WHITE);
        
        Raylib.DrawRectangleLines(20, 20, Raylib.GetScreenWidth() - 40, Raylib.GetScreenHeight() - 40, Color.WHITE);
        
        
        Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), 20, Color.BLACK);
        Raylib.DrawRectangle(Raylib.GetScreenWidth() - 20, 0, 20, Raylib.GetScreenHeight() - 20, Color.BLACK);
        Raylib.DrawRectangle(0, 20, 20, Raylib.GetScreenHeight() - 20, Color.BLACK);
        Raylib.DrawRectangle(20, Raylib.GetScreenHeight() - 20, Raylib.GetScreenWidth(), 20, Color.BLACK);
    }
}