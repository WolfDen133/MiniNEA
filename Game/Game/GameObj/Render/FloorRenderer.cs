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
            (int) _floor.Position.X,
            (int) _floor.Position.Y,
            (int) _floor.Dimensions.X,
            (int) _floor.Dimensions.Y,
            _floor.IsWin ? Color.LIME : Color.GRAY
        );
    }
}