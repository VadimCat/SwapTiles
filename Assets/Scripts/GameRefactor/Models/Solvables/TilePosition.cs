using System;
using GameRefactor.Interfaces;
using UnityEngine;

namespace GameRefactor.Models
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

  public TilePosition(Vector3Int originalPos, Vector3Int startPos)
  {
   OriginalPos = originalPos;
   Position = startPos;
  }

  public void MoveTo(Vector3Int destination)
  {
   Position = destination;
   IsCompleted = Position == OriginalPos;
  }
 }
}