using System;
using System.Collections.Generic;
using Client;
using Client.Views;
using Ji2;
using Tiles.Engines;
using Ji2.Context;
using Ji2.Context.Context;
using Ji2.Presenters;
using Ji2.Utils;
using Ji2Core.Core.ScreenNavigation;
using Ji2Core.DataTypes;
using Ji2Core.DataTypes.Array;
using Models.Interaction;
using Models.Solvables;
using UnityEngine;
using Views;

namespace Tiles
{
 public class TilesLevelFactory
 {
  private readonly DiContext _diContext;

  public TilesLevelFactory(DiContext diContext)
  {
   _diContext = diContext;
  }

  public TilesLevel BuildLevel(LevelConfig levelConfig)
  {
   IArray2D<bool> cutTemplate = levelConfig.LevelSettings().CutTemplate;
   int columns = cutTemplate.GetLength(0);
   int rows = cutTemplate.GetLength(1);
   int maxAmount = columns * rows;
   var gridFieldFactory = _diContext.Get<GridField.Factory>();
   GridField gridField = gridFieldFactory.Create(columns, rows, _diContext.Get<ScreenNavigator>().ScreenSize, 
    levelConfig.Image.Aspect());

   TileImageView.Factory tileImageFactory = 
    new(_diContext.Get<TileImageView>(), gridField, levelConfig.Image, columns, rows);
   EnginesFactory enginesFactory = new(new(gridField),
    new(gridField),
    new(_diContext.Get<AnimationQueue>()),
    new(_diContext.Get<CameraProvider>()));
   
   IEnumerable<IRules> rules = levelConfig.Engines();
   
   List<ITileEngine> solvableItems = new(maxAmount);
   List<Entity> tileEntities = new(maxAmount);
   
   List<ITileLevelEngine> engines = new(2);
   
   foreach (IRules rule in rules)
   {
    engines.Add(enginesFactory.Create(rule));
   }
   
   SelectableView.Factory selectablesFactory = new();
   Included2DArrayIndexes included2DElements = new(levelConfig.LevelSettings().CutTemplate);
   Entity.Factory entitiesFactory = _diContext.Get<Entity.Factory>();
   foreach (Vector2Int element in included2DElements)
   {
    solvableItems.Add(CreateItem((Vector3Int)element));
   }

   CurrentSelection selection = new(tileEntities);
   TilesGrid tilesGrid = new(gridField, tileEntities);
   _diContext.Register(tilesGrid);
   _diContext.Register(selection);
   
   return new TilesLevel(solvableItems);

   ITileEngine CreateItem(Vector3Int currentPos)
   {
    DiContext entityContext = new(null);
    TileImageView tileImage =
     tileImageFactory.Create(currentPos);
    GameObject tileRoot = tileImage.gameObject;
    entityContext.Register(currentPos);
    AddSelectable(tileRoot, entityContext);
    Transform tileRootTransform = tileRoot.transform;

    List<ITileEngine> solveEngines = new();

    foreach (var engine in engines)
    {
     solveEngines.Add(engine.AddEngine(tileRootTransform, entityContext));
    }

    Entity entity = entitiesFactory.Create(tileRoot, entityContext);
    tileEntities.Add(entity);
    return new TileCompositeEngine(solveEngines);
   }
   
   void AddSelectable(GameObject tileViewRoot, DiContext entityContext)
   {
    ISelectable selectable = selectablesFactory.Create(tileViewRoot.gameObject, new Selectable());
    entityContext.Register(selectable);
   }
  }
 }

 public class TilesLevel: IDisposable
 {
  public event Action LevelCompleted;
  private readonly List<ITileEngine> _solvableItems;
  
  public TilesLevel(List<ITileEngine> solvableItems)
  {
   _solvableItems = solvableItems;
   foreach (var item in solvableItems)
   {
    item.EventIsCompletedUpdated += CheckComplete;
   }
  }

  private void CheckComplete(bool completed)
  {
   bool levelCompleted = _solvableItems.TrueForAll(i => i.IsCompleted);
   if (levelCompleted)
   {
    LevelCompleted?.Invoke();
   }
  }

  public void Dispose()
  {
   foreach (var item in _solvableItems)
   {
    item.EventIsCompletedUpdated -= CheckComplete;
   }
  }
 }
}