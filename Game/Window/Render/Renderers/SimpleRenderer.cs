namespace Game.Window.Render.Renderers;

// Parent of all the renderers
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