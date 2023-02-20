using System.Numerics;

namespace Game.Player;

public class Player
{
    public Vector2 Position;

    public Vector2 Dimensions;

    public PlayerRenderer? Renderer;

    public PlayerController Controller;

    public Player(Vector2 spawn)
    {
          Position = spawn;
          Dimensions = new Vector2(60, 70);
          Renderer = new PlayerRenderer(this);
          Controller = new PlayerController(this);
    }

    public void Tick()
    { 
        Controller.Tick();
    }
}