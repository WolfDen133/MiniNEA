using System.Numerics;
using Game.Game.GameObj;
using Raylib_cs;

namespace Game.Input;

public class Controller
{
    private Controllable _parent;
    public Vector2 _velocity;
    
   
    public Controller(Controllable element)
    {
        _parent = element;
        _velocity = new Vector2();
    }
    
    public Floor? FloorBelow;
    public bool OnGround = false;
    public bool FloorC = true;
    public bool InputsEnabled = true;
    
    
    private void OnKeyDownSpace()
    {
        if (OnGround)
        {
            _velocity.Y = (int)(-Consts.MaxAcceleration * 1.5);
        }
    }
    
    private void OnKeyDownA()
    {
         _velocity.X -= Consts.AccelerationSpeed;

        if (_velocity.X < -Consts.MaxAcceleration) _velocity.X = -Consts.MaxAcceleration;
    }


    private void OnKeyDownD()
    {
        _velocity.X += Consts.AccelerationSpeed;

        if (_velocity.X > Consts.MaxAcceleration) _velocity.X = Consts.MaxAcceleration;
    }

    public void Tick()
    {
        HandleInput();

        _velocity.Y += Consts.Gravity;
        
        // Find position + velocity
        int rx = (int) Math.Round(Consts.MoveStep * (_velocity.X / Consts.MaxAcceleration));
        int ry = (int) Math.Round(Consts.MoveStep * (_velocity.Y / Consts.MaxAcceleration));
        
        // Basic offsets and positions
        float dimensionOffsetY = _parent.Dimensions.Y / 2;
        float dimensionOffsetX = _parent.Dimensions.X / 2;
        float footY = _parent.Position.Y + dimensionOffsetY;

        // Find the floor below or around the player (NEED TO UPDATE AND USE FUNCTION IN FLOOR CLASS)
        // By finding if any platforms intersect with player bounds + velocity
        foreach (var pair in Loader.Game.Floors)
        {
            Floor? floor = pair.Value;
            if (footY + ry > floor.Position.Y && _parent.Position.X + dimensionOffsetX + rx >= floor.Position.X && _parent.Position.X - dimensionOffsetX + rx <= floor.Position.X + floor.Dimensions.X) {
                FloorBelow = floor;
                break; 
            }

            FloorBelow = null;
        }
        
        OnGround = false;

        if (FloorBelow != null)
        {
            // Raylib.DrawText(Convert.ToString(FloorBelow._position.Y) + " " + Convert.ToString(footY), 0, 600, 24, Color.WHITE);
            
            // Check to see if is exactly ontop of platform
            if (Convert.ToInt32(FloorBelow.Position.Y) == Convert.ToInt32(footY))
            {
                OnGround = true;

                // TODO: move to player class
                if (FloorBelow.IsWin)
                {
                    Loader.Game.Win();
                }

                // If Floor collision and is not jumping stop movement
                if (!(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && InputsEnabled) && FloorC)
                {
                    _velocity.Y = 0;
                    ry = 0;
                }
            } 
            
            // If player y position + velocity is below top of platform, set next movement step in y to ontop of platform
            else if (footY + ry >= FloorBelow.Position.Y && footY <= FloorBelow.Position.Y)
            {
                ry = (int)Math.Round(FloorBelow.Position.Y - footY);
            }

            bool inYBounds = _parent.Position.Y + dimensionOffsetY > FloorBelow.Position.Y && _parent.Position.Y - dimensionOffsetY < FloorBelow.Position.Y + FloorBelow.Dimensions.Y;
            // If in Y bounds and colliding with a side of a platform
            if (inYBounds && Convert.ToInt32(FloorBelow.Position.X) == Convert.ToInt32(_parent.Position.X + dimensionOffsetX) ||
                Convert.ToInt32(FloorBelow.Position.X + FloorBelow.Dimensions.X) == Convert.ToInt32(_parent.Position.X - dimensionOffsetX))
            {
                _velocity.X = 0;
                rx = 0;
            }
            
            // If player x bounds + velocity is inside bounds, but not inside bounds for player x and in y bounds
            else if (inYBounds && ((_parent.Position.X + dimensionOffsetX + rx >= FloorBelow.Position.X &&
                                _parent.Position.X + dimensionOffsetX <= FloorBelow.Position.X) 
                               ||
                               (_parent.Position.X - dimensionOffsetX + rx <= FloorBelow.Position.X + FloorBelow.Dimensions.X &&
                                _parent.Position.X - dimensionOffsetX >= FloorBelow.Position.X + FloorBelow.Dimensions.X)))
            {
                if (_parent.Position.X < FloorBelow.Position.X + (FloorBelow.Dimensions.X / 2)) rx = (int)Math.Round(FloorBelow.Position.X - (_parent.Position.X + dimensionOffsetX));
                else rx = (int)Math.Round((FloorBelow.Position.X + FloorBelow.Dimensions.X) - (_parent.Position.X - dimensionOffsetX));
            }

        }

        // Move player with calculated phisics
        _parent.Move(rx, ry);

        // Position check, out of bounds
        if (!(_parent.Position.Y - _parent.Dimensions.Y + 100 > Game.Game.FloorHeight)) return;
        _parent.Position = new Vector2(50, Game.Game.FloorHeight - 175);
        _velocity = new Vector2();
    }

    private void HandleInput()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && InputsEnabled) OnKeyDownSpace();
        else { if (_velocity.Y < 0)  _velocity.Y += Consts.AccelerationSpeed / 2; }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_S)&& InputsEnabled) FloorC = false;
        else FloorC = true; 
        
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)&& InputsEnabled) { OnKeyDownA(); } 
        else { if (_velocity.X < 0) _velocity.X += Consts.AccelerationSpeed / 2; }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D)&& InputsEnabled) { OnKeyDownD(); } 
        else { if (_velocity.X > 0) _velocity.X -= Consts.AccelerationSpeed / 2; }
    }
}