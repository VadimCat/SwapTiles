using Client.Views;
using GameRefactor.Interfaces;
using UnityEngine;

namespace GameRefactor.Models.Interaction
{
 public class DefaultTilePosition
 {
  private readonly ITilePosition _tilePosition;
  private readonly Transform _transform;
  private readonly GridField _grid;

  public DefaultTilePosition(ITilePosition tilePosition, Transform transform, GridField grid)
  {
   _tilePosition = tilePosition;
   _transform = transform;
   _grid = grid;
  }

  public bool IsOnDefaultPosition()
  {
   Vector3 defaultWorldPos = _grid.GetPoint(_tilePosition.Position);
   Vector3 positionCached = _transform.position;
   return Mathf.Approximately(defaultWorldPos.x, positionCached.x) &&
          Mathf.Approximately(defaultWorldPos.y, positionCached.y);
  }
 }
}