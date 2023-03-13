using System.Numerics;
using Game.Ui.Screens;

namespace Game.Ui;

public class MenuManager
{
    public int CurrentWindow { get; set; }

    private Dictionary<int, Screens.Window> _windows = new Dictionary<int, Screens.Window>();

    public MenuManager ()
    {
        RegisterMenus();
        
        SetActiveWindow(2);
    }

    public void Tick()
    {
        if (!IsScreenActive()) return;
        _windows.TryGetValue(CurrentWindow, out Screens.Window? window);
        window.Tick();
    }

    private void RegisterMenus()
    {
        RegisterMenu(LevelSelectScreen.UI_ID, new LevelSelectScreen());
        RegisterMenu(MainMenuScreen.UI_ID, new MainMenuScreen());
    }

    private void RegisterMenu(int id, Screens.Window menu)
    {
        Console.WriteLine(menu + " " + id);
        _windows.Add(id, menu);
        
        Loader.Game.RenderManager.RenderMenu(menu.Renderer);
    }
    

    public void SetActiveWindow(int id)
    {
        DisableAll();
        CurrentWindow = id;
        _windows.TryGetValue(CurrentWindow, out Screens.Window? window);
        Loader.WindowManager.Renderer.EnableRenderer(window.Renderer.Identifier);
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