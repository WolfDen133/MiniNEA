﻿namespace Game.Window.Render.Renderers;

public class ElementRenderer : SimpleRenderer
{
    public ElementRenderer(string identifier, bool isEnabled = true)
    {
        Identifier = identifier;
        this.isEnabled = isEnabled;
    }
    public string Identifier { get; set; }
}