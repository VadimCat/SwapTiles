using Ji2.Context;
using Models.Solvables;
using UnityEngine;

namespace Tiles.Engines
{
 public interface ITileLevelEngine
 {
  public ITileEngine AddEngine(Transform tileRoot, DiContext entityContext);
 }
}