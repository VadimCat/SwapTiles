using System;
using System.Collections.Generic;
using System.Linq;
using Input.InputActions;
using Tiles.Input;

namespace Input
{
 public class ExclusiveTileInput: IDisposable
 {
  private readonly TileInput _tileInput;
  private readonly IReadOnlyCollection<GameInputActionBase> _inputActions;
  public ExclusiveTileInput(TileInput tileInput, List<GameInputActionBase> inputActions)
  {
   _tileInput = tileInput;
   _inputActions = inputActions;
   _tileInput.EventRayCasted += OnRaycast;
  }

  private void OnRaycast(InputResult input)
  {
   GameInputActionBase[] suitableActions = _inputActions.Where(a => a.CanHandle(input)).ToArray();
   
   if (suitableActions.Select(a => a.Action).Distinct().Count() > 1)
   {
    throw new InconsistentNextActionException(suitableActions);
   }
   if (suitableActions.Length > 0)
   {
    suitableActions[0].Handle(input);
   }
  }

  public void Dispose()
  {
   _tileInput.EventRayCasted -= OnRaycast;
  }
 }
}