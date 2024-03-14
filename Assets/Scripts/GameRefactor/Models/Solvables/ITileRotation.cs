using System;
using Client.Models;

namespace GameRefactor.Interfaces
{
 public interface ITileRotation : ITileSolvable
 {
  event Action<int> EventRotationUpdated;
  int Rotation { get; }
  void Rotate(RotationDirection direction);
 }
}