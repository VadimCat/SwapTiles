using System.Collections.Generic;
using Client;
using Client.Presenters;
using Client.Views;
using GameRefactor.Interfaces;
using GameRefactor.Models;
using GameRefactor.Views;
using Ji2.Utils.Shuffling;
using UnityEngine;

namespace GameRefactor.Game
{
 public class TilesLevel
 {
  private readonly TileRotationView.Factory _rotationViewFactory;
  private readonly TilePositionView.Factory _positionViewFactory;
  private readonly TileImageView.Factory _tileImageFactory;
  private readonly GridField _gridField;
  private readonly LevelConfig _levelConfig;
  private readonly List<ITileSolvable> _solvableItems;
  private readonly LevelRules _rules;
  private readonly bool[,] _template;

  public TilesLevel(TileRotationView.Factory rotationViewFactory, TilePositionView.Factory positionViewFactory,
   TileImageView.Factory tileImageFactory, LevelConfig levelConfig)
  {
   _rotationViewFactory = rotationViewFactory;
   _positionViewFactory = positionViewFactory;
   _tileImageFactory = tileImageFactory;
   _levelConfig = levelConfig;

   _rules = levelConfig.Rules();
   _template = _rules.CutTemplate;
   _solvableItems = new List<ITileSolvable>(_template.GetLength(0) * _template.GetLength(1));
  }
  
  public void BuildLevel()
  {
   LevelRules data = _levelConfig.Rules();
   var cutTemplate = data.CutTemplate;

   ShuffledV2IntArray shuffledV2IntArray = new ShuffledV2IntArray(cutTemplate);
   foreach (var element in shuffledV2IntArray.ShuffledElements)
   {
    _solvableItems.Add(CreateItem(element.original, element.shuffled));
   }
  }

  private ITileSolvable CreateItem(Vector3Int current, Vector3Int position)
  {
   TileImageView tileImage =
    _tileImageFactory.Create(_levelConfig.Image, current, _template.GetLength(0), _template.GetLength(1));

   List<ITileSolvable> solvables = new List<ITileSolvable>();
   
   ITilePosition tilePos = new TilePosition(current, position);
   tilePos = _positionViewFactory.Create(tileImage.gameObject, tilePos);
   solvables.Add(tilePos);
   
   if (_rules.RotationAngle != 0)
   {
    int rotationsCount = _rules.RotationAngle == 0 ? 0 : 360 / _rules.RotationAngle;
    int rotation = _rules.RotationAngle == 0 ? 0 : Random.Range(0, rotationsCount) * _rules.RotationAngle;
    ITileRotation tileRotation = new TileRotation(rotation, rotationsCount);
    tileRotation = _rotationViewFactory.Create(tileImage.gameObject, tileRotation);
    solvables.Add(tileRotation);
   }

   return new TileComposite(solvables);
  }
 }
}