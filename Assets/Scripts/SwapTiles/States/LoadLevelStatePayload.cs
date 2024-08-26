using SwapTiles.Game.Level;

namespace SwapTiles.States
{
 public record LoadLevelStatePayload(LevelConfig LevelConfig, float FakeLoadingTime);
}