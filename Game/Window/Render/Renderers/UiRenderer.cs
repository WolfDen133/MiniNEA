namespace Game.Window.Render.Renderers;

// Renderer that renders all ui layer elements, (always drawn last)
public class UiRenderer : SimpleRenderer
{
    public UiRenderer(string identifier, bool isEnabled = true)
    {
        Identifier = identifier;
        this.isEnabled = isEnabled;
    }
    public string Identifier { get; set; }
}