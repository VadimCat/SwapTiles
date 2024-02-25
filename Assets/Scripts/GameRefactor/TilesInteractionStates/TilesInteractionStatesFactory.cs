using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ji2.Context;
using Ji2.States;

namespace GameRefactor.TilesInteractionStates
{
 public class TilesInteractionStatesFactory: IStateFactory
 {
  private readonly IDependenciesProvider _dependenciesProvider;

  public TilesInteractionStatesFactory(IDependenciesProvider dependenciesProvider)
  {
   _dependenciesProvider = dependenciesProvider;
  }
  public Dictionary<Type, IExitableState> GetStates(StateMachine stateMachine)
  {
   return new Dictionary<Type, IExitableState>()
   {
    [typeof(NoTilesInteracted)] = new NoTilesInteracted(stateMachine),
   };
  }
 }

 public class NoTilesInteracted: IState
 {
  private readonly IStateMachine _stateMachine;

  public NoTilesInteracted(IStateMachine stateMachine)
  {
   _stateMachine = stateMachine;
  }

  public UniTask Enter()
  {
   throw new NotImplementedException();
  }

  public UniTask Exit()
  {
   throw new NotImplementedException();
  }
 }
}