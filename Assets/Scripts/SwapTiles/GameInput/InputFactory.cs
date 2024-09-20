using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Ji2.Context;
using Ji2.Presenters;
using SwapTiles.Game;
using SwapTiles.Game.Level;
using SwapTiles.Game.LevelEngines;
using SwapTiles.GameInput.Actions;
using SwapTiles.GameInput.Actions.Rotation;
using SwapTiles.GameInput.InputActions;
using SwapTiles.GameInput.InputActions.Common;
using SwapTiles.GameInput.InputActions.Rotate;
using SwapTiles.GameInput.InputActions.SwipeSwap;
using SwapTiles.GameInput.InputActions.TapSwap;
using SwapTiles.GameInput.Specifications;
using SwapTiles.Models.Interaction;
using UnityEngine.InputSystem;

namespace SwapTiles.GameInput
{
 public class InputFactory
 {
  private readonly DiContext _diContext;

  public InputFactory(IDependenciesProvider dependenciesProvider)
  {
   _diContext = new DiContext(dependenciesProvider);
   _diContext.Register(new InputLock());
   _diContext.Register(new RotationLockSource());
  }

  public ExclusiveTileInput Input(LevelConfig levelConfig)
  {
   List<GameInputActionBase> actions = new List<GameInputActionBase>();
   foreach (IRules engine in levelConfig.Engines())
   {
    switch (engine)
    {
     case PositionRules:
      actions.Add(CreateGameInputAction<SelectFirstTile>());
      actions.Add(CreateGameInputAction<DeselectFirstTile>());
      actions.Add(CreateGameInputAction<MoveSelectedInputAction>());
      actions.Add(CreateGameInputAction<SwapTilesOnSwipeEnd>());
      actions.Add(CreateGameInputAction<SwapTilesOnTapEnd>());
      break;
     case RotationRules:
      actions.Add(CreateGameInputAction<RotationSwipeOnTouchMove>());
      actions.Add(CreateGameInputAction<SwipeEndOnTouchEnd>());
      break;
    }
   }
   return new(_diContext.Get<TileInput>(), actions);
  }
  
  [SuppressMessage("ReSharper", "ConvertTypeCheckPatternToNullCheck")]
  private GameInputActionBase CreateGameInputAction<TInputAction>() where TInputAction : GameInputActionBase
  {
   if (_diContext.TryGetService(out TInputAction action))
   {
    return action;
   }

   return typeof(TInputAction) switch
   {
    Type t when t == typeof(SelectFirstTile) => SelectTileInputAction(),
    Type t when t == typeof(DeselectFirstTile) => DeselectTileInputAction(),
    Type t when t == typeof(MoveSelectedInputAction) => MoveSelectedInputAction(),
    Type t when t == typeof(RotationSwipeOnTouchMove) => RotationSwipeOnTouchMove(),
    Type t when t == typeof(SwapTilesOnTapEnd) => SwapTilesOnTapEnd(),
    Type t when t == typeof(SwapTilesOnSwipeEnd) => SwapTilesOnSwipeEnd(),
    Type t when t == typeof(SwipeEndOnTouchEnd) => SwipeEndOnTouchEnd(),
    _ => throw new NotImplementedException(),
   };
  }

  private TAction CreateAction<TAction>() where TAction : class, IAction
  {
   if (_diContext.TryGetService(out TAction action))
   {
    return action;
   }

   if (typeof(TAction) == typeof(SelectTileAction))
   {
    TAction result = new SelectTileAction() as TAction;
    _diContext.Register(result);
    return result;
   }

   if (typeof(TAction) == typeof(DeselectTileAction))
   {
    TAction result = new DeselectTileAction() as TAction;
    _diContext.Register(result);
    return result;
   }

   if (typeof(TAction) == typeof(MoveTileAction))
   {
    TAction result =
     new MoveTileAction(
      _diContext.Get<ScreenSpacePlane>(),
      _diContext.Get<InputLock>()) as TAction;
    _diContext.Register(result);
    return result;
   }

   if (typeof(TAction) == typeof(RotationSwipeUpdate))
   {
    TAction result = new RotationSwipeUpdate(_diContext.Get<CurrentSelection>())
      .Lockable(_diContext.Get<InputLock>(), _diContext.Get<RotationLockSource>())
     as TAction;

    _diContext.Register(result);
    return result;
   }

   if (typeof(TAction) == typeof(EndRotationSwipe))
   {
    TAction result = new EndRotationSwipe(_diContext.Get<InputLock>(),
     _diContext.Get<RotationLockSource>(), _diContext.Get<CurrentSelection>()) as TAction;
    _diContext.Register(result);
    return result;
   }

   if (typeof(TAction) == typeof(TrySwapTilesByPos))
   {
    TAction result =
     new TrySwapTilesByPos(_diContext.Get<TilesGrid>(), _diContext.Get<ScreenSpacePlane>()) as TAction;
    _diContext.Register(result);
    return result;
   }

   if (typeof(TAction) == typeof(SwipeWithSelected))
   {
    TAction result =
     new SwipeWithSelected(_diContext.Get<CurrentSelection>()) as TAction;
    _diContext.Register(result);
    return result;
   }

   throw new NotImplementedException();
  }

  private RotationSwipeOnTouchMove RotationSwipeOnTouchMove()
  {
   RotationSwipeOnTouchMove result = new(SwipeRotateSpec(), RotationSwipeUpdate());

   _diContext.Register(result);
   return result;
  }

  private IAction RotationSwipeUpdate()
  {
   return new RotationSwipeUpdate(_diContext.Get<CurrentSelection>())
    .AnimationExclusive(_diContext.Get<AnimationQueue>())
    .Lockable(_diContext.Get<InputLock>(), _diContext.Get<RotationLockSource>());
  }

  private SelectFirstTile SelectTileInputAction()
  {
   SelectFirstTile result = new(
    ISpecification<InputResult>.Specification
     .TouchPhase(TouchPhase.Began)
     .HasTarget(true)
     .IsAnySelected(_diContext.Get<CurrentSelection>(), false)
     .IsSelected(false),
    CreateAction<SelectTileAction>());

   _diContext.Register(result);

   return result;
  }

  private DeselectFirstTile DeselectTileInputAction()
  {
   DeselectFirstTile result = new(
    ISpecification<InputResult>.Specification
     .TouchPhase(TouchPhase.Began)
     .HasTarget(true)
     .IsSelected(true),
    CreateAction<DeselectTileAction>());

   _diContext.Register(result);

   return result;
  }

  private ISpecification<InputResult> SwipeRotateSpec()
  {
   ISpecification<InputResult> spec1 = ISpecification<InputResult>.Specification
    .TouchPhase(TouchPhase.Moved)
    .IsAnySelected(_diContext.Get<CurrentSelection>(), true)
    .HasTarget(false);

   ISpecification<InputResult> spec2 = ISpecification<InputResult>.Specification
    .TouchPhase(TouchPhase.Moved)
    .IsAnySelected(_diContext.Get<CurrentSelection>(), true)
    .HasTarget(true)
    .IsSelected(false);

   return new Or<InputResult>(spec1, spec2).SpecLog("SwipeRotateSpec");
  }

  private MoveSelectedInputAction MoveSelectedInputAction()
  {
   MoveSelectedInputAction result = new(
    ISpecification<InputResult>.Specification
     .TouchPhase(TouchPhase.Moved)
     .HasTarget(true)
     .IsSelected(true)
     .CanInteract(_diContext.Get<InputLock>(), null, true),
    CreateAction<MoveTileAction>());

   _diContext.Register(result);

   return result;
  }

  private SwapTilesOnSwipeEnd SwapTilesOnSwipeEnd()
  {
   SwapTilesOnSwipeEnd result = new(
    ISpecification<InputResult>.Specification
     .TouchPhase(TouchPhase.Ended)
     .HasTarget(true)
     .IsSelected(true)
     .IsAnySelected(_diContext.Get<CurrentSelection>(), true)
     .CanInteract(_diContext.Get<InputLock>(), null, true)
     .IsDefaultPosition(false),
    CreateAction<TrySwapTilesByPos>());
   _diContext.Register(result);
   return result;
  }

  private SwapTilesOnTapEnd SwapTilesOnTapEnd()
  {
   SwapTilesOnTapEnd result = new(
    ISpecification<InputResult>.Specification
     .TouchPhase(TouchPhase.Ended)
     .HasTarget(true)
     .IsSelected(false)
     .CanInteract(_diContext.Get<InputLock>(), null, true)
     .IsAnySelected(_diContext.Get<CurrentSelection>(), true),
    new SwipeWithSelected(_diContext.Get<CurrentSelection>()));

   _diContext.Register(result);

   return result;
  }

  private SwipeEndOnTouchEnd SwipeEndOnTouchEnd()
  {
   SwipeEndOnTouchEnd result = new(
    ISpecification<InputResult>.Specification
     .TouchPhase(TouchPhase.Ended)
     .IsAnySelected(_diContext.Get<CurrentSelection>(), true)
     .IsBlockedBy(_diContext.Get<InputLock>(), _diContext.Get<RotationLockSource>()),
    CreateAction<EndRotationSwipe>());

   _diContext.Register(result);
   return result;
  }
 }
}