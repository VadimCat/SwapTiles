using System.Collections.Generic;
using Ji2.CommonCore.SaveDataContainer;
using SwapTiles.Game.Level;

namespace SwapTiles.Models.Progress
{
 public class LevelsRepository
 {
  public IReadOnlyCollection<Level> Levels => _levels;
  
  private readonly LevelsConfig _config;
  private readonly ISave _save;
  private Level[] _levels;
  
  public LevelsRepository(LevelsConfig config, ISave save)
  {
   _config = config;
   _save = save;
  }
  
  public void Load()
  {
   _levels = new Level[_config.Levels.Count];
   for (var i = 0; i < _config.Levels.Count; i++)
   {
    _levels[i] = new Level(_config.Levels[i], _save);
    _levels[i].Load();
   }

   TryUnlockFirst();
  }

  private void TryUnlockFirst()
  {
   if(_levels[0].State == LevelState.Locked)
   {
    _levels[0].Unlock();
    _levels[0].Save();
   }
  }
 }
}