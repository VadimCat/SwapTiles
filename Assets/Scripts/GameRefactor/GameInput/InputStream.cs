using System.Collections.Generic;
using System.Linq;

namespace GameRefactor.GameInput
{
 public class InputStream
 {
  private readonly TilesRaycastInput _tilesRaycastInput;
  private readonly IReadOnlyCollection<IInputAction> _actionsOrigin;
  private List<IInputAction> _actions;
  
  public InputStream(TilesRaycastInput tilesRaycastInput, List<IInputAction> actionsOrigin)
  {
   _tilesRaycastInput = tilesRaycastInput;
   _actionsOrigin = actionsOrigin;
   Reset();
   _tilesRaycastInput.EventRayCasted += OnRaycast;
  }

  private void Reset()
  {
   _actions = new List<IInputAction>(_actionsOrigin);
   foreach (var action in _actionsOrigin)
   {
    action.Reset();
   }
  }

  private void OnRaycast(InputResult input)
  {
   if (_actions.Count == 0)
   {
    Reset();
   }
   var suitableActions = _actionsOrigin.Where(a => a.CanHandle(input)).ToArray();
   _actions.RemoveAll(a => suitableActions.Contains(a));
   if (suitableActions.Select(a => a.NextAction).Distinct().Count() != 1)
   {
    throw new InconsistentNextActionException();
   }

   foreach (var action in suitableActions)
   {
    action.Handle(input);
   }
  }
 }
}