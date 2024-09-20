using Ji2.Context;
using SwapTiles.Models.Interaction;
using SwapTiles.Models.Solvables;
using SwapTiles.Views;
using UnityEngine;

namespace SwapTiles.Game.Engines
{
 public class RotationTileLevelEngine : ITileLevelEngine
 {
  private readonly int _rotationAngle;
  private readonly TileRotationView.Factory _tileRotationViewFactory;
  private readonly SwipeRotation.Factory _swipeRotationFactory;

  public RotationTileLevelEngine(int rotationAngle, TileRotationView.Factory tileRotationViewFactory,
   SwipeRotation.Factory swipeRotationFactory)
  {
   _rotationAngle = rotationAngle;
   _tileRotationViewFactory = tileRotationViewFactory;
   _swipeRotationFactory = swipeRotationFactory;
  }

  public ITileEngine AddEngine(Transform tileRoot, DiContext entityContext)
  {
   int rotationsCount = _rotationAngle == 0 ? 0 : 360 / _rotationAngle;
   int rotation = _rotationAngle == 0 ? 0 : Random.Range(0, rotationsCount) * _rotationAngle;
   ITileRotation tileRotation = new TileRotation(rotation, rotationsCount);
   tileRotation = _tileRotationViewFactory.Create(tileRoot, tileRotation);
   entityContext.Register(tileRotation);
   SwipeRotation swipeRotation = _swipeRotationFactory.Create(tileRotation, tileRoot);
   entityContext.Register(swipeRotation);
   return tileRotation;
  }
 }
}