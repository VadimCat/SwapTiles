using Client;
using Client.Views;
using GameRefactor.Views;
using Ji2.Utils;
using Ji2Core.Core.ScreenNavigation;
using UnityEngine;

namespace GameRefactor.Game
{
 public class Bootstrap : MonoBehaviour
 {
  [SerializeField] private LevelConfig testConfig;
  [SerializeField] private ScreenNavigator screenNavigator;
  [SerializeField] private TileImageView tileImagePrefab;

  private GridField _gridField;
  private void Awake()
  {
   var rules = testConfig.Rules();
   TileRotationView.Factory rotationViewFactory = new();
   var positionViewFactory = PositionViewFactory(rules);
   
   TileImageView.Factory tileImageFactory = new TileImageView.Factory(tileImagePrefab, _gridField);
   var level = new TilesLevel(rotationViewFactory, positionViewFactory, tileImageFactory, testConfig);
   level.BuildLevel();
  }

  private TilePositionView.Factory PositionViewFactory(LevelRules rules)
  {
   _gridField = new GridField.Factory().Create(rules.Size, screenNavigator.Size, testConfig.Image.Aspect());
   TilePositionView.Factory positionViewFactory = new(_gridField);
   return positionViewFactory;
  }
 }
}