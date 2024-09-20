using System;
using Ji2Core.DataTypes;

namespace SwapTiles.Models.Solvables
{
 public interface ITileRotation : ITileEngine
 {
  event Action<int> EventRotationUpdated;
  int DiscreteRotationAngle { get; }
  void Rotate(RotationDirection direction);
 }
}