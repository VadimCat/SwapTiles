using System;
using UnityEngine;

namespace GameRefactor.Interfaces
{
 public interface ITilePosition: ITileSolvable
 {
  Vector3Int Position { get; }
  Vector3Int OriginalPos { get; }
  event Action<Vector3Int> EventPositionUpdated;
  public void MoveTo(Vector3Int destination);
 }
}