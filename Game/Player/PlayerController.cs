using System.ComponentModel.Design.Serialization;
using System.Numerics;
using Game.Game.GameObj;
using Raylib_cs;

namespace Game.Player;

public class PlayerController
{
    public Vector2 _velocity;
    private readonly Player _parent;
    
    private const int AccelerationSpeed = 2; // How many pixels per second are added per-frame
    private const int MaxAcceleration = 20; // Maximum pixels per second that are added per-frame
    private const int MoveStep = 20; // How many pixels per second that are times by acceleration, that are then added to the players position 
    private const int Gravity = 1;

    public Floor? FloorBelow;
    public bool OnGround = false;
    public bool FloorC = true;
    public PlayerController(Player player)
    {
        _velocity = new Vector2();
        _parent = player;
    }
    private void OnKeyDownSpace()
    {
        if (OnGround)
        {
            _velocity.Y = -MaxAcceleration * 1.5f;
        }
    }
    
    private void OnKeyDownA()
    {
         _velocity.X -= AccelerationSpeed;

        if (_velocity.X < -MaxAcceleration) _velocity.X = -MaxAcceleration;
    }

    
    private void OnKeyDownD()
    {
        _velocity.X += AccelerationSpeed;

        if (_velocity.X > MaxAcceleration) _velocity.X = MaxAcceleration;
    }

    public void Move(int x, int y)
    {
        Vector2 position = _parent.Position;
        position.X += Convert.ToSingle(x);
        position.Y += Convert.ToSingle(y);
        _parent.Position = position;
    }

    public void Tick()
    {
        Floor? floor;
        
        HandleInput();
        
        _velocity.Y += Gravity;
        
        int rx = (int) Math.Round(MoveStep * (_velocity.X / MaxAcceleration));
        int ry = (int) Math.Round(MoveStep * (_velocity.Y / MaxAcceleration));

        float dimensionOffsetY = _parent.Dimensions.Y / 2;
        float dimensionOffsetX = _parent.Dimensions.X / 2;
        float footY = _parent.Position.Y + dimensionOffsetY;
        
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
            
            
            if (Convert.ToInt32(FloorBelow._position.Y) == Convert.ToInt32(footY))
            {
                OnGround = true;

                if (!Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && FloorC)
                {
                    _velocity.Y = 0;
                    ry = 0;
                }
            } 
            
            else if (footY + ry >= FloorBelow._position.Y && footY <= FloorBelow._position.Y)
            {
                ry = (int)Math.Round(FloorBelow._position.Y - footY);
            }

            bool inYBounds = _parent.Position.Y + dimensionOffsetY > FloorBelow._position.Y && _parent.Position.Y - dimensionOffsetY < FloorBelow._position.Y + FloorBelow._dimensions.Y;
            // On edge of platform
            if (Convert.ToInt32(FloorBelow._position.X) == Convert.ToInt32(_parent.Position.X + dimensionOffsetX) ||
                Convert.ToInt32(FloorBelow._position.X + FloorBelow._dimensions.X) == Convert.ToInt32(_parent.Position.X - dimensionOffsetX))
            {
                if (inYBounds)
                {
                    _velocity.X = 0;
                    rx = 0;
                }

            }
            
            // Is player inside platform X bounds for the current X + rx, but not inside the platform for X
            else if (((_parent.Position.X + dimensionOffsetX + rx >= FloorBelow._position.X &&
                      _parent.Position.X + dimensionOffsetX <= FloorBelow._position.X) 
                     ||
                     (_parent.Position.X - dimensionOffsetX + rx <= FloorBelow._position.X + FloorBelow._dimensions.X &&
                      _parent.Position.X - dimensionOffsetX >= FloorBelow._position.X + FloorBelow._dimensions.X))
                     && inYBounds)
            {
                if (_parent.Position.X < FloorBelow._position.X + (FloorBelow._dimensions.X / 2)) rx = (int)Math.Round(FloorBelow._position.X - (_parent.Position.X + dimensionOffsetX));
                else rx = (int)Math.Round((FloorBelow._position.X + FloorBelow._dimensions.X) - (_parent.Position.X - dimensionOffsetX));
            }

        }
        
        Move(rx, ry);

        if (!(_parent.Position.Y - _parent.Dimensions.Y + 100 > Game.Game.FloorHeight)) return;
        _parent.Position = new Vector2(50, Game.Game.FloorHeight - 175);
        _velocity = new Vector2();
    }

    private void HandleInput()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)) OnKeyDownSpace();
        else { if (_velocity.Y < 0)  _velocity.Y += AccelerationSpeed / 2; }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_S)) FloorC = false;
        else FloorC = true; 
        
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) { OnKeyDownA(); } 
        else { if (_velocity.X < 0) _velocity.X += AccelerationSpeed / 2; }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) { OnKeyDownD(); } 
        else { if (_velocity.X > 0) _velocity.X -= AccelerationSpeed / 2; }
    }
}
