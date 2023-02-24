using System.Numerics;
using Game.Game.GameObj;
using Game.Level;
using Game.Menu;
using Game.Menu.Screens;
using Raylib_cs;

namespace Game.Game;

public class Game
{
    public Player.Player? Player;

    public RenderManager RenderManager;
    public LevelManager LevelManager;
    public MenuManager MenuManager;
    public CameraController CameraController;

    public Dictionary<string, Floor> Floors = new ();

    public const int FloorHeight = 10000;

    public Level.Level? Level;

    public bool IsRunning;
    
    public void Init()
    {
        RenderManager = new RenderManager();
        CameraController = new CameraController(this);
        LevelManager = new LevelManager();
        MenuManager = new MenuManager();
    }

    // Load the selected level data and begin game
    public void LoadLevel()
    {
        Reset();
        RegisterFloors();
        
        int sy = FloorHeight - (int)Level.GetSpawn().Y;
        
        Player = new Player.Player(new Vector2(Level.GetSpawn().X, sy));
        
        Loader.WindowManager.Camera.target = Level.GetSpawn() with {Y = FloorHeight };
        
        RenderElements();
    }

    // Remove all level data
    private void Reset()
    {
        if (Floors.Count > 0) RenderManager.UnrenderFloors(Floors);
        if (Player != null) RenderManager.UnrenderPlayer(Player);
        
        Floors = new Dictionary<string, Floor>();
        Player = null;
    }

    // Register all level platforms
    private void RegisterFloors()
    {
        // Iterate over all floors in the level data and register them as game objects
        foreach (var floorData in Level.GetFloors())
        {
            int y = FloorHeight - (int)floorData.Position.Y;
            Floors.Add(Guid.NewGuid().ToString(), new Floor(floorData.Position with { Y = y }, floorData.Dimensions));
        }
    }

    // Load renderers 
    private void RenderElements()
    {
        RenderManager.RenderFloors(Floors);
        RenderManager.RenderPlayer(Player); 
    }

    // Tick() function in all files executes the main logic for that frame
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
            
            if (Player != null)
            {
                IsRunning = true;
                MenuManager.DisableAll();
            }
        }
        
        if (!IsRunning) return;
        
        Player.Tick();
        CameraController.Tick();

        // Camera controls
    }
}

