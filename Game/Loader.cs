
using Game.Window;

namespace Game;

public class Loader
{
    public static void Main(string[] args)
    {
        WindowManager = new WindowManager();
        WindowManager.CreateWindow();
    }

    
    public static WindowManager? WindowManager
    { get; set; }

}