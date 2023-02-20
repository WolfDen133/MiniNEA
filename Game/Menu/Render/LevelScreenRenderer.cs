using Game.Menu.Screens;
using Game.Window.Render.Renderers;
using Raylib_cs;

namespace Game.Menu.Render;

public class LevelScreenRenderer : UiRenderer
{
    private LevelSelectScreen _parent;
    public LevelScreenRenderer(LevelSelectScreen parent) : base(LevelSelectScreen.UI_ID.ToString())
    {
        _parent = parent;
    }

    public override void Draw()
    {
        Raylib.DrawRectangle(20, 20, Raylib.GetScreenWidth() - 40, Raylib.GetScreenHeight() - 40, new Color(10, 10, 30, 150));

        foreach (var pair in _parent.Buttons)
        {
            pair.Value.Renderer.Draw();
        }
        
        Raylib.DrawText("Level Select", 50, 40, 48, Color.WHITE);

        Raylib.DrawRectangleLines(20, 20, Raylib.GetScreenWidth() - 40, Raylib.GetScreenHeight() - 40, Color.WHITE);
        
        Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), 20, Color.BLACK);
        Raylib.DrawRectangle(Raylib.GetScreenWidth() - 20, 0, 20, Raylib.GetScreenHeight() - 20, Color.BLACK);
        Raylib.DrawRectangle(0, 20, 20, Raylib.GetScreenHeight() - 20, Color.BLACK);
        Raylib.DrawRectangle(20, Raylib.GetScreenHeight() - 20, Raylib.GetScreenWidth(), 20, Color.BLACK);
    }
}