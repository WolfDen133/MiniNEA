namespace Game.Window.Render;

using Renderers;

public class Renderer
{
    private Dictionary<string, ElementRenderer> ElementRenderers = new (); 

    public void RegisterElementRenderer(ElementRenderer element)
    {
        ElementRenderers.Add(element.Identifier, element);
    }

    public void Draw()
    {
        //TODO: Implement background renderers.

        foreach (KeyValuePair<string, ElementRenderer> renderer in ElementRenderers)
        {
            renderer.Value.Draw();
        }
    }
}