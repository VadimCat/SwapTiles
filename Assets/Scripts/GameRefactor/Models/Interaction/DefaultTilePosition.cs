using Client.Views;
using Models.Solvables;
using UnityEngine;

namespace Models.Interaction
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

  public class Factory
  {
   private readonly GridField _grid;

   public Factory(GridField grid)
   {
    _grid = grid;
   }

   public DefaultTilePosition Create(ITilePosition tilePos, Transform tileRoot)
   {
    return new DefaultTilePosition(tilePos, tileRoot, _grid);
   }
  }
 }
}