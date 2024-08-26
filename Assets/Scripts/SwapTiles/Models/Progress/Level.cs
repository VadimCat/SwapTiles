using System.Collections.Generic;
using Ji2.CommonCore.SaveDataContainer;

namespace SwapTiles.Models.Progress
{
 public class Level
 {
  public readonly string ID;
  public LevelStatus Status { get; private set; }
  
  private readonly ISave _save;

  public Level(string id, ISave save)
  {
   ID = id;
   _save = save;
  }

  public void Unlock()
  {
   Status = LevelStatus.Unlocked;
  }
  
  public void Complete()
  {
   Status = LevelStatus.Completed;
  }
  
  public void Load()
  {
   Status = _save.GetValue(ID, LevelStatus.Locked);
  }

  public void Save()
  {
   _save.SaveValue(ID, Status);
  }
 }
}