using Game.Window.Render.Renderers;
using Raylib_cs;

namespace Game.Ui.Overlay.Render;

public class DebugRenderer : UiRenderer
{
    public DebugRenderer () : base("game:overlay.debug") {}

    public override void Draw()
    {
        if (!Loader.Game.IsRunning || Loader.Game.Player == null) return;
        
        Raylib.DrawText(
            "GAME: " + "\n" +
            " FPS: " + Raylib.GetFPS() + "\n" +
            " Delta: " + Raylib.GetFrameTime() + "\n" +
            "\n\n" +
            "PLAYER: \n Position: " + Loader.Game.Player.Position.X + ", " + Loader.Game.Player.Position.Y + "\n" + 
            " Velocity: " + Loader.Game.Player.Controller._velocity.X + " " + Loader.Game.Player.Controller._velocity.Y + "\n" +
            " OnPlatform: " + (Loader.Game.Player.Controller.OnGround ? "true" : "false") + "\n"
            , 0, 0, 24, Color.WHITE);
    }
}