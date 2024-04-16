using System;
using Client;
using Models.Interaction;
using Views;

namespace Tiles.Engines
{
 public class EnginesFactory
 {
  private readonly TilePositionView.Factory _positionViewFactory;
  private readonly DefaultTilePosition.Factory _defaultPosFactory;
  private readonly TileRotationView.Factory _tileRotationViewFactory;
  private readonly SwipeRotation.Factory _swipeRotationFactory;

  public EnginesFactory(TilePositionView.Factory positionViewFactory, DefaultTilePosition.Factory defaultPosFactory,
   TileRotationView.Factory tileRotationViewFactory, SwipeRotation.Factory swipeRotationFactory)
  {
   _positionViewFactory = positionViewFactory;
   _defaultPosFactory = defaultPosFactory;
   _tileRotationViewFactory = tileRotationViewFactory;
   _swipeRotationFactory = swipeRotationFactory;
  }

  public ITileLevelEngine Create(IRules rules)
  {
   switch (rules)
   {
    case PositionRules posRules:
     return new TilePositionLeveEngine(posRules.IncludedIndexes, _positionViewFactory, _defaultPosFactory);
    case RotationRules rotationRules:
     return new RotationTileLevelEngine(rotationRules.Angle, _tileRotationViewFactory, _swipeRotationFactory);
    default:
     throw new NotImplementedException();
   }
  }
 }
}