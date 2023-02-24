using Raylib_cs;

namespace Game.Window.Render;

using Renderers;

public class Renderer
{
    private Dictionary<string, ElementRenderer?> _elementRenderers = new (); 
    private Dictionary<string, UiRenderer> _uiRenderers = new ();

    public void RegisterElementRenderer(ElementRenderer? element)
    {
        _elementRenderers.Add(element.Identifier, element);
    }
    
    public void RegisterUiRenderer(UiRenderer element)
    {
        _uiRenderers.Add(element.Identifier, element);
    }

    public void UnregisterRenderer(string id)
    {
        _elementRenderers.TryGetValue(id, out ElementRenderer? elementRenderer);

        _uiRenderers.TryGetValue(id, out UiRenderer? uiRenderer);

        if (elementRenderer != null)
        {
            _elementRenderers.Remove(id);
            return;
        }

        if (uiRenderer != null)
        {
            _uiRenderers.Remove(id);
            return;
        }
    }

    public void EnableRenderer(string id)
    {
        _elementRenderers.TryGetValue(id, out ElementRenderer? elementRenderer);

        _uiRenderers.TryGetValue(id, out UiRenderer? uiRenderer);

        elementRenderer?.enable();
        uiRenderer?.enable();
    }

    public void DisableRenderer (string id)
    {
        _elementRenderers.TryGetValue(id, out ElementRenderer? elementRenderer);

        _uiRenderers.TryGetValue(id, out UiRenderer? uiRenderer);

        elementRenderer?.disable();
        uiRenderer?.disable();
    }

    public void Draw()
    {
        //TODO: Implement background renderers.

        // Render all game elements
        foreach (KeyValuePair<string, ElementRenderer?> renderer in _elementRenderers)
        {
            if (renderer.Value.isEnabled) renderer.Value.Draw();
        }
        
        // Render all ui elements
        foreach (KeyValuePair<string, UiRenderer> renderer in _uiRenderers)
        {
            if (renderer.Value.isEnabled) renderer.Value.Draw();
        }
    }
}