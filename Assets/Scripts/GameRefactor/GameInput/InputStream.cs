using System;
using System.Collections.Generic;
using System.Linq;
using GameRefactor.GameInput.InputActions;
using UnityEngine;

namespace GameRefactor.GameInput
{
 public class InputStream: IDisposable
 {
  private readonly TilesRaycastInput _tilesRaycastInput;
  private readonly IReadOnlyCollection<GameInputActionBase> _inputActions;
  private List<GameInputActionBase> _actions;
  
  public InputStream(TilesRaycastInput tilesRaycastInput, List<GameInputActionBase> inputActions)
  {
   _tilesRaycastInput = tilesRaycastInput;
   _inputActions = inputActions;
   Reset();
   _tilesRaycastInput.EventRayCasted += OnRaycast;
  }

  private void Reset()
  {
   _actions = new List<GameInputActionBase>(_inputActions);
   foreach (var action in _inputActions)
   {
    action.Reset();
   }
  }

  private void OnRaycast(InputResult input)
  {
   GameInputActionBase[] suitableActions = _actions.Where(a => a.CanHandle(input)).ToArray();
   // _actions = new List<GameInputActionBase>(suitableActions);
   
   if (suitableActions.Select(a => a.NextAction).Distinct().Count() > 1)
   {
    throw new InconsistentNextActionException();
   }
   
   foreach (var action in suitableActions)
   {
    action.Handle(input);
   }

   foreach (var act in _actions)
   {
    act.Reset();
   }
  }

  public void Dispose()
  {
   _tilesRaycastInput.EventRayCasted -= OnRaycast;
  }
 }
}