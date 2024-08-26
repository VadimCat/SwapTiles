using SwapTiles.GameInput.Actions;
using SwapTiles.GameInput.Specifications;

namespace SwapTiles.GameInput.InputActions
{
 public class GameInputActionBase
 {
  private readonly ISpecification<InputResult> _spec;
  public readonly IAction Action;

  protected GameInputActionBase(ISpecification<InputResult> spec, IAction action)
  {
   _spec = spec;
   Action = action;
  }

  public bool CanHandle(InputResult inputResult)
  {
   return _spec.IsMatching(inputResult);
  }

  public void Handle(InputResult inputResult)
  {
   Action.Act(inputResult);
  }
 }
}