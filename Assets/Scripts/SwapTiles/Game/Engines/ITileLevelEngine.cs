using Ji2.Context;
using SwapTiles.Models.Solvables;
using UnityEngine;

namespace SwapTiles.Game.Engines
{
 public interface ITileLevelEngine
 {
  public ITileEngine AddEngine(Transform tileRoot, DiContext entityContext);
 }
}