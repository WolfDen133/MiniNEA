using Game.Input;
using System.Numerics;

namespace Game.Player;

public class Player : Controllable
{
    public PlayerRenderer? Renderer;

    public Controller Controller;

    public Player(Vector2 spawn)
    {
          Position = spawn;
          Dimensions = new Vector2(60, 70);
          Renderer = new PlayerRenderer(this);
          Controller = new Controller(this);
    }

    public void Tick()
    { 
        Controller.Tick();
    }
}