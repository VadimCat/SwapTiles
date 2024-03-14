using System;
using System.Collections.Generic;
using System.Linq;
using Client.Presenters;
using GameRefactor.Interfaces;
using UnityEngine.Assertions;

namespace GameRefactor.Models
{
 public class TileComposite: ITileSolvable
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
  private readonly IEnumerable<ITileSolvable> _solvableItems;
  
  public TileComposite(IEnumerable<ITileSolvable> solvableItems)
  {
   _solvableItems = solvableItems;
   foreach (var solvable in solvableItems)
   {
    solvable.EventIsCompletedUpdated += CheckSolve;
   }
  }

  private void CheckSolve(bool obj)
  {
   bool isSolved = _solvableItems.All(s => s.IsCompleted);
   IsCompleted = isSolved;
  }
 }
}