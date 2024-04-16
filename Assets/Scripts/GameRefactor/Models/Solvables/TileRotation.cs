using System;
using Ji2Core.DataTypes;

namespace Models.Solvables
{
 public class TileRotation : ITileRotation
 {
  public bool IsCompleted
  {
   get => _isCompleted;
   private set
   {
    _isCompleted = value;
    EventIsCompletedUpdated?.Invoke(_isCompleted);
   }
  }

  public event Action<bool> EventIsCompletedUpdated;
  private bool _isCompleted;
        
  private readonly int _rotationsCount;
  public int Rotation
  {
   get => _rotation;
   private set
   {
    _rotation = value;
    EventRotationUpdated?.Invoke(_rotation);
   }
  }

  public event Action<int> EventRotationUpdated;
  private int _rotation;

  public TileRotation(int startRotation, int rotationsCount)
  {
   _rotationsCount = 360 / rotationsCount;
   Rotation = startRotation;
  }
        
  public void Rotate(RotationDirection direction)
  {
   Rotation = ClampAngle(Rotation + (int)direction * _rotationsCount);

   IsCompleted = Rotation == 0;
  }
        
  private int ClampAngle(int rotation)
  {
   return (rotation % 360 + 360) % 360;
  }
 }
}