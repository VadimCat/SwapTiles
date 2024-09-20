using System;
using UnityEngine;

namespace SwapTiles.Models.Solvables
{
 public class TilePosition : ITilePosition
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

  public Vector3Int Position
  {
   get => _position;
   private set
   {
    _position = value;
    EventPositionUpdated?.Invoke(_position);
   }
  }

  public event Action<Vector3Int> EventPositionUpdated;
  private Vector3Int _position;

  public Vector3Int OriginalPos { get; }

  public TilePosition(Vector2Int originalPos, Vector2Int startPos)
  {
   OriginalPos = (Vector3Int)originalPos;
   Position = (Vector3Int)startPos;
  }

  public void MoveTo(Vector3Int destination)
  {
   Position = destination;
   IsCompleted = Position == OriginalPos;
  }
 }
}