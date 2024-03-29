using System.Collections.Generic;
using System.Linq;
using Client;
using Client.Views;
using GameRefactor.GameInput;
using GameRefactor.Interfaces;
using GameRefactor.Models;
using GameRefactor.Models.Interaction;
using GameRefactor.Views;
using Ji2.Context;
using Ji2.Context.Context;
using UnityEngine;

namespace GameRefactor.Game
{
 public class TilesLevel
 {
  private readonly TileRotationView.Factory _rotationViewFactory;
  private readonly TilePositionView.Factory _positionViewFactory;
  private readonly TileImageView.Factory _tileImageFactory;
  private readonly LevelConfig _levelConfig;
  private readonly Entity.Factory _entitiesFactory;
  private readonly GridField _gridField;
  private readonly IDependenciesController _levelContext;
  private readonly List<ITileSolvable> _solvableItems;
  private readonly LevelRules _rules;
  private readonly bool[,] _template;
  private readonly SelectableView.Factory _selectablesFactory;
  private readonly List<Entity> _tileEntities;
  
  public TilesLevel(TileRotationView.Factory rotationViewFactory, TilePositionView.Factory positionViewFactory,
   TileImageView.Factory tileImageFactory, LevelConfig levelConfig, Entity.Factory entitiesFactory, GridField gridField, IDependenciesController levelContext)
  {
   _rotationViewFactory = rotationViewFactory;
   _positionViewFactory = positionViewFactory;
   _tileImageFactory = tileImageFactory;
   _selectablesFactory = new SelectableView.Factory();
   _levelConfig = levelConfig;
   _entitiesFactory = entitiesFactory;
   _gridField = gridField;
   _levelContext = levelContext;

   _rules = levelConfig.Rules();
   _template = _rules.CutTemplate;
   int maxTilesAmount = _template.GetLength(0) * _template.GetLength(1);
   _solvableItems = new List<ITileSolvable>(maxTilesAmount);
   _tileEntities = new List<Entity>(maxTilesAmount);
  }

  public void BuildLevel()
  {
   LevelRules data = _levelConfig.Rules();
   var cutTemplate = data.CutTemplate;

   ShuffledV2IntArray shuffledV2IntArray = new(cutTemplate);
   foreach ((Vector3Int original, Vector3Int shuffled) element in shuffledV2IntArray.ShuffledElements)
   {
    _solvableItems.Add(CreateItem(element.original, element.shuffled));
   }

   CurrentSelection selection = new(_tileEntities);
   TilesGrid tilesGrid = new(_gridField, _tileEntities);
   _levelContext.Register(tilesGrid);
   _levelContext.Register(selection);
  }
  

  private ITileSolvable CreateItem(Vector3Int current, Vector3Int position)
  {
   //TO DO: try to hide local context instant object factory
   DiContext entityContext = new(null);
   TileImageView tileImage =
    _tileImageFactory.Create(_levelConfig.Image, current, _template.GetLength(0), _template.GetLength(1));

   ISelectable selectable = _selectablesFactory.Create(tileImage.gameObject, new Selectable());
   entityContext.Register(selectable);
   List<ITileSolvable> solvables = new List<ITileSolvable>();

   ITilePosition tilePos = new TilePosition(current, position);
   tilePos = _positionViewFactory.Create(tileImage.gameObject, tilePos);
   solvables.Add(tilePos);
   entityContext.Register(tilePos);

   DefaultTilePosition defaultTilePosition = new(tilePos, tileImage.transform, _gridField);
   entityContext.Register(defaultTilePosition);
   if (_rules.RotationAngle != 0)
   {
    int rotationsCount = _rules.RotationAngle == 0 ? 0 : 360 / _rules.RotationAngle;
    int rotation = _rules.RotationAngle == 0 ? 0 : Random.Range(0, rotationsCount) * _rules.RotationAngle;
    ITileRotation tileRotation = new TileRotation(rotation, rotationsCount);
    tileRotation = _rotationViewFactory.Create(tileImage.gameObject, tileRotation);
    entityContext.Register(tileRotation);
    solvables.Add(tileRotation);
   }
   Entity entity = _entitiesFactory.Create(tileImage.gameObject, entityContext);
   _tileEntities.Add(entity);
   return new TileComposite(solvables);
  }
 }
}