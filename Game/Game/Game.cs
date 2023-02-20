

using System.Numerics;
using Game.Game.GameObj;
using Game.Level;
using Game.Menu;
using Game.Menu.Screens;
using Game.Window;
using Raylib_cs;

namespace Game.Game;

public class Game
{
    private Player.Player? _player;

    public RenderManager RenderManager;
    public LevelManager LevelManager;
    public MenuManager MenuManager;

    public Dictionary<string, Floor> Floors = new Dictionary<string, Floor>();

    public const int FloorHeight = 10000;

    public Level.Level? Level;

    public bool IsRunning;

    public Game()
    {
    }

    public void Init()
    {
        RenderManager = new RenderManager();
        LevelManager = new LevelManager();
        MenuManager = new MenuManager();
    }

    public void LoadLevel()
    {
        Reset();
        RegisterFloors();
        
        int sy = FloorHeight - (int)Level.GetSpawn().Y;
        
        _player = new Player.Player(new Vector2(Level.GetSpawn().X, sy));
        
        Loader.WindowManager.Camera.target = _player.Position with { Y = FloorHeight };
        
        RenderElements();
    }

    private void Reset()
    {
        if (Floors.Count > 0) RenderManager.UnrenderFloors(Floors);
        if (_player != null) RenderManager.UnrenderPlayer(_player);
        
        Floors = new Dictionary<string, Floor>();
        _player = null;
    }

    private void RegisterFloors()
    {
        foreach (var floorData in Level.GetFloors())
        {
            int y = FloorHeight - (int)floorData.Position.Y;
            Floors.Add(Guid.NewGuid().ToString(), new Floor(floorData.Position with { Y = y }, floorData.Dimensions));
        }
    }

    private void RenderElements()
    {
        RenderManager.RenderFloors(Floors);
        RenderManager.RenderPlayer(_player); 
    }

    public void Tick()
    {
        MenuManager.Tick();
        
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
        {
            if (IsRunning)
            {
                IsRunning = false;
                MenuManager.SetActiveWindow(LevelSelectScreen.UI_ID);
                return;
            } 
            
            if (_player != null)
            {
                IsRunning = true;
                MenuManager.DisableAll();
            }
        }
        
        if (!IsRunning) return;
        
        if (GameConst.Debug)
        {
            Raylib.DrawText(
                "GAME: " + "\n" +
                " FPS: " + Raylib.GetFPS() + "\n" +
                " Delta: " + Raylib.GetFrameTime() + "\n" +
                "\n\n" +
                "PLAYER: \n Position: " + _player.Position.X + ", " + _player.Position.Y + "\n" + 
                " Velocity: " + _player.Controller._velocity.X + " " + _player.Controller._velocity.Y + "\n" +
                " OnPlatform: " + (_player.Controller.OnGround ? "true" : "false") + "\n"
                , 0, 0, 24, Color.WHITE);
        }
        
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_F3)) GameConst.Debug = !GameConst.Debug;
        
        _player.Tick();
        
        int targetHeight = (int) Loader.WindowManager.Camera.target.Y;
            
        if (_player.Position.Y < targetHeight - (Raylib.GetScreenHeight() / 2 + (Raylib.GetScreenHeight() / 16)))
        {
            targetHeight -= 10;
        }

        if (_player.Position.Y > targetHeight - (Raylib.GetScreenHeight() / 16))
        {
            if (_player.Controller._velocity.Y >= 40) targetHeight = (int)_player.Position.Y;
            else targetHeight += 10;
        }

        Loader.WindowManager.Camera.target = _player.Position with { Y = targetHeight };
        Loader.WindowManager.Camera.offset = new Vector2(Raylib.GetScreenWidth() / 2 - (Raylib.GetScreenWidth() / 8), Raylib.GetScreenHeight() / 2 + (Raylib.GetScreenHeight() / 4));


        if (Raylib.IsKeyDown(KeyboardKey.KEY_E) && Loader.WindowManager.Camera.zoom < 1.3f) Loader.WindowManager.Camera.zoom += 0.05f;
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_Q)&& Loader.WindowManager.Camera.zoom > 0.7f) Loader.WindowManager.Camera.zoom -= 0.05f;
    }
}

