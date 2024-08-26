using System;

namespace SwapTiles.Models.Solvables
{
 public interface ITileEngine
 {
  bool IsCompleted { get; }
  event Action<bool> EventIsCompletedUpdated;
 }
}