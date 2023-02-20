using System.Numerics;
using Game.Menu.Misc;
using Game.Menu.Render;
using Raylib_cs;

namespace Game.Menu.Screens;

public class LevelSelectScreen : Window
{
    public const int UI_ID = 1;
    
    public Dictionary<int, Button?> Buttons = new Dictionary<int, Button?>();
    
    public LevelSelectScreen()
    {
        Renderer = new LevelScreenRenderer(this);
        RegisterButtons();
    }

    public override void Tick()
    {
        int lowestY = 0;
        int highestY = Raylib.GetScreenHeight();
        
        foreach (var pair in Buttons)
        {
            if (lowestY < pair.Value.Position.Y + pair.Value.Dimensions.Y) lowestY = (int) pair.Value.Position.Y + (int) pair.Value.Dimensions.Y;
            if (highestY > pair.Value.Position.Y) highestY = (int)pair.Value.Position.Y;
            
            Color color = pair.Value.bgColor;
            
            if (pair.Value.IsMouseOver())
            {
                color.a = 255;
                pair.Value.bgColor = color;
            }
            else
            {
                color.a = 100;
                pair.Value.bgColor = color;
            }

            if (pair.Value.IsClicked())
            {
                SelectLevel(pair.Key);
            }
        }

        
        if (Raylib.GetMouseWheelMove() != 0)
        {
            int offset = (int) Raylib.GetMouseWheelMove() * 20;

            if (lowestY + offset < Raylib.GetScreenHeight() - 50) offset = 0;
            if (highestY + offset > 130) offset = 0;

            foreach (var pair in Buttons)
            {
                Vector2 position = pair.Value.Position;
                position.Y += offset;
                pair.Value.Position = position;
            }
        }
        
    }
    
    private void RegisterButtons()
    {
        foreach (var pair in Loader.Game.LevelManager.Levels)
        {
            string name = pair.Key;

            int id = Buttons.Count + 1;
            
            AddButton(id, name);
        }
    }

    private void SelectLevel(int id)
    {
        Buttons.TryGetValue(id, out Button? button);

        string lvlName = button.Text.Data;
        
        Loader.Game.LevelManager.Levels.TryGetValue(lvlName, out Level.Level? level);

        Loader.Game.Level = level;
        
        Loader.Game.MenuManager.DisableAll();
        Loader.Game.LoadLevel();
        Loader.Game.IsRunning = true;
    }

    private void AddButton (int id, string text)
    {
        Text buttonText = new Text();

        buttonText.Color = Color.WHITE;
        buttonText.FontSize = 48;
        buttonText.Data = text;
        
        Button? button = new Button(buttonText);
        button.bgColor = new Color(40, 40, 40, 100);
        button.borderColor = Color.WHITE;
        
        int y = (Buttons.Count + 1) * 140;

        button.Position = new Vector2(120, y);
        button.Dimensions = new Vector2(400, 120);
        
        Buttons.Add(id, button);
    }
}
