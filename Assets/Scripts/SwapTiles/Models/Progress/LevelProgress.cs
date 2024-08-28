using System;
using Ji2.CommonCore.SaveDataContainer;

namespace SwapTiles.Models.Progress
{
 public class LevelProgress : ILevelProgress
 {
  public string Key { get; }

  public LevelState Progress
  {
   get => _progress;
   private set
   {
    _progress = value;
    EventProgressUpdated?.Invoke(_progress);
   }
  }

  public event Action<LevelState> EventProgressUpdated;
  
  private LevelState _progress;
  private readonly ISave _save;
  private readonly string _key;
  
  public LevelProgress(ISave save, string key)
  {
   _save = save;
   Key = key;
   _save.Load();
  }
  
  public void Load()
  {
   Progress = _save.GetValue(_key, LevelState.Locked);
  }
  
  public void Complete()
  {
   Progress = LevelState.Completed;
   _save.SaveValue(_key, Progress);
  }

  public void Unlock()
  {
   Progress = LevelState.Unlocked;
   _save.SaveValue(_key, Progress);
  }
 }
}