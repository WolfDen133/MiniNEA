using System.Numerics;
using Game.Game.GameObj.Render;

namespace Game.Game.GameObj;

public class Floor
{
    public Vector2 Position;
    public Vector2 Dimensions;
    public bool IsWin;
    public readonly FloorRenderer? Renderer;

    public Floor (Vector2 position, Vector2 dimensions, bool isWin)
    {
        Position = position;
        Dimensions = dimensions;
        IsWin = isWin;

        Renderer = new FloorRenderer(Guid.NewGuid(), this);
    }
}