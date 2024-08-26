using System;
using System.Collections.Generic;
using System.Linq;

namespace SwapTiles.Models.Solvables
{
 public class TileCompositeEngine: ITileEngine
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
  private readonly IEnumerable<ITileEngine> _solvableItems;
  
  public TileCompositeEngine(IEnumerable<ITileEngine> solvableItems)
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