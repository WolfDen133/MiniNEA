namespace Game.Window.Render.Renderers;

public class SimpleRenderer
{
    public bool isEnabled;
    
    public void enable()
    {
        isEnabled = true;
    }

    public void disable()
    {
        isEnabled = false;
    }
    public virtual void Draw () {}
}