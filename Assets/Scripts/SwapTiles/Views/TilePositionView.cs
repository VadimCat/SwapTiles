using System;
using DG.Tweening;
using SwapTiles.Models.Solvables;
using UnityEngine;

namespace SwapTiles.Views
{
 public class TilePositionView: ITilePosition
 {
  private readonly Transform _transform;
  private readonly ITilePosition _tilePosition;
  private readonly GridField _gridField;
  private const float MoveTime = .5f;
  private Grid _grid;

  public bool IsCompleted => _tilePosition.IsCompleted;

  public event Action<bool> EventIsCompletedUpdated
  {
   add => _tilePosition.EventIsCompletedUpdated += value;
   remove => _tilePosition.EventIsCompletedUpdated -= value;
  }

  public event Action<Vector3Int> EventPositionUpdated
  {
   add => _tilePosition.EventPositionUpdated += value;
   remove => _tilePosition.EventPositionUpdated -= value;
  }

  public Vector3Int Position => _tilePosition.Position;

  public Vector3Int OriginalPos => _tilePosition.OriginalPos;

  private TilePositionView(Transform transform, ITilePosition tilePosition, GridField gridField)
  {
   _transform = transform;
   _tilePosition = tilePosition;

   _gridField = gridField;
   transform.position = gridField.GetPoint(Position);
   _tilePosition.EventPositionUpdated += OnPositionUpdated;
  }

  private void OnPositionUpdated(Vector3Int position)
  {
   var newPos = _gridField.GetPoint(position);
   _transform.DOMove(newPos, MoveTime);
  }

  public void MoveTo(Vector3Int tilePosition)
  {
   _tilePosition.MoveTo(tilePosition);
  }

  public class Factory
  {
   private readonly GridField _grid;

   public Factory(GridField grid)
   {
    _grid = grid;
   }

   public ITilePosition Create(Transform tileRoot, ITilePosition tilePos)
   {
    return new TilePositionView(tileRoot, tilePos, _grid);
   }
  }
 }
}