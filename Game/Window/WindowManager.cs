using Raylib_cs;

namespace Game.Window;

public class WindowManager
{
    
    public void CreateWindow()
    {
        Raylib.InitWindow(
        1280,
        720,
        GameConst.Name
        );
        
        Raylib.SetTargetFPS(60);
        
        DrawLoop();
    }

    private void DrawLoop()
    {
        while (!Raylib.WindowShouldClose()) 
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            
            Raylib.EndDrawing();
        }
    }
}