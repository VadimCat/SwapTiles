using System;
using Ji2Core.DataTypes;

namespace Models.Solvables
{
 public interface ITileRotation : ITileEngine
 {
  event Action<int> EventRotationUpdated;
  int Rotation { get; }
  void Rotate(RotationDirection direction);
 }
}