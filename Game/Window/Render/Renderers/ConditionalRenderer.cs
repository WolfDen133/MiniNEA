namespace Game.Window.Render.Renderers;

// Renderer that only draws when the logic calls it
public class ConditionalRenderer : UiRenderer
{
    public ConditionalRenderer(string id) : base(id) {}
    
    public new void Draw()
    { }
}