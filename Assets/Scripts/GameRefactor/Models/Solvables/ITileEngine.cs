using System;

namespace Models.Solvables
{
 public interface ITileEngine
 {
  bool IsCompleted { get; }
  event Action<bool> EventIsCompletedUpdated;
 }
}