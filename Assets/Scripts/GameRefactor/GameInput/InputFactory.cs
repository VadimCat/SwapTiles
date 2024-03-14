using System;
using System.Diagnostics.CodeAnalysis;
using GameRefactor.Game;
using GameRefactor.GameInput.Actions;
using GameRefactor.GameInput.InputActions;
using GameRefactor.GameInput.InputActions.TapSwap;
using GameRefactor.GameInput.Specifications;
using GameRefactor.Models.Interaction;
using Ji2.Context;
using UnityEngine.InputSystem;

namespace GameRefactor.GameInput
{
 public class InputFactory
 {
  private readonly DiContext _diContext;

  public InputFactory(IDependenciesProvider dependenciesProvider)
  {
   _diContext = new DiContext(dependenciesProvider);
  }

  [SuppressMessage("ReSharper", "ConvertTypeCheckPatternToNullCheck")]
  public GameInputActionBase CreateGameInputAction<TInputAction>() where TInputAction : GameInputActionBase
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
    Type t when t == typeof(SwapTilesOnSwipeEnd) => SwapTilesOnSwipeEnd(),
    Type t when t == typeof(SwapTilesOnTapEnd) => SwapTilesOnTapEnd(),
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
    TAction result = new MoveTileAction(_diContext.GetService<ScreenSpacePlane>()) as TAction;
    _diContext.Register(result);
    return result;
   }

   if (typeof(TAction) == typeof(TrySwapTilesByPos))
   {
    TAction result =
     new TrySwapTilesByPos(_diContext.GetService<TilesGrid>(), _diContext.GetService<ScreenSpacePlane>()) as TAction;
    _diContext.Register(result);
    return result;
   }

   if (typeof(TAction) == typeof(SwipeWithSelected))
   {
    TAction result =
     new SwipeWithSelected(_diContext.GetService<CurrentSelection>()) as TAction;
    _diContext.Register(result);
    return result;
   }

   throw new NotImplementedException();
  }

  private SwapTilesOnTapEnd SwapTilesOnTapEnd()
  {
   SwapTilesOnTapEnd result = new(
    new TouchPhaseSpec(TouchPhase.Ended,
     new IsSelected(false,
      new HasTarget(true,
       new IsAnySelected(_diContext.GetService<CurrentSelection>(),
        true,
        new True())))), new SwipeWithSelected(_diContext.GetService<CurrentSelection>()));

   _diContext.Register(result);

   return result;
  }

  private SwapTilesOnSwipeEnd SwapTilesOnSwipeEnd()
  {
   SwapTilesOnSwipeEnd result = new(
    new TouchPhaseSpec(TouchPhase.Ended,
     new IsSelected(true, 
      new IsDefaultPosition(false, 
       new HasTarget(true, 
        new True())))),
    CreateAction<TrySwapTilesByPos>());
   _diContext.Register(result);
   return result;
  }

  private MoveSelectedInputAction MoveSelectedInputAction()
  {
   MoveSelectedInputAction result = new(new TouchPhaseSpec(TouchPhase.Moved,
     new IsSelected(true,
      new HasTarget(true,
       new True()))),
    CreateAction<MoveTileAction>());

   _diContext.Register(result);

   return result;
  }

  private SelectFirstTile SelectTileInputAction()
  {
   SelectFirstTile result = new(
    new IsAnySelected(_diContext.GetService<CurrentSelection>(), false,
     new TouchPhaseSpec(TouchPhase.Began,
      new IsSelected(false,
       new HasTarget(true,
        new True())))),
    CreateAction<SelectTileAction>());

   _diContext.Register(result);

   return result;
  }

  private DeselectFirstTile DeselectTileInputAction()
  {
   DeselectFirstTile result = new(new TouchPhaseSpec(TouchPhase.Began,
     new IsSelected(true,
      new HasTarget(true,
       new True()))),
    CreateAction<DeselectTileAction>());

   _diContext.Register(result);

   return result;
  }
 }
}