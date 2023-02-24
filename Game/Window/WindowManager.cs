using System.Numerics;
using Game.Window.Render;
using Raylib_cs;

namespace Game.Window;

public class WindowManager
{
    public Camera2D Camera;
    
    // Initialisation of the window
    public void CreateWindow()
    {
        Raylib.InitWindow(
        1280,
        720,
        GameConst.Name
        );

        Raylib.SetTargetFPS(60);
        Renderer = new Renderer();
        Camera = new Camera2D();

        Camera.target = new Vector2(0, 0);
        Camera.offset = new Vector2(0, 0);
        Camera.rotation = 0.0f;
        Camera.zoom = 1.0f;
    }

    // Update loop
    public void DrawLoop()
    {
        while (!Raylib.WindowShouldClose()) 
        {

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            Raylib.BeginMode2D(Camera);
            
            
            Loader.Game.Tick();
            Renderer.Draw();
            
            
            Raylib.EndDrawing();
        }
        
        Raylib.CloseWindow();
    }
    
    public Renderer Renderer { get; set; }
}