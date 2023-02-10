using Game.Window.Render.Renderers;
using Raylib_cs;

namespace Game.Player;

public class PlayerRenderer : ElementRenderer
{
    public PlayerRenderer(Player parentPlayer) : base("main:player")
    {
        Player = parentPlayer;
    }

    public override void Draw()
    {
        int x = Convert.ToInt32(Player.Position.X) - (Convert.ToInt32(Player.Dimensions.X) / 2);
        int y = Convert.ToInt32(Player.Position.Y) - (Convert.ToInt32(Player.Dimensions.Y) / 2);

        int width = Convert.ToInt32(Player.Dimensions.X);
        int height = Convert.ToInt32(Player.Dimensions.Y);
        
        Raylib.DrawRectangle(x, y, width, height, Color.WHITE);
    }
    
    public Player Player { get; set; }
}