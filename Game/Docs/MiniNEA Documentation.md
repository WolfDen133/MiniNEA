# MINI NEA

> ## Goals
> 
> - To create a 2D platform game
> - Answer questions to complete quests, to gain items, in order to complete the game
> - To incorporate information about computers into the game making it valuable to play, as it contains knolage

> ## Initial thoughs
> I want to create a 2D platform game, with the player being a 'Nano-helper', working in a PC building work shop, gathering, moving, or repairing various parts, and gaining knowledge to complete the challenges.
> I want the player to be controllable in the X and Y coordinates, and can move, grab, and pick up items, various controls and abilities being unlocked by hitting achievements and picking up items.
> I want the user to be able to learn about building or repairing computers, while playing a game.

> ## Analyse 
> The reason I want to create a 2D Q&A platform game, is because it can serve educational use, as it teaches about PC hardware, while also still being interesting to those already competent in the area.
> 

## Development

> ### Window Initialization
> 
> For my project, I will be using RayLib for C#.
> 
> Raylib requires some boilerplate to open a window and initialise it for drawing to the screen.
> The following code does just that, there is a `Loader.cs` which contains the main method and a `WindowManager.cs` that contains the window initialisation.
> ![Window Initialization Image](assets/windowinit.png)
> As you can see from the image above, I am going to be taking an object oriented approach to this project, so all game logic will be contained within classes. 

> ### Creating a render manager
> 
> Next im going to create a render class to handle the rendering of all the on screen elements separately from the other logic
> The following code in Renderer.cs is the basic code to handle element registration, and drawing to screen.
> ![](https://i.imgur.com/Ce5ygGI.png)
> ![](https://i.imgur.com/K1QZQtd.png)

> ### Game main file
> I created a main file for the main game logic to be contained.
> I also created a game render manager to send different elements to the main renderer.
> All of this is contained under Game/.
> ![](assets/game-renderer.png)

> ### Creating registering a player
> Firstly, I created a player class, and added its own custom renderer.
> Then registered it in the main, the main then calls to the renderer for the player to be rendered.
> There is a small section of code to render a white rectangle, with the center being the players coordinates.
> ![](assets/player-renderer.png)
> ![](assets/game1.png)

> ### Player controller and physics 
> Next I will be adding a player controller with included physics.
> Firstly, I created `PlayerController.cs` and initialised it inside `Player.cs`.
> Once I had done that, I then created a `Tick()` function inside the controller that will calculate the players positioning for each frame.
> Im going to be calculating player positions based off of velocities inside the controller file.
> ```csharp
>    public void Tick()
>    {
>        HandleInput();
>        
>        _velocity.Y += Gravity;
>        
>        int rx = (int) Math.Round(MoveStep * (_velocity.X / MaxAcceleration));
>        int ry = (int) Math.Round(MoveStep * (_velocity.Y / MaxAcceleration));
> 
>        Move(rx, ry);
>    }
> 
>     public void Move(int x, int y)
>     {
>        Vector2 position = _parent.Position;
>        position.X += Convert.ToSingle(x);
>        position.Y += Convert.ToSingle(y);
>        _parent.Position = position;
>     }
>```

