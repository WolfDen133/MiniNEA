using System.Numerics;
using Game.Menu.Screens;

namespace Game.Menu;

public class MenuManager
{
    public int CurrentWindow { get; set; }

    private Dictionary<int, Screens.Window> _windows = new Dictionary<int, Screens.Window>();

    public MenuManager ()
    {
        RegisterMenus();

        SetActiveWindow(LevelSelectScreen.UI_ID);
    }

    public void Tick()
    {
        if (!IsScreenActive()) return;
        _windows.TryGetValue(CurrentWindow, out Screens.Window? window);
        window.Tick();
    }

    private void RegisterMenus()
    {
        LevelSelectScreen screen = new LevelSelectScreen();
        _windows.Add(LevelSelectScreen.UI_ID, screen);
        Loader.Game.RenderManager.RenderMenu(screen.Renderer);
    } 

    public void SetActiveWindow(int id)
    {
        DisableAll();
        CurrentWindow = id;
        _windows.TryGetValue(CurrentWindow, out Screens.Window? window);
        Loader.WindowManager.Renderer.EnableRenderer(window.Renderer.Identifier);
        Loader.WindowManager.Camera.offset = new Vector2();
        Loader.WindowManager.Camera.target = new Vector2();
    }

    public void DisableAll()
    {
        foreach (var pair in _windows)
        {
            Loader.WindowManager.Renderer.DisableRenderer(pair.Value.Renderer.Identifier);
        }
        
        CurrentWindow = 0;
    }

    public bool IsScreenActive()
    {
        return CurrentWindow != 0;
    }
}