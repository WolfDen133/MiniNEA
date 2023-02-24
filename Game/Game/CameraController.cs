using System.Numerics;
using Raylib_cs;

namespace Game.Game;

public class CameraController
{
    private Game _game;
    public CameraController(Game game)
    {
        _game = game;
    }
    
    public void Tick()
    {
        int targetHeight = (int) Loader.WindowManager.Camera.target.Y;
            
        if (_game.Player.Position.Y < targetHeight - (Raylib.GetScreenHeight() / 2 + (Raylib.GetScreenHeight() / 16)))
        {
            targetHeight -= 10;
        }

        if (_game.Player.Position.Y > targetHeight - (Raylib.GetScreenHeight() / 16))
        {
            if (_game.Player.Controller._velocity.Y >= 40) targetHeight = (int)_game.Player.Position.Y;
            else targetHeight += 10;
        }

        Loader.WindowManager.Camera.target = new Vector2(_game.Player.Position.X, targetHeight);
        Loader.WindowManager.Camera.offset = new Vector2(Raylib.GetScreenWidth() / 2 - (Raylib.GetScreenWidth() / 8), Raylib.GetScreenHeight() / 2 + (Raylib.GetScreenHeight() / 4));
        
        if (Raylib.IsKeyDown(KeyboardKey.KEY_E) && Loader.WindowManager.Camera.zoom < 1.3f) Loader.WindowManager.Camera.zoom += 0.05f;
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_Q)&& Loader.WindowManager.Camera.zoom > 0.7f) Loader.WindowManager.Camera.zoom -= 0.05f;
    }
}