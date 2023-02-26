using System.Diagnostics;
using System.Numerics;
using Game.Window.Render.Renderers;
using Raylib_cs;

namespace Game.Ui.Overlay.Render;

public class TimerRenderer : UiRenderer
{
    public TimerRenderer () : base("game:overlay.timer") {}

    public override void Draw()
    {
        if (!Loader.Game.IsRunning) return;

        TimeSpan tspan = Loader.Game.Timer.Elapsed;
        string text = $"{tspan.Minutes:00}:{tspan.Seconds:00}.{tspan.Milliseconds / 10:00}";
        
        Raylib.DrawTextEx(Loader.Game.TimerFont, text, new Vector2(Raylib.GetScreenWidth() / 2 - 65, 10), 32, 2, Color.WHITE);
    }
}