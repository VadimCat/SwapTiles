using System;
using System.Collections.Generic;
using System.Linq;
using GameRefactor.GameInput.InputActions;

namespace GameRefactor.GameInput
{
 public class InputHandler: IDisposable
 {
  private readonly TilesRaycastInput _tilesRaycastInput;
  private readonly IReadOnlyCollection<GameInputActionBase> _inputActions;
  private List<GameInputActionBase> _suitableActions;
  public InputHandler(TilesRaycastInput tilesRaycastInput, List<GameInputActionBase> inputActions)
  {
   _tilesRaycastInput = tilesRaycastInput;
   _inputActions = inputActions;
   _tilesRaycastInput.EventRayCasted += OnRaycast;
  }

  private void OnRaycast(InputResult input)
  {
   GameInputActionBase[] suitableActions = _inputActions.Where(a => a.CanHandle(input)).ToArray();
   
   if (suitableActions.Select(a => a.Action).Distinct().Count() > 1)
   {
    throw new InconsistentNextActionException();
   }

   if (suitableActions.Length > 0)
   {
    
   }
   foreach (GameInputActionBase action in suitableActions)
   {
    action.Handle(input);
   }
  }

  public void Dispose()
  {
   _tilesRaycastInput.EventRayCasted -= OnRaycast;
  }
 }
}