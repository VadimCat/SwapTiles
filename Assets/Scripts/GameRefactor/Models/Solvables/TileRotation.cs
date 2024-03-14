using System;
using Client.Models;
using GameRefactor.Interfaces;

namespace GameRefactor.Models
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
        
  private readonly int _rotationAngle;
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

  public TileRotation(int startRotation, int rotationAngle)
  {
   _rotationAngle = rotationAngle;
   Rotation = startRotation;
  }
        
  public void Rotate(RotationDirection direction)
  {
   if (IsCompleted)
   {
    return;
   }
            
   Rotation = ClampAngle(Rotation + (int)direction * _rotationAngle);
   if (Rotation == 0)
   {
    IsCompleted = true;
   }
  }
        
  private int ClampAngle(int rotation)
  {
   return (rotation % 360 + 360) % 360;
  }
 }
}