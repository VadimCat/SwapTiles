using System.Collections.Generic;
using Ji2.CommonCore.SaveDataContainer;

namespace SwapTiles.Models.Progress
{
 public class Level
 {
  public readonly string ID;
  public LevelState State { get; private set; }
  
  private readonly ISave _save;

  public Level(string id, ISave save)
  {
   ID = id;
   _save = save;
  }

  public void Unlock()
  {
   State = LevelState.Unlocked;
  }
  
  public void Complete()
  {
   State = LevelState.Completed;
  }
  
  public void Load()
  {
   State = _save.GetValue(ID, LevelState.Locked);
  }

  public void Save()
  {
   _save.SaveValue(ID, State);
  }
 }
}