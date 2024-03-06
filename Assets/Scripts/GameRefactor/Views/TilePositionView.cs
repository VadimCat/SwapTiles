using System;
using Client.Views;
using DG.Tweening;
using GameRefactor.Interfaces;
using UnityEngine;

namespace GameRefactor.Views
{
 public class TilePositionView :MonoBehaviour,  ITilePosition
 {
  public class Factory
  {
   private readonly GridField _gridField;

   public Factory(GridField gridField)
   {
    _gridField = gridField;
   }
   
   public TilePositionView Create(GameObject root, ITilePosition tilePosition)
   {
    var component = root.AddComponent<TilePositionView>();
    component.Construct(tilePosition, _gridField);
    return component;
   }
  }
  
  private ITilePosition _tilePosition;
  private GridField _gridField;
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

  private void Construct(ITilePosition tilePosition, GridField gridField)
  {
   
   _tilePosition = tilePosition;

   _gridField = gridField;
   transform.position = gridField.GetPoint(Position);
   _tilePosition.EventPositionUpdated += OnPositionUpdated;
  }

  private void OnPositionUpdated(Vector3Int position)
  {
   var newPos = _gridField.GetPoint(position);
   transform.DOMove(newPos, MoveTime);
  }

  public void MoveTo(Vector3Int tilePosition)
  {
   _tilePosition.MoveTo(tilePosition);
  }
 }
}