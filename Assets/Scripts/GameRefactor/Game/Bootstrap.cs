using Client;
using Client.Presenters;
using Client.Views;
using GameRefactor.GameInput;
using GameRefactor.Views;
using Ji2;
using Ji2.CommonCore;
using Ji2.Utils;
using Ji2Core.Core.ScreenNavigation;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameRefactor.Game
{
 public class Bootstrap : MonoBehaviour
 {
  [SerializeField] private LevelConfig testConfig;
  [SerializeField] private ScreenNavigator screenNavigator;
  [SerializeField] private TileImageView tileImagePrefab;
  [SerializeField] private UpdateService updateService;
  
  private GridField _gridField;
  private void Awake()
  {
   LevelRules rules = testConfig.Rules();

   TilePositionView.Factory positionViewFactory = PositionViewFactory(rules);
   TileRotationView.Factory rotationViewFactory = new();
   TileImageView.Factory tileImageFactory = new TileImageView.Factory(tileImagePrefab, _gridField);

   var level = new TilesLevel(rotationViewFactory, positionViewFactory, tileImageFactory, testConfig, updateService);
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