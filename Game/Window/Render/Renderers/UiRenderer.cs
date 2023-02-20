namespace Game.Window.Render.Renderers;

public class UiRenderer : SimpleRenderer
{
    public UiRenderer(string identifier, bool isEnabled = true)
    {
        Identifier = identifier;
        this.isEnabled = isEnabled;
    }
    public string Identifier { get; set; }
}