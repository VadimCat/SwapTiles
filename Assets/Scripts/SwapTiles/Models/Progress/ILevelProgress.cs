using System;

namespace SwapTiles.Models.Progress
{
 public interface ILevelProgress
 {
  string Key { get; }
  LevelStatus Progress { get; }
  event Action<LevelStatus> EventProgressUpdated;
  void Load();
  void Complete();
  void Unlock();
 }
}