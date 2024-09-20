using Ji2.Context;
using Ji2Core.DataTypes;
using SwapTiles.Models;
using SwapTiles.Models.Interaction;
using SwapTiles.Models.Solvables;
using SwapTiles.Views;
using UnityEngine;

namespace SwapTiles.Game.Engines
{
 public class TilePositionLeveEngine : ITileLevelEngine
 {
  private readonly TilePositionView.Factory _positionViewFactory;
  private readonly DefaultTilePosition.Factory _defaultPosFactory;
  private readonly Shuffled2DArrayElements _shuffledElements;

  public TilePositionLeveEngine(Included2DArrayIndexes elements, TilePositionView.Factory positionViewFactory,
   DefaultTilePosition.Factory defaultPosFactory)
  {
   _positionViewFactory = positionViewFactory;
   _defaultPosFactory = defaultPosFactory;
   _shuffledElements = new Shuffled2DArrayElements(elements);
  }

  public ITileEngine AddEngine(Transform tileRoot, DiContext entityContext)
  {
   Vector3Int originalPos = entityContext.Get<Vector3Int>();
   ITilePosition tilePos =
    new TilePosition((Vector2Int)originalPos, _shuffledElements.ShuffledElements[(Vector2Int)originalPos]);
   tilePos = _positionViewFactory.Create(tileRoot, tilePos);
   entityContext.Register(tilePos);

   DefaultTilePosition defaultTilePosition = _defaultPosFactory.Create(tilePos, tileRoot);
   entityContext.Register(defaultTilePosition);
   return tilePos;
  }
 }
}