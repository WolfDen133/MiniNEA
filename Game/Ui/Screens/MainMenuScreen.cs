using System.Numerics;
using Game.Ui.Misc;
using Game.Ui.Render;
using Raylib_cs;

namespace Game.Ui.Screens;

public class MainMenuScreen : Window
{
    public static int UI_ID = 2;
    
    public Dictionary<int, Button?> Buttons = new Dictionary<int, Button?>();

    public MainMenuScreen()
    {
        RegisterButtons();
        Renderer = new MainMenuScreenRenderer(this);
    }

    
    private void RegisterButtons()
    {
        AddButton(0, "Play");
        AddButton(1, "Options");
        AddButton(2, "Exit");
    }

    public override void Tick()
    {
        foreach (var pair in Buttons)
        {
            if (pair.Value.IsClicked()) Select(pair.Key);
        }
    }

    private void Select(int buttonId)
    {
        switch (buttonId)
        {
            case 0:
                Buttons.TryGetValue(buttonId, out Button? button);
                Loader.Game.MenuManager.SetActiveWindow(LevelSelectScreen.UI_ID);
                break;
            case 1:
                // TODO OPTIONS
                break;
            case 2:
                Loader.Game.Close();
                break;
        }
    }

    private void AddButton (int id, string text)
    {
        Text buttonText = new Text();

        buttonText.Color = Color.WHITE;
        buttonText.FontSize = 48;
        buttonText.Data = text;
        buttonText.Font = FontUtils.ButtonFont;
        
        Button? button = new Button(buttonText);
        button.bgColor = new Color(40, 40, 40, 200);
        button.borderColor = Color.WHITE;
        
        int y = ((Raylib.GetScreenHeight() / 2) - 200) + (Buttons.Count + 1) * 140;

        button.Position = new Vector2(((Raylib.GetScreenWidth() / 2) - 175), y);
        button.Dimensions = new Vector2(350, 80);
        
        Buttons.Add(id, button);
    }
}