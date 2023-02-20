using System.Numerics;
using Game.Menu.Render;
using Raylib_cs;

namespace Game.Menu.Misc;

public class Button
{
    public Vector2 Position { get; set; }
    public Vector2 Dimensions { get; set; }
    public Color bgColor { get; set; }
    public Color borderColor { get; set; }
    public Text Text { get; set; }

    public ButtonRenderer Renderer;

    public Button(Text text)
    {
        Text = text;
        Renderer = new ButtonRenderer(Guid.NewGuid(), this);
    }
    public bool IsMouseOver()
    {
        return Raylib.GetMousePosition().X > Position.X && Raylib.GetMousePosition().X < Position.X + Dimensions.X && Raylib.GetMousePosition().Y > Position.Y &&
               Raylib.GetMousePosition().Y < Position.Y + Dimensions.Y;
    }

    public bool IsClicked()
    {
        return IsMouseOver() && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT);
    }
}