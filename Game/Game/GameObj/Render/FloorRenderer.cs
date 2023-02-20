using Game.Window.Render.Renderers;
using Raylib_cs;

namespace Game.Game.GameObj.Render;

public class FloorRenderer : ElementRenderer
{
    private readonly Floor _floor;
    public FloorRenderer(Guid id, Floor floor) : base("game:floor." + id)
    {
        _floor = floor;
    }

    public override void Draw()
    {
        Raylib.DrawRectangle(
            (int) _floor._position.X,
            (int) _floor._position.Y,
            (int) _floor._dimensions.X,
            (int) _floor._dimensions.Y,
            Color.GRAY
        );
    }
}