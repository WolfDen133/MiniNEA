namespace Game.Window.Render.Renderers;

public class ElementRenderer
{
    public ElementRenderer(string identifier)
    {
        Identifier = identifier;
    }
    public virtual void Draw () {}
    
    public string Identifier { get; set; }
}