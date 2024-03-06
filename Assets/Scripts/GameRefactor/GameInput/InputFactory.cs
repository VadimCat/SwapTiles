using System;
using GameRefactor.Game;
using GameRefactor.GameInput.Actions;
using GameRefactor.GameInput.InputActions;
using GameRefactor.GameInput.Specifications;
using Ji2.Context;

namespace GameRefactor.GameInput
{
 public class InputFactory
 {
  private readonly DiContext _diContext;

  public InputFactory(IDependenciesProvider dependenciesProvider)
  {
   _diContext = new DiContext(dependenciesProvider);
  }

  public GameInputActionBase CreateGameInputAction<TInputAction>() where TInputAction : GameInputActionBase
  {
   if (_diContext.TryGetService(out TInputAction action))
   {
    return action;
   }

   return typeof(TInputAction) switch
   {
    Type t when t == typeof(SelectTileInputAction) => SelectTileInputAction(),
    Type t when t == typeof(MoveSelectedInputAction) => MoveSelectedInputAction(),
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

   throw new NotImplementedException();
  }

  private TSpec CreateSpec<TSpec>() where TSpec : class, ISpecification
  {
   if (_diContext.TryGetService(out TSpec action))
   {
    return action;
   }

   if (typeof(TSpec) == typeof(BeganUnselectedTargetSpecification))
   {
    TSpec result = new BeganUnselectedTargetSpecification() as TSpec;
    _diContext.Register(result);
    return result;
   }

   if (typeof(TSpec) == typeof(EndSelectedTargetSpecification))
   {
    TSpec result = new EndSelectedTargetSpecification() as TSpec;
    _diContext.Register(result);
    return result;
   }

   if (typeof(TSpec) == typeof(MoveSelectedTargetSpecification))
   {
    TSpec result = new MoveSelectedTargetSpecification() as TSpec;
    _diContext.Register(result);
    return result;
   }

   throw new NotImplementedException();
  }

  private SwapTilesOnTapEnd SwapTilesOnTapEnd()
  {
   SwapTilesOnTapEnd result = new(CreateAction<TrySwapTilesByPos>(),
    CreateSpec<EndSelectedTargetSpecification>());
   _diContext.Register(result);
   return result;
  }

  private MoveSelectedInputAction MoveSelectedInputAction()
  {
   MoveSelectedInputAction result = new(CreateSpec<MoveSelectedTargetSpecification>(), CreateAction<MoveTileAction>());
   _diContext.Register(result);
   return result;
  }

  private SelectTileInputAction SelectTileInputAction()
  {
   SelectTileInputAction result = new(CreateSpec<BeganUnselectedTargetSpecification>(),
    CreateAction<SelectTileAction>());
   _diContext.Register(result);
   return result;
  }
 }
}