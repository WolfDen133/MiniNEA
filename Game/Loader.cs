
using Game.Window;

namespace Game;

public class Loader
{
    public static void Main(string[] args)
    {
        WindowManager = new WindowManager();
        WindowManager.CreateWindow();

        Game = new Game.Game();
        Game.Init();
        
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