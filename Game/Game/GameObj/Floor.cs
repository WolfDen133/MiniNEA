using System.Numerics;
using Game.Game.GameObj.Render;

namespace Game.Game.GameObj;

public class Floor
{
    public Vector2 _position;
    public Vector2 _dimensions;

    public FloorRenderer? _Renderer;

    public Floor (Vector2 position, Vector2 dimensions)
    {
        _position = position;
        _dimensions = dimensions;

        _Renderer = new FloorRenderer(Guid.NewGuid(), this);
    }

    public bool IsAround(Player.Player player)
    {
        return player.Position.Y + (player.Dimensions.Y / 2) + 2 > _position.Y && 
               player.Position.X + (player.Dimensions.X / 2) + 2 > _position.X && 
               player.Position.X - (player.Dimensions.X / 2) - 2 < _position.X + _dimensions.X && 
               player.Position.Y - (player.Dimensions.Y / 2) - 2 < _position.Y + _dimensions.Y;
    }
}