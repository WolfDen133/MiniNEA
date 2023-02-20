using Game.Menu.Misc;
using Game.Window.Render.Renderers;
using Raylib_cs;

namespace Game.Menu.Render;

public class ButtonRenderer : ConditionalRenderer
{
    private readonly Button _parent;
    
    public ButtonRenderer(Guid id, Button parent) : base("main:menu.button." + id)
    {
        _parent = parent;
    }

    public new void Draw()
    {
        int x = (int)_parent.Position.X;
        int y = (int)_parent.Position.Y;
        
        int dx = (int)_parent.Dimensions.X;
        int dy = (int)_parent.Dimensions.Y;

        
        Raylib.DrawRectangleLines(x, y, dx, dy, _parent.borderColor);
        Raylib.DrawRectangle(x, y, dx, dy, _parent.bgColor);

        int textX = (int) _parent.Position.X + 20;
        int textY = (int) _parent.Position.Y + ((dy / 2) - (_parent.Text.FontSize / 2));
        Raylib.DrawText(_parent.Text.Data, textX, textY, _parent.Text.FontSize, _parent.Text.Color);
    }
}