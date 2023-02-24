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
        Floor? floor;
        
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
            floor = pair.Value;
            if (footY + ry > floor._position.Y && _parent.Position.X + dimensionOffsetX + rx >= floor._position.X && _parent.Position.X - dimensionOffsetX + rx <= floor._position.X + floor._dimensions.X) {
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
            if (Convert.ToInt32(FloorBelow._position.Y) == Convert.ToInt32(footY))
            {
                OnGround = true;

                // If Floor collision and is not jumping stop movement
                if (!Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && FloorC)
                {
                    _velocity.Y = 0;
                    ry = 0;
                }
            } 
            
            // If player y position + velocity is below top of platform, set next movement step in y to ontop of platform
            else if (footY + ry >= FloorBelow._position.Y && footY <= FloorBelow._position.Y)
            {
                ry = (int)Math.Round(FloorBelow._position.Y - footY);
            }

            bool inYBounds = _parent.Position.Y + dimensionOffsetY > FloorBelow._position.Y && _parent.Position.Y - dimensionOffsetY < FloorBelow._position.Y + FloorBelow._dimensions.Y;
            // If in Y bounds and colliding with a side of a platform
            if (inYBounds && Convert.ToInt32(FloorBelow._position.X) == Convert.ToInt32(_parent.Position.X + dimensionOffsetX) ||
                Convert.ToInt32(FloorBelow._position.X + FloorBelow._dimensions.X) == Convert.ToInt32(_parent.Position.X - dimensionOffsetX))
            {
                _velocity.X = 0;
                rx = 0;
            }
            
            // If player x bounds + velocity is inside bounds, but not inside bounds for player x and in y bounds
            else if (inYBounds && ((_parent.Position.X + dimensionOffsetX + rx >= FloorBelow._position.X &&
                                _parent.Position.X + dimensionOffsetX <= FloorBelow._position.X) 
                               ||
                               (_parent.Position.X - dimensionOffsetX + rx <= FloorBelow._position.X + FloorBelow._dimensions.X &&
                                _parent.Position.X - dimensionOffsetX >= FloorBelow._position.X + FloorBelow._dimensions.X)))
            {
                if (_parent.Position.X < FloorBelow._position.X + (FloorBelow._dimensions.X / 2)) rx = (int)Math.Round(FloorBelow._position.X - (_parent.Position.X + dimensionOffsetX));
                else rx = (int)Math.Round((FloorBelow._position.X + FloorBelow._dimensions.X) - (_parent.Position.X - dimensionOffsetX));
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
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)) OnKeyDownSpace();
        else { if (_velocity.Y < 0)  _velocity.Y += Consts.AccelerationSpeed / 2; }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_S)) FloorC = false;
        else FloorC = true; 
        
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) { OnKeyDownA(); } 
        else { if (_velocity.X < 0) _velocity.X += Consts.AccelerationSpeed / 2; }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) { OnKeyDownD(); } 
        else { if (_velocity.X > 0) _velocity.X -= Consts.AccelerationSpeed / 2; }
    }
}