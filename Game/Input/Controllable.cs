namespace Game.Input;

using System.Numerics;

public class Controllable
{
    public Vector2 Position;
    public Vector2 Dimensions;

    public void Move(int rx, int ry)
    {
        Position.X += rx;
        Position.Y += ry;
    }
}