using System;

namespace GameRefactor.Interfaces
{
 public interface ITileSolvable
 {
  bool IsCompleted { get; }
  event Action<bool> EventIsCompletedUpdated;
 }
}