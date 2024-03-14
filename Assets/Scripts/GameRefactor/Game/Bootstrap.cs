using System.Collections.Generic;
using Client;
using Client.Presenters;
using Client.Views;
using GameRefactor.GameInput;
using GameRefactor.GameInput.InputActions;
using GameRefactor.GameInput.InputActions.TapSwap;
using GameRefactor.Views;
using Ji2;
using Ji2.CommonCore;
using Ji2.Context;
using Ji2.Context.Context;
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
  [SerializeField] private UpdateService updateService;

  private GridField _gridField;

  private void Awake()
  {
   DiContext levelContext = new(DiContext.GetOrCreateInstance());
   CameraProvider cameraProvider = new();
   levelContext.Register(cameraProvider);
   levelContext.Register(updateService);
   levelContext.Register(new ScreenSpacePlane(cameraProvider));

   LevelRules rules = testConfig.Rules();

   TilePositionView.Factory positionViewFactory = PositionViewFactory(rules);
   TileRotationView.Factory rotationViewFactory = new();
   TileImageView.Factory tileImageFactory = new(tileImagePrefab, _gridField);
   Entity.Factory entitiesFactory = new();
   TilesLevel level = new(rotationViewFactory, positionViewFactory, tileImageFactory, testConfig, entitiesFactory,
    _gridField, levelContext);
   level.BuildLevel();

   levelContext.Register(level);

   TouchScreenInputActions touchScreenInputActions = new();
   levelContext.Register(touchScreenInputActions);
   touchScreenInputActions.Enable();

   TapInputAction tapInputAction = new(touchScreenInputActions, updateService);
   tapInputAction.Enable();

   TilesRaycastInput tileRayCastInput = new(tapInputAction, cameraProvider);
   InputFactory inputFactory = new(levelContext);
   var actions = new List<GameInputActionBase>
   {
    inputFactory.CreateGameInputAction<SelectFirstTile>(),
    inputFactory.CreateGameInputAction<DeselectFirstTile>(),
    inputFactory.CreateGameInputAction<MoveSelectedInputAction>(),
    inputFactory.CreateGameInputAction<SwapTilesOnSwipeEnd>(),
    inputFactory.CreateGameInputAction<SwapTilesOnTapEnd>(),
   };
   InputHandler handler = new(tileRayCastInput, actions);
  }

  private TilePositionView.Factory PositionViewFactory(LevelRules rules)
  {
   _gridField = new GridField.Factory().Create(rules.Size, screenNavigator.Size, testConfig.Image.Aspect());
   TilePositionView.Factory positionViewFactory = new(_gridField);
   return positionViewFactory;
  }
 }
}