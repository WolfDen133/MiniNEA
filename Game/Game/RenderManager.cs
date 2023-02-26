using Game.Game.GameObj;
using Game.Player;
using Game.Ui.Overlay.Render;
using Game.Window.Render.Renderers;

namespace Game.Game;

public class RenderManager
{
    public RenderManager()
    {
        RegisterDefault();
    }

    private void RegisterDefault()
    {
        if (GameConst.Debug) Loader.WindowManager.Renderer.RegisterUiRenderer(new DebugRenderer());
        Loader.WindowManager.Renderer.RegisterUiRenderer(new TimerRenderer());
    }
    
    public void RenderPlayer(Player.Player player)
    {
        Loader.WindowManager.Renderer.RegisterElementRenderer(player.Renderer);
    }

    public void RenderFloors(Dictionary<string, Floor> floors)
    {
        foreach (var pair in floors)
        {
            Loader.WindowManager.Renderer.RegisterElementRenderer(pair.Value.Renderer);
        }
    }

    public void UnrenderFloors(Dictionary<string, Floor> floors)
    {
        foreach (var pair in floors)
        {
            Loader.WindowManager.Renderer.UnregisterRenderer(pair.Value.Renderer.Identifier);
        }
    }
    
    public void UnrenderPlayer(Player.Player player)
    {
        Loader.WindowManager.Renderer.UnregisterRenderer(player.Renderer.Identifier);
    }

    public void RenderMenu(UiRenderer renderer)
    {
        Loader.WindowManager.Renderer.RegisterUiRenderer(renderer);
    }
}

