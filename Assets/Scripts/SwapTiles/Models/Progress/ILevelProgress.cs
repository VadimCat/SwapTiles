using System;

namespace SwapTiles.Models.Progress
{
 public interface ILevelProgress
 {
  string Key { get; }
  LevelState Progress { get; }
  event Action<LevelState> EventProgressUpdated;
  void Load();
  void Complete();
  void Unlock();
 }
}