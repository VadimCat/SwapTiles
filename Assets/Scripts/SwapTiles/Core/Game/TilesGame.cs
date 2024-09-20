using System;
using System.Collections.Generic;
using SwapTiles.Models.Solvables;

namespace SwapTiles.Game
{
 public class TilesGame: IDisposable
 {
  public event Action LevelCompleted;
  private readonly List<ITileEngine> _solvableItems;
  
  public TilesGame(List<ITileEngine> solvableItems)
  {
   _solvableItems = solvableItems;
   foreach (var item in solvableItems)
   {
    item.EventIsCompletedUpdated += CheckComplete;
   }
  }

  private void CheckComplete(bool completed)
  {
   bool levelCompleted = _solvableItems.TrueForAll(i => i.IsCompleted);
   if (levelCompleted)
   {
    LevelCompleted?.Invoke();
   }
  }

  public void Dispose()
  {
   foreach (var item in _solvableItems)
   {
    item.EventIsCompletedUpdated -= CheckComplete;
   }
  }
 }
}