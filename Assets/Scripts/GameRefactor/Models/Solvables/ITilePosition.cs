using System;
using UnityEngine;

namespace Models.Solvables
{
 public interface ITilePosition: ITileEngine
 {
  Vector3Int Position { get; }
  Vector3Int OriginalPos { get; }
  event Action<Vector3Int> EventPositionUpdated;
  public void MoveTo(Vector3Int destination);
 }
}