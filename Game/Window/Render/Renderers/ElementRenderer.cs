namespace Game.Window.Render.Renderers;

// Main renderer for rendering game elements (platform & player)
public class ElementRenderer : SimpleRenderer
{
    public ElementRenderer(string identifier, bool isEnabled = true)
    {
        Identifier = identifier;
        this.isEnabled = isEnabled;
    }
    public string Identifier { get; set; }
}