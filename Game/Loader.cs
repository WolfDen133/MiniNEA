
using Game.Window;

namespace Game;

public class Loader
{
    public static void Main(string[] args)
    {
        // Initialisation
        WindowManager = new WindowManager();
        WindowManager.CreateWindow();

        // Logic
        Game = new Game.Game();
        Game.Init();
        
        // Update
        WindowManager.DrawLoop();
    }

    
    public static WindowManager? WindowManager
    { get; set; }


    public static Game.Game? Game
    {
        get;
        set;
    }

}